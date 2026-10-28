using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AnswerQuestionReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4260
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__QuestionId;

	[ProtoMember(1, Name = "question_id")]
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

	[ProtoMember(2, Name = "answer_list")]
	public uint[] AnswerLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeQuestionId()
	{
		return __pbn__QuestionId.HasValue;
	}

	public void ResetQuestionId()
	{
		__pbn__QuestionId = null;
	}
}
