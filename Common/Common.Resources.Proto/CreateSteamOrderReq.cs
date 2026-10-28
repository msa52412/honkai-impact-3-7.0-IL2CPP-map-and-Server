using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CreateSteamOrderReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1648
	}

	private IExtension __pbn__extensionData;

	private string __pbn__ProductName;

	private string __pbn__ProductDesc;

	private string __pbn__AccountId;

	private ulong? __pbn__OrderId;

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

	[ProtoMember(2, Name = "product_desc")]
	[DefaultValue("")]
	public string ProductDesc
	{
		get
		{
			return __pbn__ProductDesc ?? "";
		}
		set
		{
			__pbn__ProductDesc = value;
		}
	}

	[ProtoMember(3, Name = "account_id")]
	[DefaultValue("")]
	public string AccountId
	{
		get
		{
			return __pbn__AccountId ?? "";
		}
		set
		{
			__pbn__AccountId = value;
		}
	}

	[ProtoMember(4, Name = "order_id")]
	public ulong OrderId
	{
		get
		{
			return __pbn__OrderId.GetValueOrDefault();
		}
		set
		{
			__pbn__OrderId = value;
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

	public bool ShouldSerializeProductDesc()
	{
		return __pbn__ProductDesc != null;
	}

	public void ResetProductDesc()
	{
		__pbn__ProductDesc = null;
	}

	public bool ShouldSerializeAccountId()
	{
		return __pbn__AccountId != null;
	}

	public void ResetAccountId()
	{
		__pbn__AccountId = null;
	}

	public bool ShouldSerializeOrderId()
	{
		return __pbn__OrderId.HasValue;
	}

	public void ResetOrderId()
	{
		__pbn__OrderId = null;
	}
}
