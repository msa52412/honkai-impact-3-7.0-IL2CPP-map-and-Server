using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardSite : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "site_id")]
	public uint SiteId { get; set; }

	[ProtoMember(2, Name = "passed_floor_list", IsPacked = true)]
	public uint[] PassedFloorLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
