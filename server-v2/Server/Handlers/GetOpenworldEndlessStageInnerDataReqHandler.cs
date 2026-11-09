using System.Linq;
using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 旧深渊（迪拉克之海 / 量子奇点）「开始战斗」这一步的数据包。
///
/// 为什么必须实现
/// --------------
/// 2026-10-25 20:22 服务端日志：
///
///     20:22:00 <192.168.0.103> OpenworldStageBeginReq
///     20:22:00 <192.168.0.103> OpenworldStageBeginRsp
///     20:22:00 <192.168.0.103> GetOpenworldEndlessStageInnerDataReq not handled!
///
/// 客户端点「开始战斗」后的链路（反汇编 OWEndlessPreparePageContext）：
///
///     OnEnterBtnClick             0x173ABE0
///       -> RequestToEnterLevel    0x173C240  (发 OpenworldStageBeginReq，转圈)
///       -> OnOWEndlessStageBeginRsp 0x173B050
///            -> RequestToGetOWEndlessInnerData 0x173C3A0  (发本包 3426，继续转圈)
///                 -> OnOWEndlessStageInnerDataRsp 0x173B250
///                      if (rsp == null)                 -> 直接返回（继续转圈）
///                      if (rsp.retcode != SUCC)         -> 弹网络错误框（继续转圈）
///                      else                             -> DoBeginLevel() 0x1739BD0
///
/// DoBeginLevel 才是真正切场景的那一步：
///     areaID   = page.AreaID (+0xE8)
///     areaMeta = HJIHDABOPFD(areaID)                // OpenWorldArea 表
///     levelID  = areaMeta.get_levelID               // 3013 -> 900427
///     level    = LevelModule.TryGetLevelById(levelID)
///     LevelScoreManager.SetOpenWorldEndlessLevelBeginIntent(level, areaID, ...)
///     NewOpenWorldModule.set_CurrentAreaID(areaID)
///     OWEndlessModule.GetOWEndLessLayerInfoByAreaID(areaID, out layerInfo)
///     battleLevelInfo.currentEnterLayerIndex = layerInfo.Index
///     LevelScoreManager.OpenWorldAreaID = areaID
///     MainUIManager.PopTopPageOnly()
///     MainUIManager.MoveToNextScene()
///
/// 所以之前「转圈没反应」不是地图问题，而是这个响应从来没到，
/// 页面停在 RequestToGetOWEndlessInnerData 里等。
///
/// 响应内容
/// --------
/// battle_list 只需要覆盖本次进入的区域。客户端对它的用法（反汇编
/// OnGetOpenworldEndlessInnerDataRsp 0x189A980）只有三件事：
///     RefreshTrapList            —— 建 trap 字典
///     RefreshEffectItemList...   —— 刷新效果道具
///     RefreshSceneGearDict       —— 建 battleID → sceneGear 字典
/// 都是「按 battle_id 建表」，空列表也不会崩；这里按 OWEndlessBattleArea
/// 表把该区域的战斗都下发（battleID 全局唯一：3011→1..8, 3012→9..15,
/// 3013→16..22, 3014→23..28, 3015..3018→29..32），保持和客户端
/// GetBattleIDByAreaNameAreaID 的键一致。
/// </summary>
[PacketCmdId(CmdId.GetOpenworldEndlessStageInnerDataReq)]
internal class GetOpenworldEndlessStageInnerDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetOpenworldEndlessStageInnerDataReq req = packet.GetDecodedBody<GetOpenworldEndlessStageInnerDataReq>();
		uint areaId = req?.AreaId ?? 0u;

		GetOpenworldEndlessStageInnerDataRsp rsp = new GetOpenworldEndlessStageInnerDataRsp
		{
			retcode = GetOpenworldEndlessStageInnerDataRsp.Retcode.Succ
		};

		var battles = OWEndlessBattleArea.GetInstance().ByArea((int)areaId);
		foreach (OWEndlessBattleAreaExcel row in battles)
		{
			rsp.BattleLists.Add(new OpenworldEndlessBattle
			{
				BattleId = (uint)row.BattleID
			});
		}

		if (battles.Count == 0)
		{
			Packet.c.Warn($"[Abyss] StageInner area_id={areaId} 在 OWEndlessBattleArea 里没有战斗行，下发空 battle_list");
		}
		else
		{
			Packet.c.Log($"[Abyss] StageInner area_id={areaId} battles=[{string.Join(",", battles.Select(b => b.BattleID))}] "
				+ $"areas=[{string.Join(",", battles.Select(b => b.BattleArea))}]");
		}

		// ---- 生效中的道具 + 每个 battle 的陷阱 ----
		// 客户端 OnGetOpenworldEndlessInnerDataRsp(0x18189A980) 只做三件事：
		//   RefreshTrapList(rsp)            <- battle_list[].trap_list
		//   RefreshEffectItemListByInnerDataRsp(rsp) <- effect_item_list
		//   RefreshSceneGearDict(rsp)       <- battle_list[].scene_gear
		// 关卡内看不到陷阱 / 吃不到药水，基本就是这两块没填。
		uint itemType = AbyssConfig.SelectedEndlessType == 3u ? 3u : 2u;
		OwEndlessItemState items = OwEndlessItems.Get(session.Player.User.Uid, itemType);
		OwEndlessItems.FillInnerRsp(items, rsp, areaId);

		Packet.c.Log($"[Abyss] StageInner 道具 effect={rsp.EffectItemLists.Count} "
			+ $"traps={rsp.BattleLists.Sum(b => b.TrapLists.Count)} lastArea={items.LastAreaId}");

		session.Send(Packet.FromProto(rsp, CmdId.GetOpenworldEndlessStageInnerDataRsp));

		// 悬空陷阱可能在这一步被分配了落点，落盘一次。
		OwEndlessItemState.Persist();
	}
}
