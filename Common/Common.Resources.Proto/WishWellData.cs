using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WishWellData : IExtensible
{
	[ProtoContract]
	public class Equip : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__EquipId;

		private uint? __pbn__EquipLevel;

		[ProtoMember(1, Name = "equip_id")]
		public uint EquipId
		{
			get
			{
				return __pbn__EquipId.GetValueOrDefault();
			}
			set
			{
				__pbn__EquipId = value;
			}
		}

		[ProtoMember(2, Name = "equip_level")]
		public uint EquipLevel
		{
			get
			{
				return __pbn__EquipLevel.GetValueOrDefault();
			}
			set
			{
				__pbn__EquipLevel = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeEquipId()
		{
			return __pbn__EquipId.HasValue;
		}

		public void ResetEquipId()
		{
			__pbn__EquipId = null;
		}

		public bool ShouldSerializeEquipLevel()
		{
			return __pbn__EquipLevel.HasValue;
		}

		public void ResetEquipLevel()
		{
			__pbn__EquipLevel = null;
		}
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LeftTimes;

	private uint? __pbn__SetId;

	private uint? __pbn__MaterialId;

	private uint? __pbn__MaterialNum;

	private uint? __pbn__EquipNeedNum;

	private uint? __pbn__MaxWishTimes;

	[ProtoMember(1, Name = "left_times")]
	public uint LeftTimes
	{
		get
		{
			return __pbn__LeftTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftTimes = value;
		}
	}

	[ProtoMember(2, Name = "set_id")]
	public uint SetId
	{
		get
		{
			return __pbn__SetId.GetValueOrDefault();
		}
		set
		{
			__pbn__SetId = value;
		}
	}

	[ProtoMember(3, Name = "material_id")]
	public uint MaterialId
	{
		get
		{
			return __pbn__MaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__MaterialId = value;
		}
	}

	[ProtoMember(4, Name = "material_num")]
	public uint MaterialNum
	{
		get
		{
			return __pbn__MaterialNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MaterialNum = value;
		}
	}

	[ProtoMember(5, Name = "equip_need_num")]
	public uint EquipNeedNum
	{
		get
		{
			return __pbn__EquipNeedNum.GetValueOrDefault();
		}
		set
		{
			__pbn__EquipNeedNum = value;
		}
	}

	[ProtoMember(6, Name = "equip_list")]
	public List<Equip> EquipLists { get; } = new List<Equip>();


	[ProtoMember(7, Name = "max_wish_times")]
	public uint MaxWishTimes
	{
		get
		{
			return __pbn__MaxWishTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxWishTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLeftTimes()
	{
		return __pbn__LeftTimes.HasValue;
	}

	public void ResetLeftTimes()
	{
		__pbn__LeftTimes = null;
	}

	public bool ShouldSerializeSetId()
	{
		return __pbn__SetId.HasValue;
	}

	public void ResetSetId()
	{
		__pbn__SetId = null;
	}

	public bool ShouldSerializeMaterialId()
	{
		return __pbn__MaterialId.HasValue;
	}

	public void ResetMaterialId()
	{
		__pbn__MaterialId = null;
	}

	public bool ShouldSerializeMaterialNum()
	{
		return __pbn__MaterialNum.HasValue;
	}

	public void ResetMaterialNum()
	{
		__pbn__MaterialNum = null;
	}

	public bool ShouldSerializeEquipNeedNum()
	{
		return __pbn__EquipNeedNum.HasValue;
	}

	public void ResetEquipNeedNum()
	{
		__pbn__EquipNeedNum = null;
	}

	public bool ShouldSerializeMaxWishTimes()
	{
		return __pbn__MaxWishTimes.HasValue;
	}

	public void ResetMaxWishTimes()
	{
		__pbn__MaxWishTimes = null;
	}
}
