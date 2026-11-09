using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.AvatarArtifactLevelUpReq)]
internal class AvatarArtifactLevelUpReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		AvatarArtifactLevelUpReq Data = packet.GetDecodedBody<AvatarArtifactLevelUpReq>();
		AvatarArtifactLevelUpRsp avatarArtifactLevelUpRsp = new AvatarArtifactLevelUpRsp
		{
			retcode = AvatarArtifactLevelUpRsp.Retcode.Succ,
			AvatarId = Data.ArtifactId,
			ArtifactId = Data.ArtifactId
		};
		AvatarScheme avatarScheme = session.Player.AvatarList.Where((AvatarScheme avatar) => avatar.AvatarId == Data.AvatarId).FirstOrDefault();
		if (avatarScheme != null)
		{
			switch (avatarScheme.ArtifactLevelUp(Data.AvatarId, Data.ArtifactId))
			{
			case 1:
				avatarArtifactLevelUpRsp.retcode = AvatarArtifactLevelUpRsp.Retcode.AvatarLevelLack;
				break;
			case 2:
				avatarArtifactLevelUpRsp.retcode = AvatarArtifactLevelUpRsp.Retcode.AvatarStarLack;
				break;
			default:
				session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq
				{
					AvatarIdLists = new uint[1] { avatarScheme.AvatarId }
				}, CmdId.GetAvatarDataReq));
				break;
			}
			avatarArtifactLevelUpRsp.AvatarDetail = avatarScheme.ToDetailData(session.Player.Equipment);
		}
		session.Send(Packet.FromProto(avatarArtifactLevelUpRsp, CmdId.AvatarArtifactLevelUpRsp));
	}
}
