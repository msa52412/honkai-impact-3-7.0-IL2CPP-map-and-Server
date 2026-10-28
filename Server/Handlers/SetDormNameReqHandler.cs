using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SetDormNameReq)]
internal class SetDormNameReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetDormNameReq decodedBody = packet.GetDecodedBody<SetDormNameReq>();
		SetDormNameRsp proto = new SetDormNameRsp
		{
			retcode = SetDormNameRsp.Retcode.Succ,
			CdEndTime = 0u
		};
		session.Player.Dorms.SetDormName(decodedBody.HouseId, decodedBody.Name);
		session.ProcessPacket(Packet.FromProto(new GetDormHouseReq(), CmdId.GetDormHouseReq));
		session.Send(Packet.FromProto(proto, CmdId.SetDormNameRsp));
	}
}
