using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuyControllableBoxGachaTicketReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4712
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GachaId;

	[ProtoMember(1, Name = "gacha_id")]
	public uint GachaId
	{
		get
		{
			return __pbn__GachaId.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGachaId()
	{
		return __pbn__GachaId.HasValue;
	}

	public void ResetGachaId()
	{
		__pbn__GachaId = null;
	}
}
[ProtoContract]
public class BuyControllableBoxGachaTicketRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4713
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "GACHA_ID_ERROR")]
		GachaIdError,
		[ProtoEnum(Name = "HCOIN_LACK")]
		HcoinLack,
		[ProtoEnum(Name = "EQUIPMENT_MATERIAL_LIMIT")]
		EquipmentMaterialLimit,
		[ProtoEnum(Name = "NOT_OPENT")]
		NotOpent,
		[ProtoEnum(Name = "NOT_LACK")]
		NotLack,
		[ProtoEnum(Name = "GACHA_EMPTY")]
		GachaEmpty
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MaterialId;

	private uint? __pbn__Num;

	private uint? __pbn__HcoinCost;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "material_id")]
	public uint MaterialId
	{
		get
		{
			return __pbn__MaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__MaterialId = value;
		}
	}

	[ProtoMember(3, Name = "num")]
	public uint Num
	{
		get
		{
			return __pbn__Num.GetValueOrDefault();
		}
		set
		{
			__pbn__Num = value;
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMaterialId()
	{
		return __pbn__MaterialId.HasValue;
	}

	public void ResetMaterialId()
	{
		__pbn__MaterialId = null;
	}

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}

	public bool ShouldSerializeHcoinCost()
	{
		return __pbn__HcoinCost.HasValue;
	}

	public void ResetHcoinCost()
	{
		__pbn__HcoinCost = null;
	}
}
