using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityGetShopRsp : IExtensible
{
	[ProtoContract]
	public class ShopGoods : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__GoodsId;

		private uint? __pbn__MaterialId;

		private uint? __pbn__LeftBuyTimes;

		private uint? __pbn__CostMaterialId;

		private uint? __pbn__CostNum;

		private uint? __pbn__LimitType;

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

		[ProtoMember(2, Name = "material_id")]
		public uint MaterialId
		{
			get
			{
				return __pbn__MaterialId.GetValueOrDefault();
			}
			set
			{
				__pbn__MaterialId = value;
			}
		}

		[ProtoMember(3, Name = "left_buy_times")]
		public uint LeftBuyTimes
		{
			get
			{
				return __pbn__LeftBuyTimes.GetValueOrDefault();
			}
			set
			{
				__pbn__LeftBuyTimes = value;
			}
		}

		[ProtoMember(4, Name = "cost_material_id")]
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

		[ProtoMember(5, Name = "cost_num")]
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

		[ProtoMember(6, Name = "limit_type")]
		public uint LimitType
		{
			get
			{
				return __pbn__LimitType.GetValueOrDefault();
			}
			set
			{
				__pbn__LimitType = value;
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

		public bool ShouldSerializeMaterialId()
		{
			return __pbn__MaterialId.HasValue;
		}

		public void ResetMaterialId()
		{
			__pbn__MaterialId = null;
		}

		public bool ShouldSerializeLeftBuyTimes()
		{
			return __pbn__LeftBuyTimes.HasValue;
		}

		public void ResetLeftBuyTimes()
		{
			__pbn__LeftBuyTimes = null;
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

		public bool ShouldSerializeLimitType()
		{
			return __pbn__LimitType.HasValue;
		}

		public void ResetLimitType()
		{
			__pbn__LimitType = null;
		}
	}

	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6467
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

	private uint? __pbn__BuildingUniqueId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "head")]
	public TownCommonHead Head { get; set; }

	[ProtoMember(3, Name = "building_unique_id")]
	public uint BuildingUniqueId
	{
		get
		{
			return __pbn__BuildingUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuildingUniqueId = value;
		}
	}

	[ProtoMember(4, Name = "goods_list")]
	public List<ShopGoods> GoodsLists { get; } = new List<ShopGoods>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBuildingUniqueId()
	{
		return __pbn__BuildingUniqueId.HasValue;
	}

	public void ResetBuildingUniqueId()
	{
		__pbn__BuildingUniqueId = null;
	}
}
