using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.AvatarArtifactUnlockReq)]
internal class AvatarArtifactUnlockReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		AvatarArtifactUnlockReq Data = packet.GetDecodedBody<AvatarArtifactUnlockReq>();
		AvatarArtifactUnlockRsp avatarArtifactUnlockRsp = new AvatarArtifactUnlockRsp
		{
			retcode = AvatarArtifactUnlockRsp.Retcode.Succ,
			AvatarId = Data.AvatarId,
			ArtifactId = Data.ArtifactId
		};
		AvatarScheme avatarScheme = session.Player.AvatarList.Where((AvatarScheme avatar) => avatar.AvatarId == Data.AvatarId).FirstOrDefault();
		if (avatarScheme != null)
		{
			int num = avatarScheme.ArtifactUnlock(Data.AvatarId, Data.ArtifactId);
			if (num == 1)
			{
				avatarArtifactUnlockRsp.retcode = AvatarArtifactUnlockRsp.Retcode.AvatarLevelLack;
			}
			else
			{
				session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq
				{
					AvatarIdLists = new uint[1] { avatarScheme.AvatarId }
				}, CmdId.GetAvatarDataReq));
			}
		}
		session.Send(Packet.FromProto(avatarArtifactUnlockRsp, CmdId.AvatarArtifactUnlockRsp));
	}
}
