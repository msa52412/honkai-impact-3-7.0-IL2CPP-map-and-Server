using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StaminaExchangeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 19
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
		[ProtoEnum(Name = "STAMINA_FULL")]
		StaminaFull
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HcoinCost;

	private uint? __pbn__StaminaGet;

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

	[ProtoMember(3, Name = "stamina_get")]
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
