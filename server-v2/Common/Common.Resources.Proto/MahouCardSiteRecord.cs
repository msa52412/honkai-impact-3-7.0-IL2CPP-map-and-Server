using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardSiteRecord : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "site_type")]
	public uint SiteType { get; set; }

	[ProtoMember(2, Name = "site_id")]
	public uint SiteId { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
