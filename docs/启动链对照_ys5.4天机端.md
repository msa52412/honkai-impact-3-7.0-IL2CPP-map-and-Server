# 启动链对照：ys5.4 天机端 vs 本项目 BH3

> 对象：`D:\ys5.4\5.4.0天机端`（LunaGC 5.4 整合端）、`D:\ys5.4\GenshinImpact_5.4.0`（原神客户端）
> 对照：本项目 `_launch\launch_v2.py` + `launch_all.py`
> 日期：2026-11-03

---

## 0. 一句话

| | 他怎么绕认证 | 我们怎么绕认证 |
|---|---|---|
| 手段 | **进程内 DLL 劫持 + hook**（`version.dll` → 加载两个补丁 DLL） | **中间人证书**（mitmdump + 自签 CA 进受信任根） |
| 代价 | 要动客户端目录（放 3 个 DLL，退出复原） | 要装 CA、要处理 SNI、要 Host 探测 |
| 副作用 | 无（客户端自己不再校验） | 每次换网段要重探 Host；IP 目标握手失败 |

他那套能成立，是因为**原神 5.4 的通信是 HTTP + KCP**，本来就没有 TLS 可拦；把校验函数 hook 掉，客户端就直接连 `127.0.0.1` 了。
BH3 走 HTTPS，所以我们被迫用 mitmproxy。

---

## 1. 他的启动链（逐条有据）

### 1.1 目录角色

| 路径 | 角色 |
|---|---|
| `启动.exe` (550 KB, PE32+ x64, 2025-04-04) | 主启动器。**Rust 写的**（字符串里有 `C:\Users\Tianjg\.cargo\registry\...`），版本资源 `Ys Launcher 0.1.0` |
| `补丁\` | 4 个待注入文件 + 清单，**启动时复制进游戏目录，退出时删除** |
| `Runtime\` | 自带 JRE 17（`Runtime\java\bin\java.exe`）、`mongod.exe`、`mongos.exe`、`vc_redist.x64.exe` |
| `lib\Tianjg5.4.0.jar` (98 KB) + `lib\Resources.zip` (413 KB) | 服务端本体 + 资源包 |
| `存档\Database` | mongod 的 `--dbpath` |
| `config.json` (5 KB) | Grasscutter 配置：HTTP `:521`、游戏 `:22101`、`dispatchUrl=ws://127.0.0.1:1111`、mongodb 指向 localhost:27017 |
| `path_cache.txt` | 一行 `game=D:\ys5.4\GenshinImpact_5.4.0`，记住上次选的游戏目录 |
| `GenshinImpact_5.4.0\config.json` (52 B) | `{"proxy_host":"127.0.0.1","proxy_port":521}` —— 给代理进程读的转发目标 |

### 1.2 启动流程（从 `启动.exe` 里的中文串还原，顺序即字符串表顺序）

```
1  Launcher   自提权：powershell -Command "Start-Process -FilePath ... -Verb RunAs"
               失败 → "请求管理员权限失败:"
2  GameFinder 选游戏目录，校验含 YuanShen.exe 或 GenshinImpact.exe
               失败 → "选择的目录不包含游戏主程序"
3  Patch      备份 mhypbase.dll → mhypbase.dll.bak
               复制 补丁\rsapatch.dll / version.dll / hk4echeckbypass.dll / libraries.txt 进游戏目录
               → "正在复制补丁" / "补丁文件 X 不存在"
4  Cleanup    删 logs\latest.log（避免旧日志混淆）→ "无法删除日志文件"
5  Proxy      写注册表 HKCU\...\Internet Settings 的 ProxyEnable / ProxyServer
6  Server     起 mongod.exe --dbpath 存档\Database  → "启动数据库服务..."
               起 Runtime\java\bin\java.exe -jar lib\Tianjg5.4.0.jar → "正在启动服务器..."
               轮询等待 → "正在等待服务器启动 / 已等待 X 秒..."
7  Client     起 GenshinImpact.exe → "游戏启动完成"
8  Monitor    监控线程：游戏进程结束 → 走收尾
9  Cleanup    taskkill /F /IM <java|mongod|HttpProxy> → "已终止 X 进程"
               恢复代理设置 → "已恢复代理设置"
               复原补丁：删掉复制进去的 4 个文件，mhypbase.dll.bak 改回 mhypbase.dll
```

另外还有一个 **仅服务器模式**（"仅服务器模式"），即不启游戏只起服务端。

### 1.3 补丁链（这套东西的核心）

```
BH3/Genshin 客户端加载 VERSION.dll（UnityPlayer.dll 的导入表里有）
        ↓  同目录优先（version 不在 KnownDLLs）
    本地 version.dll  ← 这是 VersionShim（PDB 路径：C:\Users\Window\Desktop\VersionShim-master\x64\Release\VersionShim.pdb）
        ↓  把 GetFileVersionInfo* 等全部转发到 c:\windows\system32\version.*
        ↓  DllMain 里读 libraries.txt，逐个 LoadLibrary
    ├─ hk4echeckbypass.dll (533 KB)  → hook 客户端的完整性/环境校验
    └─ rsapatch.dll        (654 KB)  → hook dispatch 响应的 RSA 验签
```

再加一条：

| 文件 | 原始 | 替换后 |
|---|---|---|
| `mhypbase.dll` | 26 MB（HoYoKProtect 反作弊模块） | **139 KB 空壳** |

**一句话总结他的补丁策略：把"校验"和"验签"两个函数在进程内改成 return true，再把反作弊模块换成一个空壳。**

### 1.3.1 澄清：`version.dll` 不是 hook

很多人把这一层叫"hook 了系统 API"，**不对**。实测它的 PE 导出表：

| 项 | 结果 |
|---|---|
| 导出函数 | 17 个，与系统 `version.dll` 完全一致 |
| 其中 **转发（forwarder）** | **17 / 17** |
| 转发目标 | `c:\windows\system32\version.GetFileVersionInfoW` 等 |
| 是否导入 `VirtualProtect` / `GetProcAddress` | **都没有** |
| 实际导入 | `OpenFile / ReadFile / LoadLibraryA / GetModuleFileNameA / lstrcmpA / MessageBoxA` |

也就是说：**它的导出全部是 PE 导出表里的转发字符串**，加载器解析导出时直接跳到 system32 里的真函数，一行代码都不执行。
它唯一执行的代码在 `DllMain`：读同目录 `libraries.txt`，逐行 `LoadLibraryA`。

这一层叫 **DLL 劫持 / 侧加载（DLL side-loading）**，靠的是 DLL 搜索顺序（应用目录优先），不是 hook。
**真正 hook 的是被它加载的那两个 DLL**：

| DLL | hook 相关导入 | 说明 |
|---|---|---|
| `hk4echeckbypass.dll` | `VirtualProtect`、`GetProcAddress`、`GetModuleHandleW`、`LoadLibraryA/W/ExA` | 典型的 inline hook 组合：改页属性 → 定位目标函数 → 改函数头 |
| `rsapatch.dll` | `VirtualProtect`、`GetProcAddress`、`GetModuleHandleW/A`、`CreateThread`、`CreateFileW` | 同上，另起线程做后续工作并落日志 |

三层概念要分清：

| 层 | 干什么 | 这里谁做的 |
|---|---|---|
| DLL 劫持（side-loading） | 换掉一个会被加载的 DLL | `version.dll` |
| 导出转发（export forwarding） | 让换掉的 DLL 行为完全透明 | `version.dll`（17/17 转发） |
| API hook / detour | 改函数机器码 | `hk4echeckbypass.dll`、`rsapatch.dll` |

**为什么必须转发？** 因为游戏会调 `GetFileVersionInfo` 等函数取自身版本信息，不转发就会失败或崩。
转发让这一层"零影响"，于是同一个 shim 可以配任意 `libraries.txt`——这就是它能做成通用加载器的原因。

**为什么不直接 hook 系统 API？** 因为他要改的是**游戏自己的函数**（hk4e 的校验、RSA 验签），不是系统 API；
而且 hook 系统 API 更容易被反作弊扫到，劫持一个本来就该被加载的 DLL 反而最不显眼。

---

## 2. 我们的链

```
vbs 外壳（ShellExecute runas 提权）
  └─ launch_all.py start
       ├─ 1 mitmdump :8891        ← 系统代理指向它，负责 TLS 解密
       ├─ 2 nginx    :80          ← 静态资源（asset_bundle / webui）
       ├─ 3 Server.exe :443/:8081/:16100
       ├─ 4 证书检查（mitmproxy CA 进受信任根；Server 自己的 pfx 只自签）
       ├─ 5 Host 探测（写 config.json 的 Gameserver.Host）
       ├─ 6 起 BH3.exe
       └─ 7 打内存补丁（抽卡弹窗 / 结算 / 刻晴）
```

关键差异：**我们的"补丁"是运行时内存补丁（python 调 il2cpp 方法），不落盘、不改客户端文件**；他的补丁是**磁盘 DLL**，改了要复原。

---

## 3. 逐项对照

| 维度 | ys5.4 天机端 | 本项目 BH3 | 谁更好 |
|---|---|---|---|
| 认证绕过 | 进程内 hook（DLL） | 中间人证书（mitmproxy） | 他干净，但依赖游戏走 HTTP |
| 注入点 | `version.dll`（UnityPlayer 导入） | 3Dmigoto `d3d11.dll`（只用于截帧） | — |
| 代理 | 系统代理 → 本地 `HttpProxy.exe` → `:521` | 系统代理 → mitmdump `:8891` → nginx `:80` | 平 |
| 数据库 | mongod（独立进程，`--dbpath 存档`） | sqlite（`run\data\pemukul.sqlite`，WAL） | 我们简单 |
| 服务端 | Java + jar（自带 JRE 17） | .NET `Server.exe`（`dotnet publish` 产物） | 平 |
| 提权 | 自己 `Start-Process -Verb RunAs` | 靠 `一键启动.vbs` 外壳 | 他更内聚 |
| 补丁生命周期 | 复制进目录 → 退出复原 | 内存补丁，无需复原 | **我们** |
| 守护 | **有监控线程**，游戏退出自动收尾 | **没有**，要手动跑 `stop` | **他** |
| 日志 | 单文件 `logs\latest.log` | `_launch\logs\launcher_<日期>.log` + Tee | 平 |
| 分发形态 | 单 exe + 目录 | python 脚本 + vbs | 他更像成品 |

---

## 4. 可以直接搬过来的三件事

### 4.1 守护线程 + 退出自动收尾（我们真缺）

他：`Monitor` 线程轮询游戏进程，退出即 taskkill 服务端、恢复代理、复原补丁。
我们：`launch_all.py` 的 `finish()` 只打印 GM 地址就结束，服务端栈一直挂着，要手动 `stop`。

落点：`launch_all.py` 加 `--watch`（默认开），在 `cmd_start` 末尾起一个线程：

```python
def watch_client(root, keep_proxy=False):
    while True:
        time.sleep(5)
        if client_pid() is None:
            head("客户端已退出，自动收尾")
            lv.cmd_stop(stack_args(keep_proxy=keep_proxy))
            return
```

好处：以后不再出现"游戏关了、mitmproxy 还在、系统代理没恢复导致浏览器上不了网"。

### 4.2 补丁文件的备份 / 复原

我们现在用 3Dmigoto 时是把 `d3d11.dll` 直接放进客户端目录的（`_ext\himi\dev\3dmigoto\d3d11.dll`），**没有备份、没有复原**。
他的做法值得抄：进目录前先 `.bak`，退出时删新增、改回备份。

### 4.3 单文件自提权

他用 `Start-Process -Verb RunAs` 自提权，省掉一个 vbs 外壳。
我们保留 vbs 也没问题（`runas` 拿不到 stdout，vbs 反而是正解），但可以让 `launch_all.py` 在检测到非管理员时**自动重新以管理员启动自己**，这样直接双击 py 也行。

---

## 5. 顺带发现：BH3 也能用 `version.dll` 注入

查了三件事：

| 检查 | 结果 |
|---|---|
| `UnityPlayer.dll` 的导入表 | **有 `VERSION.dll`**（22 个导入 DLL 之一） |
| `version` 是否在 KnownDLLs | **不在**（31 项已列，含 psapi、combase 等，无 version） |
| `SafeDllSearchMode` | 未显式设置 = 默认 1，**应用目录优先于 system32** |

结论：**把 `version.dll`（VersionShim）+ `libraries.txt` 放到 `D:\BH3\7.0\` 下，就能往 BH3 进程里注入任意 DLL。**
这条路和他完全一致，不需要 3Dmigoto，也不需要 ACE 的任何东西。

这比我们现有的能力高一档：

| 现有手段 | 能做什么 | 不能做什么 |
|---|---|---|
| 3Dmigoto（d3d11） | 截帧、改 shader、改 draw call | 跑不了业务逻辑、碰不到 il2cpp |
| 运行时内存（icall/RVA） | 调 il2cpp 方法、改对象字段 | 只能在外部进程，没法 hook 原生函数 |
| **version.dll + hook DLL** | **hook 任意原生函数**（含 SSL 校验、文件校验） | 要写 C++，且要过 ACE |

潜在的三个用法（按价值排序）：

1. **干掉 mitmproxy**：hook 掉 Unity 的证书校验，让客户端接受我们的自签证书 → 不再需要装 CA、不再有 SNI/IP 握手问题。
2. **在进程内拦 ASB 校验**：比 nginx 层更靠内，`Blocks.xmf`/`BlockMeta` 的比对函数如果被 hook 成恒等，改包就活了（这是之前判定"死路"的那条）。
3. 直接改 `DressData` 之类的内存结构，比外部读写更稳。

**风险**：`BH3Base.dll` 里有 32 处 `AntiCheatExpert` 引用，ACE 大概率会检测模块列表 / 注入。要做的话第一步不是写 hook，而是**先只放一个空的 VersionShim + 一个只写日志的 DLL，看游戏能不能正常进**。这一步不成功，后面全都不用谈。

---

## 6. 不能搬的

| 项 | 原因 |
|---|---|
| `hk4echeckbypass.dll` / `rsapatch.dll` | 原神专用，hook 的是 `hk4e` 的符号，BH3 里不存在 |
| `mhypbase.dll` 空壳 | BH3 没有这个模块（它用 ACE，`BH3Base.dll` 里集成） |
| 无 TLS 的 HTTP 代理 | BH3 走 HTTPS，没有客户端补丁就必须中间人 |
| Grasscutter 那套配置 | 协议层完全不同（KCP vs 自有 TCP/HTTPS） |

---

## 7. 建议

按"投入 / 收益"排序：

1. **（半小时，零风险）** 给 `launch_all.py` 加 `--watch` 守护 + 退出自动收尾。这是我们链上唯一明显缺的功能，直接抄他的 `Monitor`。
2. **（一小时，零风险）** 3Dmigoto 进客户端目录前做备份，退出复原，避免"游戏目录被污染"这种不可追溯状态。
3. **（半天，有风险但收益最大）** 做 `version.dll` 注入的最小验证：空 shim + 日志 DLL，确认 ACE 不拦。成功的话，`Blocks.xmf` 校验和 TLS 证书两条死路同时出现转机。

前两件要不要现在就做？第三件建议单独开一轮，因为一旦确认可行，路线图会变。
