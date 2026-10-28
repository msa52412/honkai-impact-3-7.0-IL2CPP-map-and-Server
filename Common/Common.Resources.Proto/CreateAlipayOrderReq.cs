using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CreateAlipayOrderReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1494
	}

	[ProtoContract]
	public enum TradeType
	{
		[ProtoEnum(Name = "NONE")]
		None,
		[ProtoEnum(Name = "APP")]
		App,
		[ProtoEnum(Name = "QR_CODE")]
		QrCode,
		[ProtoEnum(Name = "HUA_BEI")]
		HuaBei
	}

	private IExtension __pbn__extensionData;

	private string __pbn__ProductName;

	private string __pbn__TotalFee;

	private string __pbn__NotifyUrl;

	private string __pbn__Body;

	private TradeType? __pbn__trade_type;

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

	[ProtoMember(2, Name = "total_fee")]
	[DefaultValue("")]
	public string TotalFee
	{
		get
		{
			return __pbn__TotalFee ?? "";
		}
		set
		{
			__pbn__TotalFee = value;
		}
	}

	[ProtoMember(3, Name = "notify_url")]
	[DefaultValue("")]
	public string NotifyUrl
	{
		get
		{
			return __pbn__NotifyUrl ?? "";
		}
		set
		{
			__pbn__NotifyUrl = value;
		}
	}

	[ProtoMember(4, Name = "body")]
	[DefaultValue("")]
	public string Body
	{
		get
		{
			return __pbn__Body ?? "";
		}
		set
		{
			__pbn__Body = value;
		}
	}

	[ProtoMember(5)]
	[DefaultValue(TradeType.None)]
	public TradeType trade_type
	{
		get
		{
			return __pbn__trade_type.GetValueOrDefault();
		}
		set
		{
			__pbn__trade_type = value;
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

	public bool ShouldSerializeTotalFee()
	{
		return __pbn__TotalFee != null;
	}

	public void ResetTotalFee()
	{
		__pbn__TotalFee = null;
	}

	public bool ShouldSerializeNotifyUrl()
	{
		return __pbn__NotifyUrl != null;
	}

	public void ResetNotifyUrl()
	{
		__pbn__NotifyUrl = null;
	}

	public bool ShouldSerializeBody()
	{
		return __pbn__Body != null;
	}

	public void ResetBody()
	{
		__pbn__Body = null;
	}

	public bool ShouldSerializetrade_type()
	{
		return __pbn__trade_type.HasValue;
	}

	public void Resettrade_type()
	{
		__pbn__trade_type = null;
	}
}
