using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarBonusType
{
	[ProtoEnum(Name = "GOD_WAR_BONUS_APPEND")]
	GodWarBonusAppend = 1,
	[ProtoEnum(Name = "GOD_WAR_BONUS_ADD")]
	GodWarBonusAdd,
	[ProtoEnum(Name = "GOD_WAR_BONUS_REFRESH")]
	GodWarBonusRefresh
}
