using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleTotalScoreDrop : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MaterialId;

	private uint? __pbn__DailyNum;

	[ProtoMember(1, Name = "material_id")]
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

	[ProtoMember(2, Name = "daily_num")]
	public uint DailyNum
	{
		get
		{
			return __pbn__DailyNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMaterialId()
	{
		return __pbn__MaterialId.HasValue;
	}

	public void ResetMaterialId()
	{
		__pbn__MaterialId = null;
	}

	public bool ShouldSerializeDailyNum()
	{
		return __pbn__DailyNum.HasValue;
	}

	public void ResetDailyNum()
	{
		__pbn__DailyNum = null;
	}
}
