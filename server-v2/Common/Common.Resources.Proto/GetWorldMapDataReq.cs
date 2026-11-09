using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetWorldMapDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1012
	}

	private IExtension __pbn__extensionData;

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
