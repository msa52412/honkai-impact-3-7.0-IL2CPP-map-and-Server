using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExtractEquipmentActivity : IExtensible
{
	[ProtoContract]
	public class OpenExtractWeapon : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__Quality;

		private uint? __pbn__BaseType;

		[ProtoMember(1, Name = "quality")]
		public uint Quality
		{
			get
			{
				return __pbn__Quality.GetValueOrDefault();
			}
			set
			{
				__pbn__Quality = value;
			}
		}

		[ProtoMember(2, Name = "base_type")]
		public uint BaseType
		{
			get
			{
				return __pbn__BaseType.GetValueOrDefault();
			}
			set
			{
				__pbn__BaseType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeQuality()
		{
			return __pbn__Quality.HasValue;
		}

		public void ResetQuality()
		{
			__pbn__Quality = null;
		}

		public bool ShouldSerializeBaseType()
		{
			return __pbn__BaseType.HasValue;
		}

		public void ResetBaseType()
		{
			__pbn__BaseType = null;
		}
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsReforge;

	private uint? __pbn__ReforgeTimes;

	private uint? __pbn__PowerupMaterialRatio;

	private uint? __pbn__ReforgeInitLevel;

	private uint? __pbn__RemainReforgeTimes;

	[ProtoMember(4, Name = "extract_weapon_list")]
	public List<OpenExtractWeapon> ExtractWeaponLists { get; } = new List<OpenExtractWeapon>();


	[ProtoMember(5, Name = "mainid_black_list")]
	public uint[] MainidBlackLists { get; set; }

	[ProtoMember(6, Name = "is_reforge")]
	public bool IsReforge
	{
		get
		{
			return __pbn__IsReforge.GetValueOrDefault();
		}
		set
		{
			__pbn__IsReforge = value;
		}
	}

	[ProtoMember(7, Name = "reforge_main_id_list")]
	public uint[] ReforgeMainIdLists { get; set; }

	[ProtoMember(8, Name = "reforge_times")]
	public uint ReforgeTimes
	{
		get
		{
			return __pbn__ReforgeTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ReforgeTimes = value;
		}
	}

	[ProtoMember(10, Name = "powerup_material_ratio")]
	public uint PowerupMaterialRatio
	{
		get
		{
			return __pbn__PowerupMaterialRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__PowerupMaterialRatio = value;
		}
	}

	[ProtoMember(11, Name = "reforge_init_level")]
	public uint ReforgeInitLevel
	{
		get
		{
			return __pbn__ReforgeInitLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ReforgeInitLevel = value;
		}
	}

	[ProtoMember(12, Name = "remain_reforge_times")]
	public uint RemainReforgeTimes
	{
		get
		{
			return __pbn__RemainReforgeTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__RemainReforgeTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsReforge()
	{
		return __pbn__IsReforge.HasValue;
	}

	public void ResetIsReforge()
	{
		__pbn__IsReforge = null;
	}

	public bool ShouldSerializeReforgeTimes()
	{
		return __pbn__ReforgeTimes.HasValue;
	}

	public void ResetReforgeTimes()
	{
		__pbn__ReforgeTimes = null;
	}

	public bool ShouldSerializePowerupMaterialRatio()
	{
		return __pbn__PowerupMaterialRatio.HasValue;
	}

	public void ResetPowerupMaterialRatio()
	{
		__pbn__PowerupMaterialRatio = null;
	}

	public bool ShouldSerializeReforgeInitLevel()
	{
		return __pbn__ReforgeInitLevel.HasValue;
	}

	public void ResetReforgeInitLevel()
	{
		__pbn__ReforgeInitLevel = null;
	}

	public bool ShouldSerializeRemainReforgeTimes()
	{
		return __pbn__RemainReforgeTimes.HasValue;
	}

	public void ResetRemainReforgeTimes()
	{
		__pbn__RemainReforgeTimes = null;
	}
}
