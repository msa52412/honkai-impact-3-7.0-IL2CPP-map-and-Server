using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ImpeachDanmakuReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1549
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__DanmakuSlot;

	private uint? __pbn__SenderUid;

	private string __pbn__DanmakuId;

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

	[ProtoMember(2, Name = "sender_uid")]
	public uint SenderUid
	{
		get
		{
			return __pbn__SenderUid.GetValueOrDefault();
		}
		set
		{
			__pbn__SenderUid = value;
		}
	}

	[ProtoMember(3, Name = "danmaku_id")]
	[DefaultValue("")]
	public string DanmakuId
	{
		get
		{
			return __pbn__DanmakuId ?? "";
		}
		set
		{
			__pbn__DanmakuId = value;
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

	public bool ShouldSerializeSenderUid()
	{
		return __pbn__SenderUid.HasValue;
	}

	public void ResetSenderUid()
	{
		__pbn__SenderUid = null;
	}

	public bool ShouldSerializeDanmakuId()
	{
		return __pbn__DanmakuId != null;
	}

	public void ResetDanmakuId()
	{
		__pbn__DanmakuId = null;
	}
}
