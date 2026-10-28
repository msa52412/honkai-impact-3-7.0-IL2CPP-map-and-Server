using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DormRoomAvatar : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__RoomId;

	[ProtoMember(1, Name = "room_id")]
	public uint RoomId
	{
		get
		{
			return __pbn__RoomId.GetValueOrDefault();
		}
		set
		{
			__pbn__RoomId = value;
		}
	}

	[ProtoMember(2, Name = "avatar_list")]
	public uint[] AvatarLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRoomId()
	{
		return __pbn__RoomId.HasValue;
	}

	public void ResetRoomId()
	{
		__pbn__RoomId = null;
	}
}
