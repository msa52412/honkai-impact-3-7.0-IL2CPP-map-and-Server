using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PrayGachaItemData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__TotalNum;

	private uint? __pbn__GachaNum;

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

	[ProtoMember(2, Name = "total_num")]
	public uint TotalNum
	{
		get
		{
			return __pbn__TotalNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalNum = value;
		}
	}

	[ProtoMember(3, Name = "gacha_num")]
	public uint GachaNum
	{
		get
		{
			return __pbn__GachaNum.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaNum = value;
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

	public bool ShouldSerializeTotalNum()
	{
		return __pbn__TotalNum.HasValue;
	}

	public void ResetTotalNum()
	{
		__pbn__TotalNum = null;
	}

	public bool ShouldSerializeGachaNum()
	{
		return __pbn__GachaNum.HasValue;
	}

	public void ResetGachaNum()
	{
		__pbn__GachaNum = null;
	}
}
