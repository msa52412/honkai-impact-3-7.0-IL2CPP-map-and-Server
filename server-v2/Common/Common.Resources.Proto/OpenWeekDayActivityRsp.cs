using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenWeekDayActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 535
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "IS_OPEN")]
		IsOpen,
		[ProtoEnum(Name = "HAS_OPEN")]
		HasOpen,
		[ProtoEnum(Name = "VALID_OPEN")]
		ValidOpen,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "SCOIN_LACK")]
		ScoinLack
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
