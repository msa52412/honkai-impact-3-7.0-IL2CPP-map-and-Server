using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AiCyberUnlockConditionType
{
	[ProtoEnum(Name = "AI_CYBER_UNLOCK_DATE_TIME")]
	AiCyberUnlockDateTime = 1,
	[ProtoEnum(Name = "AI_CYBER_UNLOCK_PRE_STAGE")]
	AiCyberUnlockPreStage,
	[ProtoEnum(Name = "AI_CYBER_UNLOCK_REPAIR_PROGRESS")]
	AiCyberUnlockRepairProgress,
	[ProtoEnum(Name = "AI_CYBER_UNLOCK_PRE_EVENT")]
	AiCyberUnlockPreEvent
}
