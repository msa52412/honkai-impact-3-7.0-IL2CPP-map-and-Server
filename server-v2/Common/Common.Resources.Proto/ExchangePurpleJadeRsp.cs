using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExchangePurpleJadeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1368
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LACK_HCOIN")]
		LackHcoin,
		[ProtoEnum(Name = "CAN_NOT_EXCHANGE")]
		CanNotExchange,
		[ProtoEnum(Name = "LIMIT_FOR_EXCHANGE")]
		LimitForExchange,
		[ProtoEnum(Name = "LACK_EXCHANGE_NUM")]
		LackExchangeNum,
		[ProtoEnum(Name = "LACK_MCOIN")]
		LackMcoin
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HcoinCost;

	private uint? __pbn__PurpleJadeAdded;

	private uint? __pbn__McoinCost;

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

	[ProtoMember(3, Name = "purple_jade_added")]
	public uint PurpleJadeAdded
	{
		get
		{
			return __pbn__PurpleJadeAdded.GetValueOrDefault();
		}
		set
		{
			__pbn__PurpleJadeAdded = value;
		}
	}

	[ProtoMember(4, Name = "mcoin_cost")]
	public uint McoinCost
	{
		get
		{
			return __pbn__McoinCost.GetValueOrDefault();
		}
		set
		{
			__pbn__McoinCost = value;
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

	public bool ShouldSerializePurpleJadeAdded()
	{
		return __pbn__PurpleJadeAdded.HasValue;
	}

	public void ResetPurpleJadeAdded()
	{
		__pbn__PurpleJadeAdded = null;
	}

	public bool ShouldSerializeMcoinCost()
	{
		return __pbn__McoinCost.HasValue;
	}

	public void ResetMcoinCost()
	{
		__pbn__McoinCost = null;
	}
}
