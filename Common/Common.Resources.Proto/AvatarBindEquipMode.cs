using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AvatarBindEquipMode
{
	[ProtoEnum(Name = "AVATAR_BIND_EQUIP_COMMON")]
	AvatarBindEquipCommon,
	[ProtoEnum(Name = "AVATAR_BIND_EQUIP_EXTRA_STORY")]
	AvatarBindEquipExtraStory
}
