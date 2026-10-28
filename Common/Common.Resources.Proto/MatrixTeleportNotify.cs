using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixTeleportNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2338
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "from_pos")]
	public MatrixGridPos FromPos { get; set; }

	[ProtoMember(2, Name = "to_pos")]
	public MatrixGridPos ToPos { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
