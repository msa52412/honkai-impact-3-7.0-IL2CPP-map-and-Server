using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ArmadaBuildNotifyType
{
	[ProtoEnum(Name = "ARMADA_BUILD_NOTIFY_START")]
	ArmadaBuildNotifyStart = 1,
	[ProtoEnum(Name = "ARMADA_BUILD_NOTIFY_CANCEL")]
	ArmadaBuildNotifyCancel,
	[ProtoEnum(Name = "ARMADA_BUILD_NOTIFY_FINISH")]
	ArmadaBuildNotifyFinish,
	[ProtoEnum(Name = "ARMADA_BUILD_NOTIFY_MOVE")]
	ArmadaBuildNotifyMove
}
