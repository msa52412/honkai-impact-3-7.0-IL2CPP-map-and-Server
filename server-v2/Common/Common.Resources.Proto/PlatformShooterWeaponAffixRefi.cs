using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlatformShooterWeaponAffixRefineReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6958
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__WeaponId;

	private PlatformShooterWeaponAffixRefineType? __pbn__Type;

	private bool? __pbn__IsRetry;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "weapon_id")]
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

	[ProtoMember(3, Name = "affix_index_list")]
	public uint[] AffixIndexLists { get; set; }

	[ProtoMember(4, Name = "type")]
	[DefaultValue(PlatformShooterWeaponAffixRefineType.PlatformShooterWeaponAffixRefineNone)]
	public PlatformShooterWeaponAffixRefineType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(5, Name = "is_retry")]
	public bool IsRetry
	{
		get
		{
			return __pbn__IsRetry.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRetry = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeWeaponId()
	{
		return __pbn__WeaponId.HasValue;
	}

	public void ResetWeaponId()
	{
		__pbn__WeaponId = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeIsRetry()
	{
		return __pbn__IsRetry.HasValue;
	}

	public void ResetIsRetry()
	{
		__pbn__IsRetry = null;
	}
}
[ProtoContract]
public class PlatformShooterWeaponAffixRefineRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6959
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "WEAPON_ERROR")]
		WeaponError,
		[ProtoEnum(Name = "AFFIX_ERROR")]
		AffixError,
		[ProtoEnum(Name = "NEED_CONFIRM")]
		NeedConfirm,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "REFINE_TYPE_ERROR")]
		RefineTypeError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "weapon")]
	public PlatformShooterWeapon Weapon { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}
}
[ProtoContract]
public enum PlatformShooterWeaponAffixRefineType
{
	[ProtoEnum(Name = "PLATFORM_SHOOTER_WEAPON_AFFIX_REFINE_NONE")]
	PlatformShooterWeaponAffixRefineNone,
	[ProtoEnum(Name = "PLATFORM_SHOOTER_WEAPON_AFFIX_REFINE_JUNIOR")]
	PlatformShooterWeaponAffixRefineJunior,
	[ProtoEnum(Name = "PLATFORM_SHOOTER_WEAPON_AFFIX_REFINE_SENIOR")]
	PlatformShooterWeaponAffixRefineSenior,
	[ProtoEnum(Name = "PLATFORM_SHOOTER_WEAPON_AFFIX_REFINE_TOP")]
	PlatformShooterWeaponAffixRefineTop
}
