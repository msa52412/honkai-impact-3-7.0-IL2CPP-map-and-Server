# -*- coding: utf-8 -*-
"""给 [label] 加上「左对齐 + 不换行」两个字段写入。

实证（反汇编 setter）：
  Text.set_alignment          -> [[rcx+0xD0] + 0x2C] = edx
  Text.set_horizontalOverflow -> [[rcx+0xD0] + 0x34] = edx   (0=Wrap 1=Overflow)
  Text.set_verticalOverflow   -> [[rcx+0xD0] + 0x38] = edx
Text+0xD0 = m_FontData。
"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

if 'g_label_align' in s:
    print('already patched')
    raise SystemExit(0)

# 1) 全局
old = u"static float g_label_col[4] = { 1.0f, 1.0f, 1.0f, 1.0f };\n"
assert old in s
s = s.replace(old, old + u"""static int   g_label_align  = 3;   /* TextAnchor::MiddleLeft\uff1b-1 = \u4e0d\u6539 */
static int   g_label_nowrap = 1;   /* HorizontalWrapMode::Overflow\uff1b-1 = \u4e0d\u6539 */
""", 1)

# 2) ini
old2 = u"""        logf("[label] enabled=%d  \\"%ls\\"(%d) -> \\"%ls\\"(%d)",
             g_label_enabled, g_label_from, g_label_fromlen,
             g_label_to, g_label_tolen);"""
assert old2 in s
s = s.replace(old2, u"""        g_label_align  = (int)GetPrivateProfileIntA("label", "align",  3, ini);
        g_label_nowrap = (int)GetPrivateProfileIntA("label", "nowrap", 1, ini);
        logf("[label] enabled=%d  align=%d nowrap=%d  (from %d \u5b57 -> to %d \u5b57)",
             g_label_enabled, g_label_align, g_label_nowrap,
             g_label_fromlen, g_label_tolen);""", 1)

# 3) stub：颜色写完后，追加字体字段
old3 = u"""    /* m_Text = buf */
    p = emit_mov_rax(p, &g_label_buf);"""
assert old3 in s
s = s.replace(old3, u"""    /* \u5b57\u4f53\u5c5e\u6027\uff1am_FontData=[rcx+0xD0]\uff0calignment @+0x2C\uff0chorizontalOverflow @+0x34 */
    if (g_label_align >= 0 || g_label_nowrap >= 0) {
        int j_no_fd;
        *p++ = 0x48; *p++ = 0x8B; *p++ = 0x81; *p++ = 0xD0; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;                  /* test rax,rax */
        j_no_fd = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
        if (g_label_align >= 0) {
            LONG v = g_label_align;
            *p++ = 0xC7; *p++ = 0x40; *p++ = 0x2C;
            memcpy(p, &v, 4); p += 4;
        }
        if (g_label_nowrap >= 0) {
            LONG v = g_label_nowrap;
            *p++ = 0xC7; *p++ = 0x40; *p++ = 0x34;
            memcpy(p, &v, 4); p += 4;
        }
        out[j_no_fd + 1] = (BYTE)((p - out) - (j_no_fd + 2));
    }

    /* m_Text = buf */
    p = emit_mov_rax(p, &g_label_buf);""", 1)

io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('ok')
