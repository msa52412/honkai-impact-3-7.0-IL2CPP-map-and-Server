# -*- coding: utf-8 -*-
"""支持任意 EndlessType：新增 [entry] type / type0_patch。

- type = -1  按 mode 走（0 -> 2 迪拉克，1 -> 3 量子）
- type = 0   无限深渊 Infinity Abyss
- type = 4   超弦空间 / 5 量子流形 / 6 封锁地带 ……
- type0_patch = 1 时打 TypeToMode 补丁（0 -> 2），否则 type=0 会在 SetupView NRE
"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

# ---------- 1) ENTRY_CTX 加 status_type ----------
old = """typedef struct {
    LONG      card_id;      /* 目标卡片 ExcelMeta.ID（ini 可改） */
    LONG      mode;         /* 0 = 迪拉克之海，1 = 量子奇点 */
    LONG      hit_seq;      /* stub 命中一次 +1，worker 靠它触发 */
    LONG      last_id;      /* 最近一次点击的卡片 ID（调试用） */
    ULONG_PTR last_meta;    /* 最近一次点击的 ExcelMeta（调试用） */
} ENTRY_CTX;"""
new = """typedef struct {
    LONG      card_id;      /* 目标卡片 ExcelMeta.ID（ini 可改） */
    LONG      mode;         /* 0 = 迪拉克之海，1 = 量子奇点 */
    LONG      hit_seq;      /* stub 命中一次 +1，worker 靠它触发 */
    LONG      last_id;      /* 最近一次点击的卡片 ID（调试用） */
    ULONG_PTR last_meta;    /* 最近一次点击的 ExcelMeta（调试用） */
    LONG      status_type;  /* >=0 直接写进 _currentMode.endless_type；-1 = 按 mode 走 */
} ENTRY_CTX;"""
assert old in s
s = s.replace(old, new, 1)

# ---------- 2) 全局 ----------
s = s.replace("static volatile LONG g_reentering = 0;",
              "static volatile LONG g_reentering = 0;\n"
              "static int       g_type0_patch = 1;   /* 打 TypeToMode 补丁（type 0 -> 2） */", 1)

# ---------- 3) ini ----------
old_ini = """    g_entry.card_id = (LONG)GetPrivateProfileIntA("entry", "card_id", 11, ini);
    g_entry.mode    = (LONG)GetPrivateProfileIntA("entry", "mode", 0, ini);"""
new_ini = """    g_entry.card_id = (LONG)GetPrivateProfileIntA("entry", "card_id", 11, ini);
    g_entry.mode    = (LONG)GetPrivateProfileIntA("entry", "mode", 0, ini);
    g_entry.status_type  = (LONG)GetPrivateProfileIntA("entry", "type", -1, ini);
    g_type0_patch        = (int)GetPrivateProfileIntA("entry", "type0_patch", 1, ini);"""
assert old_ini in s
s = s.replace(old_ini, new_ini, 1)

# ---------- 4) 点击逻辑：任意 type ----------
old_click = """    if (g_entry.mode == 1) {
        ULONG_PTR two = *(ULONG_PTR *)((BYTE *)mod + OFF_TWO_ENDLESS);
        if (two) {
            ULONG_PTR cur = *(ULONG_PTR *)((BYTE *)two + OFF_CUR_MODE);
            if (cur) {
                LONG old = *(LONG *)((BYTE *)cur + OFF_STATUS_TYPE);
                *(LONG *)((BYTE *)cur + OFF_STATUS_TYPE) = 3;
                logf("[entry] quantum  _currentMode=%p  endless_type %ld -> 3",
                     (void *)cur, old);
            } else {
                logf("[entry] !! _currentMode 为空，按原类型进入");
            }
        } else {
            logf("[entry] !! TwoEndless 为空，按原类型进入");
        }
    }"""
new_click = """    {
        LONG want = (g_entry.status_type >= 0)
                  ? g_entry.status_type
                  : (g_entry.mode == 1 ? 3 : 2);
        ULONG_PTR two = *(ULONG_PTR *)((BYTE *)mod + OFF_TWO_ENDLESS);
        if (two) {
            ULONG_PTR cur = *(ULONG_PTR *)((BYTE *)two + OFF_CUR_MODE);
            if (cur) {
                static const char *nm[] = {
                    "无限深渊", "Normal", "迪拉克之海", "量子奇点",
                    "超弦空间", "量子流形", "封锁地带"
                };
                const char *n = (want >= 0 && want <= 6) ? nm[want] : "?";
                LONG old = *(LONG *)((BYTE *)cur + OFF_STATUS_TYPE);
                *(LONG *)((BYTE *)cur + OFF_STATUS_TYPE) = want;
                logf("[entry] _currentMode=%p  endless_type %ld -> %ld (%s)",
                     (void *)cur, old, want, n);
            } else {
                logf("[entry] !! _currentMode 为空，按原类型进入");
            }
        } else {
            logf("[entry] !! TwoEndless 为空，按原类型进入");
        }
    }"""
assert old_click in s
s = s.replace(old_click, new_click, 1)

# ---------- 5) TypeToMode 补丁函数 ----------
anchor = "/* 在 anchor ±2GB 内找一页可执行内存（E9 rel32 只能跳 ±2GB） */"
assert anchor in s
patch_fn = r'''/* ---------------- TypeToMode 补丁（无限深渊 type 0 必需） ------------------
 * OWEndlessModule.TypeToMode(type) 原实现：2..6 原样、0/1 折成 0。
 * 客户端没有 Mode=0 的 base data，进 type 0 会在 SetupView 里 NRE（黑屏转圈）。
 * 换成：2..6 原样、1 保持 0、0 -> 2（复用迪拉克那套 OW 界面与表）。
 * 18 字节整段替换（不依赖原指令边界），原函数首部是 ifix 检查，整段跳过即可。
 * 这个函数每帧都在跑，写入前必须挂起其它线程。
 */
#define RVA_TYPETOMODE  0x019C6C90
#define LEN_TYPETOMODE  18
static const BYTE ORIG_TYPETOMODE[LEN_TYPETOMODE] = {
    0x48, 0x89, 0x5C, 0x24, 0x08, 0x57, 0x48, 0x83, 0xEC,
    0x20, 0x48, 0x8B, 0xF9, 0x8B, 0xDA, 0xB9, 0xA8, 0x08
};
static const BYTE PATCH_TYPETOMODE[LEN_TYPETOMODE] = {
    0x8B, 0xC2,                                  /* mov eax, edx          */
    0x83, 0xF8, 0x02,                            /* cmp eax, 2            */
    0x73, 0x0A,                                  /* jae  +0x0A -> ret     */
    0x83, 0xF8, 0x01,                            /* cmp eax, 1            */
    0x74, 0x05,                                  /* je   +0x05 -> ret     */
    0xB8, 0x02, 0x00, 0x00, 0x00,                /* mov eax, 2            */
    0xC3                                         /* ret                   */
};

static void apply_typetomode(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_TYPETOMODE;
    HANDLE th[MAX_SUSPEND];
    int    n, ok;
    char   hs[256];

    hexdump(fn, LEN_TYPETOMODE, hs, sizeof(hs));
    logf("[typetomode] %p 当前: %s", fn, hs);

    if (memcmp(fn, PATCH_TYPETOMODE, LEN_TYPETOMODE) == 0) {
        logf("[typetomode] 已经是补丁状态");
        return;
    }
    if (!is_original(fn, ORIG_TYPETOMODE, LEN_TYPETOMODE)) {
        logf("[typetomode] !! 原始字节不匹配，跳过（type 0 会黑屏）");
        return;
    }
    n = suspend_other_threads(th, MAX_SUSPEND);
    ok = write_code(fn, PATCH_TYPETOMODE, LEN_TYPETOMODE);
    resume_other_threads(th, n);
    if (ok && memcmp(fn, PATCH_TYPETOMODE, LEN_TYPETOMODE) == 0)
        logf("[typetomode] OK  type 0 -> mode 2（挂起 %d 个线程写入）", n);
    else
        logf("[typetomode] !! 写入失败 err=%lu", GetLastError());
}

'''
s = s.replace(anchor, patch_fn + anchor, 1)

# ---------- 6) 在 worker 里调用 ----------
old_call = "        apply_entry(ua);"
if old_call not in s:
    # 找实际调用名
    import re
    m = re.search(r"\n(\s+)apply_entry\(ua\);", s)
    old_call = m.group(0).strip() if m else None
assert old_call and "apply_entry(ua);" in s
s = s.replace("apply_entry(ua);",
              'if (g_type0_patch) {\n            apply_typetomode(ua);\n        } else {\n            logf("[typetomode] 未启用（ini [entry] type0_patch=0），type 0 会黑屏");\n        }\n        apply_entry(ua);', 1)

io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('ok, lines =', s.count('\n'))
