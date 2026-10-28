using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetElfDataReq)]
internal class GetElfDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetElfDataRsp getElfDataRsp = new GetElfDataRsp
		{
			retcode = GetElfDataRsp.Retcode.Succ
		};
		IEnumerable<Elf> collection = session.Player.Elfs.Elves.Select(delegate(ElfsScheme.Elf elf)
		{
			Elf elf2 = new Elf
			{
				ElfId = elf.ElfId,
				Level = elf.Level,
				Exp = elf.Exp,
				Star = elf.Star,
				CompensateLevel = elf.CompensateLevel,
				TotalCompensateExp = elf.TotalCompensateExp,
				EquipTalentIdLists = elf.EquipTalentIdLists
			};
			elf2.SkillLists.AddRange(elf.SkillLists);
			return elf2;
		});
		getElfDataRsp.IsTakeCompensation = false;
		getElfDataRsp.ElfLists.AddRange(collection);
		session.Send(Packet.FromProto(getElfDataRsp, CmdId.GetElfDataRsp));
	}
}
