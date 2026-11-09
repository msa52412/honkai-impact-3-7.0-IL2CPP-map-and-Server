using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExchangeHcoinByMcoinRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6734
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LACK_MCOIN")]
		LackMcoin,
		[ProtoEnum(Name = "PRODUCT_INVALID")]
		ProductInvalid
	}

	private IExtension __pbn__extensionData;

	private string __pbn__ProductName;

	private uint? __pbn__PayHcoin;

	private uint? __pbn__FreeHcoin;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

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

	[ProtoMember(3, Name = "pay_hcoin")]
	public uint PayHcoin
	{
		get
		{
			return __pbn__PayHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__PayHcoin = value;
		}
	}

	[ProtoMember(4, Name = "free_hcoin")]
	public uint FreeHcoin
	{
		get
		{
			return __pbn__FreeHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__FreeHcoin = value;
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

	public bool ShouldSerializePayHcoin()
	{
		return __pbn__PayHcoin.HasValue;
	}

	public void ResetPayHcoin()
	{
		__pbn__PayHcoin = null;
	}

	public bool ShouldSerializeFreeHcoin()
	{
		return __pbn__FreeHcoin.HasValue;
	}

	public void ResetFreeHcoin()
	{
		__pbn__FreeHcoin = null;
	}
}
