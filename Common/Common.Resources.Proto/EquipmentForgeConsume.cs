using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EquipmentForgeConsume : IExtensible
{
	[ProtoContract]
	public class ConsumeEquip : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__Id;

		private uint? __pbn__Level;

		private uint? __pbn__Exp;

		[ProtoMember(1, Name = "id")]
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

		[ProtoMember(2, Name = "level")]
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

		[ProtoMember(3, Name = "exp")]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
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
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

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

	[ProtoMember(2, Name = "equip_list")]
	public List<ConsumeEquip> EquipLists { get; } = new List<ConsumeEquip>();


	[ProtoMember(3, Name = "material_list")]
	public List<Material> MaterialLists { get; } = new List<Material>();


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
}
