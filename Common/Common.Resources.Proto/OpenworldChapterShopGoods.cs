using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterShopGoods : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__GoodsId;

	private uint? __pbn__CostMaterialId;

	private uint? __pbn__CostNum;

	private bool? __pbn__IsSold;

	private uint? __pbn__NeedFameLevel;

	private uint? __pbn__RewardId;

	[ProtoMember(1, Name = "goods_id")]
	public uint GoodsId
	{
		get
		{
			return __pbn__GoodsId.GetValueOrDefault();
		}
		set
		{
			__pbn__GoodsId = value;
		}
	}

	[ProtoMember(2, Name = "cost_material_id")]
	public uint CostMaterialId
	{
		get
		{
			return __pbn__CostMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__CostMaterialId = value;
		}
	}

	[ProtoMember(3, Name = "cost_num")]
	public uint CostNum
	{
		get
		{
			return __pbn__CostNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CostNum = value;
		}
	}

	[ProtoMember(4, Name = "is_sold")]
	public bool IsSold
	{
		get
		{
			return __pbn__IsSold.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSold = value;
		}
	}

	[ProtoMember(5, Name = "need_fame_level")]
	public uint NeedFameLevel
	{
		get
		{
			return __pbn__NeedFameLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__NeedFameLevel = value;
		}
	}

	[ProtoMember(6, Name = "reward_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGoodsId()
	{
		return __pbn__GoodsId.HasValue;
	}

	public void ResetGoodsId()
	{
		__pbn__GoodsId = null;
	}

	public bool ShouldSerializeCostMaterialId()
	{
		return __pbn__CostMaterialId.HasValue;
	}

	public void ResetCostMaterialId()
	{
		__pbn__CostMaterialId = null;
	}

	public bool ShouldSerializeCostNum()
	{
		return __pbn__CostNum.HasValue;
	}

	public void ResetCostNum()
	{
		__pbn__CostNum = null;
	}

	public bool ShouldSerializeIsSold()
	{
		return __pbn__IsSold.HasValue;
	}

	public void ResetIsSold()
	{
		__pbn__IsSold = null;
	}

	public bool ShouldSerializeNeedFameLevel()
	{
		return __pbn__NeedFameLevel.HasValue;
	}

	public void ResetNeedFameLevel()
	{
		__pbn__NeedFameLevel = null;
	}

	public bool ShouldSerializeRewardId()
	{
		return __pbn__RewardId.HasValue;
	}

	public void ResetRewardId()
	{
		__pbn__RewardId = null;
	}
}
