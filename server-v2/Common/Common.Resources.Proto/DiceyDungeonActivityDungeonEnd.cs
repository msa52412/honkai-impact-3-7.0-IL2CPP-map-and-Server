using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DiceyDungeonActivityDungeonEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6506
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TaleId;

	private uint? __pbn__ActivityId;

	private DiceyDungeonType? __pbn__DungeonType;

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

	[ProtoMember(3, Name = "dungeon_info")]
	public DiceyDungeonInfo DungeonInfo { get; set; }

	[ProtoMember(4, Name = "dungeon_type")]
	[DefaultValue(DiceyDungeonType.DiceyDungeonNone)]
	public DiceyDungeonType DungeonType
	{
		get
		{
			return __pbn__DungeonType.GetValueOrDefault();
		}
		set
		{
			__pbn__DungeonType = value;
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

	public bool ShouldSerializeDungeonType()
	{
		return __pbn__DungeonType.HasValue;
	}

	public void ResetDungeonType()
	{
		__pbn__DungeonType = null;
	}
}
[ProtoContract]
public class DiceyDungeonActivityDungeonEndRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6507
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
		[ProtoEnum(Name = "DUNGEON_NOT_BEGIN")]
		DungeonNotBegin,
		[ProtoEnum(Name = "ROLE_ERROR")]
		RoleError,
		[ProtoEnum(Name = "WEAPON_ERROR")]
		WeaponError,
		[ProtoEnum(Name = "DUNGEON_ROOM_ERROR")]
		DungeonRoomError
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

	private uint? __pbn__ActivityId;

	private uint? __pbn__DungeonId;

	private Status? __pbn__status;

	private uint? __pbn__TotalDiceyBattleScore;

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

	[ProtoMember(3, Name = "dungeon_id")]
	public uint DungeonId
	{
		get
		{
			return __pbn__DungeonId.GetValueOrDefault();
		}
		set
		{
			__pbn__DungeonId = value;
		}
	}

	[ProtoMember(4)]
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

	[ProtoMember(5, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(7, Name = "total_dicey_battle_score")]
	public uint TotalDiceyBattleScore
	{
		get
		{
			return __pbn__TotalDiceyBattleScore.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalDiceyBattleScore = value;
		}
	}

	[ProtoMember(8, Name = "daily_drop_item_list")]
	public List<DropItem> DailyDropItemLists { get; } = new List<DropItem>();


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

	public bool ShouldSerializeDungeonId()
	{
		return __pbn__DungeonId.HasValue;
	}

	public void ResetDungeonId()
	{
		__pbn__DungeonId = null;
	}

	public bool ShouldSerializestatus()
	{
		return __pbn__status.HasValue;
	}

	public void Resetstatus()
	{
		__pbn__status = null;
	}

	public bool ShouldSerializeTotalDiceyBattleScore()
	{
		return __pbn__TotalDiceyBattleScore.HasValue;
	}

	public void ResetTotalDiceyBattleScore()
	{
		__pbn__TotalDiceyBattleScore = null;
	}
}
