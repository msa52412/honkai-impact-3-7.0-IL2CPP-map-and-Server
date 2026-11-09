using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UltraEndlessEnterSiteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5212
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_SCHEDULE")]
		NotInSchedule,
		[ProtoEnum(Name = "PRE_NOT_FINISH")]
		PreNotFinish,
		[ProtoEnum(Name = "ALREADY_FINISH")]
		AlreadyFinish
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SiteId;

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
}
