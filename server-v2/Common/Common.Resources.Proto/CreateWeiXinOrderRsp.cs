using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CreateWeiXinOrderRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 208
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

	private string __pbn__PrepayId;

	private string __pbn__PartnerId;

	private string __pbn__NonceStr;

	private string __pbn__Timestamp;

	private string __pbn__Sign;

	private string __pbn__QrCodeUrl;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "prepay_id")]
	[DefaultValue("")]
	public string PrepayId
	{
		get
		{
			return __pbn__PrepayId ?? "";
		}
		set
		{
			__pbn__PrepayId = value;
		}
	}

	[ProtoMember(3, Name = "partner_id")]
	[DefaultValue("")]
	public string PartnerId
	{
		get
		{
			return __pbn__PartnerId ?? "";
		}
		set
		{
			__pbn__PartnerId = value;
		}
	}

	[ProtoMember(4, Name = "nonce_str")]
	[DefaultValue("")]
	public string NonceStr
	{
		get
		{
			return __pbn__NonceStr ?? "";
		}
		set
		{
			__pbn__NonceStr = value;
		}
	}

	[ProtoMember(5, Name = "timestamp")]
	[DefaultValue("")]
	public string Timestamp
	{
		get
		{
			return __pbn__Timestamp ?? "";
		}
		set
		{
			__pbn__Timestamp = value;
		}
	}

	[ProtoMember(6, Name = "sign")]
	[DefaultValue("")]
	public string Sign
	{
		get
		{
			return __pbn__Sign ?? "";
		}
		set
		{
			__pbn__Sign = value;
		}
	}

	[ProtoMember(7, Name = "qr_code_url")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePrepayId()
	{
		return __pbn__PrepayId != null;
	}

	public void ResetPrepayId()
	{
		__pbn__PrepayId = null;
	}

	public bool ShouldSerializePartnerId()
	{
		return __pbn__PartnerId != null;
	}

	public void ResetPartnerId()
	{
		__pbn__PartnerId = null;
	}

	public bool ShouldSerializeNonceStr()
	{
		return __pbn__NonceStr != null;
	}

	public void ResetNonceStr()
	{
		__pbn__NonceStr = null;
	}

	public bool ShouldSerializeTimestamp()
	{
		return __pbn__Timestamp != null;
	}

	public void ResetTimestamp()
	{
		__pbn__Timestamp = null;
	}

	public bool ShouldSerializeSign()
	{
		return __pbn__Sign != null;
	}

	public void ResetSign()
	{
		__pbn__Sign = null;
	}

	public bool ShouldSerializeQrCodeUrl()
	{
		return __pbn__QrCodeUrl != null;
	}

	public void ResetQrCodeUrl()
	{
		__pbn__QrCodeUrl = null;
	}
}
