using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetArmadaLabelReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2840
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "label")]
	public ArmadaLabel Label { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
