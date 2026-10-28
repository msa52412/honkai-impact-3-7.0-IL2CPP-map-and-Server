using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum StageOpenStatus
{
	[ProtoEnum(Name = "STAGE_NOT_OPEN")]
	StageNotOpen = 1,
	[ProtoEnum(Name = "STAGE_OPENED")]
	StageOpened,
	[ProtoEnum(Name = "STAGE_CLOSED")]
	StageClosed
}
