using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum EquipmentForgeType
{
	[ProtoEnum(Name = "EQUIPMENT_FORGE_TIMES")]
	EquipmentForgeTimes = 1,
	[ProtoEnum(Name = "EQUIPMENT_FORGE_PACKAGE_NUM")]
	EquipmentForgePackageNum
}
