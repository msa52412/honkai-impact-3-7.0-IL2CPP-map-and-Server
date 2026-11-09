using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldChapterQTEMapType
{
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_QTE_NORMAL_MAP")]
	OpenworldChapterQteNormalMap = 1,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_QTE_SPECIAL_MAP")]
	OpenworldChapterQteSpecialMap
}
