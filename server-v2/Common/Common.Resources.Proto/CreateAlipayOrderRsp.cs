using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CreateAlipayOrderRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1495
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private string __pbn__QrCodeUrl;

	private string __pbn__ProductName;

	private string __pbn__OrderString;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "qr_code_url")]
	[DefaultValue("")]
	public string QrCodeUrl
	{
		get
		{
			return __pbn__QrCodeUrl ?? "";
		}
		set
		{
			__pbn__QrCodeUrl = value;
		}
	}

	[ProtoMember(3, Name = "product_name")]
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

	[ProtoMember(4, Name = "order_string")]
	[DefaultValue("")]
	public string OrderString
	{
		get
		{
			return __pbn__OrderString ?? "";
		}
		set
		{
			__pbn__OrderString = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeQrCodeUrl()
	{
		return __pbn__QrCodeUrl != null;
	}

	public void ResetQrCodeUrl()
	{
		__pbn__QrCodeUrl = null;
	}

	public bool ShouldSerializeProductName()
	{
		return __pbn__ProductName != null;
	}

	public void ResetProductName()
	{
		__pbn__ProductName = null;
	}

	public bool ShouldSerializeOrderString()
	{
		return __pbn__OrderString != null;
	}

	public void ResetOrderString()
	{
		__pbn__OrderString = null;
	}
}
