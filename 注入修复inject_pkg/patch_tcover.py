# -*- coding: utf-8 -*-
"""把 [titleimg] 模块插进 bh3bypass.c（一次性脚本）"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

if 'RVA_TCOVER_GET' in s:
    print('already patched')
    raise SystemExit(0)

anchor = u"""static DWORD WINAPI cover_watch(LPVOID param)
{
    LONG last = 0;
    (void)param;
    for (;;) {
        LONG now;
        Sleep(1500);
        now = InterlockedCompareExchange(&g_cover.hits, 0, 0);
        if (now != last) {
            last = now;
            logf("[cover] \u5df2\u66ff\u6362 %ld \u6b21\u5c01\u9762\u8def\u5f84", last);
        }
    }
    return 0;
}
"""
assert anchor in s, 'cover_watch not found'

module = u"""
/* ============================ \u6807\u9898\u7f8e\u672f\u5b57\uff08\u56fe\u7247\uff09 ============================
 * \u5b9e\u8bc1\uff1a\u5361\u9762\u6807\u9898\u4e0d\u662f Text\uff0c\u662f Task_N/Cover/TitleImg \u8fd9\u4e2a Image\uff0c
 * \u8d34\u56fe Cover_Title6\uff08\u4e07\u8c61\u865a\u5883\uff09\u6765\u81ea\u9884\u5236\u4f53\u9ed8\u8ba4\u503c \u2014\u2014 \u6570\u636e\u884c\u7b2c 3 \u5217\u662f\u7a7a\u4e32\uff0c
 * \u6240\u4ee5 SetupTitleImage(0x2CFE710) \u91cc IsNullOrEmpty \u76f4\u63a5 return\uff0c\u56fe\u7247\u4fdd\u6301\u9ed8\u8ba4\u3002
 *
 *   rcx = [this+0xC8]            ; ContainerData
 *   call 0xA52620                ; \u7b2c 3 \u5217 getter -> \u6807\u9898\u56fe\u8def\u5f84
 *   if (IsNullOrEmpty(path)) return;
 *   SetImageAsync(this, [this+0x48], path)
 *
 * \u6240\u4ee5\u6302 0xA52620 \u51fa\u53e3\uff1a\u547d\u4e2d\u76ee\u6807\u884c\u5c31\u628a\u8fd4\u56de\u503c\u6362\u6210\u81ea\u5df1\u7684\u8def\u5f84\u4e32\uff0c
 * \u539f\u51fd\u6570\u7684 IsNullOrEmpty \u4f1a\u8ba4\u4e3a\u300c\u6709\u503c\u300d\uff0c\u63a5\u7740 SetImageAsync \u5c31\u6362\u6210\u65b0\u56fe\u3002
 * \u5165\u53e3 14 \u5b57\u8282\uff1a40 53 / 48 83 EC 20 / 48 8B D9 / B9 8A 0C 00 00
 */
#define RVA_TCOVER_GET   0x00A52620
#define LEN_TCOVER_GET   14
static const BYTE ORIG_TCOVER_GET[LEN_TCOVER_GET] = {
    0x40, 0x53, 0x48, 0x83, 0xEC, 0x20, 0x48, 0x8B, 0xD9,
    0xB9, 0x8A, 0x0C, 0x00, 0x00
};

static BYTE *build_tcover_stub(BYTE *out, BYTE *tramp)
{
    BYTE *p = out;
    int   j_null_meta, j_not_id, j_hit, j_klass, j_ret0, j_loop;
    int   at_replay, at_hit, at_have, at_ret0, at_loop;
    LONG  id = g_tcover.id;
    int   n  = g_tcover_wlen;
    LONG  bytes = (LONG)n * 2;
    ULONGLONG fromaddr = (ULONGLONG)(ULONG_PTR)g_tcover_w;
    ULONGLONG bufaddr  = (ULONGLONG)(ULONG_PTR)g_tcover_buf;

    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x41; *p++ = OFF_CD_META;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
    j_null_meta = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    *p++ = 0x8B; *p++ = 0x40; *p++ = OFF_META_ID;
    *p++ = 0x3D;
    memcpy(p, &id, 4); p += 4;
    j_not_id = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    j_hit = (int)(p - out);
    p = emit_mov_rax(p, NULL);
    *p++ = 0xFF; *p++ = 0xE0;

    at_replay = (int)(p - out);
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;

    at_hit = (int)(p - out);
    *(ULONGLONG *)(out + j_hit + 2) = (ULONGLONG)(ULONG_PTR)(out + at_hit);

    /* \u8ba1\u6570 */
    p = emit_mov_r11(p, &g_tcover.hits);
    *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;

    /* \u5148\u8dd1\u539f\u51fd\u6570\uff08rcx \u4ecd\u662f ContainerData\uff09\uff0c\u62ff\u5b83\u7684\u8fd4\u56de\u503c\u5f53\u540e\u5907 klass */
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x28;
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xD0;
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xC4; *p++ = 0x28;

    *p++ = 0x49; *p++ = 0x89; *p++ = 0xC2;               /* mov r10, rax */

    p = emit_mov_r11(p, &g_tcover_buf);                  /* r11 = buf */

    p = emit_mov_rax(p, &g_str_klass);
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x00;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
    j_klass = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    *p++ = 0x4D; *p++ = 0x85; *p++ = 0xD2;               /* test r10,r10 */
    j_ret0 = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
    *p++ = 0x49; *p++ = 0x8B; *p++ = 0x02;               /* mov rax,[r10] */

    at_have = (int)(p - out);
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;               /* mov [r11], rax */
    *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;
    { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
    *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;
    memcpy(p, &n, 4); p += 4;

    *p++ = 0x48; *p++ = 0xBA; memcpy(p, &fromaddr, 8); p += 8;
    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &bytes, 4); p += 4;

    at_loop = (int)(p - out);
    *p++ = 0x8A; *p++ = 0x02;
    *p++ = 0x41; *p++ = 0x88; *p++ = 0x43; *p++ = 0x14;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
    *p++ = 0x48; *p++ = 0xFF; *p++ = 0xC2;
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
    j_loop = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    p = emit_mov_rax(p, &g_tcover_buf);
    *p++ = 0xC3;

    at_ret0 = (int)(p - out);
    *p++ = 0x33; *p++ = 0xC0;
    *p++ = 0xC3;

    out[j_null_meta + 1] = (BYTE)(at_replay - (j_null_meta + 2));
    out[j_not_id    + 1] = (BYTE)(at_replay - (j_not_id   + 2));
    out[j_klass     + 1] = (BYTE)(at_have   - (j_klass    + 2));
    out[j_ret0      + 1] = (BYTE)(at_ret0   - (j_ret0     + 2));
    out[j_loop      + 1] = (BYTE)(at_loop   - (j_loop     + 2));
    return p;
}

static void apply_tcover(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_TCOVER_GET;
    BYTE *stub, *end;
    BYTE  patch[14];
    char  hs[256];

    hexdump(fn, LEN_TCOVER_GET, hs, sizeof(hs));
    logf("[titleimg] \u6807\u9898\u56fe getter %p \u5f53\u524d: %s", fn, hs);

    if (fn[0] == 0xFF && fn[1] == 0x25) {
        logf("[titleimg] \u5df2\u7ecf\u662f\u8865\u4e01\u72b6\u6001\uff0c\u8df3\u8fc7");
        return;
    }
    if (!is_original(fn, ORIG_TCOVER_GET, LEN_TCOVER_GET)) {
        logf("[titleimg] !! \u539f\u59cb\u5b57\u8282\u4e0d\u5339\u914d\uff0c\u62d2\u7edd\u6253\u8865\u4e01");
        return;
    }
    stub = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!stub) {
        logf("[titleimg] !! VirtualAlloc \u5931\u8d25 err=%lu", GetLastError());
        return;
    }
    {
        BYTE *tramp = stub + 0x200;
        BYTE *tend;
        end  = build_tcover_stub(stub, tramp);
        tend = emit_bytes(tramp, ORIG_TCOVER_GET, LEN_TCOVER_GET);
        *tend++ = 0xFF; *tend++ = 0x25;
        *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00;
        {
            ULONGLONG v = (ULONGLONG)(ULONG_PTR)((BYTE *)ua + RVA_TCOVER_GET + LEN_TCOVER_GET);
            memcpy(tend, &v, 8);
            tend += 8;
        }
        FlushInstructionCache(GetCurrentProcess(), stub, (DWORD)(end - stub));
        FlushInstructionCache(GetCurrentProcess(), tramp, (DWORD)(tend - tramp));
        logf("[titleimg] stub @ %p\uff08%d \u5b57\u8282\uff09  trampoline @ %p\uff08%d \u5b57\u8282\uff09",
             stub, (int)(end - stub), tramp, (int)(tend - tramp));
    }

    patch[0] = 0xFF;
    patch[1] = 0x25;
    patch[2] = patch[3] = patch[4] = patch[5] = 0x00;
    *(ULONGLONG *)(patch + 6) = (ULONGLONG)(ULONG_PTR)stub;

    if (!write_code(fn, patch, sizeof(patch))) {
        logf("[titleimg] !! \u5199\u5165\u53e3\u8df3\u8f6c\u5931\u8d25 err=%lu", GetLastError());
        return;
    }
    if (memcmp(fn, patch, sizeof(patch)) == 0)
        logf("[titleimg] OK  \u5165\u53e3\u5df2\u63a5\u7ba1\uff08ExcelMeta.ID=%ld \u7684\u6807\u9898\u56fe\u6362\u6210 ini \u8def\u5f84\uff09",
             g_tcover.id);
    else
        logf("[titleimg] !! \u5199\u5165\u540e\u56de\u8bfb\u4e0d\u4e00\u81f4");
}

static DWORD WINAPI tcover_watch(LPVOID param)
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

s = s.replace(anchor, anchor + module, 1)

old_t = u"""    /* test rdx, rdx */
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xD2;
    j_test = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
"""
new_t = u"""    /* test rdx, rdx */
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xD2;
    j_test = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    /* \u987a\u624b\u8bb0\u4e0b\u771f\u5b9e String \u7684 klass \u2014\u2014 \u6807\u9898\u56fe\u90a3\u4e00\u8def\u81ea\u5efa String \u8981\u7528 */
    p = emit_mov_r11(p, &g_str_klass);
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x02;
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;
"""
assert old_t in s, 'title stub anchor not found'
s = s.replace(old_t, new_t, 1)

old_i = u"""    if (g_title_enabled) {
        apply_title(ua);"""
new_i = u"""    if (g_tcover_enabled) {
        apply_tcover(ua);
        CreateThread(NULL, 0, tcover_watch, NULL, 0, NULL);
    } else {
        logf("[titleimg] \u672a\u542f\u7528\uff08ini [titleimg] enabled=0\uff09");
    }

    if (g_title_enabled) {
        apply_title(ua);"""
assert old_i in s, 'init anchor not found'
s = s.replace(old_i, new_i, 1)

io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('patched ok')
