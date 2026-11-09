/* 离线校验 build_tcover_stub 生成的机器码（不碰游戏） */
#include "bh3bypass.c"

int main(void)
{
    BYTE buf[0x400];
    BYTE *end;
    int i;

    g_tcover.id = 11;
    g_tcover_wlen = 37;
    lstrcpyW(g_tcover_w, L"SpriteOutput/WorldMap/OWEndless/Cover_Title8");

    end = build_tcover_stub(buf, (BYTE *)0x12345000);
    printf("stub %d bytes\n", (int)(end - buf));
    for (i = 0; i < (int)(end - buf); i++) printf("%02x", buf[i]);
    printf("\n");
    return 0;
}
