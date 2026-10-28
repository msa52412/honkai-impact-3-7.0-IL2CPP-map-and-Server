using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MonopolyFloor : IExtensible
{
	[ProtoContract]
	public class Building : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__Location;

		private uint? __pbn__Type;

		private uint? __pbn__Level;

		private uint? __pbn__ProductNum;

		[ProtoMember(1, Name = "location")]
		public uint Location
		{
			get
			{
				return __pbn__Location.GetValueOrDefault();
			}
			set
			{
				__pbn__Location = value;
			}
		}

		[ProtoMember(2, Name = "type")]
		public uint Type
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

		[ProtoMember(4, Name = "product_num")]
		public uint ProductNum
		{
			get
			{
				return __pbn__ProductNum.GetValueOrDefault();
			}
			set
			{
				__pbn__ProductNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeLocation()
		{
			return __pbn__Location.HasValue;
		}

		public void ResetLocation()
		{
			__pbn__Location = null;
		}

		public bool ShouldSerializeType()
		{
			return __pbn__Type.HasValue;
		}

		public void ResetType()
		{
			__pbn__Type = null;
		}

		public bool ShouldSerializeLevel()
		{
			return __pbn__Level.HasValue;
		}

		public void ResetLevel()
		{
			__pbn__Level = null;
		}

		public bool ShouldSerializeProductNum()
		{
			return __pbn__ProductNum.HasValue;
		}

		public void ResetProductNum()
		{
			__pbn__ProductNum = null;
		}
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__FloorId;

	private MonopolyRoundStatus? __pbn__RoundStatus;

	[ProtoMember(1, Name = "floor_id")]
	public uint FloorId
	{
		get
		{
			return __pbn__FloorId.GetValueOrDefault();
		}
		set
		{
			__pbn__FloorId = value;
		}
	}

	[ProtoMember(2, Name = "building_list")]
	public List<Building> BuildingLists { get; } = new List<Building>();


	[ProtoMember(3, Name = "monster_list")]
	public List<MonopolyMonster> MonsterLists { get; } = new List<MonopolyMonster>();


	[ProtoMember(4, Name = "round_status")]
	[DefaultValue(MonopolyRoundStatus.MonopolyRoundStatusNone)]
	public MonopolyRoundStatus RoundStatus
	{
		get
		{
			return __pbn__RoundStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__RoundStatus = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFloorId()
	{
		return __pbn__FloorId.HasValue;
	}

	public void ResetFloorId()
	{
		__pbn__FloorId = null;
	}

	public bool ShouldSerializeRoundStatus()
	{
		return __pbn__RoundStatus.HasValue;
	}

	public void ResetRoundStatus()
	{
		__pbn__RoundStatus = null;
	}
}
