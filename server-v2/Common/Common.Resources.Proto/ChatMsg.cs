using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatMsg : IExtensible
{
	[ProtoContract]
	public class SystemChatMsg : IExtensible
	{
		private IExtension __pbn__extensionData;

		private SystemChatMsgType? __pbn__Type;

		private uint? __pbn__ItemId;

		private uint? __pbn__ItemLevel;

		private uint? __pbn__ItemNum;

		private GachaType? __pbn__GachaType;

		[ProtoMember(1, Name = "type")]
		[DefaultValue(SystemChatMsgType.ChatMsgNone)]
		public SystemChatMsgType Type
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

		[ProtoMember(2, Name = "item_id")]
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

		[ProtoMember(3, Name = "item_level")]
		public uint ItemLevel
		{
			get
			{
				return __pbn__ItemLevel.GetValueOrDefault();
			}
			set
			{
				__pbn__ItemLevel = value;
			}
		}

		[ProtoMember(4, Name = "item_num")]
		public uint ItemNum
		{
			get
			{
				return __pbn__ItemNum.GetValueOrDefault();
			}
			set
			{
				__pbn__ItemNum = value;
			}
		}

		[ProtoMember(5, Name = "gacha_type")]
		[DefaultValue(GachaType.GachaHcoin)]
		public GachaType GachaType
		{
			get
			{
				return __pbn__GachaType ?? GachaType.GachaHcoin;
			}
			set
			{
				__pbn__GachaType = value;
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

		public bool ShouldSerializeItemId()
		{
			return __pbn__ItemId.HasValue;
		}

		public void ResetItemId()
		{
			__pbn__ItemId = null;
		}

		public bool ShouldSerializeItemLevel()
		{
			return __pbn__ItemLevel.HasValue;
		}

		public void ResetItemLevel()
		{
			__pbn__ItemLevel = null;
		}

		public bool ShouldSerializeItemNum()
		{
			return __pbn__ItemNum.HasValue;
		}

		public void ResetItemNum()
		{
			__pbn__ItemNum = null;
		}

		public bool ShouldSerializeGachaType()
		{
			return __pbn__GachaType.HasValue;
		}

		public void ResetGachaType()
		{
			__pbn__GachaType = null;
		}
	}

	[ProtoContract]
	public enum MsgChannel
	{
		[ProtoEnum(Name = "DEFAULT")]
		Default,
		[ProtoEnum(Name = "WORLD")]
		World,
		[ProtoEnum(Name = "FRIEND")]
		Friend,
		[ProtoEnum(Name = "ARMADA")]
		Armada,
		[ProtoEnum(Name = "PLAYER_CARD")]
		PlayerCard,
		[ProtoEnum(Name = "LOBBY")]
		Lobby,
		[ProtoEnum(Name = "MASTER_PUPIL")]
		MasterPupil,
		[ProtoEnum(Name = "COMMON_CHATWORLD")]
		CommonChatworld,
		[ProtoEnum(Name = "GROUP")]
		Group,
		[ProtoEnum(Name = "TEAM")]
		Team,
		[ProtoEnum(Name = "CAMPAIGN")]
		Campaign,
		[ProtoEnum(Name = "ROOM")]
		Room,
		[ProtoEnum(Name = "WORLD_ACTIVITY")]
		WorldActivity,
		[ProtoEnum(Name = "PRIVATE")]
		Private,
		[ProtoEnum(Name = "BATTLE")]
		Battle
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__Nickname;

	private uint? __pbn__Time;

	private string __pbn__Msg;

	private uint? __pbn__AvatarId;

	private uint? __pbn__DressId;

	private MsgChannel? __pbn__Channel;

	private uint? __pbn__FrameId;

	private bool? __pbn__IsSensitive;

	private bool? __pbn__IsGoback;

	private uint? __pbn__ChannelId;

	private uint? __pbn__CustomHeadId;

	private MasterPupilType? __pbn__MasterPupilTag;

	private uint? __pbn__TargetUid;

	private uint? __pbn__SeasonId;

	private uint? __pbn__SeasonGroupId;

	private uint? __pbn__SeasonLevel;

	private uint? __pbn__Mode;

	private uint? __pbn__ModePara;

	private bool? __pbn__IsArmadaLeader;

	private bool? __pbn__IsTeamLeader;

	private bool? __pbn__IsLobbyInvite;

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

	[ProtoMember(3, Name = "time")]
	public uint Time
	{
		get
		{
			return __pbn__Time.GetValueOrDefault();
		}
		set
		{
			__pbn__Time = value;
		}
	}

	[ProtoMember(4, Name = "msg")]
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

	[ProtoMember(5, Name = "avatar_id")]
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

	[ProtoMember(6, Name = "dress_id")]
	public uint DressId
	{
		get
		{
			return __pbn__DressId.GetValueOrDefault();
		}
		set
		{
			__pbn__DressId = value;
		}
	}

	[ProtoMember(7, Name = "content")]
	public ChatMsgContent Content { get; set; }

	[ProtoMember(8, Name = "channel")]
	[DefaultValue(MsgChannel.Default)]
	public MsgChannel Channel
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

	[ProtoMember(9, Name = "frame_id")]
	public uint FrameId
	{
		get
		{
			return __pbn__FrameId.GetValueOrDefault();
		}
		set
		{
			__pbn__FrameId = value;
		}
	}

	[ProtoMember(10, Name = "is_sensitive")]
	public bool IsSensitive
	{
		get
		{
			return __pbn__IsSensitive.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSensitive = value;
		}
	}

	[ProtoMember(11, Name = "is_goback")]
	public bool IsGoback
	{
		get
		{
			return __pbn__IsGoback.GetValueOrDefault();
		}
		set
		{
			__pbn__IsGoback = value;
		}
	}

	[ProtoMember(12, Name = "channel_id")]
	public uint ChannelId
	{
		get
		{
			return __pbn__ChannelId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChannelId = value;
		}
	}

	[ProtoMember(13, Name = "custom_head_id")]
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

	[ProtoMember(14, Name = "master_pupil_tag")]
	[DefaultValue(MasterPupilType.MasterPupilNoneType)]
	public MasterPupilType MasterPupilTag
	{
		get
		{
			return __pbn__MasterPupilTag.GetValueOrDefault();
		}
		set
		{
			__pbn__MasterPupilTag = value;
		}
	}

	[ProtoMember(15, Name = "system_msg")]
	public SystemChatMsg SystemMsg { get; set; }

	[ProtoMember(16, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	[ProtoMember(17, Name = "season_id")]
	public uint SeasonId
	{
		get
		{
			return __pbn__SeasonId.GetValueOrDefault();
		}
		set
		{
			__pbn__SeasonId = value;
		}
	}

	[ProtoMember(18, Name = "season_group_id")]
	public uint SeasonGroupId
	{
		get
		{
			return __pbn__SeasonGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__SeasonGroupId = value;
		}
	}

	[ProtoMember(19, Name = "season_level")]
	public uint SeasonLevel
	{
		get
		{
			return __pbn__SeasonLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__SeasonLevel = value;
		}
	}

	[ProtoMember(20, Name = "mode")]
	public uint Mode
	{
		get
		{
			return __pbn__Mode.GetValueOrDefault();
		}
		set
		{
			__pbn__Mode = value;
		}
	}

	[ProtoMember(21, Name = "mode_para")]
	public uint ModePara
	{
		get
		{
			return __pbn__ModePara.GetValueOrDefault();
		}
		set
		{
			__pbn__ModePara = value;
		}
	}

	[ProtoMember(22, Name = "is_armada_leader")]
	public bool IsArmadaLeader
	{
		get
		{
			return __pbn__IsArmadaLeader.GetValueOrDefault();
		}
		set
		{
			__pbn__IsArmadaLeader = value;
		}
	}

	[ProtoMember(23, Name = "is_team_leader")]
	public bool IsTeamLeader
	{
		get
		{
			return __pbn__IsTeamLeader.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTeamLeader = value;
		}
	}

	[ProtoMember(24, Name = "check_result")]
	public ChatMsgSensitiveCheckResult CheckResult { get; set; }

	[ProtoMember(25, Name = "is_lobby_invite")]
	public bool IsLobbyInvite
	{
		get
		{
			return __pbn__IsLobbyInvite.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLobbyInvite = value;
		}
	}

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

	public bool ShouldSerializeTime()
	{
		return __pbn__Time.HasValue;
	}

	public void ResetTime()
	{
		__pbn__Time = null;
	}

	public bool ShouldSerializeMsg()
	{
		return __pbn__Msg != null;
	}

	public void ResetMsg()
	{
		__pbn__Msg = null;
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeDressId()
	{
		return __pbn__DressId.HasValue;
	}

	public void ResetDressId()
	{
		__pbn__DressId = null;
	}

	public bool ShouldSerializeChannel()
	{
		return __pbn__Channel.HasValue;
	}

	public void ResetChannel()
	{
		__pbn__Channel = null;
	}

	public bool ShouldSerializeFrameId()
	{
		return __pbn__FrameId.HasValue;
	}

	public void ResetFrameId()
	{
		__pbn__FrameId = null;
	}

	public bool ShouldSerializeIsSensitive()
	{
		return __pbn__IsSensitive.HasValue;
	}

	public void ResetIsSensitive()
	{
		__pbn__IsSensitive = null;
	}

	public bool ShouldSerializeIsGoback()
	{
		return __pbn__IsGoback.HasValue;
	}

	public void ResetIsGoback()
	{
		__pbn__IsGoback = null;
	}

	public bool ShouldSerializeChannelId()
	{
		return __pbn__ChannelId.HasValue;
	}

	public void ResetChannelId()
	{
		__pbn__ChannelId = null;
	}

	public bool ShouldSerializeCustomHeadId()
	{
		return __pbn__CustomHeadId.HasValue;
	}

	public void ResetCustomHeadId()
	{
		__pbn__CustomHeadId = null;
	}

	public bool ShouldSerializeMasterPupilTag()
	{
		return __pbn__MasterPupilTag.HasValue;
	}

	public void ResetMasterPupilTag()
	{
		__pbn__MasterPupilTag = null;
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializeSeasonId()
	{
		return __pbn__SeasonId.HasValue;
	}

	public void ResetSeasonId()
	{
		__pbn__SeasonId = null;
	}

	public bool ShouldSerializeSeasonGroupId()
	{
		return __pbn__SeasonGroupId.HasValue;
	}

	public void ResetSeasonGroupId()
	{
		__pbn__SeasonGroupId = null;
	}

	public bool ShouldSerializeSeasonLevel()
	{
		return __pbn__SeasonLevel.HasValue;
	}

	public void ResetSeasonLevel()
	{
		__pbn__SeasonLevel = null;
	}

	public bool ShouldSerializeMode()
	{
		return __pbn__Mode.HasValue;
	}

	public void ResetMode()
	{
		__pbn__Mode = null;
	}

	public bool ShouldSerializeModePara()
	{
		return __pbn__ModePara.HasValue;
	}

	public void ResetModePara()
	{
		__pbn__ModePara = null;
	}

	public bool ShouldSerializeIsArmadaLeader()
	{
		return __pbn__IsArmadaLeader.HasValue;
	}

	public void ResetIsArmadaLeader()
	{
		__pbn__IsArmadaLeader = null;
	}

	public bool ShouldSerializeIsTeamLeader()
	{
		return __pbn__IsTeamLeader.HasValue;
	}

	public void ResetIsTeamLeader()
	{
		__pbn__IsTeamLeader = null;
	}

	public bool ShouldSerializeIsLobbyInvite()
	{
		return __pbn__IsLobbyInvite.HasValue;
	}

	public void ResetIsLobbyInvite()
	{
		__pbn__IsLobbyInvite = null;
	}
}
