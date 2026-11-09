using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixAdvanceReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2326
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "from_pos")]
	public MatrixGridPos FromPos { get; set; }

	[ProtoMember(2, Name = "to_pos")]
	public MatrixGridPos ToPos { get; set; }

	[ProtoMember(3, Name = "path_list")]
	public List<MatrixAdvancePath> PathLists { get; } = new List<MatrixAdvancePath>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
