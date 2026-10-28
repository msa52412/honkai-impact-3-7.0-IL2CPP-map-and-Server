using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GardenCrop : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CropId;

	private uint? __pbn__HarvestNum;

	[ProtoMember(1, Name = "crop_id")]
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

	[ProtoMember(2, Name = "harvest_num")]
	public uint HarvestNum
	{
		get
		{
			return __pbn__HarvestNum.GetValueOrDefault();
		}
		set
		{
			__pbn__HarvestNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCropId()
	{
		return __pbn__CropId.HasValue;
	}

	public void ResetCropId()
	{
		__pbn__CropId = null;
	}

	public bool ShouldSerializeHarvestNum()
	{
		return __pbn__HarvestNum.HasValue;
	}

	public void ResetHarvestNum()
	{
		__pbn__HarvestNum = null;
	}
}
