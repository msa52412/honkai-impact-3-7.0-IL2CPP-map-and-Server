using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VirtualTrainCommon : IExtensible
{
	[ProtoContract]
	public enum GroupType
	{
		[ProtoEnum(Name = "GROUP_CHAPTER")]
		GroupChapter = 1,
		[ProtoEnum(Name = "GROUP_RPG_TALE")]
		GroupRpgTale
	}

	[ProtoContract]
	public enum ItemType
	{
		[ProtoEnum(Name = "ITEM_NONE")]
		ItemNone,
		[ProtoEnum(Name = "ITEM_AVATAR")]
		ItemAvatar,
		[ProtoEnum(Name = "ITEM_WEAPON")]
		ItemWeapon,
		[ProtoEnum(Name = "ITEM_STIGMATA")]
		ItemStigmata,
		[ProtoEnum(Name = "ITEM_BUFF")]
		ItemBuff
	}

	[ProtoContract]
	public enum VirtualAvatarType
	{
		[ProtoEnum(Name = "VIRTUAL_AVATAR_NONE")]
		VirtualAvatarNone,
		[ProtoEnum(Name = "VIRTUAL_AVATAR_NORMAL")]
		VirtualAvatarNormal,
		[ProtoEnum(Name = "VIRTUAL_AVATAR_QAVATAR")]
		VirtualAvatarQavatar
	}

	private IExtension __pbn__extensionData;

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
