using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GardenActivityData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__GardenId;

	private uint? __pbn__DailyHarvestNum;

	private uint? __pbn__LastGrowTime;

	[ProtoMember(1, Name = "garden_id")]
	public uint GardenId
	{
		get
		{
			return __pbn__GardenId.GetValueOrDefault();
		}
		set
		{
			__pbn__GardenId = value;
		}
	}

	[ProtoMember(2, Name = "site_list")]
	public List<GardenSite> SiteLists { get; } = new List<GardenSite>();


	[ProtoMember(3, Name = "crop_list")]
	public List<GardenCrop> CropLists { get; } = new List<GardenCrop>();


	[ProtoMember(4, Name = "daily_harvest_num")]
	public uint DailyHarvestNum
	{
		get
		{
			return __pbn__DailyHarvestNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyHarvestNum = value;
		}
	}

	[ProtoMember(5, Name = "last_grow_time")]
	public uint LastGrowTime
	{
		get
		{
			return __pbn__LastGrowTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastGrowTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGardenId()
	{
		return __pbn__GardenId.HasValue;
	}

	public void ResetGardenId()
	{
		__pbn__GardenId = null;
	}

	public bool ShouldSerializeDailyHarvestNum()
	{
		return __pbn__DailyHarvestNum.HasValue;
	}

	public void ResetDailyHarvestNum()
	{
		__pbn__DailyHarvestNum = null;
	}

	public bool ShouldSerializeLastGrowTime()
	{
		return __pbn__LastGrowTime.HasValue;
	}

	public void ResetLastGrowTime()
	{
		__pbn__LastGrowTime = null;
	}
}
