using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExchangeWeekDayActivityTimesRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 260
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen,
		[ProtoEnum(Name = "COIN_LACK")]
		CoinLack,
		[ProtoEnum(Name = "TIMES_NOT_ZERO")]
		TimesNotZero
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CoinCost;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "coin_cost")]
	public uint CoinCost
	{
		get
		{
			return __pbn__CoinCost.GetValueOrDefault();
		}
		set
		{
			__pbn__CoinCost = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCoinCost()
	{
		return __pbn__CoinCost.HasValue;
	}

	public void ResetCoinCost()
	{
		__pbn__CoinCost = null;
	}
}
