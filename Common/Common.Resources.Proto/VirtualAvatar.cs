using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VirtualAvatar : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__VirtualAvatarId;

	private uint? __pbn__VirtualWeaponId;

	private uint? __pbn__VirtualStigmataId1;

	private uint? __pbn__VirtualStigmataId2;

	private uint? __pbn__VirtualStigmataId3;

	private uint? __pbn__VirtualBuffId;

	[ProtoMember(1, Name = "virtual_avatar_id")]
	public uint VirtualAvatarId
	{
		get
		{
			return __pbn__VirtualAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__VirtualAvatarId = value;
		}
	}

	[ProtoMember(2, Name = "virtual_weapon_id")]
	public uint VirtualWeaponId
	{
		get
		{
			return __pbn__VirtualWeaponId.GetValueOrDefault();
		}
		set
		{
			__pbn__VirtualWeaponId = value;
		}
	}

	[ProtoMember(3, Name = "virtual_stigmata_id_1")]
	public uint VirtualStigmataId1
	{
		get
		{
			return __pbn__VirtualStigmataId1.GetValueOrDefault();
		}
		set
		{
			__pbn__VirtualStigmataId1 = value;
		}
	}

	[ProtoMember(4, Name = "virtual_stigmata_id_2")]
	public uint VirtualStigmataId2
	{
		get
		{
			return __pbn__VirtualStigmataId2.GetValueOrDefault();
		}
		set
		{
			__pbn__VirtualStigmataId2 = value;
		}
	}

	[ProtoMember(5, Name = "virtual_stigmata_id_3")]
	public uint VirtualStigmataId3
	{
		get
		{
			return __pbn__VirtualStigmataId3.GetValueOrDefault();
		}
		set
		{
			__pbn__VirtualStigmataId3 = value;
		}
	}

	[ProtoMember(6, Name = "virtual_buff_id")]
	public uint VirtualBuffId
	{
		get
		{
			return __pbn__VirtualBuffId.GetValueOrDefault();
		}
		set
		{
			__pbn__VirtualBuffId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeVirtualAvatarId()
	{
		return __pbn__VirtualAvatarId.HasValue;
	}

	public void ResetVirtualAvatarId()
	{
		__pbn__VirtualAvatarId = null;
	}

	public bool ShouldSerializeVirtualWeaponId()
	{
		return __pbn__VirtualWeaponId.HasValue;
	}

	public void ResetVirtualWeaponId()
	{
		__pbn__VirtualWeaponId = null;
	}

	public bool ShouldSerializeVirtualStigmataId1()
	{
		return __pbn__VirtualStigmataId1.HasValue;
	}

	public void ResetVirtualStigmataId1()
	{
		__pbn__VirtualStigmataId1 = null;
	}

	public bool ShouldSerializeVirtualStigmataId2()
	{
		return __pbn__VirtualStigmataId2.HasValue;
	}

	public void ResetVirtualStigmataId2()
	{
		__pbn__VirtualStigmataId2 = null;
	}

	public bool ShouldSerializeVirtualStigmataId3()
	{
		return __pbn__VirtualStigmataId3.HasValue;
	}

	public void ResetVirtualStigmataId3()
	{
		__pbn__VirtualStigmataId3 = null;
	}

	public bool ShouldSerializeVirtualBuffId()
	{
		return __pbn__VirtualBuffId.HasValue;
	}

	public void ResetVirtualBuffId()
	{
		__pbn__VirtualBuffId = null;
	}
}
