using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetRedeemCodeInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 212
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "REDEEM_CODE_ERROR")]
		RedeemCodeError
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Desc;

	private string __pbn__RedeemCode;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(3, Name = "desc")]
	[DefaultValue("")]
	public string Desc
	{
		get
		{
			return __pbn__Desc ?? "";
		}
		set
		{
			__pbn__Desc = value;
		}
	}

	[ProtoMember(4, Name = "redeem_code")]
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

	public bool ShouldSerializeDesc()
	{
		return __pbn__Desc != null;
	}

	public void ResetDesc()
	{
		__pbn__Desc = null;
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
