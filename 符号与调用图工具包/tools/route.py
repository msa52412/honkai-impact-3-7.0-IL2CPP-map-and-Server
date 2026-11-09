"""从方法表生成「怎么调」的完整方案 —— 把六步法做成一条命令。

这一轮换角色的过程是手工走的六步，每一步都可能出错：

    1. 在 bh3_symtab.tsv 里按功能/类名找到方法
    2. 确认 RVA 是函数入口（不是被 COMDAT 折叠的共享桩、不是函数中段）
    3. 反汇编看实现：要不要 this？是不是 IFix 热更新壳？调了什么？
    4. 若不要 this：提取 [rip+X] 全局槽，走五跳链拿单例
    5. 若要 this：找该 manager 的静态访问器（或用 mgr.py 的槽表）
    6. 按签名装箱参数，判断返回类型（含 sret），生成命令

本工具把这六步一次跑完，输出一份可直接照抄的方案。

用法
----
    python route.py "GalTouchModule::get_MainPageGalTouchAvatarID"
    python route.py 0x14A68A0
    python route.py "GalTouchModule::ChangeMainPageAvatar" --args 301,0,0
    python route.py --search "MainPageAvatar"        # 按关键字列候选方法

判据说明
--------
* **IFix 壳**：函数体只剩 `IsPatched(id)` + `GetPatch(id)` 转发。这类方法的真实
  逻辑在 `ILFixDynamicMethodWrapper` 里，静态读不出；调用本身仍然有效（直接 call
  原 RVA 会走 patch），但**不能靠反汇编推断它做了什么**。
* **是否要 this**：统计前 60 条指令里 `rcx` 作为被读基址的次数。静态访问器
  （如 `get_galTouchModule`）为 0，能直接从 `[rip+slot]` 起链。
* **共享桩**：同一 RVA 被多个方法名占用，说明它们的机器码完全相同，链接器做了
  折叠（典型是 `mov rax,[rcx+disp]; ret` 这类字段 getter）。此时该 RVA 不能当
  「某个方法的入口」用，但反过来直接把字段偏移告诉了你。
"""
import os as _os, sys as _sys
_sys.path.insert(0, _os.path.dirname(_os.path.abspath(__file__)))
try:
    import _paths
except ImportError:
    _paths = None

import os
import struct
import sys

from capstone import CS_ARCH_X86, CS_MODE_64, CS_OP_IMM, CS_OP_MEM, CS_OP_REG, Cs
from capstone.x86 import X86_REG_RCX, X86_REG_RDX, X86_REG_RIP
HERE = os.path.dirname(os.path.abspath(__file__))
ROOT = os.path.dirname(os.path.dirname(HERE))
SYMTAB = _paths.SYMTAB if _paths else os.path.join(HERE, "bh3_symtab.tsv")
DLL = _paths.DLL if _paths else ""

IMAGE_SCN_MEM_EXECUTE = 0x20000000
IMAGE_SCN_MEM_WRITE = 0x80000000

md = Cs(CS_ARCH_X86, CS_MODE_64)
md.detail = True

# 表头：rva ns cls method ret params isStatic token module aliasOf
COL_RVA, COL_NS, COL_CLS, COL_METHOD, COL_RET, COL_PARAMS, \
    COL_STATIC, COL_TOKEN, COL_MODULE, COL_ALIAS = range(10)


class PE:
    def __init__(self, path):
        self.data = open(path, "rb").read()
        pe = struct.unpack_from("<I", self.data, 0x3C)[0]
        assert self.data[pe:pe + 4] == b"PE\0\0"
        nsec = struct.unpack_from("<H", self.data, pe + 6)[0]
        opt = pe + 24
        self.image_base = struct.unpack_from("<Q", self.data, opt + 24)[0]
        self.secs = []
        so = opt + struct.unpack_from("<H", self.data, pe + 20)[0]
        for i in range(nsec):
            b = so + i * 40
            name = self.data[b:b + 8].rstrip(b"\0").decode("ascii", "replace")
            vsz, va, rsz, raw = struct.unpack_from("<IIII", self.data, b + 8)
            ch = struct.unpack_from("<I", self.data, b + 36)[0]
            self.secs.append(dict(name=name, va=va, vsz=vsz, rsz=rsz, raw=raw, ch=ch))

    def off(self, rva):
        for s in self.secs:
            if s["va"] <= rva < s["va"] + max(s["vsz"], s["rsz"]):
                return s["raw"] + (rva - s["va"])
        return None

    def sec_of(self, rva):
        for s in self.secs:
            if s["va"] <= rva < s["va"] + max(s["vsz"], s["rsz"]):
                return s
        return None

    def is_data(self, rva):
        s = self.sec_of(rva)
        return bool(s and (s["ch"] & IMAGE_SCN_MEM_WRITE) and not (s["ch"] & IMAGE_SCN_MEM_EXECUTE))


def load_symtab():
    rows = []
    with open(SYMTAB, encoding="utf-8", errors="replace") as f:
        for line in f:
            p = line.rstrip("\n").split("\t")
            if len(p) < 10:
                continue
            try:
                rows.append((int(p[COL_RVA], 16), p))
            except ValueError:
                continue
    rows.sort()
    return rows


def split_params(s):
    """按 <> 深度切逗号（Dictionary<int, string> 不能被空格/逗号切碎）。"""
    if not s or not s.strip():
        return []
    out, cur, depth = [], [], 0
    for ch in s:
        if ch == "<":
            depth += 1
        elif ch == ">":
            depth -= 1
        if ch == "," and depth == 0:
            out.append("".join(cur).strip())
            cur = []
        else:
            cur.append(ch)
    if cur:
        out.append("".join(cur).strip())
    return [x for x in out if x]


RVA_IS_PATCHED = 0x35D7F80   # IFix.WrappersManagerImpl::IsPatched(int id)

# 符号表的类型列用的是**简写 mangle 形式**（不是 System.Int32 / UnityEngine.Vector3）：
#   ret 列      : void / bool / int / object / string / float / UnityEngine_Vector3
#   params 列   : "int avatarID, MoleMole.GalTouchType touchType, bool isSwitchTwins"
#                 —— 基本类型简写、类名带 . 或 _、泛型写成 List`1<...>
# 所以映射表必须按简写写，且参数要先剥掉变量名。
BOX = {
    "bool": ("int", "只保证 al 有效，判断请 & 0xFF"),
    "int": ("int", ""),
    "uint": ("int", ""),
    "short": ("int", ""),
    "ushort": ("int", ""),
    "long": ("int", ""),
    "ulong": ("int", ""),
    "byte": ("int", ""),
    "sbyte": ("int", ""),
    "char": ("int", ""),
    "IntPtr": ("int", ""),
    "float": ("F32", "float32 —— 必须用 F32 标记，按 double 传会静默变成 0.0f"),
    "double": ("float", "double，走 xmm 低 64 位"),
    "string": ("S", "托管字符串，需先 icall.py bootstrap"),
}

RET_NOTE = {
    "void": ("void", "不回读"),
    "bool": ("int", "只保证 al 有效，判断请 & 0xFF"),
    "int": ("int", "rax 低 32 位"),
    "uint": ("int", "rax 低 32 位"),
    "byte": ("int", ""),
    "short": ("int", ""),
    "float": ("f32", "xmm0 低 32 位"),
    "double": ("float", "xmm0 低 64 位"),
    "string": ("str", "Il2CppString*：+0x10 长度 / +0x14 UTF-16 字符"),
    "object": ("int", "托管对象指针"),
    "IntPtr": ("int", ""),
    "UnityEngine_Vector3": ("vec3", "12 字节 -> sret，走 call_struct"),
    "UnityEngine_Vector3Int": ("vec3", "12 字节 -> sret，走 call_struct"),
    "UnityEngine_Vector4": ("vec4", "16 字节 -> sret"),
    "UnityEngine_Quaternion": ("quat", "16 字节 -> sret"),
    "UnityEngine_Color": ("color", "16 字节 -> sret"),
    "UnityEngine_Rect": ("rect", "16 字节 -> sret"),
    "UnityEngine_Bounds": ("bounds", "24 字节 -> sret"),
}


def type_of(decl):
    """'int avatarID' -> 'int'；'MoleMole.GalTouchType touchType' -> 'MoleMole.GalTouchType'。"""
    return decl.split(" ", 1)[0].strip()


def this_usage(ins_list, head=None, lookahead=8, this_reg=None):
    """判断 this（rcx）到底有没有被用。

    `mov rbx, rcx` 这种「先保存」很常见，**但保存不等于使用** ——
    `BaseMonoUIAvatar::get_galTouchModule` 第 2 条就是 `mov rbx,rcx`，随后 rbx
    立刻被全局链的值覆盖，this 从头到尾没被读。而
    `get_MainPageGalTouchAvatarID` 里 `mov rcx,rbx; call get_MainPageGalTouchInfo`
    把 this 当下一层的 this 传了出去 —— 这才是真用。

    所以正确判据是：找到保存 this 的寄存器，在它**被覆盖之前**，是否出现过
        a) 以它为基址的内存读   [saved+disp]
        b) 被搬回 rcx 并紧接一个 call（当作下一层的 this / 参数）
    另有一种情况是函数直接用 `[rcx+disp]` 而不保存，也计入。

    返回 (need_this, evidence)。
    """
    # 保存 this 的指令在 IFix 壳里、位于 jne **之前**，不在主路径内 ——
    # 所以要从完整函数头找保存点，再在主路径里追踪它。
    this_reg = X86_REG_RCX if this_reg is None else this_reg
    tname = "rdx" if this_reg == X86_REG_RDX else "rcx"
    head = head if head is not None else ins_list
    saved = None
    save_addr = None
    for ins in head[:lookahead]:
        if ins.mnemonic != "mov" or len(ins.operands) != 2:
            continue
        d, src = ins.operands
        if (d.type == CS_OP_REG and src.type == CS_OP_REG
                and src.reg == this_reg and d.reg != this_reg):
            saved, save_addr = d.reg, ins.address
            break

    # 情况 c：直接 [rcx+disp]
    for ins in ins_list:
        for op in ins.operands:
            if op.type == CS_OP_MEM and op.mem.base == this_reg:
                return True, "读 [%s%+d] @%#x" % (tname, op.mem.disp, ins.address)

    if saved is None:
        return False, None

    for idx, ins in enumerate(ins_list):
        # 保存 this 的那条指令本身不算「使用」，从它之后开始追踪
        if save_addr is not None and ins.address <= save_addr:
            continue
        for op in ins.operands:
            if op.type == CS_OP_MEM and op.mem.base == saved:
                return True, "读 [%s%+d] @%#x" % (ins.reg_name(saved), op.mem.disp, ins.address)
        if ins.mnemonic == "mov" and len(ins.operands) == 2:
            d, s = ins.operands
            # 注意目的固定是 rcx —— call 的第一个参数总在 rcx，
            # 而 sret 方法的 this 是从 rdx 挪进来的，所以不能拿 this_reg 比。
            if (d.type == CS_OP_REG and s.type == CS_OP_REG
                    and d.reg == X86_REG_RCX and s.reg == saved):
                nxt = ins_list[idx + 1] if idx + 1 < len(ins_list) else None
                if nxt and nxt.mnemonic in ("call", "jmp"):
                    return True, "把 this 传出给 %s @%#x" % (nxt.op_str, nxt.address)
        # saved 被写 => 之后不再是 this
        try:
            _r, writes = ins.regs_access()
        except Exception:
            writes = []
        if saved in writes:
            break
    return False, None


def split_main_path(ins_list):
    """切出 IFix 壳里的**主路径**，避开 patch 分支。

    IFix 壳的形状是：

        mov rbx, rcx
        mov ecx, <patchId>
        call IsPatched
        test al, al
        jne <patch_branch>      ← 跳过去就是 patch 分支
        <主路径...>
        ret / jmp
        <patch_branch...>       ← 这里才用 this 调 wrapper

    如果连 patch 分支一起分析，`[rcx+disp]` 会被误当成「用了 this」。
    get_galTouchModule 就是这么被误判的：主路径纯全局链，patch 分支才有 this。

    返回 (主路径指令列表, 是否 IFix 壳)。
    """
    for i, ins in enumerate(ins_list):
        if ins.mnemonic != "jne" or i == 0 or ins_list[i - 1].mnemonic != "test":
            continue
        try:
            tgt = int(ins.op_str, 0)
        except ValueError:
            continue
        if tgt <= ins.address:
            continue
        main = [x for x in ins_list[i + 1:] if x.address < tgt]
        for j, x in enumerate(main):
            if x.mnemonic in ("ret", "jmp"):
                main = main[:j + 1]
                break
        return main, True
    return ins_list[:80], False


def analyze(pe, rva, limit=None, maxins=400, sret=False):
    """提取三件事：用不用 this、是不是 IFix 壳、有没有静态全局槽。

    this 判定不能用「[rcx+disp] 出现次数」—— 那样会漏掉 `mov rbx,rcx` 后转发给
    别的方法的写法（get_MainPageGalTouchAvatarID 就属于这种，第一版误判成静态）。
    正确做法是看 capstone 的 regs_access()：rcx 是否出现在**读**集合里。
    `mov ecx, imm` 是**写** rcx 低 32 位（设 patch id），不算用 this。
    """
    off = pe.off(rva)
    if off is None:
        return None
    span = maxins * 8
    if limit and limit > rva:
        span = min(span, limit - rva)
    code = pe.data[off:off + span]
    va = pe.image_base + rva
    ins_list = list(md.disasm(code, va))[:maxins]

    main, is_ifix = split_main_path(ins_list)
    slots = []
    patched = None
    prev_ecx_imm = None
    need_this, evidence = this_usage(
        main, head=ins_list, this_reg=X86_REG_RDX if sret else X86_REG_RCX)
    # patch id 从全函数找（壳在入口，主路径里不会有第二个 IsPatched）
    for ins in ins_list:
        if ins.mnemonic == "mov" and ins.op_str.startswith("ecx, "):
            try:
                prev_ecx_imm = int(ins.op_str.split(", ")[1], 0)
            except ValueError:
                prev_ecx_imm = None
        elif ins.mnemonic == "call":
            for op in ins.operands:
                if op.type == CS_OP_IMM:
                    tr = op.imm - pe.image_base
                    if tr == RVA_IS_PATCHED and prev_ecx_imm is not None:
                        patched = prev_ecx_imm
            if patched is None:
                prev_ecx_imm = None

    for ins in main:
        if ins.mnemonic != "mov":
            continue          # call/jmp [rip+x] 是虚表/导入表，不是 manager 槽
        for op in ins.operands:
            if op.type == CS_OP_MEM and op.mem.base == X86_REG_RIP:
                tgt = ins.address + ins.size + op.mem.disp - pe.image_base
                if pe.is_data(tgt):
                    slots.append((ins.address - pe.image_base, tgt))
    return dict(ins=ins_list, main=main, need_this=need_this, evidence=evidence,
                slots=slots, patched=patched)


def cmd_search(kw):
    rows = load_symtab()
    kw = kw.lower()
    n = 0
    for rva, p in rows:
        full = ("%s::%s" % (p[COL_CLS], p[COL_METHOD])).lower()
        if kw in full:
            print("  %#010x  %s::%s(%s) -> %s" % (rva, p[COL_CLS], p[COL_METHOD], p[COL_PARAMS], p[COL_RET]))
            n += 1
            if n >= 60:
                print("  ...（超过 60 条，请加长关键字）")
                break
    if n == 0:
        print("无匹配")
    return 0 if n else 1


def main():
    argv = sys.argv[1:]
    if not argv:
        print(__doc__)
        return 1
    if argv[0] == "--search":
        return cmd_search(argv[1])

    pe = PE(DLL)
    rows = load_symtab()
    classes = set(q[COL_CLS] for _r, q in rows)
    if argv[0].startswith("0x") or argv[0].startswith("0X"):
        rva = int(argv[0], 0)
        hits = [p for r, p in rows if r == rva]
        if not hits:
            print("✗ %#x 不是任何方法的入口（可能是函数中段，或符号表未覆盖）" % rva)
            return 1
    else:
        want = argv[0]
        hits = [p for r, p in rows if "%s::%s" % (p[COL_CLS], p[COL_METHOD]) == want]
        if not hits:
            print("✗ 符号表里没有 %r，用 --search 模糊找" % want)
            return 1
        rva = int(hits[0][COL_RVA], 16)

    p = hits[0]
    print("=== %s::%s" % (p[COL_CLS], p[COL_METHOD]))
    print("  RVA    %#x   段 %s" % (rva, pe.sec_of(rva)["name"] if pe.sec_of(rva) else "?"))
    print("  返回   %s" % p[COL_RET])
    print("  参数   %s" % (p[COL_PARAMS] or "(无)"))
    print("  模块   %s   isStatic %s   token %s"
          % (p[COL_MODULE], p[COL_STATIC], p[COL_TOKEN]))
    if p[COL_ALIAS]:
        print("  别名   %s  ← 机器码完全相同（链接器折叠）" % p[COL_ALIAS])

    if len(hits) > 1 or p[COL_ALIAS]:
        others = ["%s::%s" % (q[COL_CLS], q[COL_METHOD]) for q in hits[1:6]]
        if len(hits) > 1:
            print("\n⚠ 同一 RVA 被 %d 个方法共用（链接器 COMDAT 折叠，机器码完全相同）：" % len(hits))
            for o in others:
                print("    %s" % o)
        print("  ⇒ 该 RVA **不能**当作「%s」的专属入口；但它直接暴露了字段偏移"
              "（典型是 `mov rax,[rcx+disp]; ret`）。" % p[COL_METHOD])

    # 下一个函数的 RVA 作为本函数上界，避免把邻接函数当成本函数的一部分
    limit = None
    for r, _q in rows:
        if r > rva:
            limit = r
            break
    # 先定返回类型：>8 字节的结构体是 sret，此时 this 在 rdx 而不是 rcx
    ret = p[COL_RET]
    rk, rnote = RET_NOTE.get(ret, RET_NOTE.get(ret.replace(".", "_"),
                                               ("int", "未知类型，按指针读 rax")))
    is_sret = rk in ("vec3", "vec4", "quat", "color", "rect", "bounds")
    a = analyze(pe, rva, limit, sret=is_sret)
    if not a:
        print("✗ 取不到代码")
        return 1

    print("\n--- 实现特征（前 %d 条指令）" % min(len(a["ins"]), 60))
    if a["patched"] is not None:
        print("  IFix   是（首个 patch id = %#x）。真实逻辑在 ILFixDynamicMethodWrapper 里，"
              "静态读不出；但直接 call 原 RVA 仍会走 patch。" % a["patched"])
    else:
        print("  IFix   否（未见 IsPatched 转发）")
    if not a["need_this"]:
        print("  this   不需要 —— 保存了 rcx 但从未使用，是静态访问器（可从 [rip+slot] 直接起链）")
    else:
        print("  this   需要 —— 证据：%s" % a["evidence"])

    if a["slots"]:
        if a["need_this"]:
            print("\n--- 主路径引用到的全局（**不是** this 的来源，仅供交叉参考）")
        else:
            print("\n--- 静态全局槽：该类单例就在这里，可直接起链")
        seen = set()
        for at, tgt in a["slots"]:
            if tgt in seen:
                continue
            seen.add(tgt)
            print("  槽 RVA %#x   （在 %#x 处被引用）" % (tgt, at))
            print("     取实例: python mgr.py get %#x" % tgt)
        if not a["need_this"]:
            print("  ⇒ 判据：槽 → k1=*(G0) → *(k1+0x90) → *(..) → *(..+0x28) → inst")

    params = [type_of(x) for x in split_params(p[COL_PARAMS])]
    ret = p[COL_RET]
    print("\n--- 参数装箱")
    if not params:
        print("  无参数")
    else:
        for i, t in enumerate(params, 1):
            if t in BOX:
                kind, note = BOX[t]
                print("  %d. %-42s -> %-6s %s" % (i, t, kind, note or ""))
            elif t.endswith("[]"):
                print("  %d. %-42s -> int    数组指针（需自己构造 Il2CppArray）" % (i, t))
            elif t.replace(".", "_") in RET_NOTE:
                print("  %d. %-42s -> int    struct 按值传入，>8B 需手动 marshalling" % (i, t))
            else:
                # 枚举/纯数据 struct 在符号表里**没有任何方法**，而真类一定有一堆。
                # 据此区分「按 32 位整数传」和「传对象指针」。
                tail = t.replace("_", ".").split(".")[-1]
                if tail in classes:
                    print("  %d. %-42s -> int    引用类型，传对象指针" % (i, t))
                else:
                    print("  %d. %-42s -> int    枚举/纯数据 struct，按 32 位整数传" % (i, t))
    print("  返回 -> --ret %s   （%s）" % (rk, rnote))

    if a["need_this"]:
        print("\n--- 下一步")
        print("  1) 找该类的单例槽:  python mgr.py find %s" % p[COL_CLS])
        print("  2) 或从场景/其他对象拿: GetComponentByName(\"%s\")" % p[COL_CLS])

    # 调用模板
    args_ex = "--arg 0 " * len(params)
    this_part = "--this <实例> " if a["need_this"] else ""
    print("\n--- 调用模板")
    print('  python call_name.py call "%s::%s" %s%s--ret %s --rva %#x'
          % (p[COL_CLS], p[COL_METHOD], this_part, args_ex, rk, rva))
    return 0


if __name__ == "__main__":
    sys.exit(main())
