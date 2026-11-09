using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class JoinArmadaElectionRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2694
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada,
		[ProtoEnum(Name = "IMPEACH_NOT_START")]
		ImpeachNotStart,
		[ProtoEnum(Name = "IN_OPTIONAL_LIST")]
		InOptionalList,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "ACCOUNT_NONE")]
		AccountNone
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "impeach_data")]
	public ArmadaImpeachData ImpeachData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
