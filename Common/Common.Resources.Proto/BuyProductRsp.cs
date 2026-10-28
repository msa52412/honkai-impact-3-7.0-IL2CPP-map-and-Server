using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuyProductRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6732
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "DEVICE_ERROR")]
		DeviceError,
		[ProtoEnum(Name = "PRODUCT_INVALID")]
		ProductInvalid,
		[ProtoEnum(Name = "TIMES_LIMIT")]
		TimesLimit
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
