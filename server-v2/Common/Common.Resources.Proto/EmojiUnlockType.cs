using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum EmojiUnlockType
{
	[ProtoEnum(Name = "EMOJI_UNLOCK_TYPE_DEFAULT")]
	EmojiUnlockTypeDefault = 1,
	[ProtoEnum(Name = "EMOJI_UNLOCK_TYPE_ITEM_DROP")]
	EmojiUnlockTypeItemDrop
}
