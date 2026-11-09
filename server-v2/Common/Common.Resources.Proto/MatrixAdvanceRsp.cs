using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixAdvanceRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2327
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "PATH_NOT_VALID")]
		PathNotValid,
		[ProtoEnum(Name = "IN_FLOOR_EVENT")]
		InFloorEvent
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "cur_pos")]
	public MatrixGridPos CurPos { get; set; }

	[ProtoMember(3, Name = "pos_list")]
	public List<MatrixGridPos> PosLists { get; } = new List<MatrixGridPos>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
