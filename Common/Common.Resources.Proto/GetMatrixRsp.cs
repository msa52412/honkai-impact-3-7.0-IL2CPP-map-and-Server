using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMatrixRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2321
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

	private uint? __pbn__CurFloorId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "floor_brief_info_list")]
	public List<MatrixFloorBriefInfo> FloorBriefInfoLists { get; } = new List<MatrixFloorBriefInfo>();


	[ProtoMember(3, Name = "cur_floor_id")]
	public uint CurFloorId
	{
		get
		{
			return __pbn__CurFloorId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurFloorId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurFloorId()
	{
		return __pbn__CurFloorId.HasValue;
	}

	public void ResetCurFloorId()
	{
		__pbn__CurFloorId = null;
	}
}
