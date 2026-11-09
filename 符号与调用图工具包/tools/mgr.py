"""manager 单例槽的发现与获取 —— 把「按方法表走」沉淀成可复用能力。

本引擎（腾讯处理后 il2cpp）所有 Module 单例都是同一个形状。反汇编里出现下面
这一串，就说明找到了该 manager 的全局槽 `slot`（相对模块基址的 RVA）：

    mov rax, [rip + X]          ; slot = 该指令末尾 + X，落在 .data
    mov rbx, [rax]              ; k1 = klass，+0x136 是 class-init 标志（0 需先 Init）
    cmp byte [rbx+0x136], 0
    mov rax, [rbx+0x90]         ; B
    mov rbx, [rax]              ; k2
    mov rax, [rbx+0x28]         ; D
    mov rcx, [rax]              ; inst = 单例实例
    test rcx, rcx / je ...

`GalTouchModule::get_galTouchModule` @0x3082E20 就是这个形状，而且它主路径完全
不用 `this` —— 说明这类访问器就是静态单例入口，**不需要任何对象/反射**。

有了这张槽表，「拿某个 manager」从「找路径、猜组件类名、扫描类指针」退化成一次
五跳内存读，零游戏调用。

用法
----
    python mgr.py scan                     # 离线：扫代码段所有 [rip+disp] -> .data 的候选槽
    python mgr.py probe                    # 在线：逐个走链验证，写 bh3_mgr_slots.tsv
    python mgr.py table                    # 显示已确认的槽表
    python mgr.py get 0x9AA3488            # 取某槽的实例（走链 + 类名自校验）
    python mgr.py find GalTouch            # 按类名模糊查槽
    python mgr.py fields 0x9AA3488         # dump 实例前 0x100 字节并按 qword 注解
"""
import os
import struct
import sys

import numpy as np

HERE = os.path.dirname(os.path.abspath(__file__))
ROOT = os.path.dirname(os.path.dirname(HERE))
sys.path.insert(0, HERE)
sys.path.insert(0, os.path.join(ROOT, "_analysis"))

import icall as I            # noqa: E402
import rip_dis as RD         # noqa: E402

DLL = RD.DLL
SLOT_TABLE = os.path.join(HERE, "bh3_mgr_slots.tsv")

IMAGE_SCN_MEM_EXECUTE = 0x20000000
IMAGE_SCN_MEM_WRITE = 0x80000000

OFF_INIT_FLAG = 0x136
OFF_VT1 = 0x90
OFF_VT2 = 0x28


class PE2:
    """带段属性的极简 PE 解析（rip_dis.PE 不带 characteristics）。"""

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

    def sec_of(self, rva):
        for s in self.secs:
            if s["va"] <= rva < s["va"] + max(s["vsz"], s["rsz"]):
                return s
        return None


def scan_exec():
    """离线扫：所有 `REX.W 8B /r [rip+disp32]` 且目标落在可写数据段的位置。

    指令形如 `48 8B 05 disp32`（mov rax,[rip+d]），共 7 字节。
    也可能出现 4C 前缀（r8-r15）。ModRM 的 rm 必须 == 101，即 (modrm & 0xC7) == 0x05。
    """
    pe = PE2(DLL)
    exec_secs = [s for s in pe.secs if s["ch"] & IMAGE_SCN_MEM_EXECUTE]
    data_secs = [s for s in pe.secs
                 if (s["ch"] & IMAGE_SCN_MEM_WRITE) and not (s["ch"] & IMAGE_SCN_MEM_EXECUTE)]
    print("可执行段: %s" % [s["name"] for s in exec_secs])
    print("可写数据段: %s" % [s["name"] for s in data_secs])
    if not data_secs:
        print("✗ 没有可写数据段，判据不成立")
        return {}

    def in_data(rva):
        for s in data_secs:
            if s["va"] <= rva < s["va"] + max(s["vsz"], s["rsz"]):
                return True
        return False

    hits = {}
    for s in exec_secs:
        raw = pe.data[s["raw"]: s["raw"] + s["rsz"]]
        if not raw:
            continue
        b = np.frombuffer(raw, dtype=np.uint8)
        idx = np.nonzero(b == 0x8B)[0]
        for i in idx:
            i = int(i)
            if i < 1 or i + 6 > len(b) - 1:
                continue
            if b[i - 1] not in (0x48, 0x4C):
                continue
            if (b[i + 1] & 0xC7) != 0x05:
                continue
            disp = struct.unpack_from("<i", b, i + 2)[0]
            end_rva = s["va"] + i + 6          # REX(1)+8B(1)+ModRM(1)+disp(4) = 7，起点是 i-1
            tgt = end_rva + disp
            if in_data(tgt):
                hits[tgt] = hits.get(tgt, 0) + 1
    return hits


def chain(h, base, slot, verbose=False):
    """走五跳链，返回 (inst, steps)。任何一跳为 0 就返回 None。"""
    steps = []
    g0 = I.read_mem(h, base + slot, 8)
    if not g0:
        return None, steps
    g0 = struct.unpack("<Q", g0)[0]
    steps.append(("G0", g0))
    if not g0:
        return None, steps
    k1 = struct.unpack("<Q", I.read_mem(h, g0, 8) or b"\0" * 8)[0]
    steps.append(("k1", k1))
    if not k1:
        return None, steps
    b = struct.unpack("<Q", I.read_mem(h, k1 + OFF_VT1, 8) or b"\0" * 8)[0]
    steps.append(("B", b))
    if not b:
        return None, steps
    k2 = struct.unpack("<Q", I.read_mem(h, b, 8) or b"\0" * 8)[0]
    steps.append(("k2", k2))
    if not k2:
        return None, steps
    dd = struct.unpack("<Q", I.read_mem(h, k2 + OFF_VT2, 8) or b"\0" * 8)[0]
    steps.append(("D", dd))
    if not dd:
        return None, steps
    inst = struct.unpack("<Q", I.read_mem(h, dd, 8) or b"\0" * 8)[0]
    steps.append(("inst", inst))
    if verbose:
        for n, v in steps:
            print("  %-6s = %s" % (n, ("%#x" % v) if v else "null"))
    return inst or None, steps


def klass_name(h, obj):
    k = I.read_mem(h, obj, 8)
    if not k:
        return None
    k = struct.unpack("<Q", k)[0]
    np_ = I.read_mem(h, k + 0xA8, 8)
    if not np_:
        return None
    np_ = struct.unpack("<Q", np_)[0]
    raw = I.read_mem(h, np_, 64)
    if not raw:
        return None
    e = raw.find(b"\x00")
    if e <= 0:
        return None
    try:
        s = raw[:e].decode("ascii")
    except UnicodeDecodeError:
        return None
    return s if s.isprintable() and len(s) > 1 else None


# 已用真机验证过的槽（probe 之前也能直接用）
KNOWN_SLOTS = [
    (0x09AA3488, "GalTouchModule"),   # BaseMonoUIAvatar::get_galTouchModule 主路径
    (0x09A988C8, "AvatarModule"),     # NewbieDialogContext::ReplaceGalTouchAvatar 主路径
]


def load_table():
    rows = list(KNOWN_SLOTS)
    if not os.path.exists(SLOT_TABLE):
        return rows
    with open(SLOT_TABLE, encoding="utf-8") as f:
        for line in f:
            p = line.rstrip("\n").split("\t")
            if len(p) < 2:
                continue
            rows.append((int(p[0], 16), p[1]))
    return rows


def save_table(rows):
    rows = sorted(set(rows))
    with open(SLOT_TABLE, "w", encoding="utf-8") as f:
        for slot, name in rows:
            f.write("%#x\t%s\n" % (slot, name))
    return len(rows)


def cmd_scan():
    hits = scan_exec()
    print("\n候选槽 %d 个（[rip+disp] 指向可写段）" % len(hits))
    top = sorted(hits.items(), key=lambda kv: -kv[1])
    print("引用次数 top 30:")
    for tgt, n in top[:30]:
        print("  %#x  x%d" % (tgt, n))
    return 0


def cmd_probe(top=0):
    """批量预筛 + 走链验证。

    260k 个候选逐个走链要 5 次读/个，太慢。真正的前提是「槽里存着一个指针」，
    所以先把 .data 整段读进本地，用 numpy 一次取出所有槽的 G0，只对像堆指针的
    再走链。这一步把候选砍掉一个数量级。
    """
    pe = PE2(DLL)
    dsec = next((s for s in pe.secs if s["name"] == ".data"), None)
    if not dsec:
        print("✗ 没有 .data 段")
        return 1
    hits = scan_exec()
    cands = [t for t, _ in sorted(hits.items(), key=lambda kv: -kv[1])]
    if top:
        cands = cands[:top]

    h, pid, base = I.open_game()
    if not h:
        return 1

    size = dsec["vsz"]
    step = 0x100000
    buf = bytearray()
    off = 0
    while off < size:
        n = min(step, size - off)
        d = I.read_mem(h, base + dsec["va"] + off, n)
        buf += d if d else b"\0" * n
        off += n
    arr = np.frombuffer(bytes(buf), dtype="<u8", count=len(buf) // 8)
    print("\n.data 读入 %#x 字节；候选 %d 个" % (len(buf), len(cands)))

    lo = dsec["va"]
    keep = []
    for slot in cands:
        if slot % 8:
            continue
        idx = (slot - lo) // 8
        if idx < 0 or idx >= len(arr):
            continue
        v = int(arr[idx])
        # G0 必须像堆/元数据指针：非空、非小整数、8 对齐
        if 0x10000 < v < 0x7FF000000000 and v % 8 == 0:
            keep.append(slot)
    print("预筛（G0 像指针）: %d -> %d" % (len(cands), len(keep)))

    found = []
    for i, slot in enumerate(keep):
        inst, _ = chain(h, base, slot)
        if not inst:
            continue
        nm = klass_name(h, inst)
        if nm and nm[0].isalpha():
            found.append((slot, nm))
            if len(found) <= 200:
                print("  ✓ %#x -> %s" % (slot, nm))
    I.k32.CloseHandle(h)
    n = save_table(found + load_table())
    print("\n确认 %d 个，表内合计 %d 条 -> %s" % (len(found), n, SLOT_TABLE))
    return 0


def cmd_table():
    rows = load_table()
    if not rows:
        print("表为空，先跑 probe")
        return 0
    for slot, name in rows:
        print("  %#x  %s" % (slot, name))
    print("\n共 %d 条" % len(rows))
    return 0


def cmd_get(slot):
    h, pid, base = I.open_game()
    if not h:
        return 1
    inst, _ = chain(h, base, slot, verbose=True)
    print("inst = %s  类名 = %r" % (("%#x" % inst) if inst else "null",
                                    klass_name(h, inst) if inst else None))
    I.k32.CloseHandle(h)
    return 0 if inst else 1


def cmd_find(pat):
    rows = load_table()
    hit = [r for r in rows if pat.lower() in r[1].lower()]
    if not hit:
        print("表里没有匹配 %r 的槽（先 probe，或该 manager 从未被代码引用）" % pat)
        return 1
    for slot, name in hit:
        print("  %#x  %s" % (slot, name))
    return 0


def cmd_fields(slot):
    h, pid, base = I.open_game()
    if not h:
        return 1
    inst, _ = chain(h, base, slot)
    if not inst:
        print("链断了")
        I.k32.CloseHandle(h)
        return 1
    print("inst=%#x 类=%r" % (inst, klass_name(h, inst)))
    raw = I.read_mem(h, inst, 0x100)
    for off in range(0, 0x100, 8):
        v = struct.unpack_from("<Q", raw, off)[0]
        note = ""
        if 0x10000 < v < 0x7FF000000000:
            nm = klass_name(h, v)
            if nm:
                note = "-> 对象 %r" % nm
            else:
                s = I.read_mem(h, v, 48)
                if s:
                    e = s.find(b"\x00")
                    if e > 1:
                        try:
                            t = s[:e].decode("ascii")
                            if t.isprintable():
                                note = "-> 字符串 %r" % t
                        except UnicodeDecodeError:
                            pass
        lo = struct.unpack_from("<i", raw, off)[0]
        print("  +%#04x %#018x  i32=%-12d %s" % (off, v, lo, note))
    I.k32.CloseHandle(h)
    return 0


def main():
    if len(sys.argv) < 2:
        print(__doc__)
        return 1
    cmd = sys.argv[1]
    if cmd == "scan":
        return cmd_scan()
    if cmd == "probe":
        n = int(sys.argv[2]) if len(sys.argv) > 2 else 0
        return cmd_probe(n)
    if cmd == "table":
        return cmd_table()
    if cmd == "get":
        return cmd_get(int(sys.argv[2], 0))
    if cmd == "find":
        return cmd_find(sys.argv[2])
    if cmd == "fields":
        return cmd_fields(int(sys.argv[2], 0))
    print(__doc__)
    return 1


if __name__ == "__main__":
    sys.exit(main())
