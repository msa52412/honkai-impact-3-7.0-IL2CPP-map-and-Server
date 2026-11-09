using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetDanmakuListRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1546
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "DANMAKU_CLOSED")]
		DanmakuClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__DanmakuSlot;

	private uint? __pbn__DanmakuSlotStatus;

	private uint? __pbn__StartIdx;

	private uint? __pbn__EndIdx;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "danmaku_slot")]
	public uint DanmakuSlot
	{
		get
		{
			return __pbn__DanmakuSlot.GetValueOrDefault();
		}
		set
		{
			__pbn__DanmakuSlot = value;
		}
	}

	[ProtoMember(3, Name = "danmaku_slot_status")]
	public uint DanmakuSlotStatus
	{
		get
		{
			return __pbn__DanmakuSlotStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__DanmakuSlotStatus = value;
		}
	}

	[ProtoMember(4, Name = "start_idx")]
	public uint StartIdx
	{
		get
		{
			return __pbn__StartIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__StartIdx = value;
		}
	}

	[ProtoMember(5, Name = "end_idx")]
	public uint EndIdx
	{
		get
		{
			return __pbn__EndIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__EndIdx = value;
		}
	}

	[ProtoMember(6, Name = "danmaku_list")]
	public List<DanmakuContent> DanmakuLists { get; } = new List<DanmakuContent>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDanmakuSlot()
	{
		return __pbn__DanmakuSlot.HasValue;
	}

	public void ResetDanmakuSlot()
	{
		__pbn__DanmakuSlot = null;
	}

	public bool ShouldSerializeDanmakuSlotStatus()
	{
		return __pbn__DanmakuSlotStatus.HasValue;
	}

	public void ResetDanmakuSlotStatus()
	{
		__pbn__DanmakuSlotStatus = null;
	}

	public bool ShouldSerializeStartIdx()
	{
		return __pbn__StartIdx.HasValue;
	}

	public void ResetStartIdx()
	{
		__pbn__StartIdx = null;
	}

	public bool ShouldSerializeEndIdx()
	{
		return __pbn__EndIdx.HasValue;
	}

	public void ResetEndIdx()
	{
		__pbn__EndIdx = null;
	}
}
