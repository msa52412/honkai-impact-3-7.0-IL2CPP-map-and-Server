using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldStageType
{
	[ProtoEnum(Name = "OPENWORLD_STAGE_TYPE_NONE")]
	OpenworldStageTypeNone,
	[ProtoEnum(Name = "OPENWORLD_STAGE_TYPE_MAINLINE")]
	OpenworldStageTypeMainline,
	[ProtoEnum(Name = "OPENWORLD_STAGE_TYPE_EXPORE")]
	OpenworldStageTypeExpore
}
