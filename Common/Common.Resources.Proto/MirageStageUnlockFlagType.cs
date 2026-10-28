using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MirageStageUnlockFlagType
{
	[ProtoEnum(Name = "MIRAGE_STAGE_UNLOCK_FLAG_TYPE_NONE")]
	MirageStageUnlockFlagTypeNone,
	[ProtoEnum(Name = "MIRAGE_STAGE_UNLOCK_FLAG_TYPE_UNTIL_TIME")]
	MirageStageUnlockFlagTypeUntilTime
}
