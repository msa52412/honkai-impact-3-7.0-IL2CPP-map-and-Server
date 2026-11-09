using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardRole : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "role_id")]
	public uint RoleId { get; set; }

	[ProtoMember(2, Name = "level")]
	public uint Level { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
