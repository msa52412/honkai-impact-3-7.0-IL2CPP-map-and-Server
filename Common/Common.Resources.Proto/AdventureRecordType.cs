using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AdventureRecordType
{
	[ProtoEnum(Name = "ADVENTURE_RECORD_NONE")]
	AdventureRecordNone,
	[ProtoEnum(Name = "ADVENTURE_RECORD_GROWTH")]
	AdventureRecordGrowth,
	[ProtoEnum(Name = "ADVENTURE_RECORD_GROWTH_STAGE")]
	AdventureRecordGrowthStage,
	[ProtoEnum(Name = "ADVENTURE_RECORD_SKILL_STAGE")]
	AdventureRecordSkillStage
}
