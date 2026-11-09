using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetDormAvatarReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 611
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HouseId;

	[ProtoMember(1, Name = "house_id")]
	public uint HouseId
	{
		get
		{
			return __pbn__HouseId.GetValueOrDefault();
		}
		set
		{
			__pbn__HouseId = value;
		}
	}

	[ProtoMember(2, Name = "room_avatar_list")]
	public List<DormRoomAvatar> RoomAvatarLists { get; } = new List<DormRoomAvatar>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHouseId()
	{
		return __pbn__HouseId.HasValue;
	}

	public void ResetHouseId()
	{
		__pbn__HouseId = null;
	}
}
