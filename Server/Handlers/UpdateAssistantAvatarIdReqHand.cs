using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.UpdateAssistantAvatarIdReq)]
public class UpdateAssistantAvatarIdReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		UpdateAssistantAvatarIdReq decodedBody = packet.GetDecodedBody<UpdateAssistantAvatarIdReq>();
		session.Player.User.AssistantAvatarId = (int)decodedBody.AvatarId;
		UpdateAssistantAvatarIdRsp proto = new UpdateAssistantAvatarIdRsp
		{
			retcode = UpdateAssistantAvatarIdRsp.Retcode.Succ
		};
		GetMainDataRsp getMainDataRsp = new GetMainDataRsp();
		getMainDataRsp.retcode = GetMainDataRsp.Retcode.Succ;
		getMainDataRsp.AssistantAvatarId = (uint)session.Player.User.AssistantAvatarId;
		getMainDataRsp.TypeLists = new uint[1] { 19u };
		GetMainDataRsp proto2 = getMainDataRsp;
		session.Send(Packet.FromProto(proto2, CmdId.GetMainDataRsp), Packet.FromProto(proto, CmdId.UpdateAssistantAvatarIdRsp));
	}
}
