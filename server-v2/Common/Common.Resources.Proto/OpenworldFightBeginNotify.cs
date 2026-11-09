using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldFightBeginNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4540
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__AreaId;

	private OpenworldStageType? __pbn__StageType;

	private string __pbn__MonsterName;

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

	[ProtoMember(4, Name = "monster_name")]
	[DefaultValue("")]
	public string MonsterName
	{
		get
		{
			return __pbn__MonsterName ?? "";
		}
		set
		{
			__pbn__MonsterName = value;
		}
	}

	[ProtoMember(5, Name = "sub_area_name")]
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

	[ProtoMember(6, Name = "report_ext")]
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

	public bool ShouldSerializeMonsterName()
	{
		return __pbn__MonsterName != null;
	}

	public void ResetMonsterName()
	{
		__pbn__MonsterName = null;
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
