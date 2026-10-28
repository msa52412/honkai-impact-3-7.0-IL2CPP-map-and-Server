using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarChallenge : IExtensible
{
	private IExtension __pbn__extensionData;

	private bool? __pbn__IsChallengeFail;

	private uint? __pbn__ElfId;

	private uint? __pbn__ExtraItemId;

	private uint? __pbn__RefreshTeleportTimes;

	private uint? __pbn__ChallengeScore;

	private uint? __pbn__RandomSeed;

	private uint? __pbn__StepLevel;

	private bool? __pbn__IsUseVirtualAvatar;

	private uint? __pbn__GainCoinNum;

	private uint? __pbn__RefreshTeleportTimesLimit;

	private uint? __pbn__AssistantExtraItemId;

	[ProtoMember(1, Name = "is_challenge_fail")]
	public bool IsChallengeFail
	{
		get
		{
			return __pbn__IsChallengeFail.GetValueOrDefault();
		}
		set
		{
			__pbn__IsChallengeFail = value;
		}
	}

	[ProtoMember(2, Name = "avatar_list")]
	public List<GodWarChallengeAvatar> AvatarLists { get; } = new List<GodWarChallengeAvatar>();


	[ProtoMember(3, Name = "support_avatar_id_list")]
	public uint[] SupportAvatarIdLists { get; set; }

	[ProtoMember(4, Name = "elf_id")]
	public uint ElfId
	{
		get
		{
			return __pbn__ElfId.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfId = value;
		}
	}

	[ProtoMember(5, Name = "extra_item_id")]
	public uint ExtraItemId
	{
		get
		{
			return __pbn__ExtraItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ExtraItemId = value;
		}
	}

	[ProtoMember(6, Name = "refresh_teleport_times")]
	public uint RefreshTeleportTimes
	{
		get
		{
			return __pbn__RefreshTeleportTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshTeleportTimes = value;
		}
	}

	[ProtoMember(7, Name = "punish_buff_list")]
	public List<GodWarPunishBuff> PunishBuffLists { get; } = new List<GodWarPunishBuff>();


	[ProtoMember(8, Name = "challenge_score")]
	public uint ChallengeScore
	{
		get
		{
			return __pbn__ChallengeScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ChallengeScore = value;
		}
	}

	[ProtoMember(9, Name = "random_seed")]
	public uint RandomSeed
	{
		get
		{
			return __pbn__RandomSeed.GetValueOrDefault();
		}
		set
		{
			__pbn__RandomSeed = value;
		}
	}

	[ProtoMember(10, Name = "step_level")]
	public uint StepLevel
	{
		get
		{
			return __pbn__StepLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__StepLevel = value;
		}
	}

	[ProtoMember(11, Name = "punish_buff_id_list")]
	public uint[] PunishBuffIdLists { get; set; }

	[ProtoMember(12, Name = "is_use_virtual_avatar")]
	public bool IsUseVirtualAvatar
	{
		get
		{
			return __pbn__IsUseVirtualAvatar.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUseVirtualAvatar = value;
		}
	}

	[ProtoMember(13, Name = "boon_buff_id_list")]
	public uint[] BoonBuffIdLists { get; set; }

	[ProtoMember(14, Name = "gain_coin_num")]
	public uint GainCoinNum
	{
		get
		{
			return __pbn__GainCoinNum.GetValueOrDefault();
		}
		set
		{
			__pbn__GainCoinNum = value;
		}
	}

	[ProtoMember(15, Name = "refresh_teleport_times_limit")]
	public uint RefreshTeleportTimesLimit
	{
		get
		{
			return __pbn__RefreshTeleportTimesLimit.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshTeleportTimesLimit = value;
		}
	}

	[ProtoMember(16, Name = "assistant_extra_item_id")]
	public uint AssistantExtraItemId
	{
		get
		{
			return __pbn__AssistantExtraItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistantExtraItemId = value;
		}
	}

	[ProtoMember(17, Name = "bonus_list")]
	public List<GodWarBonus> BonusLists { get; } = new List<GodWarBonus>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsChallengeFail()
	{
		return __pbn__IsChallengeFail.HasValue;
	}

	public void ResetIsChallengeFail()
	{
		__pbn__IsChallengeFail = null;
	}

	public bool ShouldSerializeElfId()
	{
		return __pbn__ElfId.HasValue;
	}

	public void ResetElfId()
	{
		__pbn__ElfId = null;
	}

	public bool ShouldSerializeExtraItemId()
	{
		return __pbn__ExtraItemId.HasValue;
	}

	public void ResetExtraItemId()
	{
		__pbn__ExtraItemId = null;
	}

	public bool ShouldSerializeRefreshTeleportTimes()
	{
		return __pbn__RefreshTeleportTimes.HasValue;
	}

	public void ResetRefreshTeleportTimes()
	{
		__pbn__RefreshTeleportTimes = null;
	}

	public bool ShouldSerializeChallengeScore()
	{
		return __pbn__ChallengeScore.HasValue;
	}

	public void ResetChallengeScore()
	{
		__pbn__ChallengeScore = null;
	}

	public bool ShouldSerializeRandomSeed()
	{
		return __pbn__RandomSeed.HasValue;
	}

	public void ResetRandomSeed()
	{
		__pbn__RandomSeed = null;
	}

	public bool ShouldSerializeStepLevel()
	{
		return __pbn__StepLevel.HasValue;
	}

	public void ResetStepLevel()
	{
		__pbn__StepLevel = null;
	}

	public bool ShouldSerializeIsUseVirtualAvatar()
	{
		return __pbn__IsUseVirtualAvatar.HasValue;
	}

	public void ResetIsUseVirtualAvatar()
	{
		__pbn__IsUseVirtualAvatar = null;
	}

	public bool ShouldSerializeGainCoinNum()
	{
		return __pbn__GainCoinNum.HasValue;
	}

	public void ResetGainCoinNum()
	{
		__pbn__GainCoinNum = null;
	}

	public bool ShouldSerializeRefreshTeleportTimesLimit()
	{
		return __pbn__RefreshTeleportTimesLimit.HasValue;
	}

	public void ResetRefreshTeleportTimesLimit()
	{
		__pbn__RefreshTeleportTimesLimit = null;
	}

	public bool ShouldSerializeAssistantExtraItemId()
	{
		return __pbn__AssistantExtraItemId.HasValue;
	}

	public void ResetAssistantExtraItemId()
	{
		__pbn__AssistantExtraItemId = null;
	}
}
