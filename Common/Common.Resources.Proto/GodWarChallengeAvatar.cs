using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarChallengeAvatar : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__HpPercent;

	private bool? __pbn__IsVirtual;

	private uint? __pbn__WeaponId;

	private uint? __pbn__WeaponLevel;

	[ProtoMember(1, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(2, Name = "hp_percent")]
	public uint HpPercent
	{
		get
		{
			return __pbn__HpPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__HpPercent = value;
		}
	}

	[ProtoMember(3, Name = "is_virtual")]
	public bool IsVirtual
	{
		get
		{
			return __pbn__IsVirtual.GetValueOrDefault();
		}
		set
		{
			__pbn__IsVirtual = value;
		}
	}

	[ProtoMember(4, Name = "weapon_id")]
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

	[ProtoMember(5, Name = "weapon_level")]
	public uint WeaponLevel
	{
		get
		{
			return __pbn__WeaponLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__WeaponLevel = value;
		}
	}

	[ProtoMember(6, Name = "stigmata_1")]
	public GodWarChallengeStigmata Stigmata1 { get; set; }

	[ProtoMember(7, Name = "stigmata_2")]
	public GodWarChallengeStigmata Stigmata2 { get; set; }

	[ProtoMember(8, Name = "stigmata_3")]
	public GodWarChallengeStigmata Stigmata3 { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeHpPercent()
	{
		return __pbn__HpPercent.HasValue;
	}

	public void ResetHpPercent()
	{
		__pbn__HpPercent = null;
	}

	public bool ShouldSerializeIsVirtual()
	{
		return __pbn__IsVirtual.HasValue;
	}

	public void ResetIsVirtual()
	{
		__pbn__IsVirtual = null;
	}

	public bool ShouldSerializeWeaponId()
	{
		return __pbn__WeaponId.HasValue;
	}

	public void ResetWeaponId()
	{
		__pbn__WeaponId = null;
	}

	public bool ShouldSerializeWeaponLevel()
	{
		return __pbn__WeaponLevel.HasValue;
	}

	public void ResetWeaponLevel()
	{
		__pbn__WeaponLevel = null;
	}
}
