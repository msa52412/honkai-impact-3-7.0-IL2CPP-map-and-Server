using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InLevelAvatarActorSnapshot : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__WeaponId;

	private uint? __pbn__WeaponLevel;

	private uint? __pbn__Stigmata1Id;

	private uint? __pbn__Stigmata1Level;

	private uint? __pbn__Stigmata2Id;

	private uint? __pbn__Stigmata2Level;

	private uint? __pbn__Stigmata3Id;

	private uint? __pbn__Stigmata3Level;

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

	[ProtoMember(2, Name = "skill_list")]
	public List<InLevelAvatarSkillInfo> SkillLists { get; } = new List<InLevelAvatarSkillInfo>();


	[ProtoMember(3, Name = "weapon_id")]
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

	[ProtoMember(4, Name = "weapon_level")]
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

	[ProtoMember(5, Name = "stigmata_1_id")]
	public uint Stigmata1Id
	{
		get
		{
			return __pbn__Stigmata1Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Stigmata1Id = value;
		}
	}

	[ProtoMember(6, Name = "stigmata_1_level")]
	public uint Stigmata1Level
	{
		get
		{
			return __pbn__Stigmata1Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Stigmata1Level = value;
		}
	}

	[ProtoMember(7, Name = "stigmata_2_id")]
	public uint Stigmata2Id
	{
		get
		{
			return __pbn__Stigmata2Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Stigmata2Id = value;
		}
	}

	[ProtoMember(8, Name = "stigmata_2_level")]
	public uint Stigmata2Level
	{
		get
		{
			return __pbn__Stigmata2Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Stigmata2Level = value;
		}
	}

	[ProtoMember(9, Name = "stigmata_3_id")]
	public uint Stigmata3Id
	{
		get
		{
			return __pbn__Stigmata3Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Stigmata3Id = value;
		}
	}

	[ProtoMember(10, Name = "stigmata_3_level")]
	public uint Stigmata3Level
	{
		get
		{
			return __pbn__Stigmata3Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Stigmata3Level = value;
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

	public bool ShouldSerializeStigmata1Id()
	{
		return __pbn__Stigmata1Id.HasValue;
	}

	public void ResetStigmata1Id()
	{
		__pbn__Stigmata1Id = null;
	}

	public bool ShouldSerializeStigmata1Level()
	{
		return __pbn__Stigmata1Level.HasValue;
	}

	public void ResetStigmata1Level()
	{
		__pbn__Stigmata1Level = null;
	}

	public bool ShouldSerializeStigmata2Id()
	{
		return __pbn__Stigmata2Id.HasValue;
	}

	public void ResetStigmata2Id()
	{
		__pbn__Stigmata2Id = null;
	}

	public bool ShouldSerializeStigmata2Level()
	{
		return __pbn__Stigmata2Level.HasValue;
	}

	public void ResetStigmata2Level()
	{
		__pbn__Stigmata2Level = null;
	}

	public bool ShouldSerializeStigmata3Id()
	{
		return __pbn__Stigmata3Id.HasValue;
	}

	public void ResetStigmata3Id()
	{
		__pbn__Stigmata3Id = null;
	}

	public bool ShouldSerializeStigmata3Level()
	{
		return __pbn__Stigmata3Level.HasValue;
	}

	public void ResetStigmata3Level()
	{
		__pbn__Stigmata3Level = null;
	}
}
