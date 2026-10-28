using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RpgTaleStageRankType
{
	[ProtoEnum(Name = "RPG_TALE_STAGE_RANK_TYPE_MINI")]
	RpgTaleStageRankTypeMini = 1,
	[ProtoEnum(Name = "RPG_TALE_STAGE_RANK_TYPE_NORMAL")]
	RpgTaleStageRankTypeNormal
}
