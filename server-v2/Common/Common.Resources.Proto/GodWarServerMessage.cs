using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarServerMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "viewed_typewriter_doc_id_list")]
	public uint[] ViewedTypewriterDocIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
