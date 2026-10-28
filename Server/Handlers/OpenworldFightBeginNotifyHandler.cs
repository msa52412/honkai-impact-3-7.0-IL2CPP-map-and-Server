using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 4540 —— 开放世界（迪拉克之海地图内）单场战斗开始。
///
/// 这是纯上报的 Notify：客户端把「在哪个 area / 哪个子区域 / 打哪些怪」告诉服务端，
/// **不期待任何回包**。以前没有 handler，日志里只有一句 "not handled!"，
/// 结果完全看不到地图内「一波怪」的节奏，排查分数问题没有抓手。
/// 现在只记日志，不改任何状态。
/// </summary>
[PacketCmdId(CmdId.OpenworldFightBeginNotify)]
internal class OpenworldFightBeginNotifyHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		OpenworldFightBeginNotify req = packet.GetDecodedBody<OpenworldFightBeginNotify>();
		Packet.c.Log($"[OW] FightBegin map={req.MapId} area={req.AreaId} "
			+ $"stage={(int)req.StageType} sub={req.SubAreaName} monster={req.MonsterName}");
	}
}
