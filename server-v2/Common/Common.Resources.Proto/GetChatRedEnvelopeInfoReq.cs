using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetChatRedEnvelopeInfoReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1502
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SenderUid;

	private uint? __pbn__RedEnvelopeId;

	[ProtoMember(1, Name = "sender_uid")]
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

	[ProtoMember(2, Name = "red_envelope_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSenderUid()
	{
		return __pbn__SenderUid.HasValue;
	}

	public void ResetSenderUid()
	{
		__pbn__SenderUid = null;
	}

	public bool ShouldSerializeRedEnvelopeId()
	{
		return __pbn__RedEnvelopeId.HasValue;
	}

	public void ResetRedEnvelopeId()
	{
		__pbn__RedEnvelopeId = null;
	}
}
