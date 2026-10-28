using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityShopBuyRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6469
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "COIN_LACK")]
		CoinLack,
		[ProtoEnum(Name = "SOLD")]
		Sold,
		[ProtoEnum(Name = "FORBID")]
		Forbid
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GoodsId;

	private uint? __pbn__LeftBuyTimes;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "head")]
	public TownCommonHead Head { get; set; }

	[ProtoMember(3, Name = "goods_id")]
	public uint GoodsId
	{
		get
		{
			return __pbn__GoodsId.GetValueOrDefault();
		}
		set
		{
			__pbn__GoodsId = value;
		}
	}

	[ProtoMember(4, Name = "left_buy_times")]
	public uint LeftBuyTimes
	{
		get
		{
			return __pbn__LeftBuyTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftBuyTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGoodsId()
	{
		return __pbn__GoodsId.HasValue;
	}

	public void ResetGoodsId()
	{
		__pbn__GoodsId = null;
	}

	public bool ShouldSerializeLeftBuyTimes()
	{
		return __pbn__LeftBuyTimes.HasValue;
	}

	public void ResetLeftBuyTimes()
	{
		__pbn__LeftBuyTimes = null;
	}
}
