using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldQuestion : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__QuestionId;

	private ChatworldQuestionStatus? __pbn__Status;

	private uint? __pbn__EndTime;

	private uint? __pbn__RightAnswerId;

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

	[ProtoMember(2, Name = "question_id")]
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

	[ProtoMember(3, Name = "status")]
	[DefaultValue(ChatworldQuestionStatus.ChatworldQuestionNone)]
	public ChatworldQuestionStatus Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(4, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(5, Name = "right_answer_id")]
	public uint RightAnswerId
	{
		get
		{
			return __pbn__RightAnswerId.GetValueOrDefault();
		}
		set
		{
			__pbn__RightAnswerId = value;
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

	public bool ShouldSerializeQuestionId()
	{
		return __pbn__QuestionId.HasValue;
	}

	public void ResetQuestionId()
	{
		__pbn__QuestionId = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeRightAnswerId()
	{
		return __pbn__RightAnswerId.HasValue;
	}

	public void ResetRightAnswerId()
	{
		__pbn__RightAnswerId = null;
	}
}
