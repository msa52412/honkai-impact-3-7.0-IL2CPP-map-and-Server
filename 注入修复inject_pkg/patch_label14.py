# -*- coding: utf-8 -*-
"""在 hide 段各处加计数，定位卡在哪一步：
enter / cnt_fail / ch_fail / go_fail / sa
"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

if 'g_lgdbg_enter' not in s:
    s = s.replace("static volatile LONG g_label_lg_busy = 0;",
                  "static volatile LONG g_label_lg_busy = 0;\n"
                  "static volatile LONG g_lgdbg_enter = 0;\n"
                  "static volatile LONG g_lgdbg_cntfail = 0;\n"
                  "static volatile LONG g_lgdbg_chfail = 0;\n"
                  "static volatile LONG g_lgdbg_gofail = 0;\n"
                  "static volatile LONG g_lgdbg_sa = 0;", 1)

# 1) 段入口计数（push 之后）
old = """        *p++ = 0x51;                                          /* push rcx */
        *p++ = 0x52;                                          /* push rdx */
        *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x28;   /* sub rsp,0x28 */

        /* if (busy) goto END */"""
new = """        *p++ = 0x51;                                          /* push rcx */
        *p++ = 0x52;                                          /* push rdx */
        *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x28;   /* sub rsp,0x28 */

        p = emit_mov_r11(p, &g_lgdbg_enter);
        *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;

        /* if (busy) goto END */"""
assert old in s
s = s.replace(old, new, 1)

# 2) childCount 失败分支计数
old = """        *p++ = 0x3D; memcpy(p, &need, 4); p += 4;             /* cmp eax, need */
        *p++ = 0x7D; *p++ = 0x0D;                             /* jge +13 -> 继续 */
        p = emit_mov_r11(p, NULL); epi_fin[n_fin++] = (int)(p - out) - 8;"""
new = """        *p++ = 0x3D; memcpy(p, &need, 4); p += 4;             /* cmp eax, need */
        *p++ = 0x7D; *p++ = 0x0D;                             /* jge +13 -> 继续 */
        p = emit_mov_r11(p, &g_lgdbg_cntfail);
        *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;
        p = emit_mov_r11(p, NULL); epi_fin[n_fin++] = (int)(p - out) - 8;"""
assert old in s
s = s.replace(old, new, 1)

# 3) GetChild 失败分支计数
old = """        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        *p++ = 0x75; *p++ = 0x0D;                             /* jne +13 */
        p = emit_mov_r11(p, NULL); epi_fin[n_fin++] = (int)(p - out) - 8;"""
new = """        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        *p++ = 0x75; *p++ = 0x0D;                             /* jne +13 */
        p = emit_mov_r11(p, &g_lgdbg_chfail);
        *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;
        p = emit_mov_r11(p, NULL); epi_fin[n_fin++] = (int)(p - out) - 8;"""
assert old in s
s = s.replace(old, new, 1)

# 4) get_gameObject 失败分支计数（剩下的最后一个 jne 0x0D）
old = """        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1130));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        *p++ = 0x75; *p++ = 0x0D;
        p = emit_mov_r11(p, NULL); epi_fin[n_fin++] = (int)(p - out) - 8;"""
new = """        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1130));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        *p++ = 0x75; *p++ = 0x0D;
        p = emit_mov_r11(p, &g_lgdbg_gofail);
        *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;
        p = emit_mov_r11(p, NULL); epi_fin[n_fin++] = (int)(p - out) - 8;"""
assert old in s
s = s.replace(old, new, 1)

# 5) SetActive 之后计数
old = """        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06A85CB0));
        *p++ = 0xFF; *p++ = 0xD0;

        /* FINAL: busy = 0 */"""
new = """        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06A85CB0));
        *p++ = 0xFF; *p++ = 0xD0;

        p = emit_mov_r11(p, &g_lgdbg_sa);
        *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;

        /* FINAL: busy = 0 */"""
assert old in s
s = s.replace(old, new, 1)

# 6) watch 线程打印
old = """        now = InterlockedCompareExchange(&g_label_hits, 0, 0);
        if (now != last) {
            last = now;
            logf("[label] 已替换 %ld 次标签文字", last);
        }"""
new = """        now = InterlockedCompareExchange(&g_label_hits, 0, 0);
        if (now != last) {
            last = now;
            logf("[label] 已替换 %ld 次标签文字", last);
            logf("[label][dbg] enter=%ld cntfail=%ld chfail=%ld gofail=%ld setactive=%ld",
                 InterlockedCompareExchange(&g_lgdbg_enter, 0, 0),
                 InterlockedCompareExchange(&g_lgdbg_cntfail, 0, 0),
                 InterlockedCompareExchange(&g_lgdbg_chfail, 0, 0),
                 InterlockedCompareExchange(&g_lgdbg_gofail, 0, 0),
                 InterlockedCompareExchange(&g_lgdbg_sa, 0, 0));
        }"""
assert old in s
s = s.replace(old, new, 1)

io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('debug counters added')
