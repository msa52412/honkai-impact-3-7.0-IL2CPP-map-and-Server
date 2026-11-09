using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ReclaimEquipmentType
{
	[ProtoEnum(Name = "RECLAIM_NONE")]
	ReclaimNone,
	[ProtoEnum(Name = "RECLAIM_AVATAR")]
	ReclaimAvatar,
	[ProtoEnum(Name = "RECLAIM_WEAPON")]
	ReclaimWeapon,
	[ProtoEnum(Name = "RECLAIM_STIGMATA")]
	ReclaimStigmata
}
