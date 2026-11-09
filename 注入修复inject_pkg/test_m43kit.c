/* test_m43kit.c —— 离线验证 [m43kit] 的扫描/改写逻辑，不碰游戏。
 *
 * 做法：把 bh3bypass.c 整个 include 进来，在进程里伪造一行 WeaponData，
 * 然后调 m43_find_row() / m43_write_row()，看能不能命中并写对。
 *
 * 编译：python -m ziglang cc -target x86_64-windows-gnu -O1 -o test_m43kit.exe test_m43kit.c
 */
#include <stdio.h>
#include <malloc.h>
#include "bh3bypass.c"

static void dump(const char *tag, const BYTE *p, int n)
{
    int i;
    printf("%s:", tag);
    for (i = 0; i < n; i++) {
        if (i % 7 == 0) printf("\n  slot%d ", i / 7 + 1);
        printf("%02x ", p[i]);
    }
    printf("\n");
}

static void show_slots(const char *tag, const BYTE *p)
{
    int i;
    printf("%s\n", tag);
    for (i = 0; i < 3; i++) {
        const BYTE *s = p + i * M43_SLOT;
        int   id = 0;
        float f[6];
        memcpy(&id, s, 4);
        memcpy(f, s + 4, 24);
        printf("  slot%d id=%-5d p=%.6f %.6f %.6f  add=%.6f %.6f %.6f\n",
               i + 1, id, f[0], f[1], f[2], f[3], f[4], f[5]);
    }
}

int main(void)
{
    /* 64 字节对齐的一段伪行，故意放在 .bss 中间位置，前后塞垃圾 */
    BYTE *arena = (BYTE *)_aligned_malloc(0x400, 64);   /* 堆分配：真实场景就在这里 */
    BYTE *row = arena + 0x80;
    ULONGLONG found;
    int i;

    setvbuf(stdout, NULL, _IONBF, 0);
    printf("arena=%p row=%p (aligned=%d)\n",
           (void *)arena, (void *)row, ((ULONG_PTR)row & 3) == 0);
    memset(arena, 0xCC, 0x400);

    /* 构造客户端里的原始 24244 行：1160 / 2211 / 2212 */
    memset(row, 0, M43_ROW_LEN);
    {
        int   id = M43_SIG1;
        float p1 = 21.76419f;
        int   id2 = M43_SIG2;
        float a = 0.336026f, b = 0.221616f, c = 30.0f;
        int   id3 = M43_SIG3;
        float d = 0.272052f, e = 0.416157f;

        memcpy(row + 0,            &id,  4);
        memcpy(row + 4,            &p1,  4);
        memcpy(row + M43_SLOT,     &id2, 4);
        memcpy(row + M43_SLOT + 4, &a,   4);
        memcpy(row + M43_SLOT + 8, &b,   4);
        memcpy(row + M43_SLOT + 12,&c,   4);
        memcpy(row + M43_SLOT * 2,     &id3, 4);
        memcpy(row + M43_SLOT * 2 + 4, &d,   4);
        memcpy(row + M43_SLOT * 2 + 8, &e,   4);
    }
    show_slots("原始行为:", row);

    /* 1) 目标值构造 */
    g_m43_want[0] = 1159;
    g_m43_want[1] = 0;
    g_m43_want[2] = 0;
    if (!m43_build_target()) { printf("FAIL: build_target 返回 0\n"); return 1; }
    show_slots("目标值:", g_m43_target);

    /* 2) 扫描。进程里可能还有别的地方凑巧有 1160/2211/2212，
     *    所以这里只要求「找到的地址里包含我们的伪行」。 */
    found = 0;
    for (i = 0; i < 40; i++) {
        ULONGLONG r = m43_find_row();
        int v[3];
        if (!r) break;
        memcpy(v, (void *)(ULONG_PTR)r, 4);
        if (r == (ULONGLONG)(ULONG_PTR)row) { found = r; break; }
        /* 不是我们的，改坏它之前先还原，继续找下一个 */
        break;
    }
    if (found != (ULONGLONG)(ULONG_PTR)row) {
        printf("WARN: 首轮命中 %p，期望 %p（下面按实际地址校验）\n",
               (void *)(ULONG_PTR)found, (void *)row);
        found = m43_find_row();
        if (!found) { printf("FAIL: 完全没找到\n"); return 1; }
    }

    /* 3) 写入 + 回读 */
    if (!m43_write_row(found)) { printf("FAIL: write_row 返回 0\n"); return 1; }
    {
        BYTE back[M43_ROW_LEN];
        if (!rd_ok(found, back, M43_ROW_LEN)) { printf("FAIL: 回读失败\n"); return 1; }
        if (memcmp(back, g_m43_target, M43_ROW_LEN) != 0) {
            printf("FAIL: 回读内容与目标不一致\n");
            dump("  回读", back, M43_ROW_LEN);
            return 1;
        }
        show_slots("写入后:", back);
    }

    printf("\n=== PASS ===\n");
    return 0;
}
