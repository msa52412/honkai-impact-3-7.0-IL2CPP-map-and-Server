/* 测试壳：把 bh3bypass.c 里的 build_cover_stub 跑一遍，dump 机器码供反汇编核对。
 * 编译： python -m ziglang cc -O1 -o test_cover.exe test_cover.c
 * 只在开发时用，不进游戏。 */
#include <stdio.h>
#include "bh3bypass.c"

int main(void)
{
    static BYTE buf[0x400];
    BYTE *end;
    int   i;
    const char *path = "SpriteOutput/WorldMap/OWEndless/Cover_8";

    g_cover.id = 11;
    g_cover_wlen = MultiByteToWideChar(CP_ACP, 0, path, -1, g_cover_w, 256) - 1;

    memset(buf, 0xCC, sizeof(buf));
    end = build_cover_stub(buf, (BYTE *)0x140000000ULL, (BYTE *)0x12345000ULL);
    printf("stub %d bytes  wlen=%d\n", (int)(end - buf), g_cover_wlen);
    for (i = 0; i < (int)(end - buf); i++)
        printf("%02x", buf[i]);
    printf("\n");
    return 0;
}
