using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum StageEndStatus
{
	[ProtoEnum(Name = "STAGE_WIN")]
	StageWin = 1,
	[ProtoEnum(Name = "STAGE_NOT_MEET_CONDITION")]
	StageNotMeetCondition,
	[ProtoEnum(Name = "STAGE_All_DEAD")]
	STAGEAllDEAD,
	[ProtoEnum(Name = "STAGE_EXIT")]
	StageExit
}
