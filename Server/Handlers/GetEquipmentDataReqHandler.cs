using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetEquipmentDataReq)]
internal class GetEquipmentDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		EquipmentScheme equipment = session.Player.Equipment;
		GetEquipmentDataRsp getEquipmentDataRsp = new GetEquipmentDataRsp
		{
			retcode = GetEquipmentDataRsp.Retcode.Succ,
			VitalityValue = 0u,
			IsAll = true
		};
		getEquipmentDataRsp.MaterialLists.AddRange(equipment.MaterialList.ToList());
		getEquipmentDataRsp.MechaLists.AddRange(equipment.MechaList.ToList());
		getEquipmentDataRsp.StigmataLists.AddRange(equipment.StigmataList.ToList());
		getEquipmentDataRsp.WeaponLists.AddRange(equipment.WeaponList.ToList());
		session.Send(Packet.FromProto(getEquipmentDataRsp, CmdId.GetEquipmentDataRsp));
	}
}
