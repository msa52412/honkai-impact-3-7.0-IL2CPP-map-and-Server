using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeChapterChallengeRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 968
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
		[ProtoEnum(Name = "REWARD_INVALID")]
		RewardInvalid
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

	private uint? __pbn__ChallengeNumIndex;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chapter_id")]
	public uint ChapterId
	{
		get
		{
			return __pbn__ChapterId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChapterId = value;
		}
	}

	[ProtoMember(3, Name = "challenge_num_index")]
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

	[ProtoMember(4, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChapterId()
	{
		return __pbn__ChapterId.HasValue;
	}

	public void ResetChapterId()
	{
		__pbn__ChapterId = null;
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
