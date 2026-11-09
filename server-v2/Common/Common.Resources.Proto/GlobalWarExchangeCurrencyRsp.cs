using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalWarExchangeCurrencyRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3956
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "POINT_NOT_ACTIVE")]
		PointNotActive,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "CURRENCY_FULL")]
		CurrencyFull,
		[ProtoEnum(Name = "AREA_NOT_FINISH")]
		AreaNotFinish,
		[ProtoEnum(Name = "STAMINA_LACK")]
		StaminaLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AddCurrencyNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "add_currency_num")]
	public uint AddCurrencyNum
	{
		get
		{
			return __pbn__AddCurrencyNum.GetValueOrDefault();
		}
		set
		{
			__pbn__AddCurrencyNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAddCurrencyNum()
	{
		return __pbn__AddCurrencyNum.HasValue;
	}

	public void ResetAddCurrencyNum()
	{
		__pbn__AddCurrencyNum = null;
	}
}
