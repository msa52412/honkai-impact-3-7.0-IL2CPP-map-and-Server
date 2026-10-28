using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

[PacketCmdId(CmdId.GetNewOpenworldReq)]
internal class GetNewOpenworldReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetNewOpenworldRsp getNewOpenworldRsp = new GetNewOpenworldRsp
		{
			retcode = GetNewOpenworldRsp.Retcode.Succ,
			DataType = OpenworldDataType.OpenworldDataAll,
			QuestLevel = 1u,
			QuestStar = 0u,
			MaxQuestLevel = 1u,
			NextRefreshTime = (uint)((int)Global.GetUnixInSeconds() + 259200),
			CloseTime = (uint)((int)Global.GetUnixInSeconds() + 259200),
			GlobalRandomSeed = Global.GetRandomSeed()
		};
		getNewOpenworldRsp.Techs.AddRange(OpenWorld.ShowMapList.Select((uint x) => new OpenworldTechData
		{
			MapId = x
		}));
		getNewOpenworldRsp.MapLists.AddRange(session.Player.OpenWorlds.Select((OpenWorldScheme ow) => new OpenworldMapBriefData
		{
			MapId = ow.MapId,
			Status = 3u,
			Cycle = ow.Cycle,
			QuestLevel = ow.QuestLevel,
			HasTakeFinishRewardCycle = ow.HasTakeFinishRewardCycle
		}));
		Packet.c.Log("[Abyss] GetNewOpenworld -> mapLists="
			+ string.Join(",", getNewOpenworldRsp.MapLists.Select((OpenworldMapBriefData x) => x.MapId)));
		session.Send(Packet.FromProto(getNewOpenworldRsp, CmdId.GetNewOpenworldRsp));
	}
}
