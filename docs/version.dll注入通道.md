# BH3 7.0 的 version.dll 注入通道（已打通）

> 日期：2026-11-03
> 来源思路：`D:\ys5.4\5.4.0天机端` 的补丁链（见 `docs\启动链对照_ys5.4天机端.md`）
> 结论：**通道成立，代码已在 BH3.exe 进程里跑起来了。** ACE 没有拦。

---

## 1. 一句话

`UnityPlayer.dll` 导入 `VERSION.dll`，而 `version` 不在 KnownDLLs、`SafeDllSearchMode` 默认开启
→ 把 `version.dll` 放到 `BH3.exe` 同目录，游戏就会加载**我们的**那份，而不是 `system32` 的。
我们那份只做两件事：把 17 个导出转发给真身、在 `DllMain` 里 `LoadLibrary` 任意 DLL。

---

## 2. 三层结构（别混）

| 层 | 干什么 | 谁做的 | 是否改代码 |
|---|---|---|---|
| **DLL 侧加载** | 换掉一个本来就会被加载的 DLL | `version.dll` | 否 |
| **导出转发** | 让换掉的 DLL 行为完全透明（17/17 转发到 `system32\version.dll`） | `version.dll` | 否 |
| **API hook / detour** | 改目标函数的机器码 | 将来我们自己的载荷 | 是 |

前两层只是"混进去"，第三层才是"改逻辑"。天机端的 `hk4echeckbypass.dll`、`rsapatch.dll` 属于第三层。

---

## 3. 可行性依据（三条，都是查出来的）

| 检查 | 结果 |
|---|---|
| `UnityPlayer.dll` 导入表 | 有 `VERSION.dll`，用到 6 个函数：`GetFileVersionInfoSizeW/W`、`GetFileVersionInfoW/A`、`VerQueryValueW/A` |
| `version` 是否在 KnownDLLs | **不在**（31 项全列过） |
| `SafeDllSearchMode` | 未设置 = 默认 1，**应用目录优先于 system32** |

---

## 4. 实测记录

### 4.1 第一次：借天机端的 VersionShim

放进 `D:\BH3\7.0\`：`version.dll`(12.8 KB) + 空 `libraries.txt`，启动 BH3.exe，查模块表：

```
ModuleName   FileName
version.dll  D:\BH3\7.0\version.dll            ← 我们的
version.DLL  c:\windows\system32\version.DLL   ← 转发目标
```

游戏正常运行（636 MB / 115 线程 / 响应）。**说明：侧加载成功、转发透明、ACE 不拦。**

### 4.2 第二次：自编译垫片 + 载荷

用自己写的 `version.c` + `bh3probe.c`，`libraries.txt` 写 `bh3probe.dll`，启动后：

`verhook.log`（垫片写的）
```
attach pid 0x7bfc
  load bh3probe.dll 0x7ff8516c0000
done
```

`bh3probe.log`（**载荷**写的，证明我们的代码在游戏进程里执行了）
```
=== bh3probe attached ===
pid           = 0x7bfc
this module   = 0x7ff8516c0000
BH3.exe       = 0x140000000
UnityPlayer   = 0x0
BH3Base       = 0x7fffb3e90000
version(real) = 0x7ff851b20000
```

游戏窗口 `Honkai Impact 3` 正常出现，844 MB，无错误框。

---

## 5. 工具

### `tools\verhook_ctl.py`

```
python tools\verhook_ctl.py build        # 编译垫片 + 探针（需要 ziglang）
python tools\verhook_ctl.py install      # 装进游戏目录
python tools\verhook_ctl.py status       # 看装没装 + 日志尾部
python tools\verhook_ctl.py uninstall    # 清干净
```

行为约定：

* 安装前检查游戏是否在跑，跑着不动
* 目标位置若已有 `version.dll`，先备份 `.orig`，卸载时还原
* 如果已有的是**我们自己的**（内容相同），不重复备份

### 源码（`_ext\verhook\`）

| 文件 | 作用 |
|---|---|
| `version.c` | 垫片。17 个导出全部 `GetProcAddress` 转发到 `system32\version.dll`；`DllMain` 读 `libraries.txt`，写 `verhook.log` |
| `bh3probe.c` | 最小载荷。只写日志：pid、模块基址（用来验证通道，不改任何行为） |
| `version.dll` / `bh3probe.dll` | 编译产物（188 KB / 183 KB） |

### 编译环境

本机**没有 MSVC / Visual Studio**，用 pip 装的 zig：

```
pip install ziglang            # 98 MB，带完整 zig 编译器
python -m ziglang cc -target x86_64-windows-gnu -shared -O2 -o version.dll version.c
```

实测 zig 0.16.0 可用。产物是 `x86_64` DLL，导入 `api-ms-win-crt-*`（Win10+ 自带，无需额外运行时）。

---

## 6. 拿它能做什么（按价值排序）

| 目标 | 怎么做 | 难度 |
|---|---|---|
| **干掉 mitmproxy + CA** | hook Unity 的证书校验函数（`UnityWebRequest`/`CertVerifyCallback` 相关），让客户端接受我们的自签证书 | 中 |
| **在进程内拦 ASB 内容校验** | 定位 `Blocks.xmf`/`BlockMeta` 的比对函数，hook 成恒等 → 改 `.wmv` 那条死路可能复活 | 中高 |
| 直接读写 `DressData` 等托管结构 | 拿到 `GameAssembly.dll` 基址 + il2cpp 导出，比外部读写稳 | 中 |
| 日志/调试 | hook `CreateFileW`、`connect`、`send`，看客户端到底在访问什么 | 低 |

第一条的收益最直接：现在整条链的复杂度（CA 安装、SNI、Host 探测、IP 不能环回）**几乎全部来自 TLS 拦截**。
如果客户端能接受我们的证书，链就退化成 `nginx + Server.exe`，和天机端一样干净。

---

## 7. 已知限制 / 坑

1. **`DllMain` 执行时 `UnityPlayer.dll` 还没进模块表**（日志里 `UnityPlayer = 0x0`）。
   因为 `version.dll` 是 `UnityPlayer.dll` 的依赖，在它加载过程中就被拉起来了。
   → 想 hook `UnityPlayer.dll` 的导出，不能在 `DllMain` 里做，要起线程等它加载完
   （`GetModuleHandleA("UnityPlayer.dll")` 轮询到非 0 再动手）。
2. **`BH3Base.dll` 在 `DllMain` 时已就位**（`0x7fffb3e90000`），它是 ACE 的集成模块。
   目前 ACE 没拦，但不代表以后不会；每次改载荷都要重新验一遍"游戏能否进到登录界面"。
3. **导出签名必须全对**：mingw 的 `winver.h` 把 `VerFindFileA/W` 声明成 `LPSTR`（非 const），
   `VerInstallFileA/W` 是 8 个参数。签名写错会编不过，别照 MSDN 抄。
4. 转发函数用 `UINT_PTR` 泛型接 8 个参数，在 x64 下 ABI 安全（同一套寄存器），
   但**不要**在 32 位目标上这么干。

---

## 8. 回滚

```
python tools\verhook_ctl.py uninstall
```

会删掉 `version.dll` / `libraries.txt` / `bh3probe.dll` / 日志，并还原 `.orig`。
当前状态：**客户端目录是干净的**（已验证 `version.dll`、`libraries.txt`、`bh3probe.dll` 均不存在）。
