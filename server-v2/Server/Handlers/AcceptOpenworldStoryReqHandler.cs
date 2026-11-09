using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

[PacketCmdId(CmdId.AcceptOpenworldStoryReq)]
internal class AcceptOpenworldStoryReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		AcceptOpenworldStoryReq decodedBody = packet.GetDecodedBody<AcceptOpenworldStoryReq>();
		AcceptOpenworldStoryRsp proto = new AcceptOpenworldStoryRsp
		{
			retcode = AcceptOpenworldStoryRsp.Retcode.Succ,
			StoryId = decodedBody.StoryId
		};
		session.Player.User.AddOWStory(decodedBody.StoryId);
		session.Send(Packet.FromProto(proto, CmdId.AcceptOpenworldStoryRsp));
		session.ProcessPacket(Packet.FromProto(new GetOpenworldStoryReq(), CmdId.GetOpenworldStoryReq));
	}
}
