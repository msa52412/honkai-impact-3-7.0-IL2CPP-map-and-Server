using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AiCyberStageType
{
	[ProtoEnum(Name = "AI_CYBER_STAGE_INVALID")]
	AiCyberStageInvalid,
	[ProtoEnum(Name = "AI_CYBER_STAGE_MAIN")]
	AiCyberStageMain,
	[ProtoEnum(Name = "AI_CYBER_STAGE_DAILY")]
	AiCyberStageDaily,
	[ProtoEnum(Name = "AI_CYBER_STAGE_CHALLENGE")]
	AiCyberStageChallenge
}
