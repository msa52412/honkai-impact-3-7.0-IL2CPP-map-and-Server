using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DutyType
{
	[ProtoEnum(Name = "DUTY_DAILY")]
	DutyDaily = 1,
	[ProtoEnum(Name = "DUTY_WEEKLY")]
	DutyWeekly
}
