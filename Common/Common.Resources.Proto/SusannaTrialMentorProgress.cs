using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SusannaTrialMentorProgress : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AreaId;

	private uint? __pbn__Progress;

	private uint? __pbn__LastSiteId;

	[ProtoMember(1, Name = "area_id")]
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

	[ProtoMember(2, Name = "has_taken_progress_config_id_list")]
	public uint[] HasTakenProgressConfigIdLists { get; set; }

	[ProtoMember(3, Name = "progress")]
	public uint Progress
	{
		get
		{
			return __pbn__Progress.GetValueOrDefault();
		}
		set
		{
			__pbn__Progress = value;
		}
	}

	[ProtoMember(4, Name = "last_site_id")]
	public uint LastSiteId
	{
		get
		{
			return __pbn__LastSiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastSiteId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAreaId()
	{
		return __pbn__AreaId.HasValue;
	}

	public void ResetAreaId()
	{
		__pbn__AreaId = null;
	}

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeLastSiteId()
	{
		return __pbn__LastSiteId.HasValue;
	}

	public void ResetLastSiteId()
	{
		__pbn__LastSiteId = null;
	}
}
