using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.DressEquipmentReq)]
internal class DressEquipmentReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		DressEquipmentReq Data = packet.GetDecodedBody<DressEquipmentReq>();
		DressEquipmentRsp dressEquipmentRsp = new DressEquipmentRsp
		{
			retcode = DressEquipmentRsp.Retcode.Succ
		};
		AvatarScheme avatarScheme = session.Player.AvatarList.FirstOrDefault((AvatarScheme avatar) => avatar.AvatarId == Data.AvatarId);
		if (avatarScheme == null)
		{
			dressEquipmentRsp.retcode = DressEquipmentRsp.Retcode.AvatarNotExist;
		}
		else
		{
			switch (Data.Slot)
			{
			case EquipmentSlot.EquipmentSlotWeapon1:
				avatarScheme.WeaponUniqueId = Data.UniqueId;
				break;
			case EquipmentSlot.EquipmentSlotStigmata1:
				avatarScheme.StigmataUniqueId1 = Data.UniqueId;
				break;
			case EquipmentSlot.EquipmentSlotStigmata2:
				avatarScheme.StigmataUniqueId2 = Data.UniqueId;
				break;
			case EquipmentSlot.EquipmentSlotStigmata3:
				avatarScheme.StigmataUniqueId3 = Data.UniqueId;
				break;
			default:
				dressEquipmentRsp.retcode = DressEquipmentRsp.Retcode.EquipmentSlotError;
				break;
			}
			session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq
			{
				AvatarIdLists = new uint[1] { avatarScheme.AvatarId }
			}, CmdId.GetAvatarDataReq));
		}
		session.Send(Packet.FromProto(dressEquipmentRsp, CmdId.DressEquipmentRsp));
	}
}
