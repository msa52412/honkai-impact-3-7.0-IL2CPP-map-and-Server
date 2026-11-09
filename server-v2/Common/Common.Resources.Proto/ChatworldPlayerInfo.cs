using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldPlayerInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__Nickname;

	private uint? __pbn__AvatarId;

	private uint? __pbn__ChatDressId;

	private uint? __pbn__ChatAvatarId;

	private uint? __pbn__ChatFrameId;

	private uint? __pbn__Level;

	private string __pbn__ArmadaName;

	private uint? __pbn__MpLevel;

	private uint? __pbn__OnMedalId;

	private uint? __pbn__ActionId;

	private bool? __pbn__IsAllInfo;

	private uint? __pbn__LeftHp;

	private uint? __pbn__NextRebornTime;

	private uint? __pbn__RuntimeTag;

	private uint? __pbn__CustomHeadId;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "nickname")]
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

	[ProtoMember(3, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(4, Name = "chat_dress_id")]
	public uint ChatDressId
	{
		get
		{
			return __pbn__ChatDressId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatDressId = value;
		}
	}

	[ProtoMember(5, Name = "position")]
	public ChatworldPosition Position { get; set; }

	[ProtoMember(6, Name = "chat_avatar_id")]
	public uint ChatAvatarId
	{
		get
		{
			return __pbn__ChatAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatAvatarId = value;
		}
	}

	[ProtoMember(7, Name = "chat_frame_id")]
	public uint ChatFrameId
	{
		get
		{
			return __pbn__ChatFrameId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatFrameId = value;
		}
	}

	[ProtoMember(8, Name = "holding_list")]
	public uint[] HoldingLists { get; set; }

	[ProtoMember(9, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(10, Name = "armada_name")]
	[DefaultValue("")]
	public string ArmadaName
	{
		get
		{
			return __pbn__ArmadaName ?? "";
		}
		set
		{
			__pbn__ArmadaName = value;
		}
	}

	[ProtoMember(11, Name = "mp_level")]
	public uint MpLevel
	{
		get
		{
			return __pbn__MpLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MpLevel = value;
		}
	}

	[ProtoMember(12, Name = "on_medal_id")]
	public uint OnMedalId
	{
		get
		{
			return __pbn__OnMedalId.GetValueOrDefault();
		}
		set
		{
			__pbn__OnMedalId = value;
		}
	}

	[ProtoMember(13, Name = "action_id")]
	public uint ActionId
	{
		get
		{
			return __pbn__ActionId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActionId = value;
		}
	}

	[ProtoMember(14, Name = "chatworld_red_envelope_list")]
	public List<CommonChatworldRedEnvelope> ChatworldRedEnvelopeLists { get; } = new List<CommonChatworldRedEnvelope>();


	[ProtoMember(15, Name = "is_all_info")]
	public bool IsAllInfo
	{
		get
		{
			return __pbn__IsAllInfo.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAllInfo = value;
		}
	}

	[ProtoMember(16, Name = "left_hp")]
	public uint LeftHp
	{
		get
		{
			return __pbn__LeftHp.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftHp = value;
		}
	}

	[ProtoMember(17, Name = "next_reborn_time")]
	public uint NextRebornTime
	{
		get
		{
			return __pbn__NextRebornTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextRebornTime = value;
		}
	}

	[ProtoMember(18, Name = "buff_list")]
	public List<ChatworldBuff> BuffLists { get; } = new List<ChatworldBuff>();


	[ProtoMember(20, Name = "runtime_tag")]
	public uint RuntimeTag
	{
		get
		{
			return __pbn__RuntimeTag.GetValueOrDefault();
		}
		set
		{
			__pbn__RuntimeTag = value;
		}
	}

	[ProtoMember(22, Name = "custom_head_id")]
	public uint CustomHeadId
	{
		get
		{
			return __pbn__CustomHeadId.GetValueOrDefault();
		}
		set
		{
			__pbn__CustomHeadId = value;
		}
	}

	[ProtoMember(23, Name = "on_medal")]
	public Medal OnMedal { get; set; }

	[ProtoMember(32, Name = "battle_info")]
	public ChatworldBattle BattleInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeChatDressId()
	{
		return __pbn__ChatDressId.HasValue;
	}

	public void ResetChatDressId()
	{
		__pbn__ChatDressId = null;
	}

	public bool ShouldSerializeChatAvatarId()
	{
		return __pbn__ChatAvatarId.HasValue;
	}

	public void ResetChatAvatarId()
	{
		__pbn__ChatAvatarId = null;
	}

	public bool ShouldSerializeChatFrameId()
	{
		return __pbn__ChatFrameId.HasValue;
	}

	public void ResetChatFrameId()
	{
		__pbn__ChatFrameId = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeArmadaName()
	{
		return __pbn__ArmadaName != null;
	}

	public void ResetArmadaName()
	{
		__pbn__ArmadaName = null;
	}

	public bool ShouldSerializeMpLevel()
	{
		return __pbn__MpLevel.HasValue;
	}

	public void ResetMpLevel()
	{
		__pbn__MpLevel = null;
	}

	public bool ShouldSerializeOnMedalId()
	{
		return __pbn__OnMedalId.HasValue;
	}

	public void ResetOnMedalId()
	{
		__pbn__OnMedalId = null;
	}

	public bool ShouldSerializeActionId()
	{
		return __pbn__ActionId.HasValue;
	}

	public void ResetActionId()
	{
		__pbn__ActionId = null;
	}

	public bool ShouldSerializeIsAllInfo()
	{
		return __pbn__IsAllInfo.HasValue;
	}

	public void ResetIsAllInfo()
	{
		__pbn__IsAllInfo = null;
	}

	public bool ShouldSerializeLeftHp()
	{
		return __pbn__LeftHp.HasValue;
	}

	public void ResetLeftHp()
	{
		__pbn__LeftHp = null;
	}

	public bool ShouldSerializeNextRebornTime()
	{
		return __pbn__NextRebornTime.HasValue;
	}

	public void ResetNextRebornTime()
	{
		__pbn__NextRebornTime = null;
	}

	public bool ShouldSerializeRuntimeTag()
	{
		return __pbn__RuntimeTag.HasValue;
	}

	public void ResetRuntimeTag()
	{
		__pbn__RuntimeTag = null;
	}

	public bool ShouldSerializeCustomHeadId()
	{
		return __pbn__CustomHeadId.HasValue;
	}

	public void ResetCustomHeadId()
	{
		__pbn__CustomHeadId = null;
	}
}
