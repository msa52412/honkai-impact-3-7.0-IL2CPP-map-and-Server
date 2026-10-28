using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

[PacketCmdId(CmdId.GetOpenworldStoryReq)]
internal class GetOpenworldStoryReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetOpenworldStoryRsp getOpenworldStoryRsp = new GetOpenworldStoryRsp
		{
			retcode = GetOpenworldStoryRsp.Retcode.Succ,
			FinishStoryIdLists = (from x in session.Player.User.OpenWorldStory
				where x.IsDone
				select x.StoryId).ToArray(),
			IsAll = true
		};
		getOpenworldStoryRsp.CurStoryLists.AddRange(from x in session.Player.User.OpenWorldStory
			where !x.IsDone
			select x.ToProto());
		session.Send(Packet.FromProto(getOpenworldStoryRsp, CmdId.GetOpenworldStoryRsp));
	}
}
