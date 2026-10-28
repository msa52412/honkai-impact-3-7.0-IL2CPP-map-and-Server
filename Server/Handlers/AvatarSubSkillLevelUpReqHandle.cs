using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.AvatarSubSkillLevelUpReq)]
internal class AvatarSubSkillLevelUpReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		AvatarSubSkillLevelUpReq Data = packet.GetDecodedBody<AvatarSubSkillLevelUpReq>();
		AvatarScheme avatarScheme = session.Player.AvatarList.FirstOrDefault((AvatarScheme avatar) => avatar.AvatarId == Data.AvatarId);
		AvatarSubSkillLevelUpRsp avatarSubSkillLevelUpRsp = new AvatarSubSkillLevelUpRsp
		{
			retcode = AvatarSubSkillLevelUpRsp.Retcode.Succ
		};
		if (avatarScheme != null)
		{
			avatarScheme.LevelUpSkill(Data.SubSkillId, Data.IsLevelUpAll);
			session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq
			{
				AvatarIdLists = new uint[1] { avatarScheme.AvatarId }
			}, CmdId.GetAvatarDataReq));
		}
		else
		{
			avatarSubSkillLevelUpRsp.retcode = AvatarSubSkillLevelUpRsp.Retcode.AvatarNotExist;
		}
		session.Send(Packet.FromProto(avatarSubSkillLevelUpRsp, CmdId.AvatarSubSkillLevelUpRsp));
	}
}
