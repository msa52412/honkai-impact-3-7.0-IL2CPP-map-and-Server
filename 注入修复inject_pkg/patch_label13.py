# -*- coding: utf-8 -*-
"""把 hide 段从「字符串 Find」改成「GetChild(index)」。
根因：m_Text 指向自建缓冲区后，从它抄来的 klass 会变成垃圾值
（0x7ffaed4d6724bb49，低字节正是 movabs 的 49 BB），
导致 Transform::Find 永远失败。改用索引取第 3 个子节点。
align 段删掉（居中已由 Text::OnEnable 那个 hook 完成）。
"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

# ---------- 1) 新增 g_hide_idx ----------
if 'g_hide_idx' not in s:
    s = s.replace("static int   g_hide_wlen = 0;",
                  "static int   g_hide_wlen = 0;\n"
                  "static int   g_hide_idx = 2;   /* Progress 下第几个子节点是 Num（-1 = 不处理） */", 1)

# ---------- 2) ini 解析 ----------
old_ini = """        {
            char hn[40];
            int k;
            hn[0] = 0;
            GetPrivateProfileStringA("label", "hide", "", hn, sizeof(hn), ini);
            for (k = 0; hn[k] && k < 39; k++) g_hide_w[k] = (WCHAR)hn[k];
            g_hide_w[k] = 0;
            g_hide_wlen = k;
        }"""
new_ini = """        {
            char hn[40];
            int k;
            hn[0] = 0;
            GetPrivateProfileStringA("label", "hide", "", hn, sizeof(hn), ini);
            for (k = 0; hn[k] && k < 39; k++) g_hide_w[k] = (WCHAR)hn[k];
            g_hide_w[k] = 0;
            g_hide_wlen = k;
        }
        /* 不再按名字 Find（klass 不可靠），改成按索引取子节点。
         * Progress: [0]=Text [1]=Blank [2]=Num [3]=Splash [4]=Num_2 */
        g_hide_idx = (int)GetPrivateProfileIntA("label", "hide_index", 2, ini);"""
assert old_ini in s
s = s.replace(old_ini, new_ini, 1)

# ---------- 3) 重写一次性段 ----------
START = "    /* ---- 一次性：隐藏兄弟节点"
i0 = s.find(START)
assert i0 >= 0, 'start not found'
TAIL = "        *p++ = 0x59;                                          /* pop rcx */\n    }\n"
i1 = s.find(TAIL, i0)
assert i1 >= 0, 'tail not found'
i1 += len(TAIL)

block = r'''    /* ---- 每帧确保：关掉右边的 "1/6"（兄弟节点 Num） ---------------------
     * 为什么不用字符串 Find：Text 的 m_Text 被本 stub 换成了自建缓冲区后，
     * 下一帧从这里抄到的 klass 会变成垃圾值（实测 0x7ffaed4d6724bb49，
     * 低字节正是 movabs 的 49 BB），构造出的 "Num" 字符串无效，
     * Transform::Find 全部返回 0 —— 这就是 1/6 一直关不掉的真因。
     * 改用 GetChild(index)：Progress 下 [0]=Text [1]=Blank [2]=Num，
     * 完全不需要字符串，也不依赖任何类指针。
     * busy 防止 SetActive 触发布局重算造成的重入。
     */
    if (g_label_ua && g_hide_idx >= 0) {
        int   epi_end[4], n_end = 0;
        int   epi_fin[8], n_fin = 0;
        int   at_final, at_end;
        LONG  idx  = g_hide_idx;
        LONG  need = idx + 1;
        ULONGLONG pars = (ULONGLONG)(ULONG_PTR)&g_label_par;

        *p++ = 0x51;                                          /* push rcx */
        *p++ = 0x52;                                          /* push rdx */
        *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x28;   /* sub rsp,0x28 */

        /* if (busy) goto END */
        p = emit_mov_rax(p, &g_label_lg_busy);
        *p++ = 0x83; *p++ = 0x38; *p++ = 0x00;
        *p++ = 0x74; *p++ = 0x0D;
        p = emit_mov_r11(p, NULL); epi_end[n_end++] = (int)(p - out) - 8;
        *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE3;

        /* busy = 1 */
        p = emit_mov_rax(p, &g_label_lg_busy);
        *p++ = 0xC7; *p++ = 0x00;
        { LONG one = 1; memcpy(p, &one, 4); p += 4; }

        /* if (childCount < need) goto FINAL */
        p = emit_mov_rax(p, (const void *)(ULONG_PTR)pars);
        *p++ = 0x48; *p++ = 0x8B; *p++ = 0x08;                /* mov rcx,[rax] */
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06917F80));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x3D; memcpy(p, &need, 4); p += 4;             /* cmp eax, need */
        *p++ = 0x7C; *p++ = 0x0D;                             /* jl +13 */
        p = emit_mov_r11(p, NULL); epi_fin[n_fin++] = (int)(p - out) - 8;
        *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE3;

        /* ch = GetChild(par, idx) */
        p = emit_mov_rax(p, (const void *)(ULONG_PTR)pars);
        *p++ = 0x48; *p++ = 0x8B; *p++ = 0x08;
        *p++ = 0xBA; memcpy(p, &idx, 4); p += 4;              /* mov edx, idx */
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06916EB0));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        *p++ = 0x75; *p++ = 0x0D;                             /* jne +13 */
        p = emit_mov_r11(p, NULL); epi_fin[n_fin++] = (int)(p - out) - 8;
        *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE3;

        /* go = get_gameObject(ch) */
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1130));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        *p++ = 0x75; *p++ = 0x0D;
        p = emit_mov_r11(p, NULL); epi_fin[n_fin++] = (int)(p - out) - 8;
        *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE3;

        /* SetActive(go, false) */
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;
        *p++ = 0x31; *p++ = 0xD2;
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06A85CB0));
        *p++ = 0xFF; *p++ = 0xD0;

        /* FINAL: busy = 0 */
        at_final = (int)(p - out);
        p = emit_mov_rax(p, &g_label_lg_busy);
        *p++ = 0xC7; *p++ = 0x00;
        { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }

        at_end = (int)(p - out);
        {
            int k;
            for (k = 0; k < n_end; k++)
                *(ULONGLONG *)(out + epi_end[k]) = (ULONGLONG)(ULONG_PTR)(out + at_end);
            for (k = 0; k < n_fin; k++)
                *(ULONGLONG *)(out + epi_fin[k]) = (ULONGLONG)(ULONG_PTR)(out + at_final);
        }
        *p++ = 0x48; *p++ = 0x83; *p++ = 0xC4; *p++ = 0x28;   /* add rsp,0x28 */
        *p++ = 0x5A;                                          /* pop rdx */
        *p++ = 0x59;                                          /* pop rcx */
    }

'''

s = s[:i0] + block + s[i1:]

# 日志里带上 hide_index
s = s.replace('logf("[label] enabled=%d  align=%d nowrap=%d  (from %d 字 -> to %d 字)",\n             g_label_enabled, g_label_align, g_label_nowrap,\n             g_label_fromlen, g_label_tolen);',
              'logf("[label] enabled=%d  align=%d nowrap=%d  hide_index=%d  (from %d 字 -> to %d 字)",\n             g_label_enabled, g_label_align, g_label_nowrap, g_hide_idx,\n             g_label_fromlen, g_label_tolen);', 1)

io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('rewritten, lines =', s.count('\n'))
