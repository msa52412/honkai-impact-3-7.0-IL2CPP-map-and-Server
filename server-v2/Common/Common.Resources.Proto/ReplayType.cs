using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ReplayType
{
	[ProtoEnum(Name = "REPLAY_NONE")]
	ReplayNone,
	[ProtoEnum(Name = "REPLAY_EXBOSS")]
	ReplayExboss,
	[ProtoEnum(Name = "REPLAY_TVT")]
	ReplayTvt
}
