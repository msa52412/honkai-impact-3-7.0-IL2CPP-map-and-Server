using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum StageEnterTimesType
{
	[ProtoEnum(Name = "STAGE_ENTER_TIMES_DAILY")]
	StageEnterTimesDaily,
	[ProtoEnum(Name = "STAGE_ENTER_TIMES_WEEKLY")]
	StageEnterTimesWeekly
}
