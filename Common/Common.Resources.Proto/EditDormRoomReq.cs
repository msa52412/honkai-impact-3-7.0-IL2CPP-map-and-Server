using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EditDormRoomReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 605
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HouseId;

	private uint? __pbn__RoomId;

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

	[ProtoMember(2, Name = "room_id")]
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

	[ProtoMember(3, Name = "furniture_list")]
	public List<Furniture> FurnitureLists { get; } = new List<Furniture>();


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

	public bool ShouldSerializeRoomId()
	{
		return __pbn__RoomId.HasValue;
	}

	public void ResetRoomId()
	{
		__pbn__RoomId = null;
	}
}
