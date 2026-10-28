using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RpgTaleProgressType
{
	[ProtoEnum(Name = "RPG_TALE_PROGRESS_NONE")]
	RpgTaleProgressNone,
	[ProtoEnum(Name = "RPG_TALE_PROGRESS_EVENT")]
	RpgTaleProgressEvent,
	[ProtoEnum(Name = "RPG_TALE_PROGRESS_TOWER")]
	RpgTaleProgressTower,
	[ProtoEnum(Name = "RPG_TALE_PROGRESS_GENERAL_ACTIVITY")]
	RpgTaleProgressGeneralActivity,
	[ProtoEnum(Name = "RPG_TALE_PROGRESS_STAGE_SCORE")]
	RpgTaleProgressStageScore
}
