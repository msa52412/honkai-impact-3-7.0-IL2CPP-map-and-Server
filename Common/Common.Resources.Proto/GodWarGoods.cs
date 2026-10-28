using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarGoods : IExtensible
{
	[ProtoContract]
	public class BuffGoods : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__BuffId;

		private uint? __pbn__BuffInitLevel;

		[ProtoMember(1, Name = "buff_id")]
		public uint BuffId
		{
			get
			{
				return __pbn__BuffId.GetValueOrDefault();
			}
			set
			{
				__pbn__BuffId = value;
			}
		}

		[ProtoMember(2, Name = "buff_init_level")]
		public uint BuffInitLevel
		{
			get
			{
				return __pbn__BuffInitLevel.GetValueOrDefault();
			}
			set
			{
				__pbn__BuffInitLevel = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeBuffId()
		{
			return __pbn__BuffId.HasValue;
		}

		public void ResetBuffId()
		{
			__pbn__BuffId = null;
		}

		public bool ShouldSerializeBuffInitLevel()
		{
			return __pbn__BuffInitLevel.HasValue;
		}

		public void ResetBuffInitLevel()
		{
			__pbn__BuffInitLevel = null;
		}
	}

	[ProtoContract]
	public class LevelUpBuffGoods : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__BuffId;

		private uint? __pbn__BuffCurLevel;

		[ProtoMember(1, Name = "buff_id")]
		public uint BuffId
		{
			get
			{
				return __pbn__BuffId.GetValueOrDefault();
			}
			set
			{
				__pbn__BuffId = value;
			}
		}

		[ProtoMember(2, Name = "buff_cur_level")]
		public uint BuffCurLevel
		{
			get
			{
				return __pbn__BuffCurLevel.GetValueOrDefault();
			}
			set
			{
				__pbn__BuffCurLevel = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeBuffId()
		{
			return __pbn__BuffId.HasValue;
		}

		public void ResetBuffId()
		{
			__pbn__BuffId = null;
		}

		public bool ShouldSerializeBuffCurLevel()
		{
			return __pbn__BuffCurLevel.HasValue;
		}

		public void ResetBuffCurLevel()
		{
			__pbn__BuffCurLevel = null;
		}
	}

	[ProtoContract]
	public enum GoodsType
	{
		[ProtoEnum(Name = "GOODS_TYPE_NONE")]
		GoodsTypeNone,
		[ProtoEnum(Name = "GOODS_TYPE_BUFF")]
		GoodsTypeBuff,
		[ProtoEnum(Name = "GOODS_TYPE_LEVEL_UP_BUFF")]
		GoodsTypeLevelUpBuff
	}

	private IExtension __pbn__extensionData;

	private GoodsType? __pbn__goods_type;

	private uint? __pbn__GoodsNum;

	private uint? __pbn__GoodsIdx;

	private uint? __pbn__CoinCost;

	[ProtoMember(1)]
	[DefaultValue(GoodsType.GoodsTypeNone)]
	public GoodsType goods_type
	{
		get
		{
			return __pbn__goods_type.GetValueOrDefault();
		}
		set
		{
			__pbn__goods_type = value;
		}
	}

	[ProtoMember(2, Name = "goods_num")]
	public uint GoodsNum
	{
		get
		{
			return __pbn__GoodsNum.GetValueOrDefault();
		}
		set
		{
			__pbn__GoodsNum = value;
		}
	}

	[ProtoMember(3, Name = "goods_idx")]
	public uint GoodsIdx
	{
		get
		{
			return __pbn__GoodsIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__GoodsIdx = value;
		}
	}

	[ProtoMember(4, Name = "coin_cost")]
	public uint CoinCost
	{
		get
		{
			return __pbn__CoinCost.GetValueOrDefault();
		}
		set
		{
			__pbn__CoinCost = value;
		}
	}

	[ProtoMember(21)]
	public BuffGoods buff_goods { get; set; }

	[ProtoMember(22, Name = "levelup_buff_goods")]
	public LevelUpBuffGoods LevelupBuffGoods { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializegoods_type()
	{
		return __pbn__goods_type.HasValue;
	}

	public void Resetgoods_type()
	{
		__pbn__goods_type = null;
	}

	public bool ShouldSerializeGoodsNum()
	{
		return __pbn__GoodsNum.HasValue;
	}

	public void ResetGoodsNum()
	{
		__pbn__GoodsNum = null;
	}

	public bool ShouldSerializeGoodsIdx()
	{
		return __pbn__GoodsIdx.HasValue;
	}

	public void ResetGoodsIdx()
	{
		__pbn__GoodsIdx = null;
	}

	public bool ShouldSerializeCoinCost()
	{
		return __pbn__CoinCost.HasValue;
	}

	public void ResetCoinCost()
	{
		__pbn__CoinCost = null;
	}
}
