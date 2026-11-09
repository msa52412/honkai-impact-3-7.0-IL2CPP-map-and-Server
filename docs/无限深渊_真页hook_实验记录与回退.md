# 无限深渊 真页 hook —— 实验记录与回退归档

日期：2026-10-28
结论：**未打通**。点卡能进界面，但落在 OW 无尽分组页，按迪拉克（缓冲区）渲染，
不是 `EndlessPreActivityPageContext`（真页）。服务端单机活动链已全部回退。

---

## 一、本次实测结果

| 项 | 值 |
|---|---|
| 客户端 | BH3.exe pid=6668，08:47:27 启动 |
| 服务端 | Server.exe pid=15668，08:47:16 启动（产物 08:43，含全部新增代码） |
| hook | `rehook_abyss.bat force` 成功：DoClickAction → `0x2a746000`，TypeToMode 补丁已写 |
| 目标卡 | WorldMap → 挑战，ExcelMeta `meta.ID=11` |
| 服务端配置 | `single_activity_id=10500002`，`single_activity_type=2`，`single_cur_floor=1` |
| 点卡结果 | **进得去，但界面是迪拉克缓冲区**，不是无限深渊专属页 |

### 为什么显示成「迪拉克缓冲区」

两个因素叠加，都指向同一件事：页面是按 **mode 2（迪拉克之海）** 渲染的。

1. `TypeToMode` 补丁（RVA `0x19C6C90`）把 `type 0` 改写成 `mode 2`。
   这是为了让 `SetupView` 能拿到 base data 不崩，代价就是整个 OW 页面走迪拉克那套视图。
2. `run\abyss_ow.json` 里 `group_level=34`。`AbyssConfig.cs` 的既有注释已经写明：
   **客户端 mode=2 只认 21~24**（21 原罪 / 22 禁忌 / 23 苦痛 / 24 红莲），
   服务端 `OWEndlessGroup.json` 里虽然有 31~35 的 Type=2 行，但客户端那张加密表不认这一段。
   34 正好落在这段「阶梯上方」的档位上，呈现出来就是缓冲区。

> 教训：判断档位是否合法**必须以客户端表为准**，不能拿服务端 `ExcelOutputAsset` 里的
> `OWEndlessGroup.json` 当依据 —— 两份表不是同一张。

### 服务端活动链有没有生效

`pre_general_activity_id=10500002` 已随 3433 下发，4003/4004/4005/4006 都有响应
（handler 均被工厂加载，编译期已确认）。也就是说 **数据是送到了的，但客户端没有因此
切到真页** —— `PreActivity.Status()` 依然没有返回 0/1，点击仍走 `case 0` 的分组页分支。

这说明「补 3433 字段 + 4005/4006」这条路**不足以**让客户端构造 `EndlessPreActivityPageContext`。
`EndlessSingleModeActivity` 里缺少客户端判断状态所需的字段（proto 里 `EndlessSingleModeActivity`
根本没有 status 字段，状态来自 `general_basic_info` 或客户端本地推算），
单靠现有字段拼不出 `Going(0)`。

---

## 二、服务端改了什么（已全部回退）

回退日期 2026-10-28 08:5x。以下为**当时的改动清单**，供将来重做时参考。

### 新增文件（6 个，已删除）

| 文件 | 作用 |
|---|---|
| `Server/GameServer/SingleAbyss.cs` | 合法 ID 校验、层数/怪物组查询、`DescribeAll()` |
| `Server/Handlers/GeneralActivityGetScheduleReqHandler.cs` | 4003/4004 活动排期 |
| `Server/Handlers/GeneralActivityGetMainInfoReqHandler.cs` | 4005/4006 活动主信息，按 `activity_id_list` 过滤 |
| `Common/Common.Utils/ExcelReader/OWEndlessSingleModeStage.cs` | 关卡表读取器（+`Exists(id)`） |
| `Common/Common.Utils/ExcelReader/OWEndlessSingleFloorData.cs` | 层数据读取器（`Floors`/`MaxFloor`） |
| `Common/Common.Utils/ExcelReader/OWEndlessSingleMonsterGroup.cs` | 怪物组读取器（`Groups`） |

### 修改文件（4 个，已还原）

| 文件 | 改动 |
|---|---|
| `Server/GameServer/AbyssConfig.cs` | 新增 `public static uint SingleActivityId = 0u;` 及大段注释；`Describe()` 末尾追加 `SingleActivityId=...` |
| `Server/GameServer/OwTuning.cs` | DTO 增加 7 个 `single_*` 键 + `send_pre_activity`；静态属性组 `SingleActivityId / SendPreActivity / SingleActivityEnabled / SingleScheduleId / SingleCurFloor / SingleCurScore / SingleResetTimes / SingleMonsterRandomId / SingleActivityType`；`Describe()` 读取日志追加 4 行输出 |
| `Server/Handlers/GetEndlessStatusReqHandler.cs` | `using Common.Utils.ExcelReader;`；3433 响应里条件填 `proto.CurStatus.PreGeneralActivityId = singleId`（经 `SingleAbyss.Valid` 硬校验） |
| `Server/Commands/AbyssCommand.cs` | 新增 `case "single"` / `case "s"` / `case "singleinfo"`；用法字符串追加 `"single [0|id]", "singleinfo"` |

### 配置（已还原）

`run\abyss_ow.json` 追加过 8 个键，现已移除：

```json
"single_activity_id": 10500002,
"single_schedule_id": 0,
"single_cur_floor": 1,
"single_cur_score": 0,
"single_reset_times": 0,
"single_monster_random_id": 0,
"single_activity_type": 2,
"send_pre_activity": true
```

---

## 三、怎么恢复（如果将来要继续这条路线）

完整备份在 **`D:\BH3\7.0\.abyss_single_2026-10-28\server-v2_backup\single_abyss\`**（原相对路径镜像，整个归档目录已设 Hidden 属性）。

```text
恢复步骤：
1. 把备份目录里的文件按相同相对路径覆盖回 server-v2\
     Server/GameServer/AbyssConfig.cs
     Server/GameServer/OwTuning.cs
     Server/GameServer/SingleAbyss.cs
     Server/Handlers/GetEndlessStatusReqHandler.cs
     Server/Handlers/GeneralActivityGetScheduleReqHandler.cs
     Server/Handlers/GeneralActivityGetMainInfoReqHandler.cs
     Server/Commands/AbyssCommand.cs
     Common/Common.Utils/ExcelReader/OWEndlessSingle*.cs   (3 个)
     run/abyss_ow.json
2. cd server-v2 && dotnet build Server/Server.csproj -c Release
3. 停掉 Server.exe，build.bat 重新 publish
4. 服务端重启；客户端重新登录（3433 只在登录时请求）
```

**注意**：恢复 `run/abyss_ow.json` 会带回 `group_level=34`。若要走真页实验，
先把它改回 **21**，否则界面一定是缓冲区。

---

## 四、下次继续时的三个明确方向

1. **先修档位再谈模式**：把 `group_level` 改回 21，重看一次界面，确认「缓冲区」是档位造成的，
   还是 mode 2 渲染的固有结果。这一步成本最低，但能排除一个变量。
2. **验证 `PreActivity.Status()` 的真实取值**：与其继续猜字段，不如用 icall 在点击前直接读
   `[OWEndlessModule+0x50]` → `EndlessPreSingleActivity` → `+0x?? Status`，
   看它到底是 2 还是 0 —— 这是唯一的分叉判据，读一次就知道还差什么。
3. **不要再用服务端表判断合法性**：客户端 `OWEndlessSingleModeStageMetaData` 才是准绳，
   服务端 `ExcelOutputAsset` 只能当同名参考。表版本可能不同。

---

## 五、回退后的状态（08:56 实测）

- `server-v2` 源码：本次改动 0 残留，`dotnet build` 0 错误，`build.bat` 已重新 publish
  （`run\Common.dll` 08:54 / `run\Server.exe` 08:55）
- 服务端：已重启，`8081 / 443 / 16100` 正常，启动日志**不再有** `GeneralActivityGet*ReqHandler`
- `run\abyss_ow.json`：只剩原有 18 个键
- 客户端 hook：**已随进程消失**（BH3.exe 已关闭，`rehook_abyss.bat off` 因此报 exit 2；
  TypeToMode 补丁是写进程内存，不是改文件，进程退出即还原）
- 脚本产物：已归档并隐藏
  - 归档目录 `D:\BH3\7.0\.abyss_single_2026-10-28\`（Hidden，内含备份 + 两个日志）
  - `rehook_abyss.bat`、`rea\_rea\wmabysspick.py` 已设 Hidden 属性
  - **取消隐藏**（cmd 里执行）：
    ```cmd
    attrib -h "D:\BH3\7.0\rehook_abyss.bat"
    attrib -h "D:\BH3\7.0\rea\_rea\wmabysspick.py"
    attrib -h "D:\BH3\7.0\.abyss_single_2026-10-28"
    ```
    或在资源管理器「查看 → 隐藏的项目」里直接看到（隐藏文件仍可用完整路径运行）
- 本文档 `docs\无限深渊_真页hook_实验记录与回退.md` **保持可见**，作为记录入口

---

## 六、2026-10-28 复核：7.0 里无限深渊的界面与资源**都还在**

问题不是资源被砍，是客户端不构造 `EndlessPreActivityPageContext`。以下为实测证据。

### 6.1 代码层（反编译产物 `rea\_rea\src`）

| 项 | 证据 |
|---|---|
| 页面类 | `EndlessPreActivityPageContext`（0x180877130），17 个已绑定 UI 字段 |
| 页面方法 | `RefreshView / RefreshMonster / RefreshReward / RefreshAvatarUp / RefreshGeneral / RefreshLevel / OnClickLevel / ShowOWLevelPreparePage / SetupView` |
| 对话框 | `EndlessPreActivityGotoEndlessDialogContext`、`..ResetFloorDialogContext`、`..RewardDialogContext` |
| 关卡内 | `InLevelOWSingleEndlessPrepareDialogContext`、`OWSingleEndlessFinishMonsterGroupDialogContext` |
| 数据类 | `EndlessPreSingleActivity`（+0x10 OWSingleEndlessData / +0x28 CurScore / +0x2C CurFloor / +0x38 Meta） |
| 入口 | `OWEndlessModule.PreActivity`（get 0x1D1E9F0 / set 0x1026530） |

### 6.2 资源路径层（`bh3_blob.tsv` = 已解密的 IL2CPP 字符串堆）

```text
UI/Menus/Page/EndlessSingle/EndlessSingleModePage        ← 主页面
UI/Menus/Dialog/EndlessSingle/EndlessSingleDialog        ← 重置楼层对话框
UI/Menus/Dialog/EndlessSingle/EndlessSingleRewardDialog  ← 活动奖励
UI/Menus/Page/OWEndlessSingle/OWEndlessSinglePreparePage ← 关卡准备
UI/Menus/Dialog/OWEndless/OWEndlessSingleMonsterPositionDialog
```

页面类的节点路径成块出现在 0xDD78B 起（与字段一一对应）：

```text
BG/BG, BG/BG02, BossInfo/ScrollView, information/Btn_Reset,
information/BtnObject/Btn_Explain|Btn_Shop|Btn_Date,
LevelInfo/LevelPanel/EndlessGoBtn/Btn, information/Process/CarryOut/RemainTimer
```

文本键 17 个：`PreActivity_Main_Floor`、`PreActivity_Monster_Display_Score`、
`PreActivity_PassScore`、`PreActivity_ResetFloor_*`、`PreActivity_GotoEndless_*`、
`PreActivity_EnterActivityStageHint_Prepare|Settle` …

### 6.3 资产包层（`BH3_Data\StreamingAssets\Asb\pc`，440 个 UnityFS 包）

`.wmv` 后缀其实是 UnityFS 资源包（头部 `55 6e 69 74 79 46 53`）。全量分块扫描命中 4 个包含该玩法资产的包：

| 命中串 | 含义 |
|---|---|
| `SpriteAtlasTexture-EndlessSingle` | 该玩法专用精灵图集（图标/底板） |
| `assets/asbres/spriteoutput/endlesssingle` | 图集输出目录 |
| `EndlessSingleDialog` | 重置/结算对话框预置体 |
| `EndlessSingleModeChoose`、`EndlessSingleRow` | 玩法选择与列表行 |
| `EndlessSingle_Reset_Tip6` | 重置提示文案资源 |

**一个必须说明的局限**：页面主体 `EndlessSingleModePage` 在这 440 个包里搜不到，
但对照组 `OWEndlessStubPage`（此刻正在显示的页面）、`OWEndlessGroupPanel`、
`ActivityChapterOWPanel` **同样搜不到** —— 说明页面级预置体不在这一批包内
（或在 LZ4 块内被压缩），所以「搜不到」不能作为资源缺失的证据。
结论依据是 6.2 的路径已编进 7.0 的元数据 + 6.3 的图集/对话框实物存在。

### 6.4 表数据层

`server-v2\run\Resources\ExcelOutputAsset`（1658 张表，自客户端导出）中：

```text
OWEndlessSingleModeStage     3 行  10500001 / 10500002 / 10500003
OWEndlessSingleFloorData    24 行  7 / 7 / 10 层
OWEndlessSingleMonsterGroup 20 行  仅 10500002 有（AreaA..，BattleID 1..20）
```

客户端读取器 `OWEndlessSingleModeStageMetaData` / `OWEndlessSingleMonsterGroupMetaData`
（`MoleMole.ExcelMeta.cs:90281 / 90371`）也在。

### 6.5 协议层（比之前记录的多两条）

```text
EndlessSingleModeActivityFinishFloorReq  4038
EndlessSingleModeActivityResetReq        4040
EndlessSingleModeActivityBattleBeginReq  4042
EndlessSingleModeActivityBattleEndReq    4044
EndlessSingleModeActivityTakeItemReq     4046   ← 之前漏了
EndlessSingleModeActivityUseItemReq      4048   ← 之前漏了
```

`EndlessSingleModeActivity`（proto.cs:46362）字段：`cur_floor / floor_score_list /
is_settled / reset_times / monster_random_id / activity_type / ow_monster_score_list /
ow_item_list` —— **依然没有 status 字段**，这是整条路线卡住的根因。

### 6.6 结论

- 玩法界面、对话框、图集、关卡内 UI、表数据、协议，**7.0 全部保留**，没有被裁掉。
- 「33 层」在本版本不存在：该玩法最多 10 层（三层表分别为 7/7/10）。
- 进不去的唯一原因是 `PreActivity.Status()` 恒为 2（PassedToEndless），
  而状态来源不在这套活动 proto 里 —— 需要另找状态判定点，不是补资源。

---

## 七、2026-10-28 道具核查：雄伟药水 / 黑洞炸弹 / 零度炸弹 / 隐身药水

### 7.1 先纠正一个归属

这四件**不是单机无尽（本文件讨论的无限深渊）的专属道具**，而是
**旧世「无尽深渊」的玩家互相干扰道具**（PvP 层间道具）。同期一套共 8 件：

| 道具 | 旧效果 |
|---|---|
| 雄伟药水 | 服用后 SP 持续恢复，持续 90 秒 |
| 虚弱药水 | 对他人使用，目标 SP 持续衰减，持续 1 层或 2 小时 |
| 黑洞炸弹 | 对他人放置定时炸弹，1 小时内未通关则下降 1–2 层 |
| 零度炸弹 | 同上，下降 3–4 层 |
| 香蕉皮 | 在当前层放置，下一位到达者打滑 |
| 毒雾陷阱 | 在当前层放置，对下一位到达者造成伤害 |
| 隐形加护（隐身药水） | 自身隐身 30 分钟，不会成为他人目标 |
| 意志药水 | 抵挡陷阱效果，持续 3 层 |

（效果来自公开资料，与 7.0 表数据无关，仅用于确定这四件的身份。）

### 7.2 7.0 表里查不到这四件

查了 `server-v2\run\Resources\ExcelOutputAsset`（1658 张表，含 `TextMap.json` 315183 条）：

- 唯一与「无尽道具」相关的表是 **`OWEndlessItem.json`（28 行）**，逐条解码后没有任何
  雄伟/黑洞/零度/隐身的条目
- `TextMap.json` 里确有 `Blackhole Bomb` 字样，但它指向 `MaterialData` 的
  **114697**，`ItemType=currency`、`useID=0`、只出现在 `RewardData`（活动奖励兑换物），
  不是可用的深渊道具
- `Zero Bomb` / `Invisible Potion` / `Mighty|Majestic Potion` 在 315183 条文本里
  **零命中**；`隐身` 的印尼语词根 `tak terlihat` 只有一条剧情台词
- 客户端 IL 字符串堆（`bh3_blob.tsv`）里也没有对应文本键，
  仅 `Trap_Palsy_Bomb`（麻痹陷阱）与 `Invisible`（单个单词）

### 7.3 它们被「入侵 InVade」体系取代了

`OWEndlessItem.json` 28 件的完整清单（已解码，type 见 `OWEndlessItemEffect.json`）：

| type | 含义 | 条目 |
|---|---|---|
| 1 | 干扰器 | 150010 Interferron（只能对排名高 3 档的舰长使用，扣分） |
| 2 | 增益药水 | 150001-009 Protein / Elemental / Vitality Potion（S/M/L，下场战斗 90 秒） |
| 3 | 陷阱（携带 3） | 150021-028 Fire/Patrol/Lightning/Ice Drone、Laser Mesh、Honkai Bomb、QTE/Skill Disrupter |
| 4 | 反陷阱 | 150040 Anti-Debuff（清除并免疫陷阱，直到下场战斗结束） |
| 5 | 磁卡 | 150020 Mag Card（开关 Map Device） |
| 6 | 增益道具 | 150031-038 Energy/Grav/Thunder/Burst Drone、Spark Orb、Heal Self、Resonance、Frenzy |

功能对位：

| 旧道具 | 7.0 对应 | 说明 |
|---|---|---|
| 意志药水 | **150040 Anti-Debuff** | 语义完全一致：免疫陷阱 |
| 雄伟药水 | 150007/8/9 **Vitality Potion** | SP 恢复加速 90 秒；文案上 150001 Protein Potion（「肌肉变大」）也像「雄伟」 |
| 香蕉皮 / 毒雾陷阱 | 150021-028 八件陷阱 | 从「整人」改成「入侵时放置」 |
| 黑洞炸弹 / 零度炸弹 | **无** | 现代体系不再有「降层」，改为入侵扣分；`Grav Drone(150032)` 只是生成黑洞聚怪 |
| 隐身药水 | **无** | 现代体系没有「被指定为目标」的机制，隐身失去意义 |

### 7.4 结论

- 这四件在 7.0 **不存在**（不是被隐藏，是数据层没有）
- 同一功能的「意志药水」「雄伟药水」以 `OWEndlessItem` 的新 ID 存在
- 黑洞/零度/隐身三件的机制（降层、被 targeting）随旧无尽深渊一起被删除
- 想在这套单机无限深渊里加「药水/炸弹」，可用的现成 ID 就是 `OWEndlessItem`
  的 28 件；新增需要同时补 `OWEndlessItem` + `OWEndlessItemEffect` 两表与客户端
  `EndlessSingleModeActivityUseItemReq(4048)` 的处理
