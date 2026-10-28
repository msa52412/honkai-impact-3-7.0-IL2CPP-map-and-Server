using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldQuestionSettleNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2485
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private bool? __pbn__IsRight;

	private uint? __pbn__QuestionId;

	private uint? __pbn__RewardId;

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

	[ProtoMember(2, Name = "is_right")]
	public bool IsRight
	{
		get
		{
			return __pbn__IsRight.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRight = value;
		}
	}

	[ProtoMember(3, Name = "question_id")]
	public uint QuestionId
	{
		get
		{
			return __pbn__QuestionId.GetValueOrDefault();
		}
		set
		{
			__pbn__QuestionId = value;
		}
	}

	[ProtoMember(4, Name = "reward_id")]
	public uint RewardId
	{
		get
		{
			return __pbn__RewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardId = value;
		}
	}

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

	public bool ShouldSerializeIsRight()
	{
		return __pbn__IsRight.HasValue;
	}

	public void ResetIsRight()
	{
		__pbn__IsRight = null;
	}

	public bool ShouldSerializeQuestionId()
	{
		return __pbn__QuestionId.HasValue;
	}

	public void ResetQuestionId()
	{
		__pbn__QuestionId = null;
	}

	public bool ShouldSerializeRewardId()
	{
		return __pbn__RewardId.HasValue;
	}

	public void ResetRewardId()
	{
		__pbn__RewardId = null;
	}
}
