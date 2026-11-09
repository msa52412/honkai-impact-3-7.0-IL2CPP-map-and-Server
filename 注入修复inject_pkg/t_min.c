/* 最小验证：复刻 OnPopulateMesh 的入口 21 字节 + 复原栈帧返回 */
#include <windows.h>
#include <stdio.h>

static const unsigned char ORIG[23] = {
    0x48,0x89,0x54,0x24,0x10,
    0x55,0x53,0x57,
    0x48,0x8D,0xAC,0x24,0x60,0xFF,0xFF,0xFF,
    0x48,0x81,0xEC,0xA0,0x01,0x00,0x00
};

int main(void)
{
    BYTE *page = (BYTE *)VirtualAlloc(NULL, 0x2000, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
    BYTE *fn = page, *tail = page + 0x100;
    BYTE *p, *t;
    LONG rel;
    typedef void (*FN)(void *, void *);

    if (!page) { printf("alloc fail\n"); return 2; }

    memcpy(fn, ORIG, 23);
    rel = (LONG)(tail - (fn + 23 + 5));
    fn[23] = 0xE9;
    memcpy(fn + 24, &rel, 4);

    t = tail;
    *t++ = 0x48; *t++ = 0x81; *t++ = 0xC4; *t++ = 0xA0; *t++ = 0x01; *t++ = 0x00; *t++ = 0x00; /* add rsp,0x1a0 */
    *t++ = 0x5F;  /* pop rdi */
    *t++ = 0x5B;  /* pop rbx */
    *t++ = 0x5D;  /* pop rbp */
    *t++ = 0xC3;  /* ret */

    (void)p;
    printf("call...\n"); fflush(stdout);
    {
        FN f = (FN)(void *)fn;
        f((void *)0x1234, (void *)0x5678);
    }
    printf("returned OK\n");
    return 0;
}
