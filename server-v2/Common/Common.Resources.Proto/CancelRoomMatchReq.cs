using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CancelRoomMatchReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3167
	}

	private IExtension __pbn__extensionData;

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
