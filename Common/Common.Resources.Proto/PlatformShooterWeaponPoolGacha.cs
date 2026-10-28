using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlatformShooterWeaponPoolGachaReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6956
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__GachaRandom;

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

	[ProtoMember(2, Name = "gacha_random")]
	public uint GachaRandom
	{
		get
		{
			return __pbn__GachaRandom.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaRandom = value;
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

	public bool ShouldSerializeGachaRandom()
	{
		return __pbn__GachaRandom.HasValue;
	}

	public void ResetGachaRandom()
	{
		__pbn__GachaRandom = null;
	}
}
[ProtoContract]
public class PlatformShooterWeaponPoolGachaRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6957
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_POOL")]
		NoPool,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "ACTION_RANDOM_ERROR")]
		ActionRandomError,
		[ProtoEnum(Name = "WEAPON_ERROR")]
		WeaponError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__NewGachaRandom;

	private uint? __pbn__PoolId;

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

	[ProtoMember(3, Name = "new_gacha_random")]
	public uint NewGachaRandom
	{
		get
		{
			return __pbn__NewGachaRandom.GetValueOrDefault();
		}
		set
		{
			__pbn__NewGachaRandom = value;
		}
	}

	[ProtoMember(4, Name = "pool_list")]
	public List<PlatformShooterWeaponPool> PoolLists { get; } = new List<PlatformShooterWeaponPool>();


	[ProtoMember(5, Name = "pool_id")]
	public uint PoolId
	{
		get
		{
			return __pbn__PoolId.GetValueOrDefault();
		}
		set
		{
			__pbn__PoolId = value;
		}
	}

	[ProtoMember(6, Name = "weapon")]
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

	public bool ShouldSerializeNewGachaRandom()
	{
		return __pbn__NewGachaRandom.HasValue;
	}

	public void ResetNewGachaRandom()
	{
		__pbn__NewGachaRandom = null;
	}

	public bool ShouldSerializePoolId()
	{
		return __pbn__PoolId.HasValue;
	}

	public void ResetPoolId()
	{
		__pbn__PoolId = null;
	}
}
