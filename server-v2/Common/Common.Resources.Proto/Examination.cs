using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Examination : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__LastAnswer;

	private bool? __pbn__IsFinished;

	private bool? __pbn__IsEvaluated;

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

	[ProtoMember(2, Name = "last_answer")]
	public uint LastAnswer
	{
		get
		{
			return __pbn__LastAnswer.GetValueOrDefault();
		}
		set
		{
			__pbn__LastAnswer = value;
		}
	}

	[ProtoMember(3, Name = "is_finished")]
	public bool IsFinished
	{
		get
		{
			return __pbn__IsFinished.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFinished = value;
		}
	}

	[ProtoMember(4, Name = "answer_list")]
	public uint[] AnswerLists { get; set; }

	[ProtoMember(5, Name = "is_evaluated")]
	public bool IsEvaluated
	{
		get
		{
			return __pbn__IsEvaluated.GetValueOrDefault();
		}
		set
		{
			__pbn__IsEvaluated = value;
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

	public bool ShouldSerializeLastAnswer()
	{
		return __pbn__LastAnswer.HasValue;
	}

	public void ResetLastAnswer()
	{
		__pbn__LastAnswer = null;
	}

	public bool ShouldSerializeIsFinished()
	{
		return __pbn__IsFinished.HasValue;
	}

	public void ResetIsFinished()
	{
		__pbn__IsFinished = null;
	}

	public bool ShouldSerializeIsEvaluated()
	{
		return __pbn__IsEvaluated.HasValue;
	}

	public void ResetIsEvaluated()
	{
		__pbn__IsEvaluated = null;
	}
}
