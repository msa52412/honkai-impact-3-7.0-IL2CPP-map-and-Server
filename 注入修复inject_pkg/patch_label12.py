# -*- coding: utf-8 -*-
"""重写 stub 里的一次性段：
- done 只在 SetActive 成功之后才置位（之前是进段就置位，导致首次 Find 失败后永不重试）
- 增加 busy 标记，防止 SetActive 触发布局重算 -> 重入 -> 递归
- 失败的路径清 busy，下次 OnPopulateMesh 再试
"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

# 1) busy 全局
if 'g_label_lg_busy' not in s:
    s = s.replace(
        "static volatile LONG g_label_lg_done = 0;",
        "static volatile LONG g_label_lg_busy = 0;\n"
        "static volatile LONG g_label_lg_done = 0;", 1)

START = "    /* ---- 一次性：隐藏兄弟节点"
i0 = s.find(START)
assert i0 >= 0, 'start not found'
TAIL = "        *p++ = 0x59;                                          /* pop rcx */\n    }\n"
i1 = s.find(TAIL, i0)
assert i1 >= 0, 'tail not found'
i1 += len(TAIL)

block = r'''    /* ---- 一次性：隐藏兄弟节点 + 父布局贴左 -------------------------------
     * 放在 OnPopulateMesh 命中段：Text::OnEnable 早在打补丁之前就跑完了。
     * 关键点：done **只在 SetActive 成功之后**才置位 —— 首次命中时
     * 面板可能还没把 Num 子节点建出来，Find 返回 0；若那时就置 done，
     * 后面永远不会再试。失败的路径清 busy 并保持 done=0，下一帧再试。
     * busy 用来挡住 SetActive 触发布局重算造成的重入。
     * rcx(Text) / rdx(VertexHelper) 全程保护。
     */
    if (g_label_ua && (g_hide_wlen > 0 || g_group_align >= 0)) {
        int   epi_end[8], n_end = 0;
        int   epi_fin[8], n_fin = 0;
        int   at_nohide, at_noalign, at_final, at_end;
        int   j_nc = 0, j_ng = 0, j_ng2 = 0, j_nc2 = 0;
        LONG  hlen2   = (g_hide_wlen > 0) ? g_hide_wlen : 0;
        LONG  hbytes2 = hlen2 * 2;
        LONG  llen2   = g_lg_wlen;
        LONG  lbytes2 = llen2 * 2;
        LONG  ga2     = g_group_align;
        ULONGLONG hidew2 = (ULONGLONG)(ULONG_PTR)g_hide_w;
        ULONGLONG lgw2   = (ULONGLONG)(ULONG_PTR)g_lg_w;
        ULONGLONG hideb  = (ULONGLONG)(ULONG_PTR)g_hide_buf;
        ULONGLONG lgb    = (ULONGLONG)(ULONG_PTR)g_lg_buf;
        ULONGLONG pars   = (ULONGLONG)(ULONG_PTR)&g_label_par;

        *p++ = 0x51;                                          /* push rcx */
        *p++ = 0x52;                                          /* push rdx */
        *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x28;   /* sub rsp,0x28 */

        /* if (done) goto END */
        p = emit_mov_rax(p, &g_label_lg_done);
        *p++ = 0x83; *p++ = 0x38; *p++ = 0x00;                /* cmp dword [rax],0 */
        *p++ = 0x74; *p++ = 0x0D;                             /* je +13 -> 继续 */
        p = emit_mov_r11(p, NULL); epi_end[n_end++] = (int)(p - out) - 8;
        *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE3;

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

        /* flag = 0 -> [rsp+0x20] */
        *p++ = 0xC7; *p++ = 0x44; *p++ = 0x24; *p++ = 0x20;
        { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }

        /* tr = get_transform(Text) ；失败 -> FINAL */
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1170));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        *p++ = 0x75; *p++ = 0x0D;                             /* jne +13 -> 继续 */
        p = emit_mov_r11(p, NULL); epi_fin[n_fin++] = (int)(p - out) - 8;
        *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE3;

        /* par = get_parent(tr) ；失败 -> FINAL */
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;                /* mov rcx,rax */
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06918270));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        *p++ = 0x75; *p++ = 0x0D;
        p = emit_mov_r11(p, NULL); epi_fin[n_fin++] = (int)(p - out) - 8;
        *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE3;
        p = emit_mov_r11(p, &g_label_par);
        *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;                /* mov [r11],rax */

        /* ---------- hide：par.Find(hide) -> SetActive(false) ---------- */
        if (hlen2 > 0) {
            at_nohide = (int)(p - out);   /* 占位，稍后回填到真正位置 */

            p = emit_mov_r11(p, (const void *)(ULONG_PTR)hideb);
            p = emit_mov_rax(p, &g_label_buf);
            *p++ = 0x48; *p++ = 0x8B; *p++ = 0x00;            /* mov rax,[rax] klass */
            *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;            /* mov [r11],rax */
            *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;
            { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
            *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;
            memcpy(p, &hlen2, 4); p += 4;
            *p++ = 0x49; *p++ = 0xB8; memcpy(p, &hidew2, 8); p += 8;
            *p++ = 0x41; *p++ = 0xBA; memcpy(p, &hbytes2, 4); p += 4;
            {
                int at_lo = (int)(p - out);
                *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;
                *p++ = 0x45; *p++ = 0x88; *p++ = 0x4B; *p++ = 0x14;
                *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
                *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;
                *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
                { int j = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
                  out[j + 1] = (BYTE)(at_lo - (j + 2)); }
            }
            /* child = par.Find(hidebuf) */
            p = emit_mov_rax(p, (const void *)(ULONG_PTR)pars);
            *p++ = 0x48; *p++ = 0x8B; *p++ = 0x08;
            *p++ = 0x48; *p++ = 0xBA; memcpy(p, &hideb, 8); p += 8;
            p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06916E90));
            *p++ = 0xFF; *p++ = 0xD0;
            *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
            j_nc = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
            /* go = get_gameObject(child) */
            *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;
            p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1130));
            *p++ = 0xFF; *p++ = 0xD0;
            *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
            j_ng = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
            /* SetActive(go, false) */
            *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;
            *p++ = 0x31; *p++ = 0xD2;
            p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06A85CB0));
            *p++ = 0xFF; *p++ = 0xD0;
            /* flag = 1 */
            *p++ = 0xC7; *p++ = 0x44; *p++ = 0x24; *p++ = 0x20;
            { LONG one = 1; memcpy(p, &one, 4); p += 4; }
            at_nohide = (int)(p - out);
            out[j_nc + 1] = (BYTE)(at_nohide - (j_nc + 2));
            out[j_ng + 1] = (BYTE)(at_nohide - (j_ng + 2));
        }

        /* ---------- align：父节点 HorizontalLayoutGroup -> 贴左 ---------- */
        at_noalign = (int)(p - out);
        if (llen2 > 0 && ga2 >= 0) {
            p = emit_mov_r11(p, (const void *)(ULONG_PTR)lgb);
            p = emit_mov_rax(p, &g_label_buf);
            *p++ = 0x48; *p++ = 0x8B; *p++ = 0x00;
            *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;
            *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;
            { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
            *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;
            memcpy(p, &llen2, 4); p += 4;
            *p++ = 0x49; *p++ = 0xB8; memcpy(p, &lgw2, 8); p += 8;
            *p++ = 0x41; *p++ = 0xBA; memcpy(p, &lbytes2, 4); p += 4;
            {
                int at_lo = (int)(p - out);
                *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;
                *p++ = 0x45; *p++ = 0x88; *p++ = 0x4B; *p++ = 0x14;
                *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
                *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;
                *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
                { int j = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
                  out[j + 1] = (BYTE)(at_lo - (j + 2)); }
            }
            /* go = get_gameObject(par) */
            p = emit_mov_rax(p, (const void *)(ULONG_PTR)pars);
            *p++ = 0x48; *p++ = 0x8B; *p++ = 0x08;
            p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1130));
            *p++ = 0xFF; *p++ = 0xD0;
            *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
            j_ng2 = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
            /* lg = GetComponent(go, "HorizontalLayoutGroup") */
            *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;
            *p++ = 0x48; *p++ = 0xBA; memcpy(p, &lgb, 8); p += 8;
            p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06A859C0));
            *p++ = 0xFF; *p++ = 0xD0;
            *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
            j_nc2 = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
            *p++ = 0xC7; *p++ = 0x40; *p++ = 0x20;
            memcpy(p, &ga2, 4); p += 4;
            at_noalign = (int)(p - out);
            out[j_ng2 + 1] = (BYTE)(at_noalign - (j_ng2 + 2));
            out[j_nc2 + 1] = (BYTE)(at_noalign - (j_nc2 + 2));
        }
        (void)at_nohide;

        /* ---------- 收尾：done = flag，busy = 0 ---------- */
        at_final = (int)(p - out);
        *p++ = 0x8B; *p++ = 0x44; *p++ = 0x24; *p++ = 0x20;   /* mov eax,[rsp+0x20] */
        p = emit_mov_r11(p, &g_label_lg_done);
        *p++ = 0x41; *p++ = 0x89; *p++ = 0x03;                /* mov [r11],eax */
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
io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('rewrote block, lines =', s.count('\n'))
