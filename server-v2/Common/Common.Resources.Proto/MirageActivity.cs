using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MirageActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__DailyCurrencyDropNum;

	private uint? __pbn__CurRotateIndex;

	private uint? __pbn__CurRotateCloseTime;

	private uint? __pbn__CurDailyDropLimitResetTimes;

	[ProtoMember(1, Name = "schedule_id")]
	public uint ScheduleId
	{
		get
		{
			return __pbn__ScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScheduleId = value;
		}
	}

	[ProtoMember(2, Name = "daily_currency_drop_num")]
	public uint DailyCurrencyDropNum
	{
		get
		{
			return __pbn__DailyCurrencyDropNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyCurrencyDropNum = value;
		}
	}

	[ProtoMember(3, Name = "talent_list")]
	public List<MirageTalent> TalentLists { get; } = new List<MirageTalent>();


	[ProtoMember(4, Name = "challenge_record_list")]
	public List<MirageChallengeStageRecord> ChallengeRecordLists { get; } = new List<MirageChallengeStageRecord>();


	[ProtoMember(5, Name = "cur_rotate_index")]
	public uint CurRotateIndex
	{
		get
		{
			return __pbn__CurRotateIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__CurRotateIndex = value;
		}
	}

	[ProtoMember(6, Name = "cur_rotate_close_time")]
	public uint CurRotateCloseTime
	{
		get
		{
			return __pbn__CurRotateCloseTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CurRotateCloseTime = value;
		}
	}

	[ProtoMember(7, Name = "cur_daily_drop_limit_reset_times")]
	public uint CurDailyDropLimitResetTimes
	{
		get
		{
			return __pbn__CurDailyDropLimitResetTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__CurDailyDropLimitResetTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}

	public bool ShouldSerializeDailyCurrencyDropNum()
	{
		return __pbn__DailyCurrencyDropNum.HasValue;
	}

	public void ResetDailyCurrencyDropNum()
	{
		__pbn__DailyCurrencyDropNum = null;
	}

	public bool ShouldSerializeCurRotateIndex()
	{
		return __pbn__CurRotateIndex.HasValue;
	}

	public void ResetCurRotateIndex()
	{
		__pbn__CurRotateIndex = null;
	}

	public bool ShouldSerializeCurRotateCloseTime()
	{
		return __pbn__CurRotateCloseTime.HasValue;
	}

	public void ResetCurRotateCloseTime()
	{
		__pbn__CurRotateCloseTime = null;
	}

	public bool ShouldSerializeCurDailyDropLimitResetTimes()
	{
		return __pbn__CurDailyDropLimitResetTimes.HasValue;
	}

	public void ResetCurDailyDropLimitResetTimes()
	{
		__pbn__CurDailyDropLimitResetTimes = null;
	}
}
