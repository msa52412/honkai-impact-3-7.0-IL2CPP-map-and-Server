using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DormFacilityData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__LevelUpFacilityType;

	private uint? __pbn__LevelUpEndTime;

	private uint? __pbn__StoreStamina;

	private bool? __pbn__IsCanStoreStamina;

	private uint? __pbn__Power;

	private uint? __pbn__PowerRecoverLeftTime;

	private uint? __pbn__PowerRecoverConfigTime;

	private uint? __pbn__PowerLimit;

	private uint? __pbn__BasePowerLimit;

	private uint? __pbn__PowerRecovery;

	private uint? __pbn__BasePowerRecovery;

	private uint? __pbn__BonusPowerRecovery;

	[ProtoMember(1, Name = "facility_list")]
	public List<DormFacility> FacilityLists { get; } = new List<DormFacility>();


	[ProtoMember(2, Name = "level_up_facility_type")]
	public uint LevelUpFacilityType
	{
		get
		{
			return __pbn__LevelUpFacilityType.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelUpFacilityType = value;
		}
	}

	[ProtoMember(3, Name = "level_up_end_time")]
	public uint LevelUpEndTime
	{
		get
		{
			return __pbn__LevelUpEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelUpEndTime = value;
		}
	}

	[ProtoMember(4, Name = "store_stamina")]
	public uint StoreStamina
	{
		get
		{
			return __pbn__StoreStamina.GetValueOrDefault();
		}
		set
		{
			__pbn__StoreStamina = value;
		}
	}

	[ProtoMember(5, Name = "is_can_store_stamina")]
	public bool IsCanStoreStamina
	{
		get
		{
			return __pbn__IsCanStoreStamina.GetValueOrDefault();
		}
		set
		{
			__pbn__IsCanStoreStamina = value;
		}
	}

	[ProtoMember(10, Name = "power")]
	public uint Power
	{
		get
		{
			return __pbn__Power.GetValueOrDefault();
		}
		set
		{
			__pbn__Power = value;
		}
	}

	[ProtoMember(11, Name = "power_recover_left_time")]
	public uint PowerRecoverLeftTime
	{
		get
		{
			return __pbn__PowerRecoverLeftTime.GetValueOrDefault();
		}
		set
		{
			__pbn__PowerRecoverLeftTime = value;
		}
	}

	[ProtoMember(12, Name = "power_recover_config_time")]
	public uint PowerRecoverConfigTime
	{
		get
		{
			return __pbn__PowerRecoverConfigTime.GetValueOrDefault();
		}
		set
		{
			__pbn__PowerRecoverConfigTime = value;
		}
	}

	[ProtoMember(13, Name = "power_limit")]
	public uint PowerLimit
	{
		get
		{
			return __pbn__PowerLimit.GetValueOrDefault();
		}
		set
		{
			__pbn__PowerLimit = value;
		}
	}

	[ProtoMember(14, Name = "base_power_limit")]
	public uint BasePowerLimit
	{
		get
		{
			return __pbn__BasePowerLimit.GetValueOrDefault();
		}
		set
		{
			__pbn__BasePowerLimit = value;
		}
	}

	[ProtoMember(15, Name = "limit_bonus_list")]
	public List<DormPowerBonus> LimitBonusLists { get; } = new List<DormPowerBonus>();


	[ProtoMember(16, Name = "power_recovery")]
	public uint PowerRecovery
	{
		get
		{
			return __pbn__PowerRecovery.GetValueOrDefault();
		}
		set
		{
			__pbn__PowerRecovery = value;
		}
	}

	[ProtoMember(17, Name = "base_power_recovery")]
	public uint BasePowerRecovery
	{
		get
		{
			return __pbn__BasePowerRecovery.GetValueOrDefault();
		}
		set
		{
			__pbn__BasePowerRecovery = value;
		}
	}

	[ProtoMember(18, Name = "bonus_power_recovery")]
	public uint BonusPowerRecovery
	{
		get
		{
			return __pbn__BonusPowerRecovery.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusPowerRecovery = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLevelUpFacilityType()
	{
		return __pbn__LevelUpFacilityType.HasValue;
	}

	public void ResetLevelUpFacilityType()
	{
		__pbn__LevelUpFacilityType = null;
	}

	public bool ShouldSerializeLevelUpEndTime()
	{
		return __pbn__LevelUpEndTime.HasValue;
	}

	public void ResetLevelUpEndTime()
	{
		__pbn__LevelUpEndTime = null;
	}

	public bool ShouldSerializeStoreStamina()
	{
		return __pbn__StoreStamina.HasValue;
	}

	public void ResetStoreStamina()
	{
		__pbn__StoreStamina = null;
	}

	public bool ShouldSerializeIsCanStoreStamina()
	{
		return __pbn__IsCanStoreStamina.HasValue;
	}

	public void ResetIsCanStoreStamina()
	{
		__pbn__IsCanStoreStamina = null;
	}

	public bool ShouldSerializePower()
	{
		return __pbn__Power.HasValue;
	}

	public void ResetPower()
	{
		__pbn__Power = null;
	}

	public bool ShouldSerializePowerRecoverLeftTime()
	{
		return __pbn__PowerRecoverLeftTime.HasValue;
	}

	public void ResetPowerRecoverLeftTime()
	{
		__pbn__PowerRecoverLeftTime = null;
	}

	public bool ShouldSerializePowerRecoverConfigTime()
	{
		return __pbn__PowerRecoverConfigTime.HasValue;
	}

	public void ResetPowerRecoverConfigTime()
	{
		__pbn__PowerRecoverConfigTime = null;
	}

	public bool ShouldSerializePowerLimit()
	{
		return __pbn__PowerLimit.HasValue;
	}

	public void ResetPowerLimit()
	{
		__pbn__PowerLimit = null;
	}

	public bool ShouldSerializeBasePowerLimit()
	{
		return __pbn__BasePowerLimit.HasValue;
	}

	public void ResetBasePowerLimit()
	{
		__pbn__BasePowerLimit = null;
	}

	public bool ShouldSerializePowerRecovery()
	{
		return __pbn__PowerRecovery.HasValue;
	}

	public void ResetPowerRecovery()
	{
		__pbn__PowerRecovery = null;
	}

	public bool ShouldSerializeBasePowerRecovery()
	{
		return __pbn__BasePowerRecovery.HasValue;
	}

	public void ResetBasePowerRecovery()
	{
		__pbn__BasePowerRecovery = null;
	}

	public bool ShouldSerializeBonusPowerRecovery()
	{
		return __pbn__BonusPowerRecovery.HasValue;
	}

	public void ResetBonusPowerRecovery()
	{
		__pbn__BonusPowerRecovery = null;
	}
}
