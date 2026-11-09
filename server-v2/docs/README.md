# 崩坏3 7.0 服务端 v2（.NET 8 + SQLite）

从零重写的版本。**旧版 `D:\BH3\7.0\server`（.NET 6 + MongoDB）保持原样、只读未动。**

```
v1  server\        .NET 6  + MongoDB 4.2 standalone   ← 原样保留
v2  server-v2\     .NET 8  + SQLite 3                 ← 本目录
```

---

## 1. 一句话结论

协议层一个字节都没改（客户端锁死），持久层从 MongoDB 换成 SQLite，
于是 **运行时不再需要 mongod 这个进程**，存档从「304 MB 目录 + 3 个预分配日志」
变成「一个几 MB 的 .sqlite 文件」。

---

## 2. 目录

```
server-v2\
├── build.bat              编译（默认 publish 自包含 → run\）
├── Common\                协议类型 + 持久层（net8.0 类库）
│   ├── Common\            Global / IConfig
│   ├── Common.Database\   ★ 重写：SQLite 版持久层（22 个类，API 与 v1 完全一致）
│   ├── Common.Resources.Proto\   3560 个 message / 11099 个字段（原样）
│   └── Common.Utils.ExcelReader\ 读 Resources\ExcelOutputAsset 的配置表
├── Server\                服务端本体（net8.0 可执行）
│   ├── Program.cs         入口
│   ├── GameServer\        Packet / Session / TcpServer / PacketFactory
│   ├── Handlers\          132 个 Handler（从 v1 原样移植，一行未改）
│   ├── Commands\          控制台命令（give / avatar / level / abyss）
│   ├── Game\              Player（会话级存档聚合）
│   └── Http\              HTTP 443/8081（Kestrel + 29 个路由）
├── run\                   运行目录
│   ├── Server.exe         自包含，不需要装 .NET
│   ├── config.json
│   ├── Resources\         ExcelOutputAsset 1658 个 json（399 MB，必需）
│   └── data\pemukul.sqlite   ← 存档（唯一文件）
└── docs\                  本目录
```

---

## 3. 三层结构，以及每层改了什么

| 层 | v1 | v2 | 为什么 |
|---|---|---|---|
| **协议 / 传输** | 裸 TCP + `01 23 45 67` … `89 AB CD EF` 分帧，KCP/UDP 16100 | **完全一致** | 客户端锁死。改一个字节就断连 |
| **业务（132 Handler）** | 反射扫 `[PacketCmdId]` 注册 | **完全一致** | 它们只调 `Common.Database.*` 的静态方法，签名没变 |
| **持久化** | MongoDB 4.2 standalone | **SQLite 3** | 见 §4 |

关键设计：**持久层保留了 v1 的公开 API**，所以 132 个 Handler 一行都不用改：

```csharp
// v1 和 v2 里都是这么写的
UserScheme user = User.FromUid(uid);
AvatarScheme[] list = Avatar.AvatarsFromUid(uid);
EquipmentScheme eq = Equipment.FromUid(uid);
user.Save();
```

变的只有 `Common.Database` 里的实现。

---

## 4. SQLite 这边怎么设计的

### 4.1 表结构：id + 业务键列 + JSON payload

```sql
CREATE TABLE Users (
    id       INTEGER PRIMARY KEY AUTOINCREMENT,
    payload  TEXT NOT NULL,   -- 整个 UserScheme 的 JSON
    uid      INTEGER,
    name     TEXT,
    token    TEXT
);
CREATE UNIQUE INDEX ux_Users_uid   ON Users(uid);
CREATE UNIQUE INDEX ux_Users_name  ON Users(name);
CREATE UNIQUE INDEX ux_Users_token ON Users(token);
```

12 张表：`Users` / `Avatars` / `Equipments` / `OpenWorlds` / `Elfs` / `Dorms` /
`ClientDatas` / `Logins` / `PrivateMessages` / `DLCs` / `GrandKeys` / `AutoIncrements`。

### 4.2 为什么 payload 用 JSON 而不是拆成列

`UserScheme` 有 20 多个字段，`AvatarScheme` 继承自 proto 的 `Avatar`，
`EquipmentScheme` 里还挂着 `Weapon[]` / `Stigmata[]` / `Mecha[]`。
整个协议有 **11,099 个字段** —— 拆成列既不可能，也没意义。

用 JSON 的实际好处：

- 拿任意 SQLite 工具（DB Browser / `sqlite3`）打开就能读玩家数据，不用写代码；
- 绕开「派生类 + protobuf 继承」的坑（`AvatarScheme : Proto.Avatar` 直接用 protobuf 序列化会报 type not expected）；
- 业务键列（uid/name/token）单独抽出来做**唯一约束**，这正是 Mongo 需要手建索引才有的东西。

### 4.3 过滤为什么在内存里做

`SqlTable<T>.All()` 一次把整表读出来，然后走 LINQ —— 和 v1 的 `collection.AsQueryable()` 语义完全相同。

理由很实际：这份存档一共 **415 条文档 / 573 KB**。
内存过滤比拼 SQL 快，而且不会因为翻译查询而改变行为。
真要上规模时，`SqlTable<T>` 换掉 `All()` 的实现即可，业务代码不用动。

### 4.4 换来了什么

| | v1（MongoDB） | v2（SQLite） |
|---|---|---|
| 存档形态 | 304 MB 目录（301 MB 是预分配日志） | 一个 `pemukul.sqlite`，几百 KB |
| 进程 | 多一个 `mongod.exe` | **不需要** |
| 多文档事务 | ❌ standalone 不支持 | ✅ `InTransaction()` |
| 唯一约束 | 手建索引 | `UNIQUE INDEX` |
| 备份 | `mongodump` 全量 | 拷一个文件 |
| 自增序列 | `findOneAndUpdate` + upsert | 事务内读改写（不会重号） |

---

## 5. 编译与运行

```bat
cd /d D:\BH3\7.0\server-v2

build.bat            :: 发布自包含版本 → run\  （默认，机器上没装 .NET 8 也能跑）
build.bat quick      :: 只做编译检查（产物在 Server\bin，不动 run\）
build.bat clean      :: 清 bin/obj（run\ 和存档不动）
```

运行：

```bat
cd /d D:\BH3\7.0\server-v2\run
Server.exe
```

或者用 `D:\BH3\7.0\启动服务端v2.bat`（会连带拉起 mitmdump + nginx 并设系统代理）。

> **为什么要自包含发布**：这台机器只有 x64 的 ASP.NET Core 6.0 和 9.0，
> 没有 8.0。自包含把 .NET 8 运行时打进 `run\`（约 108 MB），双击即用、不依赖系统安装。

`run\config.json` 里和「客户端能不能连上」直接相关的四项：

```jsonc
{
  "Gameserver": { "Host": "192.168.0.103", "Port": 16100 },  // 下发给客户端的 IP / 游戏端口
  "Http": {
    "HttpPort":   8081,   // dispatch_url / account_url 走这个（客户端只认明文）
    "HttpsPort":  443,    // query_dispatch 入口 + ex_audio 的 https 请求
    "StaticPort": 80      // nginx；443 上的 /com.miHoYo.bh3oversea 由 StaticProxy 反代到这里
  }
}
```

> 换机器/换网段只改 `Gameserver.Host` 一处即可 —— 所有下发地址都是从它拼出来的。


---

## 6. 实测结果（2026-10-21）

一键启动 → 自检 → 停止，全链路实测：

```bat
D:\BH3\7.0\启动服务端v2.bat
D:\BH3\7.0\tools\python\python.exe D:\BH3\7.0\_launch\verify_v2.py
D:\BH3\7.0\停止服务端v2.bat
```

`verify_v2.py` **39 项全过**：

```
[1] 443 有监听                                    PID 283548
    exe == server-v2\run\Server.exe            D:\BH3\7.0\server-v2\run\Server.exe
    不是 v1 的 PemukulPaku.exe
[2] 27017 未监听（v2 不需要 mongod）
[3] 16100 可连
[4] 8081 /query_dispatch 200        retcode=0
    dispatch_url 带 HTTP 端口        http://192.168.0.103:8081/query_gateway
    443  /query_dispatch 200 (TLS)  retcode=0
    ex_audio 不带 scheme            192.168.0.103/com.miHoYo.bh3oversea
    443 音频清单 200 / 不是 HTML / 9234 B
    443 BlockMeta 200 / 14121 B
[5] Boot 行 'Starting server v2'
    Handler 注册数 == 132（去重）
    本轮日志无异常
[6] 存档文件存在 852 KB / journal_mode == wal
    已建表都在 12 张声明清单内
[7] query_gateway.gameserver      {'ip': '192.168.0.103', 'port': 16100}
[8] nginx 大包 200，content-length 4,728,354 与磁盘一致
[9] mdk login 200 / retcode=0 / 下发 uid+token / Users 表有行
[10] 装备物品 ID：无残留 _id / 3291 件都有 Id / uid=1002 武器 1268 圣痕 1290
[11] 探活之后服务端是否还活着       同一 PID，没有崩
```

`test_client_flow.py` **20 项全过**（完全模拟客户端，经 mitmdump:8891）：

```
query_dispatch 200 / retcode 0 / dispatch_url 带端口
query_gateway  200 / gameserver ip+port / account_url 带端口
asset_bundle   指向本地 /asset_bundle/overseas01/1.1（不是 CDN）
               data|event|ai/build.status 全 200
ex_audio       不带 scheme / 指向本机
               https://<ex>/Audio/Windows/7_0/0/                      200   9,234 B  非 HTML
               https://<ex>/StreamingAsb/7_0_0/pc/HD/asb/BlockMeta_7_0.xmf
                                                                      200  14,121 B  非 HTML
反面验证        80 端口 /query_gateway 404（符合预期）
```

补充实测：

```
Handler 注册数 == 132（去重）       132 个
TCP 16100 / HTTP 8081 / 443        全部监听
日志无异常                          无 Exception / 无 Unhandled
登录建档                            uid=1001, token 正常下发
二次登录                            uid 仍是 1001（幂等，不重复建档）
token verify                        retcode=0
不依赖 mongod                       27017 全程未启动
探活 16100 后进程存活                连续跑两轮 verify，PID 不变
停止后                              四端口全释放，ProxyEnable=0
```


> **表是懒建的**：`Global.Table<T>()` 第一次被调用时才 `CREATE TABLE`，
> 所以刚登录完只会有 `Users` / `AutoIncrements` 两张，其余 10 张等对应玩法第一次用到才出现。
> 自检脚本因此断言「已建表 ⊆ 12 张声明清单」而不是「== 12 张」。

---

## 7. 还没做的（重要）

### 7.1 存档已经导过来了（2026-10-21 完成）

v1 的 `runtime\mongo\data\jiale_data_013`（9 集合 / 416 文档）已经全部导进
`run\data\pemukul.sqlite`，**4 个账号可直接登录**（`test` = uid 1001）。

| 集合 | 行数 | 集合 | 行数 |
|---|---|---|---|
| Users | 4 | Logins | 164 |
| Avatars | 173 | ClientDatas | 7 |
| OpenWorlds | 57 | Equipments | 4 |
| Elfs / Dorms | 2 / 2 | AutoIncrements | 3 |
| PrivateMessages / DLCs / GrandKeys | 0 | **合计** | **416** |

体积对照：同样这批数据，Mongo 目录 **304 MB**（其中 301 MB 是 3×100 MB 预分配 journal），
SQLite 单文件 **872 KB**。

**做法**（`_launch\migrate_v1_to_v2.py`，三段可单独跑）：

```bat
:: 1. 复制 dbpath 到 _ext\tmp（原目录一个字节不动，mongod 一启动就会写 journal）
python migrate_v1_to_v2.py export     :: 临时 mongod(:27018) -> 12 个 .jsonl
python migrate_v1_to_v2.py check      :: Mongo 文档键 vs C# 属性名，报告差异
python migrate_v1_to_v2.py apply      :: 按 v2 的 DDL 建表 + 写行（幂等，先 DELETE 整表）
```

三个关键点：

1. **`CreatedAt` 从 ObjectId 里取**。Mongo 的 `_id` 前 4 字节是大端时间戳，
   v1 的 `Login.LocalDate` 就是拿它算「今天」，所以导入时 `CreatedAt = int(oid[:8], 16)`，
   行为与 v1 逐秒一致。
2. **payload 就是文档本身去掉 `_id`**。Mongo 的键名和 v2 的属性名完全一样
   （`check` 结果：**0 处「Mongo 有、C# 没有」**），所以不需要任何字段映射表。
3. **`AutoIncrements` 必须一起导**。里面存着 `UID=1004 / Weapon=1450 / Stigmata=2027`，
   不导的话新账号会从 100 开始撞号。

已知差异（导入后为默认值，不影响启动）：

- `Dorms` 的 `BeLikedNum / BeVisitNum / HasLikesDrop / YesterdayBeLikedNum / RecordLists` —— v1 存档里没有这些键（旧版本写的档），取 0 / 空列表。
- `Avatars` 的 `Mode / StageGoodfeel` —— 同理。
- `PrivateMessages / DLCs / GrandKeys` 三个集合本来就是空的。

### 7.2 玩法路径没有逐个验证

132 个 Handler 全部编译通过、全部注册成功、HTTP 登录链路已实测，存档也确认读得到
（用 `test` 登录拿到 uid=1001，`Users` 仍是 4 行，没有重复建档）。
但「进游戏 → 打一关 → 抽卡 → 改装备」这种完整路径需要真客户端跑一遍才算数 ——
这部分和 v1 的行为差异无法靠服务端单测发现。

**深渊这块单独查过了**，见 [`深渊玩法解析.md`](深渊玩法解析.md)。要点：

- 客户端**仍在发** `GetOpenworldEndlessDataReq`（3419，实测 14 次），之前服务端没接。
  已补 `Server\Handlers\GetOpenworldEndlessDataReqHandler.cs`，按请求的 `type` 回显。
- `GetEndlessStatusRsp` 原来 `SelectedEndlessType=5`（贪婪）和 `CurStatus=Ultra` 自相矛盾，
  导致客户端反复去领贪婪深渊的排行奖励（12 次全落空）。已统一到 `Server\GameServer\AbyssConfig.cs`。
- 游戏内聊天框可热切换：`abyss legacy 2`（迪拉克之海）/ `3`（量子奇点）/ `4`（超弦）/ `5`（贪婪）。
- 超弦空间的 4 个 handler 仍是假数据：`MaxScore=0`、`IsUpFloor=false`（**永不晋级**）、
  `ScheduleId=1028` 写死。要真能打，得先接 `UltraEndlessSchedule` 表。
- 探针脚本：`_launch\abyss_probe.py`（看客户端发了哪些深渊协议、服务端缺哪些）。

### 7.3 端口与 v1 冲突

两版都用 443 / 8081 / 16100，**不能同时启动**。
`启动服务端v2.bat` 会检查 443 上进程的 exe 路径，发现是 v1 的 `PemukulPaku.exe` 就拒绝启动。

### 7.4 上游开源版 vs 7.0 原版 DLL：四处行为差异（都已修）

`Server\Http\Controllers\` 是从上游开源仓库复制的，而 7.0 的 HTTP 层实际跑的是原版
`HttpServer.dll`。两者对「客户端该连哪儿」的回答有几处不一样，**都会让游戏直接报错**。
判断依据只有一份：客户端真实请求的日志（`_launch\logs\` 下的 mitm 日志 + nginx access.log
+ `%LocalAppData%Low\miHoYo\Honkai Impact 3\output_log.txt`）。

一句话概括这四处的共同点：**客户端只会照着 dispatch 下发的字符串拼 URL，
不判断里面有没有 scheme、有没有端口**。所以下发的每一个字段都必须和原版 DLL 逐字节对齐。

#### 7.4.1 `dispatch_url` 漏了端口 → 游戏「连接失败」

**症状**：服务端日志只有 `200 GET /query_dispatch`，之后什么都没有，客户端报连接失败。

**根因**：上游版写的是

```csharp
DispatchUrl = $"http://{Host}/query_gateway"      // 默认 80 端口
```

而 7.0 原版返回的是 `http://192.168.0.103:8081/query_gateway`
（`70_mitmdump.log` 里客户端实际请求的 URL 可以直接看到）。

80 端口是 nginx，只服务 `asset_bundle` 静态文件，**没有 `/query_gateway` 反代** → 404 →
客户端拿不到 `gameserver` → 报「连接失败」。

**修复**：`DispatchController` 里统一走 `HttpBase`（`http://{Host}:{HttpPort}`），
`dispatch_url` / `account_url` / `oaserver_url` / `cdkey_url` 全部带 8081。

#### 7.4.2 `asset_bundle_url_list` 指向 CDN → 「下载数值文件失败 502」

**症状**：过了 `query_gateway` 之后，客户端报「下载数值文件失败 502」。

**根因**：上游版在 `UseLocalCache=false` 时返回

```
https://hk-bundle-os-mihayo.akamaized.net/asset_bundle/overseas01/1.1
```

客户端就去连这个 CDN，而本机 DNS 解析不了（`v2_mitmdump.log` 里是
`error establishing server connection: [Errno 11001] getaddrinfo failed`），mitm 只能回 502。

注意 `proxy.py` **只重写 `hk-bigfile-os-mihayo`（音视频），不重写 `hk-bundle-os-mihayo`（资源包）** ——
所以音视频能走通（被转到 `3rd.ilulu.ink`），资源包走不通。

而 7.0 原版返回的是 `http://192.168.0.103/asset_bundle/overseas01/1.1`，
客户端直接打 nginx:80 —— `runtime\web\nginx\logs\access.log` 里 11:44 那几条
`GET /asset_bundle/overseas01/1.1/data/build.status 200` 就是证据，
`70_mitmdump.log` 里 `bundle-os-mihayo` 出现 **0 次**。

**修复**：`GetAssetBundleUrlList` **恒返回本地地址**（`WebBase` + `/asset_bundle`），不再看 `UseLocalCache`：

| version 里的段 | 返回 |
|---|---|
| `_os_`（本机 7.0 客户端） | `http://{Host}/asset_bundle/overseas01/1.1` |
| `_gf_` | `.../android01/1.0`（beta 版 `.../beta_release/1.0`） |
| `_global_` | `.../usa01/1.1` |

本地 nginx 上有完整 2616 个包（1.3 G），没有任何理由再走外网。
`UseLocalCache` 现在**只影响音视频/额外资源**（`ex_audio_and_video_url_list` / `ex_resource_url_list`），
保持 `false` 以复现 v1 的行为（经 `proxy.py` 转到 `3rd.ilulu.ink`）。

#### 7.4.3 音视频清单 404 → 「下载活动资源失败」

**症状**：数值文件过了，客户端弹「下载活动资源失败」并退出。
客户端日志 `%LocalAppData%Low\miHoYo\Honkai Impact 3\output_log.txt` 里是：

```
[WwiseAudioManager.CheckPackageManifestCoroutine] Download
  https://hk-bigfile-os-mihayo.akamaized.net/com.miHoYo.bh3oversea/Audio/Windows/7_0/0/?t=...
  error Generic/unknown HTTP error.HttpRspCode:404      ← 重试 3 次后弹窗
```

**根因**：`ex_audio_and_video_url_list` 指向 `hk-bigfile-os-mihayo.akamaized.net`
（`proxy.py` 会把它重写到第三方镜像 `3rd.ilulu.ink`）。镜像上 `StreamingAsb` 清单还在，
但 `Audio/Windows/7_0/0/` 目录已被删（404），于是 `CheckStreamingAssetBundle` 整体失败。

**当时的修复**（现在看只对了一半，最终方案见 §7.4.6）：

1. 把本地资源目录**联接**进 nginx 根（12 G 音频 / 17 G asb 不复制）：

```powershell
$base = 'D:\BH3\7.0\runtime\web\com.miHoYo.bh3oversea'
New-Item -ItemType Junction -Path "$base\Audio\Windows\7_0\0" `
  -Target 'D:\BH3\7.0\BH3_Data\StreamingAssets\Audio\GeneratedSoundBanks\Windows'
New-Item -ItemType Junction -Path "$base\StreamingAsb\7_0_0\pc" `
  -Target 'D:\BH3\7.0\BH3_Data\StreamingAssets\Asb\pc'
```

2. nginx 对这个前缀开 `autoindex`。

3. `GetExAudioAndVideoUrlList` 恒返回本地地址。

> ⚠️ **这一步的结论是错的**：当时以为「9.0k / 13.8k 对得上 autoindex 页大小」，
> 其实是**巧合** —— 那两个数字是**二进制清单文件**的大小。见 §7.4.6。

> 目录联接本身没白做：`Audio/Windows/7_0/0/` 下的 `.pck`、`Asb/pc/` 下的 `.wmv`
> 仍然靠它（或 §7.4.6 里的 `alias`）提供。

#### 7.4.4 同一个报错，但这次是「协议被拼了两遍」→ 又「下载活动资源失败」

上一版改完后，客户端仍然报「下载活动资源失败」，日志变成了：

```
[WwiseAudioManager.CheckPackageManifestCoroutine] Download
  https://http://192.168.0.103/com.miHoYo.bh3oversea/Audio/Windows/7_0/0/?t=639281856016812463
  error Generic/unknown HTTP error.HttpRspCode:502
```

注意 **`https://http://`**。而同一时刻 nginx 的 access.log 里
`GET /com.miHoYo.bh3oversea/Audio/Windows/7_0/0/ 200 9740` —— 本地目录索引本身是好的。

**根因**：`MoleMole.WwiseAudioManager.CheckPackageManifestCoroutine` 拿到
`ex_audio_and_video_url_list` 的条目后，是**无条件 `"https://" + 条目`** 拼接的
（不判断条目里有没有 scheme）。我们返回 `http://192.168.0.103/...`，
就被拼成 `https://http://192.168.0.103/...`，主机名成了 `http`，DNS 失败 → 502。

对照原版 v1 的值就明白了：`hk-bigfile-os-mihayo.akamaized.net/com.miHoYo.bh3oversea`
—— **纯 host+path，不带 scheme**。

**修复**（两处配合）：

1. `GetExAudioAndVideoUrlList` 只返回 host+path：

```csharp
string local = Global.config.Gameserver.Host + "/com.miHoYo.bh3oversea";
```

2. 因为客户端强制补 `https://`，请求就落在 **443**（Kestrel），而不是 nginx 的 80。
   于是在 HTTP 管道里加了一层 `StaticProxy`，把 `/com.miHoYo.bh3oversea` 前缀
   反向代理到 `http://127.0.0.1:{Http.StaticPort}`（默认 80）：

```csharp
app.UseStaticProxy();   // Server/Http/StaticProxy.cs
```

> **为什么是反代而不是在 C# 里自己列目录**：客户端靠**比对索引页里的文件名与本地文件**
> 判断要不要下载。C# 生成的 HTML 一旦和 nginx autoindex 有格式差异，就会被误判成
> 「全部缺失」，进而触发几十 G 的下载。反代让 nginx 继续做唯一的静态资源来源，
> 顺带保证 `runtime\web` 只有一份、不会两套配置各自漂移。

顺手把 `GetExResourceUrlList` 也改成恒本地（原来 `UseLocalCache=false` 时会返回
`hk-bundle-west-mihayo` / `bundle.bh3.com` 这类外网域名，本机 DNS 解析不了 → 又一个 502）。
本地目录不存在时只会拿到 404，客户端会跳过，不会卡住启动链。

**踩坑记录：反代千万别透传 `Transfer-Encoding`**。
`HttpClient` 已经把 chunked 解开了（body 是原始 HTML），但 .NET 仍把
`Transfer-Encoding: chunked` 留在 `resp.Headers` 里；一旦写给 Kestrel，
Kestrel 就认为「分帧由应用负责」，直接输出未分块的 HTML —— 头和体对不上，
按 chunked 解析的一方会把 `<html>` 当成块长度。`Connection` 同理，一并丢掉。

#### 7.4.5 附带修掉一个会「杀进程」的崩溃

验证时发现：只要有人对 **16100 连上就断**（探活脚本、客户端握手前断开、重连都算），
整个 `Server.exe` 会凭空消失。日志最后一行是：

```
Unhandled exception. System.NullReferenceException
   at PemukulPaku.GameServer.Session.DisconnectProtocol() ... Session.cs:line 115
   at PemukulPaku.GameServer.Session.KeepAliveLoop() ... Session.cs:line 108
```

**根因**：`Player` 只有登录成功后才赋值。没登录的 Session 断开时，
`DisconnectProtocol()` 直接调 `Player.SaveAll()` → 空引用；而抛异常的是**后台线程**，
.NET 会终止整个进程。表现极具迷惑性：verify 前半段全绿，后半段全是 ConnectionRefused。

**修复**（`Server/GameServer/Session.cs` + `Server.cs`）：

- `Player != null` 才 `SaveAll()`，整个方法再包一层 `try/catch`；
- 加 `_disconnected` 标志，`ClientLoop` / `KeepAliveLoop` 两条路径只会执行一次；
- 用 `TryRemove(new KeyValuePair<string, Session>(Id, this))` **只摘掉自己** ——
  同一 `ip:port` 重连时新 Session 会覆盖旧条目，旧 Session 晚到的 Disconnect
  不能把新连接踢下线；
- `Sessions` 从 `Dictionary` 换成 `ConcurrentDictionary`（原来接受循环在写、
  断开在删，跨线程操作普通 Dictionary 有概率死循环），
  接受新连接从 `Add` 改成索引器赋值（同 ip:port 重连时 `Add` 会抛重复键）。

#### 7.4.6 真正的根因：那两个 URL 要的是**二进制清单**，不是目录索引

协议前缀修好后（§7.4.4），URL 终于对了，但客户端换了个报错：

```
[WwiseAudioManager.CheckPackageManifestCoroutine] parse
  https://192.168.0.103/com.miHoYo.bh3oversea/Audio/Windows/7_0/0/?t=639281864887596249
  error : System.ArgumentException: length
  at System.Array.Copy(...)
  at BytesTools.BAPAOFDLLBH.ILLPFLGPODG()
  at MoleMole.WwiseAudioManager.ParseManifest (System.Byte[])
```

```
[StreamingAssetBundleManager.CheckBlockMetaCoroutine] Download
  https://192.168.0.103/com.miHoYo.bh3oversea/StreamingAsb/7_0_0/pc/HD/asb/BlockMeta_7_0.xmf
  error! HttpRspCode:404
```

**根因**：`WwiseAudioManager.ParseManifest(byte[])` 是**二进制解析器**。
它把收到的字节当结构化数据读长度字段，而我们回的是 nginx 的 autoindex HTML ——
于是 `Array.Copy` 拿到了荒谬的 length → `ArgumentException: length`。

§7.4.3 里「9.0k / 13.8k 说明是目录索引页」是**误判**：那两个数字确实是 9.0 KiB / 13.8 KiB，
但内容是**二进制清单文件**。

**怎么找到正确文件的** —— 拿 v1 成功那次的 `70_mitmdump.log` 对大小：

| 客户端请求 | v1 镜像返回 | 本地文件 | 大小 |
|---|---|---|---|
| `Audio/Windows/7_0/0/` | `200 OK 9.0k` | `BH3_Data\StreamingAssets\Audio\GeneratedSoundBanks\Windows\manifest.m` | **9,234 B = 9.0 KiB** |
| `StreamingAsb/7_0_0/pc/HD/asb/BlockMeta_7_0.xmf` | `200 OK 13.8k` | `BH3_Data\StreamingAssets\Asb\pc\BlockMeta.xmf` | **14,121 B = 13.8 KiB** |

决定性旁证：`manifest.m` 的修改时间是 **11:44** —— 正是 v1 成功那次客户端把它落盘的时刻。

**修复**（`conf\vhosts\127.0.0.1_80.conf`，不再用 autoindex）：

```nginx
# 音频包：目录 URL 本身返回 manifest.m；同前缀下的 .pck 原样提供
location = /com.miHoYo.bh3oversea/Audio/Windows/7_0/0/ {
    rewrite ^ /com.miHoYo.bh3oversea/__audio_manifest last;
}
location = /com.miHoYo.bh3oversea/__audio_manifest {
    alias "D:/BH3/7.0/BH3_Data/StreamingAssets/Audio/GeneratedSoundBanks/Windows/manifest.m";
    default_type application/octet-stream;
}
location /com.miHoYo.bh3oversea/Audio/Windows/7_0/0/ {
    alias "D:/BH3/7.0/BH3_Data/StreamingAssets/Audio/GeneratedSoundBanks/Windows/";
}

# 流式资源：远端多一层 HD/asb/，本地平铺在 Asb\pc\ 下；且本地文件没有 _7_0 后缀
location = /com.miHoYo.bh3oversea/StreamingAsb/7_0_0/pc/HD/asb/BlockMeta_7_0.xmf {
    alias "D:/BH3/7.0/BH3_Data/StreamingAssets/Asb/pc/BlockMeta.xmf";
    default_type application/octet-stream;
}
location /com.miHoYo.bh3oversea/StreamingAsb/7_0_0/pc/HD/asb/ {
    alias "D:/BH3/7.0/BH3_Data/StreamingAssets/Asb/pc/";
}

# 兜底：autoindex 必须 off —— 宁可 403/404，也不能再回 HTML 让客户端解析崩
location /com.miHoYo.bh3oversea/ {
    root "D:/BH3/7.0/runtime/web";
    autoindex off;
}
```

**踩坑：`location = .../0/ { alias .../manifest.m; }` 会 500**。
URI 以 `/` 结尾，nginx 的 `index` 模块会去拼 `<alias 目标>index.html`，报
`".../manifest.mindex.html" is not a directory`。所以先用 `rewrite ... last`
重写到一个**不带斜杠**的内部 URI，再 alias 到文件。

> 教训：**客户端「要什么」不能靠大小猜，要看它拿字节去干什么**。
> 这次是 `ParseManifest(byte[])` 直接决定了一切 —— 只要是 HTML 就必崩。

#### 定位方法与回归防护

```bat
:: 1. 服务端日志里客户端打到哪一步了
findstr "<HTTP>" D:\BH3\7.0\_launch\logs\v2_Server.log
:: 2. 代理日志里客户端到底请求了哪个 URL（含端口/域名），以及连到了哪
findstr /C:":80" /C:"getaddrinfo" D:\BH3\7.0\_launch\logs\v2_mitmdump.log
:: 3. nginx 日志：本地资源到底有没有被请求过
findstr "asset_bundle" D:\BH3\7.0\runtime\web\nginx\logs\access.log
:: 4. 和 v1 成功那次的日志对比
findstr "query_gateway" D:\BH3\7.0\_launch\logs\70_mitmdump.log
```

- `_launch\verify_v2.py`：**39 项**。除了进程/端口/日志/SQLite/登录链路，还有这几条专治上面这些坑：
  - `ex_audio 不带 scheme`（§7.4.4，客户端会自己补 `https://`）
  - `443 音频清单 200 / 不是 HTML / 大小 == 9234`（§7.4.6）
  - `443 BlockMeta 200 / 大小 == 14121`（§7.4.6）
  - `没有残留的 _id 键` / `每件装备/材料都有 Id`（§7.5）
  - `探活之后服务端是否还活着`（§7.4.5，PID 必须和开头是同一个，否则就是又崩了）
- `_launch\test_client_flow.py`：**20 项**。完全模拟客户端
  （代理 CONNECT + SNI → `query_dispatch` → `dispatch_url` → `asset_bundle` 的 `build.status`
  → `https://<ex_audio>/...` 的两个**二进制清单** → 反面验证 80 端口 `/query_gateway` 404）

两个脚本都跑一遍、**全绿才算通**：

```bat
tools\python\python.exe _launch\verify_v2.py
tools\python\python.exe _launch\test_client_flow.py
```

> 注意：`test_client_flow.py` 的 `https_via_proxy()` 里 `read_all()` 会一直读到 8 秒超时
> （mitm 不会主动关隧道），所以整脚本慢是正常的，不是卡住。
> 另外它必须在 `Server.exe` 已经监听 443 **之后**跑，否则 mitm 会回
> `error establishing server connection: [WinError 1225]`。

### 7.5 能登录了，但武器 / 圣痕 / 材料显示不正常（物品 ID 全是 0）

**症状**：能进游戏、能进背包，但武器、圣痕、材料、宿舍家具全是空白 / 问号 / 显示不出来。

**根因**：MongoDB 的 C# 驱动有一条**默认约定** —— 属性名 `Id` 自动映射到元素名 `_id`。
所以 v1 时代存档里，「物品 ID」这个字段的 JSON 键叫 `_id`：

```json
{"UniqueId": 101, "_id": 20001, "Level": 1, "Exp": 0, ...}
```

v2 换成 Newtonsoft + JSON 之后**没有这层约定**，`_id` 被当成未知字段直接丢掉，
于是 `Weapon.Id` / `Stigmata.Id` / `Material.Id` / `DormHouse.Id` 全部变成 **0**。
客户端拿不到物品 ID，自然一个都渲染不出来。

**为什么之前没发现**：`migrate_v1_to_v2.py check` 只比对**顶层**键，而且会
`keys.discard("_id")` —— 嵌套的 `_id` 根本不在它的视野里，所以它报告「0 处不匹配」。

**影响范围**（扫一遍 SQLite 就知道，只有这两张表有嵌套 `_id`）：

| 表 | 嵌套 `_id` 数量 | 对应类型 |
|---|---|---|
| `Equipments` | 3,291 | `Weapon` / `Stigmata` / `Material` / `Mecha` |
| `Dorms` | 749 | `DormHouse`（家具） |

**修复**（两处，一劳永逸）：

1. `_launch\migrate_v1_to_v2.py` 的 `to_payload()` 里加 `_rename_mongo_id()`，
   递归把嵌套 `_id` 改名成 `Id` —— **翻译责任放在迁移这一层**，这是唯一从 Mongo 进来的口子：

```python
def _rename_mongo_id(node):
    if isinstance(node, list):
        for item in node:
            _rename_mongo_id(item)
    elif isinstance(node, dict):
        if "_id" in node and "Id" not in node:
            node["Id"] = node.pop("_id")
        for value in node.values():
            _rename_mongo_id(value)
```

   同时 `check` 里加了「嵌套 `_id` 计数」，以后再出现这类问题会直接打出来。

2. `Common\Common.Database\Sql.cs` 的 `SqlTable<T>.Deserialize()` 兜一层：
   只要 payload 里还有 `"_id"`，读的时候就认成 `Id`。
   **只对 `Equipments` / `Dorms` 两张表生效** —— 它们的 payload 全是结构化数据、
   不含玩家自由文本，做字符串替换没有副作用（`PrivateMessages` 就不行）。

**已修复的数据**（不用重启 mongod，`_ext\tmp\mongo_import\jsonl` 里的导出还在）：

```bat
tools\python\python.exe _launch\migrate_v1_to_v2.py check
tools\python\python.exe _launch\migrate_v1_to_v2.py apply --only=Equipments,Dorms
```

`--only` 是新加的：只重导这两张表，`Users` / `Avatars` / `Logins` 等原样保留，
不会把玩家在 v2 里的进度冲掉。

> 动手前先备份。注意 **WAL 里可能还有没合并的写入**，
> 只复制 `pemukul.sqlite` 会拿到旧快照，要先 checkpoint：
>
> ```python
> con = sqlite3.connect("pemukul.sqlite")
> con.execute("PRAGMA wal_checkpoint(TRUNCATE)")
> ```

**验证**：`verify_v2.py` 第 10 节，三条断言 ——
没有残留 `_id`、3,291 件装备/材料**每一件**都有非 0 的 `Id`、至少一个账号有装备。

```
[10] 装备物品 ID（_id -> Id 翻译）
  [ OK ] 没有残留的 _id 键            0 处
  [ OK ] 每件装备/材料都有 Id          3291 件，缺失 0
  [ OK ] 至少一个账号有装备            uid=1002 武器 1268 / 圣痕 1290
```

> 教训：**换存储引擎时，要把旧引擎的「隐式约定」逐条显式化**。
> Mongo 的 `Id -> _id` 就是一条看不见的约定 —— 代码里没有任何地方写过 `_id`，
> 但只要换了序列化器，它就会静默地把数据吃掉。

---

## 8. 加一个新 Handler

```csharp
// Server\Handlers\MyThingReqHandler.cs
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.MyThingReq)]            // 这一行就是注册，不用改别的地方
internal class MyThingReqHandler : IPacketHandler
{
    public void Handle(Session session, Packet packet)
    {
        MyThingReq req = packet.GetDecodedBody<MyThingReq>();
        session.Send(Packet.FromProto(new MyThingRsp
        {
            retcode = MyThingRsp.Retcode.Succ
        }, CmdId.MyThingRsp));
    }
}
```

`build.bat` → 重启 → 自动生效（`PacketFactory` 反射扫 `[PacketCmdId]`）。

---

## 9. 与 v1 的差异清单（逐条）

| 项 | v1 | v2 |
|---|---|---|
| 目标框架 | netcoreapp6.0 | **net8.0** |
| 依赖管理 | 手工 `<Reference HintPath>` 指向 `lib\*.dll` | **NuGet 包**（protobuf-net / Newtonsoft.Json / Config.Net / Microsoft.Data.Sqlite） |
| MongoDB.Driver | 必需 | **完全移除** |
| `Common.Global.db` | `IMongoDatabase` | **`Global.Store`（SqliteStore）** |
| scheme 的 `Id` | `ObjectId`（Mongo 类型泄进业务层） | `long` + `IRow` 接口 |
| 建档时间 | `ObjectId.CreationTime` | `CreatedAt`（Unix 秒，自己存） |
| 自增 | `findOneAndUpdate` + upsert | 事务内读改写 |
| HTTP | 引 `HttpServer.dll`（反编译产物） | **源码**（`Server\Http\`，29 条路由） |
| 产物 | 框架依赖（需 .NET 6） | 自包含（不需装 .NET） |
| 存档 | `runtime\mongo\data\jiale_data_013\`（304 MB） | `run\data\pemukul.sqlite`（一个文件） |

### HTTP 路由的来源

v2 的 19 条主路由来自上游 PemukulPaku 仓库的源码，
另外 10 条（`/status/server`、`/bh3_os/combo/red_dot/list`、公告、h5log、`/sw.html` 等）
是**从 7.0 那份 `HttpServer.dll` 的 `#US` 堆里提取字符串**后补的 ——
它们在 7.0 存在但上游没有，缺了客户端会在登录前后卡住。
