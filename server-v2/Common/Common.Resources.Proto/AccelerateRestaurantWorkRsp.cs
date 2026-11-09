using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AccelerateRestaurantWorkRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5873
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "ROOM_NOT_EXIST")]
		RoomNotExist,
		[ProtoEnum(Name = "WORK_NOT_DOING")]
		WorkNotDoing,
		[ProtoEnum(Name = "TICKET_LACK")]
		TicketLack,
		[ProtoEnum(Name = "STOCK_LIMIT")]
		StockLimit,
		[ProtoEnum(Name = "AVATAR_INVALID")]
		AvatarInvalid
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__RoomId;

	private uint? __pbn__Num;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "room_id")]
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

	[ProtoMember(4, Name = "num")]
	public uint Num
	{
		get
		{
			return __pbn__Num.GetValueOrDefault();
		}
		set
		{
			__pbn__Num = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeRoomId()
	{
		return __pbn__RoomId.HasValue;
	}

	public void ResetRoomId()
	{
		__pbn__RoomId = null;
	}

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}
}
