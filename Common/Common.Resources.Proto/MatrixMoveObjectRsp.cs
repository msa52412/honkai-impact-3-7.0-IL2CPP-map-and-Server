using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixMoveObjectRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2340
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "POS_INVALID")]
		PosInvalid,
		[ProtoEnum(Name = "OBJECT_NOT_EXIST")]
		ObjectNotExist,
		[ProtoEnum(Name = "BLOCK")]
		Block,
		[ProtoEnum(Name = "IN_FLOOR_EVENT")]
		InFloorEvent
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EventId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "pos_list")]
	public List<MatrixGridPos> PosLists { get; } = new List<MatrixGridPos>();


	[ProtoMember(3, Name = "event_id")]
	public uint EventId
	{
		get
		{
			return __pbn__EventId.GetValueOrDefault();
		}
		set
		{
			__pbn__EventId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEventId()
	{
		return __pbn__EventId.HasValue;
	}

	public void ResetEventId()
	{
		__pbn__EventId = null;
	}
}
