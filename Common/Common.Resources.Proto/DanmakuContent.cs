using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DanmakuContent : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__DanmakuId;

	private uint? __pbn__SenderUid;

	private string __pbn__Nickname;

	private uint? __pbn__SendTime;

	private string __pbn__Content;

	private uint? __pbn__KingdomId;

	[ProtoMember(1, Name = "danmaku_id")]
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

	[ProtoMember(3, Name = "nickname")]
	[DefaultValue("")]
	public string Nickname
	{
		get
		{
			return __pbn__Nickname ?? "";
		}
		set
		{
			__pbn__Nickname = value;
		}
	}

	[ProtoMember(4, Name = "send_time")]
	public uint SendTime
	{
		get
		{
			return __pbn__SendTime.GetValueOrDefault();
		}
		set
		{
			__pbn__SendTime = value;
		}
	}

	[ProtoMember(5, Name = "content")]
	[DefaultValue("")]
	public string Content
	{
		get
		{
			return __pbn__Content ?? "";
		}
		set
		{
			__pbn__Content = value;
		}
	}

	[ProtoMember(6, Name = "kingdom_id")]
	public uint KingdomId
	{
		get
		{
			return __pbn__KingdomId.GetValueOrDefault();
		}
		set
		{
			__pbn__KingdomId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDanmakuId()
	{
		return __pbn__DanmakuId != null;
	}

	public void ResetDanmakuId()
	{
		__pbn__DanmakuId = null;
	}

	public bool ShouldSerializeSenderUid()
	{
		return __pbn__SenderUid.HasValue;
	}

	public void ResetSenderUid()
	{
		__pbn__SenderUid = null;
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}

	public bool ShouldSerializeSendTime()
	{
		return __pbn__SendTime.HasValue;
	}

	public void ResetSendTime()
	{
		__pbn__SendTime = null;
	}

	public bool ShouldSerializeContent()
	{
		return __pbn__Content != null;
	}

	public void ResetContent()
	{
		__pbn__Content = null;
	}

	public bool ShouldSerializeKingdomId()
	{
		return __pbn__KingdomId.HasValue;
	}

	public void ResetKingdomId()
	{
		__pbn__KingdomId = null;
	}
}
