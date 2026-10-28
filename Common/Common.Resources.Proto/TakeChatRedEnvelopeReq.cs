using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeChatRedEnvelopeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1165
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Channel;

	private uint? __pbn__SenderUid;

	private uint? __pbn__RedEnvelopeId;

	private uint? __pbn__ItemId;

	private uint? __pbn__ArmadaId;

	private bool? __pbn__IsSenderNpc;

	private string __pbn__Nickname;

	[ProtoMember(1, Name = "channel")]
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

	[ProtoMember(6, Name = "is_sender_npc")]
	public bool IsSenderNpc
	{
		get
		{
			return __pbn__IsSenderNpc.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSenderNpc = value;
		}
	}

	[ProtoMember(7, Name = "nickname")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChannel()
	{
		return __pbn__Channel.HasValue;
	}

	public void ResetChannel()
	{
		__pbn__Channel = null;
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

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}

	public bool ShouldSerializeArmadaId()
	{
		return __pbn__ArmadaId.HasValue;
	}

	public void ResetArmadaId()
	{
		__pbn__ArmadaId = null;
	}

	public bool ShouldSerializeIsSenderNpc()
	{
		return __pbn__IsSenderNpc.HasValue;
	}

	public void ResetIsSenderNpc()
	{
		__pbn__IsSenderNpc = null;
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}
}
