using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExchangeRedeemCodeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 213
	}

	private IExtension __pbn__extensionData;

	private string __pbn__RedeemCode;

	[ProtoMember(1, Name = "redeem_code")]
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
