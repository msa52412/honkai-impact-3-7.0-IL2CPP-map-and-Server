using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MpDataType
{
	[ProtoEnum(Name = "MP_DATA_ALL")]
	MpDataAll,
	[ProtoEnum(Name = "MP_DATA_LEVEL")]
	MpDataLevel,
	[ProtoEnum(Name = "MP_DATA_EXP")]
	MpDataExp,
	[ProtoEnum(Name = "MP_DATA_SKILL_POINT")]
	MpDataSkillPoint,
	[ProtoEnum(Name = "MP_DATA_SKILL")]
	MpDataSkill,
	[ProtoEnum(Name = "MP_DATA_AVATAR")]
	MpDataAvatar,
	[ProtoEnum(Name = "MP_DATA_PUNISH_TIME")]
	MpDataPunishTime
}
