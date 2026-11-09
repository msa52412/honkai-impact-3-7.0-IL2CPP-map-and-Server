using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CheckConditionEnum
{
	[ProtoEnum(Name = "CHECK_CONDITION_WEAPON")]
	CheckConditionWeapon = 13,
	[ProtoEnum(Name = "CHECK_CONDITION_STIGMATA_SLOT_1")]
	CheckConditionStigmataSlot1,
	[ProtoEnum(Name = "CHECK_CONDITION_STIGMATA_SLOT_2")]
	CheckConditionStigmataSlot2,
	[ProtoEnum(Name = "CHECK_CONDITION_STIGMATA_SLOT_3")]
	CheckConditionStigmataSlot3
}
