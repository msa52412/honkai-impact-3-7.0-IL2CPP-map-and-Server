using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarServerLocalMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "favorite_avatar_id_list")]
	public int[] FavoriteAvatarIdLists { get; set; }

	[ProtoMember(2, Name = "new_avatar_id_list")]
	public int[] NewAvatarIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
