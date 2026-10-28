using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AddTechExpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 539
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "TECH_NOT_OPEN")]
		TechNotOpen,
		[ProtoEnum(Name = "TECH_LEVEL_FULL")]
		TechLevelFull,
		[ProtoEnum(Name = "MATERAIL_NOT_ENOUGH")]
		MaterailNotEnough,
		[ProtoEnum(Name = "MATERAIL_CAN_NOT_ADD_EXP")]
		MaterailCanNotAddExp
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
