using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers.Openworld;

[PacketCmdId(CmdId.TakeOpenworldStoryRewardReq)]
internal class TakeOpenworldStoryRewardReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		TakeOpenworldStoryRewardReq Data = packet.GetDecodedBody<TakeOpenworldStoryRewardReq>();
		UserScheme.OpenWorldStoryScheme openWorldStoryScheme = session.Player.User.OpenWorldStory.FirstOrDefault((UserScheme.OpenWorldStoryScheme x) => x.StoryId == Data.StoryId);
		if (openWorldStoryScheme != null)
		{
			openWorldStoryScheme.IsDone = true;
		}
		OpenWorldStoryDataExcel storyData = BaseExcelReader<OpenWorldStoryData, OpenWorldStoryDataExcel>.GetInstance().FromId((int)Data.StoryId);
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
}
