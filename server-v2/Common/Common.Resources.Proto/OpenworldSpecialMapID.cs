using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldSpecialMapID
{
	[ProtoEnum(Name = "OPENWORLD_SAKURA_MAP_ID")]
	OpenworldSakuraMapId = 1,
	[ProtoEnum(Name = "OPENWORLD_TM_MAP_ID")]
	OpenworldTmMapId = 2,
	[ProtoEnum(Name = "OPENWORLD_DLC_MAP_ID")]
	OpenworldDlcMapId = 401,
	[ProtoEnum(Name = "OPENWORLD_SC_DLC_MAP_ID")]
	OpenworldScDlcMapId = 601,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_29_OW")]
	OpenworldChapter29Ow = 801,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_31_OW")]
	OpenworldChapter31Ow = 1001
}
