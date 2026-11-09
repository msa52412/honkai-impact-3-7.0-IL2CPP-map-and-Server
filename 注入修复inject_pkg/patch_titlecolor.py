# -*- coding: utf-8 -*-
"""给 [title] 模块加「命中时同时改字色」的能力（一次性补丁脚本）"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

if 'g_title_col' in s:
    print('already patched')
    raise SystemExit(0)

# 1) 全局：颜色（RGBA float）
old = u"static volatile LONG g_title_calls = 0;\n"
assert old in s
s = s.replace(old, old + u"static float  g_title_col[4] = { 1.0f, 1.0f, 1.0f, 1.0f };  /* \u547d\u4e2d\u65f6\u5199\u8fdb Graphic::m_Color\uff08+0x20\uff09 */\n", 1)

# 2) ini 解析 color=
old2 = u"""        logf("[title] enabled=%d  \\"%ls\\"(%d) -> \\"%ls\\"(%d)",
             g_title_enabled, g_title_from, g_title_fromlen,
             g_title_to, g_title_tolen);
"""
if old2 not in s:
    # 退一步：按行找
    idx = s.find(u'[title] enabled=%d')
    assert idx > 0, 'title log anchor not found'
    print('warn: fallback anchor')
assert old2 in s, 'title log line not found'

new2 = old2 + u"""        {
            char cs[16];
            cs[0] = 0;
            GetPrivateProfileStringA("title", "color", "", cs, sizeof(cs), ini);
            if (cs[0]) {
                unsigned v = 0;
                int k;
                for (k = 0; cs[k] && k < 8; k++) {
                    char c = cs[k];
                    int d;
                    if (c >= '0' && c <= '9')      d = c - '0';
                    else if (c >= 'a' && c <= 'f') d = c - 'a' + 10;
                    else if (c >= 'A' && c <= 'F') d = c - 'A' + 10;
                    else continue;
                    v = (v << 4) | (unsigned)d;
                }
                g_title_col[0] = (float)((v >> 16) & 0xFF) / 255.0f;
                g_title_col[1] = (float)((v >>  8) & 0xFF) / 255.0f;
                g_title_col[2] = (float)( v        & 0xFF) / 255.0f;
                g_title_col[3] = 1.0f;
                logf("[title] color=#%06X", v & 0xFFFFFF);
            }
        }
"""
s = s.replace(old2, new2, 1)

# 3) stub：命中时写 m_Color
old3 = u"""    /* ---- \u547d\u4e2d\uff1a\u9020\u4e00\u4e2a\u81ea\u5df1\u7684 String ---- */
    p = emit_mov_r11(p, &g_title_hits);
    *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;   /* lock inc [r11] */
"""
assert old3 in s, 'hit anchor not found'
new3 = old3 + u"""
    /* \u547d\u4e2d\u65f6\u987a\u4fbf\u6539\u5b57\u8272\uff1aGraphic::m_Color \u5728 this+0x20\uff0c4 \u4e2a float\uff08RGBA\uff09\u3002
     * rcx \u4e00\u76f4\u6ca1\u88ab\u52a8\u8fc7\uff0c\u4ecd\u662f Text \u672c\u4f53\uff1b\u6539\u5b8c\u540e set_text \u4f1a SetVerticesDirty\uff0c
     * \u91cd\u5efa\u7f51\u683c\u65f6 Text.OnPopulateMesh \u4f1a\u7528\u65b0\u7684 color\u3002 */
    {
        int c;
        for (c = 0; c < 4; c++) {
            LONG bits;
            memcpy(&bits, &g_title_col[c], 4);
            *p++ = 0xC7; *p++ = 0x41; *p++ = (BYTE)(0x20 + c * 4);   /* mov dword [rcx+0x20+4c], imm32 */
            memcpy(p, &bits, 4); p += 4;
        }
    }
"""
s = s.replace(old3, new3, 1)

io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('patched ok')
