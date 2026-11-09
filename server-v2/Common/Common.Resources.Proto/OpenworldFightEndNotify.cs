using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldFightEndNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4541
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__AreaId;

	private OpenworldStageType? __pbn__StageType;

	private string __pbn__MonsterKillInfo;

	private uint? __pbn__ReviveTimes;

	private OpenworldFightEndStatus? __pbn__FightEndStatus;

	private uint? __pbn__CostTime;

	private string __pbn__SubAreaName;

	private string __pbn__ReportExt;

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

	[ProtoMember(2, Name = "area_id")]
	public uint AreaId
	{
		get
		{
			return __pbn__AreaId.GetValueOrDefault();
		}
		set
		{
			__pbn__AreaId = value;
		}
	}

	[ProtoMember(3, Name = "stage_type")]
	[DefaultValue(OpenworldStageType.OpenworldStageTypeNone)]
	public OpenworldStageType StageType
	{
		get
		{
			return __pbn__StageType.GetValueOrDefault();
		}
		set
		{
			__pbn__StageType = value;
		}
	}

	[ProtoMember(4, Name = "monster_kill_info")]
	[DefaultValue("")]
	public string MonsterKillInfo
	{
		get
		{
			return __pbn__MonsterKillInfo ?? "";
		}
		set
		{
			__pbn__MonsterKillInfo = value;
		}
	}

	[ProtoMember(5, Name = "revive_times")]
	public uint ReviveTimes
	{
		get
		{
			return __pbn__ReviveTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ReviveTimes = value;
		}
	}

	[ProtoMember(6, Name = "fight_end_status")]
	[DefaultValue(OpenworldFightEndStatus.OpenworldFightStatusNone)]
	public OpenworldFightEndStatus FightEndStatus
	{
		get
		{
			return __pbn__FightEndStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__FightEndStatus = value;
		}
	}

	[ProtoMember(7, Name = "cost_time")]
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

	[ProtoMember(8, Name = "sub_area_name")]
	[DefaultValue("")]
	public string SubAreaName
	{
		get
		{
			return __pbn__SubAreaName ?? "";
		}
		set
		{
			__pbn__SubAreaName = value;
		}
	}

	[ProtoMember(9, Name = "report_ext")]
	[DefaultValue("")]
	public string ReportExt
	{
		get
		{
			return __pbn__ReportExt ?? "";
		}
		set
		{
			__pbn__ReportExt = value;
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

	public bool ShouldSerializeAreaId()
	{
		return __pbn__AreaId.HasValue;
	}

	public void ResetAreaId()
	{
		__pbn__AreaId = null;
	}

	public bool ShouldSerializeStageType()
	{
		return __pbn__StageType.HasValue;
	}

	public void ResetStageType()
	{
		__pbn__StageType = null;
	}

	public bool ShouldSerializeMonsterKillInfo()
	{
		return __pbn__MonsterKillInfo != null;
	}

	public void ResetMonsterKillInfo()
	{
		__pbn__MonsterKillInfo = null;
	}

	public bool ShouldSerializeReviveTimes()
	{
		return __pbn__ReviveTimes.HasValue;
	}

	public void ResetReviveTimes()
	{
		__pbn__ReviveTimes = null;
	}

	public bool ShouldSerializeFightEndStatus()
	{
		return __pbn__FightEndStatus.HasValue;
	}

	public void ResetFightEndStatus()
	{
		__pbn__FightEndStatus = null;
	}

	public bool ShouldSerializeCostTime()
	{
		return __pbn__CostTime.HasValue;
	}

	public void ResetCostTime()
	{
		__pbn__CostTime = null;
	}

	public bool ShouldSerializeSubAreaName()
	{
		return __pbn__SubAreaName != null;
	}

	public void ResetSubAreaName()
	{
		__pbn__SubAreaName = null;
	}

	public bool ShouldSerializeReportExt()
	{
		return __pbn__ReportExt != null;
	}

	public void ResetReportExt()
	{
		__pbn__ReportExt = null;
	}
}
