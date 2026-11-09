using PemukulPaku.GameServer.Game;
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
		// 单机版材料锁定：补给卡 1101~1115 锁 999、普通晶体核心(8101) 锁 9999
		GmLocks.Materials(session.Player);
		getEquipmentDataRsp.MaterialLists.AddRange((equipment.MaterialList??System.Array.Empty<Material>()).ToList());
		getEquipmentDataRsp.MechaLists.AddRange((equipment.MechaList??System.Array.Empty<Mecha>()).ToList());
		getEquipmentDataRsp.StigmataLists.AddRange((equipment.StigmataList??System.Array.Empty<Stigmata>()).ToList());
		getEquipmentDataRsp.WeaponLists.AddRange((equipment.WeaponList??System.Array.Empty<Weapon>()).ToList());
        GodWarProgression.ApplyDeepCurrency(session.Player,getEquipmentDataRsp);
		session.Send(Packet.FromProto(getEquipmentDataRsp, CmdId.GetEquipmentDataRsp));
	}
}
