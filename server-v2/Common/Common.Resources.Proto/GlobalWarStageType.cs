using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalWarStageType
{
	[ProtoEnum(Name = "GLOBAL_WAR_STAGE_FOR_NONE")]
	GlobalWarStageForNone,
	[ProtoEnum(Name = "GLOBAL_WAR_STAGE_FOR_POINT")]
	GlobalWarStageForPoint,
	[ProtoEnum(Name = "GLOBAL_WAR_STAGE_FOR_ISOLATE_POINT")]
	GlobalWarStageForIsolatePoint
}
