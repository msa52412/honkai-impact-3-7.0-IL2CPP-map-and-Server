using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterOWServerLocalMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	private bool? __pbn__IsOwMapUnlocked;

	private bool? __pbn__isowmapchurchLightunlocked;

	private bool? __pbn__IsOwMapChurchLightBreakPlayed;

	private bool? __pbn__IsOwMapAnimTipsShown;

	private bool? __pbn__IsOwMapMemoryTipsShown;

	private int? __pbn__LastPlayedSiteId;

	private uint? __pbn__LastPlayedTowerId;

	private bool? __pbn__EnterdEndlessTower;

	private bool? __pbn__ShowCreateSuiteBtnPopup;

	private bool? __pbn__HideVfxInLevel;

	private bool? __pbn__PlayedFuseAnim;

	private bool? __pbn__PlayedResurrectionAnim;

	private bool? __pbn__PlayedSpHeroUnlockAnim;

	[ProtoMember(1, Name = "triggered_tips_id_list")]
	public int[] TriggeredTipsIdLists { get; set; }

	[ProtoMember(2, Name = "is_ow_map_unlocked")]
	public bool IsOwMapUnlocked
	{
		get
		{
			return __pbn__IsOwMapUnlocked.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOwMapUnlocked = value;
		}
	}

	[ProtoMember(3, Name = "is_ow_map_churchLight_unlocked")]
	public bool isowmapchurchLightunlocked
	{
		get
		{
			return __pbn__isowmapchurchLightunlocked.GetValueOrDefault();
		}
		set
		{
			__pbn__isowmapchurchLightunlocked = value;
		}
	}

	[ProtoMember(4, Name = "is_ow_map_church_light_break_played")]
	public bool IsOwMapChurchLightBreakPlayed
	{
		get
		{
			return __pbn__IsOwMapChurchLightBreakPlayed.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOwMapChurchLightBreakPlayed = value;
		}
	}

	[ProtoMember(5, Name = "is_ow_map_anim_tips_shown")]
	public bool IsOwMapAnimTipsShown
	{
		get
		{
			return __pbn__IsOwMapAnimTipsShown.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOwMapAnimTipsShown = value;
		}
	}

	[ProtoMember(6, Name = "is_ow_map_memory_tips_shown")]
	public bool IsOwMapMemoryTipsShown
	{
		get
		{
			return __pbn__IsOwMapMemoryTipsShown.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOwMapMemoryTipsShown = value;
		}
	}

	[ProtoMember(7, Name = "plot_group_trigger_time_list")]
	public List<PlotGroupIDToTriggerTime> PlotGroupTriggerTimeLists { get; } = new List<PlotGroupIDToTriggerTime>();


	[ProtoMember(8, Name = "last_played_site_id")]
	public int LastPlayedSiteId
	{
		get
		{
			return __pbn__LastPlayedSiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastPlayedSiteId = value;
		}
	}

	[ProtoMember(9, Name = "last_played_tower_id")]
	public uint LastPlayedTowerId
	{
		get
		{
			return __pbn__LastPlayedTowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastPlayedTowerId = value;
		}
	}

	[ProtoMember(10, Name = "enterd_endless_tower")]
	public bool EnterdEndlessTower
	{
		get
		{
			return __pbn__EnterdEndlessTower.GetValueOrDefault();
		}
		set
		{
			__pbn__EnterdEndlessTower = value;
		}
	}

	[ProtoMember(11, Name = "card_suite_epic_active_time_list")]
	public List<CardSuiteEpicActiveTime> CardSuiteEpicActiveTimeLists { get; } = new List<CardSuiteEpicActiveTime>();


	[ProtoMember(12, Name = "hero_spirits_event_notice_list")]
	public List<HeroSpiritsEventNotice> HeroSpiritsEventNoticeLists { get; } = new List<HeroSpiritsEventNotice>();


	[ProtoMember(13, Name = "hero_card_collection_popup_list")]
	public int[] HeroCardCollectionPopupLists { get; set; }

	[ProtoMember(14, Name = "achievement_collection_popup_list")]
	public int[] AchievementCollectionPopupLists { get; set; }

	[ProtoMember(15, Name = "show_create_suite_btn_popup")]
	public bool ShowCreateSuiteBtnPopup
	{
		get
		{
			return __pbn__ShowCreateSuiteBtnPopup.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowCreateSuiteBtnPopup = value;
		}
	}

	[ProtoMember(16, Name = "new_got_hero_id_list")]
	public int[] NewGotHeroIdLists { get; set; }

	[ProtoMember(17, Name = "new_got_hero_card_id_list")]
	public int[] NewGotHeroCardIdLists { get; set; }

	[ProtoMember(18, Name = "hide_vfx_in_level")]
	public bool HideVfxInLevel
	{
		get
		{
			return __pbn__HideVfxInLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HideVfxInLevel = value;
		}
	}

	[ProtoMember(19, Name = "played_fuse_anim")]
	public bool PlayedFuseAnim
	{
		get
		{
			return __pbn__PlayedFuseAnim.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayedFuseAnim = value;
		}
	}

	[ProtoMember(20, Name = "played_resurrection_anim")]
	public bool PlayedResurrectionAnim
	{
		get
		{
			return __pbn__PlayedResurrectionAnim.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayedResurrectionAnim = value;
		}
	}

	[ProtoMember(21, Name = "played_sp_hero_unlock_anim")]
	public bool PlayedSpHeroUnlockAnim
	{
		get
		{
			return __pbn__PlayedSpHeroUnlockAnim.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayedSpHeroUnlockAnim = value;
		}
	}

	[ProtoMember(22, Name = "collection_unlock_num_list")]
	public List<ProtoVector2Int> CollectionUnlockNumLists { get; } = new List<ProtoVector2Int>();


	[ProtoMember(23, Name = "bag_item_num_list")]
	public List<ProtoVector2Int> BagItemNumLists { get; } = new List<ProtoVector2Int>();


	[ProtoMember(24, Name = "bag_item_popup_list")]
	public int[] BagItemPopupLists { get; set; }

	[ProtoMember(25, Name = "story_series_num_list")]
	public List<ProtoVector2Int> StorySeriesNumLists { get; } = new List<ProtoVector2Int>();


	[ProtoMember(26, Name = "unlocked_activate_talent_id_list")]
	public int[] UnlockedActivateTalentIdLists { get; set; }

	[ProtoMember(27, Name = "final_achievement_relic_popup_list")]
	public int[] FinalAchievementRelicPopupLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsOwMapUnlocked()
	{
		return __pbn__IsOwMapUnlocked.HasValue;
	}

	public void ResetIsOwMapUnlocked()
	{
		__pbn__IsOwMapUnlocked = null;
	}

	public bool ShouldSerializeisowmapchurchLightunlocked()
	{
		return __pbn__isowmapchurchLightunlocked.HasValue;
	}

	public void ResetisowmapchurchLightunlocked()
	{
		__pbn__isowmapchurchLightunlocked = null;
	}

	public bool ShouldSerializeIsOwMapChurchLightBreakPlayed()
	{
		return __pbn__IsOwMapChurchLightBreakPlayed.HasValue;
	}

	public void ResetIsOwMapChurchLightBreakPlayed()
	{
		__pbn__IsOwMapChurchLightBreakPlayed = null;
	}

	public bool ShouldSerializeIsOwMapAnimTipsShown()
	{
		return __pbn__IsOwMapAnimTipsShown.HasValue;
	}

	public void ResetIsOwMapAnimTipsShown()
	{
		__pbn__IsOwMapAnimTipsShown = null;
	}

	public bool ShouldSerializeIsOwMapMemoryTipsShown()
	{
		return __pbn__IsOwMapMemoryTipsShown.HasValue;
	}

	public void ResetIsOwMapMemoryTipsShown()
	{
		__pbn__IsOwMapMemoryTipsShown = null;
	}

	public bool ShouldSerializeLastPlayedSiteId()
	{
		return __pbn__LastPlayedSiteId.HasValue;
	}

	public void ResetLastPlayedSiteId()
	{
		__pbn__LastPlayedSiteId = null;
	}

	public bool ShouldSerializeLastPlayedTowerId()
	{
		return __pbn__LastPlayedTowerId.HasValue;
	}

	public void ResetLastPlayedTowerId()
	{
		__pbn__LastPlayedTowerId = null;
	}

	public bool ShouldSerializeEnterdEndlessTower()
	{
		return __pbn__EnterdEndlessTower.HasValue;
	}

	public void ResetEnterdEndlessTower()
	{
		__pbn__EnterdEndlessTower = null;
	}

	public bool ShouldSerializeShowCreateSuiteBtnPopup()
	{
		return __pbn__ShowCreateSuiteBtnPopup.HasValue;
	}

	public void ResetShowCreateSuiteBtnPopup()
	{
		__pbn__ShowCreateSuiteBtnPopup = null;
	}

	public bool ShouldSerializeHideVfxInLevel()
	{
		return __pbn__HideVfxInLevel.HasValue;
	}

	public void ResetHideVfxInLevel()
	{
		__pbn__HideVfxInLevel = null;
	}

	public bool ShouldSerializePlayedFuseAnim()
	{
		return __pbn__PlayedFuseAnim.HasValue;
	}

	public void ResetPlayedFuseAnim()
	{
		__pbn__PlayedFuseAnim = null;
	}

	public bool ShouldSerializePlayedResurrectionAnim()
	{
		return __pbn__PlayedResurrectionAnim.HasValue;
	}

	public void ResetPlayedResurrectionAnim()
	{
		__pbn__PlayedResurrectionAnim = null;
	}

	public bool ShouldSerializePlayedSpHeroUnlockAnim()
	{
		return __pbn__PlayedSpHeroUnlockAnim.HasValue;
	}

	public void ResetPlayedSpHeroUnlockAnim()
	{
		__pbn__PlayedSpHeroUnlockAnim = null;
	}
}
