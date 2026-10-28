using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum FeatureLimitLevel
{
	[ProtoEnum(Name = "LIMIT_LEVEL_ALL")]
	LimitLevelAll = 0,
	[ProtoEnum(Name = "LIMIT_LEVEL_MAIN")]
	LimitLevelMain = 100,
	[ProtoEnum(Name = "LIMIT_LEVEL_CHAT")]
	LimitLevelChat = 200
}
