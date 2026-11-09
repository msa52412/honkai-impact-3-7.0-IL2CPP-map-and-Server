# -*- coding: utf-8 -*-
"""重写 build_label_lg_stub：
  · 布局改成「判定 → replay 段 → 命中段」，跨段用绝对跳转（rel8 只有 ±127）
  · 补上 get_gameObject 的空指针检查
  · 尾部统一 add rsp/pop rcx 后跳 trampoline
"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

start = s.index(u'static BYTE *build_label_lg_stub(BYTE *out, BYTE *tramp)')
end   = s.index(u'static void apply_label_lg(HMODULE ua)')

new = u'''static BYTE *build_label_lg_stub(BYTE *out, BYTE *tramp)
{
    BYTE *p = out;
    int   j_null, j_len, j_cmp, j_loop, j_hit, j_lg;
    int   at_replay, at_hit, at_cmp, at_copy, at_after;
    int   flen = g_label_fromlen;
    int   n    = g_lg_wlen;
    LONG  fbytes = (LONG)flen * 2;
    LONG  nbytes = (LONG)n * 2;
    ULONGLONG fromaddr = (ULONGLONG)(ULONG_PTR)g_label_from;
    ULONGLONG lgbuf    = (ULONGLONG)(ULONG_PTR)g_lg_buf;
    ULONGLONG lgw      = (ULONGLONG)(ULONG_PTR)g_lg_w;
    LONG  ga           = g_group_align;

    /* ---- \u5224\u5b9a ---- */
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x81; *p++ = 0xD8; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;                      /* test rax,rax */
    j_null = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    *p++ = 0x81; *p++ = 0x78; *p++ = 0x10;                      /* cmp dword [rax+0x10], flen */
    memcpy(p, &flen, 4); p += 4;
    j_len = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &fbytes, 4); p += 4;    /* mov r10d,flen*2 */
    *p++ = 0x49; *p++ = 0xB8; memcpy(p, &fromaddr, 8); p += 8;  /* mov r8,from */
    *p++ = 0x4C; *p++ = 0x8D; *p++ = 0x58; *p++ = 0x14;         /* lea r11,[rax+0x14] */

    at_cmp = (int)(p - out);
    *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;                      /* mov r9b,[r8] */
    *p++ = 0x45; *p++ = 0x3A; *p++ = 0x0B;                      /* cmp r9b,[r11] */
    j_cmp = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
    j_loop = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    /* \u547d\u4e2d -> \u7edd\u5bf9\u8df3\u8f6c */
    j_hit = (int)(p - out);
    p = emit_mov_rax(p, NULL);
    *p++ = 0xFF; *p++ = 0xE0;

    at_replay = (int)(p - out);
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;

    at_hit = (int)(p - out);
    *(ULONGLONG *)(out + j_hit + 2) = (ULONGLONG)(ULONG_PTR)(out + at_hit);

    /* ---- \u547d\u4e2d\uff1a\u9020 "HorizontalLayoutGroup" ---- */
    p = emit_mov_r11(p, &g_lg_buf);
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x10;                      /* mov rdx,[rax] klass */
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x13;
    *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;
    { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
    *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;
    memcpy(p, &n, 4); p += 4;

    *p++ = 0x49; *p++ = 0xB8; memcpy(p, &lgw, 8); p += 8;       /* mov r8,g_lg_w */
    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &nbytes, 4); p += 4;

    at_copy = (int)(p - out);
    *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;
    *p++ = 0x45; *p++ = 0x88; *p++ = 0x4B; *p++ = 0x14;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
    { int j2 = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
      out[j2 + 1] = (BYTE)(at_copy - (j2 + 2)); }

    /* ---- \u4fee\u6b63\u5bf9\u9f50 ---- */
    *p++ = 0x51;                                                /* push rcx */
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x20;         /* sub rsp,0x20 */

    p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1170));  /* get_transform */
    *p++ = 0xFF; *p++ = 0xD0;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
    j_lg = (int)(p - out); *p++ = 0x74; *p++ = 0x00;             /* tr == 0 -> after */

    *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;                      /* rcx = tr */
    p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06918270));  /* get_parent */
    *p++ = 0xFF; *p++ = 0xD0;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
    { int j3 = (int)(p - out); *p++ = 0x74; *p++ = 0x00;        /* par == 0 -> after */

      *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;                    /* rcx = par */
      p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1130)); /* get_gameObject */
      *p++ = 0xFF; *p++ = 0xD0;
      *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
      { int j4 = (int)(p - out); *p++ = 0x74; *p++ = 0x00;      /* go == 0 -> after */

        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;                  /* rcx = go */
        *p++ = 0x48; *p++ = 0xBA; memcpy(p, &lgbuf, 8); p += 8; /* rdx = &str */
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06A859C0)); /* GetComponentByName */
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        { int j5 = (int)(p - out); *p++ = 0x74; *p++ = 0x00;    /* lg == 0 -> after */
          *p++ = 0xC7; *p++ = 0x40; *p++ = 0x20;                /* mov dword [rax+0x20], ga */
          memcpy(p, &ga, 4); p += 4;
          out[j5 + 1] = (BYTE)((p - out) - (j5 + 2));
        }
        out[j4 + 1] = (BYTE)((p - out) - (j4 + 2));
      }
      out[j3 + 1] = (BYTE)((p - out) - (j3 + 2));
    }

    at_after = (int)(p - out);
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xC4; *p++ = 0x20;         /* add rsp,0x20 */
    *p++ = 0x59;                                                /* pop rcx */
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;

    out[j_null + 1] = (BYTE)(at_replay - (j_null + 2));
    out[j_len  + 1] = (BYTE)(at_replay - (j_len  + 2));
    out[j_cmp  + 1] = (BYTE)(at_replay - (j_cmp  + 2));
    out[j_loop + 1] = (BYTE)(at_cmp   - (j_loop + 2));
    out[j_lg   + 1] = (BYTE)(at_after - (j_lg   + 2));
    return p;
}

'''
s = s[:start] + new + s[end:]
io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('rewritten')
