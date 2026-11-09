using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum EndlessRewardType
{
	[ProtoEnum(Name = "ENDLESS_PROMOTE")]
	EndlessPromote = 1,
	[ProtoEnum(Name = "ENDLESS_NORMAL")]
	EndlessNormal,
	[ProtoEnum(Name = "ENDLESS_DEMOTE")]
	EndlessDemote
}
