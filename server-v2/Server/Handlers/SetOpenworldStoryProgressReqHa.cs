using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

[PacketCmdId(CmdId.SetOpenworldStoryProgressReq)]
internal class SetOpenworldStoryProgressReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetOpenworldStoryProgressReq Data = packet.GetDecodedBody<SetOpenworldStoryProgressReq>();
		UserScheme.OpenWorldStoryScheme openWorldStoryScheme = session.Player.User.OpenWorldStory.FirstOrDefault((UserScheme.OpenWorldStoryScheme x) => x.StoryId == Data.StoryId);
		if (openWorldStoryScheme != null)
		{
			openWorldStoryScheme.StoryProgress = Data.StoryProgress;
		}
		session.Send(Packet.FromProto(new SetOpenworldStoryProgressRsp
		{
			retcode = SetOpenworldStoryProgressRsp.Retcode.Succ,
			StoryId = Data.StoryId
		}, CmdId.SetOpenworldStoryProgressRsp));
		session.ProcessPacket(Packet.FromProto(new GetOpenworldStoryReq(), CmdId.GetOpenworldStoryReq));
	}
}
