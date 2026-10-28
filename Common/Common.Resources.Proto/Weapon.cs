using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Weapon : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	private uint? __pbn__Id;

	private uint? __pbn__Level;

	private uint? __pbn__Exp;

	private bool? __pbn__IsProtected;

	private uint? __pbn__CancelProtectTime;

	private bool? __pbn__IsExtracted;

	private uint? __pbn__SubWeaponId;

	[ProtoMember(1, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(2, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
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

	[ProtoMember(4, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(5, Name = "is_protected")]
	public bool IsProtected
	{
		get
		{
			return __pbn__IsProtected.GetValueOrDefault();
		}
		set
		{
			__pbn__IsProtected = value;
		}
	}

	[ProtoMember(6, Name = "cancel_protect_time")]
	public uint CancelProtectTime
	{
		get
		{
			return __pbn__CancelProtectTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CancelProtectTime = value;
		}
	}

	[ProtoMember(7, Name = "is_extracted")]
	public bool IsExtracted
	{
		get
		{
			return __pbn__IsExtracted.GetValueOrDefault();
		}
		set
		{
			__pbn__IsExtracted = value;
		}
	}

	[ProtoMember(8, Name = "sub_weapon_id")]
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

	[ProtoMember(9, Name = "quantum_branch_list")]
	public uint[] QuantumBranchLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeIsProtected()
	{
		return __pbn__IsProtected.HasValue;
	}

	public void ResetIsProtected()
	{
		__pbn__IsProtected = null;
	}

	public bool ShouldSerializeCancelProtectTime()
	{
		return __pbn__CancelProtectTime.HasValue;
	}

	public void ResetCancelProtectTime()
	{
		__pbn__CancelProtectTime = null;
	}

	public bool ShouldSerializeIsExtracted()
	{
		return __pbn__IsExtracted.HasValue;
	}

	public void ResetIsExtracted()
	{
		__pbn__IsExtracted = null;
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
