using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtActivity : IExtensible
{
	[ProtoContract]
	public class OpenTimePeriod : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__OpenDayTime;

		private uint? __pbn__CloseDayTime;

		[ProtoMember(1, Name = "open_day_time")]
		public uint OpenDayTime
		{
			get
			{
				return __pbn__OpenDayTime.GetValueOrDefault();
			}
			set
			{
				__pbn__OpenDayTime = value;
			}
		}

		[ProtoMember(2, Name = "close_day_time")]
		public uint CloseDayTime
		{
			get
			{
				return __pbn__CloseDayTime.GetValueOrDefault();
			}
			set
			{
				__pbn__CloseDayTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeOpenDayTime()
		{
			return __pbn__OpenDayTime.HasValue;
		}

		public void ResetOpenDayTime()
		{
			__pbn__OpenDayTime = null;
		}

		public bool ShouldSerializeCloseDayTime()
		{
			return __pbn__CloseDayTime.HasValue;
		}

		public void ResetCloseDayTime()
		{
			__pbn__CloseDayTime = null;
		}
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CurScheduleId;

	private uint? __pbn__OpenTime;

	private uint? __pbn__CloseTime;

	private uint? __pbn__TvtDivision;

	private uint? __pbn__TvtStar;

	private uint? __pbn__WinStreak;

	private uint? __pbn__TotalBattleNum;

	private uint? __pbn__TotalWin;

	private uint? __pbn__MaxWinStreak;

	private uint? __pbn__MvpNum;

	private uint? __pbn__Type;

	[ProtoMember(1, Name = "cur_schedule_id")]
	public uint CurScheduleId
	{
		get
		{
			return __pbn__CurScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurScheduleId = value;
		}
	}

	[ProtoMember(2, Name = "open_time")]
	public uint OpenTime
	{
		get
		{
			return __pbn__OpenTime.GetValueOrDefault();
		}
		set
		{
			__pbn__OpenTime = value;
		}
	}

	[ProtoMember(3, Name = "close_time")]
	public uint CloseTime
	{
		get
		{
			return __pbn__CloseTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CloseTime = value;
		}
	}

	[ProtoMember(6, Name = "tvt_division")]
	public uint TvtDivision
	{
		get
		{
			return __pbn__TvtDivision.GetValueOrDefault();
		}
		set
		{
			__pbn__TvtDivision = value;
		}
	}

	[ProtoMember(7, Name = "tvt_star")]
	public uint TvtStar
	{
		get
		{
			return __pbn__TvtStar.GetValueOrDefault();
		}
		set
		{
			__pbn__TvtStar = value;
		}
	}

	[ProtoMember(8, Name = "win_streak")]
	public uint WinStreak
	{
		get
		{
			return __pbn__WinStreak.GetValueOrDefault();
		}
		set
		{
			__pbn__WinStreak = value;
		}
	}

	[ProtoMember(9, Name = "has_take_division_up_reward_list")]
	public uint[] HasTakeDivisionUpRewardLists { get; set; }

	[ProtoMember(10, Name = "total_battle_num")]
	public uint TotalBattleNum
	{
		get
		{
			return __pbn__TotalBattleNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalBattleNum = value;
		}
	}

	[ProtoMember(11, Name = "total_win")]
	public uint TotalWin
	{
		get
		{
			return __pbn__TotalWin.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalWin = value;
		}
	}

	[ProtoMember(12, Name = "max_win_streak")]
	public uint MaxWinStreak
	{
		get
		{
			return __pbn__MaxWinStreak.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxWinStreak = value;
		}
	}

	[ProtoMember(13, Name = "mvp_num")]
	public uint MvpNum
	{
		get
		{
			return __pbn__MvpNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MvpNum = value;
		}
	}

	[ProtoMember(14, Name = "has_take_mission_group_reward_list")]
	public uint[] HasTakeMissionGroupRewardLists { get; set; }

	[ProtoMember(15, Name = "open_time_period_list")]
	public List<OpenTimePeriod> OpenTimePeriodLists { get; } = new List<OpenTimePeriod>();


	[ProtoMember(16, Name = "type")]
	public uint Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurScheduleId()
	{
		return __pbn__CurScheduleId.HasValue;
	}

	public void ResetCurScheduleId()
	{
		__pbn__CurScheduleId = null;
	}

	public bool ShouldSerializeOpenTime()
	{
		return __pbn__OpenTime.HasValue;
	}

	public void ResetOpenTime()
	{
		__pbn__OpenTime = null;
	}

	public bool ShouldSerializeCloseTime()
	{
		return __pbn__CloseTime.HasValue;
	}

	public void ResetCloseTime()
	{
		__pbn__CloseTime = null;
	}

	public bool ShouldSerializeTvtDivision()
	{
		return __pbn__TvtDivision.HasValue;
	}

	public void ResetTvtDivision()
	{
		__pbn__TvtDivision = null;
	}

	public bool ShouldSerializeTvtStar()
	{
		return __pbn__TvtStar.HasValue;
	}

	public void ResetTvtStar()
	{
		__pbn__TvtStar = null;
	}

	public bool ShouldSerializeWinStreak()
	{
		return __pbn__WinStreak.HasValue;
	}

	public void ResetWinStreak()
	{
		__pbn__WinStreak = null;
	}

	public bool ShouldSerializeTotalBattleNum()
	{
		return __pbn__TotalBattleNum.HasValue;
	}

	public void ResetTotalBattleNum()
	{
		__pbn__TotalBattleNum = null;
	}

	public bool ShouldSerializeTotalWin()
	{
		return __pbn__TotalWin.HasValue;
	}

	public void ResetTotalWin()
	{
		__pbn__TotalWin = null;
	}

	public bool ShouldSerializeMaxWinStreak()
	{
		return __pbn__MaxWinStreak.HasValue;
	}

	public void ResetMaxWinStreak()
	{
		__pbn__MaxWinStreak = null;
	}

	public bool ShouldSerializeMvpNum()
	{
		return __pbn__MvpNum.HasValue;
	}

	public void ResetMvpNum()
	{
		__pbn__MvpNum = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}
}
