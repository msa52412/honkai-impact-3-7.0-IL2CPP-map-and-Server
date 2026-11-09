/* 离线校验 build_label_stub 机器码 */
#include <stdio.h>
#include "bh3bypass.c"

int main(void)
{
    static BYTE buf[0x400];
    BYTE *end;
    int   i;

    lstrcpyW(g_label_from, L"当前虚境挑战书");
    g_label_fromlen = 7;
    lstrcpyW(g_label_to, L"迪拉克之海 红莲");
    g_label_tolen = 8;
    g_label_col[0] = 1.0f;
    g_label_col[1] = 0.3019608f;
    g_label_col[2] = 0.3019608f;
    g_label_col[3] = 1.0f;
    g_label_shift = 0.0f;
    g_group_align = 3;
    { const char *hn = "Num"; int k; for (k = 0; hn[k]; k++) g_hide_w[k] = (WCHAR)hn[k]; g_hide_w[k] = 0; g_hide_wlen = k; }
    { const char *ln = "HorizontalLayoutGroup"; int k; for (k = 0; ln[k]; k++) g_lg_w[k] = (WCHAR)ln[k]; g_lg_w[k] = 0; g_lg_wlen = k; }
    g_label_ua = (BYTE *)0x140000000ULL;

    memset(buf, 0xCC, sizeof(buf));
    end = build_label_stub(buf, (BYTE *)0x12345000ULL);
    printf("stub %d bytes\n", (int)(end - buf));
    for (i = 0; i < (int)(end - buf); i++) printf("%02x", buf[i]);
    printf("\n");
    return 0;
}
