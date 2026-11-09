using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomWarExchangeCurrencyRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3063
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "POINT_ERROR")]
		PointError,
		[ProtoEnum(Name = "COST_MATERIAL_LACK")]
		CostMaterialLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PointId;

	private uint? __pbn__CurrencyNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "point_id")]
	public uint PointId
	{
		get
		{
			return __pbn__PointId.GetValueOrDefault();
		}
		set
		{
			__pbn__PointId = value;
		}
	}

	[ProtoMember(3, Name = "currency_num")]
	public uint CurrencyNum
	{
		get
		{
			return __pbn__CurrencyNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CurrencyNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePointId()
	{
		return __pbn__PointId.HasValue;
	}

	public void ResetPointId()
	{
		__pbn__PointId = null;
	}

	public bool ShouldSerializeCurrencyNum()
	{
		return __pbn__CurrencyNum.HasValue;
	}

	public void ResetCurrencyNum()
	{
		__pbn__CurrencyNum = null;
	}
}
