using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetConfigRsp : IExtensible
{
	[ProtoContract]
	public class AntiCheatStageCheckConfig : IExtensible
	{
		private IExtension __pbn__extensionData;

		[ProtoMember(1, Name = "check_stage_id_list")]
		public uint[] CheckStageIdLists { get; set; }

		[ProtoMember(2, Name = "check_stage_type_list")]
		public uint[] CheckStageTypeLists { get; set; }

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}
	}

	[ProtoContract]
	public class DiscountScheduleConfig : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__Type;

		private uint? __pbn__Discount;

		private uint? __pbn__BeginTime;

		private uint? __pbn__EndTime;

		[ProtoMember(1, Name = "type")]
		public uint Type
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

		[ProtoMember(2, Name = "discount")]
		public uint Discount
		{
			get
			{
				return __pbn__Discount.GetValueOrDefault();
			}
			set
			{
				__pbn__Discount = value;
			}
		}

		[ProtoMember(3, Name = "begin_time")]
		public uint BeginTime
		{
			get
			{
				return __pbn__BeginTime.GetValueOrDefault();
			}
			set
			{
				__pbn__BeginTime = value;
			}
		}

		[ProtoMember(4, Name = "end_time")]
		public uint EndTime
		{
			get
			{
				return __pbn__EndTime.GetValueOrDefault();
			}
			set
			{
				__pbn__EndTime = value;
			}
		}

		[ProtoMember(5, Name = "para_int_list")]
		public uint[] ParaIntLists { get; set; }

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

		public bool ShouldSerializeDiscount()
		{
			return __pbn__Discount.HasValue;
		}

		public void ResetDiscount()
		{
			__pbn__Discount = null;
		}

		public bool ShouldSerializeBeginTime()
		{
			return __pbn__BeginTime.HasValue;
		}

		public void ResetBeginTime()
		{
			__pbn__BeginTime = null;
		}

		public bool ShouldSerializeEndTime()
		{
			return __pbn__EndTime.HasValue;
		}

		public void ResetEndTime()
		{
			__pbn__EndTime = null;
		}
	}

	[ProtoContract]
	public class FriendBondLevelConfig : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__Level;

		private uint? __pbn__MinBond;

		private uint? __pbn__AssistCdReduceRate;

		private uint? __pbn__InviteAddFriendsPoint;

		private uint? __pbn__ExtraAddMpExpRate;

		private uint? __pbn__ExtraMpAddFriendsPoint;

		[ProtoMember(1, Name = "level")]
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

		[ProtoMember(2, Name = "min_bond")]
		public uint MinBond
		{
			get
			{
				return __pbn__MinBond.GetValueOrDefault();
			}
			set
			{
				__pbn__MinBond = value;
			}
		}

		[ProtoMember(3, Name = "assist_cd_reduce_rate")]
		public uint AssistCdReduceRate
		{
			get
			{
				return __pbn__AssistCdReduceRate.GetValueOrDefault();
			}
			set
			{
				__pbn__AssistCdReduceRate = value;
			}
		}

		[ProtoMember(4, Name = "invite_add_friends_point")]
		public uint InviteAddFriendsPoint
		{
			get
			{
				return __pbn__InviteAddFriendsPoint.GetValueOrDefault();
			}
			set
			{
				__pbn__InviteAddFriendsPoint = value;
			}
		}

		[ProtoMember(5, Name = "extra_add_mp_exp_rate")]
		public uint ExtraAddMpExpRate
		{
			get
			{
				return __pbn__ExtraAddMpExpRate.GetValueOrDefault();
			}
			set
			{
				__pbn__ExtraAddMpExpRate = value;
			}
		}

		[ProtoMember(6, Name = "extra_mp_add_friends_point")]
		public uint ExtraMpAddFriendsPoint
		{
			get
			{
				return __pbn__ExtraMpAddFriendsPoint.GetValueOrDefault();
			}
			set
			{
				__pbn__ExtraMpAddFriendsPoint = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeLevel()
		{
			return __pbn__Level.HasValue;
		}

		public void ResetLevel()
		{
			__pbn__Level = null;
		}

		public bool ShouldSerializeMinBond()
		{
			return __pbn__MinBond.HasValue;
		}

		public void ResetMinBond()
		{
			__pbn__MinBond = null;
		}

		public bool ShouldSerializeAssistCdReduceRate()
		{
			return __pbn__AssistCdReduceRate.HasValue;
		}

		public void ResetAssistCdReduceRate()
		{
			__pbn__AssistCdReduceRate = null;
		}

		public bool ShouldSerializeInviteAddFriendsPoint()
		{
			return __pbn__InviteAddFriendsPoint.HasValue;
		}

		public void ResetInviteAddFriendsPoint()
		{
			__pbn__InviteAddFriendsPoint = null;
		}

		public bool ShouldSerializeExtraAddMpExpRate()
		{
			return __pbn__ExtraAddMpExpRate.HasValue;
		}

		public void ResetExtraAddMpExpRate()
		{
			__pbn__ExtraAddMpExpRate = null;
		}

		public bool ShouldSerializeExtraMpAddFriendsPoint()
		{
			return __pbn__ExtraMpAddFriendsPoint.HasValue;
		}

		public void ResetExtraMpAddFriendsPoint()
		{
			__pbn__ExtraMpAddFriendsPoint = null;
		}
	}

	[ProtoContract]
	public class GachaTicket : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__MaterialId;

		private uint? __pbn__HcoinCost;

		[ProtoMember(1, Name = "material_id")]
		public uint MaterialId
		{
			get
			{
				return __pbn__MaterialId.GetValueOrDefault();
			}
			set
			{
				__pbn__MaterialId = value;
			}
		}

		[ProtoMember(2, Name = "hcoin_cost")]
		public uint HcoinCost
		{
			get
			{
				return __pbn__HcoinCost.GetValueOrDefault();
			}
			set
			{
				__pbn__HcoinCost = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeMaterialId()
		{
			return __pbn__MaterialId.HasValue;
		}

		public void ResetMaterialId()
		{
			__pbn__MaterialId = null;
		}

		public bool ShouldSerializeHcoinCost()
		{
			return __pbn__HcoinCost.HasValue;
		}

		public void ResetHcoinCost()
		{
			__pbn__HcoinCost = null;
		}
	}

	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 111
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StaminaRecoverConfigTime;

	private uint? __pbn__AvatarReviveHcoinCost;

	private uint? __pbn__SameTypePowerUpRate;

	private uint? __pbn__DayTimeOffset;

	private uint? __pbn__ServerCurTime;

	private uint? __pbn__PowerUpScoinCostRate;

	private uint? __pbn__MaxFriendNum;

	private uint? __pbn__AvatarMaxAddGoodfeel;

	private uint? __pbn__DisjoinEquipmentBackExpPercent;

	private string __pbn__RegionName;

	private uint? __pbn__MinInviteLevel;

	private uint? __pbn__IslandEventRefreshInterval;

	private uint? __pbn__SpaceshipBeginTime;

	private uint? __pbn__SpaceshipEndTime;

	private uint? __pbn__SpaceshipId;

	private bool? __pbn__IsRandomBoxProductOpen;

	private uint? __pbn__ScoinLimit;

	private uint? __pbn__FriendsPointLimit;

	private uint? __pbn__ProductRandomBoxBeginTime;

	private uint? __pbn__ProductRandomBoxEndTime;

	private uint? __pbn__WeekDayActivityCoinItemId;

	private uint? __pbn__IslandVisitMaxHelpNum;

	private uint? __pbn__CostSeniorEquipAfterCancelProtectMinTimeDiff;

	private uint? __pbn__RogueStageOpenLevel;

	private uint? __pbn__ArmadaBeginTime;

	private uint? __pbn__ArmadaEndTime;

	private string __pbn__ArmadaCustomContent;

	private uint? __pbn__StigmataRuneOpenLevel;

	private uint? __pbn__StigmataTitleOpenLevel;

	private uint? __pbn__StigmataDisjoinTitleBackExpPercent;

	private uint? __pbn__StigmataPromoteMaxTimes;

	private uint? __pbn__StigmataRuneCombatRatio;

	private uint? __pbn__StigmataDisjoinBackExpPercent;

	private uint? __pbn__StigmataRefineValueBackPercent;

	private uint? __pbn__ScoinConvertPerExp;

	private uint? __pbn__MpResetSkillHcoinCost;

	private uint? __pbn__MpMemberMaxTimesForAllAct;

	private uint? __pbn__MpMemberExhaustedDropRatio;

	private uint? __pbn__MpPunishMpExpReduceRate;

	private uint? __pbn__FriendAssistantAddBond;

	private uint? __pbn__ArmadaManageMailLength;

	private uint? __pbn__ArmadaContactLength;

	private uint? __pbn__ArmadaManageMailCd;

	private uint? __pbn__ArmadaBulletinLength;

	private uint? __pbn__OpenworldJudgeParaType1;

	private uint? __pbn__OpenworldJudgeParaType2;

	private uint? __pbn__OpenworldJudgeParaType3;

	private uint? __pbn__OpenworldJudgeParaType4;

	private uint? __pbn__TrialAvatarPlayerMinLevel;

	private uint? __pbn__DisjoinEquipmentMaxBatchNum;

	private uint? __pbn__EquipDisjointUnlockLevel;

	private uint? __pbn__ArmadaBossStageOpenLevel;

	private bool? __pbn__IsCanGalTouch;

	private bool? __pbn__IsResistanceOpen;

	private uint? __pbn__NextDayBeginTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "stamina_recover_config_time")]
	public uint StaminaRecoverConfigTime
	{
		get
		{
			return __pbn__StaminaRecoverConfigTime.GetValueOrDefault();
		}
		set
		{
			__pbn__StaminaRecoverConfigTime = value;
		}
	}

	[ProtoMember(4, Name = "avatar_revive_hcoin_cost")]
	public uint AvatarReviveHcoinCost
	{
		get
		{
			return __pbn__AvatarReviveHcoinCost.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarReviveHcoinCost = value;
		}
	}

	[ProtoMember(5, Name = "same_type_power_up_rate")]
	public uint SameTypePowerUpRate
	{
		get
		{
			return __pbn__SameTypePowerUpRate.GetValueOrDefault();
		}
		set
		{
			__pbn__SameTypePowerUpRate = value;
		}
	}

	[ProtoMember(6, Name = "day_time_offset")]
	public uint DayTimeOffset
	{
		get
		{
			return __pbn__DayTimeOffset.GetValueOrDefault();
		}
		set
		{
			__pbn__DayTimeOffset = value;
		}
	}

	[ProtoMember(7, Name = "server_cur_time")]
	public uint ServerCurTime
	{
		get
		{
			return __pbn__ServerCurTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ServerCurTime = value;
		}
	}

	[ProtoMember(8, Name = "power_up_scoin_cost_rate")]
	public uint PowerUpScoinCostRate
	{
		get
		{
			return __pbn__PowerUpScoinCostRate.GetValueOrDefault();
		}
		set
		{
			__pbn__PowerUpScoinCostRate = value;
		}
	}

	[ProtoMember(9, Name = "max_friend_num")]
	public uint MaxFriendNum
	{
		get
		{
			return __pbn__MaxFriendNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxFriendNum = value;
		}
	}

	[ProtoMember(12, Name = "avatar_max_add_goodfeel")]
	public uint AvatarMaxAddGoodfeel
	{
		get
		{
			return __pbn__AvatarMaxAddGoodfeel.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarMaxAddGoodfeel = value;
		}
	}

	[ProtoMember(13, Name = "avatar_cost_plus_config_list")]
	public List<AvatarCostPlusConfig> AvatarCostPlusConfigLists { get; } = new List<AvatarCostPlusConfig>();


	[ProtoMember(15, Name = "disjoin_equipment_back_exp_percent")]
	public uint DisjoinEquipmentBackExpPercent
	{
		get
		{
			return __pbn__DisjoinEquipmentBackExpPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__DisjoinEquipmentBackExpPercent = value;
		}
	}

	[ProtoMember(31, Name = "gacha_ticket_list")]
	public List<GachaTicket> GachaTicketLists { get; } = new List<GachaTicket>();


	[ProtoMember(32, Name = "region_name")]
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

	[ProtoMember(33, Name = "min_invite_level")]
	public uint MinInviteLevel
	{
		get
		{
			return __pbn__MinInviteLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MinInviteLevel = value;
		}
	}

	[ProtoMember(35, Name = "discount_schedule_config_list")]
	public List<DiscountScheduleConfig> DiscountScheduleConfigLists { get; } = new List<DiscountScheduleConfig>();


	[ProtoMember(36, Name = "island_event_refresh_interval")]
	public uint IslandEventRefreshInterval
	{
		get
		{
			return __pbn__IslandEventRefreshInterval.GetValueOrDefault();
		}
		set
		{
			__pbn__IslandEventRefreshInterval = value;
		}
	}

	[ProtoMember(37, Name = "spaceship_begin_time")]
	public uint SpaceshipBeginTime
	{
		get
		{
			return __pbn__SpaceshipBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__SpaceshipBeginTime = value;
		}
	}

	[ProtoMember(38, Name = "spaceship_end_time")]
	public uint SpaceshipEndTime
	{
		get
		{
			return __pbn__SpaceshipEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__SpaceshipEndTime = value;
		}
	}

	[ProtoMember(39, Name = "spaceship_id")]
	public uint SpaceshipId
	{
		get
		{
			return __pbn__SpaceshipId.GetValueOrDefault();
		}
		set
		{
			__pbn__SpaceshipId = value;
		}
	}

	[ProtoMember(40, Name = "is_random_box_product_open")]
	public bool IsRandomBoxProductOpen
	{
		get
		{
			return __pbn__IsRandomBoxProductOpen.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRandomBoxProductOpen = value;
		}
	}

	[ProtoMember(41, Name = "scoin_limit")]
	public uint ScoinLimit
	{
		get
		{
			return __pbn__ScoinLimit.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoinLimit = value;
		}
	}

	[ProtoMember(42, Name = "friends_point_limit")]
	public uint FriendsPointLimit
	{
		get
		{
			return __pbn__FriendsPointLimit.GetValueOrDefault();
		}
		set
		{
			__pbn__FriendsPointLimit = value;
		}
	}

	[ProtoMember(43, Name = "product_random_box_begin_time")]
	public uint ProductRandomBoxBeginTime
	{
		get
		{
			return __pbn__ProductRandomBoxBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ProductRandomBoxBeginTime = value;
		}
	}

	[ProtoMember(44, Name = "product_random_box_end_time")]
	public uint ProductRandomBoxEndTime
	{
		get
		{
			return __pbn__ProductRandomBoxEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ProductRandomBoxEndTime = value;
		}
	}

	[ProtoMember(45, Name = "week_day_activity_coin_item_id")]
	public uint WeekDayActivityCoinItemId
	{
		get
		{
			return __pbn__WeekDayActivityCoinItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__WeekDayActivityCoinItemId = value;
		}
	}

	[ProtoMember(46, Name = "island_visit_max_help_num")]
	public uint IslandVisitMaxHelpNum
	{
		get
		{
			return __pbn__IslandVisitMaxHelpNum.GetValueOrDefault();
		}
		set
		{
			__pbn__IslandVisitMaxHelpNum = value;
		}
	}

	[ProtoMember(47, Name = "cost_senior_equip_after_cancel_protect_min_time_diff")]
	public uint CostSeniorEquipAfterCancelProtectMinTimeDiff
	{
		get
		{
			return __pbn__CostSeniorEquipAfterCancelProtectMinTimeDiff.GetValueOrDefault();
		}
		set
		{
			__pbn__CostSeniorEquipAfterCancelProtectMinTimeDiff = value;
		}
	}

	[ProtoMember(50, Name = "bulletin_activity_list")]
	public List<BulletinActivityConfig> BulletinActivityLists { get; } = new List<BulletinActivityConfig>();


	[ProtoMember(51, Name = "armada_discovery_reward_list")]
	public List<ArmadaDiscoveryRewardConfig> ArmadaDiscoveryRewardLists { get; } = new List<ArmadaDiscoveryRewardConfig>();


	[ProtoMember(52, Name = "rogue_stage_open_level")]
	public uint RogueStageOpenLevel
	{
		get
		{
			return __pbn__RogueStageOpenLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__RogueStageOpenLevel = value;
		}
	}

	[ProtoMember(53, Name = "armada_begin_time")]
	public uint ArmadaBeginTime
	{
		get
		{
			return __pbn__ArmadaBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ArmadaBeginTime = value;
		}
	}

	[ProtoMember(54, Name = "armada_end_time")]
	public uint ArmadaEndTime
	{
		get
		{
			return __pbn__ArmadaEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ArmadaEndTime = value;
		}
	}

	[ProtoMember(55, Name = "armada_custom_content")]
	[DefaultValue("")]
	public string ArmadaCustomContent
	{
		get
		{
			return __pbn__ArmadaCustomContent ?? "";
		}
		set
		{
			__pbn__ArmadaCustomContent = value;
		}
	}

	[ProtoMember(56, Name = "stigmata_rune_open_level")]
	public uint StigmataRuneOpenLevel
	{
		get
		{
			return __pbn__StigmataRuneOpenLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataRuneOpenLevel = value;
		}
	}

	[ProtoMember(57, Name = "stigmata_title_open_level")]
	public uint StigmataTitleOpenLevel
	{
		get
		{
			return __pbn__StigmataTitleOpenLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataTitleOpenLevel = value;
		}
	}

	[ProtoMember(58, Name = "stigmata_disjoin_title_back_exp_percent")]
	public uint StigmataDisjoinTitleBackExpPercent
	{
		get
		{
			return __pbn__StigmataDisjoinTitleBackExpPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataDisjoinTitleBackExpPercent = value;
		}
	}

	[ProtoMember(59, Name = "stigmata_promote_max_times")]
	public uint StigmataPromoteMaxTimes
	{
		get
		{
			return __pbn__StigmataPromoteMaxTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataPromoteMaxTimes = value;
		}
	}

	[ProtoMember(60, Name = "stigmata_rune_combat_ratio")]
	public uint StigmataRuneCombatRatio
	{
		get
		{
			return __pbn__StigmataRuneCombatRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataRuneCombatRatio = value;
		}
	}

	[ProtoMember(61, Name = "stigmata_disjoin_back_exp_percent")]
	public uint StigmataDisjoinBackExpPercent
	{
		get
		{
			return __pbn__StigmataDisjoinBackExpPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataDisjoinBackExpPercent = value;
		}
	}

	[ProtoMember(62, Name = "stigmata_refine_value_back_percent")]
	public uint StigmataRefineValueBackPercent
	{
		get
		{
			return __pbn__StigmataRefineValueBackPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataRefineValueBackPercent = value;
		}
	}

	[ProtoMember(63, Name = "stigmata_title_exp_material_list")]
	public uint[] StigmataTitleExpMaterialLists { get; set; }

	[ProtoMember(64, Name = "scoin_convert_per_exp")]
	public uint ScoinConvertPerExp
	{
		get
		{
			return __pbn__ScoinConvertPerExp.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoinConvertPerExp = value;
		}
	}

	[ProtoMember(65, Name = "mp_reset_skill_hcoin_cost")]
	public uint MpResetSkillHcoinCost
	{
		get
		{
			return __pbn__MpResetSkillHcoinCost.GetValueOrDefault();
		}
		set
		{
			__pbn__MpResetSkillHcoinCost = value;
		}
	}

	[ProtoMember(66, Name = "mp_member_max_times_for_all_act")]
	public uint MpMemberMaxTimesForAllAct
	{
		get
		{
			return __pbn__MpMemberMaxTimesForAllAct.GetValueOrDefault();
		}
		set
		{
			__pbn__MpMemberMaxTimesForAllAct = value;
		}
	}

	[ProtoMember(67, Name = "mp_member_exhausted_drop_ratio")]
	public uint MpMemberExhaustedDropRatio
	{
		get
		{
			return __pbn__MpMemberExhaustedDropRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__MpMemberExhaustedDropRatio = value;
		}
	}

	[ProtoMember(68, Name = "friend_bond_level_config_list")]
	public List<FriendBondLevelConfig> FriendBondLevelConfigLists { get; } = new List<FriendBondLevelConfig>();


	[ProtoMember(69, Name = "mp_punish_mp_exp_reduce_rate")]
	public uint MpPunishMpExpReduceRate
	{
		get
		{
			return __pbn__MpPunishMpExpReduceRate.GetValueOrDefault();
		}
		set
		{
			__pbn__MpPunishMpExpReduceRate = value;
		}
	}

	[ProtoMember(70, Name = "friend_assistant_add_bond")]
	public uint FriendAssistantAddBond
	{
		get
		{
			return __pbn__FriendAssistantAddBond.GetValueOrDefault();
		}
		set
		{
			__pbn__FriendAssistantAddBond = value;
		}
	}

	[ProtoMember(72, Name = "armada_manage_mail_length")]
	public uint ArmadaManageMailLength
	{
		get
		{
			return __pbn__ArmadaManageMailLength.GetValueOrDefault();
		}
		set
		{
			__pbn__ArmadaManageMailLength = value;
		}
	}

	[ProtoMember(73, Name = "armada_contact_length")]
	public uint ArmadaContactLength
	{
		get
		{
			return __pbn__ArmadaContactLength.GetValueOrDefault();
		}
		set
		{
			__pbn__ArmadaContactLength = value;
		}
	}

	[ProtoMember(74, Name = "armada_manage_mail_cd")]
	public uint ArmadaManageMailCd
	{
		get
		{
			return __pbn__ArmadaManageMailCd.GetValueOrDefault();
		}
		set
		{
			__pbn__ArmadaManageMailCd = value;
		}
	}

	[ProtoMember(75, Name = "armada_bulletin_length")]
	public uint ArmadaBulletinLength
	{
		get
		{
			return __pbn__ArmadaBulletinLength.GetValueOrDefault();
		}
		set
		{
			__pbn__ArmadaBulletinLength = value;
		}
	}

	[ProtoMember(76, Name = "openworld_judge_para_type1")]
	public uint OpenworldJudgeParaType1
	{
		get
		{
			return __pbn__OpenworldJudgeParaType1.GetValueOrDefault();
		}
		set
		{
			__pbn__OpenworldJudgeParaType1 = value;
		}
	}

	[ProtoMember(77, Name = "openworld_judge_para_type2")]
	public uint OpenworldJudgeParaType2
	{
		get
		{
			return __pbn__OpenworldJudgeParaType2.GetValueOrDefault();
		}
		set
		{
			__pbn__OpenworldJudgeParaType2 = value;
		}
	}

	[ProtoMember(78, Name = "openworld_judge_para_type3")]
	public uint OpenworldJudgeParaType3
	{
		get
		{
			return __pbn__OpenworldJudgeParaType3.GetValueOrDefault();
		}
		set
		{
			__pbn__OpenworldJudgeParaType3 = value;
		}
	}

	[ProtoMember(79, Name = "openworld_judge_para_type4")]
	public uint OpenworldJudgeParaType4
	{
		get
		{
			return __pbn__OpenworldJudgeParaType4.GetValueOrDefault();
		}
		set
		{
			__pbn__OpenworldJudgeParaType4 = value;
		}
	}

	[ProtoMember(80, Name = "trial_avatar_player_min_level")]
	public uint TrialAvatarPlayerMinLevel
	{
		get
		{
			return __pbn__TrialAvatarPlayerMinLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__TrialAvatarPlayerMinLevel = value;
		}
	}

	[ProtoMember(81, Name = "disjoin_equipment_max_batch_num")]
	public uint DisjoinEquipmentMaxBatchNum
	{
		get
		{
			return __pbn__DisjoinEquipmentMaxBatchNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DisjoinEquipmentMaxBatchNum = value;
		}
	}

	[ProtoMember(82, Name = "equip_disjoint_unlock_level")]
	public uint EquipDisjointUnlockLevel
	{
		get
		{
			return __pbn__EquipDisjointUnlockLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__EquipDisjointUnlockLevel = value;
		}
	}

	[ProtoMember(83, Name = "stage_type_limit_avatar_fight_in_venture_list")]
	public uint[] StageTypeLimitAvatarFightInVentureLists { get; set; }

	[ProtoMember(84, Name = "armada_boss_stage_open_level")]
	public uint ArmadaBossStageOpenLevel
	{
		get
		{
			return __pbn__ArmadaBossStageOpenLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ArmadaBossStageOpenLevel = value;
		}
	}

	[ProtoMember(85)]
	public AntiCheatStageCheckConfig anti_cheat_stage_check_config { get; set; }

	[ProtoMember(87, Name = "is_can_gal_touch")]
	public bool IsCanGalTouch
	{
		get
		{
			return __pbn__IsCanGalTouch.GetValueOrDefault();
		}
		set
		{
			__pbn__IsCanGalTouch = value;
		}
	}

	[ProtoMember(88, Name = "is_resistance_open")]
	public bool IsResistanceOpen
	{
		get
		{
			return __pbn__IsResistanceOpen.GetValueOrDefault();
		}
		set
		{
			__pbn__IsResistanceOpen = value;
		}
	}

	[ProtoMember(90, Name = "next_day_begin_time")]
	public uint NextDayBeginTime
	{
		get
		{
			return __pbn__NextDayBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextDayBeginTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStaminaRecoverConfigTime()
	{
		return __pbn__StaminaRecoverConfigTime.HasValue;
	}

	public void ResetStaminaRecoverConfigTime()
	{
		__pbn__StaminaRecoverConfigTime = null;
	}

	public bool ShouldSerializeAvatarReviveHcoinCost()
	{
		return __pbn__AvatarReviveHcoinCost.HasValue;
	}

	public void ResetAvatarReviveHcoinCost()
	{
		__pbn__AvatarReviveHcoinCost = null;
	}

	public bool ShouldSerializeSameTypePowerUpRate()
	{
		return __pbn__SameTypePowerUpRate.HasValue;
	}

	public void ResetSameTypePowerUpRate()
	{
		__pbn__SameTypePowerUpRate = null;
	}

	public bool ShouldSerializeDayTimeOffset()
	{
		return __pbn__DayTimeOffset.HasValue;
	}

	public void ResetDayTimeOffset()
	{
		__pbn__DayTimeOffset = null;
	}

	public bool ShouldSerializeServerCurTime()
	{
		return __pbn__ServerCurTime.HasValue;
	}

	public void ResetServerCurTime()
	{
		__pbn__ServerCurTime = null;
	}

	public bool ShouldSerializePowerUpScoinCostRate()
	{
		return __pbn__PowerUpScoinCostRate.HasValue;
	}

	public void ResetPowerUpScoinCostRate()
	{
		__pbn__PowerUpScoinCostRate = null;
	}

	public bool ShouldSerializeMaxFriendNum()
	{
		return __pbn__MaxFriendNum.HasValue;
	}

	public void ResetMaxFriendNum()
	{
		__pbn__MaxFriendNum = null;
	}

	public bool ShouldSerializeAvatarMaxAddGoodfeel()
	{
		return __pbn__AvatarMaxAddGoodfeel.HasValue;
	}

	public void ResetAvatarMaxAddGoodfeel()
	{
		__pbn__AvatarMaxAddGoodfeel = null;
	}

	public bool ShouldSerializeDisjoinEquipmentBackExpPercent()
	{
		return __pbn__DisjoinEquipmentBackExpPercent.HasValue;
	}

	public void ResetDisjoinEquipmentBackExpPercent()
	{
		__pbn__DisjoinEquipmentBackExpPercent = null;
	}

	public bool ShouldSerializeRegionName()
	{
		return __pbn__RegionName != null;
	}

	public void ResetRegionName()
	{
		__pbn__RegionName = null;
	}

	public bool ShouldSerializeMinInviteLevel()
	{
		return __pbn__MinInviteLevel.HasValue;
	}

	public void ResetMinInviteLevel()
	{
		__pbn__MinInviteLevel = null;
	}

	public bool ShouldSerializeIslandEventRefreshInterval()
	{
		return __pbn__IslandEventRefreshInterval.HasValue;
	}

	public void ResetIslandEventRefreshInterval()
	{
		__pbn__IslandEventRefreshInterval = null;
	}

	public bool ShouldSerializeSpaceshipBeginTime()
	{
		return __pbn__SpaceshipBeginTime.HasValue;
	}

	public void ResetSpaceshipBeginTime()
	{
		__pbn__SpaceshipBeginTime = null;
	}

	public bool ShouldSerializeSpaceshipEndTime()
	{
		return __pbn__SpaceshipEndTime.HasValue;
	}

	public void ResetSpaceshipEndTime()
	{
		__pbn__SpaceshipEndTime = null;
	}

	public bool ShouldSerializeSpaceshipId()
	{
		return __pbn__SpaceshipId.HasValue;
	}

	public void ResetSpaceshipId()
	{
		__pbn__SpaceshipId = null;
	}

	public bool ShouldSerializeIsRandomBoxProductOpen()
	{
		return __pbn__IsRandomBoxProductOpen.HasValue;
	}

	public void ResetIsRandomBoxProductOpen()
	{
		__pbn__IsRandomBoxProductOpen = null;
	}

	public bool ShouldSerializeScoinLimit()
	{
		return __pbn__ScoinLimit.HasValue;
	}

	public void ResetScoinLimit()
	{
		__pbn__ScoinLimit = null;
	}

	public bool ShouldSerializeFriendsPointLimit()
	{
		return __pbn__FriendsPointLimit.HasValue;
	}

	public void ResetFriendsPointLimit()
	{
		__pbn__FriendsPointLimit = null;
	}

	public bool ShouldSerializeProductRandomBoxBeginTime()
	{
		return __pbn__ProductRandomBoxBeginTime.HasValue;
	}

	public void ResetProductRandomBoxBeginTime()
	{
		__pbn__ProductRandomBoxBeginTime = null;
	}

	public bool ShouldSerializeProductRandomBoxEndTime()
	{
		return __pbn__ProductRandomBoxEndTime.HasValue;
	}

	public void ResetProductRandomBoxEndTime()
	{
		__pbn__ProductRandomBoxEndTime = null;
	}

	public bool ShouldSerializeWeekDayActivityCoinItemId()
	{
		return __pbn__WeekDayActivityCoinItemId.HasValue;
	}

	public void ResetWeekDayActivityCoinItemId()
	{
		__pbn__WeekDayActivityCoinItemId = null;
	}

	public bool ShouldSerializeIslandVisitMaxHelpNum()
	{
		return __pbn__IslandVisitMaxHelpNum.HasValue;
	}

	public void ResetIslandVisitMaxHelpNum()
	{
		__pbn__IslandVisitMaxHelpNum = null;
	}

	public bool ShouldSerializeCostSeniorEquipAfterCancelProtectMinTimeDiff()
	{
		return __pbn__CostSeniorEquipAfterCancelProtectMinTimeDiff.HasValue;
	}

	public void ResetCostSeniorEquipAfterCancelProtectMinTimeDiff()
	{
		__pbn__CostSeniorEquipAfterCancelProtectMinTimeDiff = null;
	}

	public bool ShouldSerializeRogueStageOpenLevel()
	{
		return __pbn__RogueStageOpenLevel.HasValue;
	}

	public void ResetRogueStageOpenLevel()
	{
		__pbn__RogueStageOpenLevel = null;
	}

	public bool ShouldSerializeArmadaBeginTime()
	{
		return __pbn__ArmadaBeginTime.HasValue;
	}

	public void ResetArmadaBeginTime()
	{
		__pbn__ArmadaBeginTime = null;
	}

	public bool ShouldSerializeArmadaEndTime()
	{
		return __pbn__ArmadaEndTime.HasValue;
	}

	public void ResetArmadaEndTime()
	{
		__pbn__ArmadaEndTime = null;
	}

	public bool ShouldSerializeArmadaCustomContent()
	{
		return __pbn__ArmadaCustomContent != null;
	}

	public void ResetArmadaCustomContent()
	{
		__pbn__ArmadaCustomContent = null;
	}

	public bool ShouldSerializeStigmataRuneOpenLevel()
	{
		return __pbn__StigmataRuneOpenLevel.HasValue;
	}

	public void ResetStigmataRuneOpenLevel()
	{
		__pbn__StigmataRuneOpenLevel = null;
	}

	public bool ShouldSerializeStigmataTitleOpenLevel()
	{
		return __pbn__StigmataTitleOpenLevel.HasValue;
	}

	public void ResetStigmataTitleOpenLevel()
	{
		__pbn__StigmataTitleOpenLevel = null;
	}

	public bool ShouldSerializeStigmataDisjoinTitleBackExpPercent()
	{
		return __pbn__StigmataDisjoinTitleBackExpPercent.HasValue;
	}

	public void ResetStigmataDisjoinTitleBackExpPercent()
	{
		__pbn__StigmataDisjoinTitleBackExpPercent = null;
	}

	public bool ShouldSerializeStigmataPromoteMaxTimes()
	{
		return __pbn__StigmataPromoteMaxTimes.HasValue;
	}

	public void ResetStigmataPromoteMaxTimes()
	{
		__pbn__StigmataPromoteMaxTimes = null;
	}

	public bool ShouldSerializeStigmataRuneCombatRatio()
	{
		return __pbn__StigmataRuneCombatRatio.HasValue;
	}

	public void ResetStigmataRuneCombatRatio()
	{
		__pbn__StigmataRuneCombatRatio = null;
	}

	public bool ShouldSerializeStigmataDisjoinBackExpPercent()
	{
		return __pbn__StigmataDisjoinBackExpPercent.HasValue;
	}

	public void ResetStigmataDisjoinBackExpPercent()
	{
		__pbn__StigmataDisjoinBackExpPercent = null;
	}

	public bool ShouldSerializeStigmataRefineValueBackPercent()
	{
		return __pbn__StigmataRefineValueBackPercent.HasValue;
	}

	public void ResetStigmataRefineValueBackPercent()
	{
		__pbn__StigmataRefineValueBackPercent = null;
	}

	public bool ShouldSerializeScoinConvertPerExp()
	{
		return __pbn__ScoinConvertPerExp.HasValue;
	}

	public void ResetScoinConvertPerExp()
	{
		__pbn__ScoinConvertPerExp = null;
	}

	public bool ShouldSerializeMpResetSkillHcoinCost()
	{
		return __pbn__MpResetSkillHcoinCost.HasValue;
	}

	public void ResetMpResetSkillHcoinCost()
	{
		__pbn__MpResetSkillHcoinCost = null;
	}

	public bool ShouldSerializeMpMemberMaxTimesForAllAct()
	{
		return __pbn__MpMemberMaxTimesForAllAct.HasValue;
	}

	public void ResetMpMemberMaxTimesForAllAct()
	{
		__pbn__MpMemberMaxTimesForAllAct = null;
	}

	public bool ShouldSerializeMpMemberExhaustedDropRatio()
	{
		return __pbn__MpMemberExhaustedDropRatio.HasValue;
	}

	public void ResetMpMemberExhaustedDropRatio()
	{
		__pbn__MpMemberExhaustedDropRatio = null;
	}

	public bool ShouldSerializeMpPunishMpExpReduceRate()
	{
		return __pbn__MpPunishMpExpReduceRate.HasValue;
	}

	public void ResetMpPunishMpExpReduceRate()
	{
		__pbn__MpPunishMpExpReduceRate = null;
	}

	public bool ShouldSerializeFriendAssistantAddBond()
	{
		return __pbn__FriendAssistantAddBond.HasValue;
	}

	public void ResetFriendAssistantAddBond()
	{
		__pbn__FriendAssistantAddBond = null;
	}

	public bool ShouldSerializeArmadaManageMailLength()
	{
		return __pbn__ArmadaManageMailLength.HasValue;
	}

	public void ResetArmadaManageMailLength()
	{
		__pbn__ArmadaManageMailLength = null;
	}

	public bool ShouldSerializeArmadaContactLength()
	{
		return __pbn__ArmadaContactLength.HasValue;
	}

	public void ResetArmadaContactLength()
	{
		__pbn__ArmadaContactLength = null;
	}

	public bool ShouldSerializeArmadaManageMailCd()
	{
		return __pbn__ArmadaManageMailCd.HasValue;
	}

	public void ResetArmadaManageMailCd()
	{
		__pbn__ArmadaManageMailCd = null;
	}

	public bool ShouldSerializeArmadaBulletinLength()
	{
		return __pbn__ArmadaBulletinLength.HasValue;
	}

	public void ResetArmadaBulletinLength()
	{
		__pbn__ArmadaBulletinLength = null;
	}

	public bool ShouldSerializeOpenworldJudgeParaType1()
	{
		return __pbn__OpenworldJudgeParaType1.HasValue;
	}

	public void ResetOpenworldJudgeParaType1()
	{
		__pbn__OpenworldJudgeParaType1 = null;
	}

	public bool ShouldSerializeOpenworldJudgeParaType2()
	{
		return __pbn__OpenworldJudgeParaType2.HasValue;
	}

	public void ResetOpenworldJudgeParaType2()
	{
		__pbn__OpenworldJudgeParaType2 = null;
	}

	public bool ShouldSerializeOpenworldJudgeParaType3()
	{
		return __pbn__OpenworldJudgeParaType3.HasValue;
	}

	public void ResetOpenworldJudgeParaType3()
	{
		__pbn__OpenworldJudgeParaType3 = null;
	}

	public bool ShouldSerializeOpenworldJudgeParaType4()
	{
		return __pbn__OpenworldJudgeParaType4.HasValue;
	}

	public void ResetOpenworldJudgeParaType4()
	{
		__pbn__OpenworldJudgeParaType4 = null;
	}

	public bool ShouldSerializeTrialAvatarPlayerMinLevel()
	{
		return __pbn__TrialAvatarPlayerMinLevel.HasValue;
	}

	public void ResetTrialAvatarPlayerMinLevel()
	{
		__pbn__TrialAvatarPlayerMinLevel = null;
	}

	public bool ShouldSerializeDisjoinEquipmentMaxBatchNum()
	{
		return __pbn__DisjoinEquipmentMaxBatchNum.HasValue;
	}

	public void ResetDisjoinEquipmentMaxBatchNum()
	{
		__pbn__DisjoinEquipmentMaxBatchNum = null;
	}

	public bool ShouldSerializeEquipDisjointUnlockLevel()
	{
		return __pbn__EquipDisjointUnlockLevel.HasValue;
	}

	public void ResetEquipDisjointUnlockLevel()
	{
		__pbn__EquipDisjointUnlockLevel = null;
	}

	public bool ShouldSerializeArmadaBossStageOpenLevel()
	{
		return __pbn__ArmadaBossStageOpenLevel.HasValue;
	}

	public void ResetArmadaBossStageOpenLevel()
	{
		__pbn__ArmadaBossStageOpenLevel = null;
	}

	public bool ShouldSerializeIsCanGalTouch()
	{
		return __pbn__IsCanGalTouch.HasValue;
	}

	public void ResetIsCanGalTouch()
	{
		__pbn__IsCanGalTouch = null;
	}

	public bool ShouldSerializeIsResistanceOpen()
	{
		return __pbn__IsResistanceOpen.HasValue;
	}

	public void ResetIsResistanceOpen()
	{
		__pbn__IsResistanceOpen = null;
	}

	public bool ShouldSerializeNextDayBeginTime()
	{
		return __pbn__NextDayBeginTime.HasValue;
	}

	public void ResetNextDayBeginTime()
	{
		__pbn__NextDayBeginTime = null;
	}
}
