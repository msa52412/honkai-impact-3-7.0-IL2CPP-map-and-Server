/* 生成 OnEnable 版 stub 的机器码（仅做反汇编核对，不在真游戏里跑） */
#include <stdio.h>
#include "bh3bypass.c"

int main(void)
{
    static BYTE buf[0x400];
    BYTE *end;
    int   i;

    lstrcpyW(g_label_from, L"\u5f53\u524d\u865a\u5883\u6311\u6218\u4e66");
    g_label_fromlen = 7;
    g_group_align   = 3;
    {
        const char *cname = "HorizontalLayoutGroup";
        int k;
        for (k = 0; cname[k] && k < 39; k++) g_lg_w[k] = (WCHAR)cname[k];
        g_lg_wlen = k;
    }
    g_label_ua = (BYTE *)0x140000000ULL;
    { const char *hn = "Num"; int k; for (k = 0; hn[k] && k < 39; k++) g_hide_w[k] = (WCHAR)hn[k]; g_hide_wlen = k; }

    memset(buf, 0xCC, sizeof(buf));
    end = build_label_lg_stub(buf, (BYTE *)0x12345000ULL);
    printf("stub %d bytes\n", (int)(end - buf));
    for (i = 0; i < (int)(end - buf); i++) printf("%02x", buf[i]);
    printf("\n");
    return 0;
}
