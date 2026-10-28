using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetStaminaExchangeInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 17
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__UsableTimes;

	private uint? __pbn__TotalTimes;

	private uint? __pbn__HcoinCost;

	private uint? __pbn__StaminaGet;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "usable_times")]
	public uint UsableTimes
	{
		get
		{
			return __pbn__UsableTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__UsableTimes = value;
		}
	}

	[ProtoMember(3, Name = "total_times")]
	public uint TotalTimes
	{
		get
		{
			return __pbn__TotalTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalTimes = value;
		}
	}

	[ProtoMember(4, Name = "hcoin_cost")]
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

	[ProtoMember(5, Name = "stamina_get")]
	public uint StaminaGet
	{
		get
		{
			return __pbn__StaminaGet.GetValueOrDefault();
		}
		set
		{
			__pbn__StaminaGet = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUsableTimes()
	{
		return __pbn__UsableTimes.HasValue;
	}

	public void ResetUsableTimes()
	{
		__pbn__UsableTimes = null;
	}

	public bool ShouldSerializeTotalTimes()
	{
		return __pbn__TotalTimes.HasValue;
	}

	public void ResetTotalTimes()
	{
		__pbn__TotalTimes = null;
	}

	public bool ShouldSerializeHcoinCost()
	{
		return __pbn__HcoinCost.HasValue;
	}

	public void ResetHcoinCost()
	{
		__pbn__HcoinCost = null;
	}

	public bool ShouldSerializeStaminaGet()
	{
		return __pbn__StaminaGet.HasValue;
	}

	public void ResetStaminaGet()
	{
		__pbn__StaminaGet = null;
	}
}
