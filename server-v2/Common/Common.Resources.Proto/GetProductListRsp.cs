using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetProductListRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6707
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "DEVICE_ERROR")]
		DeviceError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__NextRandomBoxProductRefreshTime;

	private uint? __pbn__NextLimitProductRefreshTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "product_list")]
	public List<Product> ProductLists { get; } = new List<Product>();


	[ProtoMember(3, Name = "next_random_box_product_refresh_time")]
	public uint NextRandomBoxProductRefreshTime
	{
		get
		{
			return __pbn__NextRandomBoxProductRefreshTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextRandomBoxProductRefreshTime = value;
		}
	}

	[ProtoMember(4, Name = "next_limit_product_refresh_time")]
	public uint NextLimitProductRefreshTime
	{
		get
		{
			return __pbn__NextLimitProductRefreshTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextLimitProductRefreshTime = value;
		}
	}

	[ProtoMember(5, Name = "foundation_list")]
	public List<FoundationData> FoundationLists { get; } = new List<FoundationData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNextRandomBoxProductRefreshTime()
	{
		return __pbn__NextRandomBoxProductRefreshTime.HasValue;
	}

	public void ResetNextRandomBoxProductRefreshTime()
	{
		__pbn__NextRandomBoxProductRefreshTime = null;
	}

	public bool ShouldSerializeNextLimitProductRefreshTime()
	{
		return __pbn__NextLimitProductRefreshTime.HasValue;
	}

	public void ResetNextLimitProductRefreshTime()
	{
		__pbn__NextLimitProductRefreshTime = null;
	}
}
