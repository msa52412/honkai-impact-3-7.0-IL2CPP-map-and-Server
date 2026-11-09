using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldSetMechaTeamRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4507
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "INDEX_ERROR")]
		IndexError,
		[ProtoEnum(Name = "MECHA_NUM_ERROR")]
		MechaNumError,
		[ProtoEnum(Name = "MECHA_REPEAT")]
		MechaRepeat,
		[ProtoEnum(Name = "MECHA_NOT_EXIST")]
		MechaNotExist,
		[ProtoEnum(Name = "MECHA_POS_ERROR")]
		MechaPosError,
		[ProtoEnum(Name = "POS_REPEAT")]
		PosRepeat,
		[ProtoEnum(Name = "CLASS_REPEAT")]
		ClassRepeat,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
