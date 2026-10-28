using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ArmadaCabinType
{
	[ProtoEnum(Name = "ARMADA_CABIN_NONE")]
	ArmadaCabinNone,
	[ProtoEnum(Name = "ARMADA_CABIN_MAIN")]
	ArmadaCabinMain,
	[ProtoEnum(Name = "ARMADA_CABIN_CONTACT")]
	ArmadaCabinContact,
	[ProtoEnum(Name = "ARMADA_CABIN_HANGAR")]
	ArmadaCabinHangar,
	[ProtoEnum(Name = "ARMADA_CABIN_WORKSHOP")]
	ArmadaCabinWorkshop,
	[ProtoEnum(Name = "ARMADA_CABIN_EXCHEQUER")]
	ArmadaCabinExchequer,
	[ProtoEnum(Name = "ARMADA_CABIN_BOSS")]
	ArmadaCabinBoss
}
