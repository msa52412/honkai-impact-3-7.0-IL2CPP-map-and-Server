using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExchangeHcoinByMcoinReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6733
	}

	private IExtension __pbn__extensionData;

	private string __pbn__ProductName;

	private uint? __pbn__McoinPrice;

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

	[ProtoMember(2, Name = "mcoin_price")]
	public uint McoinPrice
	{
		get
		{
			return __pbn__McoinPrice.GetValueOrDefault();
		}
		set
		{
			__pbn__McoinPrice = value;
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

	public bool ShouldSerializeMcoinPrice()
	{
		return __pbn__McoinPrice.HasValue;
	}

	public void ResetMcoinPrice()
	{
		__pbn__McoinPrice = null;
	}
}
