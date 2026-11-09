# -*- coding: utf-8 -*-
"""插入 [label] 模块：在 Text::OnEnable 里按内容改写标签文字 + 字色

为什么要这个模块
----------------
`MapPanel0/Challenge/Task_6/Cover/Info/Mission/Progress/Text` 那行文字
是**预制体序列化**进去的，游戏运行期从不调 `Text.set_text`（实测 984 次
set_text 调用、0 次命中），所以只能在它「被启用」的那一刻改。

Text::OnEnable @ 0x06905CD0 的入口字节：
    40 53                 push rbx
    48 83 EC 20           sub rsp, 0x20
    48 8B D9              mov rbx, rcx
    E8 82 FB F0 FF        call 0x6815860   (MaskableGraphic::OnEnable)
前 9 字节刚好停在 call 之前 —— 但 9 字节装不下 `FF 25` 那种 14 字节绝对跳转，
所以改用 5 字节 E9 rel32 + 4 个 NOP，stub 必须分配在模块 ±2GB 内。
trampoline 只回放这 9 字节，然后跳回 0x6905CD9（那条 call 原地执行，不动它）。
"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

if 'RVA_LABEL_ENABLE' in s:
    print('already patched')
    raise SystemExit(0)

# ---------------- 1) 全局 ----------------
anchor = u"static volatile LONG64 g_str_klass = 0;\n"
assert anchor in s
s = s.replace(anchor, anchor + u"""
/* 标签文字替换：[label] —— 走 Text::OnEnable，而**不是** set_text
 * （那行"当前虚境挑战书"是预制体里的静态文本，游戏从不 set_text） */
static int   g_label_enabled = 0;
static WCHAR g_label_from[64];
static int   g_label_fromlen = 0;
static WCHAR g_label_to[128];
static int   g_label_tolen = 0;
static BYTE  g_label_buf[0x400];
static float g_label_col[4] = { 1.0f, 1.0f, 1.0f, 1.0f };
static volatile LONG g_label_hits = 0;
""", 1)

# ---------------- 2) ini ----------------
anchor2 = u"""        logf("[title] color=#%06X", v & 0xFFFFFF);
            }
        }
"""
assert anchor2 in s
s = s.replace(anchor2, anchor2 + u"""
    g_label_enabled = (int)GetPrivateProfileIntA("label", "enabled", 0, ini);
    {
        char from[128], to[256], cs[16];
        from[0] = to[0] = cs[0] = 0;
        GetPrivateProfileStringA("label", "from", "", from, sizeof(from), ini);
        GetPrivateProfileStringA("label", "to",   "", to,   sizeof(to),   ini);
        GetPrivateProfileStringA("label", "color", "", cs,  sizeof(cs),  ini);
        {
            int n = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, from, -1, g_label_from, 64);
            if (n <= 0) n = MultiByteToWideChar(CP_ACP, 0, from, -1, g_label_from, 64);
            g_label_fromlen = (n > 0) ? n - 1 : 0;
            n = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, to, -1, g_label_to, 128);
            if (n <= 0) n = MultiByteToWideChar(CP_ACP, 0, to, -1, g_label_to, 128);
            g_label_tolen = (n > 0) ? n - 1 : 0;
        }
        if (g_label_fromlen <= 0 || g_label_fromlen >= 60 ||
            g_label_tolen  <= 0 || g_label_tolen  >= 100) {
            g_label_enabled = 0;
            logf("[label] !! from/to 长度异常（%d / %d），自动关闭",
                 g_label_fromlen, g_label_tolen);
        }
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
            g_label_col[0] = (float)((v >> 16) & 0xFF) / 255.0f;
            g_label_col[1] = (float)((v >>  8) & 0xFF) / 255.0f;
            g_label_col[2] = (float)( v        & 0xFF) / 255.0f;
            g_label_col[3] = 1.0f;
        }
        logf("[label] enabled=%d  \\"%ls\\"(%d) -> \\"%ls\\"(%d)",
             g_label_enabled, g_label_from, g_label_fromlen,
             g_label_to, g_label_tolen);
    }
""", 1)

# ---------------- 3) 模块本体：插在 tcover_watch 之后 ----------------
anchor3 = u"""static DWORD WINAPI tcover_watch(LPVOID param)
{
    LONG last = 0;
    (void)param;
    for (;;) {
        LONG now;
        Sleep(1500);
        now = InterlockedCompareExchange(&g_tcover.hits, 0, 0);
        if (now != last) {
            last = now;
            logf("[titleimg] \u5df2\u66ff\u6362 %ld \u6b21\u6807\u9898\u56fe\u8def\u5f84", last);
        }
    }
    return 0;
}
"""
assert anchor3 in s, 'tcover_watch not found'

module = u"""
/* ============================ \u6807\u7b7e\u6587\u5b57\uff08Text::OnEnable\uff09 ============================
 * \u90a3\u884c\u300c\u5f53\u524d\u865a\u5883\u6311\u6218\u4e66\u300d\u662f\u9884\u5236\u4f53\u91cc\u7684\u9759\u6001\u6587\u672c\uff0c\u8fd0\u884c\u671f\u4ece\u4e0d set_text\uff08\u5b9e\u6d4b\uff1a
 * set_text \u8c03\u7528 984 \u6b21\u3001\u6309\u5185\u5bb9\u5339\u914d 0 \u6b21\uff09\u3002\u53ea\u80fd\u5728\u5b83\u88ab\u542f\u7528\u90a3\u4e00\u523b\u62e6\u3002
 *
 * Text::OnEnable @ 0x06905CD0\uff1a
 *   40 53            push rbx
 *   48 83 EC 20      sub rsp, 0x20
 *   48 8B D9         mov rbx, rcx          <- 9 \u5b57\u8282\u5230\u6b64\u4e3a\u6b62
 *   E8 ...           call MaskableGraphic::OnEnable
 * \u524d 9 \u5b57\u8282\u88c5\u4e0d\u4e0b 14 \u5b57\u8282\u7684 FF25 \u7edd\u5bf9\u8df3\u8f6c\uff0c\u6240\u4ee5\u7528 E9 rel32 + 4\u00d790\uff0c
 * stub \u5fc5\u987b\u843d\u5728\u6a21\u5757 \u00b12GB \u5185\uff08alloc_near\uff09\u3002
 * trampoline \u53ea\u56de\u653e 9 \u5b57\u8282\uff0c\u518d\u8df3 0x6905CD9 \u90a3\u6761 call\uff08\u539f\u5730\u6267\u884c\uff0c\u4e0d\u88ab\u6211\u4eec\u7834\u574f\uff09\u3002
 *
 * \u547d\u4e2d\u540e\uff08\u5728\u539f OnEnable \u4e4b\u524d\uff09\uff1a
 *   [rcx+0xD8] = \u81ea\u5efa String\uff08\u5185\u5bb9\u6362\u6210 to\uff09
 *   [rcx+0x20..0x2C] = RGBA float\uff08Graphic::m_Color\uff0c\u89c1 get_color \u7684 [rdx+0x20]\uff09
 * \u968f\u540e\u539f OnEnable \u4f1a SetAllDirty\uff0c\u91cd\u5efa\u7f51\u683c\u65f6\u76f4\u63a5\u7528\u65b0\u6587\u672c + \u65b0\u989c\u8272\u3002
 */
#define RVA_LABEL_ENABLE  0x06905CD0
#define LEN_LABEL_ENABLE  9
static const BYTE ORIG_LABEL_ENABLE[LEN_LABEL_ENABLE] = {
    0x40, 0x53, 0x48, 0x83, 0xEC, 0x20, 0x48, 0x8B, 0xD9
};

/* \u5728 anchor \u00b12GB \u5185\u627e\u4e00\u9875\u53ef\u6267\u884c\u5185\u5b58\uff08E9 rel32 \u53ea\u80fd\u8df3 \u00b12GB\uff09 */
static BYTE *alloc_near(BYTE *anchor)
{
    ULONGLONG a = (ULONGLONG)(ULONG_PTR)anchor;
    LONGLONG  step;
    for (step = 0x00400000; step < 0x60000000LL; step += 0x00400000) {
        BYTE *q;
        q = (BYTE *)VirtualAlloc((LPVOID)(ULONG_PTR)(a + (ULONGLONG)step), 0x2000,
                                 MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
        if (q) return q;
        q = (BYTE *)VirtualAlloc((LPVOID)(ULONG_PTR)(a - (ULONGLONG)step), 0x2000,
                                 MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
        if (q) return q;
    }
    return NULL;
}

static BYTE *build_label_stub(BYTE *out, BYTE *tramp)
{
    BYTE *p = out;
    int   j_null, j_len, j_cmp, j_loop, j_copy;
    int   at_replay, at_hit, at_cmp_loop, at_copy_loop;
    int   flen = g_label_fromlen;
    int   n    = g_label_tolen;
    LONG  fbytes = (LONG)flen * 2;
    LONG  nbytes = (LONG)n * 2;
    ULONGLONG fromaddr = (ULONGLONG)(ULONG_PTR)g_label_from;
    ULONGLONG toaddr   = (ULONGLONG)(ULONG_PTR)g_label_to;
    ULONGLONG bufaddr  = (ULONGLONG)(ULONG_PTR)g_label_buf;

    /* rax = m_Text（Text + 0xD8） */
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

    /* ---- 命中 ---- */
    at_hit = (int)(p - out);
    p = emit_mov_r11(p, &g_label_hits);
    *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;        /* lock inc [r11] */

    /* 造 String：klass 直接取原串的 [rax] */
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

    /* 字色：Graphic::m_Color @ this+0x20 */
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

    at_replay = (int)(p - out);
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;                                  /* jmp rax */

    out[j_null + 1] = (BYTE)(at_replay - (j_null + 2));
    out[j_len  + 1] = (BYTE)(at_replay - (j_len  + 2));
    out[j_cmp  + 1] = (BYTE)(at_replay - (j_cmp  + 2));
    out[j_loop + 1] = (BYTE)(at_cmp_loop - (j_loop + 2));
    out[j_copy + 1] = (BYTE)(at_copy_loop - (j_copy + 2));
    (void)at_hit;
    return p;
}

static void apply_label(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_LABEL_ENABLE;
    BYTE *page, *stub, *tramp, *end, *tend;
    char  hs[256];

    hexdump(fn, LEN_LABEL_ENABLE, hs, sizeof(hs));
    logf("[label] Text::OnEnable %p 当前: %s", fn, hs);

    if (fn[0] == 0xE9) {
        logf("[label] 已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_LABEL_ENABLE, LEN_LABEL_ENABLE)) {
        logf("[label] !! 原始字节不匹配，拒绝打补丁");
        return;
    }
    page = alloc_near(fn);
    if (!page) {
        logf("[label] !! 模块 ±2GB 内找不到可用内存，跳过（E9 跳不过去）");
        return;
    }
    stub  = page;
    tramp = page + 0x400;

    end  = build_label_stub(stub, tramp);
    tend = emit_bytes(tramp, ORIG_LABEL_ENABLE, LEN_LABEL_ENABLE);
    *tend++ = 0xFF; *tend++ = 0x25;
    *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00;
    {
        ULONGLONG v = (ULONGLONG)(ULONG_PTR)((BYTE *)ua + RVA_LABEL_ENABLE + LEN_LABEL_ENABLE);
        memcpy(tend, &v, 8);
        tend += 8;
    }
    FlushInstructionCache(GetCurrentProcess(), page, (DWORD)(tend - page));

    {
        BYTE patch[LEN_LABEL_ENABLE];
        LONG rel = (LONG)(stub - (fn + 5));
        int  i;
        patch[0] = 0xE9;
        memcpy(patch + 1, &rel, 4);
        for (i = 5; i < LEN_LABEL_ENABLE; i++) patch[i] = 0x90;

        logf("[label] stub @ %p（%d 字节）  trampoline @ %p  rel=%ld",
             stub, (int)(end - stub), tramp, rel);

        if (!write_code(fn, patch, sizeof(patch))) {
            logf("[label] !! 写入口跳转失败 err=%lu", GetLastError());
            return;
        }
        if (memcmp(fn, patch, sizeof(patch)) == 0)
            logf("[label] OK  入口已接管（\\"%ls\\" -> \\"%ls\\"）", g_label_from, g_label_to);
        else
            logf("[label] !! 写入后回读不一致");
    }
}

static DWORD WINAPI label_watch(LPVOID param)
{
    LONG last = 0;
    (void)param;
    for (;;) {
        LONG now;
        Sleep(1500);
        now = InterlockedCompareExchange(&g_label_hits, 0, 0);
        if (now != last) {
            last = now;
            logf("[label] 已替换 %ld 次标签文字", last);
        }
    }
    return 0;
}
"""
s = s.replace(anchor3, anchor3 + module, 1)

# ---------------- 4) init 调用 ----------------
old_i = u"""    if (g_title_enabled) {
        apply_title(ua);"""
new_i = u"""    if (g_label_enabled) {
        apply_label(ua);
        CreateThread(NULL, 0, label_watch, NULL, 0, NULL);
    } else {
        logf("[label] \u672a\u542f\u7528\uff08ini [label] enabled=0\uff09");
    }

    if (g_title_enabled) {
        apply_title(ua);"""
assert old_i in s
s = s.replace(old_i, new_i, 1)

io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('patched ok')
