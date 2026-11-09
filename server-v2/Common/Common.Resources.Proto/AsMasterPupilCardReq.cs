using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AsMasterPupilCardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 566
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "card")]
	public MasterPupilCard Card { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
