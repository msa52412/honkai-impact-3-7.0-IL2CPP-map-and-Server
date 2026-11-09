using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpTeamMember : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Index;

	private uint? __pbn__Uid;

	private uint? __pbn__MpExp;

	private uint? __pbn__Stamina;

	private uint? __pbn__HeadAvatarId;

	private uint? __pbn__DressId;

	private uint? __pbn__PunishEndTime;

	private uint? __pbn__VoiceId;

	private LobbyMemberStatus? __pbn__Status;

	private LobbyClientStatus? __pbn__ClientStatus;

	private uint? __pbn__AvatarTrialId;

	private bool? __pbn__IsWild;

	private string __pbn__RegionName;

	private uint? __pbn__FrameId;

	private uint? __pbn__EnterType;

	private uint? __pbn__CustomHeadId;

	private uint? __pbn__NewbieId;

	private uint? __pbn__DataVersion;

	private uint? __pbn__SeasonId;

	private uint? __pbn__SeasonGroupId;

	private uint? __pbn__SeasonLevel;

	private uint? __pbn__ExtraSkillId;

	[ProtoMember(1, Name = "index")]
	public uint Index
	{
		get
		{
			return __pbn__Index.GetValueOrDefault();
		}
		set
		{
			__pbn__Index = value;
		}
	}

	[ProtoMember(2, Name = "uid")]
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

	[ProtoMember(3, Name = "mp_exp")]
	public uint MpExp
	{
		get
		{
			return __pbn__MpExp.GetValueOrDefault();
		}
		set
		{
			__pbn__MpExp = value;
		}
	}

	[ProtoMember(4, Name = "stamina")]
	public uint Stamina
	{
		get
		{
			return __pbn__Stamina.GetValueOrDefault();
		}
		set
		{
			__pbn__Stamina = value;
		}
	}

	[ProtoMember(5, Name = "skill_list")]
	public List<MpSkill> SkillLists { get; } = new List<MpSkill>();


	[ProtoMember(6, Name = "head_avatar_id")]
	public uint HeadAvatarId
	{
		get
		{
			return __pbn__HeadAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__HeadAvatarId = value;
		}
	}

	[ProtoMember(7, Name = "dress_id")]
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

	[ProtoMember(8, Name = "punish_end_time")]
	public uint PunishEndTime
	{
		get
		{
			return __pbn__PunishEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__PunishEndTime = value;
		}
	}

	[ProtoMember(9, Name = "voice_id")]
	public uint VoiceId
	{
		get
		{
			return __pbn__VoiceId.GetValueOrDefault();
		}
		set
		{
			__pbn__VoiceId = value;
		}
	}

	[ProtoMember(11, Name = "member_info")]
	public MpTeamMemberInfo MemberInfo { get; set; }

	[ProtoMember(12, Name = "status")]
	[DefaultValue(LobbyMemberStatus.LobbyMemberUnready)]
	public LobbyMemberStatus Status
	{
		get
		{
			return __pbn__Status ?? LobbyMemberStatus.LobbyMemberUnready;
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(13, Name = "client_status")]
	[DefaultValue(LobbyClientStatus.LobbyClientNone)]
	public LobbyClientStatus ClientStatus
	{
		get
		{
			return __pbn__ClientStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__ClientStatus = value;
		}
	}

	[ProtoMember(14, Name = "avatar_trial_id")]
	public uint AvatarTrialId
	{
		get
		{
			return __pbn__AvatarTrialId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarTrialId = value;
		}
	}

	[ProtoMember(15, Name = "is_wild")]
	public bool IsWild
	{
		get
		{
			return __pbn__IsWild.GetValueOrDefault();
		}
		set
		{
			__pbn__IsWild = value;
		}
	}

	[ProtoMember(17, Name = "reclaim_avatar_info")]
	public ReclaimMpAvatar ReclaimAvatarInfo { get; set; }

	[ProtoMember(18, Name = "region_name")]
	[DefaultValue("")]
	public string RegionName
	{
		get
		{
			return __pbn__RegionName ?? "";
		}
		set
		{
			__pbn__RegionName = value;
		}
	}

	[ProtoMember(19, Name = "openworld_boss_info")]
	public OpenworldBossMp OpenworldBossInfo { get; set; }

	[ProtoMember(20, Name = "frame_id")]
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

	[ProtoMember(21, Name = "enter_type")]
	public uint EnterType
	{
		get
		{
			return __pbn__EnterType.GetValueOrDefault();
		}
		set
		{
			__pbn__EnterType = value;
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

	[ProtoMember(23, Name = "newbie_id")]
	public uint NewbieId
	{
		get
		{
			return __pbn__NewbieId.GetValueOrDefault();
		}
		set
		{
			__pbn__NewbieId = value;
		}
	}

	[ProtoMember(24, Name = "data_version")]
	public uint DataVersion
	{
		get
		{
			return __pbn__DataVersion.GetValueOrDefault();
		}
		set
		{
			__pbn__DataVersion = value;
		}
	}

	[ProtoMember(25, Name = "season_id")]
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

	[ProtoMember(26, Name = "season_group_id")]
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

	[ProtoMember(27, Name = "season_level")]
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

	[ProtoMember(28, Name = "extra_skill_id")]
	public uint ExtraSkillId
	{
		get
		{
			return __pbn__ExtraSkillId.GetValueOrDefault();
		}
		set
		{
			__pbn__ExtraSkillId = value;
		}
	}

	[ProtoMember(29, Name = "sc_dlc_avatar_info")]
	public ScDLCMpAvatar ScDlcAvatarInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIndex()
	{
		return __pbn__Index.HasValue;
	}

	public void ResetIndex()
	{
		__pbn__Index = null;
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeMpExp()
	{
		return __pbn__MpExp.HasValue;
	}

	public void ResetMpExp()
	{
		__pbn__MpExp = null;
	}

	public bool ShouldSerializeStamina()
	{
		return __pbn__Stamina.HasValue;
	}

	public void ResetStamina()
	{
		__pbn__Stamina = null;
	}

	public bool ShouldSerializeHeadAvatarId()
	{
		return __pbn__HeadAvatarId.HasValue;
	}

	public void ResetHeadAvatarId()
	{
		__pbn__HeadAvatarId = null;
	}

	public bool ShouldSerializeDressId()
	{
		return __pbn__DressId.HasValue;
	}

	public void ResetDressId()
	{
		__pbn__DressId = null;
	}

	public bool ShouldSerializePunishEndTime()
	{
		return __pbn__PunishEndTime.HasValue;
	}

	public void ResetPunishEndTime()
	{
		__pbn__PunishEndTime = null;
	}

	public bool ShouldSerializeVoiceId()
	{
		return __pbn__VoiceId.HasValue;
	}

	public void ResetVoiceId()
	{
		__pbn__VoiceId = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeClientStatus()
	{
		return __pbn__ClientStatus.HasValue;
	}

	public void ResetClientStatus()
	{
		__pbn__ClientStatus = null;
	}

	public bool ShouldSerializeAvatarTrialId()
	{
		return __pbn__AvatarTrialId.HasValue;
	}

	public void ResetAvatarTrialId()
	{
		__pbn__AvatarTrialId = null;
	}

	public bool ShouldSerializeIsWild()
	{
		return __pbn__IsWild.HasValue;
	}

	public void ResetIsWild()
	{
		__pbn__IsWild = null;
	}

	public bool ShouldSerializeRegionName()
	{
		return __pbn__RegionName != null;
	}

	public void ResetRegionName()
	{
		__pbn__RegionName = null;
	}

	public bool ShouldSerializeFrameId()
	{
		return __pbn__FrameId.HasValue;
	}

	public void ResetFrameId()
	{
		__pbn__FrameId = null;
	}

	public bool ShouldSerializeEnterType()
	{
		return __pbn__EnterType.HasValue;
	}

	public void ResetEnterType()
	{
		__pbn__EnterType = null;
	}

	public bool ShouldSerializeCustomHeadId()
	{
		return __pbn__CustomHeadId.HasValue;
	}

	public void ResetCustomHeadId()
	{
		__pbn__CustomHeadId = null;
	}

	public bool ShouldSerializeNewbieId()
	{
		return __pbn__NewbieId.HasValue;
	}

	public void ResetNewbieId()
	{
		__pbn__NewbieId = null;
	}

	public bool ShouldSerializeDataVersion()
	{
		return __pbn__DataVersion.HasValue;
	}

	public void ResetDataVersion()
	{
		__pbn__DataVersion = null;
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

	public bool ShouldSerializeExtraSkillId()
	{
		return __pbn__ExtraSkillId.HasValue;
	}

	public void ResetExtraSkillId()
	{
		__pbn__ExtraSkillId = null;
	}
}
