using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishExaminationReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4325
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ExaminationId;

	private uint? __pbn__AnswerIndex;

	[ProtoMember(1, Name = "examination_id")]
	public uint ExaminationId
	{
		get
		{
			return __pbn__ExaminationId.GetValueOrDefault();
		}
		set
		{
			__pbn__ExaminationId = value;
		}
	}

	[ProtoMember(2, Name = "answer_index")]
	public uint AnswerIndex
	{
		get
		{
			return __pbn__AnswerIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__AnswerIndex = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeExaminationId()
	{
		return __pbn__ExaminationId.HasValue;
	}

	public void ResetExaminationId()
	{
		__pbn__ExaminationId = null;
	}

	public bool ShouldSerializeAnswerIndex()
	{
		return __pbn__AnswerIndex.HasValue;
	}

	public void ResetAnswerIndex()
	{
		__pbn__AnswerIndex = null;
	}
}
