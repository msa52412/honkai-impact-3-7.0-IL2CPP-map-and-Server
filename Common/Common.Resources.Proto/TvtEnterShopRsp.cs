using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtEnterShopRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3360
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_BATTLE")]
		NotInBattle
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EnterShopOrder;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "enter_shop_order")]
	public uint EnterShopOrder
	{
		get
		{
			return __pbn__EnterShopOrder.GetValueOrDefault();
		}
		set
		{
			__pbn__EnterShopOrder = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEnterShopOrder()
	{
		return __pbn__EnterShopOrder.HasValue;
	}

	public void ResetEnterShopOrder()
	{
		__pbn__EnterShopOrder = null;
	}
}
