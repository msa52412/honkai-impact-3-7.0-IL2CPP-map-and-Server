using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 4541 —— 开放世界（迪拉克之海地图内）单场战斗结束。
///
/// 同样是纯上报的 Notify，不回包。它的价值在于：
/// 迪拉克地图里一个怪物组由多「波」怪组成，每波结束客户端发一次这个包，
/// 整组清完才发 3440 BattleEnd + 3424 FinishGroup。
/// 之前没 handler，分不清「玩家只清了一波」还是「整组清完了」，
/// 也就无法判断分数该不该涨。现在留日志，可以直接对上。
/// </summary>
[PacketCmdId(CmdId.OpenworldFightEndNotify)]
internal class OpenworldFightEndNotifyHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		OpenworldFightEndNotify req = packet.GetDecodedBody<OpenworldFightEndNotify>();
		Packet.c.Log($"[OW] FightEnd map={req.MapId} area={req.AreaId} "
			+ $"stage={(int)req.StageType} sub={req.SubAreaName} "
			+ $"status={(int)req.FightEndStatus} revive={req.ReviveTimes} "
			+ $"cost={req.CostTime} kill={req.MonsterKillInfo}");
	}
}
