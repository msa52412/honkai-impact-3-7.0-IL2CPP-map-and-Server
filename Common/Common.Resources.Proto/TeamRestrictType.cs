using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TeamRestrictType
{
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_NUM")]
	TeamRestrictTypeAvatarNum = 1,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_CLASS_NUM")]
	TeamRestrictTypeAvatarClassNum = 2,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_EACH_LEVEL")]
	TeamRestrictTypeAvatarEachLevel = 3,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_EACH_STAR")]
	TeamRestrictTypeAvatarEachStar = 4,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_ONE_STAR")]
	TeamRestrictTypeAvatarOneStar = 5,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_CLASS_NOT_SAME")]
	TeamRestrictTypeAvatarClassNotSame = 6,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_ATTR_NOT_SAME")]
	TeamRestrictTypeAvatarAttrNotSame = 7,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_EACH_ATTR")]
	TeamRestrictTypeAvatarEachAttr = 8,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_NOT_ATTR")]
	TeamRestrictTypeAvatarNotAttr = 9,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_EACH_CLASS")]
	TeamRestrictTypeAvatarEachClass = 10,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_NOT_CLASS")]
	TeamRestrictTypeAvatarNotClass = 11,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_HAS_AVATAR_ID")]
	TeamRestrictTypeHasAvatarId = 12,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_NOT_ID")]
	TeamRestrictTypeAvatarNotId = 13,
	[ProtoEnum(Name = "TEMA_RESTRICT_TYPE_ROLE_AVATAR_NUM")]
	TemaRestrictTypeRoleAvatarNum = 15,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_ROLE_LIST")]
	TeamRestrictTypeRoleList = 16,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_IN_AVATAR_LIST")]
	TeamRestrictTypeInAvatarList = 17,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_RESISTANCE")]
	TeamRestrictTypeResistance = 18,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_HAS_ELF")]
	TeamRestrictTypeHasElf = 19,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_EACH_TAG_LIST")]
	TeamRestrictTypeAvatarEachTagList = 20,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_NOT_TAG_LIST")]
	TeamRestrictTypeAvatarNotTagList = 21,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_ONE_TAG")]
	TeamRestrictTypeAvatarOneTag = 22,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_TAG_NUM")]
	TeamRestrictTypeAvatarTagNum = 23,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_ATTR_NUM")]
	TeamRestrictTypeAvatarAttrNum = 24,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_ATTR_SAME_NUM")]
	TeamRestrictTypeAvatarAttrSameNum = 25,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_IN_RPG_ROLE_LIST")]
	TeamRestrictTypeInRpgRoleList = 26,
	[ProtoEnum(Name = "TEAM_RESTRICT_TYPE_AVATAR_ELEMENT_TAG_NUM")]
	TeamRestrictTypeAvatarElementTagNum = 27
}
