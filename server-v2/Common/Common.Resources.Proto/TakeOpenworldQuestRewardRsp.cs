using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeOpenworldQuestRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4483
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_FINISH")]
		NotFinish,
		[ProtoEnum(Name = "MAP_NOT_FOUND")]
		MapNotFound,
		[ProtoEnum(Name = "QUEST_NOT_FOUND")]
		QuestNotFound
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AddStar;

	private uint? __pbn__ChallengeScore;

	private uint? __pbn__OldQuestLevel;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(3, Name = "day_reward_list")]
	public List<RewardData> DayRewardLists { get; } = new List<RewardData>();


	[ProtoMember(4, Name = "add_star")]
	public uint AddStar
	{
		get
		{
			return __pbn__AddStar.GetValueOrDefault();
		}
		set
		{
			__pbn__AddStar = value;
		}
	}

	[ProtoMember(5, Name = "challenge_score")]
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

	[ProtoMember(6, Name = "old_quest_level")]
	public uint OldQuestLevel
	{
		get
		{
			return __pbn__OldQuestLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__OldQuestLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAddStar()
	{
		return __pbn__AddStar.HasValue;
	}

	public void ResetAddStar()
	{
		__pbn__AddStar = null;
	}

	public bool ShouldSerializeChallengeScore()
	{
		return __pbn__ChallengeScore.HasValue;
	}

	public void ResetChallengeScore()
	{
		__pbn__ChallengeScore = null;
	}

	public bool ShouldSerializeOldQuestLevel()
	{
		return __pbn__OldQuestLevel.HasValue;
	}

	public void ResetOldQuestLevel()
	{
		__pbn__OldQuestLevel = null;
	}
}
