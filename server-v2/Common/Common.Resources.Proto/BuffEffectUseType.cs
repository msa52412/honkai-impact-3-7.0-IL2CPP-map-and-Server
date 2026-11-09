using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum BuffEffectUseType
{
	[ProtoEnum(Name = "BUFF_EFFECT_USE_STAGE")]
	BuffEffectUseStage = 1,
	[ProtoEnum(Name = "BUFF_EFFECT_USE_MISSION")]
	BuffEffectUseMission,
	[ProtoEnum(Name = "BUFF_EFFECT_USE_VIRTUAL_EQUIPMENT")]
	BuffEffectUseVirtualEquipment
}
