# 舰桥隐藏主题：EVA 联动（只读排查报告）

> 排查时间：2026-11-08
> 范围：**只读**，未改动服务端代码、客户端资源或任何配置表
> 结论：7.0 数据里确实存在 EVA 联动舰桥主题，且它是**唯一被标成隐藏**的主题

---

## 一、结论速览

| 项目 | 值 |
|---|---|
| 主题条目 id | `ThemeItemID = 400001` |
| 协议侧 id（`warship_id`） | `400001` |
| 3D 场景 | `Stage/MainMenu_SpaceShip/EVA/MainMenu_SpaceShip` |
| 主界面 UI | `UI/Menus/Page/Main/EVA/MainPage` |
| 状态栏 UI | `UI/Menus/Widget/MainPage/EVA/PlayerStatusPanel` |
| BGM | `ThemeBGMConfig 100`，曲目 `[401000]` |
| 背景图 | `SpriteOutput/LevelDetailPic/ScenePicArcCity09`（长空市） |
| 当前状态 | **隐藏**：客户端 `Display: false` + 服务端不下发 |

---

## 二、完整 id 链路（已逐项核实）

```
EntryThemeItemData.ThemeItemID  400001
        │  UnlockPartID = 100
        ▼
SpaceShipConfig.SpaceShipID     100
        │  ResPath
        ▼
Stage/MainMenu_SpaceShip/EVA/MainMenu_SpaceShip   （IsSkyBoxShow = false）

ThemeUIConfig.ThemeUIID         100
        │
        ├─ MainPage           = UI/Menus/Page/Main/EVA/MainPage
        └─ PlayerStatusPanel  = UI/Menus/Widget/MainPage/EVA/PlayerStatusPanel

EntryTheme.ThemeID              100
        │
        ├─ SpaceShipConfig  = 100
        ├─ ThemeUIConfig    = 100
        ├─ ThemeBGMConfig   = 100
        └─ ElevatorConfig   = 100
```

### 涉及的表与位置

| 表 | 文件 | 关键行 |
|---|---|---|
| `EntryThemeItemData` | `run\Resources\ExcelOutputAsset\EntryThemeItemData.json` | 第 1 行 `ThemeItemID 400001`，`UnlockPartID 100`，`Rarity 4`，图标 `SpriteOutput/MaterialFigures/TestIcon`（占位） |
| `EntryThemeData` | 同目录 | 第 2 行（索引 1），`Display: false`，`ThemeUIConfig: 100`，`SpaceShipConfigID: 400001`，`SpecialConfigList: [101, 102]` |
| `SpaceShipConfig` | 同目录 | `SpaceShipID 100` → EVA 场景 |
| `ThemeUIConfig` | 同目录 | `ThemeUIID 100` → EVA 主界面/状态栏预制体 |
| `ThemeBGMConfig` | 同目录 | `ThemeBGMID 100` |
| `ThemeSpecialConfig` | 同目录 | `SpecialID 101~110`，**整张表 10 条全是 EVA 替换** |

### `ThemeSpecialConfig` 全表清单（只服务 EVA）

| SpecialID | ReplaceType | 默认资源 | EVA 替换 |
|---|---|---|---|
| 101 | 0 | `UI/Menus/Widget/NotifyRemind/PopUp` | `.../NotifyRemind/EVA/PopUp` |
| 102 | 0 | `UI/Menus/Widget/NotifyRemind/PopUpSmall` | `.../NotifyRemind/EVA/PopUpSmall` |
| 103 | 0 | `UI/Menus/Widget/NotifyRemind/PopUpNum` | `.../NotifyRemind/EVA/PopUpNum` |
| 104 | 0 | `UI/Menus/Widget/NotifyRemind/NewMark` | `.../NotifyRemind/EVA/NewMark` |
| 105 | 0 | `UI/Menus/Widget/MainPage/PlayerStatusPanel` | `.../MainPage/EVA/PlayerStatusPanel` |
| 106 | 1 | `UI/Menus/Widget/PopUp/PopUpNew` | `.../PopUp/EVA/PopUpNew` |
| 107 | 0 | `SpriteOutput/MallTabIcon/MainPageIcon/IconMall` | `.../MainPageIcon/EVA/IconMall` |
| 108 | 0 | `SpriteOutput/MallTabIcon/MainPageIcon/IconShoppingFestival` | `.../MainPageIcon/EVA/IconShoppingFestival` |
| 109 | 0 | `SpriteOutput/PlayerProfile/EXPBarInner` | `.../PlayerProfile/EVA/EXPBarInner` |
| 110 | 0 | `SpriteOutput/PlayerProfile/EXPBarInnerGreen` | `.../PlayerProfile/EVA/EXPBarInnerGreen` |

> 这张表存在的唯一目的就是 EVA 主题 —— 弹窗、红点、商城图标、经验条整条换皮。

---

## 三、为什么现在看不到（三道门禁）

### 门禁 1：客户端表标了 `Display: false`

`EntryThemeData.json` 共 7 行，**只有 EVA 这行是 `Display: false`**，其余 6 行都是 `true`。

反编译确认（`rea\_rea\bodies_merged.tsv`）：

- `GIPAMEMLBNE::FLOFPIKANDP`（RVA `0x20184E0`）—— 构建可显示主题列表
- 过滤条件：`HFPNKKPPHAL::DNBNDCLBICH() != 0`（Display）**且** `HFPNKKPPHAL::PHKHBEHHNOL() == 0`（未过期）
- 消费者：`SpaceShipOverviewPageContext::InitThemeData`（RVA `0x1771690`）→ `_displayThemes`

⇒ EVA 永远进不了「舰桥 → 主题」选择列表。

### 门禁 2：服务端不下发

`Common\Common.Database\WarshipSettings.cs:110`

```csharp
public static readonly uint[] DefaultWarshipIds = { 0u, 400002u, 400003u, 400004u, 400006u };
```

`Server\Handlers\GetWarshipDataReqHandler.cs:30` 就按这个数组拼 `GetWarshipDataRsp.WarshipLists`。

**缺 400001（EVA）**，同时缺 400005（`SpecialTag: true` 的特殊主题）。

### 门禁 3：玩家默认值

`Common\Common.Database\User.cs:32` → `userScheme2.WarshipId = 0;`（休伯利安）

---

## 四、主题名对照表（TextMap 按 hash 解出）

| hash | 名称 | 服务端是否下发 |
|---|---|---|
| `1607376964` | Bridge Hyperion（休伯利安） | 是（id 0） |
| `-1688108603` | **EVA（TextMap 查无此条）** | **否** |
| `-705871638` | Taixuan Steps（太虚山） | 是（400002） |
| `-705871637` | Arc City（长空市） | 是（400003） |
| `-705871636` | Timeless Grand Hall | 是（400004） |
| `-705871635` | Above the Moon | **否**（400005，特殊标签） |
| `-705871634` | Train to the Future | 是（400006） |

### 文本被摘的证据

- EVA 行主题名 hash `-1688108603` 在 `TextMap.json`（25 MB）里**搜不到**
- 同批另外 5 个主题名（`-705871638` ~ `-705871634`）**全部在**，且是连号
- EVA 活动的其它文本仍然保留：`EVA Countdown I~IV`、`EVA Countdown Clock`、
  「HI3 × EVA 联动活动已开启」系列、`Unit-02 adalah Evangelion asli pertama...`

⇒ 不是导出丢失，而是**主题名这一条文本被单独移除**（其余活动文案都在）。

---

## 五、启用路径（本次未执行）

### 最短路径：服务端置 `WarshipId = 400001`

关键事实：`Server\Handlers\SetWarshipReqHandler.cs:11` **没有任何校验**，直接写：

```csharp
session.Player.User.WarshipId = (int)decodedBody.WarshipId;
```

而主界面走的是 `GetMainDataRsp.WarshipTheme`（`GetMainDataReqHandler.cs:84`）
→ `WarshipSettings.ThemeOf(uid, WarshipId)`。

⇒ 只要把 `User.WarshipId` 改成 `400001`（或 GM 发一个 `SetWarshipReq{warship_id=400001}`），
主界面就会按 id 去查主题、加载 EVA 场景 + EVA UI。

**不需要动客户端表** —— `Display` 只影响「选择列表里是否出现」，不影响按 id 加载。

### 若要出现在选择列表里

必须改客户端 `EntryThemeData.Display`。但客户端表在加密的 `ctable.dat`（26.9 MB，头部高熵），
按项目既有结论（ASB 改包 / 内容级校验）这条路基本是死路。

### 已知风险

| 风险 | 说明 |
|---|---|
| 主题名空白 | TextMap 缺 key，选择页/详情页标题会是空的 |
| 图标是占位 | `SpriteOutput/MaterialFigures/TestIcon`，不是正式图标 |
| 资源是否随包**无法静态验证** | bundle 头 `UnityFS` + LZ4 压缩，路径不落明文；440 个包 / 16.1 GB，抽样 4 个中等包搜 `Page/Main/EVA` 均 0 命中 |

⇒ 唯一可靠的验证方式是实际切一次，看是否黑屏/回退。

---

## 六、复现方式（只读）

```powershell
# 主题表
Get-Content "D:\BH3\7.0\app\server-v2\run\Resources\ExcelOutputAsset\EntryThemeData.json" -Raw

# 场景与 UI 映射
Get-Content "...\ExcelOutputAsset\SpaceShipConfig.json" -Raw
Get-Content "...\ExcelOutputAsset\ThemeUIConfig.json" -Raw
Get-Content "...\ExcelOutputAsset\ThemeSpecialConfig.json" -Raw

# 服务端下发列表
Select-String -Path "D:\BH3\7.0\app\server-v2\Common\Common.Database\WarshipSettings.cs" -Pattern "DefaultWarshipIds"

# 客户端过滤逻辑（反编译）
python -c "..."   # bodies_merged.tsv 中 RVA 0x20184E0 / 0x181651940 / 0x181771690
```

---

## 七、一句话总结

**EVA 舰桥不是「没做」，而是「做了但被藏起来」**：客户端表 `Display=false` 让它不进选择列表，
服务端 `DefaultWarshipIds` 又没带 `400001`，主题名文本也被单独摘掉。
但 `SetWarshipReq` 不校验 id，所以从服务端强制 `WarshipId=400001` 就能绕过前两道门禁。
