using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RewardLineActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__Score;

	private uint? __pbn__LastTakenRewardScore;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "schedule_id")]
	public uint ScheduleId
	{
		get
		{
			return __pbn__ScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScheduleId = value;
		}
	}

	[ProtoMember(3, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(4, Name = "last_taken_reward_score")]
	public uint LastTakenRewardScore
	{
		get
		{
			return __pbn__LastTakenRewardScore.GetValueOrDefault();
		}
		set
		{
			__pbn__LastTakenRewardScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeLastTakenRewardScore()
	{
		return __pbn__LastTakenRewardScore.HasValue;
	}

	public void ResetLastTakenRewardScore()
	{
		__pbn__LastTakenRewardScore = null;
	}
}
