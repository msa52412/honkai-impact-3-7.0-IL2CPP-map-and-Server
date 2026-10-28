using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExchangeRedeemCodeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 214
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "REDEEM_CODE_ERROR")]
		RedeemCodeError,
		[ProtoEnum(Name = "REDEEM_CODE_NUM_LACK")]
		RedeemCodeNumLack,
		[ProtoEnum(Name = "REDEEM_TAKE_NUM_LACK")]
		RedeemTakeNumLack,
		[ProtoEnum(Name = "REDEEM_CODE_EXPIRED")]
		RedeemCodeExpired,
		[ProtoEnum(Name = "REDEEM_HAS_TAKEN")]
		RedeemHasTaken,
		[ProtoEnum(Name = "NOT_PERMITTED")]
		NotPermitted,
		[ProtoEnum(Name = "EQUIPMENT_FULL")]
		EquipmentFull,
		[ProtoEnum(Name = "REDEEM_FREQ_LIMIT")]
		RedeemFreqLimit
	}

	private IExtension __pbn__extensionData;

	private string __pbn__RedeemCode;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(3, Name = "redeem_code")]
	[DefaultValue("")]
	public string RedeemCode
	{
		get
		{
			return __pbn__RedeemCode ?? "";
		}
		set
		{
			__pbn__RedeemCode = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRedeemCode()
	{
		return __pbn__RedeemCode != null;
	}

	public void ResetRedeemCode()
	{
		__pbn__RedeemCode = null;
	}
}
