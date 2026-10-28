using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlayerFriendBriefData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__Nickname;

	private uint? __pbn__Level;

	private uint? __pbn__AvatarId;

	private uint? __pbn__AvatarStar;

	private uint? __pbn__AvatarCombat;

	private uint? __pbn__AvatarLevel;

	private uint? __pbn__LastLoginTime;

	private uint? __pbn__DressId;

	private uint? __pbn__ArmadaId;

	private string __pbn__ArmadaName;

	private uint? __pbn__Bond;

	private uint? __pbn__OnMedalId;

	private uint? __pbn__FrameId;

	private uint? __pbn__MpLevel;

	private uint? __pbn__CustomHeadId;

	private bool? __pbn__IsAllowVisit;

	private uint? __pbn__VisitAvatar;

	private uint? __pbn__BeLikedNum;

	private uint? __pbn__ShowHouse;

	private uint? __pbn__ComfortValue;

	private uint? __pbn__HouseLevel;

	private string __pbn__HouseName;

	private bool? __pbn__IsGoback;

	private uint? __pbn__AvatarSubStar;

	private uint? __pbn__LastLogoutTime;

	private uint? __pbn__OnlineStatus;

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

	[ProtoMember(4, Name = "avatar_id")]
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

	[ProtoMember(5, Name = "avatar_star")]
	public uint AvatarStar
	{
		get
		{
			return __pbn__AvatarStar.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarStar = value;
		}
	}

	[ProtoMember(6, Name = "avatar_combat")]
	public uint AvatarCombat
	{
		get
		{
			return __pbn__AvatarCombat.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarCombat = value;
		}
	}

	[ProtoMember(7, Name = "avatar_level")]
	public uint AvatarLevel
	{
		get
		{
			return __pbn__AvatarLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarLevel = value;
		}
	}

	[ProtoMember(8, Name = "main_sub_skill_list")]
	public List<AvatarSubSkillDetailData> MainSubSkillLists { get; } = new List<AvatarSubSkillDetailData>();


	[ProtoMember(10, Name = "last_login_time")]
	public uint LastLoginTime
	{
		get
		{
			return __pbn__LastLoginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastLoginTime = value;
		}
	}

	[ProtoMember(11, Name = "dress_id")]
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

	[ProtoMember(12, Name = "armada_id")]
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

	[ProtoMember(13, Name = "armada_name")]
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

	[ProtoMember(14, Name = "bond")]
	public uint Bond
	{
		get
		{
			return __pbn__Bond.GetValueOrDefault();
		}
		set
		{
			__pbn__Bond = value;
		}
	}

	[ProtoMember(15, Name = "on_medal_id")]
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

	[ProtoMember(19, Name = "on_medal")]
	public Medal OnMedal { get; set; }

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

	[ProtoMember(17, Name = "mp_level")]
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

	[ProtoMember(18, Name = "custom_head_id")]
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

	[ProtoMember(37, Name = "is_goback")]
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

	[ProtoMember(38, Name = "avatar_sub_star")]
	public uint AvatarSubStar
	{
		get
		{
			return __pbn__AvatarSubStar.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarSubStar = value;
		}
	}

	[ProtoMember(39, Name = "last_logout_time")]
	public uint LastLogoutTime
	{
		get
		{
			return __pbn__LastLogoutTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastLogoutTime = value;
		}
	}

	[ProtoMember(40, Name = "online_status")]
	public uint OnlineStatus
	{
		get
		{
			return __pbn__OnlineStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__OnlineStatus = value;
		}
	}

	[ProtoMember(41, Name = "season_id")]
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

	[ProtoMember(42, Name = "season_group_id")]
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

	[ProtoMember(43, Name = "season_level")]
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

	[ProtoMember(44, Name = "is_logoff")]
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

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeAvatarStar()
	{
		return __pbn__AvatarStar.HasValue;
	}

	public void ResetAvatarStar()
	{
		__pbn__AvatarStar = null;
	}

	public bool ShouldSerializeAvatarCombat()
	{
		return __pbn__AvatarCombat.HasValue;
	}

	public void ResetAvatarCombat()
	{
		__pbn__AvatarCombat = null;
	}

	public bool ShouldSerializeAvatarLevel()
	{
		return __pbn__AvatarLevel.HasValue;
	}

	public void ResetAvatarLevel()
	{
		__pbn__AvatarLevel = null;
	}

	public bool ShouldSerializeLastLoginTime()
	{
		return __pbn__LastLoginTime.HasValue;
	}

	public void ResetLastLoginTime()
	{
		__pbn__LastLoginTime = null;
	}

	public bool ShouldSerializeDressId()
	{
		return __pbn__DressId.HasValue;
	}

	public void ResetDressId()
	{
		__pbn__DressId = null;
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

	public bool ShouldSerializeBond()
	{
		return __pbn__Bond.HasValue;
	}

	public void ResetBond()
	{
		__pbn__Bond = null;
	}

	public bool ShouldSerializeOnMedalId()
	{
		return __pbn__OnMedalId.HasValue;
	}

	public void ResetOnMedalId()
	{
		__pbn__OnMedalId = null;
	}

	public bool ShouldSerializeFrameId()
	{
		return __pbn__FrameId.HasValue;
	}

	public void ResetFrameId()
	{
		__pbn__FrameId = null;
	}

	public bool ShouldSerializeMpLevel()
	{
		return __pbn__MpLevel.HasValue;
	}

	public void ResetMpLevel()
	{
		__pbn__MpLevel = null;
	}

	public bool ShouldSerializeCustomHeadId()
	{
		return __pbn__CustomHeadId.HasValue;
	}

	public void ResetCustomHeadId()
	{
		__pbn__CustomHeadId = null;
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

	public bool ShouldSerializeIsGoback()
	{
		return __pbn__IsGoback.HasValue;
	}

	public void ResetIsGoback()
	{
		__pbn__IsGoback = null;
	}

	public bool ShouldSerializeAvatarSubStar()
	{
		return __pbn__AvatarSubStar.HasValue;
	}

	public void ResetAvatarSubStar()
	{
		__pbn__AvatarSubStar = null;
	}

	public bool ShouldSerializeLastLogoutTime()
	{
		return __pbn__LastLogoutTime.HasValue;
	}

	public void ResetLastLogoutTime()
	{
		__pbn__LastLogoutTime = null;
	}

	public bool ShouldSerializeOnlineStatus()
	{
		return __pbn__OnlineStatus.HasValue;
	}

	public void ResetOnlineStatus()
	{
		__pbn__OnlineStatus = null;
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
