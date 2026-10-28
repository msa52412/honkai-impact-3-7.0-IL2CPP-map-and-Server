using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReportClickRechargeButtonNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6743
	}

	private IExtension __pbn__extensionData;

	private string __pbn__ProductName;

	private string __pbn__CurrencyCode;

	private string __pbn__Price;

	[ProtoMember(1, Name = "product_name")]
	[DefaultValue("")]
	public string ProductName
	{
		get
		{
			return __pbn__ProductName ?? "";
		}
		set
		{
			__pbn__ProductName = value;
		}
	}

	[ProtoMember(2, Name = "currency_code")]
	[DefaultValue("")]
	public string CurrencyCode
	{
		get
		{
			return __pbn__CurrencyCode ?? "";
		}
		set
		{
			__pbn__CurrencyCode = value;
		}
	}

	[ProtoMember(3, Name = "price")]
	[DefaultValue("")]
	public string Price
	{
		get
		{
			return __pbn__Price ?? "";
		}
		set
		{
			__pbn__Price = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeProductName()
	{
		return __pbn__ProductName != null;
	}

	public void ResetProductName()
	{
		__pbn__ProductName = null;
	}

	public bool ShouldSerializeCurrencyCode()
	{
		return __pbn__CurrencyCode != null;
	}

	public void ResetCurrencyCode()
	{
		__pbn__CurrencyCode = null;
	}

	public bool ShouldSerializePrice()
	{
		return __pbn__Price != null;
	}

	public void ResetPrice()
	{
		__pbn__Price = null;
	}
}
