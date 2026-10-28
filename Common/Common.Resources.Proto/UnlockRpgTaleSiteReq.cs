using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UnlockRpgTaleSiteReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2307
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SiteId;

	private uint? __pbn__TaleId;

	private bool? __pbn__IsOnlyCheck;

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

	[ProtoMember(2, Name = "tale_id")]
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

	[ProtoMember(3, Name = "is_only_check")]
	public bool IsOnlyCheck
	{
		get
		{
			return __pbn__IsOnlyCheck.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOnlyCheck = value;
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

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeIsOnlyCheck()
	{
		return __pbn__IsOnlyCheck.HasValue;
	}

	public void ResetIsOnlyCheck()
	{
		__pbn__IsOnlyCheck = null;
	}
}
