# -*- coding: utf-8 -*-
"""新增 [label] 的第二个 hook：Text::OnEnable 里把父节点 HorizontalLayoutGroup
的 childAlignment 从 5(MiddleRight) 改成 3(MiddleLeft)。

为什么放 OnEnable：OnEnable 之后原函数会 SetAllDirty -> 触发布局重算，
正好用到我们改过的对齐；而放在 OnPopulateMesh（网格重建中）改布局会重入崩溃。

调用链（全部有反汇编依据）：
  Component::get_transform      @0x068B1170  (rcx=this) -> rax
  Transform::get_parent         @0x06918270  (rcx=tr)   -> rax
  Component::get_gameObject     @0x068B1130  (rcx=this) -> rax
  GameObject.GetComponentByName @0x06A859C0  (rcx=go, rdx=string) -> rax
  LayoutGroup.m_ChildAlignment  = [lg+0x20] (int)
"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

if 'build_label_lg_stub' in s:
    print('already patched')
    raise SystemExit(0)

# ---------- 1) 全局 ----------
old = u"static BYTE *g_label_ua = 0;\n"
assert old in s
s = s.replace(old, u"""static BYTE *g_label_ua = 0;
/* \u7236\u8282\u70b9\u5bf9\u9f50\uff1aHorizontalLayoutGroup.childAlignment @ +0x20\uff085=\u53f3 3=\u5de6\uff09 */
static int   g_group_align = 3;
static WCHAR g_lg_w[40];
static int   g_lg_wlen = 0;
static BYTE  g_lg_buf[0x80];
""", 1)

# ---------- 2) ini ----------
old2 = u"""            if (sx[0]) g_label_shift = (float)atof(sx);
        }"""
assert old2 in s
s = s.replace(old2, old2 + u"""
        g_group_align = (int)GetPrivateProfileIntA("label", "group_align", 3, ini);
        {
            const char *cname = "HorizontalLayoutGroup";
            int k;
            for (k = 0; cname[k] && k < 39; k++) g_lg_w[k] = (WCHAR)cname[k];
            g_lg_wlen = k;
        }""", 1)

# ---------- 3) OnEnable 常量 + stub + apply ----------
anchor = u"static void apply_label(HMODULE ua)"
assert anchor in s
module = u"""/* ---- \u7236\u8282\u70b9\u5bf9\u9f50\u4fee\u6b63\uff08Text::OnEnable\uff09 ----------------------------------
 * \u5b9e\u6d4b\uff1aProgress \u4e0a\u6302\u4e86 HorizontalLayoutGroup\uff0cpadding.left=0\uff0c
 * \u4f46 childAlignment=5\uff08MiddleRight\uff09\uff0c\u6240\u4ee5\u6574\u884c\u9760\u53f3\u3001\u5de6\u8fb9\u7a7a\u51fa\u4e00\u5927\u5757\u3002
 * \u6539\u6210 3\uff08MiddleLeft\uff09\u5373\u53ef\u3002
 *
 * Text::OnEnable @ 0x06905CD0 \u524d 9 \u5b57\u8282\u662f\u5b8c\u6574\u6307\u4ee4\uff0c\u540e\u9762\u7d27\u8ddf\u4e00\u6761 call\uff0c
 * \u88c5\u4e0d\u4e0b 14 \u5b57\u8282\u7edd\u5bf9\u8df3\u8f6c \u2192 \u7528 E9 rel32 + 4\u00d790\uff0cstub \u653e alloc_near \u9875\u3002
 */
#define RVA_LABEL_ONENABLE  0x06905CD0
#define LEN_LABEL_ONENABLE  9
static const BYTE ORIG_LABEL_ONENABLE[LEN_LABEL_ONENABLE] = {
    0x40, 0x53, 0x48, 0x83, 0xEC, 0x20, 0x48, 0x8B, 0xD9
};

static BYTE *build_label_lg_stub(BYTE *out, BYTE *tramp)
{
    BYTE *p = out;
    int   j_null, j_len, j_cmp, j_loop, j_copy, j_nolay, j_all, j_lg;
    int   at_replay, at_cmp, at_copy, at_after;
    int   flen = g_label_fromlen;
    int   n    = g_lg_wlen;
    LONG  fbytes = (LONG)flen * 2;
    LONG  nbytes = (LONG)n * 2;
    ULONGLONG fromaddr = (ULONGLONG)(ULONG_PTR)g_label_from;
    ULONGLONG lgbuf    = (ULONGLONG)(ULONG_PTR)g_lg_buf;
    ULONGLONG lgw      = (ULONGLONG)(ULONG_PTR)g_lg_w;
    LONG  ga           = g_group_align;

    /* rax = m_Text */
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
    *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;                     /* mov r9b,[r8] */
    *p++ = 0x45; *p++ = 0x3A; *p++ = 0x0B;                     /* cmp r9b,[r11] */
    j_cmp = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
    j_loop = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    /* ---- 命中：造 "HorizontalLayoutGroup" 字符串 ---- */
    p = emit_mov_r11(p, &g_lg_buf);
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x10;                     /* mov rdx,[rax] klass */
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x13;
    *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;
    { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
    *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;
    memcpy(p, &n, 4); p += 4;

    *p++ = 0x49; *p++ = 0xB8; memcpy(p, &lgw, 8); p += 8;      /* mov r8, g_lg_w */
    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &nbytes, 4); p += 4;
    at_copy = (int)(p - out);
    *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;                     /* mov r9b,[r8] */
    *p++ = 0x45; *p++ = 0x88; *p++ = 0x4B; *p++ = 0x14;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
    { int j2 = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
      out[j2 + 1] = (BYTE)(at_copy - (j2 + 2)); }

    /* rcx \u4fdd\u62a4\uff1a\u540e\u9762\u8981\u7528 Text \u505a get_transform */
    *p++ = 0x51;                                               /* push rcx */
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x20;        /* sub rsp,0x20（shadow，且保持 16 字节对齐） */

    p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1170));   /* get_transform */
    *p++ = 0xFF; *p++ = 0xD0;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;                     /* test rax,rax */
    j_all = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;                     /* mov rcx,rax (tr) */
    p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06918270));   /* get_parent */
    *p++ = 0xFF; *p++ = 0xD0;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
    j_nolay = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;                     /* mov rcx,rax (par) */
    p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1130));   /* get_gameObject */
    *p++ = 0xFF; *p++ = 0xD0;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
    *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;                     /* mov rcx,rax (go) */
    *p++ = 0x48; *p++ = 0xBA; memcpy(p, &lgbuf, 8); p += 8;    /* mov rdx,&str */
    p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06A859C0));   /* GetComponentByName */
    *p++ = 0xFF; *p++ = 0xD0;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;                     /* test rax,rax */
    j_lg = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
    *p++ = 0xC7; *p++ = 0x40; *p++ = 0x20;                     /* mov dword [rax+0x20], ga */
    memcpy(p, &ga, 4); p += 4;

    at_after = (int)(p - out);
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xC4; *p++ = 0x20;        /* add rsp,0x20 */
    *p++ = 0x59;                                               /* pop rcx */

    at_replay = (int)(p - out);
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;

    out[j_null + 1] = (BYTE)(at_replay - (j_null + 2));
    out[j_len  + 1] = (BYTE)(at_replay - (j_len  + 2));
    out[j_cmp  + 1] = (BYTE)(at_replay - (j_cmp  + 2));
    out[j_loop + 1] = (BYTE)(at_cmp - (j_loop + 2));
    out[j_all  + 1] = (BYTE)(at_after - (j_all  + 2));
    out[j_nolay + 1] = (BYTE)(at_after - (j_nolay + 2));
    out[j_lg    + 1] = (BYTE)(at_after - (j_lg    + 2));
    return p;
}

static void apply_label_lg(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_LABEL_ONENABLE;
    BYTE *page, *stub, *tramp, *end, *tend;
    BYTE  patch[LEN_LABEL_ONENABLE];
    int   i, nt;
    HANDLE th[MAX_SUSPEND];

    if (!is_original(fn, ORIG_LABEL_ONENABLE, LEN_LABEL_ONENABLE)) {
        logf("[label] !! OnEnable 原始字节不匹配，跳过对齐修正");
        return;
    }
    page = alloc_near(fn);
    if (!page) {
        logf("[label] !! alloc_near 失败，跳过对齐修正");
        return;
    }
    stub  = page;
    tramp = page + 0x400;
    end   = build_label_lg_stub(stub, tramp);
    tend  = emit_bytes(tramp, ORIG_LABEL_ONENABLE, LEN_LABEL_ONENABLE);
    *tend++ = 0xFF; *tend++ = 0x25;
    *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00;
    {
        ULONGLONG v = (ULONGLONG)(ULONG_PTR)((BYTE *)ua + RVA_LABEL_ONENABLE + LEN_LABEL_ONENABLE);
        memcpy(tend, &v, 8);
        tend += 8;
    }
    FlushInstructionCache(GetCurrentProcess(), page, (DWORD)(tend - page));

    {
        LONG rel = (LONG)(stub - (fn + 5));
        patch[0] = 0xE9;
        memcpy(patch + 1, &rel, 4);
        for (i = 5; i < LEN_LABEL_ONENABLE; i++) patch[i] = 0x90;

        nt = suspend_other_threads(th, MAX_SUSPEND);
        if (write_code(fn, patch, sizeof(patch))) {
            resume_other_threads(th, nt);
            logf("[label] OnEnable 已接管（父布局 childAlignment -> %d，stub %d 字节）",
                 g_group_align, (int)(end - stub));
        } else {
            resume_other_threads(th, nt);
            logf("[label] !! OnEnable 打补丁失败 err=%lu", GetLastError());
        }
    }
}

static void apply_label(HMODULE ua)"""
s = s.replace(anchor, module, 1)

# ---------- 4) init 里调用 ----------
old4 = u"""    if (g_label_enabled) {
        apply_label(ua);"""
assert old4 in s
s = s.replace(old4, u"""    if (g_label_enabled) {
        apply_label(ua);
        apply_label_lg(ua);""", 1)

io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('ok')
