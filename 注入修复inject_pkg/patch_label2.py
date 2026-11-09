# -*- coding: utf-8 -*-
"""重排 build_label_stub：replay 段紧跟在判定之后，命中段用绝对跳转跨过去。
（rel8 只有 ±127，命中段 120+ 字节，直接顺序放会把失败分支的跳转挤成负数。）"""
import io, re

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

start = s.index(u'static BYTE *build_label_stub(BYTE *out, BYTE *tramp)')
end   = s.index(u'static void apply_label(HMODULE ua)')
old   = s[start:end]

new = u'''static BYTE *build_label_stub(BYTE *out, BYTE *tramp)
{
    BYTE *p = out;
    int   j_null, j_len, j_cmp, j_loop, j_copy, j_hit;
    int   at_replay, at_hit, at_cmp_loop, at_copy_loop;
    int   flen = g_label_fromlen;
    int   n    = g_label_tolen;
    LONG  fbytes = (LONG)flen * 2;
    LONG  nbytes = (LONG)n * 2;
    ULONGLONG fromaddr = (ULONGLONG)(ULONG_PTR)g_label_from;
    ULONGLONG toaddr   = (ULONGLONG)(ULONG_PTR)g_label_to;
    ULONGLONG bufaddr  = (ULONGLONG)(ULONG_PTR)g_label_buf;

    /* rax = m_Text\uff08Text + 0xD8\uff09 */
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x81; *p++ = 0xD8; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;                     /* test rax, rax */
    j_null = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    /* cmp dword [rax+0x10], flen */
    *p++ = 0x81; *p++ = 0x78; *p++ = 0x10;
    memcpy(p, &flen, 4); p += 4;
    j_len = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &fbytes, 4); p += 4;   /* mov r10d, flen*2 */
    *p++ = 0x48; *p++ = 0xBA; memcpy(p, &fromaddr, 8); p += 8; /* mov rdx, from */
    *p++ = 0x4C; *p++ = 0x8D; *p++ = 0x58; *p++ = 0x14;        /* lea r11,[rax+0x14] */

    at_cmp_loop = (int)(p - out);
    *p++ = 0x44; *p++ = 0x8A; *p++ = 0x0A;                     /* mov r9b,[rdx] */
    *p++ = 0x45; *p++ = 0x3A; *p++ = 0x0B;                     /* cmp r9b,[r11] */
    j_cmp = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
    *p++ = 0x48; *p++ = 0xFF; *p++ = 0xC2;                     /* inc rdx */
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;                     /* inc r11 */
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;                     /* dec r10d */
    j_loop = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    /* \u547d\u4e2d -> \u8df3\u8fc7 replay\uff08\u7edd\u5bf9\u5730\u5740\uff0c\u4e0d\u53d7 rel8 \u9650\u5236\uff09 */
    j_hit = (int)(p - out);
    p = emit_mov_rax(p, NULL);
    *p++ = 0xFF; *p++ = 0xE0;

    at_replay = (int)(p - out);
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;

    at_hit = (int)(p - out);
    *(ULONGLONG *)(out + j_hit + 2) = (ULONGLONG)(ULONG_PTR)(out + at_hit);

    /* ---- \u547d\u4e2d ---- */
    p = emit_mov_r11(p, &g_label_hits);
    *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;        /* lock inc [r11] */

    /* \u9020 String\uff1aklass \u76f4\u63a5\u53d6\u539f\u4e32\u7684 [rax] */
    p = emit_mov_r11(p, &g_label_buf);                          /* r11 = buf */
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x10;                     /* mov rdx,[rax] */
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x13;                     /* mov [r11],rdx */
    *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;        /* mov qword [r11+8],0 */
    { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
    *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;        /* mov dword [r11+0x10],n */
    memcpy(p, &n, 4); p += 4;

    *p++ = 0x48; *p++ = 0xBA; memcpy(p, &toaddr, 8); p += 8;   /* mov rdx,to */
    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &nbytes, 4); p += 4;   /* mov r10d,n*2 */

    at_copy_loop = (int)(p - out);
    *p++ = 0x44; *p++ = 0x8A; *p++ = 0x0A;                     /* mov r9b,[rdx] */
    *p++ = 0x45; *p++ = 0x88; *p++ = 0x4B; *p++ = 0x14;        /* mov [r11+0x14],r9b */
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;                     /* inc r11 */
    *p++ = 0x48; *p++ = 0xFF; *p++ = 0xC2;                     /* inc rdx */
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;                     /* dec r10d */
    j_copy = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    /* \u5b57\u8272\uff1aGraphic::m_Color @ this+0x20 */
    {
        int c;
        for (c = 0; c < 4; c++) {
            LONG bits;
            memcpy(&bits, &g_label_col[c], 4);
            *p++ = 0xC7; *p++ = 0x41; *p++ = (BYTE)(0x20 + c * 4);
            memcpy(p, &bits, 4); p += 4;
        }
    }
    /* m_Text = buf */
    p = emit_mov_rax(p, &g_label_buf);
    *p++ = 0x48; *p++ = 0x89; *p++ = 0x81; *p++ = 0xD8; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;

    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;

    out[j_null + 1] = (BYTE)(at_replay - (j_null + 2));
    out[j_len  + 1] = (BYTE)(at_replay - (j_len  + 2));
    out[j_cmp  + 1] = (BYTE)(at_replay - (j_cmp  + 2));
    out[j_loop + 1] = (BYTE)(at_cmp_loop - (j_loop + 2));
    out[j_copy + 1] = (BYTE)(at_copy_loop - (j_copy + 2));
    return p;
}

'''
s = s[:start] + new + s[end:]
io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('rewritten')
