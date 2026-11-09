using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers.Openworld;

[PacketCmdId(CmdId.TakeOpenworldStoryRewardReq)]
internal class TakeOpenworldStoryRewardReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		TakeOpenworldStoryRewardReq Data = packet.GetDecodedBody<TakeOpenworldStoryRewardReq>();
		UserScheme.OpenWorldStoryScheme openWorldStoryScheme = session.Player.User.OpenWorldStory.FirstOrDefault((UserScheme.OpenWorldStoryScheme x) => x.StoryId == Data.StoryId);
		var storyData = BaseExcelReader<OpenWorldStoryData, OpenWorldStoryDataExcel>.GetInstance().FromId((int)Data.StoryId);
		if (storyData?.StoryMap == 1)
		{
			var rsp = CompleteSakura(session.Player, Data.StoryId, storyData);
			session.Send(Packet.FromProto(rsp, CmdId.TakeOpenworldStoryRewardRsp));
			if (rsp.RewardLists.Count > 0)
				DailyGameplay.PushInventory(session);
			session.ProcessPacket(Packet.FromProto(new GetOpenworldStoryReq(), CmdId.GetOpenworldStoryReq));
			return;
		}
		if (openWorldStoryScheme != null)
		{
			openWorldStoryScheme.IsDone = true;
		}
		if (storyData != null)
		{
			OpenWorldScheme openWorldScheme = session.Player.OpenWorlds.Where((OpenWorldScheme x) => x.MapId == storyData.StoryMap).FirstOrDefault();
			if (openWorldScheme != null && BaseExcelReader<OpenWorldStoryData, OpenWorldStoryDataExcel>.GetInstance().All.FirstOrDefault((OpenWorldStoryDataExcel story) => story.PreStory.Contains((int)Data.StoryId) && story.StoryMap == storyData.StoryMap)?.Cycle > storyData.Cycle)
			{
				openWorldScheme.Cycle = BaseExcelReader<OpenWorldCycleData, OpenWorldCycleDataExcel>.GetInstance().GetNextCycle((uint)storyData.StoryMap, (uint)storyData.Cycle);
				openWorldScheme.HasTakeFinishRewardCycle = BaseExcelReader<OpenWorldCycleData, OpenWorldCycleDataExcel>.GetInstance().GetNextCycle((uint)storyData.StoryMap, (uint)storyData.Cycle);
			}
		}
		session.Send(Packet.FromProto(new TakeOpenworldStoryRewardRsp
		{
			retcode = TakeOpenworldStoryRewardRsp.Retcode.Succ,
			StoryId = Data.StoryId
		}, CmdId.TakeOpenworldStoryRewardRsp));
		session.ProcessPacket(Packet.FromProto(new GetOpenworldStoryReq(), CmdId.GetOpenworldStoryReq));
	}

	internal static TakeOpenworldStoryRewardRsp CompleteSakura(Player player, uint storyId, OpenWorldStoryDataExcel definition)
	{
		var rsp = new TakeOpenworldStoryRewardRsp { StoryId = storyId, retcode = TakeOpenworldStoryRewardRsp.Retcode.Fail };
		lock (player.User)
		lock (player.Equipment)
		{
			var story = player.User.OpenWorldStory.FirstOrDefault(row => row.StoryId == storyId);
			if (story?.IsDone == true || OwMapTuning.AllStoryDone)
			{
				rsp.retcode = TakeOpenworldStoryRewardRsp.Retcode.Succ;
				return rsp;
			}
			if (story == null)
			{
				rsp.retcode = TakeOpenworldStoryRewardRsp.Retcode.StoryNotFound;
				return rsp;
			}
			if (definition.StoryType == 2 && story.StoryProgress < definition.MaxCount)
				return rsp;

			DailyGameplay.Plan reward;
			try
			{
				reward = definition.HuntRewardItem > 0 ? DailyGameplay.Reward(definition.HuntRewardItem) : new();
				DailyGameplay.Validate(player, reward);
			}
			catch (System.Exception error)
			{
				Packet.c.Warn("[OW] Sakura story reward: " + error.Message);
				return rsp;
			}

			var map = player.OpenWorlds?.FirstOrDefault(row => row.MapId == 1);
			uint oldCycle = map?.Cycle ?? 0;
			bool advance = map != null && map.Cycle <= definition.Cycle &&
				BaseExcelReader<OpenWorldStoryData, OpenWorldStoryDataExcel>.GetInstance().All.Any(
					row => row.StoryMap == 1 && row.Cycle > definition.Cycle &&
						(row.PreStory ?? System.Array.Empty<int>()).Contains((int)storyId));
			Common.Resources.Proto.RewardData paid = null;
			if (!DailyGameplay.Atomic(player, () =>
			{
				story.IsDone = true;
				if (advance)
				{
					map.Cycle = BaseExcelReader<OpenWorldCycleData, OpenWorldCycleDataExcel>.GetInstance()
						.GetNextCycle(1, (uint)definition.Cycle);
					map.Save();
				}
				if (definition.HuntRewardItem > 0)
					paid = DailyGameplay.Grant(player, reward);
			}))
			{
				story.IsDone = false;
				if (advance)
				{
					map.Cycle = oldCycle;
				}
				return rsp;
			}
			if (paid != null)
				rsp.RewardLists.Add(paid);
			rsp.retcode = TakeOpenworldStoryRewardRsp.Retcode.Succ;
			return rsp;
		}
	}
}
