using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GardenSite : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SiteId;

	private uint? __pbn__CropId;

	private uint? __pbn__DelayBeginTime;

	[ProtoMember(1, Name = "site_id")]
	public uint SiteId
	{
		get
		{
			return __pbn__SiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__SiteId = value;
		}
	}

	[ProtoMember(2, Name = "crop_id")]
	public uint CropId
	{
		get
		{
			return __pbn__CropId.GetValueOrDefault();
		}
		set
		{
			__pbn__CropId = value;
		}
	}

	[ProtoMember(3, Name = "delay_begin_time")]
	public uint DelayBeginTime
	{
		get
		{
			return __pbn__DelayBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DelayBeginTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSiteId()
	{
		return __pbn__SiteId.HasValue;
	}

	public void ResetSiteId()
	{
		__pbn__SiteId = null;
	}

	public bool ShouldSerializeCropId()
	{
		return __pbn__CropId.HasValue;
	}

	public void ResetCropId()
	{
		__pbn__CropId = null;
	}

	public bool ShouldSerializeDelayBeginTime()
	{
		return __pbn__DelayBeginTime.HasValue;
	}

	public void ResetDelayBeginTime()
	{
		__pbn__DelayBeginTime = null;
	}
}
