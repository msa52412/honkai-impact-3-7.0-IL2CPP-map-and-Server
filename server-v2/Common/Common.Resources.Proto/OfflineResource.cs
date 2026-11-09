using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OfflineResource : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ResourceType;

	private uint? __pbn__HasTakeBoxNum;

	private uint? __pbn__AvailableBoxNum;

	[ProtoMember(1, Name = "resource_type")]
	public uint ResourceType
	{
		get
		{
			return __pbn__ResourceType.GetValueOrDefault();
		}
		set
		{
			__pbn__ResourceType = value;
		}
	}

	[ProtoMember(2, Name = "has_take_box_num")]
	public uint HasTakeBoxNum
	{
		get
		{
			return __pbn__HasTakeBoxNum.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeBoxNum = value;
		}
	}

	[ProtoMember(3, Name = "available_box_num")]
	public uint AvailableBoxNum
	{
		get
		{
			return __pbn__AvailableBoxNum.GetValueOrDefault();
		}
		set
		{
			__pbn__AvailableBoxNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeResourceType()
	{
		return __pbn__ResourceType.HasValue;
	}

	public void ResetResourceType()
	{
		__pbn__ResourceType = null;
	}

	public bool ShouldSerializeHasTakeBoxNum()
	{
		return __pbn__HasTakeBoxNum.HasValue;
	}

	public void ResetHasTakeBoxNum()
	{
		__pbn__HasTakeBoxNum = null;
	}

	public bool ShouldSerializeAvailableBoxNum()
	{
		return __pbn__AvailableBoxNum.HasValue;
	}

	public void ResetAvailableBoxNum()
	{
		__pbn__AvailableBoxNum = null;
	}
}
