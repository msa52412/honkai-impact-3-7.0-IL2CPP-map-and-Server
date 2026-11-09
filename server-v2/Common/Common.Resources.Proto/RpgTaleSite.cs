using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleSite : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SiteId;

	private uint? __pbn__AreaId;

	private RpgTaleSiteStatus? __pbn__Status;

	private RpgTaleSiteContentType? __pbn__ContentType;

	private string __pbn__ContentParam;

	private uint? __pbn__EventType;

	private uint? __pbn__EventParam;

	private uint? __pbn__PassTimes;

	private uint? __pbn__StatusEndTime;

	private uint? __pbn__CloseTime;

	[ProtoMember(1, Name = "site_id")]
	public uint SiteId
	{
		get
		{
			return __pbn__SiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__SiteId = value;
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

	[ProtoMember(3, Name = "status")]
	[DefaultValue(RpgTaleSiteStatus.RpgTaleSiteStatusInvalid)]
	public RpgTaleSiteStatus Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(4, Name = "content_type")]
	[DefaultValue(RpgTaleSiteContentType.RpgTaleSiteContentInvalid)]
	public RpgTaleSiteContentType ContentType
	{
		get
		{
			return __pbn__ContentType.GetValueOrDefault();
		}
		set
		{
			__pbn__ContentType = value;
		}
	}

	[ProtoMember(5, Name = "content_param")]
	[DefaultValue("")]
	public string ContentParam
	{
		get
		{
			return __pbn__ContentParam ?? "";
		}
		set
		{
			__pbn__ContentParam = value;
		}
	}

	[ProtoMember(6, Name = "finished_matrix_event_id_list")]
	public uint[] FinishedMatrixEventIdLists { get; set; }

	[ProtoMember(7, Name = "event_type")]
	public uint EventType
	{
		get
		{
			return __pbn__EventType.GetValueOrDefault();
		}
		set
		{
			__pbn__EventType = value;
		}
	}

	[ProtoMember(8, Name = "event_param")]
	public uint EventParam
	{
		get
		{
			return __pbn__EventParam.GetValueOrDefault();
		}
		set
		{
			__pbn__EventParam = value;
		}
	}

	[ProtoMember(9, Name = "cur_progress")]
	public RpgTaleProgress CurProgress { get; set; }

	[ProtoMember(10, Name = "pass_times")]
	public uint PassTimes
	{
		get
		{
			return __pbn__PassTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__PassTimes = value;
		}
	}

	[ProtoMember(11, Name = "status_end_time")]
	public uint StatusEndTime
	{
		get
		{
			return __pbn__StatusEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__StatusEndTime = value;
		}
	}

	[ProtoMember(12, Name = "close_time")]
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

	[ProtoMember(13, Name = "unlock_limit_flag_list")]
	public List<RpgTaleFlag> UnlockLimitFlagLists { get; } = new List<RpgTaleFlag>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSiteId()
	{
		return __pbn__SiteId.HasValue;
	}

	public void ResetSiteId()
	{
		__pbn__SiteId = null;
	}

	public bool ShouldSerializeAreaId()
	{
		return __pbn__AreaId.HasValue;
	}

	public void ResetAreaId()
	{
		__pbn__AreaId = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeContentType()
	{
		return __pbn__ContentType.HasValue;
	}

	public void ResetContentType()
	{
		__pbn__ContentType = null;
	}

	public bool ShouldSerializeContentParam()
	{
		return __pbn__ContentParam != null;
	}

	public void ResetContentParam()
	{
		__pbn__ContentParam = null;
	}

	public bool ShouldSerializeEventType()
	{
		return __pbn__EventType.HasValue;
	}

	public void ResetEventType()
	{
		__pbn__EventType = null;
	}

	public bool ShouldSerializeEventParam()
	{
		return __pbn__EventParam.HasValue;
	}

	public void ResetEventParam()
	{
		__pbn__EventParam = null;
	}

	public bool ShouldSerializePassTimes()
	{
		return __pbn__PassTimes.HasValue;
	}

	public void ResetPassTimes()
	{
		__pbn__PassTimes = null;
	}

	public bool ShouldSerializeStatusEndTime()
	{
		return __pbn__StatusEndTime.HasValue;
	}

	public void ResetStatusEndTime()
	{
		__pbn__StatusEndTime = null;
	}

	public bool ShouldSerializeCloseTime()
	{
		return __pbn__CloseTime.HasValue;
	}

	public void ResetCloseTime()
	{
		__pbn__CloseTime = null;
	}
}
