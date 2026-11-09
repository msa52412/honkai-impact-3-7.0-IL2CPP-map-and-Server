using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarBriefInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__AvatarLevel;

	private uint? __pbn__AvatarStar;

	private uint? __pbn__WeaponId;

	private uint? __pbn__SubWeaponId;

	private uint? __pbn__DressId;

	private bool? __pbn__IsArtifactSwitchOn;

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

	[ProtoMember(2, Name = "avatar_level")]
	public uint AvatarLevel
	{
		get
		{
			return __pbn__AvatarLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarLevel = value;
		}
	}

	[ProtoMember(3, Name = "avatar_star")]
	public uint AvatarStar
	{
		get
		{
			return __pbn__AvatarStar.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarStar = value;
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

	[ProtoMember(5, Name = "stigmata_id_list")]
	public uint[] StigmataIdLists { get; set; }

	[ProtoMember(6, Name = "sub_weapon_id")]
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

	[ProtoMember(7, Name = "dress_id")]
	public uint DressId
	{
		get
		{
			return __pbn__DressId.GetValueOrDefault();
		}
		set
		{
			__pbn__DressId = value;
		}
	}

	[ProtoMember(8, Name = "is_artifact_switch_on")]
	public bool IsArtifactSwitchOn
	{
		get
		{
			return __pbn__IsArtifactSwitchOn.GetValueOrDefault();
		}
		set
		{
			__pbn__IsArtifactSwitchOn = value;
		}
	}

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

	public bool ShouldSerializeAvatarLevel()
	{
		return __pbn__AvatarLevel.HasValue;
	}

	public void ResetAvatarLevel()
	{
		__pbn__AvatarLevel = null;
	}

	public bool ShouldSerializeAvatarStar()
	{
		return __pbn__AvatarStar.HasValue;
	}

	public void ResetAvatarStar()
	{
		__pbn__AvatarStar = null;
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

	public bool ShouldSerializeDressId()
	{
		return __pbn__DressId.HasValue;
	}

	public void ResetDressId()
	{
		__pbn__DressId = null;
	}

	public bool ShouldSerializeIsArtifactSwitchOn()
	{
		return __pbn__IsArtifactSwitchOn.HasValue;
	}

	public void ResetIsArtifactSwitchOn()
	{
		__pbn__IsArtifactSwitchOn = null;
	}
}
