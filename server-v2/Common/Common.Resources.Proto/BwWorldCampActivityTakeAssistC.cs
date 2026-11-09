using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BwWorldCampActivityTakeAssistContributionReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7679
	}

	private IExtension __pbn__extensionData;

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
[ProtoContract]
public class BwWorldCampActivityTakeAssistContributionRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7680
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
		[ProtoEnum(Name = "NO_CONTRIBUTION")]
		NoContribution,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
