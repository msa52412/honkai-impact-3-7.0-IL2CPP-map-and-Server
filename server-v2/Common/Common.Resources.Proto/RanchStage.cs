using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RanchStage : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__TakeChallengeRewardTimes;

	private uint? __pbn__MaxChallengeScore;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "take_challenge_reward_times")]
	public uint TakeChallengeRewardTimes
	{
		get
		{
			return __pbn__TakeChallengeRewardTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__TakeChallengeRewardTimes = value;
		}
	}

	[ProtoMember(3, Name = "max_challenge_score")]
	public uint MaxChallengeScore
	{
		get
		{
			return __pbn__MaxChallengeScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxChallengeScore = value;
		}
	}

	[ProtoMember(4, Name = "drop_monster_list")]
	public List<RanchMonster> DropMonsterLists { get; } = new List<RanchMonster>();


	[ProtoMember(5, Name = "unique_id_list")]
	public uint[] UniqueIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeTakeChallengeRewardTimes()
	{
		return __pbn__TakeChallengeRewardTimes.HasValue;
	}

	public void ResetTakeChallengeRewardTimes()
	{
		__pbn__TakeChallengeRewardTimes = null;
	}

	public bool ShouldSerializeMaxChallengeScore()
	{
		return __pbn__MaxChallengeScore.HasValue;
	}

	public void ResetMaxChallengeScore()
	{
		__pbn__MaxChallengeScore = null;
	}
}
