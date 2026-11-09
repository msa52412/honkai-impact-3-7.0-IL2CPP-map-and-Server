using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldChapterTalentType
{
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_NORMAL_TALENT")]
	OpenworldChapterNormalTalent = 1,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_ACTIVE_TALENT")]
	OpenworldChapterActiveTalent,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_DIG_TALENT")]
	OpenworldChapterDigTalent
}
