# -*- coding: utf-8 -*-
"""工具包里所有脚本共用的路径解析 —— 一律现算，不写死。

三件东西：
    DATA  这个包里的 data/（symtab / layout / cg_*.tsv / addr2name.json）
    GAME  游戏目录（BH3.exe 所在的那一级）—— 只在需要读 UserAssembly.dll 时用
    DLL   GAME/BH3_Data/Native/UserAssembly.dll

为什么要现算：目录随时会改名（app -> 7.0 就发生过），工具包也可能被整个搬到
别的地方。任何绝对路径写死一次，下次就失效。
"""
import os

HERE = os.path.dirname(os.path.abspath(__file__))
PKG = os.path.dirname(HERE)          # 工具包根


def _first_dir_with(name, start, depth=4):
    """从 start 往上找（以及它的直接子目录），返回第一个含 name 的目录。"""
    d = start
    for _ in range(depth):
        if os.path.isfile(os.path.join(d, name)):
            return d
        try:
            for sub in os.listdir(d):
                p = os.path.join(d, sub)
                if os.path.isdir(p) and os.path.isfile(os.path.join(p, name)):
                    return p
        except OSError:
            pass
        parent = os.path.dirname(d)
        if parent == d:
            break
        d = parent
    return None


def data_dir():
    for cand in (os.path.join(PKG, "data"), os.path.join(HERE, "data"), HERE):
        if os.path.isfile(os.path.join(cand, "bh3_symtab.tsv")):
            return cand
    return os.path.join(PKG, "data")


def game_dir():
    """游戏目录：本包所在目录往上找 BH3.exe；找不到就退回包根（调用方自己判空）。"""
    hit = _first_dir_with("BH3.exe", PKG)
    return hit or ""


DATA = data_dir()
GAME = game_dir()
DLL = os.path.join(GAME, "BH3_Data", "Native", "UserAssembly.dll") if GAME else ""
SYMTAB = os.path.join(DATA, "bh3_symtab.tsv")
LAYOUT = os.path.join(DATA, "bh3_layout_resolved.tsv")
CG_FUNCS = os.path.join(DATA, "bh3_cg_funcs.tsv")
CG_EDGES = os.path.join(DATA, "bh3_cg_edges.tsv")
PDATA = os.path.join(DATA, "bh3_pdata_truth.tsv")
ADDR2NAME = os.path.join(DATA, "addr2name.json")


if __name__ == "__main__":
    print("PKG  =", PKG)
    print("DATA =", DATA)
    print("GAME =", GAME or "(没找到 BH3.exe)")
    print("DLL  =", DLL, os.path.isfile(DLL) if DLL else "")
    for k, v in (("symtab", SYMTAB), ("layout", LAYOUT), ("cg_funcs", CG_FUNCS),
                 ("cg_edges", CG_EDGES), ("addr2name", ADDR2NAME)):
        print("  %-10s %s" % (k, "OK" if os.path.isfile(v) else "缺失"))
