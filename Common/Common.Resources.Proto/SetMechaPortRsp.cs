using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetMechaPortRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4519
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "EQUIPPED")]
		Equipped,
		[ProtoEnum(Name = "CAN_NOT_RIDE")]
		CanNotRide,
		[ProtoEnum(Name = "NOT_EXIST")]
		NotExist,
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
