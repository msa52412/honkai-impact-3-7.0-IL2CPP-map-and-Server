using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CityActivityEvent : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SiteId;

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

	[ProtoMember(2, Name = "event_id_list")]
	public uint[] EventIdLists { get; set; }

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
