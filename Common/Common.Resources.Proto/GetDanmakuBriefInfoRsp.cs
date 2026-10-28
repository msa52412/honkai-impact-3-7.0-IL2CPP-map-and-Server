using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetDanmakuBriefInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1548
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

	[ProtoMember(3, Name = "brief_info")]
	public DanmakuSlotBriefInfo BriefInfo { get; set; }

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
