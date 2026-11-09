using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SendChatRedEnvelopeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1163
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ItemId;

	private uint? __pbn__Channel;

	private string __pbn__Msg;

	private string __pbn__Nickname;

	private uint? __pbn__ArmadaId;

	private uint? __pbn__RedEnvelopeId;

	[ProtoMember(1, Name = "item_id")]
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

	[ProtoMember(2, Name = "channel")]
	public uint Channel
	{
		get
		{
			return __pbn__Channel.GetValueOrDefault();
		}
		set
		{
			__pbn__Channel = value;
		}
	}

	[ProtoMember(3, Name = "msg")]
	[DefaultValue("")]
	public string Msg
	{
		get
		{
			return __pbn__Msg ?? "";
		}
		set
		{
			__pbn__Msg = value;
		}
	}

	[ProtoMember(4, Name = "nickname")]
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

	[ProtoMember(5, Name = "armada_id")]
	public uint ArmadaId
	{
		get
		{
			return __pbn__ArmadaId.GetValueOrDefault();
		}
		set
		{
			__pbn__ArmadaId = value;
		}
	}

	[ProtoMember(6, Name = "red_envelope_id")]
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

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}

	public bool ShouldSerializeChannel()
	{
		return __pbn__Channel.HasValue;
	}

	public void ResetChannel()
	{
		__pbn__Channel = null;
	}

	public bool ShouldSerializeMsg()
	{
		return __pbn__Msg != null;
	}

	public void ResetMsg()
	{
		__pbn__Msg = null;
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}

	public bool ShouldSerializeArmadaId()
	{
		return __pbn__ArmadaId.HasValue;
	}

	public void ResetArmadaId()
	{
		__pbn__ArmadaId = null;
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
