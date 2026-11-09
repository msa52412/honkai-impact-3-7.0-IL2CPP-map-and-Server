using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.ChangePlayerCardReq)]
internal class ChangePlayerCardReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		var req = packet.GetDecodedBody<ChangePlayerCardReq>();
		bool success = session.Player != null && PlayerCardService.Change(session.Player, req);
		var rsp = new ChangePlayerCardRsp
		{
			retcode = success ? ChangePlayerCardRsp.Retcode.Succ : ChangePlayerCardRsp.Retcode.Fail,
			Type = req.Type, MsgConfig = req.MsgConfig
		};
		if (success)
		{
			rsp.SlotLists.AddRange(req.SlotLists);
			session.Send(Packet.FromProto(PlayerCardService.Build(session.Player.User), CmdId.GetPlayerCardRsp));
		}
		Packet.c.Log($"[PlayerCard] type={req.Type} slots={req.SlotLists.Count} success={success}");
		session.Send(Packet.FromProto(rsp, CmdId.ChangePlayerCardRsp));
	}
}
