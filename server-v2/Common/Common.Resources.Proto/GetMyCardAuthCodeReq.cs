using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMyCardAuthCodeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6739
	}

	private IExtension __pbn__extensionData;

	private string __pbn__FacTradeSeq;

	private string __pbn__ProductName;

	private string __pbn__Amount;

	[ProtoMember(1, Name = "fac_trade_seq")]
	[DefaultValue("")]
	public string FacTradeSeq
	{
		get
		{
			return __pbn__FacTradeSeq ?? "";
		}
		set
		{
			__pbn__FacTradeSeq = value;
		}
	}

	[ProtoMember(2, Name = "product_name")]
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

	[ProtoMember(3, Name = "amount")]
	[DefaultValue("")]
	public string Amount
	{
		get
		{
			return __pbn__Amount ?? "";
		}
		set
		{
			__pbn__Amount = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFacTradeSeq()
	{
		return __pbn__FacTradeSeq != null;
	}

	public void ResetFacTradeSeq()
	{
		__pbn__FacTradeSeq = null;
	}

	public bool ShouldSerializeProductName()
	{
		return __pbn__ProductName != null;
	}

	public void ResetProductName()
	{
		__pbn__ProductName = null;
	}

	public bool ShouldSerializeAmount()
	{
		return __pbn__Amount != null;
	}

	public void ResetAmount()
	{
		__pbn__Amount = null;
	}
}
