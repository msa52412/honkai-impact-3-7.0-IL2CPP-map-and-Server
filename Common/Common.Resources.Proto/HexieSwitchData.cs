using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class HexieSwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
