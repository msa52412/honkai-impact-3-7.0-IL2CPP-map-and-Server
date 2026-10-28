using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncGodWarTaleNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6156
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "tale")]
	public GodWarTale Tale { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
