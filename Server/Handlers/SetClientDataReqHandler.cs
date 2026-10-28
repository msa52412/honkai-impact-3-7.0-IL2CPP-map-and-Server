using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SetClientDataReq)]
internal class SetClientDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetClientDataReq decodedBody = packet.GetDecodedBody<SetClientDataReq>();
		Common.Database.ClientData.SetClientData(session.Player.User.Uid, decodedBody.ClientData.Type, decodedBody.ClientData.Id, decodedBody.ClientData.Data);
		session.Send(Packet.FromProto(new SetClientDataRsp
		{
			retcode = SetClientDataRsp.Retcode.Succ,
			Id = decodedBody.ClientData.Id,
			Type = decodedBody.ClientData.Type
		}, CmdId.SetClientDataRsp));
	}
}
