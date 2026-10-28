using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetClientDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1588
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "client_data")]
	public ClientData ClientData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
