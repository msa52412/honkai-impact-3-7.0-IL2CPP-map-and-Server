# -*- coding: utf-8 -*-
"""修正 [label] stub：不再破坏 rdx。

Text::OnPopulateMesh(this=rcx, VertexHelper=rdx)，入口第一条就是
`mov [rsp+0x10], rdx`。上一版拿 rdx 当比较/拷贝游标，等于把 VertexHelper
换成了自己的字符串地址 —— 启动期任何一次文字重建都会崩。
改用 r8 当游标（r8 在 2 参数函数里是易失、未使用）。
"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

start = s.index(u'static BYTE *build_label_stub(BYTE *out, BYTE *tramp)')
end   = s.index(u'static void apply_label(HMODULE ua)')

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

    /* \u2605 rdx \u5fc5\u987b\u539f\u6837\u4fdd\u7559\uff08\u5b83\u662f VertexHelper\uff09\uff0c\u6e38\u6807\u4e00\u5f8b\u7528 r8 */

    /* rax = m_Text\uff08Text + 0xD8\uff09 */
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x81; *p++ = 0xD8; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;                     /* test rax, rax */
    j_null = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    /* cmp dword [rax+0x10], flen */
    *p++ = 0x81; *p++ = 0x78; *p++ = 0x10;
    memcpy(p, &flen, 4); p += 4;
    j_len = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &fbytes, 4); p += 4;   /* mov r10d, flen*2 */
    *p++ = 0x49; *p++ = 0xB8; memcpy(p, &fromaddr, 8); p += 8; /* mov r8, from */
    *p++ = 0x4C; *p++ = 0x8D; *p++ = 0x58; *p++ = 0x14;        /* lea r11,[rax+0x14] */

    at_cmp_loop = (int)(p - out);
    *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;                     /* mov r9b,[r8] */
    *p++ = 0x45; *p++ = 0x3A; *p++ = 0x0B;                     /* cmp r9b,[r11] */
    j_cmp = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;                     /* inc r8 */
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;                     /* inc r11 */
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;                     /* dec r10d */
    j_loop = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    /* \u547d\u4e2d -> \u7edd\u5bf9\u8df3\u8fc7 replay */
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

    /* \u9020 String\uff1aklass \u6284\u539f\u4e32\u7684 [rax]\uff1b\u7528 r8 \u5f53\u6e38\u6807 */
    p = emit_mov_r11(p, &g_label_buf);                          /* r11 = buf */
    *p++ = 0x4C; *p++ = 0x8B; *p++ = 0x00;                     /* mov r8,[rax]  klass */
    *p++ = 0x4D; *p++ = 0x89; *p++ = 0x03;                     /* mov [r11],r8 */
    *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;        /* mov qword [r11+8],0 */
    { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
    *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;        /* mov dword [r11+0x10],n */
    memcpy(p, &n, 4); p += 4;

    *p++ = 0x49; *p++ = 0xB8; memcpy(p, &toaddr, 8); p += 8;   /* mov r8,to */
    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &nbytes, 4); p += 4;   /* mov r10d,n*2 */

    at_copy_loop = (int)(p - out);
    *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;                     /* mov r9b,[r8] */
    *p++ = 0x45; *p++ = 0x88; *p++ = 0x4B; *p++ = 0x14;        /* mov [r11+0x14],r9b */
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;                     /* inc r11 */
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;                     /* inc r8 */
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
old = s[start:end]

# 上一版 apply_label 里的 build 调用签名不变，只替换 builder
s = s[:start] + new + s[end:]
io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('rebuilt builder (rdx preserved)')
