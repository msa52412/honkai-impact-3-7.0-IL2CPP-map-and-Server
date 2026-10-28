using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CommonChatworldRedEnvelopeMsg : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SenderUid;

	private uint? __pbn__Channel;

	private bool? __pbn__IsSenderNpc;

	private string __pbn__Msg;

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

	[ProtoMember(3, Name = "is_sender_npc")]
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

	[ProtoMember(4, Name = "chatworld_red_envelope")]
	public CommonChatworldRedEnvelope ChatworldRedEnvelope { get; set; }

	[ProtoMember(5, Name = "msg")]
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

	public bool ShouldSerializeChannel()
	{
		return __pbn__Channel.HasValue;
	}

	public void ResetChannel()
	{
		__pbn__Channel = null;
	}

	public bool ShouldSerializeIsSenderNpc()
	{
		return __pbn__IsSenderNpc.HasValue;
	}

	public void ResetIsSenderNpc()
	{
		__pbn__IsSenderNpc = null;
	}

	public bool ShouldSerializeMsg()
	{
		return __pbn__Msg != null;
	}

	public void ResetMsg()
	{
		__pbn__Msg = null;
	}
}
