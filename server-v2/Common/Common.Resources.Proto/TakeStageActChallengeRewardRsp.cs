using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeStageActChallengeRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 459
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HAS_TAKE")]
		HasTake,
		[ProtoEnum(Name = "CHALLENGE_NUM_LACK")]
		ChallengeNumLack,
		[ProtoEnum(Name = "PART_FAIL")]
		PartFail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActId;

	private uint? __pbn__Difficulty;

	private uint? __pbn__ChallengeNumIndex;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "act_id")]
	public uint ActId
	{
		get
		{
			return __pbn__ActId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActId = value;
		}
	}

	[ProtoMember(3, Name = "difficulty")]
	public uint Difficulty
	{
		get
		{
			return __pbn__Difficulty.GetValueOrDefault();
		}
		set
		{
			__pbn__Difficulty = value;
		}
	}

	[ProtoMember(4, Name = "challenge_num_index")]
	public uint ChallengeNumIndex
	{
		get
		{
			return __pbn__ChallengeNumIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__ChallengeNumIndex = value;
		}
	}

	[ProtoMember(5, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(6, Name = "succ_challenge_num_index_list")]
	public uint[] SuccChallengeNumIndexLists { get; set; }

	[ProtoMember(7, Name = "fail_challenge_num_index_list")]
	public uint[] FailChallengeNumIndexLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActId()
	{
		return __pbn__ActId.HasValue;
	}

	public void ResetActId()
	{
		__pbn__ActId = null;
	}

	public bool ShouldSerializeDifficulty()
	{
		return __pbn__Difficulty.HasValue;
	}

	public void ResetDifficulty()
	{
		__pbn__Difficulty = null;
	}

	public bool ShouldSerializeChallengeNumIndex()
	{
		return __pbn__ChallengeNumIndex.HasValue;
	}

	public void ResetChallengeNumIndex()
	{
		__pbn__ChallengeNumIndex = null;
	}
}
