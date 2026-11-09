# -*- coding: utf-8 -*-
"""
field_layout.py — 从 bh3_symtab.tsv + 反汇编，自动恢复 IL2CPP 类的字段偏移。

原理：
  * symtab 给了 类::方法 -> RVA（398k 条，Assembly-CSharp 229k）
  * getter/setter 的机器码几乎必然是 `mov reg,[this+disp]` / `mov [this+disp],reg`
  * 由此反推出 字段名 + 偏移 + 宽度，无需元数据解密

用法：
  python _analysis/field_layout.py LevelScoreDLCPlugin
  python _analysis/field_layout.py LevelActor --limit 60
  python _analysis/field_layout.py LevelActor --grep Score
  python _analysis/field_layout.py --batch classes.txt --out rea/_rea/bh3_fields_auto.tsv
"""
import os as _os, sys as _sys
_sys.path.insert(0, _os.path.dirname(_os.path.abspath(__file__)))
try:
    import _paths
except ImportError:
    _paths = None

import os
import re
import sys
import json
import struct
import argparse
import collections

from capstone import Cs, CS_ARCH_X86, CS_MODE_64
from capstone.x86 import X86_OP_MEM, X86_OP_REG, X86_REG_RCX

HERE = os.path.dirname(os.path.abspath(__file__))
ROOT = os.path.dirname(HERE)
DLL = _paths.DLL if _paths else ""
SYMTAB = _paths.SYMTAB if _paths else os.path.join(ROOT, "rea", "_rea", "bh3_symtab.tsv")
PDATA = _paths.PDATA if _paths else os.path.join(ROOT, "rea", "_rea", "bh3_pdata_truth.tsv")
BASE = 0x180000000


# ---------------------------------------------------------------- PE
class PE(object):
    def __init__(self, path):
        self.d = open(path, "rb").read()
        d = self.d
        pe = struct.unpack_from("<I", d, 0x3C)[0]
        nsec = struct.unpack_from("<H", d, pe + 6)[0]
        optsz = struct.unpack_from("<H", d, pe + 20)[0]
        sec = pe + 24 + optsz
        self.secs = []
        for i in range(nsec):
            o = sec + i * 40
            nm = d[o:o + 8].rstrip(b"\0").decode("latin1")
            vsz, va, rsz, roff = struct.unpack_from("<IIII", d, o + 8)
            self.secs.append((nm, va, max(vsz, rsz), roff))
        self.end = max(va + sz for _, va, sz, _ in self.secs)

    def off(self, rva):
        for _, va, sz, roff in self.secs:
            if va <= rva < va + sz:
                return roff + rva - va
        return None


# ---------------------------------------------------------------- 表
def load_symtab(path=SYMTAB):
    """返回 {类名: [(rva, 方法名, 返回类型, 参数)]} 与全表行"""
    by_cls = collections.defaultdict(list)
    rows = []
    with open(path, encoding="utf-8") as f:
        for ln in f:
            if ln.startswith("rva"):
                continue
            p = ln.rstrip("\n").split("\t")
            if len(p) < 9 or not p[0]:
                continue
            try:
                rva = int(p[0], 16)
            except ValueError:
                continue
            rows.append((rva, p[1], p[2], p[3], p[4], p[5], p[8]))
            by_cls[p[2]].append((rva, p[3], p[4], p[5]))
    return by_cls, rows


def load_symidx(path=SYMTAB):
    """RVA -> 类::方法，用于给 call 标注"""
    idx = {}
    with open(path, encoding="utf-8") as f:
        for ln in f:
            if ln.startswith("rva"):
                continue
            p = ln.rstrip("\n").split("\t")
            if len(p) < 6 or not p[0]:
                continue
            try:
                idx[int(p[0], 16)] = (p[2], p[3])
            except ValueError:
                pass
    return idx


_PDATA_CACHE = None


def load_pdata(path=PDATA):
    """RVA -> 函数结束 RVA（用于限制反汇编范围）"""
    global _PDATA_CACHE
    if _PDATA_CACHE is not None:
        return _PDATA_CACHE
    m = {}
    with open(path, encoding="utf-8") as f:
        for ln in f:
            p = ln.split("\t")
            if len(p) >= 2:
                try:
                    s = int(p[0], 16) - BASE
                    e = int(p[1], 16) - BASE
                except ValueError:
                    continue
                if e > s:
                    m[s] = e
    _PDATA_CACHE = m
    return m


# ---------------------------------------------------------------- 反汇编辅助
REG_W = {
    "rax": 8, "rbx": 8, "rcx": 8, "rdx": 8, "rsi": 8, "rdi": 8, "rbp": 8,
    "r8": 8, "r9": 8, "r10": 8, "r11": 8, "r12": 8, "r13": 8, "r14": 8, "r15": 8,
    "eax": 4, "ebx": 4, "ecx": 4, "edx": 4, "esi": 4, "edi": 4, "ebp": 4,
    "r8d": 4, "r9d": 4, "r10d": 4, "r11d": 4, "r12d": 4, "r13d": 4, "r14d": 4, "r15d": 4,
    "ax": 2, "bx": 2, "cx": 2, "dx": 2, "si": 2, "di": 2, "bp": 2,
    "r8w": 2, "r9w": 2, "r10w": 2, "r11w": 2, "r12w": 2, "r13w": 2, "r14w": 2, "r15w": 2,
    "al": 1, "bl": 1, "cl": 1, "dl": 1, "sil": 1, "dil": 1, "bpl": 1,
    "r8b": 1, "r9b": 1, "r10b": 1, "r11b": 1, "r12b": 1, "r13b": 1, "r14b": 1, "r15b": 1,
}
# 同宽别名归并（eax 视作 rax 的低半，宽度单独记）
PARENT = {}
for g, names in ((8, ["rax", "rbx", "rcx", "rdx", "rsi", "rdi", "rbp"]), (4, []), (2, []), (1, [])):
    pass


def base_reg(name):
    """eax -> rax，dil -> rdi，用于追踪 this 别名"""
    if name in ("eax", "ax", "al"):
        return "rax"
    if name in ("ebx", "bx", "bl"):
        return "rbx"
    if name in ("ecx", "cx", "cl"):
        return "rcx"
    if name in ("edx", "dx", "dl"):
        return "rdx"
    if name in ("esi", "si", "sil"):
        return "rsi"
    if name in ("edi", "di", "dil"):
        return "rdi"
    if name in ("ebp", "bp", "bpl"):
        return "rbp"
    if name in ("r8d", "r8w", "r8b"):
        return "r8"
    if name in ("r9d", "r9w", "r9b"):
        return "r9"
    if name in ("r10d", "r10w", "r10b"):
        return "r10"
    if name in ("r11d", "r11w", "r11b"):
        return "r11"
    if name in ("r12d", "r12w", "r12b"):
        return "r12"
    if name in ("r13d", "r13w", "r13b"):
        return "r13"
    if name in ("r14d", "r14w", "r14b"):
        return "r14"
    if name in ("r15d", "r15w", "r15b"):
        return "r15"
    return name


MD = Cs(CS_ARCH_X86, CS_MODE_64)
MD.detail = True


def scan_this_fields(pe, rva, pdata, maxins=600):
    """
    反汇编一个方法，找出所有 [this+disp] 访问。
    返回 ([(disp, width, 'load'|'store')], [call 目标 RVA])

    函数边界处理：pdata 给的区间常常偏小（实测 get_rankScorePercentage 只标 23 字节，
    实际有 40+），因此先按 max(pdata, 0x120) 解码，再回退到「最后一个 ret」收口，
    既不会漏掉尾部指令，也不会吃进下一个函数。
    """
    o = pe.off(rva)
    if o is None:
        return [], []
    pdr = pdata.get(rva)
    n = 0x2000 if pdr is None else min(max(pdr - rva, 0x120), 0x2000)
    code = pe.d[o:o + n]

    ins_list = list(MD.disasm(code, BASE + rva))[:maxins]
    # 收口：截到最后一个 ret（含）；若后面紧邻 int3/nop 也一并丢弃
    last_ret = -1
    for i, ins in enumerate(ins_list):
        if ins.mnemonic.startswith("ret"):
            last_ret = i
    if last_ret >= 0:
        ins_list = ins_list[:last_ret + 1]

    this = {"rcx"}          # 实例方法：this 在 rcx
    out = []
    calls = []
    for ins in ins_list:
        ops = ins.operands
        mn = ins.mnemonic

        # 追踪 this 别名，并处理「寄存器被改写 -> 不再是 this」
        #   合法继承： mov rbx, rcx / mov rdi, rbx          （源是 this）
        #   失效：     mov rbx, [rcx+0x28] / xor eax,eax    （源不是 this）
        # 注意：失效时不能 continue，因为该指令的操作数里可能正有一个 [this+disp] 要分析
        if ops and ops[0].type == X86_OP_REG:
            d = base_reg(ins.reg_name(ops[0].reg))
            src_this = (len(ops) > 1 and ops[1].type == X86_OP_REG
                        and base_reg(ins.reg_name(ops[1].reg)) in this)
            if src_this:
                if mn in ("mov", "movzx", "movsx", "movsxd", "lea"):
                    this.add(d)
                    continue          # 纯寄存器搬运，无需分析操作数
            elif len(ops) > 1 and mn in ("mov", "movzx", "movsx", "movsxd", "lea",
                                         "xor", "sub", "add", "and", "or",
                                         "imul", "shl", "shr", "sar", "pop"):
                this.discard(d)       # 该寄存器不再持有 this，但继续分析本条指令

        # 记录 call 目标（给上层做调用链）
        if mn == "call" and ops and ops[0].type == 0x2:  # X86_OP_IMM
            calls.append(ops[0].imm - BASE)

        if not ops:
            continue
        # 任一操作数是 [this+disp]
        for oi, op in enumerate(ops):
            if op.type != X86_OP_MEM:
                continue
            br = ins.reg_name(op.mem.base)
            if br not in this:
                continue
            idxr = ins.reg_name(op.mem.index) if op.mem.index else ""
            if idxr:
                continue                      # 数组/索引访问，非固定字段
            disp = op.mem.disp
            if disp == 0 or disp < 0 or disp > 0x8000:
                continue
            width = 0
            if mn in ("cmp", "test"):          # 只读，不写字段
                direction = "load"
                if ops[0].type == X86_OP_REG:
                    width = REG_W.get(ins.reg_name(ops[0].reg), 0)
            elif oi == 0:                      # 内存操作数在目标位 -> store
                if len(ops) > 1 and ops[1].type == X86_OP_REG:
                    width = REG_W.get(ins.reg_name(ops[1].reg), 0)
                direction = "store"
            else:                              # 内存操作数在源位 -> load
                width = REG_W.get(ins.reg_name(ops[0].reg), 0) if ops[0].type == X86_OP_REG else 0
                direction = "load"
            if width == 0:
                width = op.size if op.size else 0
            # 对齐检查：4/8 字节字段必须自然对齐，否则几乎必然是内部字节访问（噪声）
            if width >= 4 and disp % width:
                continue
            out.append((disp, width, direction))
    return out, calls


# ---------------------------------------------------------------- 字段名推断
GETTER = re.compile(r"^(?:get|Get)_(\w+)$")
SETTER = re.compile(r"^(?:set|Set)_(\w+)$")


def guess_names(by_cls, cls):
    """从 get_X / set_X 建立 offset -> 字段名 候选"""
    res = {}
    for rva, m, ret, par in by_cls.get(cls, []):
        g = GETTER.match(m)
        s = SETTER.match(m)
        if g:
            res.setdefault(("get", g.group(1)), rva)
        elif s:
            res.setdefault(("set", s.group(1)), rva)
    return res


def analyze_class(pe, cls, by_cls, pdata, verbose=True):
    """
    字段名绑定策略（避免一个 getter 把名字撒到它路过的所有偏移上）：
      * getter  get_X：名字只落到「返回值来源」= 最后一条 load（唯一 load 时权重更高）
      * setter  set_X：名字只落到「最后一条 store」
      * 其它方法：只登记偏移存在，不给名字（score 0）
    同一偏移上累加 score，取最高分者为字段名。
    """
    methods = sorted(by_cls.get(cls, []))
    if not methods:
        return None

    fields = {}     # disp -> stats
    votes = collections.defaultdict(collections.Counter)   # disp -> {name: score}

    def bump(disp, width, direction, mname):
        e = fields.setdefault(disp, {"w": collections.Counter(),
                                     "loads": 0, "stores": 0, "methods": set()})
        if width:
            e["w"][width] += 1
        if direction == "load":
            e["loads"] += 1
        else:
            e["stores"] += 1
        e["methods"].add(mname)

    for rva, m, ret, par in methods:
        g = GETTER.match(m)
        s = SETTER.match(m)
        fname = g.group(1) if g else (s.group(1) if s else None)
        hits, _calls = scan_this_fields(pe, rva, pdata)
        if not hits:
            continue
        for disp, width, direction in hits:
            bump(disp, width, direction, m)
        if not fname:
            continue
        if g:
            loads = [h for h in hits if h[2] == "load"]
            if not loads:
                continue
            if len(loads) == 1:
                votes[loads[0][0]][fname] += 4       # 唯一 load，强证据
            else:
                votes[loads[-1][0]][fname] += 3      # 返回值来源
        elif s:
            stores = [h for h in hits if h[2] == "store"]
            if not stores:
                continue
            votes[stores[-1][0]][fname] += 4

    rows = []
    for disp in sorted(fields):
        e = fields[disp]
        w = e["w"].most_common(1)[0][0] if e["w"] else 0
        v = votes.get(disp, collections.Counter())
        names = [n for n, _ in v.most_common()]
        rows.append({
            "offset": disp,
            "width": w,
            "names": names,
            "name": names[0] if names else "",
            "loads": e["loads"],
            "stores": e["stores"],
            "nmethods": len(e["methods"]),
        })
    if verbose:
        print("=" * 78)
        print("%s   %d 个方法 → %d 个字段偏移" % (cls, len(methods), len(rows)))
        print("  %-8s %-4s %-30s %5s %5s %4s" % ("offset", "size", "name", "load", "store", "n"))
        for r in rows:
            nm = r["name"] or ("<未命名>")
            extra = (" | " + ",".join(r["names"][1:4])) if len(r["names"]) > 1 else ""
            print("  +%#-6x %-4d %-30s %5d %5d %4d%s"
                  % (r["offset"], r["width"], nm, r["loads"], r["stores"], r["nmethods"], extra))
    return rows


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("cls", nargs="*", help="类名（可多个）；或 --batch 文件")
    ap.add_argument("--grep", default="", help="只显示名字含该串的类（列出类名）")
    ap.add_argument("--limit", type=int, default=0)
    ap.add_argument("--batch", default="")
    ap.add_argument("--out", default="")
    ap.add_argument("--json", default="")
    a = ap.parse_args()

    if not os.path.exists(DLL):
        print("找不到 DLL:", DLL)
        return 1
    pe = PE(DLL)
    by_cls, allrows = load_symtab()
    pdata = load_pdata()
    print("DLL %.1f MB / symtab %d 条 / %d 个类 / pdata %d 个函数"
          % (len(pe.d) / 1048576.0, len(allrows), len(by_cls), len(pdata)))

    if a.grep:
        hits = [c for c in sorted(by_cls) if a.grep.lower() in c.lower()]
        print("\n匹配 %d 个类：" % len(hits))
        for c in hits[:a.limit or 80]:
            print("   %-40s %d 方法" % (c, len(by_cls[c])))
        return 0

    targets = list(a.cls)
    if a.batch:
        with open(a.batch, encoding="utf-8") as f:
            targets += [x.strip() for x in f if x.strip() and not x.startswith("#")]
    if not targets:
        print("请给类名，或用 --grep / --batch")
        return 1

    allres = {}
    for c in targets:
        rows = analyze_class(pe, c, by_cls, pdata, verbose=not a.out)
        if rows is None:
            print("!! 类不存在:", c)
            continue
        allres[c] = rows

    if a.out:
        with open(a.out, "w", encoding="utf-8") as f:
            f.write("className\toffset\twidth\tfieldName\taltNames\tloads\tstores\tnMethods\n")
            for c, rows in allres.items():
                for r in rows:
                    f.write("%s\t%d\t%d\t%s\t%s\t%d\t%d\t%d\n" % (
                        c, r["offset"], r["width"], r["name"],
                        ",".join(r["names"][1:]), r["loads"], r["stores"], r["nmethods"]))
        print("已写", a.out, sum(len(v) for v in allres.values()), "行")
    if a.json:
        json.dump(allres, open(a.json, "w", encoding="utf-8"), ensure_ascii=False, indent=1)
        print("已写", a.json)
    return 0


if __name__ == "__main__":
    sys.exit(main())
