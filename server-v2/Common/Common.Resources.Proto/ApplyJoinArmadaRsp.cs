using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ApplyJoinArmadaRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2608
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "STATUS_ERROR")]
		StatusError,
		[ProtoEnum(Name = "ARMADA_NOT_EXIST")]
		ArmadaNotExist,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "MEMBER_FULL")]
		MemberFull,
		[ProtoEnum(Name = "APPLY_FULL")]
		ApplyFull,
		[ProtoEnum(Name = "IN_CD_TIME")]
		InCdTime,
		[ProtoEnum(Name = "APPLY_LIMIT")]
		ApplyLimit,
		[ProtoEnum(Name = "HAS_APPLYED")]
		HasApplyed,
		[ProtoEnum(Name = "IN_LOW_POPL_CD_TIME")]
		InLowPoplCdTime,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
