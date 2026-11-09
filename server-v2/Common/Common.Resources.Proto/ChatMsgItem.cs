using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatMsgItem : IExtensible
{
	[ProtoContract]
	public enum ChatMsgType
	{
		[ProtoEnum(Name = "STR_MSG")]
		StrMsg = 1,
		[ProtoEnum(Name = "WEAPON_MSG")]
		WeaponMsg,
		[ProtoEnum(Name = "STIGMATA_MSG")]
		StigmataMsg,
		[ProtoEnum(Name = "AVATAR_MSG")]
		AvatarMsg,
		[ProtoEnum(Name = "LOBBY_MSG")]
		LobbyMsg,
		[ProtoEnum(Name = "INTERNAL_MSG")]
		InternalMsg,
		[ProtoEnum(Name = "SEND_RED_ENVELOPE_MSG")]
		SendRedEnvelopeMsg,
		[ProtoEnum(Name = "TAKE_RED_ENVELOPE_MSG")]
		TakeRedEnvelopeMsg,
		[ProtoEnum(Name = "EMOJI")]
		Emoji,
		[ProtoEnum(Name = "ELF_MSG")]
		ElfMsg,
		[ProtoEnum(Name = "RED_ENVELOPE_TAKE_OVER_MSG")]
		RedEnvelopeTakeOverMsg,
		[ProtoEnum(Name = "CHATWORLD_BEAST_MSG")]
		ChatworldBeastMsg
	}

	private IExtension __pbn__extensionData;

	private ChatMsgType? __pbn__Type;

	private string __pbn__MsgStr;

	private uint? __pbn__EmojiId;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(ChatMsgType.StrMsg)]
	public ChatMsgType Type
	{
		get
		{
			return __pbn__Type ?? ChatMsgType.StrMsg;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "msg_str")]
	[DefaultValue("")]
	public string MsgStr
	{
		get
		{
			return __pbn__MsgStr ?? "";
		}
		set
		{
			__pbn__MsgStr = value;
		}
	}

	[ProtoMember(3, Name = "weapon")]
	public ChatMsgWeapon Weapon { get; set; }

	[ProtoMember(4, Name = "stigmata")]
	public ChatMsgStigmata Stigmata { get; set; }

	[ProtoMember(5, Name = "avatar")]
	public ChatMsgAvatar Avatar { get; set; }

	[ProtoMember(6, Name = "lobby")]
	public ChatMsgLobby Lobby { get; set; }

	[ProtoMember(7, Name = "internal")]
	public ChatMsgInternal Internal { get; set; }

	[ProtoMember(8, Name = "send_red_envelope")]
	public ChatSendRedEnvelope SendRedEnvelope { get; set; }

	[ProtoMember(9, Name = "take_red_envelope")]
	public ChatTakeRedEnvelope TakeRedEnvelope { get; set; }

	[ProtoMember(10, Name = "emoji_id")]
	public uint EmojiId
	{
		get
		{
			return __pbn__EmojiId.GetValueOrDefault();
		}
		set
		{
			__pbn__EmojiId = value;
		}
	}

	[ProtoMember(11, Name = "elf")]
	public ChatMsgElf Elf { get; set; }

	[ProtoMember(12, Name = "red_envelope_take_over")]
	public ChatRedEnvelopeTakeOver RedEnvelopeTakeOver { get; set; }

	[ProtoMember(13, Name = "chatworld_beast")]
	public ChatMsgWorldBeast ChatworldBeast { get; set; }

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

	public bool ShouldSerializeMsgStr()
	{
		return __pbn__MsgStr != null;
	}

	public void ResetMsgStr()
	{
		__pbn__MsgStr = null;
	}

	public bool ShouldSerializeEmojiId()
	{
		return __pbn__EmojiId.HasValue;
	}

	public void ResetEmojiId()
	{
		__pbn__EmojiId = null;
	}
}
