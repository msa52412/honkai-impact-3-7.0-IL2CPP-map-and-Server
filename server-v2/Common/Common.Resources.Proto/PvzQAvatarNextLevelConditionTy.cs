using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PvzQAvatarNextLevelConditionType
{
	[ProtoEnum(Name = "PVZ_QAVATAR_NEXT_LEVEL_CONDITION_NONE")]
	PvzQavatarNextLevelConditionNone,
	[ProtoEnum(Name = "PVZ_QAVATAR_NEXT_LEVEL_CONDITION_PASS_SITE")]
	PvzQavatarNextLevelConditionPassSite
}
