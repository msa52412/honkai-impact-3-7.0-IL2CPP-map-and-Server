using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarSite : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SiteId;

	private GodWarSiteStatus? __pbn__SiteStatus;

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

	[ProtoMember(2, Name = "site_status")]
	[DefaultValue(GodWarSiteStatus.GodWarSiteStatusInvalid)]
	public GodWarSiteStatus SiteStatus
	{
		get
		{
			return __pbn__SiteStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__SiteStatus = value;
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

	public bool ShouldSerializeSiteStatus()
	{
		return __pbn__SiteStatus.HasValue;
	}

	public void ResetSiteStatus()
	{
		__pbn__SiteStatus = null;
	}
}
