using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DiceyDungeonActivityRoomEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6510
	}

	[ProtoContract]
	public enum Status
	{
		[ProtoEnum(Name = "WIN")]
		Win,
		[ProtoEnum(Name = "LOSE")]
		Lose,
		[ProtoEnum(Name = "SKIP")]
		Skip
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TaleId;

	private uint? __pbn__ActivityId;

	private Status? __pbn__status;

	private DiceyDungeonType? __pbn__DungeonType;

	private uint? __pbn__BattleTime;

	private uint? __pbn__BattleRound;

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

	[ProtoMember(5, Name = "dungeon_type")]
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

	[ProtoMember(6, Name = "battle_time")]
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

	[ProtoMember(7, Name = "battle_round")]
	public uint BattleRound
	{
		get
		{
			return __pbn__BattleRound.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleRound = value;
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

	public bool ShouldSerializeDungeonType()
	{
		return __pbn__DungeonType.HasValue;
	}

	public void ResetDungeonType()
	{
		__pbn__DungeonType = null;
	}

	public bool ShouldSerializeBattleTime()
	{
		return __pbn__BattleTime.HasValue;
	}

	public void ResetBattleTime()
	{
		__pbn__BattleTime = null;
	}

	public bool ShouldSerializeBattleRound()
	{
		return __pbn__BattleRound.HasValue;
	}

	public void ResetBattleRound()
	{
		__pbn__BattleRound = null;
	}
}
