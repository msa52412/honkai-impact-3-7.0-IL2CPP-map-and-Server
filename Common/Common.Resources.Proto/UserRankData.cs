using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UserRankData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__Score;

	private uint? __pbn__Rank;

	private string __pbn__NickName;

	private uint? __pbn__ArmadaId;

	private string __pbn__ArmadaName;

	private uint? __pbn__AvatarId;

	private uint? __pbn__DressId;

	private uint? __pbn__FrameId;

	private string __pbn__HouseName;

	private uint? __pbn__CustomHeadId;

	private bool? __pbn__IsLogoff;

	private string __pbn__DlcName;

	private uint? __pbn__UltraEndlessGroupLevel;

	private uint? __pbn__KingdomId;

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

	[ProtoMember(2, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(3, Name = "rank")]
	public uint Rank
	{
		get
		{
			return __pbn__Rank.GetValueOrDefault();
		}
		set
		{
			__pbn__Rank = value;
		}
	}

	[ProtoMember(4, Name = "nick_name")]
	[DefaultValue("")]
	public string NickName
	{
		get
		{
			return __pbn__NickName ?? "";
		}
		set
		{
			__pbn__NickName = value;
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

	[ProtoMember(6, Name = "armada_name")]
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

	[ProtoMember(7, Name = "avatar_id")]
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

	[ProtoMember(8, Name = "dress_id")]
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

	[ProtoMember(10, Name = "house_name")]
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

	[ProtoMember(11, Name = "custom_head_id")]
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

	[ProtoMember(12, Name = "is_logoff")]
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

	[ProtoMember(31, Name = "avatar_info_list")]
	public List<AvatarBriefInfo> AvatarInfoLists { get; } = new List<AvatarBriefInfo>();


	[ProtoMember(32, Name = "elf_info_list")]
	public List<ElfBriefInfo> ElfInfoLists { get; } = new List<ElfBriefInfo>();


	[ProtoMember(33, Name = "dlc_name")]
	[DefaultValue("")]
	public string DlcName
	{
		get
		{
			return __pbn__DlcName ?? "";
		}
		set
		{
			__pbn__DlcName = value;
		}
	}

	[ProtoMember(34, Name = "weekly_report_endless_info")]
	public UserRankWeeklyReportEndless WeeklyReportEndlessInfo { get; set; }

	[ProtoMember(35, Name = "weekly_report_ex_boss_info")]
	public UserRankWeeklyReportExBoss WeeklyReportExBossInfo { get; set; }

	[ProtoMember(36, Name = "ultra_endless_group_level")]
	public uint UltraEndlessGroupLevel
	{
		get
		{
			return __pbn__UltraEndlessGroupLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__UltraEndlessGroupLevel = value;
		}
	}

	[ProtoMember(37, Name = "weekly_report_ultra_endless_info")]
	public UserRankWeeklyReportUltraEndless WeeklyReportUltraEndlessInfo { get; set; }

	[ProtoMember(39, Name = "slg_battle_info")]
	public UserRankSlgBattle SlgBattleInfo { get; set; }

	[ProtoMember(40, Name = "monster_info_list")]
	public List<RanchMonsterBriefInfo> MonsterInfoLists { get; } = new List<RanchMonsterBriefInfo>();


	[ProtoMember(41, Name = "monster_card_rank_info")]
	public MonsterCardActivityRankInfo MonsterCardRankInfo { get; set; }

	[ProtoMember(42, Name = "kingdom_id")]
	public uint KingdomId
	{
		get
		{
			return __pbn__KingdomId.GetValueOrDefault();
		}
		set
		{
			__pbn__KingdomId = value;
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

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeRank()
	{
		return __pbn__Rank.HasValue;
	}

	public void ResetRank()
	{
		__pbn__Rank = null;
	}

	public bool ShouldSerializeNickName()
	{
		return __pbn__NickName != null;
	}

	public void ResetNickName()
	{
		__pbn__NickName = null;
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

	public bool ShouldSerializeFrameId()
	{
		return __pbn__FrameId.HasValue;
	}

	public void ResetFrameId()
	{
		__pbn__FrameId = null;
	}

	public bool ShouldSerializeHouseName()
	{
		return __pbn__HouseName != null;
	}

	public void ResetHouseName()
	{
		__pbn__HouseName = null;
	}

	public bool ShouldSerializeCustomHeadId()
	{
		return __pbn__CustomHeadId.HasValue;
	}

	public void ResetCustomHeadId()
	{
		__pbn__CustomHeadId = null;
	}

	public bool ShouldSerializeIsLogoff()
	{
		return __pbn__IsLogoff.HasValue;
	}

	public void ResetIsLogoff()
	{
		__pbn__IsLogoff = null;
	}

	public bool ShouldSerializeDlcName()
	{
		return __pbn__DlcName != null;
	}

	public void ResetDlcName()
	{
		__pbn__DlcName = null;
	}

	public bool ShouldSerializeUltraEndlessGroupLevel()
	{
		return __pbn__UltraEndlessGroupLevel.HasValue;
	}

	public void ResetUltraEndlessGroupLevel()
	{
		__pbn__UltraEndlessGroupLevel = null;
	}

	public bool ShouldSerializeKingdomId()
	{
		return __pbn__KingdomId.HasValue;
	}

	public void ResetKingdomId()
	{
		__pbn__KingdomId = null;
	}
}
