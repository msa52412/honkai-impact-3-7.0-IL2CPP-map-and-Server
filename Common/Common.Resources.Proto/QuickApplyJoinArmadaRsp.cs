using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QuickApplyJoinArmadaRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2733
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "ALREADY_IN_ARMADA")]
		AlreadyInArmada,
		[ProtoEnum(Name = "NO_QUICK_APPLY_ARMADA")]
		NoQuickApplyArmada,
		[ProtoEnum(Name = "IN_CD_TIME")]
		InCdTime,
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
