using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class NinjaEventIDPlayedServerLocalMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "played_event_id_list")]
	public int[] PlayedEventIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
