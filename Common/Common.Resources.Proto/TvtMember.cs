using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtMember : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__RegionName;

	private string __pbn__Nickname;

	private uint? __pbn__Level;

	private uint? __pbn__HeadAvatarId;

	private uint? __pbn__FrameId;

	private uint? __pbn__DressId;

	private uint? __pbn__Division;

	private uint? __pbn__DivisionStar;

	private uint? __pbn__WinStreak;

	private double? __pbn__EloScore;

	private double? __pbn__EloScorePara;

	private uint? __pbn__BattleCombat;

	private uint? __pbn__CustomHeadId;

	private uint? __pbn__WinAddStar;

	private uint? __pbn__LoseDelStar;

	private bool? __pbn__IsRobot;

	private uint? __pbn__RobotMatchProb;

	private uint? __pbn__VoiceId;

	private uint? __pbn__LaneOrder;

	private uint? __pbn__LastLaneOrder;

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

	[ProtoMember(2, Name = "region_name")]
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

	[ProtoMember(3, Name = "nickname")]
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

	[ProtoMember(4, Name = "level")]
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

	[ProtoMember(5, Name = "head_avatar_id")]
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

	[ProtoMember(6, Name = "frame_id")]
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

	[ProtoMember(8, Name = "division")]
	public uint Division
	{
		get
		{
			return __pbn__Division.GetValueOrDefault();
		}
		set
		{
			__pbn__Division = value;
		}
	}

	[ProtoMember(9, Name = "division_star")]
	public uint DivisionStar
	{
		get
		{
			return __pbn__DivisionStar.GetValueOrDefault();
		}
		set
		{
			__pbn__DivisionStar = value;
		}
	}

	[ProtoMember(10, Name = "win_streak")]
	public uint WinStreak
	{
		get
		{
			return __pbn__WinStreak.GetValueOrDefault();
		}
		set
		{
			__pbn__WinStreak = value;
		}
	}

	[ProtoMember(11, Name = "elo_score")]
	public double EloScore
	{
		get
		{
			return __pbn__EloScore.GetValueOrDefault();
		}
		set
		{
			__pbn__EloScore = value;
		}
	}

	[ProtoMember(12, Name = "elo_score_para")]
	public double EloScorePara
	{
		get
		{
			return __pbn__EloScorePara.GetValueOrDefault();
		}
		set
		{
			__pbn__EloScorePara = value;
		}
	}

	[ProtoMember(13, Name = "battle_combat")]
	public uint BattleCombat
	{
		get
		{
			return __pbn__BattleCombat.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleCombat = value;
		}
	}

	[ProtoMember(14, Name = "custom_head_id")]
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

	[ProtoMember(15, Name = "win_add_star")]
	public uint WinAddStar
	{
		get
		{
			return __pbn__WinAddStar.GetValueOrDefault();
		}
		set
		{
			__pbn__WinAddStar = value;
		}
	}

	[ProtoMember(16, Name = "lose_del_star")]
	public uint LoseDelStar
	{
		get
		{
			return __pbn__LoseDelStar.GetValueOrDefault();
		}
		set
		{
			__pbn__LoseDelStar = value;
		}
	}

	[ProtoMember(17, Name = "card_list")]
	public List<TvtCard> CardLists { get; } = new List<TvtCard>();


	[ProtoMember(18, Name = "lineup_info")]
	public LineupBriefInfo LineupInfo { get; set; }

	[ProtoMember(19, Name = "is_robot")]
	public bool IsRobot
	{
		get
		{
			return __pbn__IsRobot.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRobot = value;
		}
	}

	[ProtoMember(20, Name = "robot_match_prob")]
	public uint RobotMatchProb
	{
		get
		{
			return __pbn__RobotMatchProb.GetValueOrDefault();
		}
		set
		{
			__pbn__RobotMatchProb = value;
		}
	}

	[ProtoMember(21, Name = "voice_id")]
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

	[ProtoMember(22, Name = "lane_order")]
	public uint LaneOrder
	{
		get
		{
			return __pbn__LaneOrder.GetValueOrDefault();
		}
		set
		{
			__pbn__LaneOrder = value;
		}
	}

	[ProtoMember(23, Name = "last_lane_order")]
	public uint LastLaneOrder
	{
		get
		{
			return __pbn__LastLaneOrder.GetValueOrDefault();
		}
		set
		{
			__pbn__LastLaneOrder = value;
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

	public bool ShouldSerializeRegionName()
	{
		return __pbn__RegionName != null;
	}

	public void ResetRegionName()
	{
		__pbn__RegionName = null;
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

	public bool ShouldSerializeHeadAvatarId()
	{
		return __pbn__HeadAvatarId.HasValue;
	}

	public void ResetHeadAvatarId()
	{
		__pbn__HeadAvatarId = null;
	}

	public bool ShouldSerializeFrameId()
	{
		return __pbn__FrameId.HasValue;
	}

	public void ResetFrameId()
	{
		__pbn__FrameId = null;
	}

	public bool ShouldSerializeDressId()
	{
		return __pbn__DressId.HasValue;
	}

	public void ResetDressId()
	{
		__pbn__DressId = null;
	}

	public bool ShouldSerializeDivision()
	{
		return __pbn__Division.HasValue;
	}

	public void ResetDivision()
	{
		__pbn__Division = null;
	}

	public bool ShouldSerializeDivisionStar()
	{
		return __pbn__DivisionStar.HasValue;
	}

	public void ResetDivisionStar()
	{
		__pbn__DivisionStar = null;
	}

	public bool ShouldSerializeWinStreak()
	{
		return __pbn__WinStreak.HasValue;
	}

	public void ResetWinStreak()
	{
		__pbn__WinStreak = null;
	}

	public bool ShouldSerializeEloScore()
	{
		return __pbn__EloScore.HasValue;
	}

	public void ResetEloScore()
	{
		__pbn__EloScore = null;
	}

	public bool ShouldSerializeEloScorePara()
	{
		return __pbn__EloScorePara.HasValue;
	}

	public void ResetEloScorePara()
	{
		__pbn__EloScorePara = null;
	}

	public bool ShouldSerializeBattleCombat()
	{
		return __pbn__BattleCombat.HasValue;
	}

	public void ResetBattleCombat()
	{
		__pbn__BattleCombat = null;
	}

	public bool ShouldSerializeCustomHeadId()
	{
		return __pbn__CustomHeadId.HasValue;
	}

	public void ResetCustomHeadId()
	{
		__pbn__CustomHeadId = null;
	}

	public bool ShouldSerializeWinAddStar()
	{
		return __pbn__WinAddStar.HasValue;
	}

	public void ResetWinAddStar()
	{
		__pbn__WinAddStar = null;
	}

	public bool ShouldSerializeLoseDelStar()
	{
		return __pbn__LoseDelStar.HasValue;
	}

	public void ResetLoseDelStar()
	{
		__pbn__LoseDelStar = null;
	}

	public bool ShouldSerializeIsRobot()
	{
		return __pbn__IsRobot.HasValue;
	}

	public void ResetIsRobot()
	{
		__pbn__IsRobot = null;
	}

	public bool ShouldSerializeRobotMatchProb()
	{
		return __pbn__RobotMatchProb.HasValue;
	}

	public void ResetRobotMatchProb()
	{
		__pbn__RobotMatchProb = null;
	}

	public bool ShouldSerializeVoiceId()
	{
		return __pbn__VoiceId.HasValue;
	}

	public void ResetVoiceId()
	{
		__pbn__VoiceId = null;
	}

	public bool ShouldSerializeLaneOrder()
	{
		return __pbn__LaneOrder.HasValue;
	}

	public void ResetLaneOrder()
	{
		__pbn__LaneOrder = null;
	}

	public bool ShouldSerializeLastLaneOrder()
	{
		return __pbn__LastLaneOrder.HasValue;
	}

	public void ResetLastLaneOrder()
	{
		__pbn__LastLaneOrder = null;
	}
}
