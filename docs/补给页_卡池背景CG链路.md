# 补给页（卡池）背景 CG 链路

> **结论先说**：补给页中间那块大画面**不是一张图，是一段 CG 视频**。
>
> 服务端只在 `display_ext` 里给一个 key：
>
> ```json
> {"common_gacha_bg":"event/Immediately/GachaPic/7.0_wbg1-1_truecolor2048",
>  "video_cg_key":"7.0_Activity_LOOP09"}
> ```
>
> - `common_gacha_bg`：视频底下的**静态底图**（纯背景，不是主画面）
> - `video_cg_key`：**主画面视频**，客户端拿它当 `CgExtraKey` 去 `CgData` 表查行 → 取 `CgPath` → 播
>   `BH3_Data\StreamingAssets\Video\<CgPath>.usm`
>
> 客户端**只会播本地已有的 `.usm`**。下载兜底那一条路（dispatch 下发的资源域名）在本机
> 完全走不通，见第五节。
>
> 所以「补给页主图不对 / 加载中」这类问题，**唯一有效的调法是换 `video_cg_key`**——
> 改 `run\gacha.json` 存盘即生效，不用重启、不用重编译。

---

## 一、链路总览

```
run\gacha.json
   bg_image / video_cg_key / display_video_avatar / up_avatar_id
            │  （按文件 mtime 热加载，改完存盘即生效）
            ▼
GachaConfig.Load()                      GachaConfig.cs:232
            ▼
GetGachaDisplayReqHandler.BuildOne()    GetGachaDisplayReqHandler.cs:49
            │
            ├─ CommonData.title_image              小图标（SpriteOutput 图集）
            ├─ CommonData.display_ext  ──────────► GachaService.DisplayExt()   :174
            │        common_gacha_bg  +  video_cg_key
            ├─ CommonData.up_avatar_list            只发 UP 那一个
            └─ CustomGachaData.display_video_avatar :83（0 → 自动用 up_avatar_id）
            ▼
┌──────────────────── 客户端 ────────────────────┐
│                                                │
│  GachaBgImageCmpt.SetupImg()                   │
│    读 display_ext["common_gacha_bg"]            │  静态底图
│    取不到 → 占位图                              │
│      SpriteOutput/Gacha/CaChaNew01/GachaBg/    │
│      GachaCurrency                             │
│                                                │
│  GachaCGBGCmpt.TryPlayCg()                     │  主画面视频
│    读 display_ext["video_cg_key"]               │
│    为空 → 回落客户端内置                        │
│      ConfigGacha.GachaVideoDict[gacha_type]     │
│                    .CgExtraKey                  │
│            ▼                                   │
│  key 当 CgExtraKey 查客户端 CgData 表           │
│            ▼  CgPath                           │
│  CGManager.LoadOrPlayVideo()                   │
│            ├─ 本地有 → 直接播                   │
│            │    StreamingAssets/Video/          │
│            │    <CgPath>.usm                    │
│            └─ 本地无 → DownloadCustomVideo()    │
│                 dispatch 域名（本机不可用）     │
└────────────────────────────────────────────────┘
```

---

## 二、服务端：配置字段

文件 `server-v2\run\gacha.json`（`Directory.GetCurrentDirectory()` 下，首次启动自动生成默认值）。

一个卡池里与背景相关的 4 个字段：

| JSON 字段 | 代码位置 | 作用 |
|---|---|---|
| `bg_image` | GachaConfig.cs:80 | 静态底图 → `display_ext.common_gacha_bg`。必须以 `event/Immediately` 或 `SpriteOutput` 开头 |
| `video_cg_key` | GachaConfig.cs:92 | 主画面视频 key → `display_ext.video_cg_key` |
| `display_video_avatar` | GachaConfig.cs:99 | `CustomGachaData.display_video_avatar`，只影响「按哪个角色出」，不决定播哪个文件；0 → 用 `up_avatar_id` |
| `up_avatar_id` | GachaConfig.cs:124 | UP 角色 |

当前配置（第 1 池，`gacha_type=5` 扩充补给，2026-10-28 17:15 实读）：

```json
"bg_image": "event/Immediately/GachaPic/7.0_wbg1-1_truecolor2048",
"video_cg_key": "7.0_Activity_LOOP01",     // ← 要换就换这里
"display_video_avatar": 712,
"up_avatar_id": 712
```

> `7.0_Activity_LOOP01` 这个 slot 已被自制 usm 占用（见第十节），
> 所以现在播的是自制视频，不是原始素材。要换回官方素材，改 key 即可；
> 要还原 LOOP01 本身，把 `.workbuddy\backup\video\` 里的原文件拷回。

**热加载**：`GachaConfig.Load()` 每次取值都比对文件 `LastWriteTimeUtc`
（GachaConfig.cs:232 / :247），变了就重解析，并打一行日志：

```
[Gacha] 重新读取 gacha.json：#1 type=5 on drops=11 sum=...
```

---

## 三、服务端：协议拼装

`GetGachaDisplayReqHandler.BuildOne()`（:49）——

```csharp
GachaDisplayCommonData commonData = new GachaDisplayCommonData
{
    TitleImage = pool.TitleImage,          // 只是标题旁的小图标
    Title      = pool.Title,
    Content    = pool.Content,
    GachaId    = pool.GachaId,
    DisplayExt = GachaService.DisplayExt(pool)   // ← 背景都在这
};
commonData.UpAvatarLists = new[] { pool.UpAvatarId };   // 只发一个
```

`GachaService.DisplayExt()`（GachaService.cs:174）按需拼 JSON，两个 key 都可缺省：

```csharp
parts.Add($"\"common_gacha_bg\":\"{pool.BgImage}\"");
parts.Add($"\"video_cg_key\":\"{pool.VideoCgKey}\"");
return parts.Count == 0 ? "{}" : "{" + string.Join(",", parts) + "}";
```

`display_ext` 是**字符串里的 JSON**，字段名固定，客户端只认它读的那几个 key，多塞无效。

`up_avatar_list` 只能发 UP 那一个——早先塞了 94 个，客户端要为每个角色加载立绘/模型，
一直加载不完，这也是「加载中....」的一半原因（见 GetGachaDisplayReqHandler.cs:65 注释）。

---

## 四、客户端：怎么把 key 变成画面

| 步骤 | 方法 | 地址 |
|---|---|---|
| 取 display_ext 的 key | 静态字段 | `0x189bce680` = `common_gacha_bg`，`0x189bce698` = `video_cg_key` |
| 静态底图 | `GachaBgImageCmpt.SetupImg` | 读 `common_gacha_bg` → 失败回落 `SpriteOutput/Gacha/CaChaNew01/GachaBg/GachaCurrency` |
| 视频区初始化 | `GachaCGBGCmpt.SetupView` | `0x180C57AD0` |
| 尝试播放 | `TryPlayCg` | `0x180C57C90`（要求 videoPlayer.gameObject 激活 且 key 非空） |
| 判定本地/下载 | `CheckCustomVideo` | `0x180C570D0` |
| 播放 | `PlayVideo` | `0x180C577E0` |
| 下载 | `DownloadCustomVideo` | `0x180C57340` |
| CG 管理器 | `MoleMole.ODNDNLLOJBP` | `GHNJPPHCKEM` 0x1814005C0 → `LoadOrPlayVideo` 0x0EFD810 |

- key → `CgData` 行（列：`CgID` / `CgKey` / `CgExtraKey` / `CgPath` / `InStreamingAssets`）
- `CgPath` → `StreamingAssets\Video\<CgPath>.usm`
- `Video\Version.txt` 是客户端自己维护的缓存清单（每行 `Video/<CgPath>.usm`），
  客户端靠它判断「这个 CG 已经下载过」；文件在但清单里没有，一样会被当成缺失。

**key 与文件名/表的关系**：`video_cg_key` 对应 `CgData.CgExtraKey`；
`CgData.CgPath` 通常等于 `<CgExtraKey>_<内容哈希>`，也就是本地文件名去掉 `_<hash>.usm` 的部分。
例：文件 `7.0_Birthday_Seele_e22459d3917c7b78bfbd62526ff64cd6.usm`
→ `CgExtraKey = 7.0_Birthday_Seele`，`CgPath = 7.0_Birthday_Seele_e22459d3917c7b78bfbd62526ff64cd6`。

---

## 五、下载兜底为什么不可用

客户端 base 来自 dispatch 的两个列表，且**强制拼 `https://`**（不看有没有 scheme）：

| 列表 | 服务端实现 | 实际下发 |
|---|---|---|
| `ex_audio_and_video_url_list` | DispatchController.cs:179 | `192.168.0.103/com.miHoYo.bh3oversea`（只给 host+path） |
| `ex_resource_url_list` | DispatchController.cs:193 | 同上（gf 走 `/tmp/Original`，global 走 `/tmp/com.miHoYo.bh3global`） |

拼出来的 `https://192.168.0.103/...` → mitm:8891 → nginx:80。

三处断点：

1. **nginx 没有 Video 映射**：`runtime\web\nginx\conf\vhosts\127.0.0.1_80.conf` 的
   `/com.miHoYo.bh3oversea/` 只是回落到 `runtime/web` 根（autoindex off），
   `Video/` 一律 404。
2. **官方域名解析不了**：`hk-bigfile-os-mihayo.akamaized.net` 本机 `getaddrinfo failed`。
3. **镜像也没了**：`runtime\proxy\proxy.py` 把该域名改写到 `3rd.ilulu.ink`，
   那个站上 Audio 目录已被删、Video 路径全部 404。

结论：**只能播本地已有 `.usm`**，不要指望「服务端换 key → 客户端去下载」。

---

## 六、本地素材盘点（2026-10-28）

`BH3_Data\StreamingAssets\Video`：**1533 个 `.usm` + `Version.txt`**。

- **没有**任何 gacha 专用 CG（官方的 `4.8_Gacha` / `5.6_Gacha` / `5.7_Gacha` 只在
  CgData 表里有行，文件不在本地，也不在 `Version.txt` 里）
- 可用的是 7.0 活动/主线/生日/support/PV 视频
- 缩略图对照表（每行一个视频、抽 3 帧）：`.workbuddy\tmp\cg_sheet.png`
  （分片 `cg_sheet_1..3.png`，重点候选大图 `big4.png`）

### 推荐候选（希儿·彼岸双生 UP 期）

| video_cg_key | 时长 | 大小 | 画面 |
|---|---|---|---|
| `7.0_Activity_LOOP09` | 10 s | 3 MB | 夜景蓝水面，希儿·彼岸双生全身站立，裙摆飘动 |
| `7.0_Activity_LOOP10` | 10 s | 3 MB | 夕阳海面，同一角色同一姿势（暖色调） |
| `7.0_MainLine_LOOP_CG09` | 10 s | 4 MB | 水面上黑色剪影、背后发光窗，彼岸双生氛围 |
| `7.0_MainLine_LOOP_CG02` | 10 s | 4 MB | 蓝雾中的角色剪影，暗色调 |
| `7.0_MainLine_LOOP_CG15` | 12 s | 2 MB | 星空，最安静 |
| `7.0_MainLine_LOOP_CG08` | 10 s | 4 MB | 雷暴废墟，纯氛围无人物 |
| `7.0_Activity_LOOP02` | 10 s | 2 MB | 白发角色抱臂特写，红绿光球 |
| `7.0_Activity_LOOP04` | 10 s | 2 MB | 粉色角色半浸在水中 |
| `7.0_Activity_LOOP11` | 10 s | 3 MB | 粉发角色大特写 |
| `7.0_Activity_LOOP07` / `LOOP08` | 8 s | 2 MB | 金发角色伏案，暖光 |
| `7.0_Activity_LOOP05` / `LOOP06` | 8 s | 2 MB | 紫发小个子捧灯 |
| `7.0_Activity_LOOP12` | 5 s | 2 MB | 银发侧脸特写 |
| `7.0_Activity_LOOP13` | 5 s | 2 MB | 紫发角色立于窗帘舞台前 |
| `7.0_Activity_LOOP14` | 6 s | 2 MB | 手绘线稿风 |
| `7.0_Activity_LOOP01` | 10 s | 4 MB | 围栏里的紫色箱子（无人物） |
| `7.0_Activity_LOOP03` | 10 s | 3 MB | 金发角色骑猪（搞笑向） |
| `7.0_Activity_CG01` | 10 s | 4 MB | 蓝天草原 + 坠落场景（剧情向） |
| `7.0_MainLine_CG01` | 34 s | 22 MB | 剧情 CG |
| `7.0_MainLine_CG03` | 61 s | 28 MB | 剧情 CG（最长） |
| `7.0_BaiYe/ChiLing/XunYu/YueLun/YunMo_support` | 5–6 s | 2–3 MB | 战斗演出，节奏快 |
| `PV_7.0` | 129 s | 38 MB | 7.0 正式 PV，2560×1080，雨夜希儿 |

> `7.0_Birthday_Seele`（21 MB / 101 s）是**生日信**视频：右侧小图 + 满屏信件文字，
> 不合适当卡池背景——这是「播放了但内容不对」的原因。

---

## 七、操作手册

换素材只有一步：

0. （换新 key 前先核对）`python tools\cg_manifest_check.py 7.0_Activity`
   —— 输出里每条都要显示 `清单:有`。只满足「文件在」不够，见第八节第 8 条；
1. 编辑 `server-v2\run\gacha.json`，把第 1 池的 `video_cg_key` 改成上表任意 key；
2. 存盘（服务端按 mtime 热加载，日志出现「重新读取 gacha.json」即可）；
3. 游戏里**退出补给页再进一次**。

回退到静态图：把 `video_cg_key` 置空 `""`，`display_ext` 里就只剩 `common_gacha_bg`。

---

## 八、坑与注意

1. **服务端 `run\Resources\ExcelOutputAsset\CgData.json` 只到 6.6 版**（1718 行，无 `7.0_*`）。
   它**不参与**客户端 CG 查找，客户端用自己 Asb 里的 7.0 表。所以不能用它判断
   「某个 7.0 key 存不存在」——真正的判据是：本地有没有对应 `.usm` + `Version.txt` 有没有记录。
2. `title_image` ≠ 卡池主图。`title_image` 只是标题旁的小图标。
3. `up_avatar_list` 只发 UP 一个，多发会拖死加载。
4. `display_video_avatar` 不决定播哪个文件，只决定「按哪个角色出」。
5. `display_ext` 里的 key 名必须精确（`common_gacha_bg` / `video_cg_key`），客户端按静态字符串取。
6. 客户端下载 CG 时用 `"https://" + base`，所以服务端**只能给 host+path，不能带 scheme**。
7. 4.4.7 客户端的旁证：其明文 `global-metadata.dat` 里同样有
   `GachaVideoDict` / `GachaVideoLink` / `GetGachaVideoCGKey` / `CustomGachaInfo.get_VideoCgKey`
   与 `CGModule`（`CgExtraKey`、`CheckCgExistIgnoreGroupByExtraKey`、`GetVersionFilePath`…），
   说明「补给页播 CG + Version.txt 缓存清单」这套机制从 4.4 就存在，没有版本特例。
   但 4.4.7 安装里**没有任何 gacha 视频**（只有 40 个老 `.mp4` 剧情/动作片段），也**没有** URL 模板明文
   （模板在加密的 `ctable.dat` 里），所以拿不到可复用的素材或路径格式。
8. **「文件在」不等于「能播」**：`Video\` 有 1533 个 `.usm`，`Version.txt` 只有 1483 条，
   差 50 个 —— 这 50 个文件在但清单里没有，客户端仍会当成缺失。
   好在缺的这批全是老素材（`2.6_CG*_mux`、`3.x_Raid_CG*`、`QTE` 教学、`start` 等），
   文档第六节候选表里的 `7.0_*` 全部在清单内（`7.0_Activity_LOOP01~14`、`7.0_MainLine_LOOP_CG01~15`、
   `7.0_*_support`、`PV_7.0` 都已核对通过）。
   换任何新 key 前跑一次 `python tools\cg_manifest_check.py <关键字>`，看是否 `清单:有`。
9. **`video_cg_key` 不是文件名，是查表键** —— 所以不能随便起个名字就叫 `my_video.usm`。
   反汇编依据（UserAssembly.dll）：
   ```
   GachaCGBGCmpt.TryPlayCg          0x180C57C90   ; IsNullOrEmpty(key) → 调下一层
     └─ CheckCustomVideo(key)       0x180C570D0   ; key 塞进查询对象 [obj+0x10]，走表查询
          （0x18557B530 / 0x184CA1320 / 0x184C69C50 → 0x181401980）
     └─ MonoVideoPlayerV2.LoadOrPlayVideo  0x180EFD810
          0x180EFD8A3  call 0x1816C8590   ; = cgDataItem.NAEEBGABDMN() → 取行里的路径字段
          0x180EFD8CF  call 0x186911B40   ; = String.Format(模板, 上面的路径)
   ```
   `NAEEBGABDMN()` @ `0x1816C8590` 的实现是：`[this+0x10] != null` → 走 `0x1845BEAC0` 解析，
   否则直接 `mov rax,[this+0x18]` 返回字符串字段。也就是**路径完全由 CgData 那一行决定**。

   旁证：`ConfigGacha.GachaVideoDict` 是 `SortedList<GachaType, GachaVideo>`，
   而 `GachaVideo` 只有 `CgExtraKey` + `GachaIDs`（MoleMole.cs:118730/118739）——
   客户端内置的回落表存的也是 key，不是文件名。

   所以想播自制视频只有三条路：
   | 方案 | 是否动原文件 | 代价 |
   |---|---|---|
   | 覆盖已有 `.usm` | 是（必须备份） | 最低 |
   | 占空槽（表里有行、本地无文件） | **否** | 低，当前采用 |
   | 改客户端 CgData 表新增行 | 否 | 高：表在加密的 `ctable.dat` 里 |
   `tools\cg_free_slot.py` 列出空槽，`tools\cg_slot_switch.py apply <CgExtraKey>` 一键切换且可 `rollback`。

---

## 九、验证记录

- **2026-10-28**：`video_cg_key = 7.0_Birthday_Seele` 时，补给页视频区**正常播放**
  → 证明 `display_ext` → `CgData` → 本地 `.usm` 整条链已通，剩下的只是素材选择。
- 同日确认：本地 `Video` 目录与 `Version.txt` 中**没有**任何 gacha 专用 CG；
  官方域名/镜像/本地 nginx 三条下载路径全部不可用。
- 4.4.7 客户端（`D:\BH3\BH3_v4.4.7_7dc3a03f50f`）已排查：无 gacha 视频素材，仅可作为机制旁证。

---

## 十、自制 USM：任意 mp4 → 可播放的 `.usm`

工具 `tools\mp4_to_usm.py`（2026-10-28：`gold.mp4` → 840 帧 / 1280×720 / 25fps，
ffprobe 840 帧、解码零错误；游戏内播放待验证）。

```bat
python tools\mp4_to_usm.py "D:\BH3\7.0\BH3_Data\StreamingAssets\gold.mp4" ^
       ".workbuddy\tmp\usm\gold.usm" --fps 25
```

默认母版取 `Video\7.0_Activity_LOOP01_*.usm`：沿用它的 0x800 文件头、4 个头部块和
`#CONTENTS END` 尾巴，只重写 `total_frames` / `framerate_n` / `framerate_d` / `max_picture_size`。
画面用 libx264（high@3.1、crf 20、`aud=1`、固定 keyint 50、无 B 帧）编成 Annex-B，再按帧切块。

### 容器三条硬规则（都是踩出来的）

1. **块占位 = size + 8**。块头是 `@SFV` + u32 size + 32 字节，其中 `u32 @ +12` 是块类型
   （0 视频帧 / 1 头部 / 2 HEADER END・METADATA END・CONTENTS END / 3 SEEKINFO）；
   但这一块在文件里实际占 `size + 8` 字节，且 `size % 0x20 == 0x18`
   （等价于负载长度 ≡ 0x18 mod 0x20）。少写这 8 字节，解复用器在第二块找不到 `@SFV`
   就直接当文件结束 —— **ffprobe 只报 1 帧**，且不报错。这是本轮卡最久的点。
2. **一帧 = 一个访问单元 = 一个块**，按 AUD（NAL 9）切；SPS/PPS/SEI 必须和第一个 IDR 片
   留在同一帧里。把参数集单独切出去会复现完全一样的「只出 1 帧」。
3. **起始码统一成 4 字节**。`00 00 01 → 00 00 00 01` 不能直接 `replace`，
   否则已经是 4 字节的 `00 00 00 01` 会变成 5 字节。

### seekinfo 故意没重写（安全）

第 2 块 `VIDEO_SEEKINFO` 是 @UTF 表（列 `ofs_byte` / `ofs_frmid` / `num_skip` / `resv`，
每 30 帧一格，`ofs_byte` 是绝对文件偏移），直接沿用母版那份 10 秒的：

- 第 0 帧所在块偏移没变（都是 `0xbc0`），循环回首帧不受影响；
- 旧偏移全部落在新文件范围内（新 12.5 MB > 旧 3.5 MB），不会越界；
- 正常播放不 seek。

要做跳转/进度条再按「帧号 → 块偏移」重算。

### 验证

```bat
ffprobe -v error -select_streams v:0 -count_frames ^
        -show_entries stream=nb_read_frames,width,height -of csv=p=0 out.usm
:: 期望 1280,720,<帧数>
```

usm 的 `format=duration` 恒为 `N/A`，时长按 帧数 ÷ 帧率 算。

### 装机位置

客户端只认 `CgData` 里存在的 key → `CgPath` → `Video\<CgPath>.usm`，**不能新增文件名**，
必须占用已有 slot。当前占用 `7.0_Activity_LOOP01`（原文件备份在 `.workbuddy\backup\video\`，
md5 与线上一致），`gacha.json` 的 `video_cg_key` 已经指向它，退出补给页再进即可。
恢复原样：把备份文件拷回 `BH3_Data\StreamingAssets\Video\`。

音轨没做：母版无音频流，`gold.mp4` 的音轨被 `-an` 丢掉。要带声音得再写 `@SFA` 块和第二条流。

---

## 十一、第二批自制 USM：朔夜观星补给背景图（2026-10-29）

### 素材与预处理

源文件 `D:\Download\bilibilidown\【崩坏三】游戏CG合集（已更新至活动·光锁迷城“天穹“）\[P0574]其他·朔夜观星补给背景图.mp4`：
972×856 / 30 fps / 844 帧 / 28.1 s，内容是德丽莎·朔夜观星卡池立绘，镜头从腿部近景缓慢拉远到整幅。

**972×856 ≈ 1.135，是竖构图，直接按 1280×720 拉伸会把角色压扁**（横向拉宽 57%）。
所以先做 1280×720 的「中景靠模糊铺底」中间片，再进 muxer：

```bat
ffmpeg -i "in.mp4" -filter_complex ^
 "[0:v]split=2[b][f];^
  [b]scale=1280:720:force_original_aspect_ratio=increase,crop=1280:720,^
     gblur=sigma=40,eq=brightness=0.02:saturation=1.25[b2];^
  [f]scale=1280:720:force_original_aspect_ratio=decrease[f2];^
  [b2][f2]overlay=(W-w)/2:(H-h)/2,fps=25,format=yuv420p[out]" ^
 -map "[out]" -an -c:v libx264 -crf 14 -preset medium stage25.mp4

python tools\mp4_to_usm.py ".workbuddy\tmp\p574\stage25.mp4" ^
       "BH3_Data\StreamingAssets\Video\5.6_Gacha_968B7654D5355968.usm" --fps 25
:: OK ...: 703 frames, 1280x720, tick=100 scale=2500
```

- 中间画幅 818×720 居中，两侧 231 px 是同一张图放大裁切 + 高斯模糊（不是纯黑边）。
- `--fps 30` 时 tick=83 不整除（见下节，最终没用 30）。

### 踩坑：`4.7_Gacha` 这个「空槽」在 7.0 客户端里根本不存在

第一版占的是 `4.7_Gacha`（CgPath `Theresa_Gacha_4.6_6463FC75BA5B5E98`，服务端 6.6 表里有行、
本地无文件、名字还是德丽莎的卡池 CG，看着完美）——**结果仍然是「加载中」**。

不是热加载、也不是 `Version.txt` 缓存的问题，日志能直接排除：

```
12:00:27  gacha_pool_2.json 存盘（key 已是 4.7_Gacha）
12:02:05  GetGachaDisplayReq        ← 客户端确实拿到了 4.7_Gacha
12:02:38  GetGachaDisplayReq  (端口 54258 → 55836，说明中间重启过游戏)
12:02:42 / :45  又请求 ✓            ← 重启后 Version.txt 已重新读入，仍是加载中
```

所以结论只有一条：**客户端 7.0 的 CgData 里没有 `4.7_Gacha` 这一行**（或在 7.0 被删/改名）。

> **空槽列表只能用「服务端 6.6 CgData」筛，6.6 有行 ≠ 7.0 客户端有行。**
> `tools\cg_free_slot.py` 给出的 302 个空槽里，只有实测能确认哪个还活着。
> 目前实测可用的只有 **`5.6_Gacha`**（CgPath `5.6_Gacha_968B7654D5355968`）。

### 最终占用的槽：复用已验证的 `5.6_Gacha`

既然 key 能不能解析是唯一的硬约束，就直接**覆盖上一个己方自制文件的内容**——
文件名、清单行、key 全部不动，只换画面。这样变量只剩「视频本身」：

| 项 | 值 |
|---|---|
| `video_cg_key` | **`5.6_Gacha`** |
| `CgPath` | `5.6_Gacha_968B7654D5355968` |
| 文件 | `BH3_Data\StreamingAssets\Video\5.6_Gacha_968B7654D5355968.usm`（12.7 MB / 703 帧） |
| 清单 | `Version.txt` 里原有的那一行，**没有改动** |
| 配置 | `server-v2\run\gacha_pool_2.json` → `video_cg_key`（mtime 12:03:40） |

`4.7_Gacha` 的文件与清单行已删掉（自建、未生效、13.4 MB 白占）。

### 帧率取 25 而不是 30

第二版刻意用 `--fps 25`：

- `tick = fps_d*scale/fps_n = 1000*2500/25000 = 100`，与已验证可播的旧文件**完全同一组参数**；
- 30 fps 时 tick=83 不整除（误差 0.1%，本可忽略），但既然要排查，就别留没必要的变量。

中间片滤镜链在输出前加了 `,fps=25`，一遍成型（`stage25.mp4` → 703 帧），避免二次编码。

### 验证

- 中间片：1280×720 / 25 fps / 703 帧。
- usm：`ffprobe -count_frames` → `1280,720,703`，`ffmpeg -f null -` 零解码错误；
  回抽第 820 帧（原 30 fps 编号）画面正常。
- 旧文件备份：`.workbuddy\backup\gacha_usm_5.6_old.usm`（248 KB 自制静态图）。

### 回退

- 恢复上一版静态图：`copy .workbuddy\backup\gacha_usm_5.6_old.usm BH3_Data\StreamingAssets\Video\5.6_Gacha_968B7654D5355968.usm`
- 恢复第二池背景配置：`copy .workbuddy\backup\gacha\gacha_pool_2.before_p574.json server-v2\run\gacha_pool_2.json`

---

## 十二、真正的病根：type=3 的 `protect_display_info` 缺字段 → NRE 打断整页初始化（2026-10-29）

### 结论

精准补给（`gacha_type=3`）的「加载中....」**跟 CG 一点关系都没有**，是
`hcoin_gacha_data` 少了一个 `protect_display_info`，客户端在
`MonoSpecialGachaCmpt.SetupHcoinGachaDataProtectedInfo()` 里抛 NullReferenceException，
把 `AttachSpecialCmpt → AttachGachaCmpt` 整条挂载链打断，**视频组件根本没被挂上**，
所以 `GachaCGBGCmpt` 一直停在预制体的默认「加载中」状态。

### 铁证（客户端 `output_log.txt`）

```
%LOCALAPPDATA%\..\LocalLow\miHoYo\Honkai Impact 3\output_log.txt

NullReferenceException: Object reference not set to an instance of an object.
  at MoleMole.MonoSpecialGachaCmpt.SetupHcoinGachaDataProtectedInfo ()
  at MoleMole.MonoSpecialGachaCmpt.SetupView ()
  at MoleMole.MonoGachaContentCmpt.AttachSpecialCmpt (proto.GachaType gachaType)
  at MoleMole.MonoGachaContentCmpt.AttachGachaCmpt (proto.GachaType gachaType)
  at MoleMole.MonoGachaContentCmpt.OnSelected (proto.GachaType gachaType)
```

> 这条日志是排查的转折点 —— 之前所有「换 key 试试」全部无效，
> 因为**换 key 的代码路径压根没被执行**。

### 反汇编

`MonoSpecialGachaCmpt::SetupHcoinGachaDataProtectedInfo` @ `0x182CEE6B0`（696 字节）：

```c
pHVar3 = HPGEGKBGMOA::JMNDBOPCHMJ(this->bgImageCmpt);   // 取 hcoin_gacha_data
if (pHVar3 != NULL) {
  ...
  if (pHVar3->protect_display_info == NULL) {
      il2cpp::codegen::raise_null_reference_exception();   // ← 崩在这
  }
  aNStackX_10[0].m_Value = pHVar3->display_max_times
                         - *(int *)(pHVar3->protect_display_info + 0x28);  // no_protect_gacha_times
  ...
  NDJIKCKIDCM::GOEHIFJFOOE(this_00, pHVar3->protect_display_info);   // @0x181F60E50
}
```

`GOEHIFJFOOE` 里对两个数组**直接解引用、没有 null 判断**：

```c
if (DAHKPOGFAGN->display_key_item_list == NULL) raise_null_reference_exception();
... 对每个 id： RewardUIData::GetItemData(...)
... 再遍历结果： if (DAHKPOGFAGN->droped_up_item_list == NULL) raise_null_reference_exception();
               FUN_1849a2fe0(droped_up_item_list, id, 0)   // Contains
```

注意：**空数组在 protobuf 里等于字段不存在**，客户端拿到的是 `null`。
所以 `droped_up_item_list` 不能留空，必须塞至少一个元素 —— 用一个不存在的 id `0`
即可（渲染循环遍历的是 `display_key_item_list` 生成的列表，`droped` 只用来判「已获得」，
id 0 永远不会命中）。

### 服务端修法（`GetGachaDisplayReqHandler.cs`）

```csharp
hcoinGachaData.ProtectDisplayInfo = new GachaProtectDisplayInfo
{
    protect_display_type = GachaProtectDisplayInfo.ProtectDisplayType.EquipDisplay,
    DisplayKeyItemLists = featuredIds.ToArray(),   // 池里 featured 的武器/圣痕 id
    DropedUpItemLists   = new[] { 0u },            // 不能留空，见上
    NoProtectGachaTimes = 0u
};
```

`protect_display_type` 用 `EquipDisplay(2)`：精准补给本来就是装备池，
保底面板显示本期 UP 装备、全部未获得。

### 验证

```
12:13:11 <Packet> [Gacha] 下发池 #1 type=5 hcoin=ok custom=ok upW=0 upS=0 avatar=803 protect=ok
12:13:11 <Packet> [Gacha] 下发池 #2 type=3 hcoin=ok custom=ok upW=1 upS=3 avatar=0 protect=ok
```

### 教训

1. **先看客户端日志，再动手换参数**。`output_log.txt` 里第一条 NRE 就写明了断点，
   而前几轮我一直在猜 `video_cg_key`，绕了 4 圈。
2. 同一症状（「加载中」）至少有三条完全不同的成因：up_avatar_list 过长、CG key 查不到、
   以及本例的**上游初始化异常把挂载链打断**。前两条修好了不代表第三条不存在。
3. 服务端 `protect_display_info` 这类「看着可选」的嵌套消息，客户端往往当必填用。
