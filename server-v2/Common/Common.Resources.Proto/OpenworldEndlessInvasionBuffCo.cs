using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldEndlessInvasionBuffConditionType
{
	[ProtoEnum(Name = "INVASION_BUFF_CONDITION_TOTAL_LEVEL")]
	InvasionBuffConditionTotalLevel = 1,
	[ProtoEnum(Name = "INVASION_BUFF_CONDITION_NUM_LEVEL")]
	InvasionBuffConditionNumLevel,
	[ProtoEnum(Name = "INVASION_BUFF_CONDITION_NUM_LEVEL_ATTRIBUTE")]
	InvasionBuffConditionNumLevelAttribute,
	[ProtoEnum(Name = "INVASION_BUFF_CONDITION_NUM_LEVEL_ALL_ATTRIBUTE")]
	InvasionBuffConditionNumLevelAllAttribute,
	[ProtoEnum(Name = "INVASION_BUFF_CONDITION_NUM_CLASS")]
	InvasionBuffConditionNumClass,
	[ProtoEnum(Name = "INVASION_BUFF_CONDITION_ID_STAR_LEVEL")]
	InvasionBuffConditionIdStarLevel
}
