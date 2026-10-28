using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum StageDifficulty
{
	[ProtoEnum(Name = "STAGE_DIFFICULTY_NORMAL")]
	StageDifficultyNormal = 1,
	[ProtoEnum(Name = "STAGE_DIFFICULTY_HARD")]
	StageDifficultyHard,
	[ProtoEnum(Name = "STAGE_DIFFICULTY_HELL")]
	StageDifficultyHell
}
