# BH3 7.0 注入载荷使用说明

2026-11-04 打包。这套是当前**实测可用**的配置（改 .wmv 含改文件大小都不再触发下载）。

## 文件清单

| 文件 | 作用 |
|---|---|
| `version.dll` | 侧加载垫片。放在游戏根目录（和 BH3.exe 同级），系统加载 version.dll 时转发到真正的系统 DLL，然后读取 `libraries.txt` 逐个 LoadLibrary 注入 |
| `libraries.txt` | 注入清单，一行一个 DLL 文件名（相对于游戏根目录） |
| `bh3bypass.dll` | 主载荷，6 处资源校验 + 模式入口（见下） |
| `bh3bypass.ini` | 模式入口配置（卡片 ID / 迪拉克 or 量子 / 延迟），改完重启生效 |
| `bh3bypass.c` | 源码（zig cc 编译） |
| `verhook_ctl.py` | 安装/卸载工具 |
| `bh3bypass.log` | 最近一次运行的补丁落地日志（参考） |

## 安装

把 `version.dll`、`libraries.txt`、`bh3bypass.dll` 三个文件复制到**游戏根目录**
（与 `BH3.exe` 同级），启动游戏即可。

或者用工具：

```bat
python verhook_ctl.py install --libs bh3bypass.dll
```

## 卸载

删掉游戏根目录的 `version.dll`、`libraries.txt`，以及 `libraries.txt` 里列出的 DLL。

```bat
python verhook_ctl.py uninstall
```

## 版本

**2026-11-07 v16（当前）**：在精简版基础上**新增模式入口常驻化**，其余不变。

以前这块靠 `app/rehook.bat` 现场装：先 arm 信箱 → hook `SetupAllEntryButton`
抓管理器 → 扫三个页签找 feature 109 的卡片 → 把 `ExcelMeta` 指针写死进 stub
→ 改 `DoClickAction` 入口。每次开游戏都要重跑，面板重建/换图后指针失效、
表现为「点了没反应」。

现在 DLL 自己接管，**启动即生效，不用再跑 bat**：

- 不信箱、不预存指针、不扫列表。点击那一刻 `rcx` 就是容器对象：
  `[rcx+0xC8] → ContainerData`，`[cd+0x18] → ExcelMeta`，`[meta+0x20] → ID`，
  三步内存读判定，**stub 里不调任何托管方法**（所以不会抛异常）。
- 判据用配置行 ID（跨进程稳定），不用指针（重启就变），也不用 FeatureType
  （那是位图，且得走托管调用）。
- `GoToOWEndless` 不在游戏线程里硬塞：stub 只把**栈上的返回地址**换成
  trampoline，等 `DoClickAction` 完整跑完 ret 回来，才在同一线程里执行。
  等价于原来「先让原逻辑跑完、再异步补一刀」，但没有跨线程调 il2cpp 的风险。

配置见 `bh3bypass.ini`：`card_id`（默认 11）/ `mode`（0 迪拉克，1 量子奇点）/
`unlock`（entry 级锁补丁）/ `delay_ms`（默认 100）。

### ⚠ 不要再跑 rehook.bat / rehook_qt.bat

两者抢同一个 `DoClickAction` 入口，**后装的覆盖先装的**。DLL 上线后再跑 bat，
python 会把自己的 stub 写进入口，DLL 那套就失效了（不会报错，只是静默失效）。
要回退到 python 方案，先把 `bh3bypass.ini` 里 `enabled=0` 再重启，然后跑 bat。

**2026-11-07 精简版**：v13~v15 期间实验的「迪拉克之海额外得分 / 倒计时」补丁
（`owboss` / `ebtime` / `begin` / `smode` / `sle` / `dae`）**已全部移除**。
那条线走不通——`StartAutoChangeLevelExtraScore` 的真实调用方在 Lua，C# 侧零调用点，
hook 只能改参数、改不了「没人调」这个事实。完整历史源码见 `bh3bypass.full-20261107.c`。

额外得分/计时器这类需求走运行时脚本（`rea/_rea/extra_score.py`），不再进 DLL。

## 补丁清单（资源校验 6 处 + 模式入口 2 处）

| # | RVA | 函数 | 补丁 | 作用 |
|---|---|---|---|---|
| 1 | `0x02FAD6D0` | NeedRefreshMemoryMappedData | `33 C0 C3` | 永不重建 asb.dat（改 asb.dat 会被采纳） |
| 2 | `0x02FADA80` | NeedToVerifyBlocks | `33 C0 C3` | 跳过块校验循环 |
| 3 | `0x02FAD520` | NeedDownloadPatchX | `32 C0 C3` | 补丁块不触发下载 |
| 4 | `0x02753570` | NeedShowDownloadResourceDialog | `32 C0 C3` | **压掉「要求下载」弹窗** |
| 5 | `0x02FAD470` | NeedDownloadAsbX | `32 C0 C3` | ASB 不触发下载 |
| 6 | `0x01F1A600` | OnCheckStreamingAssetBundleComplete | mov dl,1 stub | 块校验结果恒真 |
| 7 | `0x00A52332` | LJMINEONAFF 循环分支 | `EB 0A 90 90 90` | 忽略 entry 级锁（原来 rehook 每次都会打） |
| 8 | `0x02CFB880` | MonoWorldMapEntryContainerBase.DoClickAction | `jmp [rip]` → stub | 模式入口卡片点击接管 |
| 9 | `0x0302A4F0` | UIUtil.IsAvatartDoNotShow | `jmp [rip]` → stub | 刻晴（avatarID 2001）不再被隐藏（2026-11-08 新增） |
| 10 | `0x014A0A74` | InitNewStorageDialog 角色卡短跳 | `75 4B` → `90 90` | 抽到的新角色卡也弹「新物品」 |
| 11 | `0x0279F6B1` | DropNewItemDialogContext.Create | `0F 85 ..` → 6×NOP | 同上，第二道闸（必须成对） |
| 12 | `0x014A1E05` / `0x014A1E73` | OnScrollChange 两处 alpha=0 的 call | 5×NOP | 结算页十格逐格弹出可见 |
| 13 | `0x014A2185` | PlayVFXSequence 稀有判定 | `E8 ..` → `B0 01` | 角色卡彩光恒点亮 |
| 14 | `0x0528B1A7` | DoGachaSequence 流程选择 | `75 23` → `EB 23` | 强制 3D 开箱（**默认关**） |
| 15 | `0x03DF6510` | EquipmentSkill registry `ContainsKey` | `jmp [rip]` → probe stub | 刻晴武器技能探针（**只观测，行为不变**，2026-11-08 新增） |

### 第 9 项：刻晴可见（`[keqing]` 段）

原来靠 `app\tools\keqing_visible.py` 现场打（`apply` / `wait`），现在 DLL 启动即生效：

- 入口 `0x0302A4F0` 起 **14 字节**正好到 `call 0x035D7F80` 之前，指令边界对齐，整段覆盖成 `FF 25` 绝对跳转。
- stub 只做 `[rcx+0x1C] == avatar_id` 判定（`AvatarDataItem.avatarID`）：
  命中 `xor eax,eax; ret`，未命中跑完被覆盖的 14 字节再跳回 `函数+14`。
- 该函数尾部是 `jmp IsCollaborationAvatarDoNotShow`（尾调用），所以提前返回 false
  会一次绕过三条判据：按 classID 隐藏、按 avatarID 隐藏、协作角色清单。
- 只有 `avatar_id` 指定角色被放行，其它角色（含 316 / 901 等联动角色）行为完全不变。
- 回跳地址必须是**运行时** `ua+RVA+14`；写静态 VA 会 `0xc0000005`（10-27 踩过）。

ini 配置：

```ini
[keqing]
visible=1      ; 0 = 关闭
avatar_id=2001 ; 刻晴
```

### 第 10~14 项：抽卡补丁（`[gacha]` 段）

原来靠两个 python 脚本现场打（`gacha_popup_patch.bat` / `gacha_settle_fix.bat`），
现在随 DLL 启动生效。**六处都是纯字节改写，没有 stub、不碰托管对象**，是最安全的一类固化。

| ini 键 | 位点 | 做什么 |
|---|---|---|
| `popup=1` | A `0x014A0A74` + B `0x0279F6B1` | 抽到的新角色卡也弹「新物品」演出 |
| `settle=1` | D `0x014A1E05` + E `0x014A1E73` | 结算页十格逐格弹出（不是最后一起蹦出来） |
| `glow=1` | F `0x014A2185` | 角色卡外面那圈彩光点亮 |
| `newbox=0` | C `0x0528B1A7` | 强制 3D 开箱流程（默认关，可能黑屏） |

两个细节：

- **popup 的 A/B 必须成对**。A 只是跳过「角色卡不弹」的分支，B 是
  `Create` 里 `if (itemData is AvatarCardDataItem) return null`。只补 A 什么都不会发生。
- **settle 的 alpha 判定**：这两处 call 把格子 CanvasGroup.alpha 打成 0，
  真正恢复成 1 的只有「全部动画结束」那一刻。所以 NOP 掉之后格子在动画期间可见，
  缩放/位移动画才看得见；不补的话动画其实一直在播，只是肉眼看不到过程。

⚠ 一键启动（`launch_all.py`）第 9 步现在**不再跑这三个 bat**，
`PATCHES` 列表已清空。要回退 python 方案：把 bat 名加回 `PATCHES`，
并在 `bh3bypass.ini` 里把对应开关置 0（否则两边抢同一位点，DLL 先占、python 会跳过）。

### `rehook.bat` 与一键启动

`launch_all.py` 第 9 步原来还会跑 `rehook.bat arm` 装点击 hook。DLL v16 之后
`DoClickAction` 已由 DLL 接管，再跑 bat 会**覆盖 DLL 的 stub**（不报错，只是静默失效）。
所以现在默认跳过，需要回退 python 方案时用 `一键启动.vbs --rehook`。

⚠ 只解决「角色可见」。刻晴的武器效果 / 主动技能（原生剑气入口）**不在这里**，
仍是 `app\恢复刻晴原生剑气入口.vbs` 那套实验性运行时补丁，需要构造 IL2CPP 托管对象，
尚未固化（见文末「待办」）。

## ★ 绝对不要加的补丁

**`GetDownloadBlockSize` (RVA `0x2753210`) —— 禁止打补丁。**

恒返回 0 会让 `DownloadResources` 走 `==0` 分支**跳过下载管理器初始化**
（`[this+0xB8..0xD8]` 五个管理器字段、`[0xE8]/[0xEA]` 就绪标志全部保持 null/0），
进主场景后某系统对空管理器做虚调用 → unityplayer `call [rax+0x48]` AV **崩溃**。
（2026-11-04 07:30 实测踩过。）

弹窗压制由 #4 独立完成，块大小保持真实值让初始化分支正常走——两者解耦。

## 验证方法

启动游戏看 `bh3bypass.log`，资源校验 6 条 + 模式入口 3 条应该都是 `OK`：

```
[entry] cfg ...\bh3bypass.ini  enabled=1 card_id=11 mode=0 unlock=1 delay_ms=100
[norefresh] OK  恒返回 false
[needverify] OK  恒返回 false
[needdlpatch] OK  恒返回 false
[needshowdlg] OK  恒返回 false
[needdlasb] OK  恒返回 false
[oncheck] OK  success 恒为 true
[unlock] OK  忽略 entry 级锁
[entry] stub @ ...  trampoline @ ...
[entry] OK  入口已接管（stub @ ...，... 字节，判据 card_id=11）
=== bh3bypass done（资源校验 6 处 + 模式入口）===
```

进游戏后点一下挑战面板那张卡，日志里会多出：

```
[entry] 命中！card_id=11 meta=0x...
[entry] GoToOWEndless(0x...) 已调用
```

**如果点了没反应、日志里也没有「命中」**，说明 `card_id` 不对。看这一行之前
先确认 stub 每次点击都会更新 `last_id`（日志里 `[entry] 点击 id=NN`）。
把那个 NN 填进 `bh3bypass.ini` 的 `card_id`，重启即可。换过入口图标的话
十有八九就是这种情况。

正常进大厅的标准：`output_log.txt` 里出现 `PreloadUIViews`。

## 已知限制

- 这套补丁只解决**资源校验**（能不能改包）。改完的包能不能被正确解析是另一件事——
  7.6 的 Mesh 顶点数据格式与 7.0 不兼容（见项目笔记 2026-11-04）。
- 卡在登录界面不动通常不是补丁问题，是 SDK 在等你点「登录」。

## 刻晴武器技能（Katana_M43）：阶段 1 探针已完成，注入待做

武器 `24241~24244` = `Entities/Weapon/Katana_M43`（刻晴专属太刀），三个技能全挂在这把武器上：

| skill | WeaponData 字段 | 用途 | 注册表里 |
|---|---|---|---|
| 1160 | `IAADPOLPIPG` | 主动（图标 `Skill_Mei_Weapon_43`） | **无烘焙记录** |
| 2211 | `OMLIBFNIJLO` | 被动 1 | **无烘焙记录** |
| 2212 | `ALOGJPEEDMB` | 被动 2 | **无烘焙记录** |

index 共 1758 条，同区间 `1159/1161/1162/1163/1166/1168~1170` 都在，缺的只有
`1158/1160/1164/1165/1167/2211/2212` —— **零散裁剪，不是整段剔除**。
所以「武器技能不生效」是查表查不到，不是数值算错；改配置表没用。

### 阶段 1（已实现，`[keqing_skill] probe=1`）

`Contains@0x03DF6510` 入口挂一个只读探针 stub：

```
+0x00  mov [rip+lastkey],edx ; lock inc [rip+count] ; jmp orig
+0x20  cmp edx,1160 / jne ; cmp edx,2211 / jne ; cmp edx,2212 / jne orig
       （命中 → jmp hit）
+0x60  sub rsp,0x28 ; mov rcx,[rcx+0x20] ; test rcx,rcx ; je null
       xor r8d,r8d ; jmp ua+0x3DF6520
+0x82  null: jmp ua+0x3DF6529
```

**入口 14 字节是跨指令的**：`+0x0B` 的 `74 0C` 之后，`+0x0D` 才是 `45 33 C0`(xor r8d,r8d)，
14 字节只吃到 `0x45`。所以 stub 必须重放这 4 条语义再跳 `+0x20`(add rsp,0x28)，
不能直接跳 `+14` —— 那会落在 `33 C0` 这条半截指令上。

探针只写两个计数器（`page+0x800` count、`page+0x804` lastkey），不改变任何返回值。
配套线程每 5 秒看一次 key 变化、每分钟读一次注册表条数，写进 `bh3bypass.log`：

```
[kqskill] 首次查询到 key=1160（累计 3 次）
[kqskill] Contains 累计 57 次  registry index=1758 cache=0  <- cache 还是空的
```

`cache` 从 0 变非 0 的那一秒，就是安全的注入时机（说明武器面板解码完成）。

### 阶段 2（未实现，`[keqing_skill] inject=0`）

关键改动是**把 stub 槽位化**，而不是沿用脚本的「立即数嵌对象」：

```
mov rax,[rip+slot] ; test rax,rax ; jz orig ; ... ret
```

槽初值 0 → 行为与未打补丁完全一致；对象造好再写槽，Contains/Get 同时生效。
这样「装 hook」与「造对象」解耦，才有懒加载的余地 —— 脚本版必须先生成对象
才能算 stub，所以只能一次次手动跑。

注入动作（全部在游戏主线程、直接调函数指针，**不需要 `.icall.json` 邮箱**）：

1. `PackedName(packed, NOOP_HASH)` → `DLC_Common_Empty` 的 name
2. `override = [ua+0x9B52470]`（"Default"）
3. `LoadAbility(name, override)` → 空 ability，并校验各动作/修饰器数组都为空
4. allocator stub：`objectNew(klass)` → ctor → 写 `+0x10` name / `+0x18` override /
   `+0x20 = 1,0,1` / `+0x68 = skillId` → GC handle 保活
5. 三个 ID 各造一个对象（`+0x68` 不同），写进各自槽

`klass` 取自 registry cache 里任一已解码条目的对象头（`[obj+0x00]`），
所以**必须先有 cache** —— 这就是阶段 1 要观测 `cache` 的原因。

### 两个已知约束

- **`Get@0x3DF6530` 的 r8 是必需的**：`mov rax,[r8]` / `mov r8,[rax+0x90]`。
  从外部进程用 2 参数调用会直接崩（11-02 实测 `RIP=base+0x3DF653E`）。
  DLL 版在命中分支直接返回、不碰 r8，所以不受影响；但**不要**用邮箱去验证 Get。
- **两个被动空 ability 不产生伤害**。1160 靠空壳就能让输入槽非空（剑气逻辑在角色自身代码里），
  2211/2212 是效果本身，空壳只能让"有这个技能"，不会真的加伤害/触发 buff。
  要真恢复被动，得先找到数据源（7.6 / 9.1 客户端的烘焙记录）。

