using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishConsignedOrderReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2643
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	private uint? __pbn__ConsignId;

	private uint? __pbn__Progress;

	private uint? __pbn__OrderLevel;

	private uint? __pbn__ReliableId;

	[ProtoMember(1, Name = "item")]
	public EquipmentItem Item { get; set; }

	[ProtoMember(2, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(3, Name = "consign_id")]
	public uint ConsignId
	{
		get
		{
			return __pbn__ConsignId.GetValueOrDefault();
		}
		set
		{
			__pbn__ConsignId = value;
		}
	}

	[ProtoMember(4, Name = "progress")]
	public uint Progress
	{
		get
		{
			return __pbn__Progress.GetValueOrDefault();
		}
		set
		{
			__pbn__Progress = value;
		}
	}

	[ProtoMember(5, Name = "order_level")]
	public uint OrderLevel
	{
		get
		{
			return __pbn__OrderLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__OrderLevel = value;
		}
	}

	[ProtoMember(6, Name = "order")]
	public ConsignedOrder Order { get; set; }

	[ProtoMember(7, Name = "next_order")]
	public ConsignedOrder NextOrder { get; set; }

	[ProtoMember(8, Name = "reliable_id")]
	public uint ReliableId
	{
		get
		{
			return __pbn__ReliableId.GetValueOrDefault();
		}
		set
		{
			__pbn__ReliableId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeConsignId()
	{
		return __pbn__ConsignId.HasValue;
	}

	public void ResetConsignId()
	{
		__pbn__ConsignId = null;
	}

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeOrderLevel()
	{
		return __pbn__OrderLevel.HasValue;
	}

	public void ResetOrderLevel()
	{
		__pbn__OrderLevel = null;
	}

	public bool ShouldSerializeReliableId()
	{
		return __pbn__ReliableId.HasValue;
	}

	public void ResetReliableId()
	{
		__pbn__ReliableId = null;
	}
}
