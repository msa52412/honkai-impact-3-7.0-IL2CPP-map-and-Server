# -*- coding: utf-8 -*-
"""把 qt_watch.py 里的五段 lua 补丁转成 C 头文件。

为什么要有这一步：DLL 里要把这些代码内嵌成字符串常量，手工转义引号和换行
太容易错，而且 qt_watch.py 一改就得同步。直接从这里读常量、生成 qtpatch_lua.h，
DLL 只要 #include 就行。

用法：
    python inject_pkg/gen_qtpatch.py
"""
import io
import os
import re
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
ROOT = os.path.dirname(HERE)
SRC = os.path.join(ROOT, "_ext", "tools", "qt_watch.py")
OUT = os.path.join(HERE, "qtpatch_lua.h")

# 顺序必须和 qt_watch.py 里 apply_patch 的顺序一致：HL -> BOSS1 -> BOSS2 -> BOSS3 -> BOSS4
SEGMENTS = [
    ("QT_LUA_HL",    "PATCH_HL"),
    ("QT_LUA_BOSS1", "PATCH_BOSS1"),
    ("QT_LUA_BOSS2", "PATCH_BOSS2"),
    ("QT_LUA_BOSS3", "PATCH_BOSS3"),
    ("QT_LUA_BOSS4", "PATCH_BOSS4"),
]

# 场景闸门（2026-11-08 补的，很关键）
# ---------------------------------------------------------------------------
# python 版 qt_watch 靠 pcall 的返回值判断：PATCH_HL 返回 nordy（不是量子场景）
# 就整组收工。但 DLL 版拿不到 lua 返回值，只能把五段全都执行 —— 结果在别的玩法
# 的 lua_State 里也劫持了 LevelDesign 的元表。
#
# 需要放行的是两类场景：
#   * 量子奇点      —— _G.QuantumEndlessPlugin 存在
#   * 迪拉克之海    —— _G.load_OWEndlessData / newOWEndlessPlugin 存在
#     （迪拉克的脚本 NewOpenWorldEndlessLevelDesign.lua，和量子是两份，
#       但它同样调 SetOWEndlessLevelScoreMode / StartAutoChangeLevelScoreByTime，
#       而且参数一样是版本错配的 —— 不吞掉异常就会打死协程，
#       表现为计时器恒 0、分数不动、打完无法结算）
# 主线/其他关卡这两个都不存在，照旧挡住。
GATE = ("if _G.QuantumEndlessPlugin == nil and _G.load_OWEndlessData == nil "
        "then return 'nordy' end\n")

text = io.open(SRC, encoding="utf-8").read()


def grab(name):
    """取 NAME = \"\"\"...\"\"\" 或 '''...''' 的内容。"""
    m = re.search(r'(?ms)^%s\s*=\s*("""|\'\'\')(.*?)\1' % re.escape(name), text)
    if not m:
        raise SystemExit("没找到常量 %s（qt_watch.py 改过结构？）" % name)
    return m.group(2)


def c_escape(s):
    """转成 C 字符串字面量的内容部分。

    **按 UTF-8 字节走，不能按 Unicode 码点**：中文注释里一个「—」是 U+2014，
    直接写成 `\\x2014` 会被 C 当成 0x2014（超出 1 字节）报
    `hex escape sequence out of range`。逐字节输出才安全。
    """
    out = []
    for b in s.encode("utf-8"):
        if b == 0x5C:                 # backslash
            out.append("\\\\")
        elif b == 0x22:               # double quote
            out.append('\\"')
        elif b == 0x0A:
            out.append("\\n")
        elif b == 0x0D:
            out.append("\\r")
        elif b == 0x09:
            out.append("\\t")
        elif 32 <= b < 127:
            out.append(chr(b))
        else:
            out.append("\\x%02x" % b)
            out.append('""')          # 防止 \x 吃掉后面的十六进制字符
    return "".join(out)


lines = [
    "/* 由 inject_pkg/gen_qtpatch.py 从 _ext/tools/qt_watch.py 自动生成，不要手改。",
    " * 改 lua 补丁请改 qt_watch.py，然后重跑生成脚本 + 重新编译 DLL。",
    " * 五段顺序与 apply_patch 一致：ChangeHardLevel 包装 / 元表劫持 / 额外分 /",
    " * 结算拦截 / 补战斗得分。 */",
    "#ifndef QTPATCH_LUA_H",
    "#define QTPATCH_LUA_H",
    "",
]

names = []
for idx, (cname, pyname) in enumerate(SEGMENTS):
    body = grab(pyname)
    if idx > 0:                      # PATCH_HL 自己已经有这道判断
        body = GATE + body
    names.append(cname)
    lines.append("static const char %s[] =" % cname)
    # 一行一条源行，便于在 C 里对应回 lua 的行号。
    # 注意每条字面量末尾必须带 \n —— C 的相邻字符串是直接拼接的，
    # 少了换行会把 lua 的每一行粘成一行（`--` 注释会吃掉后面全部内容）。
    body_lines = body.split("\n")
    for idx, ln in enumerate(body_lines):
        tail = "\\n" if idx < len(body_lines) - 1 else ""
        lines.append('    "%s%s"' % (c_escape(ln), tail))
    lines[-1] += ";"
    lines.append("")

lines.append("static const char *const QT_PATCHES[] = {")
for n in names:
    lines.append("    %s," % n)
lines.append("};")
lines.append("#define QT_PATCH_COUNT %d" % len(names))
lines.append("")
lines.append("#endif /* QTPATCH_LUA_H */")
lines.append("")

io.open(OUT, "w", encoding="utf-8", newline="\n").write("\n".join(lines))
print("已生成 %s（%d 段，%d 字节源码）" % (OUT, len(names), os.path.getsize(OUT)))
for cname, pyname in SEGMENTS:
    print("  %-14s <- %-12s %d 字节" % (cname, pyname, len(grab(pyname))))
