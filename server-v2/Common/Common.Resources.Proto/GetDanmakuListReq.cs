using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetDanmakuListReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1545
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__DanmakuSlot;

	private uint? __pbn__StartIdx;

	private uint? __pbn__EndIdx;

	[ProtoMember(1, Name = "danmaku_slot")]
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

	[ProtoMember(2, Name = "start_idx")]
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

	[ProtoMember(3, Name = "end_idx")]
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
