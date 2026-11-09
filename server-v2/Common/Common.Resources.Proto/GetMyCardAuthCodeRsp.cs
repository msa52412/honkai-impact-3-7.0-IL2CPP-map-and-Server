using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMyCardAuthCodeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6740
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

	private string __pbn__AuthCode;

	private string __pbn__TradeSeq;

	private string __pbn__ProductName;

	private string __pbn__TransactionUrl;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "auth_code")]
	[DefaultValue("")]
	public string AuthCode
	{
		get
		{
			return __pbn__AuthCode ?? "";
		}
		set
		{
			__pbn__AuthCode = value;
		}
	}

	[ProtoMember(3, Name = "trade_seq")]
	[DefaultValue("")]
	public string TradeSeq
	{
		get
		{
			return __pbn__TradeSeq ?? "";
		}
		set
		{
			__pbn__TradeSeq = value;
		}
	}

	[ProtoMember(4, Name = "product_name")]
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

	[ProtoMember(5, Name = "transaction_url")]
	[DefaultValue("")]
	public string TransactionUrl
	{
		get
		{
			return __pbn__TransactionUrl ?? "";
		}
		set
		{
			__pbn__TransactionUrl = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAuthCode()
	{
		return __pbn__AuthCode != null;
	}

	public void ResetAuthCode()
	{
		__pbn__AuthCode = null;
	}

	public bool ShouldSerializeTradeSeq()
	{
		return __pbn__TradeSeq != null;
	}

	public void ResetTradeSeq()
	{
		__pbn__TradeSeq = null;
	}

	public bool ShouldSerializeProductName()
	{
		return __pbn__ProductName != null;
	}

	public void ResetProductName()
	{
		__pbn__ProductName = null;
	}

	public bool ShouldSerializeTransactionUrl()
	{
		return __pbn__TransactionUrl != null;
	}

	public void ResetTransactionUrl()
	{
		__pbn__TransactionUrl = null;
	}
}
