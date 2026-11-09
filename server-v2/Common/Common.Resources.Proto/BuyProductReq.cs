using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuyProductReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6731
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Name;

	private uint? __pbn__Price;

	private string __pbn__CurrencyCode;

	private string __pbn__FormatPrice;

	[ProtoMember(1, Name = "name")]
	[DefaultValue("")]
	public string Name
	{
		get
		{
			return __pbn__Name ?? "";
		}
		set
		{
			__pbn__Name = value;
		}
	}

	[ProtoMember(2, Name = "price")]
	public uint Price
	{
		get
		{
			return __pbn__Price.GetValueOrDefault();
		}
		set
		{
			__pbn__Price = value;
		}
	}

	[ProtoMember(3, Name = "currency_code")]
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

	[ProtoMember(4, Name = "format_price")]
	[DefaultValue("")]
	public string FormatPrice
	{
		get
		{
			return __pbn__FormatPrice ?? "";
		}
		set
		{
			__pbn__FormatPrice = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}

	public bool ShouldSerializePrice()
	{
		return __pbn__Price.HasValue;
	}

	public void ResetPrice()
	{
		__pbn__Price = null;
	}

	public bool ShouldSerializeCurrencyCode()
	{
		return __pbn__CurrencyCode != null;
	}

	public void ResetCurrencyCode()
	{
		__pbn__CurrencyCode = null;
	}

	public bool ShouldSerializeFormatPrice()
	{
		return __pbn__FormatPrice != null;
	}

	public void ResetFormatPrice()
	{
		__pbn__FormatPrice = null;
	}
}
