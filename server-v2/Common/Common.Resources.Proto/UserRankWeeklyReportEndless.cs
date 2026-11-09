using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UserRankWeeklyReportEndless : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__FinalBossId;

	private EndlessRewardType? __pbn__RewardType;

	private uint? __pbn__BattleConfigId;

	[ProtoMember(1, Name = "final_boss_id")]
	public uint FinalBossId
	{
		get
		{
			return __pbn__FinalBossId.GetValueOrDefault();
		}
		set
		{
			__pbn__FinalBossId = value;
		}
	}

	[ProtoMember(3, Name = "reward_type")]
	[DefaultValue(EndlessRewardType.EndlessPromote)]
	public EndlessRewardType RewardType
	{
		get
		{
			return __pbn__RewardType ?? EndlessRewardType.EndlessPromote;
		}
		set
		{
			__pbn__RewardType = value;
		}
	}

	[ProtoMember(4, Name = "battle_config_id")]
	public uint BattleConfigId
	{
		get
		{
			return __pbn__BattleConfigId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleConfigId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFinalBossId()
	{
		return __pbn__FinalBossId.HasValue;
	}

	public void ResetFinalBossId()
	{
		__pbn__FinalBossId = null;
	}

	public bool ShouldSerializeRewardType()
	{
		return __pbn__RewardType.HasValue;
	}

	public void ResetRewardType()
	{
		__pbn__RewardType = null;
	}

	public bool ShouldSerializeBattleConfigId()
	{
		return __pbn__BattleConfigId.HasValue;
	}

	public void ResetBattleConfigId()
	{
		__pbn__BattleConfigId = null;
	}
}
