using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldChapterTowerType
{
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_TOWER_COMMON")]
	OpenworldChapterTowerCommon = 1,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_TOWER_ENDLESS")]
	OpenworldChapterTowerEndless
}
