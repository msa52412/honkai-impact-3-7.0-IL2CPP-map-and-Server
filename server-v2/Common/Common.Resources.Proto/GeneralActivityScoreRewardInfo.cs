using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GeneralActivityScoreRewardInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__CurScore;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "cur_score")]
	public uint CurScore
	{
		get
		{
			return __pbn__CurScore.GetValueOrDefault();
		}
		set
		{
			__pbn__CurScore = value;
		}
	}

	[ProtoMember(3, Name = "score_reward_taken_list")]
	public uint[] ScoreRewardTakenLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeCurScore()
	{
		return __pbn__CurScore.HasValue;
	}

	public void ResetCurScore()
	{
		__pbn__CurScore = null;
	}
}
