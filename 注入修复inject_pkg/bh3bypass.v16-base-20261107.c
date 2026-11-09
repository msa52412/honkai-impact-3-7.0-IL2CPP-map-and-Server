/*
 * bh3bypass.dll —— 把 UserAssembly.dll 里的 ASB 资源校验函数改成「恒为真 / 恒不重建」
 *
 * ===========================================================================
 *  2026-11-07 精简版
 *  之前 v13~v15 期间堆了一整套「迪拉克之海额外得分 / 倒计时」实验补丁
 *  （owboss / ebtime / begin / smode / sle / dae），全部移除。
 *  原因：那条线（LevelScoreManager 的 extraScore 倒计时）走不通 ——
 *        StartAutoChange 的真实调用方在 Lua，C# 侧零调用点；
 *        强行 hook 只能改参数，改不了「根本没人调」这个事实。
 *  完整历史版本见 bh3bypass.full-20261107.c。
 *
 *  本文件现在只做一件事：让客户端信任本地资源包。
 * ===========================================================================
 *
 * 思路来源：原神 5.4 天机端的 hk4echeckbypass.dll / rsapatch.dll ——
 *           不去伪造数据，而是把「校验函数」的返回值改成成功。
 *
 * 为什么这次可行（前几轮不可行的原因已排除）：
 *   1. 校验函数全在 UserAssembly.dll（il2cpp 段），不是 BH3Base.dll。
 *      BH3Base.dll 是加壳启动器（只导入 LoadLibraryA），改不动也不用改。
 *   2. UserAssembly.dll 由 UnityPlayer 在引擎起来之后才加载，
 *      而我们的 version.dll 是 UnityPlayer 的依赖 —— 注入点比它早得多。
 *   3. 运行时根本不读 Blocks.xmf / BlockMeta.xmf（10 万条文件访问实测 0 命中），
 *      所以不需要伪造清单文件，只需要让「判定」通过。
 *
 * 六处补丁（RVA 已逐字节核对，全部验证生效）：
 *
 *   NeedRefreshMemoryMappedData          RVA 0x02FAD6D0
 *       InitMemoryMappedData 里 `if (NeedRefreshMemoryMappedData()) { 重建 }`。
 *       恒返回 false => 客户端永远信任现成的 asb.dat，不重建、不覆盖我们的改动。
 *
 *   NeedToVerifyBlocks                   RVA 0x02FADA80
 *       块校验总开关。恒 false => 永不进入 VerifyBlocks 循环。
 *
 *   NeedDownloadPatchX                   RVA 0x02FAD520
 *   NeedShowDownloadResourceDialog       RVA 0x02753570
 *   NeedDownloadAsbX                     RVA 0x02FAD470
 *       三个「要求下载」闸门，恒 false => 弹窗路径全部失效。
 *
 *   OnCheckStreamingAssetBundleComplete  RVA 0x01F1A600
 *       void f(this, bool success)；success 在 dl。
 *       入口前插 `mov dl,1` => 永远走成功分支。
 *
 * ★ 绝对不要加的补丁
 *   GetDownloadBlockSize (RVA 0x2753210) —— 恒返回 0 会让 DownloadResources 走
 *   `==0` 分支跳过下载管理器初始化（[this+0xB8..0xD8] 五个字段、[0xE8]/[0xEA]
 *   就绪标志全部保持 null/0），进主场景后某系统对空管理器做虚调用 →
 *   unityplayer `call [rax+0x48]` AV 崩溃。（2026-11-04 07:30 实测踩过。）
 *   弹窗压制由 needshowdlg 独立完成，块大小保持真实值让初始化分支正常走。
 *
 * 注意：
 *   * 这是 inline patch（改机器码），不是 IAT hook。ACE 是否会查 .text 完整性未知，
 *     所以每处都先比对原始字节，不匹配就拒绝动手。
 *   * 只改进程内存，磁盘上的 UserAssembly.dll 一个字节都不动。
 *
 * 编译：
 *   python -m ziglang cc -target x86_64-windows-gnu -shared -O2 -o bh3bypass.dll bh3bypass.c
 */
#include <windows.h>
#include <string.h>
#include <stdio.h>
#include <stdarg.h>

/* ---------------- 目标定义 -------------------------------------------------- */

/* NeedRefreshMemoryMappedData @ RVA 0x02FAD6D0
 *   48 89 5c 24 18    mov  [rsp+0x18], rbx
 *   55                push rbp
 *   48 8d 6c 24 a9    lea  rbp, [rsp-0x57]
 *   48 81 ec 90 00 00 00  sub rsp, 0x90
 * 指令边界在 5 / 6 / 11 / 18 => 取 18 字节整体覆盖
 */
#define RVA_NOREFRESH   0x02FAD6D0
#define LEN_NOREFRESH   18
static const BYTE ORIG_NOREFRESH[LEN_NOREFRESH] = {
    0x48, 0x89, 0x5c, 0x24, 0x18, 0x55, 0x48, 0x8d, 0x6c, 0x24, 0xa9,
    0x48, 0x81, 0xec, 0x90, 0x00, 0x00, 0x00
};

/* OnCheckStreamingAssetBundleComplete @ RVA 0x01F1A600
 *   48 89 5c 24 08    mov  [rsp+8], rbx
 *   57                push rdi
 *   48 83 ec 30       sub  rsp, 0x30
 *   48 8b d9          mov  rbx, rcx
 *   0f b6 fa          movzx edi, dl        <- success 在 dl
 * 指令边界正好 16
 */
#define RVA_ONCHECK     0x01F1A600
#define LEN_ONCHECK     16
static const BYTE ORIG_ONCHECK[LEN_ONCHECK] = {
    0x48, 0x89, 0x5c, 0x24, 0x08, 0x57, 0x48, 0x83, 0xec, 0x30,
    0x48, 0x8b, 0xd9, 0x0f, 0xb6, 0xfa
};

/* NeedToVerifyBlocks @ RVA 0x02FADA80 —— 「要不要做块校验」的总开关
 *   40 53             push rbx
 *   48 83 ec 20       sub rsp, 0x20
 *   48 8b d9          mov  rbx, rcx
 *   33 c0             xor  eax, eax
 *   指令边界在 2 / 6 / 9 / 11 => 取前 6 字节整体覆盖
 * 恒返回 false => 永不进入 VerifyBlocks 循环 => 不会因为包大小/内容不符要下载。
 */
#define RVA_NEEDVERIFY  0x02FADA80
#define LEN_NEEDVERIFY  6
static const BYTE ORIG_NEEDVERIFY[LEN_NEEDVERIFY] = {
    0x40, 0x53, 0x48, 0x83, 0xec, 0x20
};

/* 「要求下载」闸门三补丁（11-04 定稿）：
 * NeedDownloadPatchX / NeedShowDownloadResourceDialog / NeedDownloadAsbX
 * 全部恒 false，弹窗路径全部失效。
 * ★ GetDownloadBlockSize (0x2753210) 不在此列 —— 见文件头「绝对不要加的补丁」。 */
#define RVA_NEEDDLGPATCH 0x02FAD520
#define RVA_NEEDSHOWDLG  0x02753570
#define RVA_NEEDDLASB    0x02FAD470
#define LEN_GATE 6
static const BYTE ORIG_GATE_DP[LEN_GATE] = { 0x40, 0x57, 0x48, 0x83, 0xec, 0x20 };
static const BYTE ORIG_GATE_SD[LEN_GATE] = { 0x40, 0x57, 0x48, 0x83, 0xec, 0x20 };
static const BYTE ORIG_GATE_DA[LEN_GATE] = { 0x40, 0x53, 0x48, 0x83, 0xec, 0x20 };

/* ---------------- 日志 ------------------------------------------------------ */

static char g_log[MAX_PATH];

static void log_open(HINSTANCE h)
{
    char dir[MAX_PATH];
    DWORD n = GetModuleFileNameA(h, dir, MAX_PATH);
    if (!n || n >= MAX_PATH) { g_log[0] = 0; return; }
    while (n > 0 && dir[n - 1] != '\\') n--;
    dir[n] = 0;
    lstrcpyA(g_log, dir);
    lstrcatA(g_log, "bh3bypass.log");
}

static void log_line(const char *s)
{
    HANDLE f;
    DWORD  w = 0;
    if (!g_log[0]) return;
    f = CreateFileA(g_log, FILE_APPEND_DATA, FILE_SHARE_READ, NULL,
                    OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
    if (f == INVALID_HANDLE_VALUE) return;
    WriteFile(f, s, (DWORD)lstrlenA(s), &w, NULL);
    CloseHandle(f);
}

static void logf(const char *fmt, ...)
{
    char buf[1024];
    va_list ap;
    va_start(ap, fmt);
    vsnprintf(buf, sizeof(buf) - 2, fmt, ap);
    va_end(ap);
    lstrcatA(buf, "\r\n");
    log_line(buf);
}

/* ---------------- 内存写入 -------------------------------------------------- */

static BOOL write_code(void *addr, const void *data, DWORD n)
{
    DWORD old = 0;
    if (!VirtualProtect(addr, n, PAGE_EXECUTE_READWRITE, &old)) return FALSE;
    memcpy(addr, data, n);
    VirtualProtect(addr, n, old, &old);
    FlushInstructionCache(GetCurrentProcess(), addr, n);
    return TRUE;
}

/* 校验原始字节；返回 1 = 是原版，可以打补丁 */
static int is_original(const BYTE *addr, const BYTE *orig, DWORD n)
{
    return memcmp(addr, orig, n) == 0;
}

static void hexdump(const BYTE *p, DWORD n, char *out, DWORD outlen)
{
    static const char HEX[] = "0123456789abcdef";
    DWORD i, k = 0;
    out[0] = 0;
    for (i = 0; i < n && k + 4 < outlen; i++) {
        out[k++] = HEX[p[i] >> 4];
        out[k++] = HEX[p[i] & 15];
        out[k++] = ' ';
    }
    out[k] = 0;
}

/* ---------------- 通用：恒 false -------------------------------------------- */

/* 把函数入口改成 `xor eax,eax ; ret`（恒 false），剩余字节填 int3。
 * 只在入口字节与预期一致时才动手。 */
static void patch_false(const char *tag, BYTE *fn,
                        const BYTE *orig, DWORD len)
{
    BYTE body[64];
    BYTE chk[64];
    char hs[256];

    if (len > sizeof(body)) return;

    hexdump(fn, len, hs, sizeof(hs));
    logf("[%s] %p 当前: %s", tag, fn, hs);

    if (fn[0] == 0x33 && fn[1] == 0xC0 && fn[2] == 0xC3) {
        logf("[%s] 已经是补丁状态，跳过", tag);
        return;
    }
    if (!is_original(fn, orig, len)) {
        logf("[%s] !! 原始字节不匹配，拒绝打补丁（可能有别的补丁/IFix 改动）", tag);
        return;
    }

    memset(body, 0xCC, len);
    body[0] = 0x33;
    body[1] = 0xC0;
    body[2] = 0xC3;

    if (!write_code(fn, body, len)) {
        logf("[%s] !! VirtualProtect/memcpy 失败 err=%lu", tag, GetLastError());
        return;
    }
    memcpy(chk, fn, len);
    if (memcmp(chk, body, len) == 0)
        logf("[%s] OK  恒返回 false", tag);
    else
        logf("[%s] !! 写入后回读不一致", tag);
}

static void apply_norefresh(HMODULE ua)
{
    patch_false("norefresh", (BYTE *)ua + RVA_NOREFRESH,
                ORIG_NOREFRESH, LEN_NOREFRESH);
    patch_false("needverify", (BYTE *)ua + RVA_NEEDVERIFY,
                ORIG_NEEDVERIFY, LEN_NEEDVERIFY);
    patch_false("needdlpatch", (BYTE *)ua + RVA_NEEDDLGPATCH,
                ORIG_GATE_DP, LEN_GATE);
    patch_false("needshowdlg", (BYTE *)ua + RVA_NEEDSHOWDLG,
                ORIG_GATE_SD, LEN_GATE);
    patch_false("needdlasb", (BYTE *)ua + RVA_NEEDDLASB,
                ORIG_GATE_DA, LEN_GATE);
}

/* ---------------- 块校验结果恒为成功 ---------------------------------------- */

/* 观测状态：stub 每次被调用就把「传入的 success」和「调用次数」记下来，
 * 由 watch 线程定期写日志 —— 用来确认这个回调到底会不会被调用。 */
#pragma pack(push, 1)
typedef struct {
    BYTE              last_dl;
    BYTE              pad[7];
    volatile ULONGLONG hits;
} HITSTATE;
#pragma pack(pop)

static HITSTATE g_hit;

static void apply_oncheck(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_ONCHECK;
    BYTE *stub;
    BYTE  entry[LEN_ONCHECK];
    char  hs[256];

    hexdump(fn, LEN_ONCHECK, hs, sizeof(hs));
    logf("[oncheck] %p 当前: %s", fn, hs);

    if (fn[0] == 0xFF && fn[1] == 0x25) {
        logf("[oncheck] 已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_ONCHECK, LEN_ONCHECK)) {
        logf("[oncheck] !! 原始字节不匹配，拒绝打补丁");
        return;
    }

    stub = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!stub) {
        logf("[oncheck] !! VirtualAlloc 失败 err=%lu", GetLastError());
        return;
    }

    /* stub:
     *   mov rax, &g_hit          ; 48 B8 <imm64>
     *   mov [rax], dl            ; 88 10
     *   inc qword ptr [rax+8]    ; 48 FF 40 08
     *   mov dl, 1                ; B2 01
     *   <原始 16 字节>
     *   jmp qword [rip+0] -> fn+16
     * rax 是易失寄存器、不是入参（rcx=this, rdx=success），可以随便改。
     */
    {
        DWORD k = 0;
        stub[k++] = 0x48;                       /* mov rax, imm64 */
        stub[k++] = 0xB8;
        *(ULONGLONG *)(stub + k) = (ULONGLONG)&g_hit;
        k += 8;
        stub[k++] = 0x88;                       /* mov [rax], dl */
        stub[k++] = 0x10;
        stub[k++] = 0x48;                       /* inc qword ptr [rax+8] */
        stub[k++] = 0xFF;
        stub[k++] = 0x40;
        stub[k++] = 0x08;
        stub[k++] = 0xB2;                       /* mov dl, 1 */
        stub[k++] = 0x01;
        memcpy(stub + k, ORIG_ONCHECK, LEN_ONCHECK);
        k += LEN_ONCHECK;
        stub[k++] = 0xFF;                       /* jmp qword ptr [rip+0] */
        stub[k++] = 0x25;
        stub[k++] = 0x00; stub[k++] = 0x00;
        stub[k++] = 0x00; stub[k++] = 0x00;
        *(ULONGLONG *)(stub + k) = (ULONGLONG)(fn + LEN_ONCHECK);
        k += 8;
        FlushInstructionCache(GetCurrentProcess(), stub, k);
    }

    /* entry: jmp qword [rip+0] -> stub ; 余下 int3 */
    memset(entry, 0xCC, sizeof(entry));
    entry[0] = 0xFF;
    entry[1] = 0x25;
    entry[2] = 0x00; entry[3] = 0x00;
    entry[4] = 0x00; entry[5] = 0x00;
    *(ULONGLONG *)(entry + 6) = (ULONGLONG)stub;

    if (!write_code(fn, entry, LEN_ONCHECK)) {
        logf("[oncheck] !! 写入口跳转失败 err=%lu", GetLastError());
        return;
    }
    if (memcmp(fn, entry, LEN_ONCHECK) == 0)
        logf("[oncheck] OK  success 恒为 true（stub @ %p）", stub);
    else
        logf("[oncheck] !! 写入后回读不一致");
}

/* ---------------- 观测线程 -------------------------------------------------- */

static DWORD WINAPI watch(LPVOID param)
{
    ULONGLONG last = 0;
    int i;
    (void)param;
    for (i = 0; i < 180; i++) {             /* 观测 3 分钟 */
        Sleep(1000);
        if (g_hit.hits != last) {
            logf("[oncheck] 调用 %llu 次，最近一次传入 success=%u",
                 (unsigned long long)g_hit.hits, (unsigned)g_hit.last_dl);
            last = g_hit.hits;
        }
    }
    logf("[oncheck] 观测结束，总调用 %llu 次", (unsigned long long)g_hit.hits);
    return 0;
}

/* ---------------- 入口 ------------------------------------------------------ */

static DWORD WINAPI worker(LPVOID param)
{
    HMODULE ua = NULL;
    DWORD   t0 = GetTickCount();
    int     i;

    (void)param;
    logf("=== bh3bypass start (pid=%lu) ===", GetCurrentProcessId());

    /* UserAssembly.dll 是 182 MB，映射完到执行 il2cpp 初始化有相当长的间隔，
     * 10ms 轮询足够赶在 StreamingAssetBundleManager.InitLocal 之前打上补丁。 */
    for (i = 0; i < 6000; i++) {            /* 10ms × 6000 = 60s */
        ua = GetModuleHandleA("UserAssembly.dll");
        if (ua) break;
        Sleep(10);
    }
    if (!ua) {
        logf("!! 60s 内没等到 UserAssembly.dll，放弃");
        return 0;
    }
    logf("UserAssembly.dll base=%p（等待 %lu ms）", ua, GetTickCount() - t0);

    apply_norefresh(ua);
    apply_oncheck(ua);

    CreateThread(NULL, 0, watch, NULL, 0, NULL);

    logf("=== bh3bypass done（仅资源校验，无其它补丁）===");
    return 0;
}

BOOL WINAPI DllMain(HINSTANCE h, DWORD reason, LPVOID reserved)
{
    (void)reserved;
    if (reason == DLL_PROCESS_ATTACH) {
        DisableThreadLibraryCalls(h);
        log_open(h);
        CreateThread(NULL, 0, worker, NULL, 0, NULL);
    }
    return TRUE;
}
