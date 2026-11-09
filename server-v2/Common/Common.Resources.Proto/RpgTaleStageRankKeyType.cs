using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RpgTaleStageRankKeyType
{
	[ProtoEnum(Name = "RPG_TALE_STAGE_RANK_KEY_TYPE_SCHEDULE")]
	RpgTaleStageRankKeyTypeSchedule = 1,
	[ProtoEnum(Name = "RPG_TALE_STAGE_RANK_KEY_TYPE_TALE")]
	RpgTaleStageRankKeyTypeTale
}
