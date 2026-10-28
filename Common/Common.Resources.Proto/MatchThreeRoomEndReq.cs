using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatchThreeRoomEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7613
	}

	[ProtoContract]
	public enum Status
	{
		[ProtoEnum(Name = "WIN")]
		Win,
		[ProtoEnum(Name = "LOSE")]
		Lose,
		[ProtoEnum(Name = "ABANDON")]
		Abandon
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TaleId;

	private uint? __pbn__ActivityId;

	private Status? __pbn__status;

	private uint? __pbn__RoomId;

	private MatchThreeRoomType? __pbn__RoomType;

	private uint? __pbn__Score;

	private uint? __pbn__BattleTime;

	private uint? __pbn__FloorId;

	[ProtoMember(1, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

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

	[ProtoMember(3)]
	[DefaultValue(Status.Win)]
	public Status status
	{
		get
		{
			return __pbn__status.GetValueOrDefault();
		}
		set
		{
			__pbn__status = value;
		}
	}

	[ProtoMember(4, Name = "room_id")]
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

	[ProtoMember(5, Name = "room_type")]
	[DefaultValue(MatchThreeRoomType.MatchThreeRoomNone)]
	public MatchThreeRoomType RoomType
	{
		get
		{
			return __pbn__RoomType.GetValueOrDefault();
		}
		set
		{
			__pbn__RoomType = value;
		}
	}

	[ProtoMember(6, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(7, Name = "battle_time")]
	public uint BattleTime
	{
		get
		{
			return __pbn__BattleTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleTime = value;
		}
	}

	[ProtoMember(8, Name = "floor_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializestatus()
	{
		return __pbn__status.HasValue;
	}

	public void Resetstatus()
	{
		__pbn__status = null;
	}

	public bool ShouldSerializeRoomId()
	{
		return __pbn__RoomId.HasValue;
	}

	public void ResetRoomId()
	{
		__pbn__RoomId = null;
	}

	public bool ShouldSerializeRoomType()
	{
		return __pbn__RoomType.HasValue;
	}

	public void ResetRoomType()
	{
		__pbn__RoomType = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeBattleTime()
	{
		return __pbn__BattleTime.HasValue;
	}

	public void ResetBattleTime()
	{
		__pbn__BattleTime = null;
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
