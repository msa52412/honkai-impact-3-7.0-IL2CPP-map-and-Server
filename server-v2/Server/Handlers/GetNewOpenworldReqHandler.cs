using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers.Openworld;

[PacketCmdId(CmdId.GetNewOpenworldReq)]
internal class GetNewOpenworldReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint now = (uint)Global.GetUnixInSeconds();

		// 地图清单 = 存档里有的 + 调参追加的。
		// 原来只发 session.Player.OpenWorlds（= ShowMapList ∩ 存档里已建行），
		// 客户端表里有、存档里没建行的图就缺一条，客户端拿不到 status 时
		// 按 0 = NONE 渲染，看起来就是「未激活」。
		List<OpenWorldScheme> schemes = session.Player.OpenWorlds.ToList();
		foreach (uint extraId in OwMapTuning.ExtraMapIds)
		{
			if (schemes.Any(x => x.MapId == extraId))
			{
				continue;
			}
			schemes.Add(new OpenWorldScheme
			{
				OwnerUid = session.Player.User.Uid,
				MapId = extraId,
				Cycle = BaseExcelReader<OpenWorldCycleData, OpenWorldCycleDataExcel>.GetInstance().GetInitCycle(extraId),
				QuestLevel = 1u,
				HasTakeFinishRewardCycle = 0u,
				SpawnPoint = ""
			});
		}

		uint status = OwMapTuning.Status;

		// 周期完成奖励：客户端拿 MapLists[].Cycle 和 HasTakeFinishRewardCycle 比较，
		// 不相等就弹「奖励领取」并自动发 TakeOpenworldCycleFinishRewardReq。
		// 服务端 Cycle 是 GetInitCycle（第一档），而 all_story_done=true 让客户端
		// 认为周期已到顶，两边差一截 → 每次打开都要补领一轮。这里拉到配置最大档。
		foreach (OpenWorldScheme ow in schemes)
		{
			OwCycleReward.Apply(ow);
		}

		GetNewOpenworldRsp rsp = new GetNewOpenworldRsp
		{
			retcode = GetNewOpenworldRsp.Retcode.Succ,
			DataType = OpenworldDataType.OpenworldDataAll,
			// 冒险等级：原来写死 1/0，客户端 GetCurrentQuestLevel 直接读这个值显示，
			// 所以界面永远是 Lv.1。现在按调参（run\openworld.json 的 quest_level，
			// 缺省 30 = OpenWorldQuestLevelData 满级），星数同步抬到满级所需（141）。
			QuestLevel = OwQuestService.TargetLevel,
			QuestStar = OwQuestService.TargetStar,
			MaxQuestLevel = OwQuestService.MaxLevel,
			NextRefreshTime = now + OwMapTuning.NextRefreshSeconds,
			CloseTime = now + OwMapTuning.CloseSeconds,
			GlobalRandomSeed = Global.GetRandomSeed()
		};
		rsp.Techs.AddRange(OpenWorld.ShowMapList.Select(map => SakuraServant.Snapshot(session.Player.User, map,
			schemes.FirstOrDefault(scheme => scheme.MapId == map))));
		rsp.MapLists.AddRange(schemes.Select((OpenWorldScheme ow) => new OpenworldMapBriefData
		{
			MapId = ow.MapId,
			Status = status,
			Cycle = ow.Cycle,
			QuestLevel = OwQuestService.TargetLevel,
			HasTakeFinishRewardCycle = ow.HasTakeFinishRewardCycle
		}));
		Packet.c.Log("[OW] GetNewOpenworld -> mapLists="
			+ string.Join(",", rsp.MapLists.Select((OpenworldMapBriefData x) => x.MapId + ":" + x.Status)));
		session.Send(Packet.FromProto(rsp, CmdId.GetNewOpenworldRsp));
	}
}
