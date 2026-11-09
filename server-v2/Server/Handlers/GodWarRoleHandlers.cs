using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetGodWarRoleReq)]
internal class GetGodWarRoleReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		var req = packet.GetDecodedBody<GetGodWarRoleReq>();
		session.Send(Packet.FromProto(new GetGodWarRoleRsp
		{
			retcode = req.GodWarId == 1 ? GetGodWarRoleRsp.Retcode.Succ : GetGodWarRoleRsp.Retcode.NotOpen,
			GodWarId = req.GodWarId,
			RoleInfo = req.GodWarId == 1 ? GodWarService.RoleInfo(session.Player) : null
		}, CmdId.GetGodWarRoleRsp));
	}
}

[PacketCmdId(CmdId.LevelUpGodWarRoleRelationReq)]
internal class LevelUpGodWarRoleRelationReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		var req = packet.GetDecodedBody<LevelUpGodWarRoleRelationReq>();
		var result = req.GodWarId == 1 && session.Player != null
			? GodWarService.LevelUp(session, req.RoleId) : LevelUpGodWarRoleRelationRsp.Retcode.FeatureClose;
		if(result==LevelUpGodWarRoleRelationRsp.Retcode.Succ)GodWarProgression.Push(session);
		Packet.c.Log($"[GodWar] LevelUp role={req.RoleId} result={result}");
		session.Send(Packet.FromProto(new LevelUpGodWarRoleRelationRsp
		{
			retcode = result, GodWarId = req.GodWarId
		}, CmdId.LevelUpGodWarRoleRelationRsp));
	}
}
