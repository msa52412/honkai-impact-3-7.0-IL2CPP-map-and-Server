using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TvtStagePickType
{
	[ProtoEnum(Name = "TVT_STAGE_PICK_NONE")]
	TvtStagePickNone,
	[ProtoEnum(Name = "TVT_STAGE_PICK_FREE")]
	TvtStagePickFree,
	[ProtoEnum(Name = "TVT_STAGE_PICK_RANDOM")]
	TvtStagePickRandom
}
