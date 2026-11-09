/* 严格复现「打补丁 + trampoline 回放 21 字节」：
 * 对比未打补丁 / 打补丁两次调用，rdx 与 rsp 必须完全一致。
 */
#include <stdio.h>
#include "bh3bypass.c"

static ULONGLONG g_seen_rdx;
static ULONGLONG g_seen_rsp;

int main(void)
{
    static BYTE textobj[0x200];
    static BYTE fake_str[0x40];
    BYTE *page, *fn, *tail, *stub, *tramp;
    BYTE  orig[21];
    ULONGLONG rsp_plain = 0, rdx_plain = 0;
    int   ok = 1;

    page  = (BYTE *)VirtualAlloc(NULL, 0x2000, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
    if (!page) { printf("VirtualAlloc failed\n"); return 2; }
    fn    = page + 0x000;
    tail  = page + 0x100;
    stub  = page + 0x300;
    tramp = page + 0x600;

    *(ULONGLONG *)(fake_str + 0x00) = 0x1111111111111111ULL;
    *(int *)(fake_str + 0x10) = 7;
    {
        unsigned short *b = (unsigned short *)(fake_str + 0x14);
        b[0]=0x5F53; b[1]=0x524D; b[2]=0x865A; b[3]=0x5883; b[4]=0x6311; b[5]=0x6218; b[6]=0x4E66;
    }
    *(ULONGLONG *)(textobj + 0xD8) = (ULONGLONG)(ULONG_PTR)fake_str;

    lstrcpyW(g_label_from, L"\u5f53\u524d\u865a\u5883\u6311\u6218\u4e66");
    g_label_fromlen = 7;
    lstrcpyW(g_label_to, L"\u8fea\u62c9\u514b\u4e4b\u6d77 \u7ea2\u83b2");
    g_label_tolen = 8;

    memcpy(orig, ORIG_LABEL_ENABLE, 21);
    memcpy(fn, orig, 21);
    {
        LONG rel = (LONG)(tail - (fn + 21 + 5));
        fn[21] = 0xE9;
        memcpy(fn + 22, &rel, 4);
    }
    {
        BYTE *p = tail;
        *p++ = 0x48; *p++ = 0xB8; *(ULONGLONG *)p = (ULONGLONG)(ULONG_PTR)&g_seen_rdx; p += 8;
        *p++ = 0x48; *p++ = 0x89; *p++ = 0x10;
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xE0;
        *p++ = 0x48; *p++ = 0xBB; *(ULONGLONG *)p = (ULONGLONG)(ULONG_PTR)&g_seen_rsp; p += 8;
        *p++ = 0x48; *p++ = 0x89; *p++ = 0x03;
        *p++ = 0x48; *p++ = 0x81; *p++ = 0xC4; *p++ = 0xA0; *p++ = 0x01; *p++ = 0x00; *p++ = 0x00;
        *p++ = 0x5F; *p++ = 0x5B; *p++ = 0x5D; *p++ = 0xC3;
    }

    printf("baseline...\n"); fflush(stdout);
    {
        typedef void (*FN)(void *, void *);
        FN f = (FN)(void *)fn;
        f(textobj, (void *)0x1122334455667788ULL);
    }
    rsp_plain = g_seen_rsp;
    rdx_plain = g_seen_rdx;
    printf("  rdx=%llx rsp=%llx\n",
           (unsigned long long)rdx_plain, (unsigned long long)rsp_plain);

    build_label_stub(stub, tramp);
    {
        BYTE *t = emit_bytes(tramp, orig, 21);
        *t++ = 0xFF; *t++ = 0x25; *t++ = 0; *t++ = 0; *t++ = 0; *t++ = 0;
        *(ULONGLONG *)t = (ULONGLONG)(ULONG_PTR)tail;
    }
    {
        BYTE patch[21];
        int i;
        patch[0] = 0xFF; patch[1] = 0x25;
        patch[2] = patch[3] = patch[4] = patch[5] = 0;
        *(ULONGLONG *)(patch + 6) = (ULONGLONG)(ULONG_PTR)stub;
        for (i = 14; i < 21; i++) patch[i] = 0x90;
        memcpy(fn, patch, 21);
    }

    printf("patched...\n"); fflush(stdout);
    g_seen_rdx = 0; g_seen_rsp = 0;
    {
        typedef void (*FN)(void *, void *);
        FN f = (FN)(void *)fn;
        f(textobj, (void *)0x1122334455667788ULL);
    }
    printf("  rdx=%llx rsp=%llx\n",
           (unsigned long long)g_seen_rdx, (unsigned long long)g_seen_rsp);

    if (g_seen_rdx != rdx_plain) { printf("!! rdx changed\n"); ok = 0; }
    if (g_seen_rsp != rsp_plain) { printf("!! rsp changed\n"); ok = 0; }
    printf("%s\n", ok ? "=== PASS ===" : "=== FAIL ===");
    return ok ? 0 : 1;
}
