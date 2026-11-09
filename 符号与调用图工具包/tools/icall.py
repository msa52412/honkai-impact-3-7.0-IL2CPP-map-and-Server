# -*- coding: utf-8 -*-
"""icall —— 在游戏主线程里调用任意函数（常驻指令槽）

为什么是这个架构
----------------
远程线程被拦截（CreateRemoteThread 退出码 0xFFFFFFFF），只能挂到游戏现有线程上。
但每调一个函数就「打钩子→还原」一次，等于反复改 14 字节代码入口，
既有碰撞风险，又要重复做同一件事。

所以改成常驻信箱（mailbox）：钩子只打一次，stub 常驻。
之后每次调用只是往信箱里写 target/args/pending，游戏线程下一帧自己取走执行。

    0x00  u8   pending      0xA5 = 有新命令待执行（其它任何值一律当「没命令」）
    0x01  u8   done         1 = 执行完毕 / 2 = stub 拒绝执行（target 为 0）
    0x02  u8   nstack       栈参数个数（0..16）
    0x03  u8   xmm_mask     槽 0..3 中哪些是浮点（仅记录，stub 不读）
    0x08  u64  target       要调用的函数绝对地址
    0x10  u64  arg0         → rcx
    0x18  u64  arg1         → rdx
    0x20  u64  arg2         → r8
    0x28  u64  arg3         → r9
    0x30  u64  ret          ← rax
    0x38  u64  capture_this 捕获插件用（与调用无关）
    0x40  u64  xmm0         → xmm0（低 64 位；float32 只占低 32 位）
    0x48  u64  xmm1
    0x50  u64  xmm2
    0x58  u64  xmm3
    0x60  u64  ret_xmm0     ← xmm0（浮点返回值）
    0x68  u64  ret_kind     0=整数 1=浮点
    0x70  u64  stack[16]    第 5..20 个参数（每个 8 字节，浮点也占一格）
    0xF0  ..   保留

内存布局（一次分配 0x8000，整局复用）
    0x0000  信箱（0x100 字节）
    0x1000  stub（通用版约 480 字节）
    0x2000  scratch（0x2000 字节，0x2000~0x3FFF）
    0x4000  每个被 hook 的函数占一页（0x4000、0x5000、0x6000、0x7000）

注意：scratch 只能到 0x3FFF。0x4000 起是钩子槽，scratch 越界写会把捕获 stub 打烂。
（旧版文档写「scratch 0x6000 字节」，与 HOOK_BASE_OFF=0x4000 冲突，已修正。）

stub（通用版，实现在 icall_asm.py；这里只列控制流）
    mov r11, M / cmp [r11],0xA5 / jne orig
    push rbx, rcx, rdx, r8, r9
    sub  rsp, 0x100                     ; 影子 0x20 + 栈参数 0x80 + xmm 备份 0x20
    movsd [rsp+0xA0], xmm0..xmm3        ; ★ 备份原函数自己的浮点参数
    mov  rbx, r11 / mov r11,[rbx+8] / test / jz bail
    mov  rax,[rbx+0x70+8i] / mov [rsp+0x20+8i],rax   ; 16 个栈参数
    mov  rcx/rdx/r8/r9, [rbx+0x10..0x28]
    movsd xmm0..xmm3, [rbx+0x40..0x58]
    call r11
    mov  [rbx+0x30], rax / movsd [rbx+0x60], xmm0
    mov  byte [rbx+1], 1                ; done=1（调用之后才置位）
    jmp restore
 bail: mov byte [rbx+1], 2
 restore: movsd xmm0..xmm3, [rsp+0xA0] ; ★ 换回原值，否则原函数浮点参数被污染
    add rsp,0x100 / pop r9,r8,rdx,rcx / pop rbx
    <原始 14 字节> / jmp [rip] <host+14>

为什么 xmm 必须备份：被 hook 的函数入口处 xmm0-3 是它自己的浮点参数，
我们为了发起调用会覆盖它们。不还原的话原函数拿到错值 —— 不崩，只静默算错。

三道闸的意义：
  1) pending 必须等于 0xA5 —— 内存被写脏时最坏结果是「这次命令没执行」
  2) target 为 0 不调用 —— 即使 pending 被伪造成魔数，也不会 call 0
  3) bail 时 done=2 —— 「拒绝执行」是可观测事件，不再是静默失败

关键点：done 在 call **之后**才置位。所以「done 没到」＝调用崩了/卡住了，
不会被误读成「返回 0」。（上一版就是栽在这里。）

用法
----
    python icall.py install              打钩子（每个游戏进程只需一次）
    python icall.py status
    python icall.py stub                 离线打印 stub 反汇编 + 静态校验
    python icall.py bootstrap            学出 System.String 的 klass（用一次即可）
    python icall.py str "测试文本"        验证字符串拼装/回读
    python icall.py call 0x19C64E0 --arg0 scratch
    python icall.py callx 0x8FDCF0 --this 0x12345 --ret int
    python icall.py callx 0x3444AA0 --i 1 --f 0.5 --f32 1.5 --s "text" --ret f32
    python icall.py read 0x2000 64       读 scratch 前 64 字节
    python icall.py uninstall            还原入口
"""
import sys, os, json, struct, time
import ctypes
import ctypes.wintypes as wt
import pefile

def _resolve_dll():
    """
    UserAssembly.dll 在哪。**不能写死**：项目会被拷到别的盘 / 别的机器
    （`D:\\BH3\\7.0`、`Z:\\AI\\7.0`、`C:\\nas\\AI\\7.0` …），写死一个路径换台机器就炸。

    事故 10-31：从 `C:\\nas\\AI\\7.0` 跑 rehook.bat，日志里
    `rva2off → pe() → pefile.PE` 直接 FileNotFoundError，
    因为路径还指着 `D:\\BH3\\7.0\\BH3_Data\\Native\\UserAssembly.dll`。

    优先级：--dll 参数 > BH3_DLL 环境变量 > 从本文件向上找项目根 > 已知候选。
    """
    rel = os.path.join("BH3_Data", "Native", "UserAssembly.dll")

    argv = sys.argv
    for i, a in enumerate(argv):
        if a == "--dll" and i + 1 < len(argv):
            return os.path.abspath(argv[i + 1])
        if a.startswith("--dll="):
            return os.path.abspath(a.split("=", 1)[1])

    env = os.environ.get("BH3_DLL", "").strip()
    if env:
        return os.path.abspath(env)

    # rea\_rea\icall.py → rea\_rea → rea → 项目根，逐级往上试。
    # 2026-11-05：UserAssembly.dll 已从 <root>\BH3_Data\Native\ 搬到
    # <root>\app\BH3_Data\Native\。两种布局都要认，否则 orig_bytes() → pe()
    # 直接 FileNotFoundError，连 install 都跑不起来。
    # 也可用 --dll / BH3_DLL 显式指定，优先级更高（见上面）。
    here = os.path.dirname(os.path.abspath(__file__))
    d = here
    for _ in range(6):
        for sub in ("", "app"):
            cand = os.path.join(d, sub, rel) if sub else os.path.join(d, rel)
            if os.path.isfile(cand):
                return cand
        parent = os.path.dirname(d)
        if parent == d:
            break
        d = parent

    for base in (os.getcwd(), r"D:\BH3\7.0", r"Z:\AI\7.0", r"C:\nas\AI\7.0"):
        for sub in ("", "app"):
            cand = os.path.join(base, sub, rel) if sub else os.path.join(base, rel)
            if os.path.isfile(cand):
                return cand

    # 都没有：返回「按项目根推出来的那个」，报错时一眼能看出该放哪
    return os.path.join(os.path.dirname(os.path.dirname(here)), rel)


DLL_PATH = _resolve_dll()
PROC_NAME = "BH3.exe"
HOST_RVA = 0x21D2550          # MoleMole.MonoClientPacketConsumer.Update
PATCH_LEN = 14

STATE = os.path.join(os.path.dirname(os.path.abspath(__file__)), ".icall.json")

MAILBOX_OFF = 0x0000
CAPTURE_OFF = 0x38            # 捕获到的 this 存这里
STUB_OFF = 0x1000
SCRATCH_OFF = 0x2000
ALLOC_SIZE = 0x8000
MAGIC = 0xA5                  # pending 必须是这个值才执行命令（防脏字节误触发）
ALLOW_ANY = False             # --force：允许调用非函数入口的地址（默认拒绝）

# ---- 通用信箱扩展字段（2026-11-07，全部落在 0x38 之后，旧字段不变）----
NSTACK_OFF = 0x02             # u8   栈参数个数
XMMMASK_OFF = 0x03            # u8   槽 0..3 哪些是浮点（仅调试记录）
XMM_OFF = 0x40                # u64×4  xmm0..3 的位模式
RET_XMM_OFF = 0x60            # u64   浮点返回值（xmm0 低位）
RET_KIND_OFF = 0x68           # u64   0=整数 1=浮点
STACK_OFF = 0x70              # u64×16 第 5..20 个参数
MAX_STACK_ARGS = 16
MB_SIZE = 0x100               # 一次投递要写的信箱字节数

# Application::get_dataPath —— 静态无参返回 string，用来 bootstrap String klass
RVA_GET_DATAPATH = 0x6AB8500

# 每个被 hook 的函数单独占一个 0x1000 页，从 0x4000 开始
HOOK_BASE_OFF = 0x4000
HOOK_SLOT_SIZE = 0x1000

k32 = ctypes.WinDLL("kernel32", use_last_error=True)
k32.OpenProcess.restype = wt.HANDLE
k32.OpenProcess.argtypes = [wt.DWORD, wt.BOOL, wt.DWORD]
k32.CloseHandle.argtypes = [wt.HANDLE]
k32.VirtualAllocEx.restype = wt.LPVOID
k32.VirtualAllocEx.argtypes = [wt.HANDLE, wt.LPVOID, ctypes.c_size_t, wt.DWORD, wt.DWORD]
k32.VirtualProtectEx.restype = wt.BOOL
k32.VirtualProtectEx.argtypes = [wt.HANDLE, wt.LPVOID, ctypes.c_size_t, wt.DWORD,
                                 ctypes.POINTER(wt.DWORD)]
k32.ReadProcessMemory.restype = wt.BOOL
k32.ReadProcessMemory.argtypes = [wt.HANDLE, wt.LPCVOID, wt.LPVOID, ctypes.c_size_t,
                                  ctypes.POINTER(ctypes.c_size_t)]
k32.WriteProcessMemory.restype = wt.BOOL
k32.WriteProcessMemory.argtypes = [wt.HANDLE, wt.LPVOID, wt.LPCVOID, ctypes.c_size_t,
                                   ctypes.POINTER(ctypes.c_size_t)]
k32.FlushInstructionCache.restype = wt.BOOL
k32.FlushInstructionCache.argtypes = [wt.HANDLE, wt.LPCVOID, ctypes.c_size_t]


class MODULEENTRY32W(ctypes.Structure):
    _fields_ = [("dwSize", wt.DWORD), ("th32ModuleID", wt.DWORD),
                ("th32ProcessID", wt.DWORD), ("GlblcntUsage", wt.DWORD),
                ("ProccntUsage", wt.DWORD), ("modBaseAddr", ctypes.c_void_p),
                ("modBaseSize", wt.DWORD), ("hModule", wt.HMODULE),
                ("szModule", wt.WCHAR * 256), ("szExePath", wt.WCHAR * 260)]


_PID_CACHE = None
_BASE_CACHE = {}


def find_pid(name=PROC_NAME, fresh=False):
    """找进程 PID。**带缓存**。

    每次 call_typed 都会走到这里，而它是靠起一个 tasklist.exe 子进程实现的。
    遍历场景树那种成百上千次调用的场景，等于起几百个 tasklist —— 最后会把自己
    拖死（实测 subprocess 报 0xC0000142 DLL 初始化失败）。所以缓存，并且用
    OpenProcess 探活来判断是否过期。
    """
    global _PID_CACHE
    if _PID_CACHE and not fresh:
        # 探活：进程没了就重查，避免拿着旧 PID 反复 OpenProcess 失败
        h = k32.OpenProcess(0x1000, False, _PID_CACHE)
        if h:
            k32.CloseHandle(h)
            return _PID_CACHE
        _PID_CACHE = None
    import subprocess, re
    out = subprocess.check_output(["tasklist", "/FI", "IMAGENAME eq %s" % name],
                                  text=True, errors="replace")
    m = re.search(re.escape(name) + r"\s+(\d+)", out)
    _PID_CACHE = int(m.group(1)) if m else None
    return _PID_CACHE


def find_module_base(pid, want="UserAssembly.dll"):
    k32.CreateToolhelp32Snapshot.restype = wt.HANDLE
    k32.CreateToolhelp32Snapshot.argtypes = [wt.DWORD, wt.DWORD]
    snap = k32.CreateToolhelp32Snapshot(0x18, pid)
    e = MODULEENTRY32W(); e.dwSize = ctypes.sizeof(e)
    ok = k32.Module32FirstW(snap, ctypes.byref(e))
    base = None
    while ok:
        if e.szModule.lower() == want.lower():
            base = e.modBaseAddr
            break
        ok = k32.Module32NextW(snap, ctypes.byref(e))
    k32.CloseHandle(snap)
    return base


_pe = None


def pe():
    global _pe
    if _pe is None:
        if not os.path.isfile(DLL_PATH):
            raise SystemExit(
                "找不到 UserAssembly.dll：%s\n"
                "  用 --dll <路径> 指定，或设环境变量 BH3_DLL。\n"
                "  （DLL_PATH 现在是从 icall.py 位置往上找项目根推出来的，不再写死盘符）"
                % DLL_PATH)
        _pe = pefile.PE(DLL_PATH, fast_load=True)
        _pe.parse_data_directories(
            directories=[pefile.DIRECTORY_ENTRY['IMAGE_DIRECTORY_ENTRY_EXPORT']])
    return _pe


def module_span():
    """UserAssembly.dll 的内存映像大小（SizeOfImage），用于校验调用地址。

    2026-10-25 21:1x 事故后加：groupprobe.py 把反汇编里的 VA
    (0x183758440，已含 0x180000000) 当 RVA 又加了一次 base，
    实际 target 变成 0x7fffe3d758440 —— 未映射内存，客户端访问违例崩溃。
    cmd_call 之前只拦 target==0，拦不住这种越界。
    """
    try:
        return int(pe().OPTIONAL_HEADER.SizeOfImage)
    except Exception:
        return None


def rva2off(rva):
    for s in pe().sections:
        if s.VirtualAddress <= rva < s.VirtualAddress + max(s.Misc_VirtualSize, s.SizeOfRawData):
            return s.PointerToRawData + (rva - s.VirtualAddress)
    return None


def orig_bytes():
    off = rva2off(HOST_RVA)
    with open(DLL_PATH, "rb") as f:
        f.seek(off)
        return f.read(PATCH_LEN)


def file_bytes(rva, n):
    off = rva2off(rva)
    with open(DLL_PATH, "rb") as f:
        f.seek(off)
        return f.read(n)


def insn_len_at(rva, minimum=14):
    """从 rva 起按指令边界累加，返回 >= minimum 的最小边界长度。"""
    import capstone
    md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_64)
    data = file_bytes(rva, 64)
    total = 0
    for ins in md.disasm(data, 0x180000000 + rva):
        total += ins.size
        if total >= minimum:
            return total
    raise RuntimeError("无法在 64 字节内找到指令边界")


_REL_BRANCH = {
    "call", "jmp", "je", "jne", "jz", "jnz", "ja", "jae", "jb", "jbe", "jc", "jnc",
    "jg", "jge", "jl", "jle", "jo", "jno", "js", "jns", "jp", "jnp", "jpe", "jpo",
    "loop", "loope", "loopne", "jrcxz", "jecxz",
}


def unsafe_replay(orig, orig_va, new_va):
    """检查 orig 这段字节能否**原样**搬到 new_va 处执行。

    为什么需要这个
    --------------
    2026-10-25 16:01 实测事故：挂 `raise_null_reference_exception`(RVA 0x4FE400)，
    覆盖的 14 字节里有 `e8 67 a5 f4 ff`（call rel32 → 0x180449770）。
    这段字节原样搬到 stub(0xE51000) 后，rel32 位移不变，目标变成 0xD9B573，
    是个未映射地址，一跳就 Access Violation，游戏直接崩。

    规则：相对跳转 / RIP 相对寻址，只有在**目标落在本块内部**时才安全
    （整块一起平移，位移不变）。目标在块外就必须重定位，而 stub 与模块相距
    通常远超 ±2GB，rel32 装不下 —— 所以这里直接判定为不可挂钩。

    返回：问题描述列表，空列表表示安全。
    """
    import capstone
    md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_64)
    md.detail = True
    end = orig_va + len(orig)
    bad = []
    for ins in md.disasm(orig, orig_va):
        rel = ins.address - orig_va
        if ins.mnemonic in _REL_BRANCH:
            for op in ins.operands:
                if op.type == capstone.x86.X86_OP_IMM:
                    t = op.imm
                    if not (orig_va <= t < end):
                        bad.append("%s @+%#x → %#x（块外目标，需重定位）"
                                   % (ins.mnemonic, rel, t))
        for op in ins.operands:
            if (op.type == capstone.x86.X86_OP_MEM
                    and op.mem.base == capstone.x86.X86_REG_RIP):
                t = ins.address + ins.size + op.mem.disp
                if not (orig_va <= t < end):
                    bad.append("rip 相对寻址 @+%#x → %#x（块外目标，需重定位）"
                               % (rel, t))
    return bad


def build_capture_stub(slot_va, resume_va, orig):
    """只把 rcx 存下来，然后原样执行被覆盖的指令。r11 是调用者保存寄存器，可随意用。"""
    b = b""
    b += b"\x49\xBB" + struct.pack("<Q", slot_va)      # mov r11, slot
    b += b"\x49\x89\x0B"                                # mov [r11], rcx
    b += orig
    b += b"\xFF\x25\x00\x00\x00\x00" + struct.pack("<Q", resume_va)
    return b


def read_mem(h, va, n):
    buf = ctypes.create_string_buffer(n); rd = ctypes.c_size_t(0)
    if not k32.ReadProcessMemory(h, ctypes.c_void_p(va), buf, n, ctypes.byref(rd)):
        return None
    return buf.raw[:rd.value]


def write_mem(h, va, data):
    old = wt.DWORD(0)
    if not k32.VirtualProtectEx(h, ctypes.c_void_p(va), len(data), 0x40, ctypes.byref(old)):
        return False
    w = ctypes.c_size_t(0)
    ok = k32.WriteProcessMemory(h, ctypes.c_void_p(va), data, len(data), ctypes.byref(w))
    k32.FlushInstructionCache(h, ctypes.c_void_p(va), len(data))
    tmp = wt.DWORD(0)
    k32.VirtualProtectEx(h, ctypes.c_void_p(va), len(data), old.value, ctypes.byref(tmp))
    return bool(ok) and w.value == len(data)


def build_stub(mb_va, resume_va, orig):
    """信箱 stub —— 通用版，实现在 icall_asm。

    2026-11-07 重写。旧版只搬 rcx/rdx/r8/r9 四个整数参数：
      · 浮点参数（xmm0-3）完全调不了 —— `SetOWEndlessLevelScoreMode` 这类
        带 float 的方法直接排除在外；
      · 第 5 个及以后的参数（栈传）也调不了。
    新版补齐三件事：
      1) 无条件写 rcx/rdx/r8/r9 + xmm0-3（调用方按槽位类型填，另一侧填 0），
         所以不需要在机器码里分支判类型；
      2) 搬 16 个栈参数到 [rsp+0x20+8i]（= call 后被调函数的 [rsp+0x28+8i]）；
      3) 备份 / 恢复被 hook 函数入口处的 xmm0-3。这一条最容易漏：入口处的
         xmm0-3 是**它自己的浮点参数**，被我们为发起调用而覆盖后不换回来，
         orig 那几条指令看到的就是错的浮点值 —— 不崩、只静默算错。

    原版加固措施全部保留（魔数闸门 / target 非零 / done=2 可观测）。
    下面是旧版注释，保留以记录事故背景。
    """
    import icall_asm
    return icall_asm.build(mb_va, resume_va, orig)


def _build_stub_v1_legacy(mb_va, resume_va, orig):
    """旧版 stub（仅 4 整数参数），留作对照，不再使用。

    2026-10-25 加固：原版用「pending != 0」判有命令，实测 pending 被写成非 0、
    target 为 0，于是 call r11 跳到地址 0 直接崩游戏
    （crash.dmp: RIP=0, RBX=信箱, 栈顶=stub 内 call 的下一条）。
    现在改成三道闸：
      1) pending 必须等于魔数 0xA5，其它任何值一律当「没命令」
      2) target 为 0 时不调用，直接走恢复路径
      3) 走到 (2) 时把 done 置 2，让「拒绝执行」在外部可观测
    这样内存被写脏最多是「本次命令没执行」，不会带走进程，也不会静默失败。
    """
    b = b""
    b += b"\x49\xBB" + struct.pack("<Q", mb_va)      # mov r11, M
    b += b"\x41\x80\x3B" + bytes([MAGIC])            # cmp byte [r11], 0xA5
    jne_pos = len(b)
    b += b"\x75\x00"                                  # jne orig（不碰 done）
    b += b"\x41\xC6\x03\x00"                          # mov byte [r11], 0
    b += b"\x53"                                      # push rbx
    b += b"\x51\x52\x41\x50\x41\x51"                  # push rcx,rdx,r8,r9
    b += b"\x48\x83\xEC\x20"                          # sub rsp,0x20
    b += b"\x4C\x89\xDB"                              # mov rbx,r11
    b += b"\x4C\x8B\x5B\x08"                          # mov r11,[rbx+8]  (target)
    b += b"\x4D\x85\xDB"                              # test r11,r11
    jz_pos = len(b)
    b += b"\x74\x00"                                  # jz bail
    b += b"\x48\x8B\x4B\x10"                          # mov rcx,[rbx+0x10]
    b += b"\x48\x8B\x53\x18"                          # mov rdx,[rbx+0x18]
    b += b"\x4C\x8B\x43\x20"                          # mov r8,[rbx+0x20]
    b += b"\x4C\x8B\x4B\x28"                          # mov r9,[rbx+0x28]
    b += b"\x41\xFF\xD3"                              # call r11
    b += b"\x48\x89\x43\x30"                          # mov [rbx+0x30],rax
    b += b"\xC6\x43\x01\x01"                          # mov byte [rbx+1],1   done=1
    b += b"\xEB\x04"                                  # jmp restore（跳过 bail 那 4 字节）
    bail_pos = len(b)                                 # bail:
    b += b"\xC6\x43\x01\x02"                          # mov byte [rbx+1],2   done=2
    b += b"\x48\x83\xC4\x20"                          # restore: add rsp,0x20
    b += b"\x41\x59\x41\x58\x5A\x59"                  # pop r9,r8,rdx,rcx
    b += b"\x5B"                                      # pop rbx
    orig_pos = len(b)
    b += orig
    b += b"\xFF\x25\x00\x00\x00\x00" + struct.pack("<Q", resume_va)
    rel = orig_pos - (jne_pos + 2)
    assert 0 <= rel <= 127, (rel, orig_pos)
    b = b[:jne_pos + 1] + bytes([rel]) + b[jne_pos + 2:]
    rel2 = bail_pos - (jz_pos + 2)
    assert 0 <= rel2 <= 127, (rel2, bail_pos)
    b = b[:jz_pos + 1] + bytes([rel2]) + b[jz_pos + 2:]
    return b


def load_state():
    if not os.path.exists(STATE):
        return None
    try:
        return json.load(open(STATE))
    except Exception:
        return None


def save_state(d):
    """写状态。合并而不是覆盖 —— string_klass 这类「学来的常量」要跨 install 保留，
    否则每次重装都要重新 bootstrap 一遍。"""
    old = load_state() or {}
    old.update(d)
    json.dump(old, open(STATE, "w"), indent=2)


def open_game():
    pid = find_pid()
    if not pid:
        print("游戏未运行"); return None, None, None
    h = k32.OpenProcess(0x0410 | 0x0020 | 0x0008, False, pid)
    if not h:
        print("OpenProcess 失败"); return None, None, None
    base = _BASE_CACHE.get(pid)
    if base is None:
        base = find_module_base(pid)
        if base:
            _BASE_CACHE[pid] = base
    return h, pid, base


def cmd_install():
    h, pid, base = open_game()
    if not h:
        return 1
    host_va = base + HOST_RVA
    orig = orig_bytes()
    cur = read_mem(h, host_va, PATCH_LEN)
    if cur != orig:
        print("宿主入口非原始字节 [%s]，先 uninstall 或重启游戏" % (cur.hex(" ") if cur else "读取失败"))
        k32.CloseHandle(h); return 1

    page = k32.VirtualAllocEx(h, None, ALLOC_SIZE, 0x3000, 0x40)
    if not page:
        print("VirtualAllocEx 失败"); k32.CloseHandle(h); return 1
    mb_va = page + MAILBOX_OFF
    stub_va = page + STUB_OFF
    scratch_va = page + SCRATCH_OFF

    write_mem(h, mb_va, b"\x00" * MB_SIZE)
    # 自检：信箱必须读回全 0。读不回来说明这块地址根本没归我们（或被别的东西占着），
    # 继续挂钩就是在赌命 —— 2026-10-25 那次崩溃就是 pending 被写成非 0 导致的。
    # 2026-11-07：范围从 0x40 扩到 MB_SIZE(0x100)，因为栈参数/xmm 字段在 0x40 之后。
    chk = read_mem(h, mb_va, MB_SIZE)
    if chk != b"\x00" * MB_SIZE:
        print("✗ 信箱自检失败：写入全 0 后读回 %s" % (chk.hex(" ") if chk else "读取失败"))
        print("  这块地址可能被进程占用，放弃安装（入口未改动）")
        k32.CloseHandle(h); return 1
    stub = build_stub(mb_va, host_va + PATCH_LEN, orig)
    # ★ 搬移安全性：orig 那几条指令会被原样复制到 stub 里执行。如果其中有
    #   rip 相对寻址或相对跳转，位移不变而基址变了，目标就错 —— 2026-10-25
    #   崩过一次（raise_null_reference_exception 的入口含 call rel32）。
    #   当前宿主是纯 push/sub rsp 所以安全，但换成别的宿主必须先过这一关。
    bad = unsafe_replay(orig, host_va, stub_va)
    if bad:
        print("✗ 宿主入口不能搬移（%d 处）：" % len(bad))
        for b in bad:
            print("    " + b)
        print("  换一个入口，或先做重定位再挂。入口未改动。")
        k32.CloseHandle(h); return 1
    if not write_mem(h, stub_va, stub):
        print("stub 写入失败"); k32.CloseHandle(h); return 1
    # 自检 2：stub 必须原样落地。写进去和读回来不一致，说明这块地址不可写/被共享，
    # 那就绝不能把入口指过去 —— 否则游戏一跳就是执行随机字节。
    back = read_mem(h, stub_va, len(stub))
    if back != stub:
        print("✗ stub 落地自检失败：写入 %d 字节，读回 %s" % (
            len(stub), ("%d 字节" % len(back)) if back else "读取失败"))
        print("  入口未改动，安全退出")
        k32.CloseHandle(h); return 1
    entry = b"\xFF\x25\x00\x00\x00\x00" + struct.pack("<Q", stub_va)
    if not write_mem(h, host_va, entry):
        print("入口跳转写入失败"); k32.CloseHandle(h); return 1
    # 自检 3：入口必须指到 stub_va
    cur2 = read_mem(h, host_va, PATCH_LEN)
    if cur2 != entry:
        print("✗ 入口自检失败：读回 %s，预期 %s" % (
            cur2.hex(" ") if cur2 else "?", entry.hex(" ")))
        print("  正在尝试回滚入口…")
        write_mem(h, host_va, orig)
        k32.CloseHandle(h); return 1

    save_state({"pid": pid, "base": base, "page": page, "mailbox": mb_va,
                "stub": stub_va, "scratch": scratch_va})
    print("✓ 已安装（三道自检通过：信箱全 0 / stub 落地一致 / 入口指向正确）")
    print("  PID=%d  模块基址=%#x" % (pid, base))
    print("  信箱=%#x  stub=%#x (%d 字节)  scratch=%#x" % (mb_va, stub_va, len(stub), scratch_va))
    print("  scratch 可用 %d 字节（%#x~%#x，别越过 %#x）" % (
        HOOK_BASE_OFF - SCRATCH_OFF, scratch_va, page + HOOK_BASE_OFF - 1, page + HOOK_BASE_OFF))
    k32.CloseHandle(h)
    return 0


def cmd_uninstall():
    h, pid, base = open_game()
    if not h:
        return 1
    ok = write_mem(h, base + HOST_RVA, orig_bytes())
    print("✓ 已还原入口" if ok else "✗ 还原失败")
    k32.CloseHandle(h)
    return 0 if ok else 1


def cmd_status():
    h, pid, base = open_game()
    if not h:
        return 1
    host_va = base + HOST_RVA
    cur = read_mem(h, host_va, PATCH_LEN)
    st = load_state()
    hooked = cur and cur[:2] == b"\xFF\x25"
    print("PID=%d  基址=%#x  Update @ %#x" % (pid, base, host_va))
    print("入口 [%s]  %s" % (cur.hex(" ") if cur else "?",
                            "已挂钩子" if hooked else ("原始" if cur == orig_bytes() else "未知")))
    if st:
        alive = st.get("pid") == pid
        print("信箱 %#x  %s" % (st.get("mailbox", 0), "有效" if alive else "失效（进程已重启）"))
    else:
        print("未安装")
    k32.CloseHandle(h)
    return 0


def cmd_call(target, args, wait=5.0):
    """原始接口：最多 4 个整数参数。

    2026-11-07 起内部改走通用路径（make_packet + _submit），旧调用方式也能
    用上新 stub 的加固。需要浮点 / 栈参数 / 托管字符串请用 callx。
    """
    st = load_state()
    if st:
        args = [st["scratch"] if a == "scratch" else a for a in args]
    regs, xmms, stack, nstack, mask = _pack_args(args)
    pkt = make_packet(target, regs, xmms, stack, nstack, mask)
    ok, ret, _st, h, mb = _submit(target, pkt, wait)
    if not ok:
        return 1
    print("✓ 已执行  ret(rax) = %#x  (%d)" % (ret, ret))
    k32.CloseHandle(h)
    return 0


# ============================================================================
# 通用调用（2026-11-07）：浮点 / 栈参数 / 双返回值 / 托管字符串
#
# 这一层解决的是「参数怎么摆」。机器码那侧见 icall_asm：它无条件写
# rcx/rdx/r8/r9 + xmm0-3，所以这里只要把「该是整数的槽填整数、该是浮点的
# 槽填浮点、另一侧留 0」摆好，stub 不需要任何分支。
# ============================================================================

class F32(float):
    """标记「这个数是 C# 的 float（32 位）」。

    不标就按 double 传，而 float 1.5f 的位模式是 0x3FC00000、double 1.5 是
    0x3FF8000000000000 —— 低 32 位恰好是 0，被调函数读到的是 0.0f。
    这种错不会崩，只会让结果莫名其妙，属于最难查的一类。
    """
    __slots__ = ()


class S(str):
    """标记「这个字符串要转成 Il2CppString*」，需要先 bootstrap 拿到 String klass。"""
    __slots__ = ()


def _pack_args(args):
    """按 Windows x64 约定装箱。

    前 4 个参数按**顺序**占槽 0..3（不是整数浮点各数各的）：整数进
    rcx/rdx/r8/r9，浮点进 xmm0..3。第 5 个起全部压栈，每个 8 字节，
    浮点也是 8 字节槽（float 只占低 4 字节）。
    """
    regs = [0, 0, 0, 0]
    xmms = [0, 0, 0, 0]
    stack = [0] * MAX_STACK_ARGS
    mask = 0
    for i, a in enumerate(args):
        if isinstance(a, F32):
            v = struct.unpack("<I", struct.pack("<f", float(a)))[0]
            if i < 4:
                xmms[i] = v
                mask |= 1 << i
            else:
                stack[i - 4] = v
        elif isinstance(a, float):
            v = struct.unpack("<Q", struct.pack("<d", float(a)))[0]
            if i < 4:
                xmms[i] = v
                mask |= 1 << i
            else:
                stack[i - 4] = v
        else:
            v = int(a) & 0xFFFFFFFFFFFFFFFF
            if i < 4:
                regs[i] = v
            else:
                stack[i - 4] = v
    return regs, xmms, stack, max(0, len(args) - 4), mask


def _string_bytes(text):
    """一个 Il2CppString 在 scratch 里占的对齐后字节数。"""
    n = 0x14 + len(text.encode("utf-16-le", "surrogatepass"))
    return (n + 7) // 8 * 8


def _mkstring(h, va, text, klass):
    """在 va 处拼一个 Il2CppString。

    布局（Unity/il2cpp 实际结构，不是文档里想当然的 +0x18）：
        +0x00 klass
        +0x08 monitor
        +0x10 length   int32，单位是 UTF-16 码元
        +0x14 chars[]  utf-16le
    """
    data = text.encode("utf-16-le", "surrogatepass")
    buf = bytearray(0x14)
    struct.pack_into("<Q", buf, 0x00, klass)
    struct.pack_into("<Q", buf, 0x08, 0)
    struct.pack_into("<i", buf, 0x10, len(data) // 2)
    buf += data
    buf += b"\x00" * ((8 - len(buf) % 8) % 8)
    if not write_mem(h, va, bytes(buf)):
        return None
    return va


def _symtab_path():
    import os
    return os.path.join(os.path.dirname(os.path.abspath(__file__)), "bh3_symtab.tsv")


_SYM_RVAS = None


def sym_rvas():
    """符号表里所有方法入口的 RVA，升序去重。

    用途只有一个：拦截「跳进函数半截」的调用。这种调用不会报错，只会把
    游戏打崩（实测：把 0x6918300 当函数入口，那其实是 get_right 的中间，
    整个进程直接没了）。所以每次投递前查一次入口表，代价 0.1s，值得。
    """
    global _SYM_RVAS
    if _SYM_RVAS is not None:
        return _SYM_RVAS
    from array import array
    a = array("I")
    try:
        with open(_symtab_path(), "r", encoding="utf-8", errors="replace") as f:
            for line in f:
                h = line.split("\t", 1)[0]
                if not h:
                    continue
                try:
                    v = int(h, 16)
                except ValueError:
                    continue
                if 0 < v < 0x20000000:
                    a.append(v)
    except OSError:
        pass
    _SYM_RVAS = array("I", sorted(set(a)))
    return _SYM_RVAS


def is_fn_entry(rva):
    """rva 是不是某个方法的入口。没有符号表时一律放行（不误伤）。"""
    a = sym_rvas()
    if not a:
        return True
    import bisect
    i = bisect.bisect_left(a, rva)
    return i < len(a) and a[i] == rva


def fn_owner(rva):
    """rva 落在哪个函数体内 → (入口rva, '类::方法')。只用于报错，慢就慢点。"""
    a = sym_rvas()
    if not a:
        return None, None
    import bisect
    i = bisect.bisect_right(a, rva) - 1
    if i < 0:
        return None, None
    start = int(a[i])
    if start == rva:
        return None, None
    nm = ""
    try:
        with open(_symtab_path(), "r", encoding="utf-8", errors="replace") as f:
            for line in f:
                if line.startswith("%X\t" % start):
                    c = line.rstrip("\n").split("\t")
                    if len(c) >= 4:
                        nm = "%s::%s" % (c[2], c[3])
                    break
    except OSError:
        pass
    return start, nm


def _submit(target, pkt, wait=6.0, quiet=False):
    """把 pkt 写进信箱、置魔数、等 done。返回 (ok, ret_rax, st, h, mb)。

    pkt 必须是 MB_SIZE 字节（0x100），因为 stub 会读 0x40 之后的 xmm/栈字段。
    """
    st = load_state()
    if not st:
        print("未安装，先 install"); return False, None, None, None, None
    if not target:
        print("✗ target 为 0，拒绝投递"); return False, None, None, None, None
    h, pid, base = open_game()
    if not h:
        return False, None, None, None, None
    if st["pid"] != pid:
        print("信箱已失效（游戏重启过），重新 install")
        k32.CloseHandle(h); return False, None, None, None, None

    span = module_span()
    if span and not (0 <= target - base < span):
        print("✗ target %#x 不在 UserAssembly.dll 里" % target)
        print("  base=%#x  换算 rva=%#x" % (base, target - base & 0xFFFFFFFFFFFFFFFF))
        k32.CloseHandle(h); return False, None, None, None, None

    # 入口校验：跳进函数半截不会报错，只会静默崩掉整个进程。
    rva_of = target - base
    if not ALLOW_ANY and not is_fn_entry(rva_of):
        owner, nm = fn_owner(rva_of)
        print("✗ 拒绝调用：rva %#x 不是函数入口" % rva_of)
        if owner is not None:
            print("  它落在 %s (rva %#x) 内部，偏移 +%#x"
                  % (nm or "?", owner, rva_of - owner))
            print("  从函数中间开始执行必崩，十有八九是 RVA 抄错/算错")
        else:
            print("  符号表里没有以它开头的方法")
        print("  确实要在任意地址执行就加 --force")
        k32.CloseHandle(h); return False, None, None, None, None

    mb = st["mailbox"]
    pkt = pkt + b"\x00" * (MB_SIZE - len(pkt))
    if not write_mem(h, mb, pkt):
        print("信箱写入失败"); k32.CloseHandle(h); return False, None, None, None, None
    # 回读核对：target 必须真的落在信箱里。写失败但 pending 写成功，
    # 就是「stub 看见魔数却读到 target=0」的经典成因。
    back = read_mem(h, mb, MB_SIZE)
    if back != pkt:
        print("✗ 信箱回读不一致，放弃投递（pending 未置位）")
        write_mem(h, mb, b"\x00" * MB_SIZE)
        k32.CloseHandle(h); return False, None, None, None, None
    if not write_mem(h, mb, bytes([MAGIC])):
        print("✗ 魔数写入失败"); k32.CloseHandle(h); return False, None, None, None, None
    chk = read_mem(h, mb, 1)
    if not chk or chk[0] != MAGIC:
        print("✗ 魔数回读失败")
        write_mem(h, mb, b"\x00")
        k32.CloseHandle(h); return False, None, None, None, None

    t0 = time.time()
    while time.time() - t0 < wait:
        d = read_mem(h, mb, 2)
        if d:
            if d[0] == 0 and d[1] == 1:
                raw = read_mem(h, mb + 0x30, 8)
                ret = struct.unpack("<Q", raw)[0] if raw and len(raw) == 8 else None
                return True, ret, st, h, mb
            if d[0] == 0 and d[1] == 2:
                print("✗ stub 拒绝执行：看见魔数但 target 读出来是 0")
                write_mem(h, mb, b"\x00\x00")
                k32.CloseHandle(h); return False, None, None, None, None
        time.sleep(0.02)
    print("✗ 超时：游戏线程没取走命令（可能卡住/已崩溃）")
    write_mem(h, mb, b"\x00")
    k32.CloseHandle(h)
    return False, None, None, None, None


def make_packet(target, regs, xmms, stack, nstack, mask):
    """按通用布局组包（0x100 字节）。"""
    buf = bytearray(MB_SIZE)
    buf[NSTACK_OFF] = nstack & 0xFF
    buf[XMMMASK_OFF] = mask & 0xFF
    struct.pack_into("<Q", buf, 0x08, target & 0xFFFFFFFFFFFFFFFF)
    for i, v in enumerate(regs):
        struct.pack_into("<Q", buf, 0x10 + i * 8, v & 0xFFFFFFFFFFFFFFFF)
    for i, v in enumerate(xmms):
        struct.pack_into("<Q", buf, XMM_OFF + i * 8, v & 0xFFFFFFFFFFFFFFFF)
    for i in range(MAX_STACK_ARGS):
        struct.pack_into("<Q", buf, STACK_OFF + i * 8,
                         (stack[i] if i < len(stack) else 0) & 0xFFFFFFFFFFFFFFFF)
    return bytes(buf)


def _materialize_args(st0, args, h0, reserve=0):
    """把 S("文本") 落成 scratch 里的真实 Il2CppString*，其余原样透传。

    reserve 是预留给 sret 缓冲的字节数 —— 字符串从低地址往上长，sret 缓冲
    固定在最顶端，两者不会撞。
    """
    conv = []
    cursor = 0
    limit = HOOK_BASE_OFF - SCRATCH_OFF - reserve
    for a in args:
        if isinstance(a, S):
            klass = st0.get("string_klass")
            if not klass:
                print("✗ 还没有 String klass —— 先跑: python icall.py bootstrap")
                return None
            va = st0["scratch"] + cursor
            if _mkstring(h0, va, str(a), klass) is None:
                print("✗ 字符串写入 scratch 失败")
                return None
            cursor += _string_bytes(str(a))
            if cursor >= limit:
                print("✗ scratch 溢出")
                return None
            conv.append(va)
        else:
            conv.append(a)
    return conv


def call_typed(target, args, ret="void", wait=6.0, verbose=True):
    """在游戏主线程调用 target，返回 Python 值。

    args 元素类型决定传参：
        int            整数/指针
        float          double（xmm 低 64 位）
        F32            32 位 float（xmm 低 32 位）
        S("文本")      Il2CppString*（自动拼进 scratch，需先 bootstrap）
    ret: "void" | "int" | "float"(double) | "f32" | "str"
    """
    st0 = load_state()
    if not st0:
        print("未安装，先 install"); return None
    h0, pid0, _ = open_game()
    if not h0:
        return None

    conv = _materialize_args(st0, args, h0)
    k32.CloseHandle(h0)
    if conv is None:
        return None

    regs, xmms, stack, nstack, mask = _pack_args(conv)
    pkt = make_packet(target, regs, xmms, stack, nstack, mask)
    ok, rax, st, h, mb = _submit(target, pkt, wait)
    if not ok:
        return None

    if verbose:
        if ret in ("float", "double", "f32"):
            print("✓ 已执行  ret(xmm0)  nstack=%d" % nstack)
        else:
            print("✓ ret(rax)=%#x (%d)  nstack=%d"
                  % (rax, rax if rax is not None else 0, nstack))
    try:
        if ret == "int":
            return rax
        if ret in ("float", "double"):
            d = read_mem(h, mb + RET_XMM_OFF, 8)
            return struct.unpack("<d", d)[0] if d else None
        if ret == "f32":
            d = read_mem(h, mb + RET_XMM_OFF, 4)
            return struct.unpack("<f", d)[0] if d else None
        if ret == "str" and rax:
            return _read_string(h, rax, verbose)
        return rax
    finally:
        k32.CloseHandle(h)


SRET_OFF = 0x1F00             # scratch 顶端 0x100 字节，专给「按值返回大结构体」的缓冲区
SKIP_SENTINEL = 0xCC          # sret 缓冲预填值：调用后仍是 0xCC 的字节 = 没被写过

# 按值返回时走 sret 的类型：名字 -> (float 个数, 字节数)
# Vector2 是 8 字节，正好塞进一个寄存器，不走 sret，所以不在表里。
STRUCT_RET = {
    "vec3":  (3, 12),         # Vector3
    "vec4":  (4, 16),         # Vector4
    "quat":  (4, 16),         # Quaternion (x,y,z,w)
    "color": (4, 16),         # Color (r,g,b,a)
    "rect":  (4, 16),         # Rect (x,y,w,h)
    "bounds": (6, 24),        # Bounds center(3) + extents(3)
}


def call_struct(target, args, nbytes, wait=6.0, verbose=True, what=""):
    """调用「按值返回 ≥9 字节结构体」的方法，返回缓冲区原始字节。

    为什么需要专门一条路径
    ----------------------
    MS x64 ABI 下，返回值 >8 字节不进寄存器，而是调用方提供一个缓冲区，
    指针通过**第 0 个整数槽**传进去，原来的第 0 个参数顺延一格。
    il2cpp 生成的 C++ 方法把 this 当普通首参，所以顺序变成：

        rcx = 缓冲区   rdx = this   r8 = 第 1 个参数 ...

    实测（Transform::get_position）：rax 回来的正是缓冲区地址，18 个字节的
    Vector3 只写了 12 字节、第 4 个 dword 保持预填值 —— 这条确实成立。

    这也解释了为什么裸 call_typed 调 Vector3 getter 会拿到垃圾：它把 this
    放在了 rcx，函数却把 rcx 当返回缓冲区，往里写 12 字节 —— 写穿的是
    this 指向的对象头。所以这个场景不能靠调用方「小心一点」，必须走这条路径。
    """
    st0 = load_state()
    if not st0:
        print("未安装，先 install"); return None
    h0, pid0, _ = open_game()
    if not h0:
        return None

    buf_va = st0["scratch"] + SRET_OFF
    if not write_mem(h0, buf_va, bytes([SKIP_SENTINEL]) * nbytes):
        print("✗ sret 缓冲清零失败")
        k32.CloseHandle(h0); return None

    conv = _materialize_args(st0, args, h0, reserve=SRET_OFF + 0x100)
    k32.CloseHandle(h0)
    if conv is None:
        return None

    regs, xmms, stack, nstack, mask = _pack_args([buf_va] + conv)
    pkt = make_packet(target, regs, xmms, stack, nstack, mask)
    ok, rax, st, h, mb = _submit(target, pkt, wait)
    if not ok:
        return None
    data = read_mem(h, buf_va, nbytes)
    k32.CloseHandle(h)
    if data is None:
        print("✗ sret 缓冲读回失败")
        return None
    if verbose:
        live = sum(1 for b in data if b != SKIP_SENTINEL)
        print("✓ sret  %#x 写回 %d/%d 字节   rax=%#x%s"
              % (buf_va, live, nbytes, rax,
                 "" if rax == buf_va else "   ← rax≠buf，调用约定可能不符"))
        if what:
            print("  %s" % what)
    return data


def unpack_floats(data, n=None):
    """把 sret 缓冲当 float 数组解释。n 给定时只取前 n 个。"""
    k = len(data) // 4 if n is None else n
    return struct.unpack("<%df" % k, data[:k * 4])


def _read_string(h, ptr, verbose=True):
    """读一个 Il2CppString* 的内容（用于验证返回的字符串）。"""
    head = read_mem(h, ptr, 0x14)
    if not head or len(head) < 0x14:
        return None
    klass, _mon, n = struct.unpack_from("<QQi", head, 0)
    if not (0 <= n < 0x10000):
        if verbose:
            print("  （%#x 不像 string：length=%d）" % (ptr, n))
        return None
    data = read_mem(h, ptr + 0x14, n * 2)
    if data is None:
        return None
    return data.decode("utf-16-le", "replace")


def _cstr_at(h, va, n=48):
    """读 va 处的内容，是「纯 ASCII + 有 NUL 结尾」就返回 str，否则 None。"""
    raw = read_mem(h, va, n)
    if not raw:
        return None
    end = raw.find(b"\x00")
    if end <= 0:
        return None
    s = raw[:end]
    if any(c < 0x20 or c > 0x7E for c in s):
        return None
    return s.decode("ascii")


def scan_klass_strings(h, klass, span=0x200):
    """扫描 klass 结构体，把「指向可读 ASCII 串」的字段找出来。

    返回 {偏移: 字符串值} —— 只保留能读出干净 ASCII 的项。
    用途：① bootstrap 验证 klass 真的是 System.String；② 反查 name/ns 偏移。
    """
    blob = read_mem(h, klass, span)
    if not blob:
        return {}
    out = {}
    for off in range(0, span - 7, 8):
        ptr = struct.unpack_from("<Q", blob, off)[0]
        if ptr < 0x10000 or ptr > 0x7FFFFFFFFFFF:
            continue
        s = _cstr_at(h, ptr)
        if s is not None:
            out[off] = s
    return out


def cmd_bootstrap():
    """调 Application::get_dataPath() 拿一个真实 string，学出 System.String 的 klass。

    为什么必须这么做
    ----------------
    要造 Il2CppString 就必须知道 System.String 的 Il2CppClass*，而它只存在于
    运行时的托管堆里：静态文件里拿不到（元数据加密、API 表 dump 不可用）。
    唯一稳的办法是让游戏自己产一个 string，再把它的 klass 抄下来。

    get_dataPath 是最便宜的入口：静态、无参、返回 string、随时可调（不依赖
    任何游戏状态）。

    关于验证（本作踩过的坑）
    -----------------------
    标准 Unity 的 Il2CppClass 是 +0x10 = name，本作不成立：klass+0x00 指向
    自身、+0x10 读出来是乱码。实测（_klass_probe.py 扫 0x200 字节）：
        +0x20 -> "System"   命名空间
        +0xa8 -> "String"   类名
    所以验证改成「扫描结构体里指向 ASCII 串的指针，同时命中 String 和 System」。
    双命中才算数——单个 "String" 有巧合可能，"String"+"System" 一起出现基本
    排除了误判。顺便把 name/ns 偏移存进 state，后面解任意类名都能复用。
    """
    st = load_state()
    if not st:
        print("未安装，先 install"); return 1
    base = st["base"]
    target = base + RVA_GET_DATAPATH
    print("调用 Application::get_dataPath()  target=%#x" % target)
    ptr = call_typed(target, [], ret="int", wait=8.0)
    if not ptr:
        print("✗ 没拿到返回值（rax=0）。可能 RVA 不是 method pointer，")
        print("  试试 --rva 换一个无参静态 string 方法")
        return 1
    print("  返回 string 指针 = %#x" % ptr)
    h, pid, _ = open_game()
    if not h:
        return 1
    print("  内容 = %r" % _read_string(h, ptr))
    kh = read_mem(h, ptr, 8)
    if not kh:
        k32.CloseHandle(h); return 1
    klass = struct.unpack("<Q", kh)[0]
    print("  String klass = %#x" % klass)

    hits = scan_klass_strings(h, klass)
    k32.CloseHandle(h)
    for off, val in sorted(hits.items()):
        print("  +%#04x -> %r" % (off, val))

    name_off = None
    ns_off = None
    for off, val in hits.items():
        if val == "String":
            name_off = off
        elif val == "System":
            ns_off = off
    if name_off is None or ns_off is None:
        print("✗ 结构体里没同时找到 'String' 和 'System'，klass 存疑，不保存")
        return 1
    save_state({"string_klass": klass,
                "klass_name_off": name_off,
                "klass_ns_off": ns_off})
    print("✓ 已保存 string_klass=%#x（name@+%#x, ns@+%#x）"
          % (klass, name_off, ns_off))
    print("  现在可以用 S(\"文本\") / --s \"文本\" 传托管字符串")
    return 0


def cmd_mkstring_demo(text):
    """在 scratch 拼一个字符串并回读，验证 _mkstring 的布局对不对。"""
    st = load_state()
    if not st:
        print("未安装，先 install"); return 1
    klass = st.get("string_klass")
    if not klass:
        print("✗ 先跑 bootstrap"); return 1
    h, pid, _ = open_game()
    if not h:
        return 1
    va = st["scratch"]
    if _mkstring(h, va, text, klass) is None:
        print("写入失败"); k32.CloseHandle(h); return 1
    back = _read_string(h, va)
    k32.CloseHandle(h)
    print("地址 = %#x   写入 %r   回读 %r   %s"
          % (va, text, back, "✓" if back == text else "✗ 不一致"))
    return 0 if back == text else 1


def cmd_read(off_or_va, n, absolute=False):
    st = load_state()
    if not st:
        print("未安装"); return 1
    h, pid, base = open_game()
    if not h:
        return 1
    va = off_or_va if absolute else st["page"] + off_or_va
    d = read_mem(h, va, n)
    k32.CloseHandle(h)
    if d is None:
        print("读取失败"); return 1
    for i in range(0, len(d), 16):
        chunk = d[i:i + 16]
        print("%#012x  %-47s  %s" % (va + i, chunk.hex(" "),
                                     "".join(chr(c) if 32 <= c < 127 else "." for c in chunk)))
    return 0


def cmd_hook(rva, slot_off=CAPTURE_OFF):
    st = load_state()
    if not st:
        print("未安装，先 install"); return 1
    h, pid, base = open_game()
    if not h:
        return 1
    if st["pid"] != pid:
        print("信箱已失效（游戏重启过），重新 install"); k32.CloseHandle(h); return 1
    page = st["page"]
    hooks = st.setdefault("hooks", {})
    key = str(rva)
    if key in hooks:
        print("该函数已挂钩（%#x）" % hooks[key]["slot"]); k32.CloseHandle(h); return 1

    L = insn_len_at(rva)
    orig = file_bytes(rva, L)
    fn_va = base + rva
    cur = read_mem(h, fn_va, L)
    if cur != orig:
        print("入口字节与文件不一致（%s vs %s），跳过" % (
            cur.hex(" ") if cur else "读取失败", orig.hex(" ")))
        k32.CloseHandle(h); return 1

    n = len(hooks)
    if HOOK_BASE_OFF + (n + 1) * HOOK_SLOT_SIZE > ALLOC_SIZE:
        print("挂钩槽位用尽"); k32.CloseHandle(h); return 1
    stub_va = page + HOOK_BASE_OFF + n * HOOK_SLOT_SIZE
    slot_va = page + slot_off

    # 安全闸：被覆盖的字节要原样搬到 stub 执行。若里面有块外的相对跳转/rip 寻址，
    # 搬过去就会跳错地址 → 崩游戏（2026-10-25 16:01 真实事故）。直接拒绝。
    bad = unsafe_replay(orig, fn_va, stub_va)
    if bad:
        print("✗ 这段函数头不能安全挂钩（覆盖 %d 字节）：" % L)
        for b in bad:
            print("    " + b)
        print("  原因：stub 与原函数相距太远，相对跳转无法原样搬运。入口未改动。")
        k32.CloseHandle(h); return 1

    stub = build_capture_stub(slot_va, fn_va + L, orig)
    if not write_mem(h, stub_va, stub):
        print("stub 写入失败"); k32.CloseHandle(h); return 1
    back = read_mem(h, stub_va, len(stub))
    if back != stub:
        print("✗ 捕获 stub 落地校验失败（写入 %d，读回 %s），入口未改动" % (
            len(stub), len(back) if back else "失败"))
        k32.CloseHandle(h); return 1
    jmp = b"\xFF\x25\x00\x00\x00\x00" + struct.pack("<Q", stub_va)
    if not write_mem(h, fn_va, jmp):
        print("入口跳转写入失败"); k32.CloseHandle(h); return 1
    cur2 = read_mem(h, fn_va, L)
    if cur2[:len(jmp)] != jmp:
        print("✗ 入口回读不符（%s），回滚" % (cur2.hex(" ") if cur2 else "?"))
        write_mem(h, fn_va, orig)
        k32.CloseHandle(h); return 1

    hooks[key] = {"slot": slot_va, "stub": stub_va, "len": L, "orig": orig.hex()}
    save_state(st)
    print("✓ 已挂钩 RVA %#x（覆盖 %d 字节，原始 %s）" % (rva, L, orig.hex(" ")))
    print("  入口 %#x → stub %#x，捕获结果写到 %#x" % (fn_va, stub_va, slot_va))
    print("  现在去游戏里触发一次，然后 python icall.py read %#x 8" % slot_off)
    k32.CloseHandle(h)
    return 0


def cmd_unhook(rva=None):
    st = load_state()
    if not st:
        print("未安装"); return 1
    h, pid, base = open_game()
    if not h:
        return 1
    hooks = st.get("hooks", {})
    if not hooks:
        print("没有挂钩"); k32.CloseHandle(h); return 1
    keys = [str(rva)] if rva is not None else list(hooks)
    for k in keys:
        if k not in hooks:
            print("未挂钩 RVA %s" % k); continue
        info = hooks[k]
        ok = write_mem(h, base + int(k), bytes.fromhex(info["orig"]))
        print(("✓ 已还原 RVA %s" if ok else "✗ 还原失败 RVA %s") % k)
        if ok:
            hooks.pop(k, None)
    save_state(st)
    k32.CloseHandle(h)
    return 0


def main():
    global ALLOW_ANY
    if len(sys.argv) < 2:
        print(__doc__); return 1
    if "--force" in sys.argv:
        ALLOW_ANY = True
        print("⚠ --force：已关掉「必须是函数入口」的校验")
    cmd = sys.argv[1]
    if cmd == "install":
        return cmd_install()
    if cmd == "uninstall":
        return cmd_uninstall()
    if cmd == "status":
        return cmd_status()
    if cmd == "hook":
        slot = int(sys.argv[3], 0) if len(sys.argv) > 3 else CAPTURE_OFF
        return cmd_hook(int(sys.argv[2], 0) & 0x0FFFFFFF, slot)
    if cmd == "unhook":
        return cmd_unhook(int(sys.argv[2], 0) & 0x0FFFFFFF if len(sys.argv) > 2 else None)
    if cmd == "bootstrap":
        return cmd_bootstrap()
    if cmd == "str":
        return cmd_mkstring_demo(" ".join(sys.argv[2:]) or "test")
    if cmd == "stub":
        # 离线自检：打印将要写入的 stub 反汇编，不需要游戏在跑
        import icall_asm
        orig = orig_bytes()
        resume_va = 0x180000000 + HOST_RVA + PATCH_LEN
        s = icall_asm.build(0x1E000000, resume_va, orig)
        for off, mn, ops, sz in icall_asm.disasm(s):
            print("  +%#06x  %-9s %s" % (off, mn, ops))
        bad = icall_asm.check(s, 0x1E000000, resume_va, len(orig))
        print("\n长度 %d 字节   静态校验：%s" % (len(s), "通过" if not bad else bad))
        return 0 if not bad else 1
    if cmd == "callx":
        raw = int(sys.argv[2], 0)
        if "--abs" in sys.argv:
            target = raw
        else:
            b = find_module_base(find_pid() or 0) or 0
            if not b:
                print("游戏未运行"); return 1
            target = b + raw
        args, ret, wait, i = [], "int", 6.0, 3
        this = None
        sret = 0
        while i < len(sys.argv):
            a = sys.argv[i]
            if a == "--wait":
                wait = float(sys.argv[i + 1]); i += 2; continue
            if a == "--sret":
                sret = int(sys.argv[i + 1], 0); i += 2; continue
            if a == "--ret":
                ret = sys.argv[i + 1]; i += 2; continue
            if a == "--this":
                # 单独存，最后 unshift —— 实例方法的 this 永远是第 1 个参数(rcx)，
                # 不能跟着命令行书写顺序走，否则 --i 5 --this X 会把它排到第二位。
                this = int(sys.argv[i + 1], 0); i += 2; continue
            if a == "--i":
                args.append(int(sys.argv[i + 1], 0)); i += 2; continue
            if a == "--f":
                args.append(float(sys.argv[i + 1])); i += 2; continue
            if a == "--f32":
                args.append(F32(sys.argv[i + 1])); i += 2; continue
            if a == "--s":
                args.append(S(sys.argv[i + 1])); i += 2; continue
            i += 1
        if this is not None:
            args.insert(0, this)
        if ret in STRUCT_RET:
            sret = STRUCT_RET[ret][1]
        print("target=%#x  %d 个参数  ret=%s%s"
              % (target, len(args), ret, "  sret=%d 字节" % sret if sret else ""))
        if sret:
            data = call_struct(target, args, sret, wait=wait)
            if data is None:
                return 1
            n = STRUCT_RET[ret][0] if ret in STRUCT_RET else sret // 4
            vals = unpack_floats(data, n)
            print("返回 = (%s)" % ", ".join("%.6g" % v for v in vals))
            return 0
        r = call_typed(target, args, ret=ret, wait=wait)
        print("返回 = %r" % (r,))
        return 0 if r is not None else 1
    if cmd == "read":
        off = int(sys.argv[2], 0)
        n = int(sys.argv[3], 0) if len(sys.argv) > 3 else 64
        return cmd_read(off, n, "--va" in sys.argv)
    if cmd == "call":
        raw = int(sys.argv[2], 0)
        target = raw if "--abs" in sys.argv else (find_module_base(find_pid() or 0) or 0) + (raw & 0x0FFFFFFF)
        if not target:
            print("游戏未运行"); return 1
        args, wait = [], 5.0
        i = 3
        while i < len(sys.argv):
            a = sys.argv[i]
            if a == "--wait":
                wait = float(sys.argv[i + 1]); i += 2; continue
            if a.startswith("--arg"):
                v = sys.argv[i + 1]
                args.append(v if v == "scratch" else int(v, 0)); i += 2; continue
            i += 1
        return cmd_call(target, args, wait)
    print(__doc__); return 1


if __name__ == "__main__":
    sys.exit(main())
