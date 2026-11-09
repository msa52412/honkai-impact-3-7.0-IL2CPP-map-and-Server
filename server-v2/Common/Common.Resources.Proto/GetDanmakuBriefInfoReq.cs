using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetDanmakuBriefInfoReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1547
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__DanmakuSlot;

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
}
