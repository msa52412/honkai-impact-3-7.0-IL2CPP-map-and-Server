# 量子奇点 BOSS 关 / 切层失败 —— 反编译客户端代码核对报告

生成时间：2026-10-26 18:40
依据：`D:\BH3\7.0\rea\_rea\src`（反编译产物，视为完成产物，未再做反汇编）
数据：`D:\BH3\7.0\server-v2\run\Resources\ExcelOutputAsset\*.json`

---

## 一、客户端表结构（代码确认，非推测）

### 1. `OWEndlessBattleConfig`（`MoleMole.ExcelMeta.cs:88964`）

```csharp
public class OWEndlessBattleConfig {
  public int   BattleConfigID;        // +0x20
  public byte  EndlessConfigType;     // +0x24
  public int[] EnvironmentIDList { get; }
  public int[] AreaIDList        { get; }
  public int[] BossAreaIDList    { get; }
  public int   MapID             { get; }
  public int   AreaScoreConfigID { get; }
}
```

**结论**：客户端这张表里**没有任何天气/能级字段**，服务端唯一能选的键就是 `BattleConfigID`。
服务端 `OWEndlessBattleConfigExcel` 字段与服务端 JSON 完全一一对应 → 服务端选盘链路正确。

### 2. `OWEndlessAreaScoreConfig`（`MoleMole.ExcelMeta.cs:88808`）

```csharp
public class OWEndlessAreaScoreConfig {
  public int    AreaScoreConfigID;      // +0x20
  public List<MonsterGroup> _splitBattles;      // +0x28
  public string[] MonsterScoreConfig { get; }   // 每层一个字符串 "a:b:c:boss"
  public int[]    BossScoreConfig    { get; }   // 每层一个 BOSS 组 ID
  public byte     Type               { get; }
  public List<MonsterGroup> SplitBattles { get; }

  public struct MonsterGroup {        // 0xC 字节
    public int  groupScoreId;         // +0x0
    public bool isBoss;               // +0x4
    public int  layerId;              // +0x8
  }
}
```

**结论**：
- `SplitBattles` 元素结构 `{groupScoreId, isBoss, layerId}` 与服务端 JSON **完全一致**
  → 服务端 `IndexOf / Advance / nextGroup` 推进与客户端同序，**进度推进逻辑没问题**
- **`MonsterScoreConfig` 长度 = 层数**，`BossScoreConfig` 长度也 = 层数
- 注意：这里的 `MonsterGroup` 是 0xC 字节的 **struct**，与 `OWEndLessLayerInfo.MonsterGroup`（0x78 字节的 class）同名但**不是同一个类型**

### 3. `OpenWorldArea`（`MoleMole.ExcelMeta.cs:84226`）

```csharp
public class OpenWorldArea {
  public int areaId;                  // +0x20
  public int    mapId        { get; }
  public string[] areaList   { get; }   // 该区域的战斗区名，如 AreaA..AreaH
  public int    levelID      { get; }
  public string switchPoint  { get; }
}
```

---

## 二、运行时对象（代码确认）

### `OWEndLessBattleLevelInfo`（`MoleMole.cs:402065`，0xA0 字节，43 方法）

| 偏移 | 字段 | 含义 |
|---|---|---|
| +0x20 | `currentEnterLayerIndex` | 本次进入的层号 |
| +0x28 | `_areaList` | ← `OWEndlessBattleConfig.AreaIDList` |
| +0x30 | `_bossAreaIDList` | ← `OWEndlessBattleConfig.BossAreaIDList` |
| +0x38 | `_maxLayerIndex` | 最大层号（`GetMaxLayIndex()` 算） |
| +0x68 | `_environmentIDList` | 每层天气 |
| +0x74 | `_scoreConfigID` | AreaScore |
| +0x78 | `_areaCount` | 区数 |
| +0x7C | `_groupLevel` | 段位 |
| +0x80 | `_groupHardLevel` | 能级 |
| +0x88 | `_areaBattleInfoList` | `List<OWEndLessLayerInfo>`，**每层一个** |
| +0x90 | `_bossExtraScore` | BOSS 额外分 |
| +0x94 | `_bossExtraScoreInLevel` | 关卡内 BOSS 额外分 |

关键方法：
```
Refresh(GetOpenworldEndlessDataRsp rsp)   0x1815C4E90   ← 全部在此构建
GetMaxLayIndex()                          0x1815C45D0
GetAreaLayerIndex(int areaID)             0x1815C3AC0   ← areaID → 层号
GetEnvironmentIDByLayerIndex(int)         0x1815C3B70
```

### `OWEndLessLayerInfo`（`MoleMole.cs:402316`，0x90 字节）

```
+0x10 _layerIndex        +0x14 _monsterAreaID     +0x18 _bossAreaID
+0x1C _scoreConfigID     +0x20 _environmentID     +0x24 _buffID
+0x28 _maxScore          +0x2C _randomSeed        +0x30 _groupHardLevel
+0x34 _eliteRatio        +0x38 _monsterType
+0x40 _areaConfig          ← OpenWorldArea 行（含 areaList / levelID）
+0x48 _environmentConfig   ← OWEndlessEnvironmentMeta（含 BuffID）
+0x50 _scoreConfig         ← OWEndlessAreaScoreConfig
+0x58 _monsterConfigList
+0x60 _monsterGroupIDList  +0x68 _bossGroupID      ← BOSS 组 ID
+0x70 monsterGroupList     +0x78 bossGroup         ← MonsterGroup 对象
+0x88 invadeBossGroupList
```

**结论**：
- **天气与 buff 是「每层一份」存在 `OWEndLessLayerInfo` 上的**（`_environmentID` / `_buffID`），不是全局
- **`_bossGroupID` 是 BOSS 组的唯一来源**；`bossGroup`（MonsterGroup）由它构建

关键方法：
```
GeneralMonster()                                        0x181C765C0   ← 生成小怪
GeneralBoss()                                           0x181C75A10   ← 生成 BOSS
IsNormalMonsterClear()                                  0x181C779E0
GetBattleAreaIndexListByRandomSeed(resultCount, candidateCount, randomSeed)  0x181C76E80
GetMonsterListByMaxScore(BossRank, maxScore, initType, scoreRatio, groupID)  0x181C77110
GetRandomEmptyAreaNameForInvade()                       0x181C77640
```

**`GetBattleAreaIndexListByRandomSeed(resultCount, candidateCount, randomSeed)`** —— 战斗圈（挑战区）
的位置是按随机种子从 `_areaConfig.areaList` 里挑的，`candidateCount` 就是候选区个数。

### `MonsterGroup`（`MoleMole.cs:402449`，0x78 字节）

```
+0x10 useNewAlgo        +0x14 groupID          +0x18 maxScore
+0x1C MonsterInitType   +0x20 MonsterScoreRatio
+0x28 areaName          ← 战斗区名
+0x30 isBoss            +0x34 bossID           +0x38 canBossRefresh   ← BOSS 能否刷新
+0x3C environmentID     +0x40 monsterIDList    +0x48 monsterNature
+0x50 uniqueIDList      +0x58 luaFilePath      ← 切场景用的 lua
+0x60 isFinish          +0x61 isInvade         +0x62 usCustomAmount
+0x70 buffIDList
```

**`canBossRefresh`（+0x38）是 BOSS 能不能刷的直接开关**，`luaFilePath`（+0x58）是切场景目标。

### `QTInLevelData`（`MoleMole.cs:402750`）

```
+0x10 _areaScoreConfigId   +0x14 _monsterGroup   +0x18 _battleConfig
+0x20 _transaction         +0x28 lastFinishReq   +0x30 scoreWhenLastFinishReq
+0x34 _extraScore          +0x38 lastDisplayProgress  +0x3C _progress
+0x40 _randomSeed          +0x48 _module
```
`Refresh(GetOpenworldEndlessDataRsp rsp)` @ 0x1811F15F0

**结论：`QTInLevelData` 里没有任何「时限 / 倒计时」字段。**
关卡内计时器来自 `QTEndlessGetBattleTime()`（`MoleMole.cs:216254`，返回 float），
以及 `QTEndlessGetHardLevel()` / `QTEndlessGetTemp()`，**不是服务端下发的**。

---

## 三、切层 / BOSS 关的入口 API（代码确认）

```
OWEndLessJumpToOtherLua(int layerIndex, bool isBoss, bool sceneChange, bool stopBGM)   0x183428980
GetOWEndlessBattleGroupListByLayerIndex(LuaTable table, int LayerIndex)               0x183414410
QTEndlessInitPlugin()                                                                 0x1834315A0
QTEndlessGetCurrentMonsterGroup(LuaTable battleTbl)                                   0x183431070
QTEndlessGetMonsterGroup(LuaTable battleTbl, int monsterGroupIndex)                   0x1834311C0
GetTrapMaxNumByAreaName(string areaName)                                              0x18341D960
GetOWEndlessTrapTableByAreaName(string areaName)                                      0x183415800
```

```
OWEndlessModule.GetOWEndLessLayerInfoByAreaID(int areaID, bool isBossLayer)   0x181898CB0
OWEndlessModule.GetOWEndLessLayerInfoByLayerIndex(int layerIndex)             0x181898F00
OWEndlessModule.IsTheLastLayerByLayerIndex(int layerIndex)                    0x181899FC0
OWEndlessModule.GetCurrentEnterOWEndLessLayerInfo()                           0x1818982D0
OWEndlessModule.GetLastEnterOWEndLessLayerInfo()                              0x1818987B0
```

**结论**：切场景由 `(layerIndex, isBoss)` 两个参数决定，客户端据此从 `_areaBattleInfoList`
里取 `OWEndLessLayerInfo`，再用该层的 `_areaConfig` / `_bossGroupID` 生成战斗区与怪物。

---

## 四、用「区数 vs 层数」量化自洽性

判据：`len(AreaIDList)` 必须 == `len(MonsterScoreConfig)`（层数），
`len(BossAreaIDList)` 也应 == 层数，且 `BossScoreConfig` 非空。

| type | 区数 vs 层数 | BOSS区数 vs 层数 | BossScoreConfig | 行数 |
|---|---|---|---|---|
| **2 迪拉克** | 区 == 层 | BOSS区 == 层 | **非空** | **598** |
| **3 量子** | **区 < 层** | BOSS区 < 层 | **空** | **248** |
| **3 量子** | 区 == 层 | BOSS区 < 层 | **空** | **51** |

### 代表盘

```
cfg=62136 (迪拉克) score=1006
  AreaIDList=[3011,3012,3014,3013] (len=4)   BossAreaIDList=[3015,3016,3017,3018] (len=4)
  MonsterScoreConfig 4 层                      BossScoreConfig=[131,132,133,134]   ← 非空
  → 完全自洽

cfg=100004 (量子 20 场) score=2001
  AreaIDList=[3100] (len=1)                   BossAreaIDList=[] (len=0)
  MonsterScoreConfig 4 层                      BossScoreConfig=[]                  ← 空
  → 区数 1 < 层数 4，第 2 层起取不到区

cfg=2001 (量子 5 场) score=2101
  AreaIDList=[3100] (len=1)                   BossAreaIDList=[] (len=0)
  MonsterScoreConfig 1 层                      BossScoreConfig=[]                  ← 空
  → 小怪层数自洽；BOSS 层拿不到 _bossGroupID

cfg=5001 (量子 5 场) score=2104
  AreaIDList=[3100] (len=1)                   BossAreaIDList=[] (len=0)
  MonsterScoreConfig 1 层                      BossScoreConfig=[]                  ← 空
  → 同上
```

---

## 五、结论（代码 + 数据双重确认）

> **2026-10-26 18:45 重要修正**
> 用户指出：**迪拉克是 4 张物理不同的地图；量子全程只有 1 张地图，只是每层刷的怪不同，
> BOSS 也在同一张图上。**
> 因此下面第 4 节「区数 < 层数 = 数据残缺」的**判据作废**——
> 量子的 `AreaIDList` 长度 1、`BossAreaIDList` 为空，都是**设计如此**，不是缺陷。
> 相应地 `BossScoreConfig` 为空也不能直接判为异常（量子 BOSS 由 `SplitBattles.isBoss` 标记）。
> 第 4 节保留仅作记录。

### 修正后的确认结论

1. **迪拉克（type=2）**
   - 4 张物理地图：`AreaIDList=[3011,3012,3014,3013]`，各自 `levelID` 不同
   - BOSS 另有 4 张图：`BossAreaIDList=[3015,3016,3017,3018]`，`levelID=900429`
   - `BossScoreConfig=[131,132,133,134]` 非空
   - → 层 / 图 / BOSS 三者一一对应，结构完整

2. **量子（type=3）**
   - 全程 1 张图：`AreaIDList=[3100]`，`levelID=900430`
   - BOSS 不换图：`BossAreaIDList=[]`
   - 每层只换怪：`MonsterScoreConfig` 每层一个字符串
   - → 上述三点均为设计，**不是数据缺失**

3. **第 6 场（layerId=1）加载失败 —— 数据层已排除**
   - `10400` 解析结果：`InitType 1001` → `WaveList=[2109,2109]` → `DistRule/RankDist` 合计
     **10 只**（DeadWalker r1×4 + r2 …），怪物数据完整
   - 20 场盘逐场体检：`10000/10100/10300/10400/10500/10700/10800/10900/11100/11200/11300/11500`
     全部能刷出怪；`10/11/12/13` 四个 BOSS 组全部能刷出 rank3 的 BOSS
   - `10200/10600/11000/11400` 的 `MonsterInitType` 落在 1500–1599 段，服务端导出表整段缺失
     （`InitType 按百段分布: 1500-1599:0`），但 **21400 用的 1504 同样缺行、实测照样能打**
     → 服务端导出表缺段不影响客户端（客户端自带一份）
   - **结论：不是怪的问题，也不是图的问题**

4. **剩下唯一待测的判决点：层信息是否被构建出来**
   链路（已由代码确认）：
   ```
   OWEndlessModule +0x68 _baseDataList : List<EndlessBaseData>
     └ EndlessBaseData +0x10 Mode / +0x28 _BattleLevelInfo
         └ OWEndLessBattleLevelInfo +0x88 _areaBattleInfoList : List<OWEndLessLayerInfo>
   ```
   - 若 `_areaBattleInfoList.size == 1` → 量子多层盘只建出第 0 层，
     `GetOWEndLessLayerInfoByLayerIndex(1)` 返回 null → `OWEndLessJumpToOtherLua(1,…)` 失败 → **加载不出来**
   - 若 `size == 4` → 层信息齐全，需另找原因（下一步查 `MonsterGroup.luaFilePath` 与
     `JumpToOtherLevelWithSceneChange`）
   - 探针已备好：`rea\_rea\qtlayer.py`（纯字段读，不调用任何客户端方法）

5. **BOSS 关（无圈 / 不刷 / 计时器 0）**
   - 圈的位置来自 `OWEndLessLayerInfo.GetBattleAreaIndexListByRandomSeed(resultCount, candidateCount, randomSeed)`，
     候选区是 `_areaConfig.areaList`
   - `OpenWorldArea[3100].areaList = []`，`OWEndlessBattleArea` 中 3100 **零行**
     → `GetTrapMaxNumByAreaName` 恒 0、候选区为 0
   - 计时器来自 `QTEndlessGetBattleTime()`，`QTInLevelData` 无时限字段 → 显示 0 是未完成初始化的表现
   - **待 `qtlayer.py` 结果确认 `bossGroup` / `_bossGroupID` 是否为空**

### 服务端当前可做的事

- 量子：只能用 51 个单层盘（`MonsterScoreConfig` 层数 = 1），多层盘第 2 层起无层信息
- 迪拉克：`battle_config_id=62136` 结构完整，推荐作为完整可玩路线
- 当前 `abyss_ow.json`：`qt_battle_config_id=5001`（AreaScore 2104，BOSS 组 52，wave 1002 `Twin03`）

---

## 六、2026-10-26 18:48 运行期实测（qtlayer.py，只读字段）

客户端 pid=63172，`OWEndlessModule=0x700c8a36dd0`，`_baseDataList.size=2`。

### 迪拉克 Mode=2（score 1006，准备页）

```
_areaList          size=4  [3011, 3012, 3014, 3013]
_bossAreaIDList    size=4  [3015, 3016, 3017, 3018]
_environmentIDList size=4  [112, 111, 113, 115]
_areaBattleInfoList size=4
_maxLayerIndex = 0        _groupHardLevel = 6

[0] layer=0 monsterArea=3011 bossArea=3015 env=112 buff=1009 bossGroupID=131 areaConfig=非空
    _monsterConfigList=54  _monsterGroupIDList=5  monsterGroupList=5  bossGroup=非空
[1] layer=1 monsterArea=3012 bossArea=3016 env=111 buff=1008 bossGroupID=132
    _monsterConfigList=40  _monsterGroupIDList=5  monsterGroupList=5  bossGroup=非空
[2] layer=2 monsterArea=3014 bossArea=3017 env=113 buff=1010 bossGroupID=133
    _monsterConfigList=33  _monsterGroupIDList=5  monsterGroupList=5  bossGroup=非空
[3] layer=3 monsterArea=3013 bossArea=3018 env=115 buff=1012 bossGroupID=134
    _monsterConfigList=53  _monsterGroupIDList=4  monsterGroupList=4  bossGroup=非空
```

### 量子 Mode=3（score 2104，准备页）

```
_areaList          size=1  [3100]
_bossAreaIDList    size=0  []
_environmentIDList size=1  [1001]
_areaBattleInfoList size=1
_maxLayerIndex = -1       _groupHardLevel = 10

[0] layer=0 monsterArea=3100 bossArea=-1 env=1001 buff=2002 bossGroupID=0 areaConfig=非空
    _monsterConfigList=49  _monsterGroupIDList=5  monsterGroupList=0  bossGroup=指针非空但无组
```

### 三条实测结论

**1. `_areaBattleInfoList` 的条数 = `_areaList` 的条数**
量子 `_areaList.size=1` → `_areaBattleInfoList.size=1`；
迪拉克 `_areaList.size=4` → `_areaBattleInfoList.size=4`。
→ 量子多层盘（248 个）**只建出第 0 层**，`GetOWEndLessLayerInfoByLayerIndex(1)` 必返回 null，
`OWEndLessJumpToOtherLua(1,…)` 失败 → **第 6 场加载不出来**。判决点确认。

**2. `_bossGroupID` = `BossScoreConfig[layerIndex]`，逐项精确吻合**
```
迪拉克 BossScoreConfig = [131,132,133,134]   → 实测 bossGroupID = 131,132,133,134  ✓
量子   2104 BossScoreConfig = []             → 实测 bossGroupID = 0                ✓
```
→ **量子全部 299 个 AreaScore 的 `BossScoreConfig` 都是空数组 → `_bossGroupID` 恒为 0
→ `bossGroup` 建不出来 → BOSS 关不刷怪。这是 BOSS 关的根因。**

**3. 量子 `monsterGroupList=0` 而 `_monsterGroupIDList=5`**
小怪靠 `_monsterGroupIDList` + `_monsterConfigList`（49 条，非空）直接算，所以能刷；
而 `monsterGroupList`（带 `areaName` 的 MonsterGroup 对象）为 0，
`bossGroupID` 也为 0 → **圈和 BOSS 都出不来**，与用户「地图内根本没有更新圈」完全一致。

### 实测日志佐证（新盘 5001 全流程）

```
18:30:45 BattleBegin group=50800 layer=1 → 18:31:06 BattleEnd status=1 cost=21   ✓
18:31:13 BattleBegin group=50900 layer=2 → 18:31:22 BattleEnd status=1 cost=9    ✓
18:31:30 BattleBegin group=51000 layer=3 → 18:31:52 BattleEnd status=1 cost=21   ✓
18:32:00 BattleBegin group=51100 layer=4 → 18:32:18 BattleEnd status=1 cost=18   ✓
18:32:18 FinishGroup next=52 index=4/5 finished=4
18:32:26 StageBeginReq → Loading_Proto 1,37 → ✗ 此后无任何 BattleBegin（BOSS 52 同样卡死）
```
→ 4 场小怪全过（含 `51000`，其 InitType 1506 在服务端导出表里缺行，照样能打，
  证明「服务端导出表缺段」与运行无关）
→ **BOSS 组 52 与 B23 一样卡死，换 wave 也无效**（52 走 wave 1002 `Twin03`，23 走 wave 1004）

### 最终定案

| 问题 | 根因 | 服务端能否修 |
|---|---|---|
| 量子第 6 场加载不出来 | `_areaBattleInfoList` 条数 = `_areaList` 条数，量子只有 1 张图 → 只有 1 层 | **不能**（客户端表） |
| 量子 BOSS 关不刷怪 / 无圈 / 计时器 0 | 量子 299 行 `BossScoreConfig` 全空 → `_bossGroupID=0` → `bossGroup` 空 | **不能**（客户端表） |
| 量子小怪场 | 正常，4 场全过 | — |
| 迪拉克全流程 | 4 层信息齐全、bossGroupID 131–134、monsterGroupList 5/5/5/4 | 结构完整，可用 |

**结论：量子奇点在本版客户端数据下只能打小怪场，BOSS 关 100% 不可用，服务端无任何杠杆。
要完整体验（含 BOSS）必须走迪拉克。**

---

## 七、附：本次新增工具

| 工具 | 用途 |
|---|---|
| `tools/qt_area_vs_layer.py` | 区数 vs 层数自洽性量化（本报告第四节） |
| `tools/qt_nbsp_scan.py` | 全量 BOSS 组健康度 + 单层盘清单 |
| `tools/qt_nbsp_detail.py` | 逐场 group→initType→wave 明细 |
| `tools/qt_boss_diff.py` | B10 vs B13/B23 全链路 diff |
| `tools/qt_bossrange.py` | `OWEndlessBossData` 的 GroupLevelRange 分布 |
| `tools/qt_mon_range.py` | 怪物池 GroupLevelRange 覆盖（已排除此项） |
| `tools/qt_bossarea_stat.py` | `BossAreaIDList` 空/非空统计 |
| `tools/qt_1layer_list.py` | 所有短盘及其 BOSS 组 |

---

## 八、运行时 patch 可行性（2026-10-26 18:55）

**问题：能否运行时改动 `_areaBattleInfoList` 和 `BossScoreConfig`？**

### 8.1 表对象层面：不能改

| 类 | 大小 | 字段 | 结论 |
|---|---|---|---|
| `OWEndlessAreaScoreConfig` | **0x30** | `+0x10 _readOffset` / `+0x18 DataImpl` / `+0x20 AreaScoreConfigID` / `+0x28 _splitBattles` | 无 int[] 缓存字段 |
| `OWEndlessBattleConfig` | **0x28** | `_readOffset` / `DataImpl` / `BattleConfigID` / `EndlessConfigType` | 同上 |

`BossScoreConfig`、`AreaIDList`、`BossAreaIDList`、`areaList` 全是 `{ get; }` 属性，
**每次调用都从 `DataImpl` blob 按 `_readOffset` 现场解析**。

- 改返回值 → 无效，下次调用重新解析
- 改 blob → 长度 0 无空间可写，扩容会移位整个 blob，不可行

→ **`BossScoreConfig` 运行时改不了，也不需要改。**

### 8.2 下游缓存字段全部可写

`OWEndLessBattleLevelInfo`：`+0x28 _areaList` / `+0x30 _bossAreaIDList` /
`+0x68 _environmentIDList` / `+0x88 _areaBattleInfoList` —— 都是 `__xxx_k__BackingField` 裸字段。

`OWEndLessLayerInfo`：`+0x68 _bossGroupID` / `+0x70 monsterGroupList` / `+0x78 bossGroup` —— 裸字段。

`MonsterGroup`：`+0x14 groupID` / `+0x18 maxScore` / `+0x1C MonsterInitType` /
`+0x28 areaName` / `+0x30 isBoss` / `+0x34 bossID` / `+0x3C environmentID` /
`+0x50 uniqueIDList` / `+0x58 luaFilePath` —— 全部裸字段。

### 8.3 关键发现：量子 `bossGroup` 是「真实对象 + 全 0 数据」

`qtbossdump.py` 实测（pid=63172）：

```
量子 layer0  bossGroup = 0x700cdcf0300   klass = 0xdd1c558  ← 与迪拉克 BOSS 组同一 klass
  useNewAlgo=0  groupID=0   maxScore=0   InitType=0  ScoreRatio=0.0
  areaName=null  luaFilePath=null
  isBoss=1      ← 已经是对的
  bossID=0      envID=0
  monsterIDList=[]  uniqueIDList=[]  buffIDList=[]
```

对比迪拉克 layer0 `bossGroup = 0x700ce48c980`：

```
  groupID=131  maxScore=1700  InitType=0  ScoreRatio=0.0
  areaName='AreaBoss'  luaFilePath=''
  isBoss=1     bossID=100038   envID=112
  monsterIDList=[]   uniqueIDList=[771]
```

→ Refresh 里 `new MonsterGroup()` 确实建了对象，但因为 `BossScoreConfig=[]` 没数据可填，
得到空壳。**所以不需要分配内存，只需往这个空壳写字段。**

### 8.4 表里 group 52 的定义是完整的（不是数据缺失）

```
OWEndlessMonsterGroupScore（键是 MonsterGroupID，不是 groupScoreId）
  52  → Score=3600, MonsterInitType=2301, RandomType=2
  131 → Score=1700, MonsterInitType=0,    RandomType=1
  132 → Score=2200, MonsterInitType=0,    RandomType=1

OWEndlessMonsterInitType(2301) → WaveList=[1002]  isNegate=true  EnvironmentIDList=[]  Type=1
QTEndlessMonsterWave(1002)     → DistRule=[Twin03,Twin03]  RankDist=[3,3]
                                 → 精确命中 monsterID=1139 UlyssesWarrior rank3 elite，2/2 可解析
```

→ **两条 BOSS 路线不同**：
- 迪拉克 BOSS（131–134）`InitType=0` → 靠 `uniqueIDList` 直接生成 boss 单位
- 量子 BOSS（52）`InitType=2301` → 靠 `WaveList=[1002]` 走波次刷怪

→ 也说明之前「52 走 wave1002 Twin03」的推断正确，且 wave 1002 本身健康。

### 8.5 新发现：量子无圈的根因是 `areaList` 空

```
OpenWorldArea(3100).areaList = []                              ← 空数组
OpenWorldArea(3011).areaList = [AreaA,AreaB,AreaC,AreaD,AreaE,AreaF,AreaG,AreaH]
OpenWorldArea(3015).areaList = [AreaA]
```

迪拉克 layer0 的 5 个 monsterGroup 的 `areaName` = AreaA/AreaH/AreaD/AreaB/AreaF，
全部从 `3011.areaList` 里挑。量子 3100 一个都没有 → `monsterGroupList n=0`。

→ **「地图内根本没有更新圈」的根因是 `areaList` 为空，不是 `BossScoreConfig`。**

`areaList` 同样是 blob 派生属性，不可改。唯一杠杆是把 `LayerInfo+0x40 _areaConfig`
指针换成别的 `OpenWorldArea`，但会带来 `levelID` 不符、加载错地图的风险。

### 8.6 已就绪的补丁脚本

`D:\BH3\7.0\rea\_rea\qtbossfix.py` —— 默认 dry-run，`--apply` 写入，`--restore` 回滚。

dry-run 实测输出（8 处写入，全部落在已存在对象上，零分配）：

```
0x700cdcf0314  bossGroup.groupID        00000000 -> 34000000        (52)
0x700cdcf0318  bossGroup.maxScore       00000000 -> 100e0000        (3600)
0x700cdcf031c  bossGroup.MonsterInitType 00000000 -> fd080000       (2301)
0x700cdcf0330  bossGroup.isBoss         01       -> 01             (已是目标值)
0x700cdcf033c  bossGroup.environmentID  00000000 -> e9030000        (1001)
0x700cdcf0328  bossGroup.areaName       0        -> 0x700c72a3060  (借迪拉克)
0x700cdcf0358  bossGroup.luaFilePath    0        -> 0x700c6c64f40  (借迪拉克)
0x700cc249068  LayerInfo._bossGroupID   00000000 -> 34000000        (52)
```

备份文件：`rea\_rea\qtbossfix_backup.json`

### 8.7 打之前必须知道的四个风险

1. 每次准备页 Refresh 都会重建 LayerInfo → 补丁被覆盖，需在准备页打完立刻进 BOSS 关
2. 若 BattleBegin 之前客户端再 Refresh 一次，补丁白打
3. `areaList=[]` 未解决，BOSS 可能因找不到刷怪点仍然不刷
4. 这是改运行中的游戏进程，属于「拿游戏去赌」的范畴，**必须先经用户确认**

### 8.8 结论

| 目标 | 能否运行时改 | 说明 |
|---|---|---|
| `BossScoreConfig`（表属性） | **不能** | 无缓存字段，blob 现场解析，长度 0 无法扩容 |
| `_areaBattleInfoList` 已有元素的字段 | **能，零风险** | `_bossGroupID`、`bossGroup.*` 都是裸字段 |
| `_areaBattleInfoList` 新增元素（多层盘） | **能，但重** | 需分配 0x90 对象 + 填 klass + 建 List，且 Refresh 会冲掉 |
| `monsterGroupList`（圈） | **不能直接改** | 依赖 `areaList`，而 `areaList` 是 blob 属性 |
