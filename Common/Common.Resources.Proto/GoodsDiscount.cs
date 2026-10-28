using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GoodsDiscount : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__Discount;

	[ProtoMember(1, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(2, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(3, Name = "discount")]
	public uint Discount
	{
		get
		{
			return __pbn__Discount.GetValueOrDefault();
		}
		set
		{
			__pbn__Discount = value;
		}
	}

	[ProtoMember(4, Name = "goods_cost")]
	public BuyGoodsCost GoodsCost { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeDiscount()
	{
		return __pbn__Discount.HasValue;
	}

	public void ResetDiscount()
	{
		__pbn__Discount = null;
	}
}
