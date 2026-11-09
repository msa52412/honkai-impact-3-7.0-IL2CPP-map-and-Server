using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TaleId;

	private uint? __pbn__CurSiteId;

	private uint? __pbn__EndTime;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__SiteRefreshNum;

	private uint? __pbn__LastResetSiteRefreshNumTime;

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

	[ProtoMember(2, Name = "site_list")]
	public List<RpgTaleSite> SiteLists { get; } = new List<RpgTaleSite>();


	[ProtoMember(3, Name = "cur_site_id")]
	public uint CurSiteId
	{
		get
		{
			return __pbn__CurSiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurSiteId = value;
		}
	}

	[ProtoMember(4, Name = "reward_taken_collection_count_list")]
	public uint[] RewardTakenCollectionCountLists { get; set; }

	[ProtoMember(5, Name = "end_time")]
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

	[ProtoMember(8, Name = "finished_event_id_list")]
	public uint[] FinishedEventIdLists { get; set; }

	[ProtoMember(9, Name = "area_list")]
	public List<RpgTaleArea> AreaLists { get; } = new List<RpgTaleArea>();


	[ProtoMember(10, Name = "overall_list")]
	public List<RpgTaleOverall> OverallLists { get; } = new List<RpgTaleOverall>();


	[ProtoMember(11, Name = "schedule_id")]
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

	[ProtoMember(12, Name = "stage_list")]
	public List<RpgTaleStage> StageLists { get; } = new List<RpgTaleStage>();


	[ProtoMember(13, Name = "total_score_drop_list")]
	public List<RpgTaleTotalScoreDrop> TotalScoreDropLists { get; } = new List<RpgTaleTotalScoreDrop>();


	[ProtoMember(14, Name = "ability_list")]
	public List<RpgTaleAbility> AbilityLists { get; } = new List<RpgTaleAbility>();


	[ProtoMember(15, Name = "site_refresh_num")]
	public uint SiteRefreshNum
	{
		get
		{
			return __pbn__SiteRefreshNum.GetValueOrDefault();
		}
		set
		{
			__pbn__SiteRefreshNum = value;
		}
	}

	[ProtoMember(16, Name = "last_reset_site_refresh_num_time")]
	public uint LastResetSiteRefreshNumTime
	{
		get
		{
			return __pbn__LastResetSiteRefreshNumTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastResetSiteRefreshNumTime = value;
		}
	}

	[ProtoMember(17, Name = "qavatar_battle")]
	public RpgTaleQAvatarBattle QavatarBattle { get; set; }

	[ProtoMember(18, Name = "stage_enter_times_limit_list")]
	public List<RpgTaleStageEnterTimesLimit> StageEnterTimesLimitLists { get; } = new List<RpgTaleStageEnterTimesLimit>();


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

	public bool ShouldSerializeCurSiteId()
	{
		return __pbn__CurSiteId.HasValue;
	}

	public void ResetCurSiteId()
	{
		__pbn__CurSiteId = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}

	public bool ShouldSerializeSiteRefreshNum()
	{
		return __pbn__SiteRefreshNum.HasValue;
	}

	public void ResetSiteRefreshNum()
	{
		__pbn__SiteRefreshNum = null;
	}

	public bool ShouldSerializeLastResetSiteRefreshNumTime()
	{
		return __pbn__LastResetSiteRefreshNumTime.HasValue;
	}

	public void ResetLastResetSiteRefreshNumTime()
	{
		__pbn__LastResetSiteRefreshNumTime = null;
	}
}
