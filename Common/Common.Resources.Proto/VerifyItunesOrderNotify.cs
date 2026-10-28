using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VerifyItunesOrderNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 82
	}

	private IExtension __pbn__extensionData;

	private string __pbn__ReceiptData;

	private string __pbn__DeviceUuid;

	private bool? __pbn__IsAutoReissue;

	private string __pbn__CurrencyCode;

	private string __pbn__PriceAmount;

	[ProtoMember(1, Name = "receipt_data")]
	[DefaultValue("")]
	public string ReceiptData
	{
		get
		{
			return __pbn__ReceiptData ?? "";
		}
		set
		{
			__pbn__ReceiptData = value;
		}
	}

	[ProtoMember(2, Name = "device_uuid")]
	[DefaultValue("")]
	public string DeviceUuid
	{
		get
		{
			return __pbn__DeviceUuid ?? "";
		}
		set
		{
			__pbn__DeviceUuid = value;
		}
	}

	[ProtoMember(3, Name = "is_auto_reissue")]
	public bool IsAutoReissue
	{
		get
		{
			return __pbn__IsAutoReissue.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAutoReissue = value;
		}
	}

	[ProtoMember(4, Name = "currency_code")]
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

	[ProtoMember(5, Name = "price_amount")]
	[DefaultValue("")]
	public string PriceAmount
	{
		get
		{
			return __pbn__PriceAmount ?? "";
		}
		set
		{
			__pbn__PriceAmount = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReceiptData()
	{
		return __pbn__ReceiptData != null;
	}

	public void ResetReceiptData()
	{
		__pbn__ReceiptData = null;
	}

	public bool ShouldSerializeDeviceUuid()
	{
		return __pbn__DeviceUuid != null;
	}

	public void ResetDeviceUuid()
	{
		__pbn__DeviceUuid = null;
	}

	public bool ShouldSerializeIsAutoReissue()
	{
		return __pbn__IsAutoReissue.HasValue;
	}

	public void ResetIsAutoReissue()
	{
		__pbn__IsAutoReissue = null;
	}

	public bool ShouldSerializeCurrencyCode()
	{
		return __pbn__CurrencyCode != null;
	}

	public void ResetCurrencyCode()
	{
		__pbn__CurrencyCode = null;
	}

	public bool ShouldSerializePriceAmount()
	{
		return __pbn__PriceAmount != null;
	}

	public void ResetPriceAmount()
	{
		__pbn__PriceAmount = null;
	}
}
