using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScoinExchangeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 15
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HCOIN_LACK")]
		HcoinLack,
		[ProtoEnum(Name = "TIMES_LACK")]
		TimesLack,
		[ProtoEnum(Name = "LACK_PURPLE_JADE")]
		LackPurpleJade
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HcoinCost;

	private uint? __pbn__ScoinGet;

	private uint? __pbn__BoostRate;

	private uint? __pbn__PurpleJadeCost;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "hcoin_cost")]
	public uint HcoinCost
	{
		get
		{
			return __pbn__HcoinCost.GetValueOrDefault();
		}
		set
		{
			__pbn__HcoinCost = value;
		}
	}

	[ProtoMember(3, Name = "scoin_get")]
	public uint ScoinGet
	{
		get
		{
			return __pbn__ScoinGet.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoinGet = value;
		}
	}

	[ProtoMember(4, Name = "boost_rate")]
	public uint BoostRate
	{
		get
		{
			return __pbn__BoostRate.GetValueOrDefault();
		}
		set
		{
			__pbn__BoostRate = value;
		}
	}

	[ProtoMember(5, Name = "purple_jade_cost")]
	public uint PurpleJadeCost
	{
		get
		{
			return __pbn__PurpleJadeCost.GetValueOrDefault();
		}
		set
		{
			__pbn__PurpleJadeCost = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHcoinCost()
	{
		return __pbn__HcoinCost.HasValue;
	}

	public void ResetHcoinCost()
	{
		__pbn__HcoinCost = null;
	}

	public bool ShouldSerializeScoinGet()
	{
		return __pbn__ScoinGet.HasValue;
	}

	public void ResetScoinGet()
	{
		__pbn__ScoinGet = null;
	}

	public bool ShouldSerializeBoostRate()
	{
		return __pbn__BoostRate.HasValue;
	}

	public void ResetBoostRate()
	{
		__pbn__BoostRate = null;
	}

	public bool ShouldSerializePurpleJadeCost()
	{
		return __pbn__PurpleJadeCost.HasValue;
	}

	public void ResetPurpleJadeCost()
	{
		__pbn__PurpleJadeCost = null;
	}
}
