using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SwitchAvatarSubSkillReq)]
internal class SwitchAvatarSubSkillReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SwitchAvatarSubSkillReq Data = packet.GetDecodedBody<SwitchAvatarSubSkillReq>();
		AvatarScheme avatarScheme = session.Player.AvatarList.FirstOrDefault((AvatarScheme avatar) => avatar.AvatarId == Data.AvatarId);
		SwitchAvatarSubSkillRsp switchAvatarSubSkillRsp = new SwitchAvatarSubSkillRsp
		{
			retcode = SwitchAvatarSubSkillRsp.Retcode.Succ
		};
		if (avatarScheme != null)
		{
			avatarScheme.SwitchSubSkill(Data.SkillId, Data.SubSkillId);
			session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq
			{
				AvatarIdLists = new uint[1] { avatarScheme.AvatarId }
			}, CmdId.GetAvatarDataReq));
		}
		else
		{
			switchAvatarSubSkillRsp.retcode = SwitchAvatarSubSkillRsp.Retcode.Fail;
		}
		session.Send(Packet.FromProto(switchAvatarSubSkillRsp, CmdId.SwitchAvatarSubSkillRsp));
	}
}
