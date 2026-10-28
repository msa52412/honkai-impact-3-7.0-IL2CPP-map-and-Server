using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.ElfSkillLevelUpReq)]
internal class ElfSkillLevelUpReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		ElfSkillLevelUpReq decodedBody = packet.GetDecodedBody<ElfSkillLevelUpReq>();
		ElfSkillLevelUpRsp elfSkillLevelUpRsp = new ElfSkillLevelUpRsp
		{
			retcode = ElfSkillLevelUpRsp.Retcode.Succ
		};
		elfSkillLevelUpRsp.ElfSkill = session.Player.Elfs.SkillLevelUp(decodedBody.ElfId, decodedBody.ElfSkillId);
		if (elfSkillLevelUpRsp.ElfSkill != null)
		{
			session.ProcessPacket(Packet.FromProto(new GetElfDataReq(), CmdId.GetElfDataReq));
		}
		session.Send(Packet.FromProto(elfSkillLevelUpRsp, CmdId.ElfSkillLevelUpRsp));
	}
}
