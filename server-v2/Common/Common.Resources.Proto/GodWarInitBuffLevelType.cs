using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarInitBuffLevelType
{
	[ProtoEnum(Name = "GOD_WAR_INIT_BUFF_LEVEL_NONE")]
	GodWarInitBuffLevelNone,
	[ProtoEnum(Name = "GOD_WAR_INIT_BUFF_LEVEL_SETTLED")]
	GodWarInitBuffLevelSettled,
	[ProtoEnum(Name = "GOD_WAR_INIT_BUFF_LEVEL_ALL")]
	GodWarInitBuffLevelAll
}
