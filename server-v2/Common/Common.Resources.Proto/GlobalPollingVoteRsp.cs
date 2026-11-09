using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalPollingVoteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5816
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen = 3,
		[ProtoEnum(Name = "VOTE_NOT_OPEN")]
		VoteNotOpen = 4,
		[ProtoEnum(Name = "TARGET_ID_INVALID")]
		TargetIdInvalid = 5,
		[ProtoEnum(Name = "TOO_FREQUENT")]
		TooFrequent = 6,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack = 7,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed = 8
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
