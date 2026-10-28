using System;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetGalInteractTriggerEventReq)]
internal class GetGalInteractTriggerEventReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetGalInteractTriggerEventReq decodedBody = packet.GetDecodedBody<GetGalInteractTriggerEventReq>();
		Random random = new Random();
		GetGalInteractTriggerEventRsp proto = new GetGalInteractTriggerEventRsp
		{
			retcode = GetGalInteractTriggerEventRsp.Retcode.Succ,
			AvatarId = decodedBody.AvatarId,
			EventId = decodedBody.EventIdLists[random.Next(0, decodedBody.EventIdLists.Length - 1)]
		};
		session.Send(Packet.FromProto(proto, CmdId.GetGalInteractTriggerEventRsp));
	}
}
