using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DanmakuSlotBriefInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__DanmakuSlot;

	private uint? __pbn__DanmakuSlotStatus;

	private uint? __pbn__DanmakuNum;

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

	[ProtoMember(2, Name = "danmaku_slot_status")]
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

	[ProtoMember(3, Name = "danmaku_num")]
	public uint DanmakuNum
	{
		get
		{
			return __pbn__DanmakuNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DanmakuNum = value;
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

	public bool ShouldSerializeDanmakuSlotStatus()
	{
		return __pbn__DanmakuSlotStatus.HasValue;
	}

	public void ResetDanmakuSlotStatus()
	{
		__pbn__DanmakuSlotStatus = null;
	}

	public bool ShouldSerializeDanmakuNum()
	{
		return __pbn__DanmakuNum.HasValue;
	}

	public void ResetDanmakuNum()
	{
		__pbn__DanmakuNum = null;
	}
}
