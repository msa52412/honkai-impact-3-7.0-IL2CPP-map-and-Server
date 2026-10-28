using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapter : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__MaxEquipCost;

	private uint? __pbn__MaxRewardTakeNum;

	private uint? __pbn__Fame;

	private uint? __pbn__FameLevel;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(2, Name = "slot_list")]
	public List<OpenworldChapterSlot> SlotLists { get; } = new List<OpenworldChapterSlot>();


	[ProtoMember(4, Name = "max_equip_cost")]
	public uint MaxEquipCost
	{
		get
		{
			return __pbn__MaxEquipCost.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxEquipCost = value;
		}
	}

	[ProtoMember(5, Name = "buff_list")]
	public List<OpenworldChapterBuff> BuffLists { get; } = new List<OpenworldChapterBuff>();


	[ProtoMember(6, Name = "max_reward_take_num")]
	public uint MaxRewardTakeNum
	{
		get
		{
			return __pbn__MaxRewardTakeNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxRewardTakeNum = value;
		}
	}

	[ProtoMember(7, Name = "fame")]
	public uint Fame
	{
		get
		{
			return __pbn__Fame.GetValueOrDefault();
		}
		set
		{
			__pbn__Fame = value;
		}
	}

	[ProtoMember(8, Name = "fame_level")]
	public uint FameLevel
	{
		get
		{
			return __pbn__FameLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__FameLevel = value;
		}
	}

	[ProtoMember(9, Name = "challenge")]
	public OpenworldChapterChallenge Challenge { get; set; }

	[ProtoMember(10, Name = "endless_tower_site_list")]
	public List<OpenworldChapterEndlessTowerSite> EndlessTowerSiteLists { get; } = new List<OpenworldChapterEndlessTowerSite>();


	[ProtoMember(11, Name = "hero_list")]
	public List<OpenworldChapterHero> HeroLists { get; } = new List<OpenworldChapterHero>();


	[ProtoMember(14, Name = "hero_card_list")]
	public List<OpenworldChapterHeroCard> HeroCardLists { get; } = new List<OpenworldChapterHeroCard>();


	[ProtoMember(15, Name = "hero_card_group_list")]
	public List<OpenworldChapterHeroCardGroup> HeroCardGroupLists { get; } = new List<OpenworldChapterHeroCardGroup>();


	[ProtoMember(16, Name = "achievement_mission_list")]
	public List<OpenworldChapterAchievementMission> AchievementMissionLists { get; } = new List<OpenworldChapterAchievementMission>();


	[ProtoMember(17, Name = "endless_challenge_list")]
	public List<OpenworldChapterEndlessChallenge> EndlessChallengeLists { get; } = new List<OpenworldChapterEndlessChallenge>();


	[ProtoMember(18, Name = "building_list")]
	public List<OpenworldChapterBuilding> BuildingLists { get; } = new List<OpenworldChapterBuilding>();


	[ProtoMember(19, Name = "terminal")]
	public OpenworldChapterTerminal Terminal { get; set; }

	[ProtoMember(20, Name = "quest_data")]
	public OpenworldChapterQuestData QuestData { get; set; }

	[ProtoMember(21, Name = "furnace")]
	public OpenworldChapterFurnace Furnace { get; set; }

	[ProtoMember(22, Name = "talent_list")]
	public List<OpenworldChapterTalent> TalentLists { get; } = new List<OpenworldChapterTalent>();


	[ProtoMember(23, Name = "active_talent_id_list")]
	public uint[] ActiveTalentIdLists { get; set; }

	[ProtoMember(24, Name = "dig_site_list")]
	public List<OpenworldChapterDigSite> DigSiteLists { get; } = new List<OpenworldChapterDigSite>();


	[ProtoMember(25, Name = "got_last_reward_special_qte_map_list")]
	public uint[] GotLastRewardSpecialQteMapLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeMaxEquipCost()
	{
		return __pbn__MaxEquipCost.HasValue;
	}

	public void ResetMaxEquipCost()
	{
		__pbn__MaxEquipCost = null;
	}

	public bool ShouldSerializeMaxRewardTakeNum()
	{
		return __pbn__MaxRewardTakeNum.HasValue;
	}

	public void ResetMaxRewardTakeNum()
	{
		__pbn__MaxRewardTakeNum = null;
	}

	public bool ShouldSerializeFame()
	{
		return __pbn__Fame.HasValue;
	}

	public void ResetFame()
	{
		__pbn__Fame = null;
	}

	public bool ShouldSerializeFameLevel()
	{
		return __pbn__FameLevel.HasValue;
	}

	public void ResetFameLevel()
	{
		__pbn__FameLevel = null;
	}
}
