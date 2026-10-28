using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DormPowerBonus : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__HouseId;

	private uint? __pbn__Bonus;

	[ProtoMember(1, Name = "house_id")]
	public uint HouseId
	{
		get
		{
			return __pbn__HouseId.GetValueOrDefault();
		}
		set
		{
			__pbn__HouseId = value;
		}
	}

	[ProtoMember(2, Name = "bonus")]
	public uint Bonus
	{
		get
		{
			return __pbn__Bonus.GetValueOrDefault();
		}
		set
		{
			__pbn__Bonus = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHouseId()
	{
		return __pbn__HouseId.HasValue;
	}

	public void ResetHouseId()
	{
		__pbn__HouseId = null;
	}

	public bool ShouldSerializeBonus()
	{
		return __pbn__Bonus.HasValue;
	}

	public void ResetBonus()
	{
		__pbn__Bonus = null;
	}
}
