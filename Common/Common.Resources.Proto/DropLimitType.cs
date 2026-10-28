using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DropLimitType
{
	[ProtoEnum(Name = "DROP_LIMIT_STAGE")]
	DropLimitStage = 1,
	[ProtoEnum(Name = "DROP_LIMIT_DLC")]
	DropLimitDlc,
	[ProtoEnum(Name = "DROP_LIMIT_GOD_WAR")]
	DropLimitGodWar,
	[ProtoEnum(Name = "DROP_LIMIT_SC_DLC")]
	DropLimitScDlc,
	[ProtoEnum(Name = "DROP_LIMIT_CHAPTER_OW")]
	DropLimitChapterOw,
	[ProtoEnum(Name = "DROP_LIMIT_CHAPTER_OW_MOON_TOWER")]
	DropLimitChapterOwMoonTower
}
