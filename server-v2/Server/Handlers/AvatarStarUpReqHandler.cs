using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.AvatarStarUpReq)]
internal class AvatarStarUpReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		AvatarStarUpReq Data = packet.GetDecodedBody<AvatarStarUpReq>();
		AvatarStarUpRsp avatarStarUpRsp = new AvatarStarUpRsp
		{
			retcode = AvatarStarUpRsp.Retcode.Succ
		};
		AvatarScheme avatarScheme = session.Player.AvatarList.FirstOrDefault((AvatarScheme x) => x.AvatarId == Data.AvatarId);
		if (avatarScheme != null)
		{
			avatarScheme.StarUp();
			session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq
			{
				AvatarIdLists = new uint[1] { avatarScheme.AvatarId }
			}, CmdId.GetAvatarDataReq));
		}
		else
		{
			avatarStarUpRsp.retcode = AvatarStarUpRsp.Retcode.AvatarNotExist;
		}
		session.Send(Packet.FromProto(avatarStarUpRsp, CmdId.AvatarStarUpRsp));
	}
}
