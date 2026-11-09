using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TriggerRecommendGoodsNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1538
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RecommendId;

	private uint? __pbn__TriggerRecommendGoodsTime;

	[ProtoMember(1, Name = "recommend_id")]
	public uint RecommendId
	{
		get
		{
			return __pbn__RecommendId.GetValueOrDefault();
		}
		set
		{
			__pbn__RecommendId = value;
		}
	}

	[ProtoMember(2, Name = "shop_goods_schedule_id_list")]
	public uint[] ShopGoodsScheduleIdLists { get; set; }

	[ProtoMember(3, Name = "trigger_recommend_goods_time")]
	public uint TriggerRecommendGoodsTime
	{
		get
		{
			return __pbn__TriggerRecommendGoodsTime.GetValueOrDefault();
		}
		set
		{
			__pbn__TriggerRecommendGoodsTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRecommendId()
	{
		return __pbn__RecommendId.HasValue;
	}

	public void ResetRecommendId()
	{
		__pbn__RecommendId = null;
	}

	public bool ShouldSerializeTriggerRecommendGoodsTime()
	{
		return __pbn__TriggerRecommendGoodsTime.HasValue;
	}

	public void ResetTriggerRecommendGoodsTime()
	{
		__pbn__TriggerRecommendGoodsTime = null;
	}
}
