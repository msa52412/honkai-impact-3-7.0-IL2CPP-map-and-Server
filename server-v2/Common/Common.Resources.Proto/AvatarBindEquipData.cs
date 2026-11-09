using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarBindEquipData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__WeaponUid;

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

	[ProtoMember(2, Name = "weapon_uid")]
	public uint WeaponUid
	{
		get
		{
			return __pbn__WeaponUid.GetValueOrDefault();
		}
		set
		{
			__pbn__WeaponUid = value;
		}
	}

	[ProtoMember(3, Name = "stigmata_uid_list")]
	public uint[] StigmataUidLists { get; set; }

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

	public bool ShouldSerializeWeaponUid()
	{
		return __pbn__WeaponUid.HasValue;
	}

	public void ResetWeaponUid()
	{
		__pbn__WeaponUid = null;
	}
}
