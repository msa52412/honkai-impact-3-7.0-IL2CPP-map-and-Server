using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMatrixFloorRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2323
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "floor_brief_info")]
	public MatrixFloorBriefInfo FloorBriefInfo { get; set; }

	[ProtoMember(3, Name = "floor_map_info")]
	public MatrixFloorMapInfo FloorMapInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
