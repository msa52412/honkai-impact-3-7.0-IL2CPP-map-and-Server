using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UltraEndlessReportSiteFloorRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5207
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "PRE_NOT_FINISH")]
		PreNotFinish,
		[ProtoEnum(Name = "NOT_IN_SCHEDULE")]
		NotInSchedule
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SiteId;

	private uint? __pbn__Floor;

	private bool? __pbn__IsUpFloor;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "site_id")]
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

	[ProtoMember(3, Name = "floor")]
	public uint Floor
	{
		get
		{
			return __pbn__Floor.GetValueOrDefault();
		}
		set
		{
			__pbn__Floor = value;
		}
	}

	[ProtoMember(4, Name = "is_up_floor")]
	public bool IsUpFloor
	{
		get
		{
			return __pbn__IsUpFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUpFloor = value;
		}
	}

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

	public bool ShouldSerializeFloor()
	{
		return __pbn__Floor.HasValue;
	}

	public void ResetFloor()
	{
		__pbn__Floor = null;
	}

	public bool ShouldSerializeIsUpFloor()
	{
		return __pbn__IsUpFloor.HasValue;
	}

	public void ResetIsUpFloor()
	{
		__pbn__IsUpFloor = null;
	}
}
