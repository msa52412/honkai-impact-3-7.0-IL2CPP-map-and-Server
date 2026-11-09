using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum EquipmentForgeRefreshType
{
	[ProtoEnum(Name = "EQUIPMENT_FORGE_REFRESH_NONE")]
	EquipmentForgeRefreshNone,
	[ProtoEnum(Name = "EQUIPMENT_FORGE_REFRESH_SCHEDULE")]
	EquipmentForgeRefreshSchedule,
	[ProtoEnum(Name = "EQUIPMENT_FORGE_REFRESH_DAILY")]
	EquipmentForgeRefreshDaily
}
