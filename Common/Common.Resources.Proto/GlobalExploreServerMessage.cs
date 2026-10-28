using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalExploreServerMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "clicked_event_collection_id_list")]
	public int[] ClickedEventCollectionIdLists { get; set; }

	[ProtoMember(2, Name = "viewed_message_id_list")]
	public int[] ViewedMessageIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
