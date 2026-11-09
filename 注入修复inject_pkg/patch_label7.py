# -*- coding: utf-8 -*-
"""给 [label] 加 shift_x：把 Text 自己的 RectTransform 往左推 N 像素。

API（反汇编实证）：
  Component::get_transform        @0x068B1170  (this=rcx) -> rax = Transform
  RectTransform::get_anchoredPosition @0x06915700  (rcx=&out, rdx=this)   sret
  RectTransform::set_anchoredPosition @0x06915AC0  (rcx=this, rdx=Vector2 按值)

只对「第一次见到的这个 Text」推一次（g_label_done_tr 记住对象），
避免每次重建网格都再减一次导致越跑越左。
shift_x=0 时整段代码不生成，等于关闭。
"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

if 'g_label_shift' in s:
    print('already patched')
    raise SystemExit(0)

# 1) 全局 + ua
old = u"static int   g_label_nowrap = 1;   /* HorizontalWrapMode::Overflow\uff1b-1 = \u4e0d\u6539 */\n"
assert old in s
s = s.replace(old, old + u"""static float g_label_shift  = 0.0f;  /* \u5de6\u79fb\u50cf\u7d20\uff1b0 = \u4e0d\u52a8 */
static ULONGLONG g_label_done_tr = 0; /* \u5df2\u7ecf\u63a8\u8fc7\u7684 RectTransform\uff0c\u6bcf\u4e2a\u5bf9\u8c61\u53ea\u63a8\u4e00\u6b21 */
static float g_label_vec[4];          /* get_anchoredPosition \u7684 sret \u7f13\u51b2 */
static BYTE *g_label_ua = 0;
""", 1)

# 2) ini
old2 = u"""        g_label_nowrap = (int)GetPrivateProfileIntA("label", "nowrap", 1, ini);"""
assert old2 in s
s = s.replace(old2, old2 + u"""
        {
            char sx[32];
            sx[0] = 0;
            GetPrivateProfileStringA("label", "shift_x", "", sx, sizeof(sx), ini);
            if (sx[0]) g_label_shift = (float)atof(sx);
        }""", 1)

# 3) stub 追加位移块（在 m_Text 写完、jmp trampoline 之前）
old3 = u"""    /* m_Text = buf */
    p = emit_mov_rax(p, &g_label_buf);
    *p++ = 0x48; *p++ = 0x89; *p++ = 0x81; *p++ = 0xD8; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;

    p = emit_mov_rax(p, tramp);"""
assert old3 in s
s = s.replace(old3, u"""    /* m_Text = buf */
    p = emit_mov_rax(p, &g_label_buf);
    *p++ = 0x48; *p++ = 0x89; *p++ = 0x81; *p++ = 0xD8; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;

    /* ---- \u5de6\u79fb\uff1arcx=Text, rdx=VertexHelper \u90fd\u8981\u4fdd\u62a4 ---- */
    if (g_label_shift != 0.0f && g_label_ua) {
        int j_skip_all, j_done, at_skip;

        *p++ = 0x51;                                         /* push rcx */
        *p++ = 0x52;                                         /* push rdx */
        *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x28;  /* sub rsp,0x28 */

        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1170));
        *p++ = 0xFF; *p++ = 0xD0;                            /* call rax -> tr */

        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;               /* test rax,rax */
        j_skip_all = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

        p = emit_mov_r11(p, &g_label_done_tr);
        *p++ = 0x49; *p++ = 0x3B; *p++ = 0x03;               /* cmp rax,[r11] */
        j_done = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
        *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;               /* mov [r11],rax */

        /* get_anchoredPosition(rcx=&vec, rdx=tr) */
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC2;               /* mov rdx,rax */
        p = emit_mov_rax(p, &g_label_vec);
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;               /* mov rcx,rax */
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06915700));
        *p++ = 0xFF; *p++ = 0xD0;                            /* call rax */

        /* vec.x -= shift */
        p = emit_mov_r11(p, &g_label_vec);
        *p++ = 0xF3; *p++ = 0x41; *p++ = 0x0F; *p++ = 0x10; *p++ = 0x03;   /* movss xmm0,[r11] */
        p = emit_mov_rax(p, &g_label_shift);
        *p++ = 0xF3; *p++ = 0x0F; *p++ = 0x10; *p++ = 0x08;                /* movss xmm1,[rax] */
        *p++ = 0xF3; *p++ = 0x0F; *p++ = 0x5C; *p++ = 0xC1;                /* subss xmm0,xmm1 */
        *p++ = 0xF3; *p++ = 0x41; *p++ = 0x0F; *p++ = 0x11; *p++ = 0x03;   /* movss [r11],xmm0 */

        /* set_anchoredPosition(rcx=tr, rdx=*(u64*)vec) */
        *p++ = 0x49; *p++ = 0x8B; *p++ = 0x13;               /* mov rdx,[r11] */
        p = emit_mov_rax(p, &g_label_done_tr);
        *p++ = 0x48; *p++ = 0x8B; *p++ = 0x08;               /* mov rcx,[rax] */
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06915AC0));
        *p++ = 0xFF; *p++ = 0xD0;                            /* call rax */

        at_skip = (int)(p - out);
        out[j_skip_all + 1] = (BYTE)(at_skip - (j_skip_all + 2));
        out[j_done     + 1] = (BYTE)(at_skip - (j_done + 2));

        *p++ = 0x48; *p++ = 0x83; *p++ = 0xC4; *p++ = 0x28;  /* add rsp,0x28 */
        *p++ = 0x5A;                                         /* pop rdx */
        *p++ = 0x59;                                         /* pop rcx */
    }

    p = emit_mov_rax(p, tramp);""", 1)

# 4) apply_label 里设置 ua
old4 = u"""    hexdump(fn, LEN_LABEL_ENABLE, hs, sizeof(hs));
    logf("[label] Text::OnPopulateMesh %p \u5f53\u524d: %s", fn, hs);"""
assert old4 in s
s = s.replace(old4, u"""    g_label_ua = (BYTE *)ua;
    hexdump(fn, LEN_LABEL_ENABLE, hs, sizeof(hs));
    logf("[label] Text::OnPopulateMesh %p \u5f53\u524d: %s  shift_x=%.1f", fn, hs, g_label_shift);""", 1)

io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('ok')
