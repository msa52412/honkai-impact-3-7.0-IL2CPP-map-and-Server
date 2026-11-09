using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AvatarRollType
{
	[ProtoEnum(Name = "AVATAR_ROLL_AVATAR")]
	AvatarRollAvatar = 1,
	[ProtoEnum(Name = "AVATAR_ROLL_STIGMATA")]
	AvatarRollStigmata = 2,
	[ProtoEnum(Name = "AVATAR_ROLL_ERROR")]
	AvatarRollError = 100
}
