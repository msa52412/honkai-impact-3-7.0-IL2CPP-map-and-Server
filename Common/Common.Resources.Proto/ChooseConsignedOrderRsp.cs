using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChooseConsignedOrderRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2642
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
		[ProtoEnum(Name = "STATUS_ERROR")]
		StatusError,
		[ProtoEnum(Name = "INDEX_ERROR")]
		IndexError,
		[ProtoEnum(Name = "TIMES_LIMIT")]
		TimesLimit
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "choosen_order")]
	public ConsignedOrder ChoosenOrder { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
