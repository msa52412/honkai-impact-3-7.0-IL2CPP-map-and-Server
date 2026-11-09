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
 *  本文件现在做两件事：
 *    A. 让客户端信任本地资源包（下面那 6 处 patch）。
 *    B. 世界地图 -> 挑战 的模式入口（v16 新增，见文件尾部「模式入口」段）。
 *
 * ===========================================================================
 *  v16 新增：模式入口常驻化（2026-11-07）
 *
 *  以前这块靠 rehook.bat 现场装：python 先 arm 一个「信箱」，再现场搜三张
 *  页签找到目标卡片，把 ExcelMeta 指针写死进 stub，最后改 DoClickAction 入口。
 *  每次开游戏都得重跑一遍，面板重建/换图就失效。
 *
 *  搬进 DLL 后不再需要信箱，也不再需要预存任何堆指针 —— 点击那一刻
 *  DoClickAction 的 rcx 就是容器对象，顺着
 *      [rcx+0xC8] -> ContainerData
 *      [cd +0x18] -> ExcelMeta
 *      [meta+0x20] -> 配置行 ID
 *  纯内存读三步就能判定是不是目标卡。判据用「配置行 ID」（跨进程稳定），
 *  不用指针（重启就变），也不用 FeatureType（那是位图，且要走托管调用）。
 *
 *  stub 只做判定 + 打标记，**不在游戏线程里调任何托管方法**；
 *  GoToOWEndless 由 DLL 自己的线程延迟 120ms 执行，时序与原来
 *  rehook.bat 的「先让原逻辑跑完、再异步补一刀」完全一致。
 *
 *  配置 app\bh3bypass.ini（不写就用默认值）：
 *      [entry]
 *      enabled = 1      1=启用入口 0=只留资源校验
 *      card_id = 11     目标卡片的 ExcelMeta.ID
 *      mode    = 0      0=迪拉克之海 1=量子奇点
 *      unlock  = 1      1=打「忽略 entry 级锁」补丁（原本 rehook 每次都会打）
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
#include <stdlib.h>
#include <tlhelp32.h>

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

/* ---------------- 模式入口：目标定义 ---------------------------------------- */

/* MonoWorldMapEntryContainerBase.DoClickAction(IList<int>) @ RVA 0x02CFB880
 *   40 55             push rbp
 *   56                push rsi
 *   57                push rdi
 *   41 54             push r12
 *   41 55             push r13
 *   41 56             push r14
 *   41 57             push r15
 *   48 83 ec 70       sub  rsp, 0x70      <- 第 16 字节正好是指令边界
 * 前 16 字节全是 prologue，没碰过 rcx/rdx，所以 stub 里 rcx=this、rdx=list
 * 原样可用，只动用 rax/r9/r11（三者都是易失寄存器）。
 */
#define RVA_DOCLICK     0x02CFB880
#define LEN_DOCLICK     16
static const BYTE ORIG_DOCLICK[LEN_DOCLICK] = {
    0x40, 0x55, 0x56, 0x57, 0x41, 0x54, 0x41, 0x55,
    0x41, 0x56, 0x41, 0x57, 0x48, 0x83, 0xec, 0x70
};

/* 两个要调的托管方法（都在 UserAssembly 里，晚于本 DLL 加载） */
#define RVA_GET_MODULE  0x01A92080   /* MonoWorldMapEndlessEntry.get_module；rcx 传 0 即可  */
#define RVA_GOTO_OW     0x019C5E00   /* OWEndlessModule.GoToOWEndless(module)；this 在 rcx */

/* LJMINEONAFF 循环里的 entry 级锁分支 @ RVA 0x00A52332
 *   83 f8 02          cmp  eax, 2
 *   75 c9             jne  ...
 * 改成 jmp +0x0a / nop×3  => ebx=3 并直接跳出（等价于忽略 entry 级锁）。
 * 这是 rehook 流程里每次都会打的那 5 字节，行为完全一致。 */
#define RVA_UNLOCK      0x00A52332
#define LEN_UNLOCK      5
static const BYTE ORIG_UNLOCK[LEN_UNLOCK]  = { 0x83, 0xf8, 0x02, 0x75, 0xc9 };
static const BYTE PATCH_UNLOCK[LEN_UNLOCK] = { 0xeb, 0x0a, 0x90, 0x90, 0x90 };

/* 容器/配置行偏移（与 wmpick.py 的 RVA_GET_META / meta.ID 一致） */
#define OFF_CD_META     0x18   /* ContainerData + 0x18 = ExcelMeta* */
#define OFF_META_ID     0x20   /* ExcelMeta    + 0x20 = int32 ID */
#define OFF_CD_IN_CONTAINER 0xC8  /* 容器 this + 0xC8 = ContainerData* */

/* ---------------- 卡片改指向：SetupContainerData ---------------------------
 *
 * 2026-11-07 新增。wmv 里槽位的序列化布局是
 *     +0x20 entryID (int32)
 *     +0x24 index   (int32)      <- 同一 EntryID 分组的第几行
 *     +0x28 isFixedEntry (bool)
 * 想直接改成 (9999, 8)（9999=动态入口占位，index 8 = Dirac Sea）：
 *   - entryID 的高字节和 index 全部落在 LZ4 的 match 区，patch_same_length 改不动；
 *   - 重压缩整块再补零到等长 → Unity 的 LZ4_decompress_safe 要求「最后一段
 *     literals 必须精确消耗到输入末尾」，尾部 padding 直接判 corrupted
 *     （实测日志：`CAB-97db9bad... is corrupted! [Position out of bounds!]`）。
 * 所以改成运行时在查表之前改参数。
 *
 * SetupContainerData @ RVA 0x02CFE4C0 的真身（反汇编：
 *   40 57                  push rdi
 *   48 83 ec 20            sub  rsp, 0x20
 *   48 8b f9               mov  rdi, rcx        ; this
 *   b9 5f 0b 00 00         mov  ecx, 0xB5F      ; WorldMap 表 id
 *   e8 ...                 call <检查>
 *   ...
 *   44 8b 47 1c            mov  r8d, [rdi+0x1C] ; index
 *   8b 57 18               mov  edx, [rdi+0x18] ; entryID
 *   e8 ...                 call <查表(entryID, index)>
 *   48 89 87 c8 00 00 00   mov [rdi+0xC8], rax  ; -> ContainerData
 *
 * 前 14 字节正好停在 call 之前，stub 里改完两个字段原样 replay 即可，
 * 不需要 trampoline（没有返回值要保）。判据用「改之前的 entryID」，
 * 命中一次之后该实例的 entryID 已经是 9999，不会重复命中。
 */
#define RVA_SETUPDATA   0x02CFE4C0
#define LEN_SETUPDATA   14
static const BYTE ORIG_SETUPDATA[LEN_SETUPDATA] = {
    0x40, 0x57, 0x48, 0x83, 0xec, 0x20, 0x48, 0x8b, 0xf9,
    0xb9, 0x5f, 0x0b, 0x00, 0x00
};
#define OFF_SLOT_EID    0x18   /* 容器 this + 0x18 = entryID */
#define OFF_SLOT_INDEX  0x1C   /* 容器 this + 0x1C = index   */

/* 量子奇点：module -> TwoEndless -> _currentMode(EndlessStatus) -> endless_type */
#define OFF_TWO_ENDLESS 0x58   /* OWEndlessModule + 0x58 = TwoEndless*  */
#define OFF_CUR_MODE    0x10   /* TwoEndless      + 0x10 = _currentMode */
#define OFF_STATUS_TYPE 0x10   /* EndlessStatus   + 0x10 = endless_type */

/* ---------------- 日志 ------------------------------------------------------ */

static char g_log[MAX_PATH];
static char g_dir[MAX_PATH];        /* DLL 所在目录，带结尾反斜杠；ini 也从这里找 */

static void log_open(HINSTANCE h)
{
    char dir[MAX_PATH];
    DWORD n = GetModuleFileNameA(h, dir, MAX_PATH);
    if (!n || n >= MAX_PATH) { g_log[0] = 0; g_dir[0] = 0; return; }
    while (n > 0 && dir[n - 1] != '\\') n--;
    dir[n] = 0;
    lstrcpyA(g_dir, dir);
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

/* ============================================================================
 *  模式入口：WorldMap -> Challenge 的卡片点击接管
 *
 *  与 rehook.bat 的差别：
 *    rehook.bat   ：python 先 arm 信箱 -> hook SetupAllEntryButton 抓管理器
 *                   -> 现场扫三个页签找 feature 109 的卡片 -> 把 ExcelMeta
 *                   指针**写死**进 stub -> 改 DoClickAction 入口。
 *                   缺点：每次开游戏都要重跑；面板重建/换图后指针失效。
 *   本实现       ：不信箱、不预存指针、不扫列表。点击发生的那一刻
 *                   rcx 就是容器对象，三步内存读就能判出是不是目标卡。
 *
 *  stub（全部是内存读 + 比较，**不调任何托管方法**，因此不会抛异常）：
 *      rax = [rcx+0xC8]          ; ContainerData
 *      if (!rax) goto replay
 *      r9  = [rax+0x18]          ; ExcelMeta
 *      if (!r9)  goto replay
 *      eax = [r9+0x20]           ; 配置行 ID
 *      [last_id] = eax ; [last_meta] = r9
 *      if (eax == card_id) lock inc [hit_seq]
 *  replay:
 *      <原始 16 字节> ; jmp DoClickAction+16
 *
 *  真正的 GoToOWEndless 由 entry_watch 线程在 120ms 后执行 —— 复刻
 *  rehook 那套「先让原点击逻辑跑完，再异步补一刀」的时序，避免和原逻辑抢。
 * ==========================================================================*/

typedef struct {
    LONG      card_id;      /* 目标卡片 ExcelMeta.ID（ini 可改） */
    LONG      mode;         /* 0 = 迪拉克之海，1 = 量子奇点 */
    LONG      hit_seq;      /* stub 命中一次 +1，worker 靠它触发 */
    LONG      last_id;      /* 最近一次点击的卡片 ID（调试用） */
    ULONG_PTR last_meta;    /* 最近一次点击的 ExcelMeta（调试用） */
} ENTRY_CTX;

static ENTRY_CTX g_entry;
static ULONG_PTR g_ret_addr;        /* DoClickAction 的真实返回地址，stub 写入、trampoline 读 */
static HMODULE   g_ua;              /* UserAssembly.dll 基址 */
static int       g_entry_enabled = 0;
static int       g_unlock        = 1;
static int       g_delay_ms      = 100;   /* 命中后等多久再进模式（对齐 rehook 的 120ms） */
static volatile LONG g_reentering = 0;

/* 卡片改指向：[card] eid_from -> (eid_to, index_to) */
typedef struct {
    LONG eid_from;          /* 命中判据：改之前的 entryID */
    LONG eid_to;            /* 改成这个 entryID（9999 = 动态入口） */
    LONG index_to;          /* 同组第几行（Dirac Sea = 8） */
    volatile LONG hits;     /* 实际改了几次（调试用） */
} CARD_CTX;

static CARD_CTX g_card;
static int      g_card_enabled = 0;

/* 封面替换：[cover] 把某张卡的封面路径换成别的图（不改数据行，不动 wmv） */
typedef struct {
    LONG      id;           /* 命中判据：ContainerData->ExcelMeta.ID */
    volatile LONG hits;     /* 命中次数（调试用） */
} COVER_CTX;

static COVER_CTX g_cover;
static int       g_cover_enabled = 0;
static WCHAR     g_cover_w[256];     /* ini 里的路径，UTF-16 */
static int       g_cover_wlen = 0;
static BYTE      g_cover_buf[0x400];  /* 自建 String 对象（头 0x14 + 正文） */

/* 标题图替换：[titleimg] —— 卡面标题是**美术字图片**（Image），不是 Text。
 * 路径来自 ContainerData 第 3 列的 getter @0x00A52620（和封面的 0xA51940 平行）。 */
static COVER_CTX g_tcover;
static int       g_tcover_enabled = 0;
static WCHAR     g_tcover_w[256];
static int       g_tcover_wlen = 0;
static BYTE      g_tcover_buf[0x400];

/* [m43kit] 刻晴 M43（24244）武器槽 -> 借用 M42 已烘焙的技能（2026-11-08）
 *
 * WeaponData 行内布局（实测，与 app\tools\keqing_m43_kit.py 的 SLOT 一致）：
 *   每槽 28 字节 = int skillID + float p1 p2 p3 + float p1Add p2Add p3Add
 *   槽间距 0x1C，三槽共 0x54
 * 原始值（客户端解析出来的，也是 server WeaponData.json 里的）：
 *   24244 -> 1160 / 2211 / 2212   三个技能在客户端 1758 条烘焙表里一条都没有
 * 目标值（默认）：
 *   24244 -> 1159 / 0 / 0         1159 = Skill_Mei_Weapon_42，刻晴剑气主动
 * 实测确认：槽位是**实时读取**的，改完进战斗就有剑气。 */
#define M43_SIG1        1160
#define M43_SIG2        2211
#define M43_SIG3        2212
#define M43_SLOT        0x1C
#define M43_ROW_LEN     (M43_SLOT * 3)

static int       g_m43_enabled = 0;
static int       g_m43_want[3] = { 1159, 0, 0 };
static BYTE      g_m43_target[M43_ROW_LEN];
static ULONGLONG g_m43_row = 0;          /* 已改写的行地址（缓存，失效后重扫） */
static DWORD     g_m43_next_scan = 0;

/* 自建 String 用的 klass：从 Text::set_text 的入参里抓一个真实 System.String 的类指针。
 * 标题图那一路原函数可能返回空串/无效串，不能像封面那样直接抄 [rax]。 */
static volatile LONG64 g_str_klass = 0;

/* 标签文字替换：[label] —— 走 Text::OnEnable，而**不是** set_text
 * （那行"当前虚境挑战书"是预制体里的静态文本，游戏从不 set_text） */
static int g_luastate_enabled = 1;   /* Lua 状态捕获 */
static int g_qtwave_enabled = 1;   /* 量子多波保护 */

/* [qtpatch] 量子补丁改主线程执行的开关和参数；实现见文件后半段。
 * 声明必须放在这里，因为 load_cfg() 在上面就要读它们。 */
typedef void (*qt_sethook_t)(void *L, void *fn, int mask, int count);
typedef int  (*qt_loadstr_t)(void *L, const char *s);
typedef int  (*qt_pcall_t)(void *L, int nargs, int nres, int ef);
typedef int  (*qt_gettop_t)(void *L);
typedef void (*qt_settop_t)(void *L, int idx);

static int g_qt_enabled = 0;   /* ini [qtpatch] enabled */
static int g_qt_retry   = 12;  /* 前 N 次用密集间隔（赶进关窗口） */
static int g_qt_gap     = 1200;/* 密集间隔（毫秒） */
static int g_qt_idle    = 8000;/* N 次之后降到这个间隔（维持性重打） */
static int   g_label_enabled = 0;
static WCHAR g_label_from[64];
static int   g_label_fromlen = 0;
static WCHAR g_label_to[128];
static int   g_label_tolen = 0;
static BYTE  g_label_buf[0x400];
static float g_label_col[4] = { 1.0f, 1.0f, 1.0f, 1.0f };
static int   g_label_align  = 3;   /* TextAnchor::MiddleLeft；-1 = 不改 */
static int   g_label_nowrap = 1;   /* HorizontalWrapMode::Overflow；-1 = 不改 */
static float g_label_shift  = 0.0f;  /* 左移像素；0 = 不动 */
static ULONGLONG g_label_done_tr = 0; /* 已经推过的 RectTransform，每个对象只推一次 */
static float g_label_vec[4];          /* get_anchoredPosition 的 sret 缓冲 */
static BYTE *g_label_ua = 0;
/* 父节点对齐：HorizontalLayoutGroup.childAlignment @ +0x20（5=右 3=左） */
static int   g_group_align = 3;
static WCHAR g_lg_w[40];
static int   g_lg_wlen = 0;
static BYTE  g_lg_buf[0x80];
static WCHAR g_hide_w[40];
static int   g_hide_wlen = 0;
static int   g_hide_idx = 2;   /* Progress 下第几个子节点是 Num（-1 = 不处理） */
static BYTE  g_hide_buf[0x80];
static ULONGLONG g_lg_klass = 0;   /* 从 m_Text 抄来的 System.String klass */
static ULONGLONG g_label_par = 0;  /* Text 的父 Transform（Progress） */
static volatile LONG g_label_hits = 0;
static volatile LONG g_label_lg_busy = 0;
static volatile LONG g_label_lg_done = 0;  /* 隐藏 Num / 改父布局只做一次 */
static volatile LONG g_label_calls = 0;

/* 标题替换：把某段文字整体换掉（内容匹配） */
static int   g_title_enabled = 0;
static WCHAR g_title_from[64];
static int   g_title_fromlen = 0;
static WCHAR g_title_to[128];
static int   g_title_tolen = 0;
static BYTE  g_title_buf[0x200];
static volatile LONG g_title_hits = 0;
static volatile LONG g_title_calls = 0;
static float  g_title_col[4] = { 1.0f, 1.0f, 1.0f, 1.0f };  /* 命中时写进 Graphic::m_Color（+0x20） */


static int  g_kq_enabled   = 1;      /* ini [keqing] visible，默认开 */
static int  g_kq_visible_int = 1;    /* ini [keqing] visible_int：int 重载也拦一遍 */
static LONG g_kq_avatar_id = 2001;   /* ini [keqing] avatar_id       */

static int  g_gacha_popup  = 1;      /* ini [gacha] popup：角色卡也弹新物品 */
static int  g_gacha_newbox = 0;      /* ini [gacha] newbox：强制 3D 开箱（有黑屏风险） */
static int  g_gacha_settle = 1;      /* ini [gacha] settle：结算页逐格弹出 */
static int  g_gacha_glow   = 1;      /* ini [gacha] glow：角色卡彩光恒稀有 */

/* 刻晴武器技能（Katana_M43）注册表注入 —— 阶段 1 只观测 */
static int   g_kqskill_probe  = 1;   /* ini [keqing_skill] probe */
static int   g_kqskill_inject = 0;   /* ini [keqing_skill] inject（阶段 2，暂未实现） */
static BYTE *g_kqskill_page   = NULL; /* stub 页：+0x000 stub，+0x800 count，+0x804 lastkey */

static void load_cfg(void)
{
    char ini[MAX_PATH];

    g_entry.card_id = 11;
    g_entry.mode    = 0;
    if (!g_dir[0]) { g_entry_enabled = 1; return; }

    lstrcpyA(ini, g_dir);
    lstrcatA(ini, "bh3bypass.ini");

    g_entry_enabled = (int)GetPrivateProfileIntA("entry", "enabled", 1, ini);
    g_entry.card_id = (LONG)GetPrivateProfileIntA("entry", "card_id", 11, ini);
    g_entry.mode    = (LONG)GetPrivateProfileIntA("entry", "mode", 0, ini);
    g_unlock        = (int)GetPrivateProfileIntA("entry", "unlock", 1, ini);
    g_delay_ms      = (int)GetPrivateProfileIntA("entry", "delay_ms", 100, ini);
    if (g_delay_ms < 0)   g_delay_ms = 0;
    if (g_delay_ms > 1000) g_delay_ms = 1000;

    g_kq_enabled   = (int)GetPrivateProfileIntA("keqing", "visible", 1, ini);
    g_kq_visible_int = (int)GetPrivateProfileIntA("keqing", "visible_int", 1, ini);
    g_kq_avatar_id = (LONG)GetPrivateProfileIntA("keqing", "avatar_id", 2001, ini);
    if (g_kq_avatar_id <= 0) g_kq_avatar_id = 2001;
    logf("[keqing] visible=%d  visible_int=%d  avatar_id=%ld", g_kq_enabled, g_kq_visible_int, g_kq_avatar_id);

    g_gacha_popup  = (int)GetPrivateProfileIntA("gacha", "popup",  1, ini);
    g_gacha_newbox = (int)GetPrivateProfileIntA("gacha", "newbox", 0, ini);
    g_gacha_settle = (int)GetPrivateProfileIntA("gacha", "settle", 1, ini);
    g_gacha_glow   = (int)GetPrivateProfileIntA("gacha", "glow",   1, ini);
    logf("[gacha] popup=%d  newbox=%d  settle=%d  glow=%d",
         g_gacha_popup, g_gacha_newbox, g_gacha_settle, g_gacha_glow);

    g_kqskill_probe  = (int)GetPrivateProfileIntA("keqing_skill", "probe",  1, ini);
    g_kqskill_inject = (int)GetPrivateProfileIntA("keqing_skill", "inject", 0, ini);
    logf("[kqskill] probe=%d  inject=%d", g_kqskill_probe, g_kqskill_inject);

    g_m43_enabled  = (int)GetPrivateProfileIntA("m43kit", "enabled", 1, ini);
    g_m43_want[0]  = (int)GetPrivateProfileIntA("m43kit", "slot1", 1159, ini);
    g_m43_want[1]  = (int)GetPrivateProfileIntA("m43kit", "slot2",    0, ini);
    g_m43_want[2]  = (int)GetPrivateProfileIntA("m43kit", "slot3",    0, ini);
    logf("[m43kit] enabled=%d  slot=%d/%d/%d",
         g_m43_enabled, g_m43_want[0], g_m43_want[1], g_m43_want[2]);

    g_card_enabled    = (int)GetPrivateProfileIntA("card", "enabled", 1, ini);    g_card.eid_from   = (LONG)GetPrivateProfileIntA("card", "eid_from", 1013, ini);
    g_card.eid_to     = (LONG)GetPrivateProfileIntA("card", "eid_to", 9999, ini);
    g_card.index_to   = (LONG)GetPrivateProfileIntA("card", "index_to", 8, ini);

    g_cover_enabled = (int)GetPrivateProfileIntA("cover", "enabled", 0, ini);
    g_cover.id      = (LONG)GetPrivateProfileIntA("cover", "id", 11, ini);
    {
        char path[256];
        path[0] = 0;
        GetPrivateProfileStringA("cover", "path", "", path, sizeof(path), ini);
        g_cover_wlen = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS,
                                           path, -1, g_cover_w, 256);
        if (g_cover_wlen <= 0)
            g_cover_wlen = MultiByteToWideChar(CP_ACP, 0, path, -1, g_cover_w, 256);
        if (g_cover_wlen > 0) g_cover_wlen--;      /* 去掉结尾 NUL，得到字符数 */
        if (g_cover_wlen < 0) g_cover_wlen = 0;
        if (g_cover_wlen == 0) {
            g_cover_enabled = 0;
            logf("[cover] !! [cover] path 为空，自动关闭");
        } else if (g_cover_wlen * 2 + 0x18 > (int)sizeof(g_cover_buf)) {
            g_cover_enabled = 0;
            logf("[cover] !! path 太长（%d 字符），自动关闭", g_cover_wlen);
        }
        logf("[cover] enabled=%d  id=%ld  path=\"%s\"（%d 字符）",
             g_cover_enabled, g_cover.id, path, g_cover_wlen);
    }

    g_tcover_enabled = (int)GetPrivateProfileIntA("titleimg", "enabled", 0, ini);
    g_tcover.id      = (LONG)GetPrivateProfileIntA("titleimg", "id", 11, ini);
    {
        char path[256];
        path[0] = 0;
        GetPrivateProfileStringA("titleimg", "path", "", path, sizeof(path), ini);
        g_tcover_wlen = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS,
                                            path, -1, g_tcover_w, 256);
        if (g_tcover_wlen <= 0)
            g_tcover_wlen = MultiByteToWideChar(CP_ACP, 0, path, -1, g_tcover_w, 256);
        if (g_tcover_wlen > 0) g_tcover_wlen--;
        if (g_tcover_wlen < 0) g_tcover_wlen = 0;
        if (g_tcover_wlen == 0) {
            g_tcover_enabled = 0;
            logf("[titleimg] !! path 为空，自动关闭");
        } else if (g_tcover_wlen * 2 + 0x18 > (int)sizeof(g_tcover_buf)) {
            g_tcover_enabled = 0;
            logf("[titleimg] !! path 太长（%d 字符），自动关闭", g_tcover_wlen);
        }
        logf("[titleimg] enabled=%d  id=%ld  path=\"%s\"（%d 字符）",
             g_tcover_enabled, g_tcover.id, path, g_tcover_wlen);
    }

    g_title_enabled = (int)GetPrivateProfileIntA("title", "enabled", 0, ini);
    {
        char from[256], to[512];
        int  n;

        from[0] = to[0] = 0;
        GetPrivateProfileStringA("title", "from", "", from, sizeof(from), ini);
        GetPrivateProfileStringA("title", "to",   "", to,   sizeof(to),   ini);
        if (!from[0]) lstrcpyA(from, "\xE4\xB8\x87\xE8\xB1\xA1\xE8\x99\x9A\xE5\xA2\x83"); /* 万象虚境 */
        if (!to[0])   lstrcpyA(to,   "\xE8\xBF\xAA\xE6\x8B\x89\xE5\x85\x8B\xE4\xB9\x8B\xE6\xB5\xB7"); /* 迪拉克之海 */

        n = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, from, -1, g_title_from, 64);
        if (n <= 0) n = MultiByteToWideChar(CP_ACP, 0, from, -1, g_title_from, 64);
        g_title_fromlen = (n > 0) ? n - 1 : 0;

        n = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, to, -1, g_title_to, 128);
        if (n <= 0) n = MultiByteToWideChar(CP_ACP, 0, to, -1, g_title_to, 128);
        g_title_tolen = (n > 0) ? n - 1 : 0;

        if (g_title_fromlen <= 0 || g_title_fromlen >= 60 ||
            g_title_tolen  <= 0 || g_title_tolen  >= 100) {
            g_title_enabled = 0;
            logf("[title] !! from/to 长度异常（%d / %d），自动关闭",
                 g_title_fromlen, g_title_tolen);
        }
        logf("[title] enabled=%d  \"%ls\"(%d) -> \"%ls\"(%d)",
             g_title_enabled, g_title_from, g_title_fromlen,
             g_title_to, g_title_tolen);
        {
            char cs[16];
            cs[0] = 0;
            GetPrivateProfileStringA("title", "color", "", cs, sizeof(cs), ini);
            if (cs[0]) {
                unsigned v = 0;
                int k;
                for (k = 0; cs[k] && k < 8; k++) {
                    char c = cs[k];
                    int d;
                    if (c >= '0' && c <= '9')      d = c - '0';
                    else if (c >= 'a' && c <= 'f') d = c - 'a' + 10;
                    else if (c >= 'A' && c <= 'F') d = c - 'A' + 10;
                    else continue;
                    v = (v << 4) | (unsigned)d;
                }
                g_title_col[0] = (float)((v >> 16) & 0xFF) / 255.0f;
                g_title_col[1] = (float)((v >>  8) & 0xFF) / 255.0f;
                g_title_col[2] = (float)( v        & 0xFF) / 255.0f;
                g_title_col[3] = 1.0f;
                logf("[title] color=#%06X", v & 0xFFFFFF);
            }
        }

    g_luastate_enabled = (int)GetPrivateProfileIntA("luastate", "enabled", 1, ini);
    logf("[luastate] enabled=%d", g_luastate_enabled);
    g_qtwave_enabled = (int)GetPrivateProfileIntA("qtwave", "enabled", 1, ini);
    logf("[qtwave] enabled=%d", g_qtwave_enabled);
    g_qt_enabled = (int)GetPrivateProfileIntA("qtpatch", "enabled", 1, ini);
    g_qt_retry   = (int)GetPrivateProfileIntA("qtpatch", "retry", 12, ini);
    g_qt_gap     = (int)GetPrivateProfileIntA("qtpatch", "gap_ms", 1200, ini);
    g_qt_idle    = (int)GetPrivateProfileIntA("qtpatch", "idle_ms", 8000, ini);
    if (g_qt_retry < 1)   { g_qt_retry = 1; }
    if (g_qt_gap   < 100) { g_qt_gap = 100; }
    if (g_qt_idle  < 1000) { g_qt_idle = 1000; }
    logf("[qtpatch] enabled=%d retry=%d gap=%dms idle=%dms",
         g_qt_enabled, g_qt_retry, g_qt_gap, g_qt_idle);

    g_label_enabled = (int)GetPrivateProfileIntA("label", "enabled", 0, ini);
    {
        char from[128], to[256], cs[16];
        from[0] = to[0] = cs[0] = 0;
        GetPrivateProfileStringA("label", "from", "", from, sizeof(from), ini);
        GetPrivateProfileStringA("label", "to",   "", to,   sizeof(to),   ini);
        GetPrivateProfileStringA("label", "color", "", cs,  sizeof(cs),  ini);
        {
            int n = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, from, -1, g_label_from, 64);
            if (n <= 0) n = MultiByteToWideChar(CP_ACP, 0, from, -1, g_label_from, 64);
            g_label_fromlen = (n > 0) ? n - 1 : 0;
            n = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, to, -1, g_label_to, 128);
            if (n <= 0) n = MultiByteToWideChar(CP_ACP, 0, to, -1, g_label_to, 128);
            g_label_tolen = (n > 0) ? n - 1 : 0;
        }
        if (g_label_fromlen <= 0 || g_label_fromlen >= 60 ||
            g_label_tolen  <= 0 || g_label_tolen  >= 100) {
            g_label_enabled = 0;
            logf("[label] !! from/to 长度异常（%d / %d），自动关闭",
                 g_label_fromlen, g_label_tolen);
        }
        if (cs[0]) {
            unsigned v = 0;
            int k;
            for (k = 0; cs[k] && k < 8; k++) {
                char c = cs[k];
                int d;
                if (c >= '0' && c <= '9')      d = c - '0';
                else if (c >= 'a' && c <= 'f') d = c - 'a' + 10;
                else if (c >= 'A' && c <= 'F') d = c - 'A' + 10;
                else continue;
                v = (v << 4) | (unsigned)d;
            }
            g_label_col[0] = (float)((v >> 16) & 0xFF) / 255.0f;
            g_label_col[1] = (float)((v >>  8) & 0xFF) / 255.0f;
            g_label_col[2] = (float)( v        & 0xFF) / 255.0f;
            g_label_col[3] = 1.0f;
        }
        g_label_align  = (int)GetPrivateProfileIntA("label", "align",  3, ini);
        g_label_nowrap = (int)GetPrivateProfileIntA("label", "nowrap", 1, ini);
        {
            char sx[32];
            sx[0] = 0;
            GetPrivateProfileStringA("label", "shift_x", "", sx, sizeof(sx), ini);
            if (sx[0]) g_label_shift = (float)atof(sx);
        }
        g_group_align = (int)GetPrivateProfileIntA("label", "group_align", 3, ini);
        {
            char hn[40];
            int k;
            hn[0] = 0;
            GetPrivateProfileStringA("label", "hide", "", hn, sizeof(hn), ini);
            for (k = 0; hn[k] && k < 39; k++) g_hide_w[k] = (WCHAR)hn[k];
            g_hide_w[k] = 0;
            g_hide_wlen = k;
        }
        {
            const char *cname = "HorizontalLayoutGroup";
            int k;
            for (k = 0; cname[k] && k < 39; k++) g_lg_w[k] = (WCHAR)cname[k];
            g_lg_wlen = k;
        }
        logf("[label] enabled=%d  align=%d nowrap=%d  (from %d 字 -> to %d 字)",
             g_label_enabled, g_label_align, g_label_nowrap,
             g_label_fromlen, g_label_tolen);
    }
    }

    logf("[entry] cfg %s  enabled=%d card_id=%ld mode=%d unlock=%d delay_ms=%d",
         ini, g_entry_enabled, g_entry.card_id, g_entry.mode, g_unlock, g_delay_ms);
    logf("[card] enabled=%d  %ld -> (eid=%ld, index=%ld)",
         g_card_enabled, g_card.eid_from, g_card.eid_to, g_card.index_to);
}

/* ---- stub 生成 ---------------------------------------------------------- */

static BYTE *emit_bytes(BYTE *p, const BYTE *src, int n)
{
    memcpy(p, src, n);
    return p + n;
}

static BYTE *emit_mov_r11(BYTE *p, const void *addr)
{
    ULONGLONG v = (ULONGLONG)(ULONG_PTR)addr;
    *p++ = 0x49;
    *p++ = 0xBB;                        /* mov r11, imm64 */
    memcpy(p, &v, 8);
    return p + 8;
}

static BYTE *emit_mov_rax(BYTE *p, const void *addr)
{
    ULONGLONG v = (ULONGLONG)(ULONG_PTR)addr;
    *p++ = 0x48;
    *p++ = 0xB8;                        /* mov rax, imm64 */
    memcpy(p, &v, 8);
    return p + 8;
}

static void entry_on_return(void);      /* 前向声明，stub 里要取它地址 */

static BYTE *build_entry_stub(BYTE *out, BYTE *ua, BYTE *tramp)
{
    BYTE *p = out;
    int   j_replay_a, j_replay_b, j_hit, j_skip;
    int   replay, hit;

    /* mov rax, [rcx+0xC8] */
    {
        static const BYTE m[] = { 0x48, 0x8B, 0x81, OFF_CD_IN_CONTAINER, 0x00, 0x00, 0x00 };
        p = emit_bytes(p, m, sizeof(m));
    }
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;               /* test rax, rax */
    j_replay_a = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    *p++ = 0x4C; *p++ = 0x8B; *p++ = 0x48; *p++ = OFF_CD_META;  /* mov r9,[rax+0x18] */
    *p++ = 0x4D; *p++ = 0x85; *p++ = 0xC9;               /* test r9, r9 */
    j_replay_b = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    *p++ = 0x41; *p++ = 0x8B; *p++ = 0x41; *p++ = OFF_META_ID;  /* mov eax,[r9+0x20] */

    p = emit_mov_r11(p, &g_entry.last_id);
    *p++ = 0x41; *p++ = 0x89; *p++ = 0x03;               /* mov [r11], eax */
    p = emit_mov_r11(p, &g_entry.last_meta);
    *p++ = 0x4D; *p++ = 0x89; *p++ = 0x0B;               /* mov [r11], r9 */

    p = emit_mov_r11(p, &g_entry.card_id);
    *p++ = 0x41; *p++ = 0x3B; *p++ = 0x03;               /* cmp eax, [r11]（REX.B 不能省） */
    j_hit = (int)(p - out); *p++ = 0x74; *p++ = 0x00;    /* jz hit */

    j_skip = (int)(p - out); *p++ = 0xEB; *p++ = 0x00;   /* jmp replay */

    /* ---- 命中 ----
     * 不动返回值、不调任何东西，只做一件事：把栈上的返回地址换成 trampoline，
     * 并把原返回地址存进 g_ret_addr。等 DoClickAction 完整跑完 ret 回来，
     * 才会落到 trampoline —— 那时是同一个游戏线程，栈也已干净。
     * 这就是当初 rehook 那套「先让原逻辑跑完、再异步补一刀」的精确版，
     * 而且完全没有跨线程调 il2cpp 的风险。 */
    hit = (int)(p - out);
    p = emit_mov_r11(p, &g_entry.hit_seq);
    *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;  /* lock inc dword [r11] */

    *p++ = 0x4C; *p++ = 0x8B; *p++ = 0x1C; *p++ = 0x24;  /* mov r11, [rsp]（原返回地址） */
    p = emit_mov_rax(p, &g_ret_addr);
    *p++ = 0x4C; *p++ = 0x89; *p++ = 0x18;               /* mov [rax], r11 */
    p = emit_mov_rax(p, tramp);
    *p++ = 0x48; *p++ = 0x89; *p++ = 0x04; *p++ = 0x24;  /* mov [rsp], rax（换掉返回地址） */

    replay = (int)(p - out);
    p = emit_bytes(p, ORIG_DOCLICK, LEN_DOCLICK);
    *p++ = 0xFF; *p++ = 0x25;                            /* jmp qword [rip+0] */
    *p++ = 0x00; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;
    {
        ULONGLONG v = (ULONGLONG)(ULONG_PTR)(ua + RVA_DOCLICK + LEN_DOCLICK);
        memcpy(p, &v, 8);
        p += 8;
    }

    /* 回填相对跳转（全在 127 字节内） */
    out[j_replay_a + 1] = (BYTE)(replay - (j_replay_a + 2));
    out[j_replay_b + 1] = (BYTE)(replay - (j_replay_b + 2));
    out[j_hit + 1]      = (BYTE)(hit    - (j_hit + 2));
    out[j_skip + 1]     = (BYTE)(replay - (j_skip + 2));

    return p;
}

/* trampoline：DoClickAction 已经 ret，rsp 已恢复到调用者状态（mod 16 == 0）。
 * 这里保存全部易失寄存器 → 调 C 函数干活 → 原样恢复 → 跳回真正的返回地址。
 * 唯一必须保住的是 rax（DoClickAction 的返回值，调用者可能会用）。 */
static BYTE *build_trampoline(BYTE *out)
{
    BYTE *p = out;

    *p++ = 0x50;                                         /* push rax */
    *p++ = 0x51;                                         /* push rcx */
    *p++ = 0x52;                                         /* push rdx */
    *p++ = 0x41; *p++ = 0x50;                            /* push r8  */
    *p++ = 0x41; *p++ = 0x51;                            /* push r9  */
    *p++ = 0x41; *p++ = 0x52;                            /* push r10 */
    *p++ = 0x41; *p++ = 0x53;                            /* push r11 */
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x28;  /* sub rsp, 0x28（影子空间+对齐） */

    p = emit_mov_rax(p, &entry_on_return);
    *p++ = 0xFF; *p++ = 0xD0;                            /* call rax */

    *p++ = 0x48; *p++ = 0x83; *p++ = 0xC4; *p++ = 0x28;  /* add rsp, 0x28 */
    *p++ = 0x41; *p++ = 0x5B;                            /* pop r11 */
    *p++ = 0x41; *p++ = 0x5A;                            /* pop r10 */
    *p++ = 0x41; *p++ = 0x59;                            /* pop r9  */
    *p++ = 0x41; *p++ = 0x58;                            /* pop r8  */
    *p++ = 0x5A;                                         /* pop rdx */
    *p++ = 0x59;                                         /* pop rcx */
    *p++ = 0x41; *p++ = 0x5A;                            /* pop r10  <- 先接住 rax */
    p = emit_mov_rax(p, &g_ret_addr);
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x00;               /* mov rax, [rax]（真正返回地址） */
    *p++ = 0x49; *p++ = 0x92;                            /* xchg rax, r10 */
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE2;               /* jmp r10 */

    return p;
}

/* ---- 落盘 --------------------------------------------------------------- */

static void apply_unlock(HMODULE ua)
{
    BYTE *at = (BYTE *)ua + RVA_UNLOCK;
    BYTE  chk[LEN_UNLOCK];
    char  hs[256];

    hexdump(at, LEN_UNLOCK, hs, sizeof(hs));
    logf("[unlock] %p 当前: %s", at, hs);

    if (memcmp(at, PATCH_UNLOCK, LEN_UNLOCK) == 0) {
        logf("[unlock] 已在位，跳过");
        return;
    }
    if (!is_original(at, ORIG_UNLOCK, LEN_UNLOCK)) {
        logf("[unlock] !! 原始字节不匹配，拒绝打补丁");
        return;
    }
    if (!write_code(at, PATCH_UNLOCK, LEN_UNLOCK)) {
        logf("[unlock] !! 写入失败 err=%lu", GetLastError());
        return;
    }
    memcpy(chk, at, LEN_UNLOCK);
    if (memcmp(chk, PATCH_UNLOCK, LEN_UNLOCK) == 0)
        logf("[unlock] OK  忽略 entry 级锁");
    else
        logf("[unlock] !! 写入后回读不一致");
}

static void apply_entry(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_DOCLICK;
    BYTE *stub;
    BYTE *end;
    BYTE  entry[LEN_DOCLICK];
    char  hs[256];

    hexdump(fn, LEN_DOCLICK, hs, sizeof(hs));
    logf("[entry] DoClickAction %p 当前: %s", fn, hs);

    if (fn[0] == 0xFF && fn[1] == 0x25) {
        logf("[entry] 已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_DOCLICK, LEN_DOCLICK)) {
        logf("[entry] !! 原始字节不匹配，拒绝打补丁（客户端版本变了？）");
        return;
    }

    stub = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!stub) {
        logf("[entry] !! VirtualAlloc 失败 err=%lu", GetLastError());
        return;
    }
    {
        BYTE *tramp = stub + 0x200;
        BYTE *tend;
        end  = build_entry_stub(stub, (BYTE *)ua, tramp);
        tend = build_trampoline(tramp);
        FlushInstructionCache(GetCurrentProcess(), stub, (DWORD)(end - stub));
        FlushInstructionCache(GetCurrentProcess(), tramp, (DWORD)(tend - tramp));
        logf("[entry] stub @ %p（%d 字节）  trampoline @ %p（%d 字节）",
             stub, (int)(end - stub), tramp, (int)(tend - tramp));
    }

    memset(entry, 0xCC, sizeof(entry));
    entry[0] = 0xFF;
    entry[1] = 0x25;
    entry[2] = entry[3] = entry[4] = entry[5] = 0x00;
    *(ULONGLONG *)(entry + 6) = (ULONGLONG)(ULONG_PTR)stub;

    if (!write_code(fn, entry, LEN_DOCLICK)) {
        logf("[entry] !! 写入口跳转失败 err=%lu", GetLastError());
        return;
    }
    if (memcmp(fn, entry, LEN_DOCLICK) == 0)
        logf("[entry] OK  入口已接管（stub @ %p，%d 字节，判据 card_id=%ld）",
             stub, (int)(end - stub), g_entry.card_id);
    else
        logf("[entry] !! 写入后回读不一致");
}

/* ---- 卡片改指向 --------------------------------------------------------- */

static BYTE *build_setup_stub(BYTE *out, BYTE *ua)
{
    BYTE *p = out;
    int   j_replay, replay;
    LONG  from = g_card.eid_from;
    LONG  to   = g_card.eid_to;
    LONG  idx  = g_card.index_to;

    /* mov eax, [rcx+0x18] */
    *p++ = 0x8B; *p++ = 0x41; *p++ = OFF_SLOT_EID;
    /* cmp eax, imm32 */
    *p++ = 0x3D;
    memcpy(p, &from, 4); p += 4;
    /* jne replay */
    j_replay = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
    /* mov dword [rcx+0x18], imm32   ; entryID = eid_to */
    *p++ = 0xC7; *p++ = 0x41; *p++ = OFF_SLOT_EID;
    memcpy(p, &to, 4); p += 4;
    /* mov dword [rcx+0x1C], imm32   ; index = index_to */
    *p++ = 0xC7; *p++ = 0x41; *p++ = OFF_SLOT_INDEX;
    memcpy(p, &idx, 4); p += 4;
    /* lock inc dword [g_card.hits] */
    p = emit_mov_r11(p, &g_card.hits);
    *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;   /* REX.B 不能省 */

    replay = (int)(p - out);
    p = emit_bytes(p, ORIG_SETUPDATA, LEN_SETUPDATA);
    *p++ = 0xFF; *p++ = 0x25;
    *p++ = 0x00; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;
    {
        ULONGLONG v = (ULONGLONG)(ULONG_PTR)(ua + RVA_SETUPDATA + LEN_SETUPDATA);
        memcpy(p, &v, 8);
        p += 8;
    }
    out[j_replay + 1] = (BYTE)(replay - (j_replay + 2));
    return p;
}

static void apply_card(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_SETUPDATA;
    BYTE *stub;
    BYTE *end;
    BYTE  patch[LEN_SETUPDATA];
    char  hs[256];

    hexdump(fn, LEN_SETUPDATA, hs, sizeof(hs));
    logf("[card] SetupContainerData %p 当前: %s", fn, hs);

    if (fn[0] == 0xFF && fn[1] == 0x25) {
        logf("[card] 已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_SETUPDATA, LEN_SETUPDATA)) {
        logf("[card] !! 原始字节不匹配，拒绝打补丁（客户端版本变了？）");
        return;
    }

    stub = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!stub) {
        logf("[card] !! VirtualAlloc 失败 err=%lu", GetLastError());
        return;
    }
    end = build_setup_stub(stub, (BYTE *)ua);
    FlushInstructionCache(GetCurrentProcess(), stub, (DWORD)(end - stub));
    logf("[card] stub @ %p（%d 字节）", stub, (int)(end - stub));

    memset(patch, 0xCC, sizeof(patch));
    patch[0] = 0xFF;
    patch[1] = 0x25;
    patch[2] = patch[3] = patch[4] = patch[5] = 0x00;
    *(ULONGLONG *)(patch + 6) = (ULONGLONG)(ULONG_PTR)stub;

    if (!write_code(fn, patch, LEN_SETUPDATA)) {
        logf("[card] !! 写入口跳转失败 err=%lu", GetLastError());
        return;
    }
    if (memcmp(fn, patch, LEN_SETUPDATA) == 0)
        logf("[card] OK  入口已接管（entryID %ld -> %ld / index -> %ld）",
             g_card.eid_from, g_card.eid_to, g_card.index_to);
    else
        logf("[card] !! 写入后回读不一致");
}

static DWORD WINAPI card_watch(LPVOID param)
{
    LONG last = 0;
    (void)param;
    for (;;) {
        LONG now;
        Sleep(1500);
        now = InterlockedCompareExchange(&g_card.hits, 0, 0);
        if (now != last) {
            last = now;
            logf("[card] 累计改写 %ld 个容器实例", last);
        }
    }
    return 0;
}

/* ---- 封面替换 -----------------------------------------------------------
 *
 * 链路（反汇编实证）：
 *   SetupEntryImage @ 0x02CFE570
 *       call ContainerData::FNBHMJNJAOC(cd)      @ 0x0A51940  <- 这里
 *       if (IsNullOrEmpty(path)) return;          @ 0x64FFAE0
 *       SetImageAsync(entryImage,       path)     @ 0x2CFE000
 *       SetImageAsync([this+0x28],      path)     @ 0x2CFDEB0
 *       SetImageAsync([this+0x88],      path)     @ 0x2CFDEB0
 *
 * FNBHMJNJAOC 就是「取本行第 2 列（封面）」，返回 Il2CppString*。
 * 它在 getter 里查 ExcelMeta 的列缓存，所以运行时换缓存指针能立刻生效，
 * 但重启就没了（缓存是新进程里重新填的）。
 *
 * 这里改在 getter 出口：命中目标行 ID 时，先调原函数拿到真实 String，
 * 再把它的对象头（klass + monitor）原样拷进自己的缓冲区、正文换成 ini 里的
 * 路径，返回这个自建对象。指针每次都不同 -> 调用方不会命中「同一张图」的
 * 缓存判断，也不需要动 wmv / Excel / 列缓存。
 *
 * 为什么不用「就地改原 String 的正文」：那要求新路径不比旧路径长
 * （"SpriteOutput/WorldMap/OWEndless/Cover_8" 39 字符，比很多卡自己的封面长），
 * 自建缓冲区没有长度上限。
 *
 * 返回的是 DLL .data 里的静态缓冲区：进程内地址固定、永不释放，GC 扫到
 * 一个堆外指针只会忽略，不会崩。
 *
 * 入口 14 字节（两条指令，正好停在第 1 个 call 之前）整体接管：
 *   40 53                  push rbx
 *   48 83 EC 20            sub  rsp, 0x20
 *   48 8B D9               mov  rbx, rcx        ; this = ContainerData
 *   B9 A7 0C 00 00         mov  ecx, 0xCA7      ; 类初始化 token
 * 所以 trampoline 必须把这 14 字节**全部**回放，再跳到 0xA5194E（那条 call）。
 * ★ 2026-11-07 17:34 踩过：一开始只回放前 6 字节就跳 0xA51946，
 *   而 0xA51946 已经被 FF 25 这条 14 字节跳转吃掉了，落地在指令中间，
 *   崩在 0xA51954（RIP，写 0x1b）。stub 长度必须 = 被覆盖长度。
 */
#define RVA_COVER_GET   0x00A51940
#define LEN_COVER_GET   14
static const BYTE ORIG_COVER_GET[LEN_COVER_GET] = {
    0x40, 0x53, 0x48, 0x83, 0xEC, 0x20, 0x48, 0x8B, 0xD9,
    0xB9, 0xA7, 0x0C, 0x00, 0x00
};

static BYTE *build_cover_stub(BYTE *out, BYTE *ua, BYTE *tramp)
{
    BYTE *p = out;
    int   j_null_meta, j_not_id, j_null_ret, j_zero, j_hit;
    int   at_null, at_replay, at_hit, at_loop;
    LONG  id = g_cover.id;
    int   n  = g_cover_wlen;

    /* 注意 rel8 只能跳 ±127：replay 段紧跟判定之后，命中段再放它后面，
     * 否则「跳过命中段」两个条件跳转会溢出成负数（实测 0x80 会变成往回跳）。 */
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x41; *p++ = OFF_CD_META;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;               /* test rax, rax */
    j_null_meta = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    *p++ = 0x8B; *p++ = 0x40; *p++ = OFF_META_ID;        /* mov eax, [rax+0x20] */
    *p++ = 0x3D;                                        /* cmp eax, imm32 */
    memcpy(p, &id, 4); p += 4;
    j_not_id = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    /* 命中 -> 跳过 replay 段，直接去 hit 段（绝对地址，没有 127 字节限制） */
    j_hit = (int)(p - out);
    p = emit_mov_rax(p, NULL);                           /* 占位，稍后回填 */
    *p++ = 0xFF; *p++ = 0xE0;                            /* jmp rax */

    at_replay = (int)(p - out);
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;                            /* jmp rax */

    at_hit = (int)(p - out);
    *(ULONGLONG *)(out + j_hit + 2) = (ULONGLONG)(ULONG_PTR)(out + at_hit);

    /* ---- 命中：先跑原函数，再把结果替换成自己的 String ---- */
    p = emit_mov_r11(p, &g_cover.hits);
    *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;  /* lock inc dword [r11] */

    *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x28;  /* sub rsp, 0x28（对齐） */
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xD0;                            /* call rax */
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xC4; *p++ = 0x28;  /* add rsp, 0x28 */
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;               /* test rax, rax */
    j_null_ret = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    /* 拷对象头：klass / monitor */
    p = emit_mov_r11(p, &g_cover_buf);
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x10;               /* mov rdx, [rax] */
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x13;               /* mov [r11], rdx */
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x50; *p++ = 0x08;  /* mov rdx, [rax+8] */
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x53; *p++ = 0x08;  /* mov [r11+8], rdx */
    /* 长度 = n */
    *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;  /* mov dword [r11+0x10], n */
    memcpy(p, &n, 4); p += 4;

    /* 逐字节拷 UTF-16 正文（不用 rsi/rdi，避免破坏被调用者保存寄存器） */
    {
        ULONGLONG src = (ULONGLONG)(ULONG_PTR)g_cover_w;
        LONG      bytes = (LONG)n * 2;
        *p++ = 0x48; *p++ = 0xBA;                        /* mov rdx, g_cover_w */
        memcpy(p, &src, 8); p += 8;
        *p++ = 0x41; *p++ = 0xBA;                        /* mov r10d, n*2 */
        memcpy(p, &bytes, 4); p += 4;
    }
    at_loop = (int)(p - out);
    *p++ = 0x8A; *p++ = 0x02;                            /* mov al, [rdx] */
    *p++ = 0x41; *p++ = 0x88; *p++ = 0x43; *p++ = 0x14;  /* mov [r11+0x14], al */
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;               /* inc r11 */
    *p++ = 0x48; *p++ = 0xFF; *p++ = 0xC2;               /* inc rdx */
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;               /* dec r10d */
    j_zero = (int)(p - out); *p++ = 0x75; *p++ = 0x00;   /* jnz loop */

    p = emit_mov_rax(p, &g_cover_buf);
    *p++ = 0xC3;                                         /* ret */

    at_null = (int)(p - out);
    *p++ = 0x33; *p++ = 0xC0;                            /* xor eax, eax */
    *p++ = 0xC3;                                         /* ret */

    out[j_null_meta + 1] = (BYTE)(at_replay - (j_null_meta + 2));
    out[j_not_id    + 1] = (BYTE)(at_replay - (j_not_id + 2));
    out[j_null_ret  + 1] = (BYTE)(at_null   - (j_null_ret + 2));
    out[j_zero      + 1] = (BYTE)(at_loop   - (j_zero + 2));
    return p;
}

static void apply_cover(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_COVER_GET;
    BYTE *stub;
    BYTE *end;
    BYTE  patch[14];
    char  hs[256];

    hexdump(fn, LEN_COVER_GET, hs, sizeof(hs));
    logf("[cover] FNBHMJNJAOC %p 当前: %s", fn, hs);

    if (fn[0] == 0xFF && fn[1] == 0x25) {
        logf("[cover] 已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_COVER_GET, LEN_COVER_GET)) {
        logf("[cover] !! 原始字节不匹配，拒绝打补丁（客户端版本变了？）");
        return;
    }

    stub = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!stub) {
        logf("[cover] !! VirtualAlloc 失败 err=%lu", GetLastError());
        return;
    }
    {
        BYTE *tramp = stub + 0x200;
        BYTE *tend;
        end  = build_cover_stub(stub, (BYTE *)ua, tramp);
        tend = emit_bytes(tramp, ORIG_COVER_GET, LEN_COVER_GET);
        *tend++ = 0xFF; *tend++ = 0x25;
        *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00;
        {
            ULONGLONG v = (ULONGLONG)(ULONG_PTR)((BYTE *)ua + RVA_COVER_GET + LEN_COVER_GET);
            memcpy(tend, &v, 8);
            tend += 8;
        }
        FlushInstructionCache(GetCurrentProcess(), stub, (DWORD)(end - stub));
        FlushInstructionCache(GetCurrentProcess(), tramp, (DWORD)(tend - tramp));
        logf("[cover] stub @ %p（%d 字节）  trampoline @ %p（%d 字节）",
             stub, (int)(end - stub), tramp, (int)(tend - tramp));
    }

    patch[0] = 0xFF;
    patch[1] = 0x25;
    patch[2] = patch[3] = patch[4] = patch[5] = 0x00;
    *(ULONGLONG *)(patch + 6) = (ULONGLONG)(ULONG_PTR)stub;

    if (!write_code(fn, patch, sizeof(patch))) {
        logf("[cover] !! 写入口跳转失败 err=%lu", GetLastError());
        return;
    }
    if (memcmp(fn, patch, sizeof(patch)) == 0)
        logf("[cover] OK  入口已接管（ExcelMeta.ID=%ld 的封面换成 ini 里的路径）",
             g_cover.id);
    else
        logf("[cover] !! 写入后回读不一致");
}

static DWORD WINAPI cover_watch(LPVOID param)
{
    LONG last = 0;
    (void)param;
    for (;;) {
        LONG now;
        Sleep(1500);
        now = InterlockedCompareExchange(&g_cover.hits, 0, 0);
        if (now != last) {
            last = now;
            logf("[cover] 已替换 %ld 次封面路径", last);
        }
    }
    return 0;
}

/* ============================ 标题美术字（图片） ============================
 * 实证：卡面标题不是 Text，是 Task_N/Cover/TitleImg 这个 Image，
 * 贴图 Cover_Title6（万象虚境）来自预制体默认值 —— 数据行第 3 列是空串，
 * 所以 SetupTitleImage(0x2CFE710) 里 IsNullOrEmpty 直接 return，图片保持默认。
 *
 *   rcx = [this+0xC8]            ; ContainerData
 *   call 0xA52620                ; 第 3 列 getter -> 标题图路径
 *   if (IsNullOrEmpty(path)) return;
 *   SetImageAsync(this, [this+0x48], path)
 *
 * 所以挂 0xA52620 出口：命中目标行就把返回值换成自己的路径串，
 * 原函数的 IsNullOrEmpty 会认为「有值」，接着 SetImageAsync 就换成新图。
 * 入口 14 字节：40 53 / 48 83 EC 20 / 48 8B D9 / B9 8A 0C 00 00
 */
#define RVA_TCOVER_GET   0x00A52620
#define LEN_TCOVER_GET   14
static const BYTE ORIG_TCOVER_GET[LEN_TCOVER_GET] = {
    0x40, 0x53, 0x48, 0x83, 0xEC, 0x20, 0x48, 0x8B, 0xD9,
    0xB9, 0x8A, 0x0C, 0x00, 0x00
};

static BYTE *build_tcover_stub(BYTE *out, BYTE *tramp)
{
    BYTE *p = out;
    int   j_null_meta, j_not_id, j_hit, j_klass, j_ret0, j_loop;
    int   at_replay, at_hit, at_have, at_ret0, at_loop;
    LONG  id = g_tcover.id;
    int   n  = g_tcover_wlen;
    LONG  bytes = (LONG)n * 2;
    ULONGLONG fromaddr = (ULONGLONG)(ULONG_PTR)g_tcover_w;
    ULONGLONG bufaddr  = (ULONGLONG)(ULONG_PTR)g_tcover_buf;

    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x41; *p++ = OFF_CD_META;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
    j_null_meta = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    *p++ = 0x8B; *p++ = 0x40; *p++ = OFF_META_ID;
    *p++ = 0x3D;
    memcpy(p, &id, 4); p += 4;
    j_not_id = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    j_hit = (int)(p - out);
    p = emit_mov_rax(p, NULL);
    *p++ = 0xFF; *p++ = 0xE0;

    at_replay = (int)(p - out);
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;

    at_hit = (int)(p - out);
    *(ULONGLONG *)(out + j_hit + 2) = (ULONGLONG)(ULONG_PTR)(out + at_hit);

    /* 计数 */
    p = emit_mov_r11(p, &g_tcover.hits);
    *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;

    /* 先跑原函数（rcx 仍是 ContainerData），拿它的返回值当后备 klass */
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x28;
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xD0;
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xC4; *p++ = 0x28;

    *p++ = 0x49; *p++ = 0x89; *p++ = 0xC2;               /* mov r10, rax */

    p = emit_mov_r11(p, &g_tcover_buf);                  /* r11 = buf */

    p = emit_mov_rax(p, &g_str_klass);
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x00;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
    j_klass = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    *p++ = 0x4D; *p++ = 0x85; *p++ = 0xD2;               /* test r10,r10 */
    j_ret0 = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
    *p++ = 0x49; *p++ = 0x8B; *p++ = 0x02;               /* mov rax,[r10] */

    at_have = (int)(p - out);
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;               /* mov [r11], rax */
    *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;
    { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
    *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;
    memcpy(p, &n, 4); p += 4;

    *p++ = 0x48; *p++ = 0xBA; memcpy(p, &fromaddr, 8); p += 8;
    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &bytes, 4); p += 4;

    at_loop = (int)(p - out);
    *p++ = 0x8A; *p++ = 0x02;
    *p++ = 0x41; *p++ = 0x88; *p++ = 0x43; *p++ = 0x14;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
    *p++ = 0x48; *p++ = 0xFF; *p++ = 0xC2;
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
    j_loop = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    p = emit_mov_rax(p, &g_tcover_buf);
    *p++ = 0xC3;

    at_ret0 = (int)(p - out);
    *p++ = 0x33; *p++ = 0xC0;
    *p++ = 0xC3;

    out[j_null_meta + 1] = (BYTE)(at_replay - (j_null_meta + 2));
    out[j_not_id    + 1] = (BYTE)(at_replay - (j_not_id   + 2));
    out[j_klass     + 1] = (BYTE)(at_have   - (j_klass    + 2));
    out[j_ret0      + 1] = (BYTE)(at_ret0   - (j_ret0     + 2));
    out[j_loop      + 1] = (BYTE)(at_loop   - (j_loop     + 2));
    return p;
}

static void apply_tcover(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_TCOVER_GET;
    BYTE *stub, *end;
    BYTE  patch[14];
    char  hs[256];

    hexdump(fn, LEN_TCOVER_GET, hs, sizeof(hs));
    logf("[titleimg] 标题图 getter %p 当前: %s", fn, hs);

    if (fn[0] == 0xFF && fn[1] == 0x25) {
        logf("[titleimg] 已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_TCOVER_GET, LEN_TCOVER_GET)) {
        logf("[titleimg] !! 原始字节不匹配，拒绝打补丁");
        return;
    }
    stub = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!stub) {
        logf("[titleimg] !! VirtualAlloc 失败 err=%lu", GetLastError());
        return;
    }
    {
        BYTE *tramp = stub + 0x200;
        BYTE *tend;
        end  = build_tcover_stub(stub, tramp);
        tend = emit_bytes(tramp, ORIG_TCOVER_GET, LEN_TCOVER_GET);
        *tend++ = 0xFF; *tend++ = 0x25;
        *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00;
        {
            ULONGLONG v = (ULONGLONG)(ULONG_PTR)((BYTE *)ua + RVA_TCOVER_GET + LEN_TCOVER_GET);
            memcpy(tend, &v, 8);
            tend += 8;
        }
        FlushInstructionCache(GetCurrentProcess(), stub, (DWORD)(end - stub));
        FlushInstructionCache(GetCurrentProcess(), tramp, (DWORD)(tend - tramp));
        logf("[titleimg] stub @ %p（%d 字节）  trampoline @ %p（%d 字节）",
             stub, (int)(end - stub), tramp, (int)(tend - tramp));
    }

    patch[0] = 0xFF;
    patch[1] = 0x25;
    patch[2] = patch[3] = patch[4] = patch[5] = 0x00;
    *(ULONGLONG *)(patch + 6) = (ULONGLONG)(ULONG_PTR)stub;

    if (!write_code(fn, patch, sizeof(patch))) {
        logf("[titleimg] !! 写入口跳转失败 err=%lu", GetLastError());
        return;
    }
    if (memcmp(fn, patch, sizeof(patch)) == 0)
        logf("[titleimg] OK  入口已接管（ExcelMeta.ID=%ld 的标题图换成 ini 路径）",
             g_tcover.id);
    else
        logf("[titleimg] !! 写入后回读不一致");
}

static DWORD WINAPI tcover_watch(LPVOID param)
{
    LONG last = 0;
    (void)param;
    for (;;) {
        LONG now;
        Sleep(1500);
        now = InterlockedCompareExchange(&g_tcover.hits, 0, 0);
        if (now != last) {
            last = now;
            logf("[titleimg] 已替换 %ld 次标题图路径", last);
        }
    }
    return 0;
}

/* ============================ 标签文字（Text::OnEnable） ============================
 * 那行「当前虚境挑战书」是预制体里的静态文本，运行期从不 set_text（实测：
 * set_text 调用 984 次、按内容匹配 0 次）。只能在它被启用那一刻拦。
 *
 * Text::OnEnable @ 0x06905CD0：
 *   40 53            push rbx
 *   48 83 EC 20      sub rsp, 0x20
 *   48 8B D9         mov rbx, rcx          <- 9 字节到此为止
 *   E8 ...           call MaskableGraphic::OnEnable
 * 前 9 字节装不下 14 字节的 FF25 绝对跳转，所以用 E9 rel32 + 4×90，
 * stub 必须落在模块 ±2GB 内（alloc_near）。
 * trampoline 只回放 9 字节，再跳 0x6905CD9 那条 call（原地执行，不被我们破坏）。
 *
 * 命中后（在原 OnEnable 之前）：
 *   [rcx+0xD8] = 自建 String（内容换成 to）
 *   [rcx+0x20..0x2C] = RGBA float（Graphic::m_Color，见 get_color 的 [rdx+0x20]）
 * 随后原 OnEnable 会 SetAllDirty，重建网格时直接用新文本 + 新颜色。
 */
#define RVA_LABEL_ENABLE  0x06905D20
#define LEN_LABEL_ENABLE  23
static const BYTE ORIG_LABEL_ENABLE[LEN_LABEL_ENABLE] = {
    0x48, 0x89, 0x54, 0x24, 0x10,                        /* mov [rsp+0x10], rdx   (5) */
    0x55, 0x53, 0x57,                                    /* push rbp/rbx/rdi      (3) */
    0x48, 0x8D, 0xAC, 0x24, 0x60, 0xFF, 0xFF, 0xFF,      /* lea rbp,[rsp-0xa0]    (8) */
    0x48, 0x81, 0xEC, 0xA0, 0x01, 0x00, 0x00             /* sub rsp,0x1a0         (7) */
};                                                       /* = 23 -> 0x6905D37 */

/* 打补丁时挂起其它线程：热函数被其它线程执行到一半时被我们改写，
 * 会直接执行到写了一半的指令。挂起后再写，写完再放开。 */
#define MAX_SUSPEND 256
static int suspend_other_threads(HANDLE *out, int max)
{
    DWORD  pid = GetCurrentProcessId();
    DWORD  me  = GetCurrentThreadId();
    HANDLE snap;
    THREADENTRY32 te;
    int    n = 0;

    snap = CreateToolhelp32Snapshot(TH32CS_SNAPTHREAD, 0);
    if (snap == INVALID_HANDLE_VALUE) return 0;
    te.dwSize = sizeof(te);
    if (Thread32First(snap, &te)) {
        do {
            HANDLE h;
            if (te.th32OwnerProcessID != pid) continue;
            if (te.th32ThreadID == me) continue;
            if (n >= max) break;
            h = OpenThread(THREAD_SUSPEND_RESUME, FALSE, te.th32ThreadID);
            if (!h) continue;
            if (SuspendThread(h) == (DWORD)-1) { CloseHandle(h); continue; }
            out[n++] = h;
        } while (Thread32Next(snap, &te));
    }
    CloseHandle(snap);
    return n;
}

static void resume_other_threads(HANDLE *h, int n)
{
    int i;
    for (i = 0; i < n; i++) { ResumeThread(h[i]); CloseHandle(h[i]); }
}

/* 在 anchor ±2GB 内找一页可执行内存（E9 rel32 只能跳 ±2GB） */
static BYTE *alloc_near(BYTE *anchor)
{
    ULONGLONG a = (ULONGLONG)(ULONG_PTR)anchor;
    LONGLONG  step;
    for (step = 0x00400000; step < 0x60000000LL; step += 0x00400000) {
        BYTE *q;
        q = (BYTE *)VirtualAlloc((LPVOID)(ULONG_PTR)(a + (ULONGLONG)step), 0x2000,
                                 MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
        if (q) return q;
        q = (BYTE *)VirtualAlloc((LPVOID)(ULONG_PTR)(a - (ULONGLONG)step), 0x2000,
                                 MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
        if (q) return q;
    }
    return NULL;
}

static BYTE *build_label_stub(BYTE *out, BYTE *tramp)
{
    BYTE *p = out;
    int   j_null, j_len, j_cmp, j_loop, j_copy, j_hit;
    int   at_replay, at_hit, at_cmp_loop, at_copy_loop;
    int   flen = g_label_fromlen;
    int   n    = g_label_tolen;
    LONG  fbytes = (LONG)flen * 2;
    LONG  nbytes = (LONG)n * 2;
    ULONGLONG fromaddr = (ULONGLONG)(ULONG_PTR)g_label_from;
    ULONGLONG toaddr   = (ULONGLONG)(ULONG_PTR)g_label_to;
    ULONGLONG bufaddr  = (ULONGLONG)(ULONG_PTR)g_label_buf;

    /* ★ rdx 必须原样保留（它是 VertexHelper），游标一律用 r8 */

    /* 进来先计数：区分「钩子没被调」和「调了但内容不匹配」 */
    p = emit_mov_r11(p, &g_label_calls);
    *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;

    /* rax = m_Text（Text + 0xD8） */
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x81; *p++ = 0xD8; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;                     /* test rax, rax */
    j_null = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    /* cmp dword [rax+0x10], flen */
    *p++ = 0x81; *p++ = 0x78; *p++ = 0x10;
    memcpy(p, &flen, 4); p += 4;
    j_len = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &fbytes, 4); p += 4;   /* mov r10d, flen*2 */
    *p++ = 0x49; *p++ = 0xB8; memcpy(p, &fromaddr, 8); p += 8; /* mov r8, from */
    *p++ = 0x4C; *p++ = 0x8D; *p++ = 0x58; *p++ = 0x14;        /* lea r11,[rax+0x14] */

    at_cmp_loop = (int)(p - out);
    *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;                     /* mov r9b,[r8] */
    *p++ = 0x45; *p++ = 0x3A; *p++ = 0x0B;                     /* cmp r9b,[r11] */
    j_cmp = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;                     /* inc r8 */
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;                     /* inc r11 */
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;                     /* dec r10d */
    j_loop = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    /* 命中 -> 绝对跳过 replay */
    j_hit = (int)(p - out);
    p = emit_mov_rax(p, NULL);
    *p++ = 0xFF; *p++ = 0xE0;

    at_replay = (int)(p - out);
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;

    at_hit = (int)(p - out);
    *(ULONGLONG *)(out + j_hit + 2) = (ULONGLONG)(ULONG_PTR)(out + at_hit);

    /* ---- 命中 ---- */
    p = emit_mov_r11(p, &g_label_hits);
    *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;        /* lock inc [r11] */

    /* 造 String：klass 抄原串的 [rax]；用 r8 当游标 */
    p = emit_mov_r11(p, &g_label_buf);                          /* r11 = buf */
    *p++ = 0x4C; *p++ = 0x8B; *p++ = 0x00;                     /* mov r8,[rax]  klass */
    *p++ = 0x4D; *p++ = 0x89; *p++ = 0x03;                     /* mov [r11],r8 */
    *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;        /* mov qword [r11+8],0 */
    { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
    *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;        /* mov dword [r11+0x10],n */
    memcpy(p, &n, 4); p += 4;

    *p++ = 0x49; *p++ = 0xB8; memcpy(p, &toaddr, 8); p += 8;   /* mov r8,to */
    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &nbytes, 4); p += 4;   /* mov r10d,n*2 */

    at_copy_loop = (int)(p - out);
    *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;                     /* mov r9b,[r8] */
    *p++ = 0x45; *p++ = 0x88; *p++ = 0x4B; *p++ = 0x14;        /* mov [r11+0x14],r9b */
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;                     /* inc r11 */
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;                     /* inc r8 */
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;                     /* dec r10d */
    j_copy = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    /* 字色：Graphic::m_Color @ this+0x20 */
    {
        int c;
        for (c = 0; c < 4; c++) {
            LONG bits;
            memcpy(&bits, &g_label_col[c], 4);
            *p++ = 0xC7; *p++ = 0x41; *p++ = (BYTE)(0x20 + c * 4);
            memcpy(p, &bits, 4); p += 4;
        }
    }
    /* 字体属性：m_FontData=[rcx+0xD0]，alignment @+0x2C，horizontalOverflow @+0x34 */
    if (g_label_align >= 0 || g_label_nowrap >= 0) {
        int j_no_fd;
        *p++ = 0x48; *p++ = 0x8B; *p++ = 0x81; *p++ = 0xD0; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;                  /* test rax,rax */
        j_no_fd = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
        if (g_label_align >= 0) {
            LONG v = g_label_align;
            *p++ = 0xC7; *p++ = 0x40; *p++ = 0x2C;
            memcpy(p, &v, 4); p += 4;
        }
        if (g_label_nowrap >= 0) {
            LONG v = g_label_nowrap;
            *p++ = 0xC7; *p++ = 0x40; *p++ = 0x34;
            memcpy(p, &v, 4); p += 4;
        }
        out[j_no_fd + 1] = (BYTE)((p - out) - (j_no_fd + 2));
    }

    /* m_Text = buf */
    p = emit_mov_rax(p, &g_label_buf);
    *p++ = 0x48; *p++ = 0x89; *p++ = 0x81; *p++ = 0xD8; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;





    /* ---- 左移：rcx=Text, rdx=VertexHelper 都要保护 ---- */
    if (g_label_shift != 0.0f && g_label_ua) {
        int j_skip_all, j_done, at_skip;

        *p++ = 0x51;                                         /* push rcx */
        *p++ = 0x52;                                         /* push rdx */
        *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x28;  /* sub rsp,0x28 */

        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1170));
        *p++ = 0xFF; *p++ = 0xD0;                            /* call rax -> tr */

        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;               /* test rax,rax */
        j_skip_all = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

        p = emit_mov_r11(p, &g_label_done_tr);
        *p++ = 0x49; *p++ = 0x3B; *p++ = 0x03;               /* cmp rax,[r11] */
        j_done = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
        *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;               /* mov [r11],rax */

        /* get_anchoredPosition(rcx=&vec, rdx=tr) */
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC2;               /* mov rdx,rax */
        p = emit_mov_rax(p, &g_label_vec);
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;               /* mov rcx,rax */
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06915700));
        *p++ = 0xFF; *p++ = 0xD0;                            /* call rax */

        /* vec.x -= shift */
        p = emit_mov_r11(p, &g_label_vec);
        *p++ = 0xF3; *p++ = 0x41; *p++ = 0x0F; *p++ = 0x10; *p++ = 0x03;   /* movss xmm0,[r11] */
        p = emit_mov_rax(p, &g_label_shift);
        *p++ = 0xF3; *p++ = 0x0F; *p++ = 0x10; *p++ = 0x08;                /* movss xmm1,[rax] */
        *p++ = 0xF3; *p++ = 0x0F; *p++ = 0x5C; *p++ = 0xC1;                /* subss xmm0,xmm1 */
        *p++ = 0xF3; *p++ = 0x41; *p++ = 0x0F; *p++ = 0x11; *p++ = 0x03;   /* movss [r11],xmm0 */

        /* set_anchoredPosition(rcx=tr, rdx=*(u64*)vec) */
        *p++ = 0x49; *p++ = 0x8B; *p++ = 0x13;               /* mov rdx,[r11] */
        p = emit_mov_rax(p, &g_label_done_tr);
        *p++ = 0x48; *p++ = 0x8B; *p++ = 0x08;               /* mov rcx,[rax] */
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06915AC0));
        *p++ = 0xFF; *p++ = 0xD0;                            /* call rax */

        at_skip = (int)(p - out);
        out[j_skip_all + 1] = (BYTE)(at_skip - (j_skip_all + 2));
        out[j_done     + 1] = (BYTE)(at_skip - (j_done + 2));

        *p++ = 0x48; *p++ = 0x83; *p++ = 0xC4; *p++ = 0x28;  /* add rsp,0x28 */
        *p++ = 0x5A;                                         /* pop rdx */
        *p++ = 0x59;                                         /* pop rcx */
    }

    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;

    out[j_null + 1] = (BYTE)(at_replay - (j_null + 2));
    out[j_len  + 1] = (BYTE)(at_replay - (j_len  + 2));
    out[j_cmp  + 1] = (BYTE)(at_replay - (j_cmp  + 2));
    out[j_loop + 1] = (BYTE)(at_cmp_loop - (j_loop + 2));
    out[j_copy + 1] = (BYTE)(at_copy_loop - (j_copy + 2));
    return p;
}

/* ---- 父节点对齐修正（Text::OnEnable） ----------------------------------
 * 实测：Progress 上挂了 HorizontalLayoutGroup，padding.left=0，
 * 但 childAlignment=5（MiddleRight），所以整行靠右、左边空出一大块。
 * 改成 3（MiddleLeft）即可。
 *
 * Text::OnEnable @ 0x06905CD0 前 9 字节是完整指令，后面紧跟一条 call，
 * 装不下 14 字节绝对跳转 → 用 E9 rel32 + 4×90，stub 放 alloc_near 页。
 */
#define RVA_LABEL_ONENABLE  0x06905CD0
#define LEN_LABEL_ONENABLE  9
static const BYTE ORIG_LABEL_ONENABLE[LEN_LABEL_ONENABLE] = {
    0x40, 0x53, 0x48, 0x83, 0xEC, 0x20, 0x48, 0x8B, 0xD9
};

static BYTE *build_label_lg_stub(BYTE *out, BYTE *tramp)
{
    BYTE *p = out;
    int   j_null, j_len, j_cmp, j_loop, j_hit;
    int   at_replay, at_hit, at_cmp, at_epi;
    int   epi_fix[8], n_epi = 0;
    int   flen = g_label_fromlen;
    LONG  fbytes = (LONG)flen * 2;
    ULONGLONG fromaddr = (ULONGLONG)(ULONG_PTR)g_label_from;
    ULONGLONG lgbuf    = (ULONGLONG)(ULONG_PTR)g_lg_buf;
    ULONGLONG lgw      = (ULONGLONG)(ULONG_PTR)g_lg_w;
    ULONGLONG hidebuf  = (ULONGLONG)(ULONG_PTR)g_hide_buf;
    ULONGLONG hidew    = (ULONGLONG)(ULONG_PTR)g_hide_w;
    ULONGLONG klasssrc = (ULONGLONG)(ULONG_PTR)&g_lg_klass;
    ULONGLONG parsrc   = (ULONGLONG)(ULONG_PTR)&g_label_par;
    LONG  ga = g_group_align;
    int   hlen = (g_hide_wlen > 0) ? g_hide_wlen : 0;

    /* ================= 判定 ================= */
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x81; *p++ = 0xD8; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
    j_null = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    *p++ = 0x81; *p++ = 0x78; *p++ = 0x10;
    memcpy(p, &flen, 4); p += 4;
    j_len = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &fbytes, 4); p += 4;
    *p++ = 0x49; *p++ = 0xB8; memcpy(p, &fromaddr, 8); p += 8;
    *p++ = 0x4C; *p++ = 0x8D; *p++ = 0x58; *p++ = 0x14;

    at_cmp = (int)(p - out);
    *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;
    *p++ = 0x45; *p++ = 0x3A; *p++ = 0x0B;
    j_cmp = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
    j_loop = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    j_hit = (int)(p - out);
    p = emit_mov_rax(p, NULL);
    *p++ = 0xFF; *p++ = 0xE0;

    at_replay = (int)(p - out);
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;

    at_hit = (int)(p - out);
    *(ULONGLONG *)(out + j_hit + 2) = (ULONGLONG)(ULONG_PTR)(out + at_hit);

    /* ================= 命中 ================= */
    *p++ = 0x51;                                                /* push rcx */
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x20;         /* sub rsp,0x20 */

    /* g_lg_klass = [m_Text] */
    p = emit_mov_r11(p, &g_lg_klass);
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x10;                      /* mov rdx,[rax] */
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x13;                      /* mov [r11],rdx */

    /* tr = get_transform(Text) */
    p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1170));
    *p++ = 0xFF; *p++ = 0xD0;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
    { int j = (int)(p - out); *p++ = 0x75; *p++ = 0x0D;
      *p++ = 0x49; *p++ = 0xBB; epi_fix[n_epi++] = (int)(p - out); p += 8;
      *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE3; }

    /* par = get_parent(tr) */
    *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;
    p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06918270));
    *p++ = 0xFF; *p++ = 0xD0;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
    { int j = (int)(p - out); *p++ = 0x75; *p++ = 0x0D;
      *p++ = 0x49; *p++ = 0xBB; epi_fix[n_epi++] = (int)(p - out); p += 8;
      *p++ = 0x41; *p++ = 0xFF; *p++ = 0xE3; }
    p = emit_mov_r11(p, &g_label_par);
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;                      /* mov [r11],rax */

    /* ---------------- hide（去掉 "1/6"） ---------------- */
    if (hlen > 0) {
        LONG  hbytes = (LONG)hlen * 2;
        int   j_nochild, j_nogo, at_hide_done, at_hide_loop;

        /* 构造 "Num" 字符串 */
        p = emit_mov_r11(p, (const void *)(ULONG_PTR)hidebuf);
        p = emit_mov_rax(p, (const void *)(ULONG_PTR)klasssrc);
        *p++ = 0x48; *p++ = 0x8B; *p++ = 0x00;
        *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;
        *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;
        { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
        *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;
        memcpy(p, &hlen, 4); p += 4;
        *p++ = 0x49; *p++ = 0xB8; memcpy(p, &hidew, 8); p += 8;
        *p++ = 0x41; *p++ = 0xBA; memcpy(p, &hbytes, 4); p += 4;
        at_hide_loop = (int)(p - out);
        *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;
        *p++ = 0x45; *p++ = 0x88; *p++ = 0x4B; *p++ = 0x14;
        *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
        *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;
        *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
        { int j = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
          out[j + 1] = (BYTE)(at_hide_loop - (j + 2)); }

        /* child = par.Find("Num") */
        p = emit_mov_rax(p, (const void *)(ULONG_PTR)parsrc);
        *p++ = 0x48; *p++ = 0x8B; *p++ = 0x08;                  /* mov rcx,[rax] */
        *p++ = 0x48; *p++ = 0xBA; memcpy(p, &hidebuf, 8); p += 8;
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06916E90));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        j_nochild = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

        /* go = get_gameObject(child) */
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1130));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        j_nogo = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

        /* SetActive(go, false) */
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;                  /* mov rcx,rax */
        *p++ = 0x31; *p++ = 0xD2;                                /* xor edx,edx */
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06A85CB0));
        *p++ = 0xFF; *p++ = 0xD0;

        at_hide_done = (int)(p - out);
        out[j_nochild + 1] = (BYTE)(at_hide_done - (j_nochild + 2));
        out[j_nogo    + 1] = (BYTE)(at_hide_done - (j_nogo    + 2));
    }

    /* ---------------- align（整行贴左） ---------------- */
    {
        LONG  lbytes = (LONG)g_lg_wlen * 2;
        int   j_nogo2, at_align_loop, at_align_done;

        p = emit_mov_r11(p, (const void *)(ULONG_PTR)lgbuf);
        p = emit_mov_rax(p, (const void *)(ULONG_PTR)klasssrc);
        *p++ = 0x48; *p++ = 0x8B; *p++ = 0x00;
        *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;
        *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;
        { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
        *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;
        memcpy(p, &g_lg_wlen, 4); p += 4;
        *p++ = 0x49; *p++ = 0xB8; memcpy(p, &lgw, 8); p += 8;
        *p++ = 0x41; *p++ = 0xBA; memcpy(p, &lbytes, 4); p += 4;
        at_align_loop = (int)(p - out);
        *p++ = 0x45; *p++ = 0x8A; *p++ = 0x08;
        *p++ = 0x45; *p++ = 0x88; *p++ = 0x4B; *p++ = 0x14;
        *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;
        *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC0;
        *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;
        { int j = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
          out[j + 1] = (BYTE)(at_align_loop - (j + 2)); }

        p = emit_mov_rax(p, (const void *)(ULONG_PTR)parsrc);
        *p++ = 0x48; *p++ = 0x8B; *p++ = 0x08;                  /* mov rcx,[rax] */
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x068B1130));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        j_nogo2 = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
        *p++ = 0x48; *p++ = 0x89; *p++ = 0xC1;
        *p++ = 0x48; *p++ = 0xBA; memcpy(p, &lgbuf, 8); p += 8;
        p = emit_mov_rax(p, (const void *)(g_label_ua + 0x06A859C0));
        *p++ = 0xFF; *p++ = 0xD0;
        *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;
        { int j = (int)(p - out); *p++ = 0x74; *p++ = 0x00;
          *p++ = 0xC7; *p++ = 0x40; *p++ = 0x20;
          memcpy(p, &ga, 4); p += 4;
          out[j + 1] = (BYTE)((p - out) - (j + 2)); }
        at_align_done = (int)(p - out);
        out[j_nogo2 + 1] = (BYTE)(at_align_done - (j_nogo2 + 2));
    }

    at_epi = (int)(p - out);
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xC4; *p++ = 0x20;         /* add rsp,0x20 */
    *p++ = 0x59;                                                /* pop rcx */
    {
        int k;
        for (k = 0; k < n_epi; k++)
            *(ULONGLONG *)(out + epi_fix[k]) = (ULONGLONG)(ULONG_PTR)(out + at_epi);
    }
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;

    out[j_null + 1] = (BYTE)(at_replay - (j_null + 2));
    out[j_len  + 1] = (BYTE)(at_replay - (j_len  + 2));
    out[j_cmp  + 1] = (BYTE)(at_replay - (j_cmp  + 2));
    out[j_loop + 1] = (BYTE)(at_cmp   - (j_loop + 2));
    return p;
}


/* ==================== 量子多波保护（qtwave） ====================
 * QuantumEndlessCommon.lua 的 ChangeHardLevel 在第 2 波之前会索引一张
 * 客户端缺失的表，抛 LuaScriptException -> do_wave 中断 -> 没有刷怪圈。
 * 实测：第 0 层每场只有 1 波（正常），第 1 层起每场 2 波（崩）。
 *       与 layer / 环境 / hardLevel 都无关（都改过，无效）。
 * 做法：在 QTInLevelData.GetBattle(1 参重载) 的返回点把 List<QTWave> 的
 *       _size 截断成 1，Lua 就只会 do 一波，不再进 ChangeHardLevel 的坏分支。
 * 入口 0x011F0854 处 11 字节是函数尾声（mov rbx/add rsp/pop rdi/ret），
 * 用 E9 rel32 + 6 NOP 接管，stub 自己完成收尾，不需要 trampoline。
 */
#define RVA_QTWAVE_TAIL 0x011F0854
#define LEN_QTWAVE_TAIL 11
static const BYTE ORIG_QTWAVE_TAIL[LEN_QTWAVE_TAIL] = {
    0x48, 0x8B, 0x5C, 0x24, 0x30,        /* mov rbx, [rsp+0x30] */
    0x48, 0x83, 0xC4, 0x20,              /* add rsp, 0x20        */
    0x5F,                                /* pop rdi              */
    0xC3                                 /* ret                  */
};

static BYTE *build_qtwave_stub(BYTE *out)
{
    BYTE *p = out;
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC0;          /* test rax, rax */
    *p++ = 0x74; *p++ = 0x07;                       /* jz  +7        */
    *p++ = 0xC7; *p++ = 0x40; *p++ = 0x18;          /* mov dword [rax+0x18], 1 */
    { LONG one = 1; memcpy(p, &one, 4); p += 4; }
    p = emit_bytes(p, ORIG_QTWAVE_TAIL, LEN_QTWAVE_TAIL);
    return p;
}


/* ==================== Lua 状态捕获（luastate） ====================
 * 目标：在运行时拿到 lua_State*，从而可以 lua_dostring 往 Lua 里填表。
 * 为什么不在内存里扫：本作 LuaState 托管对象的字段偏移与标准 Lua 不同，
 *   扫 tt=8 + global_State 自环都验证不过（改了 5 种偏移全不匹配）。
 * 做法：hook LuaDLL.lua_gettop 的入口（RVA 0x05B0A1B0），
 *   它被 LuaInterface 几乎每次调用都会走，第一参数 rcx 就是 lua_State*。
 *   入口 6 字节是两条完整指令：40 53 (push rbx) + 48 83 EC 50 (sub rsp,0x50)。
 */
#define RVA_LUA_GETTOP  0x05B0A1B0
#define LEN_LUA_GETTOP  6
static const BYTE ORIG_LUA_GETTOP[LEN_LUA_GETTOP] = {
    0x40, 0x53,                          /* push rbx      */
    0x48, 0x83, 0xEC, 0x50               /* sub rsp, 0x50 */
};
static volatile LONG64 g_lua_state = 0;
static volatile LONG   g_lua_hits   = 0;

static BYTE *build_luastate_stub(BYTE *out, BYTE *fn, BYTE *back)
{
    BYTE *p = out;
    ULONGLONG g = (ULONGLONG)(ULONG_PTR)&g_lua_state;
    ULONGLONG h = (ULONGLONG)(ULONG_PTR)&g_lua_hits;
    LONG rel;

    *p++ = 0x49; *p++ = 0xBB; memcpy(p, &g, 8); p += 8;   /* movabs r11, &g_lua_state */
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x0B;                /* mov [r11], rcx          */
    *p++ = 0x49; *p++ = 0xBB; memcpy(p, &h, 8); p += 8;   /* movabs r11, &g_lua_hits */
    *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;   /* lock inc dword [r11]    */
    /* 回放被覆盖的 6 字节 */
    *p++ = 0x40; *p++ = 0x53;
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x50;
    /* jmp back */
    rel = (LONG)(back - (p + 5));
    *p++ = 0xE9; memcpy(p, &rel, 4); p += 4;
    (void)fn;
    return p;
}

static void apply_luastate(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_LUA_GETTOP;
    BYTE *back = fn + LEN_LUA_GETTOP;
    BYTE *page, *stub, *end;
    BYTE  patch[LEN_LUA_GETTOP];
    char  hs[256];
    LONG  rel;
    int   i;

    hexdump(fn, LEN_LUA_GETTOP, hs, sizeof(hs));
    logf("[luastate] lua_gettop %p 当前: %s", fn, hs);
    logf("[luastate] &g_lua_state = %p", (void *)&g_lua_state);

    if (fn[0] == 0xE9) { logf("[luastate] 已是补丁状态，跳过"); return; }
    if (!is_original(fn, ORIG_LUA_GETTOP, LEN_LUA_GETTOP)) {
        logf("[luastate] !! 原始字节不匹配，拒绝");
        return;
    }
    page = alloc_near(fn);
    if (!page) { logf("[luastate] !! alloc_near 失败"); return; }
    stub = page;
    end  = build_luastate_stub(stub, fn, back);
    FlushInstructionCache(GetCurrentProcess(), stub, (DWORD)(end - stub));

    rel = (LONG)(stub - (fn + 5));
    patch[0] = 0xE9;
    memcpy(patch + 1, &rel, 4);
    patch[5] = 0x90;
    if (!write_code(fn, patch, sizeof(patch))) {
        logf("[luastate] !! 写入口失败 err=%lu", GetLastError());
        return;
    }
    for (i = 0; i < LEN_LUA_GETTOP; i++) { }
    logf("[luastate] OK  已接管（stub %d 字节）", (int)(end - stub));
}

/* ==================== [qtpatch] 量子补丁改在主线程执行 ====================
 *
 * 为什么重做
 * ----------
 * 原来靠 _ext\tools\qt_watch.py：python 从**外部进程**直接调游戏里的
 * lua_dostring，把五段补丁塞进某个 lua_State。问题是游戏主线程同一时刻
 * 很可能正在这个 VM 里跑，而 lua 不是线程安全的 —— 两条线一起动栈，
 * ulua.dll 里就是访问违例。实测崩溃点固定在 ulua.dll +0x27c450 / +0x27c444，
 * 18:17、18:19、18:26、18:32 连着四次，之前 10:32 / 12:12 / 13:31 / 13:35
 * 也是同一个签名。
 *
 * 现在怎么做
 * ----------
 * 外部线程只干一件事：lua_sethook（写 L 的 hook/mask/count 四个字段，
 * 单位置写，竞争窗口小到可以忽略）。真正的五段 lua 在 **VM 的指令计数回调**
 * 里执行 —— 那一刻一定是游戏主线程、栈是平衡的，不跟任何人抢。
 *
 * 时序：轮询 [luastate] 抓到的 g_lua_state → 出现新 L → 挂 hook（每 3 万条
 * 指令回一次）→ 回调里每隔 gap_ms 试一次，试满 retry 次就摘掉。补丁本身幂等
 * （`__qtfix_fn == orig` 时直接返回 already），重复执行只是多几次编译开销。
 *
 * 非量子场景同样会试满 retry 次（拿不到返回值，判不出 nordy 还是 retry），
 * 12 次 × 5 段大约几十毫秒，摊在十几秒里可以忽略。
 *
 * 依赖 [luastate] enabled=1（不然拿不到 L）。
 * 启用后请把 GM 面板的「量子深渊修复」停掉，别让 python 那套再跑。
 */
#include "qtpatch_lua.h"

#define LUA_MASKCOUNT   8
/* 每多少条指令回一次。这个数字必须让「两次重挂之间」一定能达到，否则
 * lua_sethook 每次重挂都会把 hookcount 清零，回调永远等不到 —— 第一版设了
 * 50 万，结果日志里只有「新 L=...」、一条「第 N 次下发」都没有。 */
#define QTP_HOOK_COUNT  5000
/* 重挂间隔。要明显大于「跑满 QTP_HOOK_COUNT 条指令所需的时间」，
 * 否则重挂会把倒计时反复清零。 */
#define QTP_REARM_MS    30000

typedef void (*qt_sethook_t)(void *L, void *fn, int mask, int count);
typedef int  (*qt_loadstr_t)(void *L, const char *s);
typedef int  (*qt_pcall_t)(void *L, int nargs, int nres, int ef);
typedef int  (*qt_gettop_t)(void *L);
typedef void (*qt_settop_t)(void *L, int idx);

static qt_sethook_t p_qt_sethook = NULL;
static qt_loadstr_t p_qt_loadstr = NULL;
static qt_pcall_t   p_qt_pcall   = NULL;
static qt_gettop_t  p_qt_gettop  = NULL;
static qt_settop_t  p_qt_settop  = NULL;
typedef int  (*qt_pushlstr_t)(void *L, const char *s, size_t len);
typedef void (*qt_setfield_t)(void *L, int idx, const char *k);
static qt_pushlstr_t p_qt_pushlstr = NULL;
static qt_setfield_t p_qt_setfield = NULL;

/* 项目根的绝对路径（`_ext\tmp` 所在的那一级），下面用 lua 全局 __QTP_ROOT 交给
 * 补丁脚本。**不能把路径写死在 lua 里**：用户会把游戏目录/app 目录改名
 * （app -> 7.0 就发生过一次），写死之后 cfg 和日志全落到不存在的目录，补丁静默失效。
 * 从 DLL 自己的路径往上退两级算出来：<root>\<game>\bh3bypass.dll -> <root>。 */
static char g_qt_root[MAX_PATH] = {0};

/* lua 5.1 的 LUA_GLOBALSINDEX */
#define LUA_GLOBALSINDEX (-10002)

static volatile LONG64 g_qt_cur_L   = 0;   /* 当前挂过 hook 的 L */
static volatile LONG64 g_qt_seen_L  = 0;   /* 轮询线程见过的最后一个 L */
static volatile LONG   g_qt_busy    = 0;   /* 回调重入闸 */
static volatile LONG   g_qt_tries   = 0;
static volatile DWORD  g_qt_last    = 0;
static volatile LONG   g_qt_first_hook = 0;/* 只用来打第一条「回调已触发」 */

/* 在 VM 回调里把五段全部跑一遍；返回 pcall 成功了几段（仅供参考，不是判据）。 */
static int qt_resolve_lua(void);   /* 定义在后面，线程里要用 */

static int qt_run_all(void *L)
{
    int top = p_qt_gettop(L);
    int ok = 0;
    int i;

    /* 把项目根交给 lua：补丁脚本里所有 cfg / 日志路径都拼在它后面。
     * 放在这里而不是只设一次，是因为每个 lua_State 是独立的全局表。 */
    if (g_qt_root[0] && p_qt_pushlstr && p_qt_setfield) {
        p_qt_pushlstr(L, g_qt_root, (size_t)lstrlenA(g_qt_root));
        p_qt_setfield(L, LUA_GLOBALSINDEX, "__QTP_ROOT");
    }

    for (i = 0; i < QT_PATCH_COUNT; i++) {
        if (p_qt_loadstr(L, QT_PATCHES[i]) == 0) {
            if (p_qt_pcall(L, 0, 1, 0) == 0) {
                ok++;
            }
        }
        /* 每段都会给栈留一个返回值，不弹掉会一直长 */
        p_qt_settop(L, top);
    }
    return ok;
}

/* 指令计数回调 —— 这个是游戏主线程在 VM 循环里调进来的。
 *
 * **不要**在这里写「L 不等于当前 L 就返回」：游戏同时有好几个 lua_State
 * （日志里 0x57EE2620 / 0x55420440 / 0x2B46DA60 每 200ms 就换一个），
 * g_lua_state 只记住最后一个。回调要等指令计数跑满才来，那时候 g_qt_cur_L
 * 早就是别的 VM 了，一判断就全跳过 —— 第一版日志只有「新 L=…」、一条下发都没有，
 * 就是这个原因。
 *
 * 现在是「哪个 L 的回调先到就打哪个」，用一个全局节流控制总频率；
 * 打到非量子的 L 上没关系，补丁第一段会返回 nordy 什么都不做。
 */
static void qt_hook(void *L, void *ar)
{
    DWORD now;
    LONG  n;
    int   ok;

    (void)ar;
    if (!g_qt_enabled || !L) {
        return;
    }
    if (!g_qt_first_hook) {          /* 第一次证明「hook 真的会被调用」 */
        g_qt_first_hook = 1;
        logf("[qtpatch] 回调已触发（L=%p LUA_MASKCOUNT=%d count=%d）",
             L, LUA_MASKCOUNT, QTP_HOOK_COUNT);
    }
    now = GetTickCount();
    if ((DWORD)(now - g_qt_last) < (DWORD)g_qt_gap) {
        return;                      /* 全局节流：不管哪个 L，总共 gap 一次 */
    }
    if (InterlockedCompareExchange(&g_qt_busy, 1, 0) != 0) {
        return;                      /* 跑 lua 时会再触发回调，这里挡住重入 */
    }
    g_qt_last = now;
    ok = qt_run_all(L);
    n = InterlockedIncrement(&g_qt_tries);
    if (n == 1 || (n % 30) == 0) {
        logf("[qtpatch] L=%p 第 %ld 次下发（%d/%d 段 pcall 通过）",
             L, n, ok, QT_PATCH_COUNT);
    }
    InterlockedExchange(&g_qt_busy, 0);
}

/* 轮询线程：只负责「挂 hook」，一个字都不往 lua 里写。
 *
 * 两个触发条件：
 *   1) g_lua_state 变了 —— 新 VM，重置计数、立刻挂；
 *   2) 距上次挂超过 QTP_REARM_MS —— lua_State 是 malloc 出来的，新一场很可能
 *      复用同一个地址，新 VM 初始化会把 hook 字段清零，光看「L 变了」会漏掉。
 *      这个间隔必须远大于「跑满 QTP_HOOK_COUNT 条指令」所需时间，否则重挂会
 *      把倒计时反复清零、回调永远不触发（第一版 8 秒配 50 万条就是这么死的）。
 */
static DWORD WINAPI qt_watch_thread(LPVOID param)
{
    DWORD  armed = 0;
    LONG64 armed_L = 0;
    DWORD  armed_at = 0;
    int    warned = 0;

    (void)param;
    for (;;) {
        LONG64 L;
        DWORD  now;
        Sleep(200);
        if (!g_qt_enabled) {
            continue;
        }
        if (!p_qt_sethook && !qt_resolve_lua()) {
            continue;               /* ulua.dll 还没加载，下一轮再试 */
        }
        L = g_lua_state;             /* [luastate] 抓到的那个 */
        if (!L) {
            continue;
        }
        if (L != g_qt_seen_L) {
            g_qt_seen_L = L;
            g_qt_cur_L  = L;
            g_qt_tries  = 0;
            g_qt_last   = 0;
            armed       = 0;
            warned      = 0;
            logf("[qtpatch] 新 L=%p，挂主线程 hook", (void *)(ULONG_PTR)L);
        }
        now = GetTickCount();
        if (armed == 0 || (DWORD)(now - armed) >= QTP_REARM_MS) {
            armed = now;
            armed_L = L;
            armed_at = now;
            p_qt_sethook((void *)(ULONG_PTR)L, (void *)qt_hook, LUA_MASKCOUNT, QTP_HOOK_COUNT);
        }
        /* 挂了 hook 却一直没回调 —— 说明这段时间 lua 根本没执行，
         * 或者 hook 被别的东西摘了。打一条日志，省得下次又要靠猜。 */
        if (!warned && armed_L == L && g_qt_tries == 0
                && (DWORD)(now - armed_at) >= 6000) {
            warned = 1;
            logf("[qtpatch] 警告 L=%p 挂 hook 已 6 秒仍无回调（这段没跑 lua？）",
                 (void *)(ULONG_PTR)L);
        }
    }
    return 0;
}

/* 取 ulua.dll 的导出。
 *
 * 为什么不在初始化时一次取完：DLL 是在游戏刚起来时注入的，那会儿 ulua.dll
 * 还没被加载（它要等第一次调 lua 才 LoadLibrary），GetModuleHandle 返回 NULL。
 * 实测日志就是「[qtpatch] !! 没找到 ulua.dll，跳过」——补丁整个没跑。
 * 所以改成懒加载：轮询线程每轮试一次，等 lua 跑起来它自然就在了。
 * 返回 1 表示导出齐了。 */
static int qt_resolve_lua(void)
{
    static int logged = 0;
    HMODULE u = GetModuleHandleA("ulua.dll");

    if (!u) {
        return 0;
    }
    p_qt_sethook = (qt_sethook_t)(void *)GetProcAddress(u, "lua_sethook");
    p_qt_loadstr = (qt_loadstr_t)(void *)GetProcAddress(u, "luaL_loadstring");
    p_qt_pcall   = (qt_pcall_t)(void *)GetProcAddress(u, "lua_pcall");
    p_qt_gettop  = (qt_gettop_t)(void *)GetProcAddress(u, "lua_gettop");
    p_qt_settop  = (qt_settop_t)(void *)GetProcAddress(u, "lua_settop");
    p_qt_pushlstr = (qt_pushlstr_t)(void *)GetProcAddress(u, "lua_pushlstring");
    p_qt_setfield = (qt_setfield_t)(void *)GetProcAddress(u, "lua_setfield");
    if (!p_qt_pushlstr || !p_qt_setfield) {
        logf("[qtpatch] 注意：ulua 没导出 lua_pushlstring/lua_setfield，"
             "lua 侧拿不到 __QTP_ROOT（会退回默认路径）");
    }
    if (!p_qt_sethook || !p_qt_loadstr || !p_qt_pcall || !p_qt_gettop || !p_qt_settop) {
        if (!logged) {
            logged = 1;
            logf("[qtpatch] !! ulua 导出缺失 sethook=%p loadstring=%p pcall=%p gettop=%p settop=%p",
                 (void *)p_qt_sethook, (void *)p_qt_loadstr, (void *)p_qt_pcall,
                 (void *)p_qt_gettop, (void *)p_qt_settop);
        }
        return 0;
    }
    logf("[qtpatch] ulua=%p 导出已就绪 sethook=%p，内嵌 %d 段 lua（retry=%d gap=%dms idle=%dms）",
         u, (void *)p_qt_sethook, QT_PATCH_COUNT, g_qt_retry, g_qt_gap, g_qt_idle);
    return 1;
}

/* 从 DLL 自身路径算 `_ext\tmp` 所在的那一级。
 *
 * 默认是**游戏目录的上一级**（本机就是 D:\BH3\7.0，_ext 在根上）；
 * 但如果 `<游戏目录>\_ext\tmp` 存在，就以游戏目录为准 —— 这样把整个包直接
 * 覆盖进游戏目录也能用，不需要人为把 _ext 摆到上一级去。
 * 用户会把 app 目录改名（app -> 7.0 就发生过），所以路径一律现算，绝不写死。 */
static void qt_compute_root(void)
{
    char  path[MAX_PATH];
    char  game[MAX_PATH];
    char  probe[MAX_PATH];
    char *p;
    HMODULE self;

    self = GetModuleHandleA("bh3bypass.dll");
    if (!self) {
        return;
    }
    if (!GetModuleFileNameA(self, path, MAX_PATH)) {
        return;
    }
    p = strrchr(path, '\\');           /* 去掉 bh3bypass.dll */
    if (!p) {
        return;
    }
    *p = 0;
    lstrcpynA(game, path, MAX_PATH);   /* 游戏目录 */
    p = strrchr(path, '\\');           /* 再往上一级 */
    if (!p) {
        lstrcpynA(g_qt_root, game, MAX_PATH);
        logf("[qtpatch] 项目根 = %s（没有上一级，用游戏目录）", g_qt_root);
        return;
    }
    *p = 0;

    lstrcpynA(probe, game, MAX_PATH);
    lstrcatA(probe, "\\_ext\\tmp");
    if (GetFileAttributesA(probe) != INVALID_FILE_ATTRIBUTES) {
        lstrcpynA(g_qt_root, game, MAX_PATH);
        logf("[qtpatch] 项目根 = %s（游戏目录里有 _ext\\tmp）", g_qt_root);
    } else {
        lstrcpynA(g_qt_root, path, MAX_PATH);
        logf("[qtpatch] 项目根 = %s（用上一级，lua 里的 __QTP_ROOT）", g_qt_root);
    }
}

static void apply_qtpatch(HMODULE ua)
{
    (void)ua;
    qt_compute_root();
    if (!qt_resolve_lua()) {
        logf("[qtpatch] ulua.dll 这时还没加载（正常），等它出现后自动接管");
    }
    if (!g_lua_state) {
        logf("[qtpatch] 注意 g_lua_state 还是 0，等 [luastate] 抓到 L 后自动开始");
    }
    CreateThread(NULL, 0, qt_watch_thread, NULL, 0, NULL);
}

static void apply_qtwave(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_QTWAVE_TAIL;
    BYTE *page, *stub, *end;
    BYTE  patch[LEN_QTWAVE_TAIL];
    char  hs[256];
    LONG  rel;
    int   i;

    hexdump(fn, LEN_QTWAVE_TAIL, hs, sizeof(hs));
    logf("[qtwave] GetBattle 返回点 %p 当前: %s", fn, hs);

    if (fn[0] == 0xE9) {
        logf("[qtwave] 已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_QTWAVE_TAIL, LEN_QTWAVE_TAIL)) {
        logf("[qtwave] !! 原始字节不匹配，拒绝打补丁");
        return;
    }
    page = alloc_near(fn);
    if (!page) {
        logf("[qtwave] !! alloc_near 失败");
        return;
    }
    stub = page;
    end  = build_qtwave_stub(stub);
    FlushInstructionCache(GetCurrentProcess(), stub, (DWORD)(end - stub));

    rel = (LONG)(stub - (fn + 5));
    patch[0] = 0xE9;
    memcpy(patch + 1, &rel, 4);
    for (i = 5; i < LEN_QTWAVE_TAIL; i++) patch[i] = 0x90;

    if (!write_code(fn, patch, sizeof(patch))) {
        logf("[qtwave] !! 写入口失败 err=%lu", GetLastError());
        return;
    }
    logf("[qtwave] OK  已接管：GetBattle 只返回第 1 波（stub %d 字节）", (int)(end - stub));
}

static void apply_label_lg(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_LABEL_ONENABLE;
    BYTE *page, *stub, *tramp, *end, *tend;
    BYTE  patch[LEN_LABEL_ONENABLE];
    int   i, nt;
    HANDLE th[MAX_SUSPEND];

    if (!is_original(fn, ORIG_LABEL_ONENABLE, LEN_LABEL_ONENABLE)) {
        logf("[label] !! OnEnable 原始字节不匹配，跳过对齐修正");
        return;
    }
    page = alloc_near(fn);
    if (!page) {
        logf("[label] !! alloc_near 失败，跳过对齐修正");
        return;
    }
    stub  = page;
    tramp = page + 0x400;
    end   = build_label_lg_stub(stub, tramp);
    tend  = emit_bytes(tramp, ORIG_LABEL_ONENABLE, LEN_LABEL_ONENABLE);
    *tend++ = 0xFF; *tend++ = 0x25;
    *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00;
    {
        ULONGLONG v = (ULONGLONG)(ULONG_PTR)((BYTE *)ua + RVA_LABEL_ONENABLE + LEN_LABEL_ONENABLE);
        memcpy(tend, &v, 8);
        tend += 8;
    }
    FlushInstructionCache(GetCurrentProcess(), page, (DWORD)(tend - page));

    {
        LONG rel = (LONG)(stub - (fn + 5));
        patch[0] = 0xE9;
        memcpy(patch + 1, &rel, 4);
        for (i = 5; i < LEN_LABEL_ONENABLE; i++) patch[i] = 0x90;

        nt = suspend_other_threads(th, MAX_SUSPEND);
        if (write_code(fn, patch, sizeof(patch))) {
            resume_other_threads(th, nt);
            logf("[label] OnEnable 已接管（父布局 childAlignment -> %d，stub %d 字节）",
                 g_group_align, (int)(end - stub));
        } else {
            resume_other_threads(th, nt);
            logf("[label] !! OnEnable 打补丁失败 err=%lu", GetLastError());
        }
    }
}

static void apply_label(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_LABEL_ENABLE;
    BYTE *stub, *tramp, *end, *tend;
    BYTE  patch[LEN_LABEL_ENABLE];
    int   i;
    char  hs[256];

    g_label_ua = (BYTE *)ua;
    hexdump(fn, LEN_LABEL_ENABLE, hs, sizeof(hs));
    logf("[label] Text::OnPopulateMesh %p 当前: %s  shift_x=%.1f", fn, hs, g_label_shift);

    if (fn[0] == 0xFF && fn[1] == 0x25) {
        logf("[label] 已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_LABEL_ENABLE, LEN_LABEL_ENABLE)) {
        logf("[label] !! 原始字节不匹配，拒绝打补丁");
        return;
    }
    stub = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!stub) {
        logf("[label] !! VirtualAlloc 失败 err=%lu", GetLastError());
        return;
    }
    tramp = stub + 0x400;
    end   = build_label_stub(stub, tramp);
    tend  = emit_bytes(tramp, ORIG_LABEL_ENABLE, LEN_LABEL_ENABLE);
    *tend++ = 0xFF; *tend++ = 0x25;
    *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00;
    {
        ULONGLONG v = (ULONGLONG)(ULONG_PTR)((BYTE *)ua + RVA_LABEL_ENABLE + LEN_LABEL_ENABLE);
        memcpy(tend, &v, 8);
        tend += 8;
    }
    FlushInstructionCache(GetCurrentProcess(), stub, (DWORD)(tend - stub));
    logf("[label] stub @ %p（%d 字节）  trampoline @ %p（%d 字节）",
         stub, (int)(end - stub), tramp, (int)(tend - tramp));

    patch[0] = 0xFF;
    patch[1] = 0x25;
    patch[2] = patch[3] = patch[4] = patch[5] = 0x00;
    *(ULONGLONG *)(patch + 6) = (ULONGLONG)(ULONG_PTR)stub;
    for (i = 14; i < LEN_LABEL_ENABLE; i++) patch[i] = 0x90;

    {
        HANDLE th[MAX_SUSPEND];
        int    nt = suspend_other_threads(th, MAX_SUSPEND);
        BOOL   wok = write_code(fn, patch, sizeof(patch));
        resume_other_threads(th, nt);
        logf("[label] 写入时挂起了 %d 个线程", nt);
        if (!wok) {
            logf("[label] !! 写入口跳转失败 err=%lu", GetLastError());
            return;
        }
    }
    if (memcmp(fn, patch, sizeof(patch)) == 0)
        logf("[label] OK  入口已接管（from len=%d -> to len=%d）",
             g_label_fromlen, g_label_tolen);
    else
        logf("[label] !! 写入后回读不一致");
}

static DWORD WINAPI label_watch(LPVOID param)
{
    LONG last = 0, lastc = 0;
    (void)param;
    for (;;) {
        LONG now;
        Sleep(1500);
        {
            LONG nc = InterlockedCompareExchange(&g_label_calls, 0, 0);
            if (nc != lastc) {
                lastc = nc;
                logf("[label] OnPopulateMesh 调用 %ld 次（命中 %ld）",
                     nc, InterlockedCompareExchange(&g_label_hits, 0, 0));
            }
        }
        now = InterlockedCompareExchange(&g_label_hits, 0, 0);
        if (now != last) {
            last = now;
            logf("[label] 已替换 %ld 次标签文字", last);
        }
    }
    return 0;
}

/* ---- 标题替换 -----------------------------------------------------------
 *
 * 链路（反汇编实证）：
 *   基类 MonoWorldMapEntryContainerBase::SetupTitle @ 0x02CFFB80
 *       rbx = [this+0x40]        ; Text 组件
 *       rax = [rax+0x1a8](this)  ; 虚调用 GetEntryTitle()
 *       rdx = rax                ; 标题串
 *       rcx = rbx
 *       jmp 0x069075C0           ; <- UnityEngine.UI.Text::set_text
 *
 * 0x069075C0 就是 UnityEngine.UI.Text::set_text 本体（符号表里可查），
 * 任何给 Text 赋值的路径最后都要走它。rcx=Text、rdx=字符串。
 *
 * ★ 2026-11-07 修正：早先挂的 0x03884080 根本不是 set_text，而是一个
 *   「空则返回」的泛型包装（call IsNullOrEmpty → call [vtable+0x5F8]），
 *   所以日志里一次都没命中。真正的赋值函数是 0x069075C0，它内部
 *   `mov [rbx+0xD8], rdi` 就是 m_Text，随后 SetVerticesDirty + SetLayoutDirty。
 *
 * 命中判据用**字符串内容**而不是 TextID 哈希：写死不依赖 TextMap 的哈希算法，
 * 也不依赖 GetText 是否被调用。判定 = 长度相等 + UTF-16 逐字节比较，
 * 不匹配时只花 2 条指令（test / cmp 长度）就跳回原逻辑。
 *
 * 命中后：把入参字符串的对象头（klass）拷进自己的缓冲区，正文换成目标文本，
 * rdx 指向缓冲区 —— 指针和 TextMap 里那个实例不同，set_text 内部的
 * `m_Text != value` 成立，SetVerticesDirty 会被触发，网格重建 => 画面真的变。
 * （这正是之前在内存里改字符串内容看不到变化的原因：引用没变，不 dirty。）
 *
 * 入口 16 字节（5 条完整指令），之后 0x69075D0 是 `mov rcx,rdx`：
 *   48 89 5C 24 08   mov  [rsp+8], rbx
 *   57               push rdi
 *   48 83 EC 20      sub  rsp, 0x20
 *   48 8B D9         mov  rbx, rcx     ; this
 *   48 8B FA         mov  rdi, rdx     ; value
 * trampoline 原样回放这 16 字节再跳 0x69075D0，所以 rdx 换掉之后
 * 那句 `mov rdi,rdx` 会把新指针带进去。
 */
#define RVA_SET_TEXT    0x069075C0
#define LEN_SET_TEXT    16
static const BYTE ORIG_SET_TEXT[LEN_SET_TEXT] = {
    0x48, 0x89, 0x5C, 0x24, 0x08, 0x57, 0x48, 0x83, 0xEC, 0x20,
    0x48, 0x8B, 0xD9, 0x48, 0x8B, 0xFA
};

static BYTE *build_title_stub(BYTE *out, BYTE *ua, BYTE *tramp)
{
    BYTE *p = out;
    int   j_test, j_len, j_cmp, j_loop;
    int   at_replay, at_loop;
    int   flen = g_title_fromlen;
    int   n    = g_title_tolen;
    LONG  fbytes = (LONG)flen * 2;
    LONG  nbytes = (LONG)n * 2;
    ULONGLONG fromaddr = (ULONGLONG)(ULONG_PTR)g_title_from;
    ULONGLONG toaddr   = (ULONGLONG)(ULONG_PTR)g_title_to;
    ULONGLONG bufaddr  = (ULONGLONG)(ULONG_PTR)g_title_buf;

    /* 进来先计数：用来区分「钩子根本没被调用」和「调用了但内容不匹配」 */
    p = emit_mov_r11(p, &g_title_calls);
    *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;   /* lock inc [r11] */

    /* test rdx, rdx */
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xD2;
    j_test = (int)(p - out); *p++ = 0x74; *p++ = 0x00;

    /* 顺手记下真实 String 的 klass —— 标题图那一路自建 String 要用 */
    p = emit_mov_r11(p, &g_str_klass);
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x02;
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;

    /* cmp dword [rdx+0x10], flen ; jne replay */
    *p++ = 0x83; *p++ = 0x7A; *p++ = 0x10; *p++ = (BYTE)flen;
    j_len = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    /* 逐字节比较：r10d=字节数, rax=from, r11=value+0x14 */
    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &fbytes, 4); p += 4;
    *p++ = 0x48; *p++ = 0xB8; memcpy(p, &fromaddr, 8); p += 8;
    *p++ = 0x4C; *p++ = 0x8D; *p++ = 0x5A; *p++ = 0x14;   /* lea r11,[rdx+0x14] */
    at_loop = (int)(p - out);
    *p++ = 0x44; *p++ = 0x8A; *p++ = 0x08;                /* mov r9b, [rax] */
    *p++ = 0x45; *p++ = 0x3A; *p++ = 0x0B;                /* cmp r9b, [r11] */
    j_cmp = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
    *p++ = 0x48; *p++ = 0xFF; *p++ = 0xC0;                /* inc rax */
    *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;                /* inc r11 */
    *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;                /* dec r10d */
    j_loop = (int)(p - out); *p++ = 0x75; *p++ = 0x00;

    at_replay = (int)(p - out);
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;                             /* jmp rax */

    /* ---- 命中：造一个自己的 String ---- */
    p = emit_mov_r11(p, &g_title_hits);
    *p++ = 0xF0; *p++ = 0x41; *p++ = 0xFF; *p++ = 0x03;   /* lock inc [r11] */

    /* 命中时顺便改字色：Graphic::m_Color 在 this+0x20，4 个 float（RGBA）。
     * rcx 一直没被动过，仍是 Text 本体；改完后 set_text 会 SetVerticesDirty，
     * 重建网格时 Text.OnPopulateMesh 会用新的 color。 */
    {
        int c;
        for (c = 0; c < 4; c++) {
            LONG bits;
            memcpy(&bits, &g_title_col[c], 4);
            *p++ = 0xC7; *p++ = 0x41; *p++ = (BYTE)(0x20 + c * 4);   /* mov dword [rcx+0x20+4c], imm32 */
            memcpy(p, &bits, 4); p += 4;
        }
    }

    *p++ = 0x49; *p++ = 0xBB; memcpy(p, &bufaddr, 8); p += 8;   /* mov r11, buf */
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x02;                /* mov rax, [rdx]  ; klass */
    *p++ = 0x49; *p++ = 0x89; *p++ = 0x03;                /* mov [r11], rax */
    *p++ = 0x49; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x08;   /* mov qword [r11+8], 0 */
    { LONG zero = 0; memcpy(p, &zero, 4); p += 4; }
    *p++ = 0x41; *p++ = 0xC7; *p++ = 0x43; *p++ = 0x10;   /* mov dword [r11+0x10], n */
    memcpy(p, &n, 4); p += 4;

    *p++ = 0x48; *p++ = 0xBA; memcpy(p, &toaddr, 8); p += 8;    /* mov rdx, to */
    *p++ = 0x41; *p++ = 0xBA; memcpy(p, &nbytes, 4); p += 4;    /* mov r10d, n*2 */
    {
        BYTE *lp = p;
        (void)lp;
    }
    {
        int at_loop2 = (int)(p - out);
        int j2;
        *p++ = 0x8A; *p++ = 0x02;                         /* mov al, [rdx] */
        *p++ = 0x41; *p++ = 0x88; *p++ = 0x43; *p++ = 0x14;/* mov [r11+0x14], al */
        *p++ = 0x49; *p++ = 0xFF; *p++ = 0xC3;            /* inc r11 */
        *p++ = 0x48; *p++ = 0xFF; *p++ = 0xC2;            /* inc rdx */
        *p++ = 0x41; *p++ = 0xFF; *p++ = 0xCA;            /* dec r10d */
        j2 = (int)(p - out); *p++ = 0x75; *p++ = 0x00;
        out[j2 + 1] = (BYTE)(at_loop2 - (j2 + 2));
    }

    *p++ = 0x48; *p++ = 0xBA; memcpy(p, &bufaddr, 8); p += 8;   /* mov rdx, buf */
    p = emit_mov_rax(p, tramp);
    *p++ = 0xFF; *p++ = 0xE0;                             /* jmp rax */

    out[j_test + 1] = (BYTE)(at_replay - (j_test + 2));
    out[j_len  + 1] = (BYTE)(at_replay - (j_len  + 2));
    out[j_cmp  + 1] = (BYTE)(at_replay - (j_cmp  + 2));
    out[j_loop + 1] = (BYTE)(at_loop   - (j_loop + 2));
    return p;
}

static void apply_title(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_SET_TEXT;
    BYTE *stub;
    BYTE *end;
    BYTE  patch[14];
    char  hs[256];

    hexdump(fn, LEN_SET_TEXT, hs, sizeof(hs));
    logf("[title] set_text %p 当前: %s", fn, hs);

    if (fn[0] == 0xFF && fn[1] == 0x25) {
        logf("[title] 已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_SET_TEXT, LEN_SET_TEXT)) {
        logf("[title] !! 原始字节不匹配，拒绝打补丁（客户端版本变了？）");
        return;
    }

    stub = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!stub) {
        logf("[title] !! VirtualAlloc 失败 err=%lu", GetLastError());
        return;
    }
    {
        BYTE *tramp = stub + 0x200;
        BYTE *tend;
        end  = build_title_stub(stub, (BYTE *)ua, tramp);
        tend = emit_bytes(tramp, ORIG_SET_TEXT, LEN_SET_TEXT);
        *tend++ = 0xFF; *tend++ = 0x25;
        *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00;
        {
            ULONGLONG v = (ULONGLONG)(ULONG_PTR)((BYTE *)ua + RVA_SET_TEXT + LEN_SET_TEXT);
            memcpy(tend, &v, 8);
            tend += 8;
        }
        FlushInstructionCache(GetCurrentProcess(), stub, (DWORD)(end - stub));
        FlushInstructionCache(GetCurrentProcess(), tramp, (DWORD)(tend - tramp));
        logf("[title] stub @ %p（%d 字节）  trampoline @ %p（%d 字节）",
             stub, (int)(end - stub), tramp, (int)(tend - tramp));
    }

    memset(patch, 0xCC, sizeof(patch));
    patch[0] = 0xFF;
    patch[1] = 0x25;
    patch[2] = patch[3] = patch[4] = patch[5] = 0x00;
    *(ULONGLONG *)(patch + 6) = (ULONGLONG)(ULONG_PTR)stub;

    if (!write_code(fn, patch, sizeof(patch))) {
        logf("[title] !! 写入口跳转失败 err=%lu", GetLastError());
        return;
    }
    if (memcmp(fn, patch, sizeof(patch)) == 0)
        logf("[title] OK  入口已接管（\"%ls\" -> \"%ls\"）", g_title_from, g_title_to);
    else
        logf("[title] !! 写入后回读不一致");
}

static DWORD WINAPI title_watch(LPVOID param)
{
    LONG last = 0, lastc = 0;
    (void)param;
    for (;;) {
        LONG now, nc;
        Sleep(1500);
        nc = InterlockedCompareExchange(&g_title_calls, 0, 0);
        if (nc != lastc) {
            lastc = nc;
            logf("[title] set_text 调用 %ld 次（命中 %ld）",
                 nc, InterlockedCompareExchange(&g_title_hits, 0, 0));
        }
        now = InterlockedCompareExchange(&g_title_hits, 0, 0);
        if (now != last) {
            last = now;
            logf("[title] 已替换 %ld 次标题文本", last);
        }
    }
    return 0;
}



typedef void *(*PFN_GET_MODULE)(void *);
typedef void  (*PFN_GOTO_OW)(void *);

static void entry_on_return(void)
{
    PFN_GET_MODULE gm;
    PFN_GOTO_OW    go;
    void *mod;

    if (!g_ua) return;

    /* 万一 UI 卡在别的地方连点，别叠着进 */
    if (InterlockedCompareExchange(&g_reentering, 1, 0) != 0) {
        logf("[entry] 上一次还没走完，忽略这次点击");
        return;
    }

    /* DoClickAction 刚返回，UI 事件处理还在同一帧里往下走。
     * 让出一点时间（默认 100ms），等这一帧的后续处理结束再进模式 ——
     * 这就是原来 rehook 那套 120ms 异步补刀在做的事，只是现在没有额外的线程。 */
    if (g_delay_ms > 0) Sleep((DWORD)g_delay_ms);

    gm = (PFN_GET_MODULE)((BYTE *)g_ua + RVA_GET_MODULE);
    go = (PFN_GOTO_OW)((BYTE *)g_ua + RVA_GOTO_OW);

    logf("[entry] 命中！card_id=%ld meta=%p", g_entry.last_id,
         (void *)g_entry.last_meta);

    mod = gm(NULL);
    if (!mod) {
        logf("[entry] !! get_module 返回空，放弃本次");
        g_reentering = 0;
        return;
    }

    /* endless_type 必须**显式写成目标值**，不能只在量子分支里改。
     *
     * 2026-11-08 的坑：原来只有 `if (mode == 1) { type = 3; }`，切回迪拉克
     * （mode=0）时什么都不做，于是一旦之前点过一次量子、type 被改成了 3，
     * 之后点卡还是按 3 进量子 —— 表现就是「GM 点了切到迪拉克但没生效」。
     * 现在 0/1 两个方向都写死：0 -> 2（迪拉克），1 -> 3（量子）。 */
    {
        LONG want = (g_entry.mode == 1) ? 3 : ((g_entry.mode == 0) ? 2 : -1);
        if (want > 0) {
            ULONG_PTR two = *(ULONG_PTR *)((BYTE *)mod + OFF_TWO_ENDLESS);
            if (two) {
                ULONG_PTR cur = *(ULONG_PTR *)((BYTE *)two + OFF_CUR_MODE);
                if (cur) {
                    LONG old = *(LONG *)((BYTE *)cur + OFF_STATUS_TYPE);
                    *(LONG *)((BYTE *)cur + OFF_STATUS_TYPE) = want;
                    logf("[entry] %s  _currentMode=%p  endless_type %ld -> %ld",
                         want == 3 ? "quantum" : "dirac", (void *)cur, old, want);
                } else {
                    logf("[entry] !! _currentMode 为空，按原类型进入");
                }
            } else {
                logf("[entry] !! TwoEndless 为空，按原类型进入");
            }
        }
    }

    go(mod);
    logf("[entry] GoToOWEndless(%p) 已调用", mod);
    g_reentering = 0;
}

/* 点击观测：stub 每次点击都会刷 last_id / last_meta（纯写内存，不走任何调用）。
 * 这里只是轮询把变化记进日志 —— 换过入口图标、card_id 对不上时，
 * 「点了没反应」的唯一线索就是这行。零风险，纯读内存。 */
static DWORD WINAPI entry_click_log(LPVOID param)
{
    LONG      last_id   = -1;
    ULONG_PTR last_meta = 0;
    (void)param;

    for (;;) {
        LONG      id = *(volatile LONG *)&g_entry.last_id;
        ULONG_PTR mt = *(volatile ULONG_PTR *)&g_entry.last_meta;
        if (id != last_id || mt != last_meta) {
            logf("[entry] 点击 id=%ld meta=%p  %s", id, (void *)mt,
                 id == g_entry.card_id ? "=> 命中" : "(不是目标卡)");
            last_id   = id;
            last_meta = mt;
        }
        Sleep(300);
    }
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

/* ---------------- 刻晴（avatarID 2001）角色列表可见 -------------------------- */

/* UIUtil.IsAvatartDoNotShow(AvatarDataItem) @ RVA 0x0302A4F0
 *
 *   0x0302A4F0  40 53                 push rbx
 *   0x0302A4F2  48 83 EC 20           sub  rsp,0x20
 *   0x0302A4F6  48 8B D9              mov  rbx,rcx          ; rcx = AvatarDataItem
 *   0x0302A4F9  B9 7C F3 00 00        mov  ecx,0xf37c
 *   0x0302A4FE  E8 7D DA 5A 00        call 0x035D7F80       ; 方法初始化探针
 *
 * 14 字节正好在 call 之前收尾，指令边界对齐，可以整段覆盖成绝对跳转。
 *
 * 为什么在入口拦一刀就够：这个函数尾部是 `jmp IsCollaborationAvatarDoNotShow`
 * （尾调用），所以提前 return false 会一次绕过三条判据 —— 按 classID 隐藏、
 * 按 avatarID 隐藏、协作角色隐藏。只有 avatarID==2001 放行，
 * 其它角色（含 316 / 901 等联动角色）完全不受影响。
 *
 * 2026-10-27 的坑（python 版踩过，这里同样适用）：回跳目标必须是
 * **运行时地址** ua+RVA+14，不能写静态 VA 0x18302A4FE，否则 RIP 落到
 * 未映射地址 → 0xc0000005。
 */
#define RVA_KQ_VISIBLE   0x0302A4F0
#define LEN_KQ_VISIBLE   14
static const BYTE ORIG_KQ_VISIBLE[LEN_KQ_VISIBLE] = {
    0x40, 0x53,                         /* push rbx                     */
    0x48, 0x83, 0xEC, 0x20,             /* sub  rsp, 0x20               */
    0x48, 0x8B, 0xD9,                   /* mov  rbx, rcx                */
    0xB9, 0x7C, 0xF3, 0x00, 0x00        /* mov  ecx, 0xf37c             */
};

/* ---- int 重载：IsAvatartDoNotShow(int avatarID) @ 0x0302A450 ------------
 *
 * 2026-11-09 补。原来只 hook 了 (AvatarDataItem) 那个重载，但「刻晴完全
 * 不出现」说明客户端角色列表走的不是那一条 —— 很可能调的就是这个 int 版：
 * 列表手里本来就只有 avatarID，不需要先拿到 AvatarDataItem。
 *
 * 入口：40 53 | 48 83 EC 20 | 8B D9 | B9 0A F0 00 00
 *       push rbx; sub rsp,0x20; mov ebx,ecx; mov ecx,0xf00a
 * 13 字节正好收在 call 之前，指令边界对齐。
 * 参数是 int，rcx 的低 32 位就是 avatarID，不用解引用。
 */
#define RVA_KQ_VISIBLE_INT   0x0302A450
#define LEN_KQ_VISIBLE_INT   13
static const BYTE ORIG_KQ_VISIBLE_INT[LEN_KQ_VISIBLE_INT] = {
    0x40, 0x53,                         /* push rbx                     */
    0x48, 0x83, 0xEC, 0x20,             /* sub  rsp, 0x20               */
    0x8B, 0xD9,                         /* mov  ebx, ecx                */
    0xB9, 0x0A, 0xF0, 0x00, 0x00        /* mov  ecx, 0xf00a             */
};

/* stub（48 字节）：
 *   00  48 85 C9            test rcx,rcx
 *   03  74 0C               jz   orig
 *   05  8B 41 1C            mov  eax,[rcx+0x1C]     ; AvatarDataItem.avatarID
 *   08  3D <id>             cmp  eax,avatar_id
 *   0D  75 02               jne  orig
 *   0F  EB 1C               jmp  false_ret
 *   11  <被覆盖的 14 字节>
 *   1F  FF 25 00 00 00 00   jmp  qword [rip+0]      ; 回原函数 +14
 *   25  <resume>            （运行时地址）
 *   2D  31 C0 C3            xor eax,eax ; ret
 */
static BYTE *build_kq_stub(BYTE *out, BYTE *ua, LONG avatar_id)
{
    BYTE *p = out;
    BYTE *resume = ua + RVA_KQ_VISIBLE + LEN_KQ_VISIBLE;
    int   jz_o, jne_o, jmp_f;
    int   orig_at;

    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC9;              /* test rcx,rcx       */
    *p++ = 0x74; jz_o = (int)(p - out); *p++ = 0;       /* jz  orig           */
    *p++ = 0x8B; *p++ = 0x41; *p++ = 0x1C;              /* mov eax,[rcx+0x1C] */
    *p++ = 0x3D; memcpy(p, &avatar_id, 4); p += 4;      /* cmp eax,avatar_id  */
    *p++ = 0x75; jne_o = (int)(p - out); *p++ = 0;      /* jne orig           */
    *p++ = 0xEB; jmp_f = (int)(p - out); *p++ = 0;      /* jmp false_ret      */

    orig_at = (int)(p - out);
    out[jz_o]  = (BYTE)(orig_at - (jz_o + 1));
    out[jne_o] = (BYTE)(orig_at - (jne_o + 1));

    memcpy(p, ORIG_KQ_VISIBLE, LEN_KQ_VISIBLE); p += LEN_KQ_VISIBLE;
    *p++ = 0xFF; *p++ = 0x25; *p++ = 0; *p++ = 0; *p++ = 0; *p++ = 0;
    memcpy(p, &resume, 8); p += 8;                      /* 绝对跳转回原函数 */
    out[jmp_f] = (BYTE)((p - out) - (jmp_f + 1));
    *p++ = 0x31; *p++ = 0xC0; *p++ = 0xC3;              /* xor eax,eax ; ret */
    return p;
}

static void apply_keqing(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_KQ_VISIBLE;
    BYTE *stub, *end;
    BYTE  entry[LEN_KQ_VISIBLE];
    ULONGLONG sv;
    char  hs[256];

    hexdump(fn, LEN_KQ_VISIBLE, hs, sizeof(hs));
    logf("[keqing] IsAvatartDoNotShow %p 当前: %s", fn, hs);

    if (fn[0] == 0xFF && fn[1] == 0x25) {
        logf("[keqing] 已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_KQ_VISIBLE, LEN_KQ_VISIBLE)) {
        logf("[keqing] !! 原始字节不匹配，拒绝打补丁（客户端版本变了？）");
        return;
    }

    stub = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!stub) {
        logf("[keqing] !! VirtualAlloc 失败 err=%lu", GetLastError());
        return;
    }

    end = build_kq_stub(stub, (BYTE *)ua, g_kq_avatar_id);
    FlushInstructionCache(GetCurrentProcess(), stub, (DWORD)(end - stub));
    logf("[keqing] stub @ %p（%d 字节）  回跳 @ %p",
         stub, (int)(end - stub), ua + RVA_KQ_VISIBLE + LEN_KQ_VISIBLE);

    memset(entry, 0xCC, sizeof(entry));
    entry[0] = 0xFF;
    entry[1] = 0x25;
    entry[2] = entry[3] = entry[4] = entry[5] = 0x00;
    sv = (ULONGLONG)(ULONG_PTR)stub;
    memcpy(entry + 6, &sv, 8);

    if (!write_code(fn, entry, LEN_KQ_VISIBLE)) {
        logf("[keqing] !! 写入口跳转失败 err=%lu", GetLastError());
        return;
    }
    if (memcmp(fn, entry, LEN_KQ_VISIBLE) == 0)
        logf("[keqing] OK  avatarID=%ld 不再隐藏（其它角色行为不变）", g_kq_avatar_id);
    else
        logf("[keqing] !! 写入后回读不一致");
}

/* int 重载的 stub：参数直接就是 avatarID，比指针版少一次解引用。 */
static BYTE *build_kq_stub_int(BYTE *out, BYTE *ua, LONG avatar_id)
{
    BYTE *p = out;
    BYTE *resume = ua + RVA_KQ_VISIBLE_INT + LEN_KQ_VISIBLE_INT;
    int   jne_o, jmp_f, orig_at;

    *p++ = 0x81; *p++ = 0xF9;                       /* cmp ecx,id  */
    memcpy(p, &avatar_id, 4); p += 4;
    *p++ = 0x75; jne_o = (int)(p - out); *p++ = 0;  /* jne orig    */
    *p++ = 0xEB; jmp_f = (int)(p - out); *p++ = 0;  /* jmp false   */

    orig_at = (int)(p - out);
    out[jne_o] = (BYTE)(orig_at - (jne_o + 1));

    memcpy(p, ORIG_KQ_VISIBLE_INT, LEN_KQ_VISIBLE_INT); p += LEN_KQ_VISIBLE_INT;
    *p++ = 0xFF; *p++ = 0x25; *p++ = 0; *p++ = 0; *p++ = 0; *p++ = 0;
    memcpy(p, &resume, 8); p += 8;
    out[jmp_f] = (BYTE)((p - out) - (jmp_f + 1));
    *p++ = 0x31; *p++ = 0xC0; *p++ = 0xC3;          /* xor eax,eax ; ret */
    return p;
}

static void apply_keqing_int(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_KQ_VISIBLE_INT;
    BYTE *stub, *end;
    BYTE  entry[LEN_KQ_VISIBLE_INT];
    ULONGLONG sv;
    char  hs[128];

    hexdump(fn, LEN_KQ_VISIBLE_INT, hs, sizeof(hs));
    logf("[keqing] int 版入口 %p 当前: %s", fn, hs);

    if (fn[0] == 0x48 && fn[1] == 0xB8) {
        logf("[keqing] int 版已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_KQ_VISIBLE_INT, LEN_KQ_VISIBLE_INT)) {
        logf("[keqing] !! int 版原始字节不匹配，跳过（客户端版本变了？）");
        return;
    }

    stub = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!stub) {
        logf("[keqing] !! int 版 VirtualAlloc 失败 err=%lu", GetLastError());
        return;
    }

    end = build_kq_stub_int(stub, (BYTE *)ua, g_kq_avatar_id);
    FlushInstructionCache(GetCurrentProcess(), stub, (DWORD)(end - stub));

    /* 这里不能照抄指针版的 `FF 25 + 8 字节地址` —— 那是 14 字节，
     * 而这个入口只有 13 字节可用，写 13 字节会**把跳转地址截断一个字节**，
     * 直接跳到野地址 → 0xc0000005（09:29 那次崩溃就是这么来的）。
     * 改用 `mov rax,imm64 ; jmp rax ; nop`，10+2+1 = 13 正好塞满。
     * 入口处 rax 还没参与运算，拿它当跳板是安全的。 */
    sv = (ULONGLONG)(ULONG_PTR)stub;
    memset(entry, 0xCC, sizeof(entry));
    entry[0] = 0x48;
    entry[1] = 0xB8;
    memcpy(entry + 2, &sv, 8);
    entry[10] = 0xFF;
    entry[11] = 0xE0;
    entry[12] = 0x90;

    if (!write_code(fn, entry, LEN_KQ_VISIBLE_INT)) {
        logf("[keqing] !! int 版写入口跳转失败 err=%lu", GetLastError());
        return;
    }
    if (memcmp(fn, entry, LEN_KQ_VISIBLE_INT) == 0)
        logf("[keqing] int 版 OK  avatarID=%ld 也放行（stub @ %p）", g_kq_avatar_id, (void *)stub);
    else
        logf("[keqing] !! int 版写入后回读不一致");
}

/* ---------------- 刻晴武器技能（Katana_M43）注册表注入 -------------------- */

/* 武器 24241~24244 = Entities/Weapon/Katana_M43 = 刻晴专属太刀。
 * 三个技能都挂在这把武器上：
 *
 *   1160  WeaponData.IAADPOLPIPG  主动（图标 Skill_Mei_Weapon_43）
 *   2211  WeaponData.OMLIBFNIJLO  被动 1
 *   2212  WeaponData.ALOGJPEEDMB  被动 2
 *
 * 客户端注册表（EquipmentSkill registry，index 1758 条）里这三个 ID
 * **全部没有烘焙记录**。同区间 1159/1161/1162/1163/1166/1168~1170 都在，
 * 缺的只有 1158/1160/1164/1165/1167/2211/2212 —— 是零散裁剪，不是整段剔除。
 * 所以「武器技能不生效」的根因是查表查不到，不是数值算错。
 *
 * 这也决定了修法：不是"调参数"，是"把记录补进注册表"。补法沿用
 * tools\keqing_native_input.py 验证过的路子 —— 造一个空 ability 对象
 * （DLC_Common_Empty），让它出现在 index/cache 里。主动技能靠这一条就能
 * 让输入槽变非空（剑气逻辑在角色自身代码里，1160 只是开关）；
 * 两个被动能不能真出效果，得实测才知道，空 ability 本身不产生伤害。
 *
 * 阶段 1（probe，默认开）：只 hook Contains，记录查询次数与 key，
 *   重放被覆盖的 14 字节后跳回原函数，**行为与未打补丁完全一致**。
 *   目的：确认玩家在哪一步会查这三个 key、此时 registry cache 解码了没有。
 *
 * 阶段 2（inject，暂未实现）：把 stub 改成槽位化 ——
 *       mov rax,[rip+slot] / test rax,rax / jz orig / ... ret
 *   hook 一装好就能生效（槽=0 时走原逻辑），对象什么时候造好就什么时候写槽。
 *   这样「装 hook」与「造对象」解耦，才有懒加载的余地。
 *
 * Contains 的 14 字节覆盖是**跨指令**的，这点必须留意：
 *   +0x00  48 83 EC 28        sub  rsp,0x28
 *   +0x04  48 8B 49 20        mov  rcx,[rcx+0x20]
 *   +0x08  48 85 C9           test rcx,rcx
 *   +0x0B  74 0C              je   +0x19
 *   +0x0D  45 33 C0           xor  r8d,r8d      ; ← 14 字节只吃到 0x45
 * 所以 stub 必须完整重放这 4 条语义，再跳回 ua+0x03DF6520（add rsp,0x28
 * 的指令边界），不能直接跳 +14，否则会落在 `33 C0` 这条半截指令上。
 */
#define RVA_KQ_CONTAINS   0x03DF6510
#define LEN_KQ_CONTAINS   14
static const BYTE ORIG_KQ_CONTAINS[LEN_KQ_CONTAINS] = {
    0x48, 0x83, 0xEC, 0x28,             /* sub  rsp,0x28        */
    0x48, 0x8B, 0x49, 0x20,             /* mov  rcx,[rcx+0x20]  */
    0x48, 0x85, 0xC9,                   /* test rcx,rcx         */
    0x74, 0x0C,                         /* je   +0x19           */
    0x45                                /* xor  r8d,r8d 的首字节 */
};

/* stub 布局（固定偏移，避免 fixup 表）：
 *   0x00 hit      mov [rip+d],edx / lock inc [rip+d] / jmp orig
 *   0x20 dispatch cmp edx,key ×3，命中 → hit，否则落到 0x60
 *   0x60 orig     重放 14 字节语义 → jmp ua+0x3DF6520；rcx==0 → 0x82
 *   0x82 null     jmp ua+0x3DF6529（原抛异常路径）
 * 数据区：page+0x800 count(dword)，page+0x804 lastkey(dword)
 */
#define KQS_HIT    0x00
#define KQS_DISP   0x20
#define KQS_ORIG   0x60
#define KQS_NULL   0x82
#define KQS_END    0x90
#define KQS_COUNT  0x800
#define KQS_LASTKEY 0x804

static BYTE *put_jmp_abs(BYTE *p, const void *target)
{
    ULONGLONG t = (ULONGLONG)(ULONG_PTR)target;
    *p++ = 0xFF; *p++ = 0x25;
    *p++ = 0x00; *p++ = 0x00; *p++ = 0x00; *p++ = 0x00;
    memcpy(p, &t, 8); p += 8;
    return p;
}

/* 写一条 `E9 rel32`，目标地址固定 */
static BYTE *put_jmp_rel32(BYTE *p, const BYTE *target)
{
    LONG d = (LONG)(target - (p + 5));
    *p++ = 0xE9;
    memcpy(p, &d, 4); p += 4;
    return p;
}

static BYTE *build_kqskill_probe_stub(BYTE *out, BYTE *ua, BYTE *data)
{
    static const LONG KEYS[3] = { 1160, 2211, 2212 };
    BYTE *p;
    int   i;
    LONG  d;

    /* --- hit：记 key + 计数，然后回 orig --- */
    p = out + KQS_HIT;
    *p++ = 0x89; *p++ = 0x15;                       /* mov [rip+d],edx */
    d = (LONG)((LONG_PTR)(data + 4) - (LONG_PTR)(p + 4));
    memcpy(p, &d, 4); p += 4;
    *p++ = 0xF0; *p++ = 0xFF; *p++ = 0x05;          /* lock inc dword [rip+d] */
    d = (LONG)((LONG_PTR)data - (LONG_PTR)(p + 4));
    memcpy(p, &d, 4); p += 4;
    p = put_jmp_rel32(p, out + KQS_ORIG);

    /* --- dispatch：三个 key --- */
    p = out + KQS_DISP;
    for (i = 0; i < 3; i++) {
        *p++ = 0x81; *p++ = 0xFA;                   /* cmp edx, key */
        memcpy(p, &KEYS[i], 4); p += 4;
        *p++ = 0x0F; *p++ = 0x85;                   /* jne：前两个跳过 jmp，最后一个直接去 orig */
        if (i == 2) d = (LONG)((LONG_PTR)(out + KQS_ORIG) - (LONG_PTR)(p + 4));
        else        d = 5;                          /* 跳过紧随其后的 E9 rel32 */
        memcpy(p, &d, 4); p += 4;
        p = put_jmp_rel32(p, out + KQS_HIT);
    }

    /* --- orig：重放被覆盖的 14 字节语义 --- */
    p = out + KQS_ORIG;
    *p++ = 0x48; *p++ = 0x83; *p++ = 0xEC; *p++ = 0x28;  /* sub rsp,0x28 */
    *p++ = 0x48; *p++ = 0x8B; *p++ = 0x49; *p++ = 0x20;  /* mov rcx,[rcx+0x20] */
    *p++ = 0x48; *p++ = 0x85; *p++ = 0xC9;               /* test rcx,rcx */
    *p++ = 0x0F; *p++ = 0x84;                            /* je .null */
    d = (LONG)((LONG_PTR)(out + KQS_NULL) - (LONG_PTR)(p + 4));
    memcpy(p, &d, 4); p += 4;
    *p++ = 0x45; *p++ = 0x33; *p++ = 0xC0;               /* xor r8d,r8d */
    p = put_jmp_abs(p, ua + 0x03DF6520);                 /* add rsp,0x28 处 */

    /* --- null：原函数 rcx==0 的抛异常路径 --- */
    p = out + KQS_NULL;
    p = put_jmp_abs(p, ua + 0x03DF6529);

    return out + KQS_END;
}

/* ---------------- 注册表只读探测（供探针线程用） --------------------------- */

/* 直接 memcpy 游戏内存可能触发访问违例，先用 VirtualQuery 确认整段可读。
 * 不用 __try/__except：zig cc(clang) 在 mingw 目标下不支持 MSVC 的 SEH 语法。 */
static int rd_ok(ULONGLONG addr, void *out, DWORD n)
{
    MEMORY_BASIC_INFORMATION mbi;
    ULONGLONG end;

    if (!addr) return 0;
    if (VirtualQuery((LPCVOID)(ULONG_PTR)addr, &mbi, sizeof(mbi)) != sizeof(mbi)) return 0;
    if (mbi.State != MEM_COMMIT) return 0;
    if (mbi.Protect & (PAGE_NOACCESS | PAGE_GUARD)) return 0;
    end = (ULONGLONG)(ULONG_PTR)mbi.BaseAddress + mbi.RegionSize;
    if (addr + n > end) return 0;
    memcpy(out, (void *)(ULONG_PTR)addr, n);
    return 1;
}

static ULONGLONG rd_ptr(ULONGLONG a)
{
    ULONGLONG v = 0;
    return rd_ok(a, &v, 8) ? v : 0;
}

static int rd_i32(ULONGLONG a)
{
    int v = -1;
    return rd_ok(a, &v, 4) ? v : -1;
}

/* 读 EquipmentSkill registry 的 index / cache 条数。
 * 路径（与 probe_registry.py 一致）：
 *   v1 = [ua+0x9A19DA0]  v2 = [v1+0x28]  dict = [v2]
 *   index_addr = [dict+0x20]  cache_addr = [dict+0x28]
 *   条目数 = [[addr+0x20]]      （Dictionary 的 _count 在 +0x20） */
static int kq_registry_counts(int *out_index, int *out_cache)
{
    ULONGLONG v1, v2, dict, ia, ca;
    int ic, cc;

    if (!g_ua) return 0;
    v1 = rd_ptr((ULONGLONG)(ULONG_PTR)g_ua + 0x9A19DA0);
    if (!v1) return 0;
    v2 = rd_ptr(v1 + 0x28);
    if (!v2) return 0;
    dict = rd_ptr(v2);
    if (!dict) return 0;
    ia = rd_ptr(dict + 0x20);
    ca = rd_ptr(dict + 0x28);
    if (!ia || !ca) return 0;
    ic = rd_i32(ia + 0x20);
    cc = rd_i32(ca + 0x20);
    if (ic < 0 || ic > 100000 || cc < 0 || cc > 100000) return 0;
    *out_index = ic;
    *out_cache = cc;
    return 1;
}

static DWORD WINAPI kqskill_watch(LPVOID param)
{
    LONG lastkey = -1;
    int  i;

    (void)param;
    for (i = 0; i < 720; i++) {                 /* 5s × 720 = 1 小时 */
        Sleep(5000);

        if (g_kqskill_page) {
            LONG key = *(volatile LONG *)(g_kqskill_page + KQS_LASTKEY);
            if (key != lastkey) {               /* key 变化立刻记，避免刷屏 */
                LONG cnt = *(volatile LONG *)(g_kqskill_page + KQS_COUNT);
                lastkey = key;
                logf("[kqskill] 首次查询到 key=%ld（累计 %ld 次）", key, cnt);
            }
        }

        if (i % 12 == 11) {                     /* 每分钟报一次累计 + 注册表状态 */
            LONG cnt = g_kqskill_page ? *(volatile LONG *)(g_kqskill_page + KQS_COUNT) : 0;
            int  ic = 0, cc = 0;
            if (kq_registry_counts(&ic, &cc))
                logf("[kqskill] Contains 累计 %ld 次  registry index=%d cache=%d  %s",
                     cnt, ic, cc,
                     cc ? "<- cache 已解码，注入时机成熟"
                        : "<- cache 还是空的（还没打开过武器面板）");
            else
                logf("[kqskill] Contains 累计 %ld 次  registry 还没挂上", cnt);
        }
    }
    logf("[kqskill] 探针观测结束");
    return 0;
}

static void apply_keqing_skill(HMODULE ua)
{
    BYTE *b  = (BYTE *)ua;
    BYTE *fn = b + RVA_KQ_CONTAINS;
    BYTE *page, *stub, *end;
    BYTE  entry[LEN_KQ_CONTAINS];
    ULONGLONG sv;
    char  hs[256];

    hexdump(fn, LEN_KQ_CONTAINS, hs, sizeof(hs));
    logf("[kqskill] Contains %p 当前: %s", fn, hs);

    if (fn[0] == 0xFF && fn[1] == 0x25) {
        logf("[kqskill] 已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_KQ_CONTAINS, LEN_KQ_CONTAINS)) {
        logf("[kqskill] !! 原始字节不匹配，拒绝打补丁（客户端版本变了？）");
        return;
    }

    page = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!page) {
        logf("[kqskill] !! VirtualAlloc 失败 err=%lu", GetLastError());
        return;
    }
    memset(page, 0, 0x1000);

    stub = page;
    end  = build_kqskill_probe_stub(stub, b, page + KQS_COUNT);
    FlushInstructionCache(GetCurrentProcess(), stub, (DWORD)(end - stub));
    logf("[kqskill] probe stub @ %p（%d 字节）  计数 @ %p",
         stub, (int)(end - stub), page + KQS_COUNT);

    memset(entry, 0x90, sizeof(entry));
    entry[0] = 0xFF;
    entry[1] = 0x25;
    entry[2] = entry[3] = entry[4] = entry[5] = 0x00;
    sv = (ULONGLONG)(ULONG_PTR)stub;
    memcpy(entry + 6, &sv, 8);

    if (!write_code(fn, entry, LEN_KQ_CONTAINS)) {
        logf("[kqskill] !! 写入口跳转失败 err=%lu", GetLastError());
        return;
    }
    if (memcmp(fn, entry, LEN_KQ_CONTAINS) != 0) {
        logf("[kqskill] !! 写入后回读不一致");
        return;
    }

    g_kqskill_page = page;
    logf("[kqskill] OK  探针已装（只记录，行为不变）");
}

/* ---------------- 抽卡：角色卡弹窗 + 结算页逐格 / 彩光 ---------------------- */

/* 六处都是纯字节改写（NOP / 无条件跳 / mov al,1），没有 stub、没有运行时依赖。
 * 原先是两个 python 脚本现场打（tools\gacha_avatar_popup.py / gacha_settle_fix.py），
 * 由 gacha_popup_patch.bat / gacha_settle_fix.bat 双击调用。现在随 DLL 启动生效。
 *
 * A/B —— 让抽到的角色卡也弹「新物品」演出
 *   A  GachaResultPageContext_New.InitNewStorageDialog 里
 *      `jne 0x1814A0AC1`（短跳 75 4B）会把角色卡整段跳过 -> NOP 掉继续往下走。
 *   B  DropNewItemDialogContext.Create 开头 `if (itemData is AvatarCardDataItem) return null`
 *      （0F 85 9A 00 00 00）-> NOP 掉 6 字节。
 *      只补 A 不补 B 仍然什么都不弹，两处必须成对。
 *   C  UIUtil.DoGachaSequence 的 `jne`（75 23）改成 `jmp`（EB 23），强制走 3D 开箱流程。
 *      默认关：新流程要加载开箱舞台资源，7.0 包可能不全，可能黑屏。
 *
 * D/E/F —— 结算页「十格逐格弹出 + 角色卡彩光」
 *   D/E  OnScrollChange 里把格子 alpha 打成 0 的两处 call（SetupCanvasAlpha(0) /
 *        SetTransparent()）-> NOP。否则动画一直在播但格子透明，只看到最后「啪一下全出来」。
 *   F    PlayVFXSequence 里 `_rareItemList.Contains(id)` 的 call -> `mov al,1`，
 *        等价于「这一格按稀有处理」，彩光节点才会被点亮。
 *        只影响 _dropList（稀有物 / 角色卡 / 碎片），材料不受影响。
 */
#define RVA_GACHA_CARD     0x014A0A74
static const BYTE ORIG_GACHA_CARD[2]  = { 0x75, 0x4B };
static const BYTE PATCH_GACHA_CARD[2] = { 0x90, 0x90 };

#define RVA_GACHA_CREATE   0x0279F6B1
static const BYTE ORIG_GACHA_CREATE[6]  = { 0x0F, 0x85, 0x9A, 0x00, 0x00, 0x00 };
static const BYTE PATCH_GACHA_CREATE[6] = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };

#define RVA_GACHA_NEWBOX   0x0528B1A7
static const BYTE ORIG_GACHA_NEWBOX[2]  = { 0x75, 0x23 };
static const BYTE PATCH_GACHA_NEWBOX[2] = { 0xEB, 0x23 };

#define RVA_SETTLE_ALPHA1  0x014A1E05
static const BYTE ORIG_SETTLE_ALPHA1[5]  = { 0xE8, 0x96, 0x0A, 0x82, 0x01 };
static const BYTE PATCH_SETTLE_ALPHA1[5] = { 0x90, 0x90, 0x90, 0x90, 0x90 };

#define RVA_SETTLE_ALPHA2  0x014A1E73
static const BYTE ORIG_SETTLE_ALPHA2[5]  = { 0xE8, 0x68, 0x00, 0x82, 0x01 };
static const BYTE PATCH_SETTLE_ALPHA2[5] = { 0x90, 0x90, 0x90, 0x90, 0x90 };

#define RVA_SETTLE_GLOW    0x014A2185
static const BYTE ORIG_SETTLE_GLOW[5]  = { 0xE8, 0xC6, 0xEC, 0xD0, 0x03 };
static const BYTE PATCH_SETTLE_GLOW[5] = { 0xB0, 0x01, 0x90, 0x90, 0x90 };

/* 通用：原地改写一小段字节。已是补丁状态就跳过，原始字节对不上就拒绝。 */
static int patch_bytes(const char *tag, BYTE *at, const BYTE *orig,
                       const BYTE *pat, DWORD len)
{
    char hs[256];

    hexdump(at, len, hs, sizeof(hs));
    logf("[%s] %p 当前: %s", tag, at, hs);

    if (memcmp(at, pat, len) == 0) {
        logf("[%s] 已在位，跳过", tag);
        return 1;
    }
    if (!is_original(at, orig, len)) {
        logf("[%s] !! 原始字节不匹配，拒绝打补丁（客户端版本变了？）", tag);
        return 0;
    }
    if (!write_code(at, pat, len)) {
        logf("[%s] !! 写入失败 err=%lu", tag, GetLastError());
        return 0;
    }
    if (memcmp(at, pat, len) == 0) {
        logf("[%s] OK", tag);
        return 1;
    }
    logf("[%s] !! 写入后回读不一致", tag);
    return 0;
}

static void apply_gacha(HMODULE ua)
{
    BYTE *b = (BYTE *)ua;

    if (g_gacha_popup) {
        patch_bytes("gacha/card",   b + RVA_GACHA_CARD,
                    ORIG_GACHA_CARD, PATCH_GACHA_CARD, sizeof(ORIG_GACHA_CARD));
        patch_bytes("gacha/create", b + RVA_GACHA_CREATE,
                    ORIG_GACHA_CREATE, PATCH_GACHA_CREATE, sizeof(ORIG_GACHA_CREATE));
    } else {
        logf("[gacha] popup 未启用（ini [gacha] popup=0）");
    }

    if (g_gacha_newbox) {
        patch_bytes("gacha/newbox", b + RVA_GACHA_NEWBOX,
                    ORIG_GACHA_NEWBOX, PATCH_GACHA_NEWBOX, sizeof(ORIG_GACHA_NEWBOX));
    } else {
        logf("[gacha] newbox 未启用（ini [gacha] newbox=0）");
    }

    if (g_gacha_settle) {
        patch_bytes("settle/alpha1", b + RVA_SETTLE_ALPHA1,
                    ORIG_SETTLE_ALPHA1, PATCH_SETTLE_ALPHA1, sizeof(ORIG_SETTLE_ALPHA1));
        patch_bytes("settle/alpha2", b + RVA_SETTLE_ALPHA2,
                    ORIG_SETTLE_ALPHA2, PATCH_SETTLE_ALPHA2, sizeof(ORIG_SETTLE_ALPHA2));
    } else {
        logf("[gacha] settle 未启用（ini [gacha] settle=0）");
    }

    if (g_gacha_glow) {
        patch_bytes("settle/glow", b + RVA_SETTLE_GLOW,
                    ORIG_SETTLE_GLOW, PATCH_SETTLE_GLOW, sizeof(ORIG_SETTLE_GLOW));
    } else {
        logf("[gacha] glow 未启用（ini [gacha] glow=0）");
    }
}

/* ---------------- [m43kit] 刻晴 M43 武器槽改写 ------------------------------ */

/* 技能 ID -> 该槽的六个 float 参数。数值取自 server 的 WeaponData.json，
 * 与 keqing_m43_kit.py 写入的值完全一致（那条路已实测出剑气）。 */
typedef struct {
    int   id;
    float p1, p2, p3, a1, a2, a3;
} M43_KIT;

static const M43_KIT g_m43_kits[] = {
    /* M42 主动：Skill_Mei_Weapon_42 —— 刻晴原生剑气 */
    { 1159, 0.126805f, 0.0f,       0.0f,       0.000473f, 0.0f,       0.0f },
    /* M42 被动：Equip_Katana_M42_PassiveSkill（automatic=1，客户端已烘焙） */
    { 2210, 3.420118f, 0.126805f,  3.420118f,  0.011834f, 0.000473f,  0.011834f },
};

/* 按 ini 的三个槽位拼出 84 字节目标值；遇到不在内置表里的技能就放弃 */
static int m43_build_target(void)
{
    int i;
    memset(g_m43_target, 0, sizeof(g_m43_target));
    for (i = 0; i < 3; i++) {
        BYTE *slot = g_m43_target + (size_t)i * M43_SLOT;
        const M43_KIT *k = NULL;
        size_t j;
        int id = g_m43_want[i];
        if (!id) continue;                              /* 0 = 清空 */
        for (j = 0; j < sizeof(g_m43_kits) / sizeof(g_m43_kits[0]); j++) {
            if (g_m43_kits[j].id == id) { k = &g_m43_kits[j]; break; }
        }
        if (!k) {
            logf("[m43kit] 技能 %d 不在内置表（只有 1159/2210），放弃本次改写", id);
            return 0;
        }
        memcpy(slot, &k->id, 4);
        memcpy(slot + 4, &k->p1, 24);                   /* p1 p2 p3 a1 a2 a3 */
    }
    return 1;
}

/* 行首三元组的已知取值。客户端每次重载武器表都是**原样**（1160/2211/2212），
 * 但为了 python 工具已经改过、或本工具上一轮已改过的情况下仍能重新定位，
 * 这里把所有见过的状态都认下来。p1 是 prop1Param1，用来把 (1159,0,0) 这种
 * 天然有歧义的三元组钉死（M43 原值 21.76419 / 借用 M42 后 0.126805）。 */
typedef struct {
    int   s1, s2, s3;
    float p1;
} M43_ALIAS;

static const M43_ALIAS g_m43_alias[] = {
    { 1160, 2211, 2212, 21.76419f },   /* 原样：server WeaponData / 客户端解包值 */
    { 1159, 2135, 2136,  0.126805f },  /* python --kit jm   */
    { 1159, 2210,    0,  0.126805f },  /* python --kit m42  */
    { 1159,    0,    0,  0.126805f },  /* 本工具目标态 --kit wave */
};

static int m43_row_match(const BYTE *q, ULONG_PTR cand)
{
    int   v0 = 0, v1 = 0, v2 = 0;
    float f1 = 0.0f;
    size_t i;

    memcpy(&v0, q, 4);
    if (!rd_ok((ULONGLONG)cand + 4, &f1, 4)) return 0;

    for (i = 0; i < sizeof(g_m43_alias) / sizeof(g_m43_alias[0]); i++) {
        const M43_ALIAS *a = &g_m43_alias[i];
        float d;

        if (v0 != a->s1) continue;
        if (!rd_ok((ULONGLONG)cand + M43_SLOT, &v1, 4) || v1 != a->s2) continue;
        if (!rd_ok((ULONGLONG)cand + M43_SLOT * 2, &v2, 4) || v2 != a->s3) continue;
        d = f1 - a->p1;
        if (d < 0.0f) d = -d;
        if (d > 1e-4f) continue;
        return 1;
    }
    return 0;
}

/* 在**自己进程**里扫行首：int s1 @A, s2 @A+0x1C, s3 @A+0x38。
 *
 * 关键：不要直接在游戏堆上走 memchr —— 即便 VirtualQuery 报 MEM_COMMIT +
 * PAGE_READWRITE，直接解引用也可能踩到刚被释放的页，整个客户端当场崩。
 * 所以统一走 rd_ok 把 64KB 一块拷进自己的缓冲区再搜。
 *
 * 首字节候选两个：1160 = 0x488 -> 0x88，1159 = 0x487 -> 0x87。 */
static BYTE g_m43_scanbuf[0x10000 + M43_ROW_LEN];

static ULONGLONG m43_find_row(DWORD *p_regions, ULONGLONG *p_mb, DWORD *p_cands)
{
    MEMORY_BASIC_INFORMATION mbi;
    ULONG_PTR addr = 0;            /* 用整数累加：NULL 上做指针算术是 UB */
    DWORD     regions = 0;
    ULONGLONG bytes = 0;
    DWORD     cands = 0;

    while (VirtualQuery((LPCVOID)addr, &mbi, sizeof(mbi)) == sizeof(mbi)) {
        BYTE prot = (BYTE)(mbi.Protect & 0xFF);

        /* 只排除「不可读」的页：NOACCESS / GUARD。
         * 不能要求 prot 恰好等于 RW —— Unity 堆上有 EXECUTE_READWRITE、
         * 文件映射上有 EXECUTE_READ，写死会整片漏扫（python 版只排除 GUARD）。 */
        if (mbi.State == MEM_COMMIT &&
            prot != 0 && prot != PAGE_NOACCESS &&
            !(mbi.Protect & PAGE_GUARD)) {
            BYTE   *base = (BYTE *)mbi.BaseAddress;
            SIZE_T  size = mbi.RegionSize;

            if (size >= M43_ROW_LEN) {
                SIZE_T limit = size - M43_ROW_LEN;   /* 最后一个合法候选偏移 */
                SIZE_T off = 0;

                regions++;
                bytes += (ULONGLONG)size;

                while (off <= limit) {
                    SIZE_T chunk = limit - off + 1;
                    BYTE  *q, *qe;

                    if (chunk > 0x10000) chunk = 0x10000;
                    if (!rd_ok((ULONGLONG)(ULONG_PTR)(base + off),
                               g_m43_scanbuf, (DWORD)chunk)) {
                        off += chunk;
                        continue;
                    }

                    q  = g_m43_scanbuf;
                    qe = g_m43_scanbuf + chunk;
                    while (q < qe) {
                        BYTE *a = (BYTE *)memchr(q, 0x88, (size_t)(qe - q));
                        BYTE *b = (BYTE *)memchr(q, 0x87, (size_t)(qe - q));
                        BYTE *n = (a && b) ? (a < b ? a : b) : (a ? a : b);

                        if (!n) break;
                        cands++;
                        {
                            ULONG_PTR cand = (ULONG_PTR)(base + off + (n - g_m43_scanbuf));
                            if ((cand & 3) == 0 && m43_row_match(n, cand)) {
                                if (p_regions) *p_regions = regions;
                                if (p_mb) *p_mb = bytes >> 20;
                                if (p_cands) *p_cands = cands;
                                return (ULONGLONG)cand;
                            }
                        }
                        q = n + 1;
                    }
                    off += chunk;
                }
            }
        }

        if (!mbi.RegionSize) break;
        addr = (ULONG_PTR)mbi.BaseAddress + mbi.RegionSize;
        if ((ULONGLONG)addr > 0x7FFFFFFFFFFFULL) break;
    }

    if (p_regions) *p_regions = regions;
    if (p_mb) *p_mb = bytes >> 20;
    if (p_cands) *p_cands = cands;
    return 0;
}

static int m43_write_row(ULONGLONG row)
{
    void *p = (void *)(ULONG_PTR)row;
    DWORD old = 0;
    BYTE  back[M43_ROW_LEN];

    if (!VirtualProtect(p, M43_ROW_LEN, PAGE_READWRITE, &old)) return 0;
    memcpy(p, g_m43_target, M43_ROW_LEN);
    VirtualProtect(p, M43_ROW_LEN, old, &old);

    if (!rd_ok(row, back, M43_ROW_LEN)) return 0;
    return memcmp(back, g_m43_target, M43_ROW_LEN) == 0;
}

/* 守护线程：命中后只校验 84 字节（零开销）；行被表卸载/重载掉才重新全扫。
 * 未命中时 3s 重试，每 60s 打一条统计，方便判断「是没扫到」还是「根本没触发」。 */
static DWORD WINAPI m43_watch(LPVOID param)
{
    int   logged = 0;
    DWORD last_nf = 0;

    (void)param;
    for (;;) {
        ULONGLONG row;
        DWORD     regions = 0, cands = 0, t0, ms;
        ULONGLONG mb = 0;

        Sleep(1500);

        if (g_m43_row) {
            BYTE cur[M43_ROW_LEN];
            if (rd_ok(g_m43_row, cur, M43_ROW_LEN) &&
                memcmp(cur, g_m43_target, M43_ROW_LEN) == 0) {
                continue;                       /* 已经是目标状态，什么都不做 */
            }
            g_m43_row = 0;                      /* 地址失效或被还原 */
            logged = 0;
        }

        if ((LONG)(GetTickCount() - g_m43_next_scan) < 0) continue;

        t0  = GetTickCount();
        row = m43_find_row(&regions, &mb, &cands);
        ms  = GetTickCount() - t0;

        if (!row) {
            g_m43_next_scan = GetTickCount() + 3000;
            if (!last_nf || GetTickCount() - last_nf > 60000) {
                last_nf = GetTickCount();
                logf("[m43kit] 未命中：%u 区 / %llu MB / %u 个 0x87|0x88 候选 / %u ms",
                     regions, (unsigned long long)mb, cands, ms);
            }
            continue;
        }

        if (!logged) {
            BYTE orig[M43_ROW_LEN];
            if (rd_ok(row, orig, M43_ROW_LEN)) {
                int s1 = 0, s2 = 0, s3 = 0;
                memcpy(&s1, orig, 4);
                memcpy(&s2, orig + M43_SLOT, 4);
                memcpy(&s3, orig + M43_SLOT * 2, 4);
                logf("[m43kit] 命中 row=%p 原始槽 %d/%d/%d（%u 区 / %llu MB / %u 候选 / %u ms）",
                     (void *)(ULONG_PTR)row, s1, s2, s3,
                     regions, (unsigned long long)mb, cands, ms);
            }
        }

        if (m43_write_row(row)) {
            g_m43_row = row;
            g_m43_next_scan = 0;
            logged = 1;
            logf("[m43kit] 已改写 -> %d/%d/%d", g_m43_want[0], g_m43_want[1], g_m43_want[2]);
        } else {
            g_m43_next_scan = GetTickCount() + 3000;
            logf("[m43kit] 写入失败 row=%p", (void *)(ULONG_PTR)row);
        }
    }
    return 0;
}

static void apply_m43kit(HMODULE ua)
{
    (void)ua;
    if (!m43_build_target()) return;
    logf("[m43kit] 启动守护：找 1160/%d/%d 的行，改成 %d/%d/%d",
         M43_SIG2, M43_SIG3, g_m43_want[0], g_m43_want[1], g_m43_want[2]);
    CreateThread(NULL, 0, m43_watch, NULL, 0, NULL);
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

    g_ua = ua;

    load_cfg();

    apply_norefresh(ua);
    apply_oncheck(ua);

    if (g_card_enabled) {
        apply_card(ua);
        CreateThread(NULL, 0, card_watch, NULL, 0, NULL);
    } else {
        logf("[card] 未启用（ini [card] enabled=0）");
    }

    if (g_cover_enabled) {
        apply_cover(ua);
        CreateThread(NULL, 0, cover_watch, NULL, 0, NULL);
    } else {
        logf("[cover] 未启用（ini [cover] enabled=0）");
    }

    if (g_tcover_enabled) {
        apply_tcover(ua);
        CreateThread(NULL, 0, tcover_watch, NULL, 0, NULL);
    } else {
        logf("[titleimg] 未启用（ini [titleimg] enabled=0）");
    }

    if (g_luastate_enabled) {
        apply_luastate(ua);
    }

    if (g_qt_enabled) {
        if (g_luastate_enabled) {
            apply_qtpatch(ua);
        } else {
            logf("[qtpatch] !! 需要 [luastate] enabled=1 才能拿到 lua_State，跳过");
        }
    } else {
        logf("[qtpatch] 未启用（ini [qtpatch] enabled=0）");
    }

    if (g_qtwave_enabled) {
        apply_qtwave(ua);
    }

    if (g_kq_enabled) {
        apply_keqing(ua);
        // 同一个判据两个重载都要拦：客户端不同界面调的不一定是同一个版本。
        // 万一 int 版那刀打出问题，改 ini 的 visible_int=0 就能单独关掉它。
        if (g_kq_visible_int) {
            apply_keqing_int(ua);
        } else {
            logf("[keqing] int 版跳过（ini [keqing] visible_int=0）");
        }
    } else {
        logf("[keqing] 未启用（ini [keqing] visible=0）");
    }

    if (g_kqskill_probe) {
        apply_keqing_skill(ua);
        CreateThread(NULL, 0, kqskill_watch, NULL, 0, NULL);
    } else {
        logf("[kqskill] 未启用（ini [keqing_skill] probe=0）");
    }

    apply_gacha(ua);

    if (g_m43_enabled) {
        apply_m43kit(ua);
    } else {
        logf("[m43kit] 未启用（ini [m43kit] enabled=0）");
    }

    if (g_label_enabled) {
        apply_label(ua);
        apply_label_lg(ua);
        CreateThread(NULL, 0, label_watch, NULL, 0, NULL);
    } else {
        logf("[label] 未启用（ini [label] enabled=0）");
    }

    if (g_title_enabled) {
        apply_title(ua);
        CreateThread(NULL, 0, title_watch, NULL, 0, NULL);
    } else {
        logf("[title] 未启用（ini [title] enabled=0）");
    }

    if (g_entry_enabled) {
        if (g_unlock) apply_unlock(ua);
        apply_entry(ua);
        CreateThread(NULL, 0, entry_click_log, NULL, 0, NULL);
    } else {
        logf("[entry] 未启用（ini enabled=0），只保留资源校验");
    }

    CreateThread(NULL, 0, watch, NULL, 0, NULL);

    logf("=== bh3bypass done（资源校验 6 处 + 模式入口）===");
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
