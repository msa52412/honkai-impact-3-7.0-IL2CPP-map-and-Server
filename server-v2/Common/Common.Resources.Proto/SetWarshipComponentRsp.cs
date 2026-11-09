using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetWarshipComponentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5459
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "WARSHIP_NOT_EXIST")]
		WarshipNotExist,
		[ProtoEnum(Name = "COMPONENT_TYPE_ERROR")]
		ComponentTypeError,
		[ProtoEnum(Name = "COMPONENT_NOT_EXIST")]
		ComponentNotExist,
		[ProtoEnum(Name = "WARSHIP_ERROR")]
		WarshipError
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
