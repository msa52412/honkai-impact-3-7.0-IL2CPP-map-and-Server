using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RanchNotifyType
{
	[ProtoEnum(Name = "RANCH_NOTIFY_UNKNOWN")]
	RanchNotifyUnknown,
	[ProtoEnum(Name = "RANCH_NOTIFY_SYNTHESIS")]
	RanchNotifySynthesis,
	[ProtoEnum(Name = "RANCH_NOTIFY_STAGE_END")]
	RanchNotifyStageEnd,
	[ProtoEnum(Name = "RANCH_NOTIFY_LOCK")]
	RanchNotifyLock
}
