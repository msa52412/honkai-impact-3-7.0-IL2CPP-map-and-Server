using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgBattleData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleId;

	private uint? __pbn__BattleScheduleId;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__TodayBeginTime;

	private uint? __pbn__TodayEndTime;

	private uint? __pbn__LastSettleTime;

	private bool? __pbn__IsSettled;

	private uint? __pbn__LevelGroupId;

	[ProtoMember(1, Name = "battle_id")]
	public uint BattleId
	{
		get
		{
			return __pbn__BattleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleId = value;
		}
	}

	[ProtoMember(2, Name = "battle_schedule_id")]
	public uint BattleScheduleId
	{
		get
		{
			return __pbn__BattleScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleScheduleId = value;
		}
	}

	[ProtoMember(3, Name = "country_data_list")]
	public List<SlgBattleCountryData> CountryDataLists { get; } = new List<SlgBattleCountryData>();


	[ProtoMember(4, Name = "point_data_list")]
	public List<SlgBattlePointData> PointDataLists { get; } = new List<SlgBattlePointData>();


	[ProtoMember(5, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(6, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(7, Name = "today_begin_time")]
	public uint TodayBeginTime
	{
		get
		{
			return __pbn__TodayBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayBeginTime = value;
		}
	}

	[ProtoMember(8, Name = "today_end_time")]
	public uint TodayEndTime
	{
		get
		{
			return __pbn__TodayEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayEndTime = value;
		}
	}

	[ProtoMember(9, Name = "player_data")]
	public SlgBattlePlayerData PlayerData { get; set; }

	[ProtoMember(10, Name = "adjoining_point_list")]
	public uint[] AdjoiningPointLists { get; set; }

	[ProtoMember(11, Name = "last_settle_time")]
	public uint LastSettleTime
	{
		get
		{
			return __pbn__LastSettleTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastSettleTime = value;
		}
	}

	[ProtoMember(12, Name = "is_settled")]
	public bool IsSettled
	{
		get
		{
			return __pbn__IsSettled.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSettled = value;
		}
	}

	[ProtoMember(13, Name = "level_group_id")]
	public uint LevelGroupId
	{
		get
		{
			return __pbn__LevelGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelGroupId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}

	public bool ShouldSerializeBattleScheduleId()
	{
		return __pbn__BattleScheduleId.HasValue;
	}

	public void ResetBattleScheduleId()
	{
		__pbn__BattleScheduleId = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeTodayBeginTime()
	{
		return __pbn__TodayBeginTime.HasValue;
	}

	public void ResetTodayBeginTime()
	{
		__pbn__TodayBeginTime = null;
	}

	public bool ShouldSerializeTodayEndTime()
	{
		return __pbn__TodayEndTime.HasValue;
	}

	public void ResetTodayEndTime()
	{
		__pbn__TodayEndTime = null;
	}

	public bool ShouldSerializeLastSettleTime()
	{
		return __pbn__LastSettleTime.HasValue;
	}

	public void ResetLastSettleTime()
	{
		__pbn__LastSettleTime = null;
	}

	public bool ShouldSerializeIsSettled()
	{
		return __pbn__IsSettled.HasValue;
	}

	public void ResetIsSettled()
	{
		__pbn__IsSettled = null;
	}

	public bool ShouldSerializeLevelGroupId()
	{
		return __pbn__LevelGroupId.HasValue;
	}

	public void ResetLevelGroupId()
	{
		__pbn__LevelGroupId = null;
	}
}
