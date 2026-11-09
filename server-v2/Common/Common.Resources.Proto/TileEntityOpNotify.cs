using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TileEntityOpNotify : IExtensible
{
	[ProtoContract]
	public class MoveOp : IExtensible
	{
		private IExtension __pbn__extensionData;

		private TileMoveType? __pbn__MoveType;

		private uint? __pbn__Direction;

		[ProtoMember(1, Name = "path_cell_id_list")]
		public uint[] PathCellIdLists { get; set; }

		[ProtoMember(2, Name = "move_type")]
		[DefaultValue(TileMoveType.TileMoveWalk)]
		public TileMoveType MoveType
		{
			get
			{
				return __pbn__MoveType.GetValueOrDefault();
			}
			set
			{
				__pbn__MoveType = value;
			}
		}

		[ProtoMember(3, Name = "direction")]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeMoveType()
		{
			return __pbn__MoveType.HasValue;
		}

		public void ResetMoveType()
		{
			__pbn__MoveType = null;
		}

		public bool ShouldSerializeDirection()
		{
			return __pbn__Direction.HasValue;
		}

		public void ResetDirection()
		{
			__pbn__Direction = null;
		}
	}

	[ProtoContract]
	public class StatusOp : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__Status;

		[ProtoMember(1, Name = "status")]
		public uint Status
		{
			get
			{
				return __pbn__Status.GetValueOrDefault();
			}
			set
			{
				__pbn__Status = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeStatus()
		{
			return __pbn__Status.HasValue;
		}

		public void ResetStatus()
		{
			__pbn__Status = null;
		}
	}

	[ProtoContract]
	public class TileEntityOp : IExtensible
	{
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

		[ProtoMember(2, Name = "move")]
		public MoveOp Move { get; set; }

		[ProtoMember(3, Name = "wait_select_path")]
		public TileWaitSelectPath WaitSelectPath { get; set; }

		[ProtoMember(4, Name = "update_cell")]
		public UpdateCellOp UpdateCell { get; set; }

		[ProtoMember(5, Name = "update_entity")]
		public UpdateEntityOp UpdateEntity { get; set; }

		[ProtoMember(6, Name = "status")]
		public StatusOp Status { get; set; }

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

	[ProtoContract]
	public class UpdateCellOp : IExtensible
	{
		private IExtension __pbn__extensionData;

		[ProtoMember(1, Name = "cell_list")]
		public List<TileMapCell> CellLists { get; } = new List<TileMapCell>();


		[ProtoMember(2, Name = "trigger_list")]
		public List<TileTrigger> TriggerLists { get; } = new List<TileTrigger>();


		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}
	}

	[ProtoContract]
	public class UpdateEntityOp : IExtensible
	{
		private IExtension __pbn__extensionData;

		[ProtoMember(1, Name = "create_entity_list")]
		public List<TileEntity> CreateEntityLists { get; } = new List<TileEntity>();


		[ProtoMember(2, Name = "delete_entity_list")]
		public uint[] DeleteEntityLists { get; set; }

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}
	}

	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4914
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

	[ProtoMember(3, Name = "entity_op_list")]
	public List<TileEntityOp> EntityOpLists { get; } = new List<TileEntityOp>();


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
