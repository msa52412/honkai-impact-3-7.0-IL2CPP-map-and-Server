/* 测试壳：跑 build_title_stub，dump 机器码供反汇编核对。 */
#include <stdio.h>
#include "bh3bypass.c"

int main(void)
{
    static BYTE buf[0x400];
    BYTE *end;
    int   i;

    lstrcpyW(g_title_from, L"当前虚境挑战书");
    lstrcpyW(g_title_to,   L"迪拉克之海 红莲");
    g_title_fromlen = 7;
    g_title_tolen   = 8;
    g_title_col[0] = 1.0f;
    g_title_col[1] = 0.3019608f;
    g_title_col[2] = 0.3019608f;
    g_title_col[3] = 1.0f;
    g_title_enabled = 1;

    memset(buf, 0xCC, sizeof(buf));
    end = build_title_stub(buf, (BYTE *)0x140000000ULL, (BYTE *)0x12345000ULL);
    printf("stub %d bytes\n", (int)(end - buf));
    for (i = 0; i < (int)(end - buf); i++) printf("%02x", buf[i]);
    printf("\n");
    return 0;
}
