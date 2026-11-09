using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MainStoryLevelSpecialJobServerMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "job_id_list")]
	public int[] JobIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
