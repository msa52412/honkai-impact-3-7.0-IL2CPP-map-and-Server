# -*- coding: utf-8 -*-
"""
callers.py — 方法级交叉引用（离线）。

补的两个缺口：
  * who <目标>    谁调用了它（call 指令反查；靠「从函数起点线性解码能否精确到达 call 点」过滤 E8 误报）
  * calls <目标>  它调用了谁（按 pdata 边界解码本函数，逐条 call 标注符号）

用法：
  python _analysis/callers.py who LevelActor::HasLevelScorePlugin
  python _analysis/callers.py who LevelDesignManager::SetOWEndlessLevelScoreMode --depth 2
  python _analysis/callers.py calls InLevelMainPageContext::SetOWEndlessLevelScoreVisible
  python _analysis/callers.py calls 0x2262070
  python _analysis/callers.py find LevelScore
"""
import os as _os, sys as _sys
_sys.path.insert(0, _os.path.dirname(_os.path.abspath(__file__)))
try:
    import _paths
except ImportError:
    _paths = None

import os
import sys
import bisect
import argparse
import collections

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import field_layout as F
import callsite as CS
from capstone import Cs, CS_ARCH_X86, CS_MODE_64
from capstone.x86 import X86_OP_IMM

MD = Cs(CS_ARCH_X86, CS_MODE_64)
MD.detail = True


class Ctx(object):
    def __init__(self):
        self.pe = F.PE(F.DLL)
        self.by_cls, self.rows = F.load_symtab()
        self.pdata = F.load_pdata()
        self.pds = sorted(self.pdata.items())
        self.starts = [s for s, _ in self.pds]
        self.name_of = {}
        for rva, ns, cls, meth, ret, par, mod in self.rows:
            self.name_of.setdefault(rva, (cls, meth, par, ret))

    def resolve(self, rva):
        i = bisect.bisect_right(self.starts, rva) - 1
        if i >= 0:
            s, e = self.pds[i]
            if s <= rva < e:
                return s, e
        return None, None

    def label(self, rva):
        hit = self.name_of.get(rva)
        return "%s::%s" % (hit[0], hit[1]) if hit else None

    def find(self, spec):
        if spec.startswith("0x"):
            v = int(spec, 16)
            rva = v - F.BASE if v >= F.BASE else v
            hit = self.name_of.get(rva)
            return [(rva,) + hit] if hit else [(rva, "?", "?", "?", "?")]
        if "::" in spec:
            cls, meth = spec.split("::", 1)
            return [(rva, cls, m, par, ret)
                    for rva, m, ret, par in self.by_cls.get(cls, [])
                    if m == meth or m == "_" + meth]
        low = spec.lower()
        return [(rva, cls, meth, par, ret)
                for rva, ns, cls, meth, ret, par, mod in self.rows
                if low in meth.lower()]


def exact_reach(ctx, fs, site):
    """能否从函数起点精确解码到 site（过滤数据里的 E8 误报）"""
    o = ctx.pe.off(fs)
    if o is None or site <= fs:
        return False
    code = ctx.pe.d[o:o + (site - fs + 5)]
    for ins in MD.disasm(code, F.BASE + fs):
        a = ins.address - F.BASE
        if a == site:
            return True
        if a > site:
            return False
    return False


def who(ctx, rva, depth=1, seen=None, indent=0, tag=""):
    seen = seen or set()
    if rva in seen:
        return
    seen.add(rva)
    sites = CS.sites_to(rva, ctx.pe)
    callers = collections.Counter()
    for s in sites:
        fs, _fe = ctx.resolve(int(s))
        if fs is None:
            continue
        if not exact_reach(ctx, fs, int(s)):
            continue
        callers[fs] += 1
    if not callers:
        print("  " * indent + "  (无静态调用者：可能虚调用 / 委托 / TVM 保护)")
        return
    for fs, n in callers.most_common():
        lab = ctx.label(fs) or ("sub_%X" % fs)
        print("  " * indent + "  <- %s  @%#x%s" % (lab, fs, "  x%d" % n if n > 1 else ""))
        if depth > 1:
            who(ctx, fs, depth - 1, seen, indent + 1)


def calls_of(ctx, rva):
    fs, fe = ctx.resolve(rva)
    if fs is None:
        print("RVA 不在任何已知函数内")
        return
    o = ctx.pe.off(fs)
    code = ctx.pe.d[o:o + (fe - fs)]
    print("函数 %s  @%#x  长度 %d 字节" % (ctx.label(fs) or "sub_%X" % fs, fs, fe - fs))
    print()
    n = 0
    for ins in MD.disasm(code, F.BASE + fs):
        if ins.mnemonic != "call":
            continue
        ops = ins.operands
        if not ops or ops[0].type != X86_OP_IMM:
            print("  %#010x  call  (间接)" % (ins.address - F.BASE))
            continue
        tgt = ops[0].imm - F.BASE
        n += 1
        print("  %#010x  call  %s" % (ins.address - F.BASE, ctx.label(tgt) or ("sub_%X" % tgt)))
    if n == 0:
        print("  (无直接 call)")


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("cmd", choices=["who", "calls", "find"])
    ap.add_argument("spec")
    ap.add_argument("--depth", type=int, default=1)
    a = ap.parse_args()

    ctx = Ctx()
    print("symtab %d 类 / %d 方法 / pdata %d 函数\n" % (len(ctx.by_cls), len(ctx.rows), len(ctx.pdata)))

    hits = ctx.find(a.spec)
    if not hits:
        print("没找到:", a.spec)
        return 1

    if a.cmd == "find":
        for rva, cls, meth, par, ret in hits[:60]:
            print("  %#010x  %s::%s(%s) -> %s" % (rva, cls, meth, par, ret))
        return 0

    if len(hits) > 1:
        print("匹配 %d 个重载：" % len(hits))
        for rva, cls, meth, par, ret in hits[:6]:
            print("   %#010x  %s::%s(%s)" % (rva, cls, meth, par))
        print()

    for rva, cls, meth, par, ret in hits[:1]:
        if a.cmd == "who":
            print("谁调用 %s::%s(%s)  @%#x" % (cls, meth, par, rva))
            who(ctx, rva, a.depth)
        else:
            calls_of(ctx, rva)
    return 0


if __name__ == "__main__":
    sys.exit(main())
