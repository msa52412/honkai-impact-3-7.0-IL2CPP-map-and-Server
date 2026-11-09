# -*- coding: utf-8 -*-
"""把「隐藏 Num + 父布局贴左」并进 build_label_stub（OnPopulateMesh 命中段）。
原因：Text::OnEnable 在 DLL 装 hook 之前就已经跑完，那个 hook 永远不会命中。
"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

# ---------- 1) 新增一次性 guard ----------
if 'g_label_lg_done' not in s:
    s = s.replace(
        "static volatile LONG g_label_hits = 0;",
        "static volatile LONG g_label_hits = 0;\n"
        "static volatile LONG g_label_lg_done = 0;  /* 隐藏 Num / 改父布局只做一次 */", 1)

# ---------- 2) 在 build_label_stub 的「m_Text = buf」之后插入 ----------
anchor = """    /* m_Text = buf */
    p = emit_mov_rax(p, &g_label_buf);
    *p++ = 0x48; *p++ = 0x89; *p++ = 0x81; *p++ = 0xD8; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;
"""
assert anchor in s, 'anchor not found'
assert 'g_label_lg_done' in s

block = anchor + r'''
    /* ---- 一次性：隐藏兄弟节点 + 父布局贴左 -------------------------------
     * 为什么放在这里：Text::OnEnable 在 DLL 打补丁之前就已经执行过，
     * 挂 OnEnable 的 hook 永远不命中（实测 childAlignment 一直是 5）。
     * OnPopulateMesh 每帧都会跑，命中这一段就一定是目标 Text。
     * 进段先置 done=1，避免 SetActive 触发布局重算 -> 重入 -> 无限递归。
     * rcx(Text) / rdx(VertexHelper) 全程保护，rdx 绝不能被改成别的东西。
     */
    if (g_label_ua && (g_hide_wlen > 0 || g_group_align >= 0)) {
        int   j_done, at_lg_end, epi_lg[8], n_epi_lg = 0;
        int   at_nohide, j_nc, j_ng, at_noalign, j_ng2, j_nc2;
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

        /* if (done) goto end */
        p = emit_mov_rax(p, &g_label_lg_done);
        *p++ = 0x83; *p++ = 0x38; *p++ = 0x00;                /* cmp dword [rax],0 */
        j_done = (int)(p - out); *p++ = 0x74; *p++ = 0x0D;    /* je +13 -> 继续 */
        p = emit_mov_r11(p, NULL); epi_lg[n_epi_lg++] = (int)(p - out) - 8;
        *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE3;                /* jmp r11 */

        /* 立刻置位，防重入 */
        p = emit_mov_rax(p, &g_label_lg_done);
        *p++ = 0xC7; *p++ = 0x00;                             /* mov dword [rax],1 */
        { LONG one = 1; memcpy(p, &one, 4); p += 4; }

        /* tr = get_transform(Text) */
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1170));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        { int j = (int)(p - out); *p++ = 0x74; *p++ = 0x0D;
          p = emit_mov_r11(p, NULL); epi_lg[n_epi_lg++] = (int)(p - out) - 8;
          *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE3; (void)j; }

        /* par = get_parent(tr) */
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06918270));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        { int j = (int)(p - out); *p++ = 0x74; *p++ = 0x0D;
          p = emit_mov_r11(p, NULL); epi_lg[n_epi_lg++] = (int)(p - out) - 8;
          *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE3; (void)j; }
        p = emit_mov_r11(p, &g_label_par);
        *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;                /* mov [r11],rax */

        at_nohide = (int)(p - out);

        /* ---------- hide：par.Find("Num") -> SetActive(false) ---------- */
        if (hlen2 > 0) {
            /* 造 String（klass 抄 g_label_buf 的头 8 字节） */
            p = emit_mov_r11(p, (const void *)(ULONG_PTR)hideb);
            p = emit_mov_rax(p, &g_label_buf);
            *p++ = 0x48; *p++ = 0x8B; *p++ = 0x00;            /* mov rax,[rax] */
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
            *p++ = 0x48; *p++ = 0x8B; *p++ = 0x08;            /* mov rcx,[rax] */
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
            at_nohide = (int)(p - out);
            out[j_nc + 1] = (BYTE)(at_nohide - (j_nc + 2));
            out[j_ng + 1] = (BYTE)(at_nohide - (j_ng + 2));
        }

        at_noalign = (int)(p - out);

        /* ---------- align：父节点 HorizontalLayoutGroup -> MiddleLeft ---------- */
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
            *p++ = 0xC7; *p++ = 0x40; *p++ = 0x20;            /* mov dword [rax+0x20], ga */
            memcpy(p, &ga2, 4); p += 4;
            at_noalign = (int)(p - out);
            out[j_ng2 + 1] = (BYTE)(at_noalign - (j_ng2 + 2));
            out[j_nc2 + 1] = (BYTE)(at_noalign - (j_nc2 + 2));
        }
        (void)at_nohide;

        at_lg_end = (int)(p - out);
        {
            int k;
            for (k = 0; k < n_epi_lg; k++)
                *(ULONGLONG *)(out + epi_lg[k]) = (ULONGLONG)(ULONG_PTR)(out + at_lg_end);
        }
        (void)j_done;
        *p++ = 0x48; *p++ = 0x83; *p++ = 0xC4; *p++ = 0x28;   /* add rsp,0x28 */
        *p++ = 0x5A;                                          /* pop rdx */
        *p++ = 0x59;                                          /* pop rcx */
    }

'''

s = s.replace(anchor, block, 1)
io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('inserted, file lines =', s.count('\n'))
