using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MonopolyBuyGoodsRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3862
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LACK_MATERIAL")]
		LackMaterial,
		[ProtoEnum(Name = "BUY_TIMES_LACK")]
		BuyTimesLack,
		[ProtoEnum(Name = "INVALID_POSITION")]
		InvalidPosition,
		[ProtoEnum(Name = "STATUS_ERROR")]
		StatusError
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
