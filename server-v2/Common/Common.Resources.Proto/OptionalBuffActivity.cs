using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OptionalBuffActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "buff_list")]
	public uint[] BuffLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
