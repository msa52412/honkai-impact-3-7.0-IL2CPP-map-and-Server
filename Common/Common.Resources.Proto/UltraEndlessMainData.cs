using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UltraEndlessMainData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__EffectTime;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__CloseTime;

	private uint? __pbn__LastScheduleId;

	private uint? __pbn__LastSettleTopRankScheduleId;

	private uint? __pbn__CurSeasonId;

	private uint? __pbn__LastSettleSeasonId;

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

	[ProtoMember(2, Name = "effect_time")]
	public uint EffectTime
	{
		get
		{
			return __pbn__EffectTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EffectTime = value;
		}
	}

	[ProtoMember(3, Name = "begin_time")]
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

	[ProtoMember(4, Name = "end_time")]
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

	[ProtoMember(5, Name = "close_time")]
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

	[ProtoMember(6, Name = "last_schedule_id")]
	public uint LastScheduleId
	{
		get
		{
			return __pbn__LastScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastScheduleId = value;
		}
	}

	[ProtoMember(8, Name = "site_list")]
	public List<UltraEndlessSite> SiteLists { get; } = new List<UltraEndlessSite>();


	[ProtoMember(10, Name = "last_settle_top_rank_schedule_id")]
	public uint LastSettleTopRankScheduleId
	{
		get
		{
			return __pbn__LastSettleTopRankScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastSettleTopRankScheduleId = value;
		}
	}

	[ProtoMember(11, Name = "cur_season_id")]
	public uint CurSeasonId
	{
		get
		{
			return __pbn__CurSeasonId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurSeasonId = value;
		}
	}

	[ProtoMember(12, Name = "last_settle_season_id")]
	public uint LastSettleSeasonId
	{
		get
		{
			return __pbn__LastSettleSeasonId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastSettleSeasonId = value;
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

	public bool ShouldSerializeEffectTime()
	{
		return __pbn__EffectTime.HasValue;
	}

	public void ResetEffectTime()
	{
		__pbn__EffectTime = null;
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

	public bool ShouldSerializeCloseTime()
	{
		return __pbn__CloseTime.HasValue;
	}

	public void ResetCloseTime()
	{
		__pbn__CloseTime = null;
	}

	public bool ShouldSerializeLastScheduleId()
	{
		return __pbn__LastScheduleId.HasValue;
	}

	public void ResetLastScheduleId()
	{
		__pbn__LastScheduleId = null;
	}

	public bool ShouldSerializeLastSettleTopRankScheduleId()
	{
		return __pbn__LastSettleTopRankScheduleId.HasValue;
	}

	public void ResetLastSettleTopRankScheduleId()
	{
		__pbn__LastSettleTopRankScheduleId = null;
	}

	public bool ShouldSerializeCurSeasonId()
	{
		return __pbn__CurSeasonId.HasValue;
	}

	public void ResetCurSeasonId()
	{
		__pbn__CurSeasonId = null;
	}

	public bool ShouldSerializeLastSettleSeasonId()
	{
		return __pbn__LastSettleSeasonId.HasValue;
	}

	public void ResetLastSettleSeasonId()
	{
		__pbn__LastSettleSeasonId = null;
	}
}
