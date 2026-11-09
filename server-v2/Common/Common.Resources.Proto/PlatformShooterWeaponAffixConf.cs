using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlatformShooterWeaponAffixConfirmInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AffixIndex;

	private PlatformShooterWeaponAffixConfirmType? __pbn__Type;

	[ProtoMember(1, Name = "affix_index")]
	public uint AffixIndex
	{
		get
		{
			return __pbn__AffixIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__AffixIndex = value;
		}
	}

	[ProtoMember(2, Name = "type")]
	[DefaultValue(PlatformShooterWeaponAffixConfirmType.PlatformShooterWeaponAffixConfirmNone)]
	public PlatformShooterWeaponAffixConfirmType Type
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAffixIndex()
	{
		return __pbn__AffixIndex.HasValue;
	}

	public void ResetAffixIndex()
	{
		__pbn__AffixIndex = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}
}
[ProtoContract]
public class PlatformShooterWeaponAffixConfirmReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6960
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__WeaponId;

	private uint? __pbn__AffixIndex;

	private PlatformShooterWeaponAffixConfirmType? __pbn__Type;

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

	[ProtoMember(3, Name = "affix_index")]
	public uint AffixIndex
	{
		get
		{
			return __pbn__AffixIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__AffixIndex = value;
		}
	}

	[ProtoMember(4, Name = "type")]
	[DefaultValue(PlatformShooterWeaponAffixConfirmType.PlatformShooterWeaponAffixConfirmNone)]
	public PlatformShooterWeaponAffixConfirmType Type
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

	[ProtoMember(5, Name = "confirm_info_list")]
	public List<PlatformShooterWeaponAffixConfirmInfo> ConfirmInfoLists { get; } = new List<PlatformShooterWeaponAffixConfirmInfo>();


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

	public bool ShouldSerializeAffixIndex()
	{
		return __pbn__AffixIndex.HasValue;
	}

	public void ResetAffixIndex()
	{
		__pbn__AffixIndex = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}
}
[ProtoContract]
public class PlatformShooterWeaponAffixConfirmRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6961
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
		[ProtoEnum(Name = "NO_NEED_CONFIRM")]
		NoNeedConfirm,
		[ProtoEnum(Name = "DUPLICATE_CONFIRM_INFO")]
		DuplicateConfirmInfo
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
public enum PlatformShooterWeaponAffixConfirmType
{
	[ProtoEnum(Name = "PLATFORM_SHOOTER_WEAPON_AFFIX_CONFIRM_NONE")]
	PlatformShooterWeaponAffixConfirmNone,
	[ProtoEnum(Name = "PLATFORM_SHOOTER_WEAPON_AFFIX_CONFIRM_CONFIRM")]
	PlatformShooterWeaponAffixConfirmConfirm,
	[ProtoEnum(Name = "PLATFORM_SHOOTER_WEAPON_AFFIX_CONFIRM_ABANDON")]
	PlatformShooterWeaponAffixConfirmAbandon
}
