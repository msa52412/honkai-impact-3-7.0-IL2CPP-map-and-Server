using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarTaleType
{
	[ProtoEnum(Name = "GOD_WAR_TALE_STORY_CHALLENGE")]
	GodWarTaleStoryChallenge = 1,
	[ProtoEnum(Name = "GOD_WAR_TALE_AVATAR_CHALLENGE")]
	GodWarTaleAvatarChallenge,
	[ProtoEnum(Name = "GOD_WAR_TALE_WEEKLY_CHALLENGE")]
	GodWarTaleWeeklyChallenge,
	[ProtoEnum(Name = "GOD_WAR_TALE_LOBBY")]
	GodWarTaleLobby
}
