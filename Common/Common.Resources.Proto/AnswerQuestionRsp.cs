using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AnswerQuestionRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4261
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "NO_QUESTION")]
		NoQuestion,
		[ProtoEnum(Name = "HAVE_ANSWER")]
		HaveAnswer,
		[ProtoEnum(Name = "NO_ANSWER")]
		NoAnswer,
		[ProtoEnum(Name = "NOT_JOIN_ARMADA")]
		NotJoinArmada
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "question")]
	public Question Question { get; set; }

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
