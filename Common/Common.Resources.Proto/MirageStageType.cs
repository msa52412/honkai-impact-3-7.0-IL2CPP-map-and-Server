using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MirageStageType
{
	[ProtoEnum(Name = "MIRAGE_STAGE_TYPE_CHALLENGE")]
	MirageStageTypeChallenge = 1,
	[ProtoEnum(Name = "MIRAGE_STAGE_TYPE_DAILY")]
	MirageStageTypeDaily,
	[ProtoEnum(Name = "MIRAGE_STAGE_TYPE_TUTORIAL")]
	MirageStageTypeTutorial
}
