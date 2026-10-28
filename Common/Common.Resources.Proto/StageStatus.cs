using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum StageStatus
{
	[ProtoEnum(Name = "STAGE_LOCKED")]
	StageLocked = 1,
	[ProtoEnum(Name = "STAGE_DOING")]
	StageDoing,
	[ProtoEnum(Name = "STAGE_DONE")]
	StageDone
}
