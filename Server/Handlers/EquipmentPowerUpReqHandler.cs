using System.Linq;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.EquipmentPowerUpReq)]
internal class EquipmentPowerUpReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		EquipmentPowerUpReq Data = packet.GetDecodedBody<EquipmentPowerUpReq>();
		EquipmentPowerUpRsp equipmentPowerUpRsp = new EquipmentPowerUpRsp
		{
			retcode = EquipmentPowerUpRsp.Retcode.Succ,
			MainItem = Data.MainItem,
			BoostRate = 100u
		};
		GetEquipmentDataRsp getEquipmentDataRsp = new GetEquipmentDataRsp
		{
			retcode = GetEquipmentDataRsp.Retcode.Succ,
			VitalityValue = 5900u
		};
		switch (Data.MainItem.Type)
		{
		case EquipmentType.EquipmentWeapon:
		{
			Material[] collection2 = session.Player.Equipment.AddWeaponExpByConsumeItem(Data.MainItem.IdOrUniqueId, Data.ConsumeItemList.ItemLists);
			getEquipmentDataRsp.MaterialLists.AddRange(collection2);
			getEquipmentDataRsp.WeaponLists.Add(session.Player.Equipment.WeaponList.FirstOrDefault((Weapon weapon) => weapon.UniqueId == Data.MainItem.IdOrUniqueId));
			break;
		}
		case EquipmentType.EquipmentStigmata:
		{
			Material[] collection = session.Player.Equipment.AddStigmataExpByConsumeItem(Data.MainItem.IdOrUniqueId, Data.ConsumeItemList.ItemLists);
			getEquipmentDataRsp.MaterialLists.AddRange(collection);
			getEquipmentDataRsp.StigmataLists.Add(session.Player.Equipment.StigmataList.FirstOrDefault((Stigmata stigmata) => stigmata.UniqueId == Data.MainItem.IdOrUniqueId));
			break;
		}
		default:
			equipmentPowerUpRsp.retcode = EquipmentPowerUpRsp.Retcode.MainItemNotExist;
			break;
		}
		session.Send(Packet.FromProto(getEquipmentDataRsp, CmdId.GetEquipmentDataRsp), Packet.FromProto(equipmentPowerUpRsp, CmdId.EquipmentPowerUpRsp));
	}
}
