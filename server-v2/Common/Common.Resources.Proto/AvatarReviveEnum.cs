using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AvatarReviveEnum
{
	[ProtoEnum(Name = "AVATAR_REVIVE_TIMES_UN_LIMIT")]
	AvatarReviveTimesUnLimit = 65535
}
