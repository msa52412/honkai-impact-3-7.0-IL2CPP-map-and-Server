using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatchThreeRoomEndRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7614
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_EXIST")]
		ActivityNotExist,
		[ProtoEnum(Name = "ROOM_NOT_BEGIN")]
		RoomNotBegin,
		[ProtoEnum(Name = "ROOM_ERROR")]
		RoomError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__RoomId;

	private bool? __pbn__IsFirstWin;

	private uint? __pbn__FloorId;

	[ProtoMember(1, IsRequired = true)]
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

	[ProtoMember(4, Name = "daily_drop_item_list")]
	public List<DropItem> DailyDropItemLists { get; } = new List<DropItem>();


	[ProtoMember(5, Name = "is_first_win")]
	public bool IsFirstWin
	{
		get
		{
			return __pbn__IsFirstWin.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFirstWin = value;
		}
	}

	[ProtoMember(6, Name = "floor_id")]
	public uint FloorId
	{
		get
		{
			return __pbn__FloorId.GetValueOrDefault();
		}
		set
		{
			__pbn__FloorId = value;
		}
	}

	[ProtoMember(7, Name = "reward_data")]
	public RewardData RewardData { get; set; }

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

	public bool ShouldSerializeIsFirstWin()
	{
		return __pbn__IsFirstWin.HasValue;
	}

	public void ResetIsFirstWin()
	{
		__pbn__IsFirstWin = null;
	}

	public bool ShouldSerializeFloorId()
	{
		return __pbn__FloorId.HasValue;
	}

	public void ResetFloorId()
	{
		__pbn__FloorId = null;
	}
}
