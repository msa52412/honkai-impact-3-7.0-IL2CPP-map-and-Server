/* 在真实进程里跑一遍 build_label_stub 生成的机器码：
 * 造一个假的 Text 对象 + 假 String，调用 stub，验证 m_Text / m_Color 被改、trampoline 被跳回。
 */
#include <stdio.h>
#include "bh3bypass.c"

static volatile int g_resumed = 0;

/* 假的"原函数后半段"：stub 回放 21 字节后应该跳到这里 */
static void resume_fn(void)
{
    g_resumed++;
}

int main(void)
{
    static BYTE *page;
    static BYTE  textobj[0x200];
    static BYTE  fake_str[0x40];
    static BYTE  buf[0x400];
    BYTE *end, *stub, *tramp;
    int   i, ok = 1;
    unsigned short *body;

    /* 假 String：klass / monitor / len=7 / 正文 */
    *(ULONGLONG *)(fake_str + 0x00) = 0x1111111111111111ULL;   /* klass 占位 */
    *(ULONGLONG *)(fake_str + 0x08) = 0;
    *(int *)(fake_str + 0x10) = 7;
    body = (unsigned short *)(fake_str + 0x14);
    body[0] = 0x5F53; body[1] = 0x524D; body[2] = 0x865A; body[3] = 0x5883;
    body[4] = 0x6311; body[5] = 0x6218; body[6] = 0x4E66;     /* 当前虚境挑战书 */

    /* 假 Text：m_Color(+0x20) / m_Text(+0xD8) */
    *(float *)(textobj + 0x20) = 1.0f;
    *(float *)(textobj + 0x24) = 1.0f;
    *(float *)(textobj + 0x28) = 1.0f;
    *(float *)(textobj + 0x2C) = 1.0f;
    *(ULONGLONG *)(textobj + 0xD8) = (ULONGLONG)(ULONG_PTR)fake_str;

    lstrcpyW(g_label_from, L"当前虚境挑战书");
    g_label_fromlen = 7;
    lstrcpyW(g_label_to, L"迪拉克之海 红莲");
    g_label_tolen = 8;
    g_label_col[0] = 1.0f;
    g_label_col[1] = 0.3019608f;
    g_label_col[2] = 0.3019608f;
    g_label_col[3] = 1.0f;

    page  = (BYTE *)VirtualAlloc(NULL, 0x2000, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
    if (!page) { printf("VirtualAlloc 失败\n"); return 2; }
    stub  = page;
    tramp = page + 0x400;
    end   = build_label_stub(stub, tramp);

    /* trampoline 里放：nop;nop...; jmp resume_fn（这里只验 stub 行为，不验 21 字节回放） */
    {
        BYTE *t = tramp;
        *t++ = 0xFF; *t++ = 0x25; *t++ = 0; *t++ = 0; *t++ = 0; *t++ = 0;
        *(ULONGLONG *)t = (ULONGLONG)(ULONG_PTR)resume_fn;
    }

    printf("stub=%d bytes  text=%p\n", (int)(end - stub), (void *)textobj);
    {
        typedef void (*FN)(void *, void *);
        FN f = (FN)(void *)stub;
        f(textobj, (void *)0xDEADBEEF);
    }

    printf("resumed=%d\n", g_resumed);
    if (!g_resumed) { printf("!! trampoline 没跳回\n"); ok = 0; }

    {
        ULONGLONG p = *(ULONGLONG *)(textobj + 0xD8);
        printf("m_Text -> %p  (buf=%p) %s\n", (void *)p, (void *)g_label_buf,
               p == (ULONGLONG)(ULONG_PTR)g_label_buf ? "OK" : "!! 没换成自建串");
        if (p != (ULONGLONG)(ULONG_PTR)g_label_buf) ok = 0;
        printf("  len=%d  body=", *(int *)((BYTE *)p + 0x10));
        for (i = 0; i < *(int *)((BYTE *)p + 0x10); i++)
            printf("%04X ", *(unsigned short *)((BYTE *)p + 0x14 + i * 2));
        printf("\n");
    }
    printf("m_Color = %.3f %.3f %.3f %.3f\n",
           *(float *)(textobj + 0x20), *(float *)(textobj + 0x24),
           *(float *)(textobj + 0x28), *(float *)(textobj + 0x2C));

    /* 不匹配的情况：换成别的长度，应该原样跳回、且不改任何字段 */
    *(int *)(fake_str + 0x10) = 6;
    *(ULONGLONG *)(textobj + 0xD8) = (ULONGLONG)(ULONG_PTR)fake_str;
    *(float *)(textobj + 0x20) = 1.0f;
    {
        typedef void (*FN)(void *, void *);
        FN f = (FN)(void *)stub;
        f(textobj, (void *)0xDEADBEEF);
    }
    printf("不匹配: m_Text=%p color=%.3f resumed=%d %s\n",
           (void *)*(ULONGLONG *)(textobj + 0xD8), *(float *)(textobj + 0x20), g_resumed,
           (*(ULONGLONG *)(textobj + 0xD8) == (ULONGLONG)(ULONG_PTR)fake_str &&
            *(float *)(textobj + 0x20) == 1.0f) ? "OK" : "!! 误改");

    printf("%s\n", ok ? "=== PASS ===" : "=== FAIL ===");
    return ok ? 0 : 1;
}
