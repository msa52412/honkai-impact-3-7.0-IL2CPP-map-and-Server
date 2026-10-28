using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ClientSettingType
{
	[ProtoEnum(Name = "CLIENT_SETTING_NONE")]
	ClientSettingNone,
	[ProtoEnum(Name = "CLIENT_SETTING_ALL")]
	ClientSettingAll,
	[ProtoEnum(Name = "CLIENT_SETTING_WEEKLY_GUIDE_SWITCH")]
	ClientSettingWeeklyGuideSwitch,
	[ProtoEnum(Name = "CLIENT_SETTING_SNS_SHOW")]
	ClientSettingSnsShow,
	[ProtoEnum(Name = "CLIENT_SETTING_AVATAR_ARTIFACT")]
	ClientSettingAvatarArtifact,
	[ProtoEnum(Name = "CLIENT_SETTING_MASTER_PUPIL_TAG_SWITCH")]
	ClientSettingMasterPupilTagSwitch
}
