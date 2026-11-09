/*
 * bh3bypass.dll —— 把 UserAssembly.dll 里的 ASB 校验函数改成「恒为真 / 恒不重建」
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
 * 两个目标（RVA 已用 tools\verify_asb_funcs.py 逐字节核对）：
 *
 *   NeedRefreshMemoryMappedData          RVA 0x02FAD6D0
 *       InitMemoryMappedData 里 `if (NeedRefreshMemoryMappedData()) { 重建 }`。
 *       恒返回 false => 客户端永远信任现成的 asb.dat，不重建、不覆盖我们的改动。
 *
 *   OnCheckStreamingAssetBundleComplete  RVA 0x01F1A600
 *       void f(this, bool success)；success 在 dl。
 *       入口前插 `mov dl,1` => 永远走成功分支，不弹「需要下载 51MB」、不卡下载态。
 *
 * 注意：
 *   * 这是 inline patch（改机器码），不是 IAT hook。ACE 是否会查 .text 完整性未知，
 *     所以两处都先比对原始字节，不匹配就拒绝动手。
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
 *   48 83 ec 20       sub  rsp, 0x20
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

/* 「要求下载」闸门四补丁（11-04 agent 静态分析 + 主线核对）：
 * GetDownloadBlockSize / NeedDownloadPatchX / NeedShowDownloadResourceDialog /
 * NeedDownloadAsbX —— 全部恒 false/0，弹窗路径全部失效 */
#define RVA_GETBLOCKSIZE 0x02753210
#define RVA_NEEDDLGPATCH 0x02FAD520
#define RVA_NEEDSHOWDLG  0x02753570
#define RVA_NEEDDLASB    0x02FAD470
#define LEN_GATE 6
static const BYTE ORIG_GATE_BS[LEN_GATE] = { 0x40, 0x53, 0x48, 0x83, 0xec, 0x20 };
static const BYTE ORIG_GATE_DP[LEN_GATE] = { 0x40, 0x57, 0x48, 0x83, 0xec, 0x20 };
static const BYTE ORIG_GATE_SD[LEN_GATE] = { 0x40, 0x57, 0x48, 0x83, 0xec, 0x20 };
static const BYTE ORIG_GATE_DA[LEN_GATE] = { 0x40, 0x53, 0x48, 0x83, 0xec, 0x20 };

/* 「迪拉克之海 BOSS 层额外得分」补丁（11-05）
 *
 * 现象：BOSS 层能打，但没有额外得分（结算弹窗 Num02 空、服务端总分不加）。
 * 根因：整条链只有一个入口，参数由关卡 Lua 给，而 Lua 传的是 0。
 *
 *   SetOWEndLessGroupFinish(this, groupID, time, battleID, extraScore)   RVA 0x03444500
 *     +0x15E -> SetBossExtraScoreInLevel(0x189CC10)  ← 全工程唯一调用点
 *                -> OWEndLessBattleLevelInfo._bossExtraScoreInLevel(+0x94)
 *                -> 结算弹窗 SetupBossView 的 Point/Num02
 *     +0x1B5 -> RequestFinishMonsterGroupReq(0x181AB6D90) ← 全工程唯一调用点
 *                -> 3424 extra_score -> 服务端 Advance 的 gain += extraScore
 *
 * 服务端日志 8/8 条 FinishGroup 都是 extra=0（含已完成的 layer0/1/2 BOSS），
 * 说明函数确实被调、只是第 5 个参数为 0。服务端下发的 boss_extra_score 已经是
 * 1000（OWEndlessGroup.ExtraScore），客户端也拿到了，只是没人用。
 *
 * 做法：入口前插 14 字节跳板，在第 5 个参数（入口处 [rsp+0x28]）为 0 且
 * battleID ∈ 29..32（OWEndlessBattleArea 里 3015..3018 = AreaBoss，全表固定）
 * 时把它改成 1000。改一个参数同时救「结算弹窗额外得分」和「服务端加分」。
 *
 * 注意：不能无条件改 —— 这个函数对每个怪物组都会调一次，无条件改等于每组 +1000。
 * 也救不了关卡内 HUD 的倒计时：那条链是 LevelScoreManager.
 * StartAutoChangeLevelExtraScoreByTime（RVA 0x183453D10，C# 侧 0 个调用点，
 * 只有 Lua 能触发），与本补丁无关。
 *
 * 入口字节（指令边界 5/10/15/17）：
 *   48 89 5c 24 10    mov  [rsp+0x10], rbx
 *   48 89 6c 24 18    mov  [rsp+0x18], rbp
 *   48 89 7c 24 20    mov  [rsp+0x20], rdi
 *   41 56             push r14
 * 合计 17 字节；`jmp [rip+0]` + 8 字节绝对地址只要 14 字节，
 * 余下 3 字节填 int3，跳回 fn+17 = `sub rsp,0x30`。
 */
#define RVA_OWBOSS      0x03444500
#define LEN_OWBOSS      17
#define OWBOSS_EXTRA    1000
static const BYTE ORIG_OWBOSS[LEN_OWBOSS] = {
    0x48, 0x89, 0x5c, 0x24, 0x10,
    0x48, 0x89, 0x6c, 0x24, 0x18,
    0x48, 0x89, 0x7c, 0x24, 0x20,
    0x41, 0x56
};

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

/* 取与日志同目录的兄弟文件名。g_log 尾部固定是 "bh3bypass.log"（13 字符），
 * 早先这里误写成 11，拼出来的实际是 app\bhowboss_off.txt，开关永远不生效。 */
static void sibling_path(char *out, const char *name)
{
    int n = lstrlenA(g_log);
    if (n < 13) { out[0] = 0; return; }
    lstrcpyA(out, g_log);
    out[n - 13] = 0;
    lstrcatA(out, name);
}

/* 读一个纯数字小文件（失败返回 def）。用于 owboss_target.txt 覆盖目标 battleID，
 * 这样万一某期配置的「最后一个 BOSS」不是 battle 32，改文本即可，不用重编译。 */
static int read_int_file(const char *path, int def)
{
    HANDLE f;
    char   buf[64];
    DWORD  w = 0;
    int    i, v = 0, got = 0;

    f = CreateFileA(path, GENERIC_READ, FILE_SHARE_READ, NULL,
                    OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
    if (f == INVALID_HANDLE_VALUE) return def;
    if (!ReadFile(f, buf, sizeof(buf) - 1, &w, NULL)) w = 0;
    CloseHandle(f);
    buf[w] = 0;
    for (i = 0; i < (int)w; i++) {
        if (buf[i] >= '0' && buf[i] <= '9') {
            v = v * 10 + (buf[i] - '0');
            got = 1;
            if (v > 100000) return def;
        } else if (got) {
            break;
        }
    }
    return got ? v : def;
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

/* ---------------- 补丁 1：永不重建 asb.dat ---------------------------------- */

/* 通用：把函数入口改成 `xor eax,eax ; ret`（恒 false），剩余字节填 int3。
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
    /* 「要求下载」闸门补丁（11-04 定稿）：
     * 保留 needshowdlg / needdlpatch / needdlasb 三个恒 false。
     * ★ getblocksize（0x2753210）不补 —— 恒 0 会让 DownloadResources 走
     *   "==0" 分支跳过下载管理器初始化（[this+0xB8..0xD8] 全 null），
     *   进主场景后对空管理器虚调用 → unityplayer call [rax+0x48] 崩溃。
     *   弹窗压制由 needshowdlg 独立完成，块大小返回真实值让初始化正常走。 */
    patch_false("needdlpatch", (BYTE *)ua + RVA_NEEDDLGPATCH,
                ORIG_GATE_DP, LEN_GATE);
    patch_false("needshowdlg", (BYTE *)ua + RVA_NEEDSHOWDLG,
                ORIG_GATE_SD, LEN_GATE);
    patch_false("needdlasb", (BYTE *)ua + RVA_NEEDDLASB,
                ORIG_GATE_DA, LEN_GATE);
}

/* ---------------- 补丁 2：块校验结果恒为成功 -------------------------------- */

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

/* ---------------- 补丁 3：BOSS 层额外得分 ---------------------------------- */

/* stub 写入的观测结构。偏移必须和下面手写的机器码一致：
 *   +0x00 group / +0x04 battle / +0x08 time / +0x0C extra
 *   +0x10 hits（调用次数） / +0x18 applied（实际补分次数） */
#pragma pack(push, 1)
typedef struct {
    DWORD             group;
    DWORD             battle;
    DWORD             time;
    DWORD             extra;
    volatile ULONGLONG hits;
    volatile ULONGLONG applied;
} OWHIT;
#pragma pack(pop)

static OWHIT g_ow;

/* 手写 stub（字节逐个核对过，改动请同步改注释里的偏移表）：
 *
 *   off  bytes                        asm
 *    0   49 ba <imm64>                mov  r10, &g_ow            ; 先无条件记录
 *   10   41 89 12                     mov  [r10], edx            ; groupID
 *   13   45 89 4a 04                  mov  [r10+4], r9d          ; battleID
 *   17   45 89 42 08                  mov  [r10+8], r8d          ; time
 *   21   49 ff 42 10                  inc  qword [r10+0x10]      ; hits
 *   25   41 81 f9 <imm32>             cmp  r9d, 32               ; 只认最后一个 BOSS
 *   32   75 1b                        jne  skip                  ; -> +61
 *   34   83 7c 24 28 00               cmp  dword [rsp+0x28], 0   ; Lua 传的是 0？
 *   39   75 14                        jne  skip                  ; -> +61
 *   41   c7 44 24 28 e8 03 00 00      mov  dword [rsp+0x28], 1000
 *   49   41 c7 42 0c e8 03 00 00      mov  dword [r10+0xC], 1000
 *   57   49 ff 42 18                  inc  qword [r10+0x18]      ; applied
 *   61   <17 字节原始 prologue>        skip:
 *   78   ff 25 00 00 00 00            jmp  qword [rip+0]
 *   84   <8 字节 fn+17>
 *
 * 为什么只认 battleID 32
 * ----------------------
 * 迪拉克一场 = 4 层，每层末尾一个 BOSS，计分组是 AreaScoreConfig 的
 * BossScoreConfig = [131, 132, 133, 134]。OWEndlessBattleArea 里 AreaBoss
 * 只有 4 行：battleID 29/30/31/32 -> areaId 3015/3016/3017/3018，与层序一一对应。
 * 玩家实测最终 BOSS 是 group=134 / area=3018 / battle=32，即第 4 层最后一个。
 * 游戏机制上「计时器 + 额外得分」只挂在最后一个 BOSS，前三层 BOSS 不该补，
 * 所以不能按 29..32 整段放行。目标值可在运行时用 owboss_target.txt 覆盖。
 */
#define STUB_OWBOSS_LEN   92
#define STUB_OW_OFF_GOW   2    /* mov r10, &g_ow 的 imm64 偏移 */
#define STUB_OW_OFF_TGT   28   /* cmp r9d, imm32 的 imm32 偏移 */
#define STUB_OW_OFF_TAIL  84   /* jmp 目标 fn+17 的 imm64 偏移 */

/* 写成静态模板而不是逐字节赋值：-O2 会把连续的字节 store 合成 memcpy，
 * 生成物无法逐字节核对。模板留 0 占位，运行时填地址后再拷进可执行页。 */
static BYTE g_stub_ow[STUB_OWBOSS_LEN] = {
    0x49, 0xBA,                          /* mov  r10,&g_ow         */
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x41, 0x89, 0x12,                    /* mov  [r10],edx         */
    0x45, 0x89, 0x4A, 0x04,              /* mov  [r10+4],r9d       */
    0x45, 0x89, 0x42, 0x08,              /* mov  [r10+8],r8d       */
    0x49, 0xFF, 0x42, 0x10,              /* inc  qword [r10+0x10]  */
    0x41, 0x81, 0xF9,                    /* cmp  r9d, imm32        */
    0x20, 0x00, 0x00, 0x00,              /*   imm32 = 32（默认）   */
    0x75, 0x1B,                          /* jne  +61               */
    0x83, 0x7C, 0x24, 0x28, 0x00,        /* cmp  [rsp+0x28],0      */
    0x75, 0x14,                          /* jne  +61               */
    0xC7, 0x44, 0x24, 0x28, 0xE8, 0x03, 0x00, 0x00,  /* mov [rsp+0x28],1000 */
    0x41, 0xC7, 0x42, 0x0C, 0xE8, 0x03, 0x00, 0x00,  /* mov [r10+0xC],1000  */
    0x49, 0xFF, 0x42, 0x18,              /* inc  qword [r10+0x18]  */
    0x48, 0x89, 0x5C, 0x24, 0x10,        /* mov  [rsp+0x10],rbx    */
    0x48, 0x89, 0x6C, 0x24, 0x18,        /* mov  [rsp+0x18],rbp    */
    0x48, 0x89, 0x7C, 0x24, 0x20,        /* mov  [rsp+0x20],rdi    */
    0x41, 0x56,                          /* push r14               */
    0xFF, 0x25, 0x00, 0x00, 0x00, 0x00,  /* jmp  qword [rip+0]     */
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
};

static void apply_owboss(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_OWBOSS;
    BYTE *stub;
    BYTE  entry[LEN_OWBOSS];
    char  hs[256];
    char  path[MAX_PATH];
    int   target;

    hexdump(fn, LEN_OWBOSS, hs, sizeof(hs));
    logf("[owboss] %p 当前: %s", fn, hs);

    if (fn[0] == 0xFF && fn[1] == 0x25) {
        logf("[owboss] 已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_OWBOSS, LEN_OWBOSS)) {
        logf("[owboss] !! 原始字节不匹配，拒绝打补丁");
        return;
    }

    sibling_path(path, "owboss_target.txt");
    target = read_int_file(path, 32);
    if (target <= 0) {
        logf("[owboss] owboss_target.txt 内容无效，回退 32");
        target = 32;
    }
    logf("[owboss] 目标 battleID = %d（owboss_target.txt 可覆盖，默认 32 = 最终 BOSS）", target);

    stub = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!stub) {
        logf("[owboss] !! VirtualAlloc 失败 err=%lu", GetLastError());
        return;
    }

    /* 填三个运行时值，再整体拷进可执行页 */
    *(ULONGLONG *)(g_stub_ow + STUB_OW_OFF_GOW)  = (ULONGLONG)&g_ow;
    *(int *)(g_stub_ow + STUB_OW_OFF_TGT)        = target;
    *(ULONGLONG *)(g_stub_ow + STUB_OW_OFF_TAIL) = (ULONGLONG)(fn + LEN_OWBOSS);
    memcpy(stub, g_stub_ow, STUB_OWBOSS_LEN);
    FlushInstructionCache(GetCurrentProcess(), stub, STUB_OWBOSS_LEN);

    /* entry: jmp qword [rip+0] -> stub ; 余下 int3 */
    memset(entry, 0xCC, sizeof(entry));
    entry[0] = 0xFF;
    entry[1] = 0x25;
    entry[2] = 0x00; entry[3] = 0x00;
    entry[4] = 0x00; entry[5] = 0x00;
    *(ULONGLONG *)(entry + 6) = (ULONGLONG)stub;

    if (!write_code(fn, entry, LEN_OWBOSS)) {
        logf("[owboss] !! 写入口跳转失败 err=%lu", GetLastError());
        return;
    }
    if (memcmp(fn, entry, LEN_OWBOSS) == 0)
        logf("[owboss] OK  battleID==%d 且 extraScore==0 时补 %d 分（stub @ %p）",
             target, OWBOSS_EXTRA, stub);
    else
        logf("[owboss] !! 写入后回读不一致");
}

/* ============================================================================
 * 第二处补丁：额外得分倒计时起始值
 * ============================================================================
 *
 * 2026-11-05 13:15 追加。上一版打在 SetOWEndLessGroupFinish 上，日志证明它在
 * 进 BOSS 时**一次都没被调用**（hits 恒 0），所以那条路修不了 HUD 的 0。
 *
 * 用户实测描述：「额外得分显示为 0，应该进入 boss 后从 1000 开始按时间减」。
 * 那就是 LevelScoreManager 的额外得分滚动值，入口是
 *
 *   LevelDesignManager.StartAutoChangeLevelExtraScoreByTime(init, final, dur)
 *     VA 0x183453D10 / RVA 0x03453D10
 *
 * 反汇编确认的入参寄存器（实例方法，rcx=this）：
 *     +000F  mov rbx, rcx        ; this  = LevelDesignManager
 *     +001C  movaps xmm6, xmm3   ; dur   = xmm3  ← 注意不是 xmm0
 *     +001F  mov rdi, r8         ; final = r8
 *     +0022  mov rsi, rdx        ; init  = rdx
 *     +00BD  call 0x1824C4FE0    ; LevelScoreManager.StartAutoChangeLevelExtraScore
 *
 * 调用点普查：LevelScoreManager.StartAutoChangeLevelExtraScore 全工程只有 1 个
 * 调用点（就是上面这个包装），SetOWEndlessLevelScoreMode / GetBossExtraScore /
 * GetBossExtraScoreInLevel 的 C# 调用点都是 0 —— 整条显示链完全由关卡 Lua 驱动。
 *
 * 所以断点只可能在「Lua 没调」或「Lua 调了但 init 是 0」。本补丁只处理后者：
 * rdx == 0 时顶成 1000，其余一律不动。若日志显示 hits==0（Lua 根本没调），
 * 说明得改成从游戏线程主动调这个函数，那是下一步。
 *
 * 入口 prologue 边界（5 / 10 / 11 / 15）：
 *   48 89 5c 24 08   mov [rsp+8], rbx
 *   48 89 74 24 10   mov [rsp+0x10], rsi
 *   57               push rdi
 *   48 83 ec 40      sub rsp, 0x40
 * = 15 字节，跳回 fn+15 = `mov rbx, rcx`，干净边界。
 */
#define RVA_EBTIME      0x03453D10
#define LEN_EBTIME      15
#define EBTIME_INIT     1000
static const BYTE ORIG_EBTIME[LEN_EBTIME] = {
    0x48, 0x89, 0x5C, 0x24, 0x08,
    0x48, 0x89, 0x74, 0x24, 0x10,
    0x57,
    0x48, 0x83, 0xEC, 0x40
};

#pragma pack(push, 1)
typedef struct {
    volatile ULONGLONG hits;      /* +0x00 */
    volatile ULONGLONG applied;   /* +0x08 */
    volatile ULONGLONG thisp;     /* +0x10 */
    volatile ULONGLONG init;      /* +0x18 */
    volatile ULONGLONG final;     /* +0x20 */
    volatile ULONGLONG dur[2];    /* +0x28  16 字节，容纳 movdqu */
} EBHIT;
#pragma pack(pop)

static EBHIT g_eb;

/*   off  bytes                        asm
 *    0   49 ba <imm64>                mov  r10, &g_eb
 *   10   49 89 4a 10                  mov  [r10+0x10], rcx     ; this
 *   14   49 89 52 18                  mov  [r10+0x18], rdx     ; initScore
 *   18   4d 89 42 20                  mov  [r10+0x20], r8      ; finalScore
 *   22   f3 41 0f 7f 5a 28            movdqu [r10+0x28], xmm3  ; durationTime
 *   28   49 ff 02                     inc  qword [r10]         ; hits
 *   31   48 85 d2                     test rdx, rdx            ; init == 0 ?
 *   34   75 0b                        jne  skip                ; -> +47
 *   36   48 c7 c2 e8 03 00 00         mov  rdx, 1000
 *   43   49 ff 42 08                  inc  qword [r10+8]       ; applied
 *   47   <15 字节原始 prologue>        skip:
 *   62   ff 25 00 00 00 00            jmp  qword [rip+0]
 *   68   <8 字节 fn+15>
 *   总长 76 字节
 *
 * 注意 movdqu 必须是 F3 0F 7F；写成 66 0F 7F 就是 MOVDQA，要求目标 16 字节对齐，
 * 而 &g_eb+0x28 不对齐 → 一进游戏就 GP 崩。
 */
#define STUB_EB_LEN       76
#define STUB_EB_OFF_GEB   2    /* mov r10, &g_eb 的 imm64 偏移 */
#define STUB_EB_OFF_TAIL  68   /* jmp 目标 fn+15 的 imm64 偏移 */

static BYTE g_stub_eb[STUB_EB_LEN] = {
    0x49, 0xBA,                          /* mov  r10,&g_eb         */
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x49, 0x89, 0x4A, 0x10,              /* mov  [r10+0x10],rcx    */
    0x49, 0x89, 0x52, 0x18,              /* mov  [r10+0x18],rdx    */
    0x4D, 0x89, 0x42, 0x20,              /* mov  [r10+0x20],r8     */
    0xF3, 0x41, 0x0F, 0x7F, 0x5A, 0x28,  /* movdqu [r10+0x28],xmm3 */
    0x49, 0xFF, 0x02,                    /* inc  qword [r10]       */
    0x48, 0x85, 0xD2,                    /* test rdx,rdx           */
    0x75, 0x0B,                          /* jne  +47               */
    0x48, 0xC7, 0xC2, 0xE8, 0x03, 0x00, 0x00,  /* mov rdx,1000     */
    0x49, 0xFF, 0x42, 0x08,              /* inc  qword [r10+8]     */
    0x48, 0x89, 0x5C, 0x24, 0x08,        /* mov  [rsp+8],rbx       */
    0x48, 0x89, 0x74, 0x24, 0x10,        /* mov  [rsp+0x10],rsi    */
    0x57,                                /* push rdi               */
    0x48, 0x83, 0xEC, 0x40,              /* sub  rsp,0x40          */
    0xFF, 0x25, 0x00, 0x00, 0x00, 0x00,  /* jmp  qword [rip+0]     */
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
};

static void apply_ebtime(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_EBTIME;
    BYTE *stub;
    BYTE  entry[LEN_EBTIME];
    char  hs[256];

    hexdump(fn, LEN_EBTIME, hs, sizeof(hs));
    logf("[ebtime] %p 当前: %s", fn, hs);

    if (fn[0] == 0xFF && fn[1] == 0x25) {
        logf("[ebtime] 已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_EBTIME, LEN_EBTIME)) {
        logf("[ebtime] !! 原始字节不匹配，拒绝打补丁");
        return;
    }

    stub = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!stub) {
        logf("[ebtime] !! VirtualAlloc 失败 err=%lu", GetLastError());
        return;
    }

    *(ULONGLONG *)(g_stub_eb + STUB_EB_OFF_GEB)  = (ULONGLONG)&g_eb;
    *(ULONGLONG *)(g_stub_eb + STUB_EB_OFF_TAIL) = (ULONGLONG)(fn + LEN_EBTIME);
    memcpy(stub, g_stub_eb, STUB_EB_LEN);
    FlushInstructionCache(GetCurrentProcess(), stub, STUB_EB_LEN);

    memset(entry, 0xCC, sizeof(entry));
    entry[0] = 0xFF;
    entry[1] = 0x25;
    entry[2] = 0x00; entry[3] = 0x00;
    entry[4] = 0x00; entry[5] = 0x00;
    *(ULONGLONG *)(entry + 6) = (ULONGLONG)stub;

    if (!write_code(fn, entry, LEN_EBTIME)) {
        logf("[ebtime] !! 写入口跳转失败 err=%lu", GetLastError());
        return;
    }
    if (memcmp(fn, entry, LEN_EBTIME) == 0)
        logf("[ebtime] OK  initScore==0 时顶成 %d（stub @ %p）", EBTIME_INIT, stub);
    else
        logf("[ebtime] !! 写入后回读不一致");
}

/* ============================================================================
 * 第三/第四处补丁：主动把额外得分倒计时点起来
 * ============================================================================
 *
 * 2026-11-05 13:20 实证：进 BOSS 场后 [ebtime] 与 [owboss] 的 hits 都还是 0，
 * 说明关卡 Lua 压根没调 StartAutoChangeLevelExtraScoreByTime。光改参数救不了，
 * 必须自己调。
 *
 * 为什么不能直调 Lua 包装层：StartAutoChangeLevelExtraScoreByTime 与内层
 * LevelScoreManager.StartAutoChangeLevelExtraScore 都带
 * `mov ecx,<id>; call 0x1835D7F80` 的校验。拆开看它是「该方法是否被 Lua 覆盖」
 * 查询（table[id] != 0 → 走 Lua 分支）。未覆盖时返回 0 → 正常路径，
 * 所以从 native 调是可行的，前提是拿到合法实例 + 在游戏线程上。
 *
 * 两个钩子：
 *   A) SetOpenWorldEndlessLevelBeginIntent  0x024C0B60  (DoBeginLevel +0x152 唯一调用点)
 *      rcx=LevelScoreManager  rdx=level  r8d=mapID  r9d=areaID
 *      [rsp+0x28]=progress [rsp+0x30]=hardLevel [rsp+0x38]=difficulty
 *      只记录，不改行为。
 *
 *   B) SetOWEndlessLevelScoreMode            0x03444AA0  (纯 Lua 调用)
 *      rcx=LevelDesignManager  dl=show  r8b=playAnim  r9=textmap  [rsp+0x28]=isExtra
 *      isExtra != 0 就是「额外得分」那块面板在开。此刻 rcx 与
 *      StartAutoChangeLevelExtraScoreByTime 的 this 是同一个对象，
 *      于是直接用它调 StartAutoChangeLevelExtraScoreByTime(1000, 0, 300.0f)。
 *
 * 顺序上 DoBeginLevel 先于 UI 建立，所以 A 一定在 B 之前，areaID 可靠。
 * 只在 areaID==3018（第 4 层最终 BOSS）时才动手。
 *
 * 调用约定要点（StartAutoChangeLevelExtraScoreByTime 实测）：
 *   rdx=initScore  r8=finalScore  xmm3=durationTime(float)  ← 不是 xmm0
 *   300.0f = 0x43960000
 */
#define RVA_BEGIN       0x024C0B60
#define LEN_BEGIN       21
#define RVA_SMODE       0x03444AA0
#define LEN_SMODE       20
#define AREA_FINAL_BOSS 3018

static const BYTE ORIG_BEGIN[LEN_BEGIN] = {
    0x48, 0x89, 0x6C, 0x24, 0x10,
    0x48, 0x89, 0x74, 0x24, 0x18,
    0x48, 0x89, 0x7C, 0x24, 0x20,
    0x41, 0x56,
    0x48, 0x83, 0xEC, 0x40
};
static const BYTE ORIG_SMODE[LEN_SMODE] = {
    0x48, 0x89, 0x5C, 0x24, 0x08,
    0x48, 0x89, 0x6C, 0x24, 0x10,
    0x48, 0x89, 0x74, 0x24, 0x18,
    0x57,
    0x48, 0x83, 0xEC, 0x60
};

#pragma pack(push, 1)
typedef struct {
    volatile ULONGLONG hitsA;      /* 0x00 */
    volatile ULONGLONG done;       /* 0x08 */
    volatile ULONGLONG lsm;        /* 0x10 */
    volatile ULONGLONG level;      /* 0x18 */
    volatile ULONGLONG mapId;      /* 0x20 */
    volatile ULONGLONG areaId;     /* 0x28 */
    volatile DWORD     progress;   /* 0x30 */
    volatile DWORD     hardLevel;  /* 0x34 */
    volatile DWORD     difficulty; /* 0x38 */
    volatile DWORD     pad0;       /* 0x3C */
    volatile ULONGLONG extraRaw;   /* 0x40 */
    volatile ULONGLONG hitsB;      /* 0x48 */
    volatile ULONGLONG showRd;     /* 0x50 = sm 的 rcx（LevelDesignManager） */
    volatile ULONGLONG r8v;        /* 0x58 = sm 的 rdx（show） */
    volatile ULONGLONG r9v;        /* 0x60 = sm 的 r8（playAnim） */
    volatile ULONGLONG textmap;    /* 0x68 = sm 的 r9（string*） */
    volatile DWORD     isExtra;    /* 0x70 */
    volatile DWORD     pad3;       /* 0x74 */
    /* ---- v13：驱动原生 HUD 用 ---- */
    volatile ULONGLONG savedScore; /* 0x78 = SetLevelExtraScore 的 rdx（明文分数） */
    volatile ULONGLONG savedThis;  /* 0x80 = SetLevelExtraScore 的 rcx */
    volatile ULONGLONG ctx;        /* 0x88 = InLevelMainPageContext 实例（由外部工具写入） */
    volatile ULONGLONG hudHits;    /* 0x90 */
    volatile ULONGLONG hudStart;   /* 0x98 = 倒计时起点（GetTickCount） */
} OWREC;
#pragma pack(pop)

static OWREC g_rec;

/* ---- A: BeginIntent 纯记录 + 重置「本关已起倒计时」标志 ---------------------
 *  off  bytes                    asm
 *    0  49 ba <imm64>            mov  r10,&g_rec
 *    a  49 89 4a 10              mov  [r10+0x10],rcx    ; lsm
 *    e  49 89 52 18              mov  [r10+0x18],rdx    ; level
 *   12  4d 89 42 20              mov  [r10+0x20],r8     ; mapID
 *   16  4d 89 4a 28              mov  [r10+0x28],r9     ; areaID
 *   1a  49 ff 02                 inc  qword [r10]       ; hitsA
 *   1d  49 c7 42 08 00 00 00 00  mov  qword [r10+8],0  ; done = 0（每关重置）
 *   25  8b 44 24 28              mov  eax,[rsp+0x28]    ; progress
 *   29  41 89 42 30              mov  [r10+0x30],eax
 *   2d  8b 44 24 30              mov  eax,[rsp+0x30]    ; hardLevel
 *   31  41 89 42 34              mov  [r10+0x34],eax
 *   35  8b 44 24 38              mov  eax,[rsp+0x38]    ; difficulty
 *   39  41 89 42 38              mov  [r10+0x38],eax
 *   3d  48 8b 81 c0 03 00 00     mov  rax,[rcx+0x3C0]   ; levelExtraScore 原始密文
 *   44  49 89 42 40              mov  [r10+0x40],rax
 *   48  <21 字节原始 prologue>    （5+5+5+2+4，41 56 是 2 字节）
 *   5d  ff 25 00 00 00 00        jmp  qword [rip+0]
 *   63  <8 字节 fn+20>
 *   总长 0x6B = 107
 */
#define STUB_BEG_LEN      107
#define STUB_BEG_OFF_REC  2
#define STUB_BEG_OFF_TAIL 99    /* 0x63 */

static BYTE g_stub_beg[STUB_BEG_LEN] = {
    0x49, 0xBA,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x49, 0x89, 0x4A, 0x10,
    0x49, 0x89, 0x52, 0x18,
    0x4D, 0x89, 0x42, 0x20,
    0x4D, 0x89, 0x4A, 0x28,
    0x49, 0xFF, 0x02,
    0x49, 0xC7, 0x42, 0x08, 0x00, 0x00, 0x00, 0x00,
    0x8B, 0x44, 0x24, 0x28,
    0x41, 0x89, 0x42, 0x30,
    0x8B, 0x44, 0x24, 0x30,
    0x41, 0x89, 0x42, 0x34,
    0x8B, 0x44, 0x24, 0x38,
    0x41, 0x89, 0x42, 0x38,
    0x48, 0x8B, 0x81, 0xC0, 0x03, 0x00, 0x00,
    0x49, 0x89, 0x42, 0x40,
    /* --- 20 字节原始 prologue --- */
    0x48, 0x89, 0x6C, 0x24, 0x10,
    0x48, 0x89, 0x74, 0x24, 0x18,
    0x48, 0x89, 0x7C, 0x24, 0x20,
    0x41, 0x56,
    0x48, 0x83, 0xEC, 0x40,
    0xFF, 0x25, 0x00, 0x00, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
};

/* ---- B: SetOWEndlessLevelScoreMode 强制开额外得分面板 ----------------------
 * v6 事故（13:34 崩溃，Crash_2026-11-05_133310）：
 *   v6 的 stub 里 `mov rcx,[r10+0x10]` 把 this 换成了 lsm，`pop rcx` 又把
 *   lsm 弹回去，于是原函数带着 this=LevelScoreManager 往下跑。按
 *   LevelDesignManager 的偏移去读 LevelScoreManager，某字段值为 1 被当成
 *   string 指针 → TrimHelper `mov eax,[rcx+0x10]` 读 0x11 → 崩。
 *   另外 `call r11` 会冲掉 rdx/r8/r9（playAnim / textmap）。
 *
 * v7：只顶 show/isExtra，不调函数 —— 实测 HUD 仍然是 0。
 *   ★ 更正：v6 日志里那条 [ebtime] 是**我们自己的 stub 调出来的**，
 *     v7 不调之后 [ebtime] 就消失了 ⇒ **关卡脚本从来没调过倒计时**，
 *     倒计时确实得由我们起。
 *
 * v8：起倒计时 + 顶显示开关，关键是**把 this 保住**：
 *   - `sub rsp,0x28` 代替 push+sub 0x20，不用栈槽存 rcx
 *   - call 之后从 g_rec 恢复 rcx/rdx/r8/r9（call 会全冲掉）
 *   - `done` 标志由 begin 钩子每关清零，重打同一关也能再起
 *
 * v9：**只起倒计时，不再顶 show/isExtra**。
 *   实测 v8 顶了之后 UI 反而坏掉（标签消失 + 错位）：真正建面板的是
 *   Lua 方法 0x2f7，它拿到 textmap=0 就重建出一个没标签的面板。
 *   而 v5/v7 不顶的时候「额外得分 0」那个元素本来就可见
 *   ⇒ 让它自己跟着 LevelScoreManager+0x3C0 跑就行。
 *
 * ★ v10：**先打开「额外得分开关」再起倒计时**（真正的病根）。
 *   SetLevelExtraScore 0x024BF220 反汇编：
 *     +22  38 83 70 01 00 00   cmp byte [this+0x170], al(=0)
 *     +28  0F 84 ...           je   ret        ← 开关是 0 就直接返回，
 *                                                levelExtraScore 一个字节都不写
 *   而 [lsm+0x170]=1 只在 SetOWEndlessLevelScoreMode 里 show!=0 时设：
 *     +8D  call 0x180261460（取 LevelScoreManager 单例）
 *     +A2  mov  byte [rax+0x170], 1
 *   我们的 stub 跑在原函数体**之前** ⇒ 那一刻开关还是 0
 *   ⇒ StartAutoChangeLevelExtraScore 内部那次 SetLevelExtraScore 被静默跳过
 *   ⇒ levelExtraScore(+0x3C0) 恒为 0 ⇒ HUD 永远显示 0。
 *   实测关卡里 [lsm+0x170] 事后就是 1（游戏自己设的），只是我们调得太早。
 *   修法：call 之前手动 `mov byte [rcx+0x170],1`。
 *
 * ★ v11 修正：**开关要打在 LevelScoreManager 上，不是 LevelDesignManager**。
 *   smode 的 this(rcx) 是 LevelDesignManager，存在 g_rec+0x50；
 *   LevelScoreManager 是 begin 存下来的 g_rec+0x10。
 *   v10 用 [r10+0x50] 去写 +0x170，打错了对象（还污染了 LDM 的无关字段），
 *   所以 SetLevelExtraScore 的 gate 依旧是 0，HUD 仍然是 0。
 *   现在改成先 `mov rcx,[r10+0x10]` → 置 1 → 再 `mov rcx,[r10+0x50]` 传给 ebtime。
 *
 * ★ 顺带实证：textmap 不是 0 也不是数字 id，是字符串
 *   'OWEndless_ExtraScoreText'（len=24），由游戏自己传进来，标签不用我们造。
 *
 *  off  bytes                    asm
 *    0  49 ba <imm64>            mov  r10,&g_rec
 *    a  49 89 4a 50              mov  [r10+0x50],rcx    ; LDM this
 *    e  49 89 52 58              mov  [r10+0x58],rdx    ; show
 *   12  4d 89 42 60              mov  [r10+0x60],r8     ; playAnim
 *   16  4d 89 4a 68              mov  [r10+0x68],r9     ; textmap(string*)
 *   1a  49 ff 42 48              inc  qword [r10+0x48]  ; hitsB
 *   1e  8b 44 24 28              mov  eax,[rsp+0x28]    ; isExtra
 *   22  41 89 42 70              mov  [r10+0x70],eax
 *   26  49 83 7a 10 00           cmp  qword [r10+0x10],0 ; begin 跑过没有
 *   2b  74 5e                    je   skip              -> +0x8B
 *   2d  41 81 7a 28 ca 0b 00 00  cmp  dword [r10+0x28],3018
 *   35  75 54                    jne  skip              -> +0x8B
 *   37  49 83 7a 08 00           cmp  qword [r10+8],0   ; 本关起过没
 *   3c  75 3d                    jne  after             -> +0x7B
 *   3e  48 83 ec 28              sub  rsp,0x28
 *   42  49 c7 42 08 01 00 00 00  mov  qword [r10+8],1
 *   4a  49 bb <imm64>            mov  r11,StartAutoChangeLevelExtraScoreByTime
 *   54  ba e8 03 00 00           mov  edx,1000
 *   59  45 33 c0                 xor  r8d,r8d
 *   5c  b8 00 00 96 43           mov  eax,0x43960000    ; 300.0f
 *   61  66 0f 6e d8              movd xmm3,eax
 *   65  49 8b 4a 10              mov  rcx,[r10+0x10]    ; ★ LevelScoreManager
 *   69  c6 81 70 01 00 00 01     mov  byte [rcx+0x170],1 ; ★ 打开额外得分开关
 *   70  49 8b 4a 50              mov  rcx,[r10+0x50]    ; this = LDM
 *   74  41 ff d3                 call r11
 *   77  48 83 c4 28              add  rsp,0x28
 *   7b  49 8b 4a 50              mov  rcx,[r10+0x50]    after: 恢复 this
 *   7f  49 8b 52 58              mov  rdx,[r10+0x58]
 *   83  4d 8b 42 60              mov  r8,[r10+0x60]
 *   87  4d 8b 4a 68              mov  r9,[r10+0x68]
 *   8b  <20 字节原始 prologue>    skip:
 *   9f  ff 25 00 00 00 00        jmp  qword [rip+0]
 *   a5  <8 字节 fn+20>
 *   总长 0xAD = 173
 */
#define STUB_SM_LEN      181
#define STUB_SM_OFF_REC  2
#define STUB_SM_OFF_FN   84    /* 0x54+2（v20 在 +0x26 插了 8 字节，整体后移） */
#define STUB_SM_OFF_TAIL 173   /* 0xAD（prologue 结束 0xA7 + 6 字节 jmp） */

static BYTE g_stub_sm[STUB_SM_LEN] = {
    0x49, 0xBA,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x49, 0x89, 0x4A, 0x50,
    0x49, 0x89, 0x52, 0x58,
    0x4D, 0x89, 0x42, 0x60,
    0x4D, 0x89, 0x4A, 0x68,
    0x49, 0xFF, 0x42, 0x48,
    0x8B, 0x44, 0x24, 0x28,
    0x41, 0x89, 0x42, 0x70,
    /* ★ v20：强制 isExtra = 0（第 5 个参数在 [rsp+0x28]）
     *   原因：isExtra!=0 时游戏显示 ExtraLevelScorePanel（**没有 Icon 子节点**），
     *   表现就是「位置错 + 缺左侧图标」；isExtra=0 走 LevelScorePanel（有 Icon）。
     *   先记录原值到 g_rec+0x70 再清零，日志仍能看到游戏原本传了什么。
     *   C7 44 24 28 00 00 00 00   mov dword ptr [rsp+0x28], 0
     */
    0xC7, 0x44, 0x24, 0x28, 0x00, 0x00, 0x00, 0x00,
    0x49, 0x83, 0x7A, 0x10, 0x00,
    0x74, 0x5E,
    0x41, 0x81, 0x7A, 0x28, 0xCA, 0x0B, 0x00, 0x00,
    0x75, 0x54,
    0x49, 0x83, 0x7A, 0x08, 0x00,
    0x75, 0x3D,
    0x48, 0x83, 0xEC, 0x28,
    0x49, 0xC7, 0x42, 0x08, 0x01, 0x00, 0x00, 0x00,
    0x49, 0xBB,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0xBA, 0xE8, 0x03, 0x00, 0x00,
    0x45, 0x33, 0xC0,
    0xB8, 0x00, 0x00, 0x96, 0x43,
    0x66, 0x0F, 0x6E, 0xD8,
    0x49, 0x8B, 0x4A, 0x10,
    0xC6, 0x81, 0x70, 0x01, 0x00, 0x00, 0x01,
    0x49, 0x8B, 0x4A, 0x50,
    0x41, 0xFF, 0xD3,
    0x48, 0x83, 0xC4, 0x28,
    0x49, 0x8B, 0x4A, 0x50,
    0x49, 0x8B, 0x52, 0x58,
    0x4D, 0x8B, 0x42, 0x60,
    0x4D, 0x8B, 0x4A, 0x68,
    /* --- 20 字节原始 prologue --- */
    0x48, 0x89, 0x5C, 0x24, 0x08,
    0x48, 0x89, 0x6C, 0x24, 0x10,
    0x48, 0x89, 0x74, 0x24, 0x18,
    0x57,
    0x48, 0x83, 0xEC, 0x60,
    0xFF, 0x25, 0x00, 0x00, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
};

static void install_stub(HMODULE ua, const char *tag, DWORD rva,
                         const BYTE *orig, int len,
                         BYTE *tmpl, int tmplLen, int offTail,
                         const ULONGLONG *patches, const int *patchOff, int npatch)
{
    BYTE *fn = (BYTE *)ua + rva;
    BYTE *stub;
    BYTE  entry[32];
    char  hs[256];
    int   i;

    hexdump(fn, len, hs, sizeof(hs));
    logf("[%s] %p 当前: %s", tag, fn, hs);

    if (fn[0] == 0xFF && fn[1] == 0x25) {
        logf("[%s] 已经是补丁状态，跳过", tag);
        return;
    }
    if (!is_original(fn, orig, len)) {
        logf("[%s] !! 原始字节不匹配，拒绝打补丁", tag);
        return;
    }

    stub = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!stub) {
        logf("[%s] !! VirtualAlloc 失败 err=%lu", tag, GetLastError());
        return;
    }

    for (i = 0; i < npatch; i++)
        *(ULONGLONG *)(tmpl + patchOff[i]) = patches[i];
    *(ULONGLONG *)(tmpl + offTail) = (ULONGLONG)(fn + len);
    memcpy(stub, tmpl, tmplLen);
    FlushInstructionCache(GetCurrentProcess(), stub, tmplLen);

    memset(entry, 0xCC, sizeof(entry));
    entry[0] = 0xFF;
    entry[1] = 0x25;
    entry[2] = 0x00; entry[3] = 0x00;
    entry[4] = 0x00; entry[5] = 0x00;
    *(ULONGLONG *)(entry + 6) = (ULONGLONG)stub;

    if (!write_code(fn, entry, (DWORD)len)) {
        logf("[%s] !! 写入口跳转失败 err=%lu", tag, GetLastError());
        return;
    }
    if (memcmp(fn, entry, len) == 0)
        logf("[%s] OK（stub @ %p）", tag, stub);
    else
        logf("[%s] !! 写入后回读不一致", tag);
}

static void apply_begin(HMODULE ua)
{
    ULONGLONG p[1];
    int       o[1];
    p[0] = (ULONGLONG)&g_rec;
    o[0] = STUB_BEG_OFF_REC;
    install_stub(ua, "begin", RVA_BEGIN, ORIG_BEGIN, LEN_BEGIN,
                 g_stub_beg, STUB_BEG_LEN, STUB_BEG_OFF_TAIL, p, o, 1);
}

static void apply_smode(HMODULE ua)
{
    ULONGLONG p[2];
    int       o[2];
    p[0] = (ULONGLONG)&g_rec;
    o[0] = STUB_SM_OFF_REC;
    p[1] = (ULONGLONG)((BYTE *)ua + RVA_EBTIME);
    o[1] = STUB_SM_OFF_FN;
    install_stub(ua, "smode", RVA_SMODE, ORIG_SMODE, LEN_SMODE,
                 g_stub_sm, STUB_SM_LEN, STUB_SM_OFF_TAIL, p, o, 2);
}

/* ---- v17: 进关时启动游戏自己的额外得分计时器 ----------------------------
 * LevelScoreManager.SetLevelExtraScore(long)  RVA 0x024BF220   rcx = this(LevelScoreManager)
 * 进关时必被调用（LevelDesignManager.SetLevelExtraScore -> 这里）。
 * 在入口处顺手调一次
 *     StartAutoChangeLevelExtraScoreByTime(this, 1000, 0, 300.0f)  RVA 0x3453D10
 * 把计时器拉起来；之后游戏每帧会调 DoAutoChangeLevelExtraScore，
 * 由 dae stub 负责把数值转交给 InLevelMainPageContext.OnLevelExtraScoreChanged。
 *
 * ★ float 参数走 xmm3，普通 C 函数指针传不进去，所以直接在机器码里
 *   mov eax,0x43960000(=300.0f) ; movd xmm3,eax
 *
 *  off  bytes                     asm
 *    0  49 ba <imm64>             mov  r10,&g_rec
 *    a  4d 8b 42 78               mov  r8,[r10+0x78]   ; ctx（未定位就跳过）
 *    e  4d 85 c0                  test r8,r8
 *   11  74 3c                    je   skip
 *   13  48 83 ec 38               sub  rsp,0x38
 *   17  48 89 4c 24 20            mov  [rsp+0x20],rcx  ; 存 this
 *   1c  48 89 54 24 28            mov  [rsp+0x28],rdx  ; 存 score
 *   21  48 c7 c2 e8 03 00 00      mov  rdx,1000        ; initScore
 *   28  45 33 c0                 xor  r8d,r8d         ; finalScore = 0
 *   2b  b8 00 00 96 43            mov  eax,0x43960000  ; 300.0f
 *   30  66 0f 6e d8               movd xmm3,eax        ; durationTime
 *   34  49 bb <imm64>             mov  r11,StartAutoChangeLevelExtraScoreByTime
 *   3e  41 ff d3                  call r11
 *   41  48 8b 4c 24 20            mov  rcx,[rsp+0x20]
 *   46  48 8b 54 24 28            mov  rdx,[rsp+0x28]
 *   4b  48 83 c4 38               add  rsp,0x38
 *   4f  <16 字节原始 prologue>     skip:
 *   5f  ff 25 00 00 00 00         jmp  qword [rip+0]
 *   65  <8 字节 fn+16>
 *   总长 0x6D = 109
 */
#define RVA_SLE     0x024BF220
#define LEN_SLE     16
#define STUB_SLE_LEN      109
#define STUB_SLE_OFF_REC1 2
#define STUB_SLE_OFF_FN   54    /* 0x36 */
#define STUB_SLE_OFF_TAIL 101   /* 0x65 = jmp(0x5F)+6 */

static const BYTE ORIG_SLE[LEN_SLE] = {
    0x48, 0x89, 0x5C, 0x24, 0x08,
    0x57,
    0x48, 0x83, 0xEC, 0x40,
    0x48, 0x8B, 0xD9,
    0x48, 0x8B, 0xFA
};

static BYTE g_stub_sle[STUB_SLE_LEN] = {
    0x49, 0xBA,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x4D, 0x8B, 0x42, 0x78,
    0x4D, 0x85, 0xC0,
    0x90, 0x90,          /* v17b: 去掉 ctx 守卫，进关无条件启动计时 */
    0x48, 0x83, 0xEC, 0x38,
    0x48, 0x89, 0x4C, 0x24, 0x20,
    0x48, 0x89, 0x54, 0x24, 0x28,
    0x48, 0xC7, 0xC2, 0xE8, 0x03, 0x00, 0x00,
    0x45, 0x33, 0xC0,
    0xB8, 0x00, 0x00, 0x96, 0x43,
    0x66, 0x0F, 0x6E, 0xD8,
    0x49, 0xBB,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x41, 0xFF, 0xD3,
    0x48, 0x8B, 0x4C, 0x24, 0x20,
    0x48, 0x8B, 0x54, 0x24, 0x28,
    0x48, 0x83, 0xC4, 0x38,
    /* --- 16 字节原始 prologue --- */
    0x48, 0x89, 0x5C, 0x24, 0x08,
    0x57,
    0x48, 0x83, 0xEC, 0x40,
    0x48, 0x8B, 0xD9,
    0x48, 0x8B, 0xFA,
    0xFF, 0x25, 0x00, 0x00, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
};

static void apply_sle(HMODULE ua)
{
    ULONGLONG p[2];
    int       o[2];
    p[0] = (ULONGLONG)&g_rec;                       o[0] = STUB_SLE_OFF_REC1;
    p[1] = (ULONGLONG)((BYTE *)ua + 0x03453D10);    o[1] = STUB_SLE_OFF_FN;
    install_stub(ua, "sle", RVA_SLE, ORIG_SLE, LEN_SLE,
                 g_stub_sle, STUB_SLE_LEN, STUB_SLE_OFF_TAIL, p, o, 2);
}

/* ---- v16: 每帧驱动原生 HUD（挂 UpdateExtraScoreTimer，自算倒计时）---------
 * LevelScorePlugin.UpdateExtraScoreTimer()  RVA 0x02A9CB70   rcx = this
 *
 * 为什么换掉 v14 的 DoAutoChangeLevelExtraScore：
 *   那个只在「游戏自己的计时器已启动」时才每帧跑，而迪拉克里计时器从不启动
 *   （起计时的 Lua 在 476 行崩了），所以 v14 一次都没被触发。
 *
 * 这里改成：只要 g_rec.ctx 非空，就自己用 GetTickCount 数 300 秒，
 * 值 = 1000 * (1 - elapsed/300s)，再交给游戏自己的
 * InLevelMainPageContext.OnLevelExtraScoreChanged(ctx, 值, 0) 去写 Text。
 *
 * 入口 14 字节（2+4+3+5，不含相对跳转，可安全复制）：
 *   40 53 / 48 83 EC 20 / 48 8B D9 / B9 41 52 00 00
 */
#define RVA_DAE     0x024B6C90
#define LEN_DAE     19
#define STUB_DAE_LEN      86
#define STUB_DAE_OFF_REC1 2
#define STUB_DAE_OFF_FN   33    /* 0x21 */
#define STUB_DAE_OFF_TAIL 78    /* 0x4E = jmp(0x48)+6 */

static const BYTE ORIG_DAE[LEN_DAE] = {
    0x40, 0x56,
    0x48, 0x83, 0xEC, 0x50,
    0x48, 0x8B, 0xF1,
    0x0F, 0x29, 0x74, 0x24, 0x40,
    0xB9, 0x93, 0xA8, 0x00, 0x00
};

static void   *g_on_extra = 0;
static DWORD (*g_tick)(void) = 0;

/* 游戏线程上执行：算倒计时并交给游戏自己的更新函数 */
static void hud_tick(OWREC *r)
{
    ULONGLONG ctx;
    DWORD     now, el;
    LONG64    v;

    if (!g_on_extra || !g_tick) return;
    ctx = r->ctx;
    if (!ctx) return;

    now = g_tick();
    if (r->hudStart == 0) r->hudStart = (ULONGLONG)now;
    el = (DWORD)(now - (DWORD)r->hudStart);
    v  = 1000 - (LONG64)((ULONGLONG)el * 1000ULL / 300000ULL);
    if (v < 0) v = 0;

    ((void (*)(void *, LONG64, LONG64))g_on_extra)((void *)ctx, v, 0);
    r->hudHits++;
}

static BYTE g_stub_dae[STUB_DAE_LEN] = {
    0x49, 0xBA,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x4D, 0x8B, 0x42, 0x78,
    0x4D, 0x85, 0xC0,
    0x74, 0x22,
    0x48, 0x83, 0xEC, 0x38,
    0x48, 0x89, 0x4C, 0x24, 0x20,
    0x49, 0x8B, 0xCA,
    0x49, 0xBB,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x41, 0xFF, 0xD3,
    0x48, 0x8B, 0x4C, 0x24, 0x20,
    0x48, 0x83, 0xC4, 0x38,
    /* --- 19 字节原始 prologue --- */
    0x40, 0x56,
    0x48, 0x83, 0xEC, 0x50,
    0x48, 0x8B, 0xF1,
    0x0F, 0x29, 0x74, 0x24, 0x40,
    0xB9, 0x93, 0xA8, 0x00, 0x00,
    0xFF, 0x25, 0x00, 0x00, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
};

static void apply_dae(HMODULE ua)
{
    ULONGLONG p[2];
    int       o[2];

    g_on_extra = (void *)((BYTE *)ua + 0x02251000);
    g_tick     = (DWORD (*)(void))GetProcAddress(GetModuleHandleA("kernel32.dll"), "GetTickCount");

    p[0] = (ULONGLONG)&g_rec;                     o[0] = STUB_DAE_OFF_REC1;
    p[1] = (ULONGLONG)&hud_tick;                  o[1] = STUB_DAE_OFF_FN;
    install_stub(ua, "dae", RVA_DAE, ORIG_DAE, LEN_DAE,
                 g_stub_dae, STUB_DAE_LEN, STUB_DAE_OFF_TAIL, p, o, 2);
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

static DWORD WINAPI watch_owboss(LPVOID param)
{
    ULONGLONG last = 0;
    int i;
    (void)param;
    for (i = 0; i < 7200; i++) {            /* 观测 2 小时，够打一整期 */
        Sleep(1000);
        if (g_ow.hits != last) {
            logf("[owboss] 第 %llu 次调用：group=%lu battle=%lu time=%lu | 已补分 %llu 次(extra=%lu)",
                 (unsigned long long)g_ow.hits, (unsigned long)g_ow.group,
                 (unsigned long)g_ow.battle, (unsigned long)g_ow.time,
                 (unsigned long long)g_ow.applied, (unsigned long)g_ow.extra);
            last = g_ow.hits;
        }
    }
    logf("[owboss] 观测结束，总调用 %llu 次，补分 %llu 次",
         (unsigned long long)g_ow.hits, (unsigned long long)g_ow.applied);
    return 0;
}

static DWORD WINAPI watch_eb(LPVOID param)
{
    ULONGLONG last = 0;
    int i;
    (void)param;
    for (i = 0; i < 7200; i++) {
        Sleep(1000);
        if (g_eb.hits != last) {
            union { ULONGLONG u; float f; } d;
            d.u = g_eb.dur[0];
            logf("[ebtime] 第 %llu 次调用：init=%lld final=%lld dur=%g | 已改 %llu 次",
                 (unsigned long long)g_eb.hits,
                 (long long)g_eb.init, (long long)g_eb.final,
                 (double)d.f, (unsigned long long)g_eb.applied);
            last = g_eb.hits;
        }
    }
    logf("[ebtime] 观测结束，总调用 %llu 次，改参 %llu 次",
         (unsigned long long)g_eb.hits, (unsigned long long)g_eb.applied);
    return 0;
}

static DWORD WINAPI watch_rec(LPVOID param)
{
    ULONGLONG la = 0, lb = 0, lr = 0;
    int i;
    (void)param;
    for (i = 0; i < 7200; i++) {
        Sleep(1000);
        if (g_rec.hitsA != la) {
            logf("[begin] 第 %llu 次：lsm=%p mapID=%lu areaID=%lu progress=%lu hard=%lu diff=%lu extraRaw=%016llX",
                 (unsigned long long)g_rec.hitsA, (void *)(ULONG_PTR)g_rec.lsm,
                 (unsigned long)g_rec.mapId, (unsigned long)g_rec.areaId,
                 (unsigned long)g_rec.progress, (unsigned long)g_rec.hardLevel,
                 (unsigned long)g_rec.difficulty,
                 (unsigned long long)g_rec.extraRaw);
            la = g_rec.hitsA;
        }
        if (g_rec.hitsB != lb) {
            logf("[smode] 第 %llu 次：show=%u playAnim=%u isExtra=%u textmap=%p areaID=%lu | 已主动开倒计时=%llu",
                 (unsigned long long)g_rec.hitsB,
                 (unsigned)(g_rec.r8v & 0xFF), (unsigned)(g_rec.r9v & 0xFF),
                 (unsigned)g_rec.isExtra, (void *)(ULONG_PTR)g_rec.textmap,
                 (unsigned long)g_rec.areaId,
                 (unsigned long long)g_rec.done);
            lb = g_rec.hitsB;
        }
        /* 轮询 LevelScoreManager+0x3C0（levelExtraScore，SafeInt64 密文）看它变不变。
         * 用 ReadProcessMemory 读自己，地址失效时只会返回 FALSE，不会崩。 */
        if (g_rec.lsm != 0) {
            ULONGLONG v = 0;
            SIZE_T got = 0;
            if (ReadProcessMemory(GetCurrentProcess(),
                                  (LPCVOID)((BYTE *)(ULONG_PTR)g_rec.lsm + 0x3C0),
                                  &v, sizeof(v), &got) && got == sizeof(v)) {
                if (v != lr) {
                    logf("[extra] lsm+0x3C0 = %016llX", (unsigned long long)v);
                    lr = v;
                }
            }
        }
    }
    logf("[begin/smode] 观测结束：begin=%llu smode=%llu 开倒计时=%llu",
         (unsigned long long)g_rec.hitsA, (unsigned long long)g_rec.hitsB,
         (unsigned long long)g_rec.done);
    return 0;
}

/* ---------------- 主线程 ---------------------------------------------------- */
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

    /* BOSS 额外得分补丁：DLL 同目录放一个 owboss_off.txt 就整块跳过 */
    {
        char off[MAX_PATH];
        sibling_path(off, "owboss_off.txt");
        if (off[0] && GetFileAttributesA(off) != INVALID_FILE_ATTRIBUTES) {
            logf("[owboss] 发现 owboss_off.txt，跳过本补丁");
        } else {
            /* v15: 停用 -- apply_owboss(ua); */
        }
    }

    /* 额外得分倒计时起始值：DLL 同目录放 ebtime_off.txt 跳过 */
    {
        char off[MAX_PATH];
        sibling_path(off, "ebtime_off.txt");
        if (off[0] && GetFileAttributesA(off) != INVALID_FILE_ATTRIBUTES) {
            logf("[ebtime] 发现 ebtime_off.txt，跳过本补丁");
        } else {
            apply_ebtime(ua);
        }
    }

    /* 关卡开始记录 + 主动开倒计时：DLL 同目录放 ebauto_off.txt 跳过 */
    {
        char off[MAX_PATH];
        sibling_path(off, "ebauto_off.txt");
        if (off[0] && GetFileAttributesA(off) != INVALID_FILE_ATTRIBUTES) {
            logf("[begin/smode] 发现 ebauto_off.txt，跳过本补丁");
        } else {
            /* v15: 停用 -- apply_begin(ua); */
            /* v15: 停用 -- apply_smode(ua); */
            apply_sle(ua);
            apply_dae(ua);
        }
    }

    CreateThread(NULL, 0, watch, NULL, 0, NULL);
    CreateThread(NULL, 0, watch_owboss, NULL, 0, NULL);
    CreateThread(NULL, 0, watch_eb, NULL, 0, NULL);
    CreateThread(NULL, 0, watch_rec, NULL, 0, NULL);

    logf("=== bh3bypass done ===");
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
