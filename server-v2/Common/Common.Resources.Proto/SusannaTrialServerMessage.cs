using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SusannaTrialServerMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public int[] HasEnteredAreas { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
