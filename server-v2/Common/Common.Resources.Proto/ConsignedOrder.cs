using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ConsignedOrder : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__ConsignerUid;

	private uint? __pbn__RewardId;

	private uint? __pbn__Progress;

	private uint? __pbn__UniqueId;

	private uint? __pbn__OrderLevel;

	private uint? __pbn__OrderCredit;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "consigner_uid")]
	public uint ConsignerUid
	{
		get
		{
			return __pbn__ConsignerUid.GetValueOrDefault();
		}
		set
		{
			__pbn__ConsignerUid = value;
		}
	}

	[ProtoMember(3, Name = "reward_id")]
	public uint RewardId
	{
		get
		{
			return __pbn__RewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardId = value;
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

	[ProtoMember(5, Name = "unique_id")]
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

	[ProtoMember(6, Name = "order_level")]
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

	[ProtoMember(7, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(8, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(9, Name = "order_credit")]
	public uint OrderCredit
	{
		get
		{
			return __pbn__OrderCredit.GetValueOrDefault();
		}
		set
		{
			__pbn__OrderCredit = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeConsignerUid()
	{
		return __pbn__ConsignerUid.HasValue;
	}

	public void ResetConsignerUid()
	{
		__pbn__ConsignerUid = null;
	}

	public bool ShouldSerializeRewardId()
	{
		return __pbn__RewardId.HasValue;
	}

	public void ResetRewardId()
	{
		__pbn__RewardId = null;
	}

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeOrderLevel()
	{
		return __pbn__OrderLevel.HasValue;
	}

	public void ResetOrderLevel()
	{
		__pbn__OrderLevel = null;
	}

	public bool ShouldSerializeOrderCredit()
	{
		return __pbn__OrderCredit.HasValue;
	}

	public void ResetOrderCredit()
	{
		__pbn__OrderCredit = null;
	}
}
