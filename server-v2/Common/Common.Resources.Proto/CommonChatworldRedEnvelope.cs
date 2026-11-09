using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CommonChatworldRedEnvelope : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__RedEnvelopeId;

	private uint? __pbn__ExpireTime;

	private uint? __pbn__SenderUid;

	private uint? __pbn__ItemId;

	[ProtoMember(1, Name = "red_envelope_id")]
	public uint RedEnvelopeId
	{
		get
		{
			return __pbn__RedEnvelopeId.GetValueOrDefault();
		}
		set
		{
			__pbn__RedEnvelopeId = value;
		}
	}

	[ProtoMember(2, Name = "expire_time")]
	public uint ExpireTime
	{
		get
		{
			return __pbn__ExpireTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpireTime = value;
		}
	}

	[ProtoMember(3, Name = "sender_uid")]
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

	[ProtoMember(4, Name = "item_id")]
	public uint ItemId
	{
		get
		{
			return __pbn__ItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRedEnvelopeId()
	{
		return __pbn__RedEnvelopeId.HasValue;
	}

	public void ResetRedEnvelopeId()
	{
		__pbn__RedEnvelopeId = null;
	}

	public bool ShouldSerializeExpireTime()
	{
		return __pbn__ExpireTime.HasValue;
	}

	public void ResetExpireTime()
	{
		__pbn__ExpireTime = null;
	}

	public bool ShouldSerializeSenderUid()
	{
		return __pbn__SenderUid.HasValue;
	}

	public void ResetSenderUid()
	{
		__pbn__SenderUid = null;
	}

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}
}
