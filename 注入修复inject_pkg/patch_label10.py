# -*- coding: utf-8 -*-
"""重写 OnEnable 版 stub：对齐修正 + 关掉兄弟节点 Num（"1/6"）。

新增：
  - 字符串构造抽成 emit_string_obj()，供 "HorizontalLayoutGroup" / "Num" 复用
  - 所有失败分支改成「短跳 + mov r11,epilogue; jmp r11」，避开 rel8 ±127 限制
  - Transform::Find @0x06916E90 (this, name) -> child
    GameObject::SetActive @0x06A85CB0 (this, bool)
"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

# ---------- 全局 ----------
if 'g_hide_wlen' not in s:
    old = u"static BYTE  g_lg_buf[0x80];\n"
    assert old in s
    s = s.replace(old, old + u"""static WCHAR g_hide_w[40];
static int   g_hide_wlen = 0;
static BYTE  g_hide_buf[0x80];
static ULONGLONG g_lg_klass = 0;   /* \u4ece m_Text \u6284\u6765\u7684 System.String klass */
static ULONGLONG g_label_par = 0;  /* Text \u7684\u7236 Transform\uff08Progress\uff09 */
""", 1)

# ---------- ini ----------
if 'hide", ""' not in s:
    old2 = u"""        g_group_align = (int)GetPrivateProfileIntA("label", "group_align", 3, ini);"""
    assert old2 in s
    s = s.replace(old2, old2 + u"""
        {
            char hn[40];
            int k;
            hn[0] = 0;
            GetPrivateProfileStringA("label", "hide", "", hn, sizeof(hn), ini);
            for (k = 0; hn[k] && k < 39; k++) g_hide_w[k] = (WCHAR)hn[k];
            g_hide_w[k] = 0;
            g_hide_wlen = k;
        }""", 1)

# ---------- 重写 builder ----------
start = s.index(u'static BYTE *build_label_lg_stub(BYTE *out, BYTE *tramp)')
end   = s.index(u'static void apply_label_lg(HMODULE ua)')

new = u'''/* \u5728 stub \u91cc\u6784\u9020\u4e00\u4e2a Il2CppString\uff1aklass \u53d6 g_lg_klass\uff08\u6765\u81ea m_Text\uff09 */
static BYTE *emit_string_obj(BYTE *p, ULONGLONG klass_src, ULONGLONG bufaddr,
                             ULONGLONG waddr, int len)
{
    LONG  bytes = (LONG)len * 2;
    int   at_loop;

    p = emit_mov_r11(p, (const void *)(ULONG_PTR)bufaddr);
    p = emit_mov_rax(p, (const void *)(ULONG_PTR)klass_src);
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x00;                      /* mov rax,[rax] */
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;                      /* mov [r11],rax */
    *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;
    { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
    *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;
    memcpy(p, &len, 4); p += 4;

    *p++ = 0x49; *p++ = 0xB8; memcpy(p, &waddr, 8); p += 8;     /* mov r8,src */
    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &bytes, 4); p += 4;     /* mov r10d,bytes */

    at_loop = (int)(p - s_none);
    *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;                      /* mov r9b,[r8] */
    *p++ = 0x45; *p++ = 0x88; *p++ = 0x4B; *p++ = 0x14;         /* mov [r11+0x14],r9b */
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
    { int j = (int)(p - s_none); *p++ = 0x75; *p++ = 0x00;
      s_none[0] = s_none[0]; /* no-op */
      *(int *)0 = 0; }
    return p;
}

'''
# 上面这个草稿有 bug（依赖全局游标），不用它，改用下面的内联版本
new = u'''static BYTE *build_label_lg_stub(BYTE *out, BYTE *tramp)
{
    BYTE *p = out;
    int   j_null, j_len, j_cmp, j_loop, j_hit;
    int   at_replay, at_hit, at_cmp, at_epi;
    int   epi_fix[8], n_epi = 0;
    int   flen = g_label_fromlen;
    LONG  fbytes = (LONG)flen * 2;
    ULONGLONG fromaddr = (ULONGLONG)(ULONG_PTR)g_label_from;
    ULONGLONG lgbuf    = (ULONGLONG)(ULONG_PTR)g_lg_buf;
    ULONGLONG lgw      = (ULONGLONG)(ULONG_PTR)g_lg_w;
    ULONGLONG hidebuf  = (ULONGLONG)(ULONG_PTR)g_hide_buf;
    ULONGLONG hidew    = (ULONGLONG)(ULONG_PTR)g_hide_w;
    ULONGLONG klasssrc = (ULONGLONG)(ULONG_PTR)&g_lg_klass;
    ULONGLONG parsrc   = (ULONGLONG)(ULONG_PTR)&g_label_par;
    LONG  ga = g_group_align;
    int   hlen = (g_hide_wlen > 0) ? g_hide_wlen : 0;

    /* ================= \u5224\u5b9a ================= */
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x81; *p++ = 0xD8; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
    j_null = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    *p++ = 0x81; *p++ = 0x78; *p++ = 0x10;
    memcpy(p, &flen, 4); p += 4;
    j_len = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &fbytes, 4); p += 4;
    *p++ = 0x49; *p++ = 0xB8; memcpy(p, &fromaddr, 8); p += 8;
    *p++ = 0x4C; *p++ = 0x8D; *p++ = 0x58; *p++ = 0x14;

    at_cmp = (int)(p - out);
    *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;
    *p++ = 0x45; *p++ = 0x3A; *p++ = 0x0B;
    j_cmp = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
    j_loop = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    j_hit = (int)(p - out);
    p = emit_mov_rax(p, NULL);
    *p++ = 0xFF; *p++ = 0xE0;

    at_replay = (int)(p - out);
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;

    at_hit = (int)(p - out);
    *(ULONGLONG *)(out + j_hit + 2) = (ULONGLONG)(ULONG_PTR)(out + at_hit);

    /* ================= \u547d\u4e2d ================= */
    *p++ = 0x51;                                                /* push rcx */
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x20;         /* sub rsp,0x20 */

    /* g_lg_klass = [m_Text] */
    p = emit_mov_r11(p, &g_lg_klass);
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x10;                      /* mov rdx,[rax] */
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x13;                      /* mov [r11],rdx */

    /* tr = get_transform(Text) */
    p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1170));
    *p++ = 0xFF; *p++ = 0xD0;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
    { int j = (int)(p - out); *p++ = 0x74; *p++ = 0x0D;
      *p++ = 0x49; *p++ = 0xBB; epi_fix[n_epi++] = (int)(p - out); p += 8;
      *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE3; }

    /* par = get_parent(tr) */
    *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;
    p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06918270));
    *p++ = 0xFF; *p++ = 0xD0;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
    { int j = (int)(p - out); *p++ = 0x74; *p++ = 0x0D;
      *p++ = 0x49; *p++ = 0xBB; epi_fix[n_epi++] = (int)(p - out); p += 8;
      *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE3; }
    p = emit_mov_r11(p, &g_label_par);
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;                      /* mov [r11],rax */

    /* ---------------- hide\uff08\u53bb\u6389 "1/6"\uff09 ---------------- */
    if (hlen > 0) {
        LONG  hbytes = (LONG)hlen * 2;
        int   j_nochild, j_nogo, at_hide_done, at_hide_loop;

        /* \u6784\u9020 "Num" \u5b57\u7b26\u4e32 */
        p = emit_mov_r11(p, (const void *)(ULONG_PTR)hidebuf);
        p = emit_mov_rax(p, (const void *)(ULONG_PTR)klasssrc);
        *p++ = 0x48; *p++ = 0x8B; *p++ = 0x00;
        *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;
        *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;
        { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
        *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;
        memcpy(p, &hlen, 4); p += 4;
        *p++ = 0x49; *p++ = 0xB8; memcpy(p, &hidew, 8); p += 8;
        *p++ = 0x41; *p++ = 0xBA; memcpy(p, &hbytes, 4); p += 4;
        at_hide_loop = (int)(p - out);
        *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;
        *p++ = 0x45; *p++ = 0x88; *p++ = 0x4B; *p++ = 0x14;
        *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
        *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;
        *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
        { int j = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
          out[j + 1] = (BYTE)(at_hide_loop - (j + 2)); }

        /* child = par.Find("Num") */
        p = emit_mov_rax(p, (const void *)(ULONG_PTR)parsrc);
        *p++ = 0x48; *p++ = 0x8B; *p++ = 0x08;                  /* mov rcx,[rax] */
        *p++ = 0x48; *p++ = 0xBA; memcpy(p, &hidebuf, 8); p += 8;
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06916E90));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        j_nochild = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

        /* go = get_gameObject(child) */
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1130));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        j_nogo = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

        /* SetActive(go, false) */
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;                  /* mov rcx,rax */
        *p++ = 0x31; *p++ = 0xD2;                                /* xor edx,edx */
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06A85CB0));
        *p++ = 0xFF; *p++ = 0xD0;

        at_hide_done = (int)(p - out);
        out[j_nochild + 1] = (BYTE)(at_hide_done - (j_nochild + 2));
        out[j_nogo    + 1] = (BYTE)(at_hide_done - (j_nogo    + 2));
    }

    /* ---------------- align\uff08\u6574\u884c\u8d34\u5de6\uff09 ---------------- */
    {
        LONG  lbytes = (LONG)g_lg_wlen * 2;
        int   j_nogo2, at_align_loop, at_align_done;

        p = emit_mov_r11(p, (const void *)(ULONG_PTR)lgbuf);
        p = emit_mov_rax(p, (const void *)(ULONG_PTR)klasssrc);
        *p++ = 0x48; *p++ = 0x8B; *p++ = 0x00;
        *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;
        *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;
        { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
        *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;
        memcpy(p, &g_lg_wlen, 4); p += 4;
        *p++ = 0x49; *p++ = 0xB8; memcpy(p, &lgw, 8); p += 8;
        *p++ = 0x41; *p++ = 0xBA; memcpy(p, &lbytes, 4); p += 4;
        at_align_loop = (int)(p - out);
        *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;
        *p++ = 0x45; *p++ = 0x88; *p++ = 0x4B; *p++ = 0x14;
        *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
        *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;
        *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
        { int j = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
          out[j + 1] = (BYTE)(at_align_loop - (j + 2)); }

        p = emit_mov_rax(p, (const void *)(ULONG_PTR)parsrc);
        *p++ = 0x48; *p++ = 0x8B; *p++ = 0x08;                  /* mov rcx,[rax] */
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1130));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        j_nogo2 = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;
        *p++ = 0x48; *p++ = 0xBA; memcpy(p, &lgbuf, 8); p += 8;
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06A859C0));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        { int j = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
          *p++ = 0xC7; *p++ = 0x40; *p++ = 0x20;
          memcpy(p, &ga, 4); p += 4;
          out[j + 1] = (BYTE)((p - out) - (j + 2)); }
        at_align_done = (int)(p - out);
        out[j_nogo2 + 1] = (BYTE)(at_align_done - (j_nogo2 + 2));
    }

    at_epi = (int)(p - out);
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xC4; *p++ = 0x20;         /* add rsp,0x20 */
    *p++ = 0x59;                                                /* pop rcx */
    {
        int k;
        for (k = 0; k < n_epi; k++)
            *(ULONGLONG *)(out + epi_fix[k]) = (ULONGLONG)(ULONG_PTR)(out + at_epi);
    }
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;

    out[j_null + 1] = (BYTE)(at_replay - (j_null + 2));
    out[j_len  + 1] = (BYTE)(at_replay - (j_len  + 2));
    out[j_cmp  + 1] = (BYTE)(at_replay - (j_cmp  + 2));
    out[j_loop + 1] = (BYTE)(at_cmp   - (j_loop + 2));
    return p;
}

'''
s = s[:start] + new + s[end:]
io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('rewritten')
