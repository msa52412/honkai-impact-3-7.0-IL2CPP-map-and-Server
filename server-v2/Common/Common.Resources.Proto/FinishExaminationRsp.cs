using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishExaminationRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4326
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HAS_FINISHED")]
		HasFinished,
		[ProtoEnum(Name = "NOT_CUR_EXAMINATION")]
		NotCurExamination
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ExaminationId;

	private bool? __pbn__IsFinished;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "examination_id")]
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

	[ProtoMember(4, Name = "reward_data")]
	public RewardData RewardData { get; set; }

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

	public bool ShouldSerializeIsFinished()
	{
		return __pbn__IsFinished.HasValue;
	}

	public void ResetIsFinished()
	{
		__pbn__IsFinished = null;
	}
}
