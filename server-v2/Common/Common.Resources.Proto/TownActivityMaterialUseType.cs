using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TownActivityMaterialUseType
{
	[ProtoEnum(Name = "TOWN_ACTIVITY_MATERIAL_USE_BUILDING")]
	TownActivityMaterialUseBuilding = 1,
	[ProtoEnum(Name = "TOWN_ACTIVITY_MATERIAL_USE_AVATAR_BUFF")]
	TownActivityMaterialUseAvatarBuff,
	[ProtoEnum(Name = "TOWN_ACTIVITY_MATERIAL_USE_KILL_MONSTER")]
	TownActivityMaterialUseKillMonster,
	[ProtoEnum(Name = "TOWN_ACTIVITY_MATERIAL_USE_GLOBAL_BUFF")]
	TownActivityMaterialUseGlobalBuff
}
