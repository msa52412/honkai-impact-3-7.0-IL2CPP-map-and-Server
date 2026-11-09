using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WeaponInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__WeaponId;

	private uint? __pbn__SubWeaponId;

	[ProtoMember(1, Name = "weapon_id")]
	public uint WeaponId
	{
		get
		{
			return __pbn__WeaponId.GetValueOrDefault();
		}
		set
		{
			__pbn__WeaponId = value;
		}
	}

	[ProtoMember(2, Name = "sub_weapon_id")]
	public uint SubWeaponId
	{
		get
		{
			return __pbn__SubWeaponId.GetValueOrDefault();
		}
		set
		{
			__pbn__SubWeaponId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeWeaponId()
	{
		return __pbn__WeaponId.HasValue;
	}

	public void ResetWeaponId()
	{
		__pbn__WeaponId = null;
	}

	public bool ShouldSerializeSubWeaponId()
	{
		return __pbn__SubWeaponId.HasValue;
	}

	public void ResetSubWeaponId()
	{
		__pbn__SubWeaponId = null;
	}
}
