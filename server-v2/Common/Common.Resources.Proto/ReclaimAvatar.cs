using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReclaimAvatar : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ReclaimAvatarId;

	private uint? __pbn__ReclaimWeaponId;

	private uint? __pbn__ReclaimStigmataId1;

	private uint? __pbn__ReclaimStigmataId2;

	private uint? __pbn__ReclaimStigmataId3;

	[ProtoMember(1, Name = "reclaim_avatar_id")]
	public uint ReclaimAvatarId
	{
		get
		{
			return __pbn__ReclaimAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__ReclaimAvatarId = value;
		}
	}

	[ProtoMember(2, Name = "reclaim_weapon_id")]
	public uint ReclaimWeaponId
	{
		get
		{
			return __pbn__ReclaimWeaponId.GetValueOrDefault();
		}
		set
		{
			__pbn__ReclaimWeaponId = value;
		}
	}

	[ProtoMember(3, Name = "reclaim_stigmata_id_1")]
	public uint ReclaimStigmataId1
	{
		get
		{
			return __pbn__ReclaimStigmataId1.GetValueOrDefault();
		}
		set
		{
			__pbn__ReclaimStigmataId1 = value;
		}
	}

	[ProtoMember(4, Name = "reclaim_stigmata_id_2")]
	public uint ReclaimStigmataId2
	{
		get
		{
			return __pbn__ReclaimStigmataId2.GetValueOrDefault();
		}
		set
		{
			__pbn__ReclaimStigmataId2 = value;
		}
	}

	[ProtoMember(5, Name = "reclaim_stigmata_id_3")]
	public uint ReclaimStigmataId3
	{
		get
		{
			return __pbn__ReclaimStigmataId3.GetValueOrDefault();
		}
		set
		{
			__pbn__ReclaimStigmataId3 = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReclaimAvatarId()
	{
		return __pbn__ReclaimAvatarId.HasValue;
	}

	public void ResetReclaimAvatarId()
	{
		__pbn__ReclaimAvatarId = null;
	}

	public bool ShouldSerializeReclaimWeaponId()
	{
		return __pbn__ReclaimWeaponId.HasValue;
	}

	public void ResetReclaimWeaponId()
	{
		__pbn__ReclaimWeaponId = null;
	}

	public bool ShouldSerializeReclaimStigmataId1()
	{
		return __pbn__ReclaimStigmataId1.HasValue;
	}

	public void ResetReclaimStigmataId1()
	{
		__pbn__ReclaimStigmataId1 = null;
	}

	public bool ShouldSerializeReclaimStigmataId2()
	{
		return __pbn__ReclaimStigmataId2.HasValue;
	}

	public void ResetReclaimStigmataId2()
	{
		__pbn__ReclaimStigmataId2 = null;
	}

	public bool ShouldSerializeReclaimStigmataId3()
	{
		return __pbn__ReclaimStigmataId3.HasValue;
	}

	public void ResetReclaimStigmataId3()
	{
		__pbn__ReclaimStigmataId3 = null;
	}
}
