using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RecommendGoods : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ShopId;

	private string __pbn__BannerBg;

	private uint? __pbn__AccurateDeliverScheduleId;

	[ProtoMember(1, Name = "shop_id")]
	public uint ShopId
	{
		get
		{
			return __pbn__ShopId.GetValueOrDefault();
		}
		set
		{
			__pbn__ShopId = value;
		}
	}

	[ProtoMember(2, Name = "goods")]
	public Goods Goods { get; set; }

	[ProtoMember(3, Name = "banner_bg")]
	[DefaultValue("")]
	public string BannerBg
	{
		get
		{
			return __pbn__BannerBg ?? "";
		}
		set
		{
			__pbn__BannerBg = value;
		}
	}

	[ProtoMember(4, Name = "accurate_deliver_schedule_id")]
	public uint AccurateDeliverScheduleId
	{
		get
		{
			return __pbn__AccurateDeliverScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__AccurateDeliverScheduleId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeShopId()
	{
		return __pbn__ShopId.HasValue;
	}

	public void ResetShopId()
	{
		__pbn__ShopId = null;
	}

	public bool ShouldSerializeBannerBg()
	{
		return __pbn__BannerBg != null;
	}

	public void ResetBannerBg()
	{
		__pbn__BannerBg = null;
	}

	public bool ShouldSerializeAccurateDeliverScheduleId()
	{
		return __pbn__AccurateDeliverScheduleId.HasValue;
	}

	public void ResetAccurateDeliverScheduleId()
	{
		__pbn__AccurateDeliverScheduleId = null;
	}
}
