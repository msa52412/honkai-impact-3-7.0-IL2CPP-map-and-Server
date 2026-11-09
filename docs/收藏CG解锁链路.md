# 收藏 → 影像（CG）解锁链路

> 结论先说：收藏里 CG 那一栏的解锁，服务端**唯一**入口是 `GetFinishGuideDataRsp.guide_id_list`。
> 之前没有这个 handler（日志里一直是 `GetFinishGuideDataReq not handled!`），
> 所以 CG 一栏永远 0%/全锁、点进去提示「通关xx解锁」。
> 补齐 handler、把 CgID 区间整段塞进 `guide_id_list` 之后，CG 直接 433/433 = 100%。

## 一、反编译依据（IL2CPP 指令级）

方法体在 `BH3_Data/Native/UserAssembly.dll`（VA 见 `rea/_rea/src/MoleMole.cs`），
用 capstone 直接反汇编就能读，工具在 `tools/_cgdis.py`、`tools/_d.py`、`tools/_callers.py`。

| 环节 | 函数 | 行为 |
|---|---|---|
| 格子锁定态 | `CgDataItem.FNKCPONDMHO()` `0x1816C7F80` | `UnlockType==Level` 时 = `!MonoCgModule.IENAKGAAPCK(cgId)` |
| 是否解锁 | `MonoCgModule.IENAKGAAPCK(int)` `0x181400D40` | `HashSet<int>(模块+0x48).Contains(cgId)` |
| 唯一服务端入口 | `IOJLPHAEDLN(GetFinishGuideDataRsp)` `0x181400E30` | 遍历 `guide_id_list`，逐个 `HADMDPGEICA(id)` |
| 入库 | `HADMDPGEICA(int)` `0x1814007F0` | `if (JCEJLKHPNOL(id)) { set.Add(id); FireNotify(0x2D8); }` |
| 其余填充点 | `PlayVideo` / `OnCgFinishedCallback` / `MarkCGIDFinished` | 全是客户端本地播片回调，服务端插不上手 |

也就是说：`guide_id_list` 里放的不是「引导步骤 id」，而是**被当成「已解锁 CG」的 CgID**。
名字叫 guide 只是历史包袱。

## 二、id 空间

服务端这份表（6.6 导出，`run/Resources/ExcelOutputAsset/CgData.json`）只有 1718 行：
`40001-48016`（1592）+ `50001-50126`（125）+ `80001`。
7.0 客户端表更大（约 2000+，实测认到 `50215` 以后），所以**按区间补**，不按配置表发：

```
40001-61000   →  21001 个 id  ≈ 82KB（protobuf repeated uint）
```

运行期可调：`server-v2/run/guide_cgpad.txt` 写 `40001-61000`，改完重新登录即生效（不用重编译）。
客户端会忽略自己表里没有的 id，所以区间给宽一点只有带宽代价。

## 三、两套「CG 集合」，别混淆

| 用途 | 数据来源 | 谁消费 |
|---|---|---|
| 收藏**总览卡片**上的 CG 百分比 | `GetHasGotItemIdListRsp.item_id_list` ∩ 客户端 CgData | `JKABNPDAGEE`（收藏模块） |
| 影像页**每个格子的锁** + 页头 `433/433` | `GetFinishGuideDataRsp.guide_id_list` | `ODNDNLLOJBP`（CG 模块） |

两条都要给，否则会出现「总览百分比涨了但格子还锁着」。
`GetHasGotItemIdListReqHandler` 里那段 `50001-60300` 的 CG 补洞就是为第一条留的。

## 四、验证方法（不用问人）

客户端是本机，日志和截图都能直接看：

- 服务端：`_launch/logs/v2_Server.log` 里搜 `FinishGuide(影像解锁)` / `has_got_items`
- 客户端：`%LocalAppData%Low\miHoYo\Honkai Impact 3\output_log.txt`
- 客户端还会定期用 `ClientReport` 把 `cg_list=...`（它自己认为已解锁的 CG）上报，
  服务端日志里搜 `cg_list` 就能看到解锁集合长什么样
- 截图：`ffmpeg -f gdigrab -i title="Honkai Impact 3" -frames:v 1 out.png`

## 五、仍然锁着的两栏

`女武神`、`活动相册` 在总览里显示「敬请期待」，这是客户端 `WikiType.json` 里 `Lock=1` 的**面板开关**，
不是数据缺失，has_got 补多少 id 都绕不过去（需要改客户端配置）。

## 六、改过的文件

- 新增 `server-v2/Server/Handlers/GetFinishGuideDataReqHandler.cs`（CmdId 127/128）
- 改 `server-v2/Server/Handlers/GetHasGotItemIdListReqHandler.cs`
  （CG 只发 ≥50001 的配置 id；补洞区间 50001-60300，缓存按 `collection_cgpad.txt` 内容失效）
