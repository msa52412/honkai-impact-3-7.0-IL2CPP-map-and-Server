using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetAdventureGroupRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3901
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

	private uint? __pbn__Level;

	private uint? __pbn__Badge;

	private uint? __pbn__LastExamination;

	private uint? __pbn__Grain;

	private uint? __pbn__HasTakeRewardLevel;

	private uint? __pbn__NextScoinAddTime;

	private uint? __pbn__StorageScoin;

	private uint? __pbn__StorageStamina;

	private uint? __pbn__TodayResetQuestNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "level")]
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

	[ProtoMember(3, Name = "badge")]
	public uint Badge
	{
		get
		{
			return __pbn__Badge.GetValueOrDefault();
		}
		set
		{
			__pbn__Badge = value;
		}
	}

	[ProtoMember(4, Name = "last_examination")]
	public uint LastExamination
	{
		get
		{
			return __pbn__LastExamination.GetValueOrDefault();
		}
		set
		{
			__pbn__LastExamination = value;
		}
	}

	[ProtoMember(5, Name = "avatar_list")]
	public List<AdventureGroupAvatar> AvatarLists { get; } = new List<AdventureGroupAvatar>();


	[ProtoMember(6, Name = "quest_list")]
	public List<AdventureGroupQuest> QuestLists { get; } = new List<AdventureGroupQuest>();


	[ProtoMember(7, Name = "available_quest_list")]
	public List<AdventureGroupAvailableQuest> AvailableQuestLists { get; } = new List<AdventureGroupAvailableQuest>();


	[ProtoMember(8, Name = "grain")]
	public uint Grain
	{
		get
		{
			return __pbn__Grain.GetValueOrDefault();
		}
		set
		{
			__pbn__Grain = value;
		}
	}

	[ProtoMember(9, Name = "has_take_reward_level")]
	public uint HasTakeRewardLevel
	{
		get
		{
			return __pbn__HasTakeRewardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeRewardLevel = value;
		}
	}

	[ProtoMember(10, Name = "next_scoin_add_time")]
	public uint NextScoinAddTime
	{
		get
		{
			return __pbn__NextScoinAddTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextScoinAddTime = value;
		}
	}

	[ProtoMember(11, Name = "storage_scoin")]
	public uint StorageScoin
	{
		get
		{
			return __pbn__StorageScoin.GetValueOrDefault();
		}
		set
		{
			__pbn__StorageScoin = value;
		}
	}

	[ProtoMember(12, Name = "storage_stamina")]
	public uint StorageStamina
	{
		get
		{
			return __pbn__StorageStamina.GetValueOrDefault();
		}
		set
		{
			__pbn__StorageStamina = value;
		}
	}

	[ProtoMember(13, Name = "has_take_compensation_list")]
	public uint[] HasTakeCompensationLists { get; set; }

	[ProtoMember(14, Name = "cabin_level_list")]
	public List<CabinLevel> CabinLevelLists { get; } = new List<CabinLevel>();


	[ProtoMember(15, Name = "decoration_slot_list")]
	public List<AdventureDecorationSlot> DecorationSlotLists { get; } = new List<AdventureDecorationSlot>();


	[ProtoMember(16, Name = "facility_level_list")]
	public List<FacilityLevel> FacilityLevelLists { get; } = new List<FacilityLevel>();


	[ProtoMember(17, Name = "today_reset_quest_num")]
	public uint TodayResetQuestNum
	{
		get
		{
			return __pbn__TodayResetQuestNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayResetQuestNum = value;
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

	public bool ShouldSerializeBadge()
	{
		return __pbn__Badge.HasValue;
	}

	public void ResetBadge()
	{
		__pbn__Badge = null;
	}

	public bool ShouldSerializeLastExamination()
	{
		return __pbn__LastExamination.HasValue;
	}

	public void ResetLastExamination()
	{
		__pbn__LastExamination = null;
	}

	public bool ShouldSerializeGrain()
	{
		return __pbn__Grain.HasValue;
	}

	public void ResetGrain()
	{
		__pbn__Grain = null;
	}

	public bool ShouldSerializeHasTakeRewardLevel()
	{
		return __pbn__HasTakeRewardLevel.HasValue;
	}

	public void ResetHasTakeRewardLevel()
	{
		__pbn__HasTakeRewardLevel = null;
	}

	public bool ShouldSerializeNextScoinAddTime()
	{
		return __pbn__NextScoinAddTime.HasValue;
	}

	public void ResetNextScoinAddTime()
	{
		__pbn__NextScoinAddTime = null;
	}

	public bool ShouldSerializeStorageScoin()
	{
		return __pbn__StorageScoin.HasValue;
	}

	public void ResetStorageScoin()
	{
		__pbn__StorageScoin = null;
	}

	public bool ShouldSerializeStorageStamina()
	{
		return __pbn__StorageStamina.HasValue;
	}

	public void ResetStorageStamina()
	{
		__pbn__StorageStamina = null;
	}

	public bool ShouldSerializeTodayResetQuestNum()
	{
		return __pbn__TodayResetQuestNum.HasValue;
	}

	public void ResetTodayResetQuestNum()
	{
		__pbn__TodayResetQuestNum = null;
	}
}
