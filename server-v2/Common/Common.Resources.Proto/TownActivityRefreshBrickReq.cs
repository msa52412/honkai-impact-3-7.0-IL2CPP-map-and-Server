using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityRefreshBrickReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6481
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "head")]
	public TownCommonHead Head { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
