using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldMsgItem : IExtensible
{
	[ProtoContract]
	public enum ChatworldMsgType
	{
		[ProtoEnum(Name = "STR_MSG")]
		StrMsg = 1,
		[ProtoEnum(Name = "ENTER_CHATWORLD")]
		EnterChatworld = 2,
		[ProtoEnum(Name = "MOVE_IN_CHATWORLD")]
		MoveInChatworld = 3,
		[ProtoEnum(Name = "LEAVE_CHATWORLD")]
		LeaveChatworld = 4,
		[ProtoEnum(Name = "INT_MSG")]
		IntMsg = 5,
		[ProtoEnum(Name = "CLIENT_MSG")]
		ClientMsg = 6,
		[ProtoEnum(Name = "NORMAL_MSG")]
		NormalMsg = 7,
		[ProtoEnum(Name = "USE_ITEM")]
		UseItem = 8,
		[ProtoEnum(Name = "CHANGE_AVATAR")]
		ChangeAvatar = 9,
		[ProtoEnum(Name = "INTERACT_MSG")]
		InteractMsg = 10,
		[ProtoEnum(Name = "ACTION_MSG")]
		ActionMsg = 11,
		[ProtoEnum(Name = "SEND_RED_ENVELOPE")]
		SendRedEnvelope = 12,
		[ProtoEnum(Name = "RED_ENVELOPE_TAKEN_OVER")]
		RedEnvelopeTakenOver = 13,
		[ProtoEnum(Name = "TREASURE_MSG")]
		TreasureMsg = 14,
		[ProtoEnum(Name = "SKILL_ATTACK_MSG")]
		SkillAttackMsg = 15,
		[ProtoEnum(Name = "SKILL_DAMAGE_MSG")]
		SkillDamageMsg = 16,
		[ProtoEnum(Name = "PLAYER_REBORN_MSG")]
		PlayerRebornMsg = 17,
		[ProtoEnum(Name = "BUFF_MSG")]
		BuffMsg = 18,
		[ProtoEnum(Name = "SKILL_CANCEL_MSG")]
		SkillCancelMsg = 20,
		[ProtoEnum(Name = "PLAYER_BATTLE_MSG")]
		PlayerBattleMsg = 21,
		[ProtoEnum(Name = "ENTITY_MSG")]
		EntityMsg = 23,
		[ProtoEnum(Name = "ENTITY_HP_MSG")]
		EntityHpMsg = 24
	}

	private IExtension __pbn__extensionData;

	private ChatworldMsgType? __pbn__Type;

	private int? __pbn__MsgInt;

	private byte[] __pbn__ClientMsg;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(ChatworldMsgType.StrMsg)]
	public ChatworldMsgType Type
	{
		get
		{
			return __pbn__Type ?? ChatworldMsgType.StrMsg;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "player_info")]
	public ChatworldEntity PlayerInfo { get; set; }

	[ProtoMember(5, Name = "msg_int")]
	public int MsgInt
	{
		get
		{
			return __pbn__MsgInt.GetValueOrDefault();
		}
		set
		{
			__pbn__MsgInt = value;
		}
	}

	[ProtoMember(6, Name = "client_msg")]
	public byte[] ClientMsg
	{
		get
		{
			return __pbn__ClientMsg;
		}
		set
		{
			__pbn__ClientMsg = value;
		}
	}

	[ProtoMember(7, Name = "normal_msg")]
	public ChatMsgItem NormalMsg { get; set; }

	[ProtoMember(8, Name = "use_item_msg")]
	public ChatworldMsgUseItem UseItemMsg { get; set; }

	[ProtoMember(10, Name = "interact_msg")]
	public ChatworldMsgInteract InteractMsg { get; set; }

	[ProtoMember(11, Name = "action_msg")]
	public ChatworldMsgAction ActionMsg { get; set; }

	[ProtoMember(12, Name = "red_envelope_msg")]
	public CommonChatworldRedEnvelopeMsg RedEnvelopeMsg { get; set; }

	[ProtoMember(13, Name = "chatworld_treasure_msg")]
	public ChatworldTreasureMsg ChatworldTreasureMsg { get; set; }

	[ProtoMember(14, Name = "skill_attack_msg")]
	public ChatworldSkillAttackMsg SkillAttackMsg { get; set; }

	[ProtoMember(15, Name = "skill_damage_msg")]
	public ChatworldSkillDamageMsg SkillDamageMsg { get; set; }

	[ProtoMember(17, Name = "skill_cancel_msg")]
	public ChatworldSkillCancelMsg SkillCancelMsg { get; set; }

	[ProtoMember(20, Name = "entity_msg")]
	public ChatworldEntityMsg EntityMsg { get; set; }

	[ProtoMember(21, Name = "entity_hp_msg")]
	public ChatworldEntityHp EntityHpMsg { get; set; }

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

	public bool ShouldSerializeMsgInt()
	{
		return __pbn__MsgInt.HasValue;
	}

	public void ResetMsgInt()
	{
		__pbn__MsgInt = null;
	}

	public bool ShouldSerializeClientMsg()
	{
		return __pbn__ClientMsg != null;
	}

	public void ResetClientMsg()
	{
		__pbn__ClientMsg = null;
	}
}
