# -*- coding: utf-8 -*-
"""在所有可执行段里找 call 目标函数的调用点。

为什么不复用 disasm_func.py --callers：它只扫 .text，而游戏代码几乎全在 il2cpp 段，
只扫 .text 会得出「0 个调用点」的假结论（10-31 踩过）。

用法:
    python tools\\find_callers_all.py 0x149db00
    python tools\\find_callers_all.py 0x330afc0 --max 20
"""
import os as _os, sys as _sys
_sys.path.insert(0, _os.path.dirname(_os.path.abspath(__file__)))
try:
    import _paths
except ImportError:
    _paths = None

import struct
import sys

DLL = _paths.DLL if _paths else ""
IMAGE_BASE = 0x180000000
EXEC = ("text", "il2cpp", "tvm0")


def load(path=DLL):
    data = open(path, "rb").read()
    e = struct.unpack_from("<I", data, 0x3C)[0]
    nsec = struct.unpack_from("<H", data, e + 6)[0]
    optsz = struct.unpack_from("<H", data, e + 20)[0]
    sec0 = e + 24 + optsz
    secs = []
    for i in range(nsec):
        o = sec0 + i * 40
        name = data[o:o + 8].rstrip(b"\0").decode("latin-1")
        vsize, vaddr, rsize, raddr = struct.unpack_from("<IIII", data, o + 8)
        secs.append({"name": name, "va": vaddr, "vsize": vsize,
                     "rptr": raddr, "rsize": rsize})
    return data, secs


def main():
    if len(sys.argv) < 2:
        print(__doc__)
        return 1
    target = int(sys.argv[1], 16)
    maxn = 40
    if "--max" in sys.argv:
        maxn = int(sys.argv[sys.argv.index("--max") + 1])

    data, secs = load()
    print("目标 RVA %#x (VA %#x)" % (target, IMAGE_BASE + target))

    total = 0
    for s in secs:
        key = s["name"].lower()
        if not any(k in key for k in EXEC):
            continue
        base = s["va"]
        blob = data[s["rptr"]:s["rptr"] + s["rsize"]]
        hits = []
        for i in range(len(blob) - 5):
            if blob[i] != 0xE8:
                continue
            disp = struct.unpack_from("<i", blob, i + 1)[0]
            if base + i + 5 + disp == target:
                hits.append(base + i)
        print("  段 %-8s 大小 %#x  调用点 %d 处" % (s["name"], len(blob), len(hits)))
        for a in hits[:maxn]:
            # 看调用点前 24 字节，判断是不是 tail-call / 参数准备
            print("      call @ RVA %#x" % a)
        total += len(hits)

    print()
    print("合计 %d 处调用点" % total)
    if total == 0:
        print("→ 没有直接 call。可能是：① 通过函数指针/虚表调用；② 是导出/icall；③ 地址算错。")
    return 0


if __name__ == "__main__":
    sys.exit(main())
