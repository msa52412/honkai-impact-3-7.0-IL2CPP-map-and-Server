using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterMoonChallengeTower : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TowerId;

	private uint? __pbn__MaxScore;

	private uint? __pbn__SavePointFloor;

	private uint? __pbn__MaxFinishFloor;

	[ProtoMember(1, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	[ProtoMember(2, Name = "max_score")]
	public uint MaxScore
	{
		get
		{
			return __pbn__MaxScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxScore = value;
		}
	}

	[ProtoMember(3, Name = "save_point_floor")]
	public uint SavePointFloor
	{
		get
		{
			return __pbn__SavePointFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__SavePointFloor = value;
		}
	}

	[ProtoMember(4, Name = "max_finish_floor")]
	public uint MaxFinishFloor
	{
		get
		{
			return __pbn__MaxFinishFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxFinishFloor = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}

	public bool ShouldSerializeMaxScore()
	{
		return __pbn__MaxScore.HasValue;
	}

	public void ResetMaxScore()
	{
		__pbn__MaxScore = null;
	}

	public bool ShouldSerializeSavePointFloor()
	{
		return __pbn__SavePointFloor.HasValue;
	}

	public void ResetSavePointFloor()
	{
		__pbn__SavePointFloor = null;
	}

	public bool ShouldSerializeMaxFinishFloor()
	{
		return __pbn__MaxFinishFloor.HasValue;
	}

	public void ResetMaxFinishFloor()
	{
		__pbn__MaxFinishFloor = null;
	}
}
[ProtoContract]
public class OpenworldChapterMoonChallengeTowerBattleBeginReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4675
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__TowerId;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(2, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}
}
[ProtoContract]
public class OpenworldChapterMoonChallengeTowerBattleBeginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4676
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError,
		[ProtoEnum(Name = "TOWER_ERROR")]
		TowerError,
		[ProtoEnum(Name = "DAILY_TIMES_LACK")]
		DailyTimesLack,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "FLOOR_ERROR")]
		FloorError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__TowerId;

	private uint? __pbn__CurFloor;

	private uint? __pbn__CurScore;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(3, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	[ProtoMember(4, Name = "cur_floor")]
	public uint CurFloor
	{
		get
		{
			return __pbn__CurFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__CurFloor = value;
		}
	}

	[ProtoMember(5, Name = "cur_score")]
	public uint CurScore
	{
		get
		{
			return __pbn__CurScore.GetValueOrDefault();
		}
		set
		{
			__pbn__CurScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}

	public bool ShouldSerializeCurFloor()
	{
		return __pbn__CurFloor.HasValue;
	}

	public void ResetCurFloor()
	{
		__pbn__CurFloor = null;
	}

	public bool ShouldSerializeCurScore()
	{
		return __pbn__CurScore.HasValue;
	}

	public void ResetCurScore()
	{
		__pbn__CurScore = null;
	}
}
[ProtoContract]
public class OpenworldChapterMoonChallengeTowerBattleEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4677
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__TowerId;

	private uint? __pbn__EndFloor;

	private uint? __pbn__Score;

	private MoonChallengeTowerBattleEndStatus? __pbn__BattleEndStatus;

	private uint? __pbn__CostTime;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(2, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	[ProtoMember(3, Name = "end_floor")]
	public uint EndFloor
	{
		get
		{
			return __pbn__EndFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__EndFloor = value;
		}
	}

	[ProtoMember(4, Name = "score")]
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

	[ProtoMember(5, Name = "battle_end_status")]
	[DefaultValue(MoonChallengeTowerBattleEndStatus.MoonChallengeTowerBattleEndInvalid)]
	public MoonChallengeTowerBattleEndStatus BattleEndStatus
	{
		get
		{
			return __pbn__BattleEndStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleEndStatus = value;
		}
	}

	[ProtoMember(6, Name = "cost_time")]
	public uint CostTime
	{
		get
		{
			return __pbn__CostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CostTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}

	public bool ShouldSerializeEndFloor()
	{
		return __pbn__EndFloor.HasValue;
	}

	public void ResetEndFloor()
	{
		__pbn__EndFloor = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeBattleEndStatus()
	{
		return __pbn__BattleEndStatus.HasValue;
	}

	public void ResetBattleEndStatus()
	{
		__pbn__BattleEndStatus = null;
	}

	public bool ShouldSerializeCostTime()
	{
		return __pbn__CostTime.HasValue;
	}

	public void ResetCostTime()
	{
		__pbn__CostTime = null;
	}
}
[ProtoContract]
public class OpenworldChapterMoonChallengeTowerBattleEndRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4678
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError,
		[ProtoEnum(Name = "TOWER_ERROR")]
		TowerError,
		[ProtoEnum(Name = "DAILY_TIMES_LACK")]
		DailyTimesLack,
		[ProtoEnum(Name = "FLOOR_ERROR")]
		FloorError,
		[ProtoEnum(Name = "SCORE_ERROR")]
		ScoreError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__TowerId;

	private uint? __pbn__EndFloor;

	private uint? __pbn__Score;

	private uint? __pbn__OldMaxScore;

	private uint? __pbn__SavePointFloor;

	private uint? __pbn__SavePointDropTerminalExp;

	private MoonChallengeTowerBattleEndStatus? __pbn__BattleEndStatus;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(3, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	[ProtoMember(4, Name = "end_floor")]
	public uint EndFloor
	{
		get
		{
			return __pbn__EndFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__EndFloor = value;
		}
	}

	[ProtoMember(5, Name = "score")]
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

	[ProtoMember(6, Name = "old_max_score")]
	public uint OldMaxScore
	{
		get
		{
			return __pbn__OldMaxScore.GetValueOrDefault();
		}
		set
		{
			__pbn__OldMaxScore = value;
		}
	}

	[ProtoMember(7, Name = "save_point_floor")]
	public uint SavePointFloor
	{
		get
		{
			return __pbn__SavePointFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__SavePointFloor = value;
		}
	}

	[ProtoMember(8, Name = "score_drop_material")]
	public Material ScoreDropMaterial { get; set; }

	[ProtoMember(9, Name = "save_point_drop_material")]
	public Material SavePointDropMaterial { get; set; }

	[ProtoMember(10, Name = "save_point_drop_terminal_exp")]
	public uint SavePointDropTerminalExp
	{
		get
		{
			return __pbn__SavePointDropTerminalExp.GetValueOrDefault();
		}
		set
		{
			__pbn__SavePointDropTerminalExp = value;
		}
	}

	[ProtoMember(11, Name = "battle_end_status")]
	[DefaultValue(MoonChallengeTowerBattleEndStatus.MoonChallengeTowerBattleEndInvalid)]
	public MoonChallengeTowerBattleEndStatus BattleEndStatus
	{
		get
		{
			return __pbn__BattleEndStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleEndStatus = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}

	public bool ShouldSerializeEndFloor()
	{
		return __pbn__EndFloor.HasValue;
	}

	public void ResetEndFloor()
	{
		__pbn__EndFloor = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeOldMaxScore()
	{
		return __pbn__OldMaxScore.HasValue;
	}

	public void ResetOldMaxScore()
	{
		__pbn__OldMaxScore = null;
	}

	public bool ShouldSerializeSavePointFloor()
	{
		return __pbn__SavePointFloor.HasValue;
	}

	public void ResetSavePointFloor()
	{
		__pbn__SavePointFloor = null;
	}

	public bool ShouldSerializeSavePointDropTerminalExp()
	{
		return __pbn__SavePointDropTerminalExp.HasValue;
	}

	public void ResetSavePointDropTerminalExp()
	{
		__pbn__SavePointDropTerminalExp = null;
	}

	public bool ShouldSerializeBattleEndStatus()
	{
		return __pbn__BattleEndStatus.HasValue;
	}

	public void ResetBattleEndStatus()
	{
		__pbn__BattleEndStatus = null;
	}
}
[ProtoContract]
public class OpenworldChapterMoonChallengeTowerGetDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4673
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}
}
[ProtoContract]
public class OpenworldChapterMoonChallengeTowerGetDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4674
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__DailyChallengeTimes;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(3, Name = "daily_challenge_times")]
	public uint DailyChallengeTimes
	{
		get
		{
			return __pbn__DailyChallengeTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyChallengeTimes = value;
		}
	}

	[ProtoMember(4, Name = "challenge_tower_list")]
	public List<OpenworldChapterMoonChallengeTower> ChallengeTowerLists { get; } = new List<OpenworldChapterMoonChallengeTower>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeDailyChallengeTimes()
	{
		return __pbn__DailyChallengeTimes.HasValue;
	}

	public void ResetDailyChallengeTimes()
	{
		__pbn__DailyChallengeTimes = null;
	}
}
[ProtoContract]
public class OpenworldChapterMoonChallengeTowerGetRankReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4681
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__TowerId;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(2, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}
}
[ProtoContract]
public class OpenworldChapterMoonChallengeTowerGetRankRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4682
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError,
		[ProtoEnum(Name = "TOWER_ERROR")]
		TowerError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__TowerId;

	private uint? __pbn__RankPercent;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(3, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	[ProtoMember(4, Name = "rank_data")]
	public RankShowData RankData { get; set; }

	[ProtoMember(5, Name = "rank_percent")]
	public uint RankPercent
	{
		get
		{
			return __pbn__RankPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__RankPercent = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}

	public bool ShouldSerializeRankPercent()
	{
		return __pbn__RankPercent.HasValue;
	}

	public void ResetRankPercent()
	{
		__pbn__RankPercent = null;
	}
}
[ProtoContract]
public class OpenworldChapterMoonChallengeTowerSweepReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4679
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__TowerId;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(2, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}
}
[ProtoContract]
public class OpenworldChapterMoonChallengeTowerSweepRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4680
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError,
		[ProtoEnum(Name = "TOWER_ERROR")]
		TowerError,
		[ProtoEnum(Name = "DAILY_TIMES_LACK")]
		DailyTimesLack,
		[ProtoEnum(Name = "SWEEP_LOCK")]
		SweepLock,
		[ProtoEnum(Name = "FLOOR_ERROR")]
		FloorError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__TowerId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(3, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	[ProtoMember(4, Name = "score_drop_material")]
	public Material ScoreDropMaterial { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}
}
