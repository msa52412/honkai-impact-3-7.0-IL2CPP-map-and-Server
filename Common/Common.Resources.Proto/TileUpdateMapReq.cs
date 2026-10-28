using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TileUpdateMapReq : IExtensible
{
	[ProtoContract]
	public class MoveEntity : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__UniqueId;

		private uint? __pbn__ToPosition;

		private uint? __pbn__Direction;

		private uint? __pbn__SavedPosition;

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

		[ProtoMember(2, Name = "to_position")]
		public uint ToPosition
		{
			get
			{
				return __pbn__ToPosition.GetValueOrDefault();
			}
			set
			{
				__pbn__ToPosition = value;
			}
		}

		[ProtoMember(3, Name = "path_cell_id_list")]
		public uint[] PathCellIdLists { get; set; }

		[ProtoMember(4, Name = "direction")]
		public uint Direction
		{
			get
			{
				return __pbn__Direction.GetValueOrDefault();
			}
			set
			{
				__pbn__Direction = value;
			}
		}

		[ProtoMember(5, Name = "saved_position")]
		public uint SavedPosition
		{
			get
			{
				return __pbn__SavedPosition.GetValueOrDefault();
			}
			set
			{
				__pbn__SavedPosition = value;
			}
		}

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

		public bool ShouldSerializeToPosition()
		{
			return __pbn__ToPosition.HasValue;
		}

		public void ResetToPosition()
		{
			__pbn__ToPosition = null;
		}

		public bool ShouldSerializeDirection()
		{
			return __pbn__Direction.HasValue;
		}

		public void ResetDirection()
		{
			__pbn__Direction = null;
		}

		public bool ShouldSerializeSavedPosition()
		{
			return __pbn__SavedPosition.HasValue;
		}

		public void ResetSavedPosition()
		{
			__pbn__SavedPosition = null;
		}
	}

	[ProtoContract]
	public class OuterValue : IExtensible
	{
		[ProtoContract]
		public enum Type
		{
			[ProtoEnum(Name = "NONE")]
			None,
			[ProtoEnum(Name = "RPG_OVERALL")]
			RpgOverall
		}

		private IExtension __pbn__extensionData;

		private Type? __pbn__type;

		private uint? __pbn__Key;

		private uint? __pbn__Value;

		[ProtoMember(1)]
		[DefaultValue(Type.None)]
		public Type type
		{
			get
			{
				return __pbn__type.GetValueOrDefault();
			}
			set
			{
				__pbn__type = value;
			}
		}

		[ProtoMember(2, Name = "key")]
		public uint Key
		{
			get
			{
				return __pbn__Key.GetValueOrDefault();
			}
			set
			{
				__pbn__Key = value;
			}
		}

		[ProtoMember(3, Name = "value")]
		public uint Value
		{
			get
			{
				return __pbn__Value.GetValueOrDefault();
			}
			set
			{
				__pbn__Value = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializetype()
		{
			return __pbn__type.HasValue;
		}

		public void Resettype()
		{
			__pbn__type = null;
		}

		public bool ShouldSerializeKey()
		{
			return __pbn__Key.HasValue;
		}

		public void ResetKey()
		{
			__pbn__Key = null;
		}

		public bool ShouldSerializeValue()
		{
			return __pbn__Value.HasValue;
		}

		public void ResetValue()
		{
			__pbn__Value = null;
		}
	}

	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4902
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TowerId;

	private uint? __pbn__FloorId;

	[ProtoMember(1, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	[ProtoMember(2, Name = "floor_id")]
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

	[ProtoMember(3, Name = "cell_list")]
	public List<TileMapCell> CellLists { get; } = new List<TileMapCell>();


	[ProtoMember(4, Name = "trigger_list")]
	public List<TileTrigger> TriggerLists { get; } = new List<TileTrigger>();


	[ProtoMember(5, Name = "create_entity_list")]
	public List<TileEntity> CreateEntityLists { get; } = new List<TileEntity>();


	[ProtoMember(6, Name = "move_entity_list")]
	public List<MoveEntity> MoveEntityLists { get; } = new List<MoveEntity>();


	[ProtoMember(7, Name = "delete_entity_list")]
	public uint[] DeleteEntityLists { get; set; }

	[ProtoMember(8, Name = "mark_list")]
	public List<TileMark> MarkLists { get; } = new List<TileMark>();


	[ProtoMember(9, Name = "outer_value_list")]
	public List<OuterValue> OuterValueLists { get; } = new List<OuterValue>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}

	public bool ShouldSerializeFloorId()
	{
		return __pbn__FloorId.HasValue;
	}

	public void ResetFloorId()
	{
		__pbn__FloorId = null;
	}
}
