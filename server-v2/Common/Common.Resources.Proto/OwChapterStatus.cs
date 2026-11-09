using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OwChapterStatus : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "antigravity_coin_index_list")]
	public uint[] AntigravityCoinIndexLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
