using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldChapterTowerSiteType
{
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_TOWER_SITE_STAGE")]
	OpenworldChapterTowerSiteStage = 1,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_TOWER_SITE_PLOT")]
	OpenworldChapterTowerSitePlot
}
