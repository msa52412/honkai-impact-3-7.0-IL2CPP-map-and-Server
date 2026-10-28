using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChangePlayerCardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 482
	}

	private IExtension __pbn__extensionData;

	private PlayerCardType? __pbn__Type;

	private PlayerCardMsgConfig? __pbn__MsgConfig;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(PlayerCardType.CardAll)]
	public PlayerCardType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "slot_list")]
	public List<PlayerCardSlot> SlotLists { get; } = new List<PlayerCardSlot>();


	[ProtoMember(3, Name = "msg_config")]
	[DefaultValue(PlayerCardMsgConfig.CardMsgAll)]
	public PlayerCardMsgConfig MsgConfig
	{
		get
		{
			return __pbn__MsgConfig.GetValueOrDefault();
		}
		set
		{
			__pbn__MsgConfig = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeMsgConfig()
	{
		return __pbn__MsgConfig.HasValue;
	}

	public void ResetMsgConfig()
	{
		__pbn__MsgConfig = null;
	}
}
