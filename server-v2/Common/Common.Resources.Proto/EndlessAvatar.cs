using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EndlessAvatar : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__Star;

	private uint? __pbn__Level;

	private uint? __pbn__WeaponId;

	private uint? __pbn__StigmataId1;

	private uint? __pbn__StigmataId2;

	private uint? __pbn__StigmataId3;

	private uint? __pbn__QuantumWeaponId;

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

	[ProtoMember(2, Name = "star")]
	public uint Star
	{
		get
		{
			return __pbn__Star.GetValueOrDefault();
		}
		set
		{
			__pbn__Star = value;
		}
	}

	[ProtoMember(3, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
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

	[ProtoMember(5, Name = "stigmata_id_1")]
	public uint StigmataId1
	{
		get
		{
			return __pbn__StigmataId1.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataId1 = value;
		}
	}

	[ProtoMember(6, Name = "stigmata_id_2")]
	public uint StigmataId2
	{
		get
		{
			return __pbn__StigmataId2.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataId2 = value;
		}
	}

	[ProtoMember(7, Name = "stigmata_id_3")]
	public uint StigmataId3
	{
		get
		{
			return __pbn__StigmataId3.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataId3 = value;
		}
	}

	[ProtoMember(8, Name = "quantum_weapon_id")]
	public uint QuantumWeaponId
	{
		get
		{
			return __pbn__QuantumWeaponId.GetValueOrDefault();
		}
		set
		{
			__pbn__QuantumWeaponId = value;
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

	public bool ShouldSerializeStar()
	{
		return __pbn__Star.HasValue;
	}

	public void ResetStar()
	{
		__pbn__Star = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeWeaponId()
	{
		return __pbn__WeaponId.HasValue;
	}

	public void ResetWeaponId()
	{
		__pbn__WeaponId = null;
	}

	public bool ShouldSerializeStigmataId1()
	{
		return __pbn__StigmataId1.HasValue;
	}

	public void ResetStigmataId1()
	{
		__pbn__StigmataId1 = null;
	}

	public bool ShouldSerializeStigmataId2()
	{
		return __pbn__StigmataId2.HasValue;
	}

	public void ResetStigmataId2()
	{
		__pbn__StigmataId2 = null;
	}

	public bool ShouldSerializeStigmataId3()
	{
		return __pbn__StigmataId3.HasValue;
	}

	public void ResetStigmataId3()
	{
		__pbn__StigmataId3 = null;
	}

	public bool ShouldSerializeQuantumWeaponId()
	{
		return __pbn__QuantumWeaponId.HasValue;
	}

	public void ResetQuantumWeaponId()
	{
		__pbn__QuantumWeaponId = null;
	}
}
