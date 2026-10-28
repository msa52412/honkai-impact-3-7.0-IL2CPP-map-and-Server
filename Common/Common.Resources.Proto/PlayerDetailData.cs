using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlayerDetailData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__Nickname;

	private uint? __pbn__Level;

	private string __pbn__SelfDesc;

	private uint? __pbn__ArmadaId;

	private string __pbn__ArmadaName;

	private uint? __pbn__MpLevel;

	private uint? __pbn__OnMedalId;

	private bool? __pbn__IsNewbie;

	private bool? __pbn__IsGoback;

	private uint? __pbn__MpLikeNum;

	private uint? __pbn__CustomHeadId;

	private uint? __pbn__FrameId;

	private bool? __pbn__IsAllowVisit;

	private uint? __pbn__VisitAvatar;

	private uint? __pbn__BeLikedNum;

	private uint? __pbn__ShowHouse;

	private uint? __pbn__ComfortValue;

	private uint? __pbn__HouseLevel;

	private string __pbn__HouseName;

	private uint? __pbn__TotalWikiScore;

	private uint? __pbn__TotalAchievementNumGet;

	private uint? __pbn__TotalAchievementNum;

	private uint? __pbn__ChatForbidFinishTime;

	private uint? __pbn__MasterUid;

	private MasterPupilType? __pbn__MasterPupilTag;

	private uint? __pbn__SeasonId;

	private uint? __pbn__SeasonGroupId;

	private uint? __pbn__SeasonLevel;

	private bool? __pbn__IsLogoff;

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

	[ProtoMember(3, Name = "level")]
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

	[ProtoMember(4, Name = "leader_avatar")]
	public AvatarDetailData LeaderAvatar { get; set; }

	[ProtoMember(5, Name = "self_desc")]
	[DefaultValue("")]
	public string SelfDesc
	{
		get
		{
			return __pbn__SelfDesc ?? "";
		}
		set
		{
			__pbn__SelfDesc = value;
		}
	}

	[ProtoMember(6, Name = "armada_id")]
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

	[ProtoMember(7, Name = "armada_name")]
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

	[ProtoMember(8, Name = "mp_level")]
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

	[ProtoMember(9, Name = "on_medal_id")]
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

	[ProtoMember(10, Name = "is_newbie")]
	public bool IsNewbie
	{
		get
		{
			return __pbn__IsNewbie.GetValueOrDefault();
		}
		set
		{
			__pbn__IsNewbie = value;
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

	[ProtoMember(12, Name = "mp_like_num")]
	public uint MpLikeNum
	{
		get
		{
			return __pbn__MpLikeNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MpLikeNum = value;
		}
	}

	[ProtoMember(13, Name = "grand_key_list")]
	public List<GrandKey> GrandKeyLists { get; } = new List<GrandKey>();


	[ProtoMember(14, Name = "leader_elf")]
	public ElfDetailData LeaderElf { get; set; }

	[ProtoMember(15, Name = "custom_head_id")]
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

	[ProtoMember(16, Name = "frame_id")]
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

	[ProtoMember(17, Name = "adventure")]
	public PlayerAdventureData Adventure { get; set; }

	[ProtoMember(18, Name = "backup_avatar_list")]
	public List<AvatarDetailData> BackupAvatarLists { get; } = new List<AvatarDetailData>();


	[ProtoMember(19, Name = "on_medal")]
	public Medal OnMedal { get; set; }

	[ProtoMember(30, Name = "is_allow_visit")]
	public bool IsAllowVisit
	{
		get
		{
			return __pbn__IsAllowVisit.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAllowVisit = value;
		}
	}

	[ProtoMember(31, Name = "visit_avatar")]
	public uint VisitAvatar
	{
		get
		{
			return __pbn__VisitAvatar.GetValueOrDefault();
		}
		set
		{
			__pbn__VisitAvatar = value;
		}
	}

	[ProtoMember(32, Name = "be_liked_num")]
	public uint BeLikedNum
	{
		get
		{
			return __pbn__BeLikedNum.GetValueOrDefault();
		}
		set
		{
			__pbn__BeLikedNum = value;
		}
	}

	[ProtoMember(33, Name = "show_house")]
	public uint ShowHouse
	{
		get
		{
			return __pbn__ShowHouse.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowHouse = value;
		}
	}

	[ProtoMember(34, Name = "comfort_value")]
	public uint ComfortValue
	{
		get
		{
			return __pbn__ComfortValue.GetValueOrDefault();
		}
		set
		{
			__pbn__ComfortValue = value;
		}
	}

	[ProtoMember(35, Name = "house_level")]
	public uint HouseLevel
	{
		get
		{
			return __pbn__HouseLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HouseLevel = value;
		}
	}

	[ProtoMember(36, Name = "house_name")]
	[DefaultValue("")]
	public string HouseName
	{
		get
		{
			return __pbn__HouseName ?? "";
		}
		set
		{
			__pbn__HouseName = value;
		}
	}

	[ProtoMember(37, Name = "total_wiki_score")]
	public uint TotalWikiScore
	{
		get
		{
			return __pbn__TotalWikiScore.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalWikiScore = value;
		}
	}

	[ProtoMember(38, Name = "total_achievement_num_get")]
	public uint TotalAchievementNumGet
	{
		get
		{
			return __pbn__TotalAchievementNumGet.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalAchievementNumGet = value;
		}
	}

	[ProtoMember(39, Name = "total_achievement_num")]
	public uint TotalAchievementNum
	{
		get
		{
			return __pbn__TotalAchievementNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalAchievementNum = value;
		}
	}

	[ProtoMember(40, Name = "chat_forbid_finish_time")]
	public uint ChatForbidFinishTime
	{
		get
		{
			return __pbn__ChatForbidFinishTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatForbidFinishTime = value;
		}
	}

	[ProtoMember(41, Name = "master_uid")]
	public uint MasterUid
	{
		get
		{
			return __pbn__MasterUid.GetValueOrDefault();
		}
		set
		{
			__pbn__MasterUid = value;
		}
	}

	[ProtoMember(42, Name = "master_pupil_tag")]
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

	[ProtoMember(43, Name = "season_id")]
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

	[ProtoMember(44, Name = "season_group_id")]
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

	[ProtoMember(45, Name = "season_level")]
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

	[ProtoMember(46, Name = "is_logoff")]
	public bool IsLogoff
	{
		get
		{
			return __pbn__IsLogoff.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLogoff = value;
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

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeSelfDesc()
	{
		return __pbn__SelfDesc != null;
	}

	public void ResetSelfDesc()
	{
		__pbn__SelfDesc = null;
	}

	public bool ShouldSerializeArmadaId()
	{
		return __pbn__ArmadaId.HasValue;
	}

	public void ResetArmadaId()
	{
		__pbn__ArmadaId = null;
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

	public bool ShouldSerializeIsNewbie()
	{
		return __pbn__IsNewbie.HasValue;
	}

	public void ResetIsNewbie()
	{
		__pbn__IsNewbie = null;
	}

	public bool ShouldSerializeIsGoback()
	{
		return __pbn__IsGoback.HasValue;
	}

	public void ResetIsGoback()
	{
		__pbn__IsGoback = null;
	}

	public bool ShouldSerializeMpLikeNum()
	{
		return __pbn__MpLikeNum.HasValue;
	}

	public void ResetMpLikeNum()
	{
		__pbn__MpLikeNum = null;
	}

	public bool ShouldSerializeCustomHeadId()
	{
		return __pbn__CustomHeadId.HasValue;
	}

	public void ResetCustomHeadId()
	{
		__pbn__CustomHeadId = null;
	}

	public bool ShouldSerializeFrameId()
	{
		return __pbn__FrameId.HasValue;
	}

	public void ResetFrameId()
	{
		__pbn__FrameId = null;
	}

	public bool ShouldSerializeIsAllowVisit()
	{
		return __pbn__IsAllowVisit.HasValue;
	}

	public void ResetIsAllowVisit()
	{
		__pbn__IsAllowVisit = null;
	}

	public bool ShouldSerializeVisitAvatar()
	{
		return __pbn__VisitAvatar.HasValue;
	}

	public void ResetVisitAvatar()
	{
		__pbn__VisitAvatar = null;
	}

	public bool ShouldSerializeBeLikedNum()
	{
		return __pbn__BeLikedNum.HasValue;
	}

	public void ResetBeLikedNum()
	{
		__pbn__BeLikedNum = null;
	}

	public bool ShouldSerializeShowHouse()
	{
		return __pbn__ShowHouse.HasValue;
	}

	public void ResetShowHouse()
	{
		__pbn__ShowHouse = null;
	}

	public bool ShouldSerializeComfortValue()
	{
		return __pbn__ComfortValue.HasValue;
	}

	public void ResetComfortValue()
	{
		__pbn__ComfortValue = null;
	}

	public bool ShouldSerializeHouseLevel()
	{
		return __pbn__HouseLevel.HasValue;
	}

	public void ResetHouseLevel()
	{
		__pbn__HouseLevel = null;
	}

	public bool ShouldSerializeHouseName()
	{
		return __pbn__HouseName != null;
	}

	public void ResetHouseName()
	{
		__pbn__HouseName = null;
	}

	public bool ShouldSerializeTotalWikiScore()
	{
		return __pbn__TotalWikiScore.HasValue;
	}

	public void ResetTotalWikiScore()
	{
		__pbn__TotalWikiScore = null;
	}

	public bool ShouldSerializeTotalAchievementNumGet()
	{
		return __pbn__TotalAchievementNumGet.HasValue;
	}

	public void ResetTotalAchievementNumGet()
	{
		__pbn__TotalAchievementNumGet = null;
	}

	public bool ShouldSerializeTotalAchievementNum()
	{
		return __pbn__TotalAchievementNum.HasValue;
	}

	public void ResetTotalAchievementNum()
	{
		__pbn__TotalAchievementNum = null;
	}

	public bool ShouldSerializeChatForbidFinishTime()
	{
		return __pbn__ChatForbidFinishTime.HasValue;
	}

	public void ResetChatForbidFinishTime()
	{
		__pbn__ChatForbidFinishTime = null;
	}

	public bool ShouldSerializeMasterUid()
	{
		return __pbn__MasterUid.HasValue;
	}

	public void ResetMasterUid()
	{
		__pbn__MasterUid = null;
	}

	public bool ShouldSerializeMasterPupilTag()
	{
		return __pbn__MasterPupilTag.HasValue;
	}

	public void ResetMasterPupilTag()
	{
		__pbn__MasterPupilTag = null;
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

	public bool ShouldSerializeIsLogoff()
	{
		return __pbn__IsLogoff.HasValue;
	}

	public void ResetIsLogoff()
	{
		__pbn__IsLogoff = null;
	}
}
