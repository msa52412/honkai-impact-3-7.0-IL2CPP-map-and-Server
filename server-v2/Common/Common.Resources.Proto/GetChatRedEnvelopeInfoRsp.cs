using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetChatRedEnvelopeInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1503
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SenderUid;

	private uint? __pbn__RedEnvelopeId;

	private uint? __pbn__TakenNum;

	private uint? __pbn__ExpireTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

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

	[ProtoMember(3, Name = "red_envelope_id")]
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

	[ProtoMember(4, Name = "taken_num")]
	public uint TakenNum
	{
		get
		{
			return __pbn__TakenNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TakenNum = value;
		}
	}

	[ProtoMember(5, Name = "expire_time")]
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

	[ProtoMember(6, Name = "take_red_envelope_list")]
	public List<TakeChatRedEnvelopePlayer> TakeRedEnvelopeLists { get; } = new List<TakeChatRedEnvelopePlayer>();


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

	public bool ShouldSerializeTakenNum()
	{
		return __pbn__TakenNum.HasValue;
	}

	public void ResetTakenNum()
	{
		__pbn__TakenNum = null;
	}

	public bool ShouldSerializeExpireTime()
	{
		return __pbn__ExpireTime.HasValue;
	}

	public void ResetExpireTime()
	{
		__pbn__ExpireTime = null;
	}
}
