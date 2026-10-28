using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldMapType
{
	[ProtoEnum(Name = "OPENWORLD_NORMAL_MAP")]
	OpenworldNormalMap = 1,
	[ProtoEnum(Name = "OPENWORLD_ACTIVITY_MAP")]
	OpenworldActivityMap,
	[ProtoEnum(Name = "OPENWORLD_ENDLESS_MAP")]
	OpenworldEndlessMap,
	[ProtoEnum(Name = "OPENWORLD_DLC_MAP")]
	OpenworldDlcMap,
	[ProtoEnum(Name = "OPENWORLD_ENDLESS_SINGLE_MAP")]
	OpenworldEndlessSingleMap,
	[ProtoEnum(Name = "OPENWORLD_AVATAR_ACTIVITY_MAP")]
	OpenworldAvatarActivityMap,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_MAP")]
	OpenworldChapterMap,
	[ProtoEnum(Name = "OPENWORLD_SC_DLC_MAP")]
	OpenworldScDlcMap,
	[ProtoEnum(Name = "OPENWORLD_HUNT_ACTIVITY_MAP")]
	OpenworldHuntActivityMap,
	[ProtoEnum(Name = "OPENWORLD_AI_CYBER_ACTIVITY_MAP")]
	OpenworldAiCyberActivityMap
}
