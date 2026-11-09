using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TileMapCell : IExtensible
{
	[ProtoContract]
	public class CellLayer : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__ModelId;

		private uint? __pbn__Direction;

		[ProtoMember(1, Name = "model_id")]
		public uint ModelId
		{
			get
			{
				return __pbn__ModelId.GetValueOrDefault();
			}
			set
			{
				__pbn__ModelId = value;
			}
		}

		[ProtoMember(2, Name = "direction")]
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

		[ProtoMember(3, Name = "updated_type_list")]
		public List<CellDataType> UpdatedTypeLists { get; } = new List<CellDataType>();


		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeModelId()
		{
			return __pbn__ModelId.HasValue;
		}

		public void ResetModelId()
		{
			__pbn__ModelId = null;
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
	public enum CellDataType
	{
		[ProtoEnum(Name = "CELL_DATA_TYPE_NONE")]
		CellDataTypeNone,
		[ProtoEnum(Name = "CELL_DATA_TYPE_BARRIER")]
		CellDataTypeBarrier,
		[ProtoEnum(Name = "CELL_DATA_TYPE_FOG")]
		CellDataTypeFog,
		[ProtoEnum(Name = "CELL_DATA_TYPE_TERRAIN")]
		CellDataTypeTerrain,
		[ProtoEnum(Name = "CELL_DATA_TYPE_LAYER_MODEL_ID")]
		CellDataTypeLayerModelId,
		[ProtoEnum(Name = "CELL_DATA_TYPE_LAYER_DIRECTION")]
		CellDataTypeLayerDirection,
		[ProtoEnum(Name = "CELL_DATA_TYPE_HEIGHT")]
		CellDataTypeHeight
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CellId;

	private uint? __pbn__Barrier;

	private uint? __pbn__Fog;

	private uint? __pbn__Terrain;

	private uint? __pbn__Height;

	[ProtoMember(1, Name = "cell_id")]
	public uint CellId
	{
		get
		{
			return __pbn__CellId.GetValueOrDefault();
		}
		set
		{
			__pbn__CellId = value;
		}
	}

	[ProtoMember(2, Name = "ground")]
	public CellLayer Ground { get; set; }

	[ProtoMember(3, Name = "decoration")]
	public CellLayer Decoration { get; set; }

	[ProtoMember(4, Name = "barrier")]
	public uint Barrier
	{
		get
		{
			return __pbn__Barrier.GetValueOrDefault();
		}
		set
		{
			__pbn__Barrier = value;
		}
	}

	[ProtoMember(5, Name = "fog")]
	public uint Fog
	{
		get
		{
			return __pbn__Fog.GetValueOrDefault();
		}
		set
		{
			__pbn__Fog = value;
		}
	}

	[ProtoMember(6, Name = "terrain")]
	public uint Terrain
	{
		get
		{
			return __pbn__Terrain.GetValueOrDefault();
		}
		set
		{
			__pbn__Terrain = value;
		}
	}

	[ProtoMember(7, Name = "updated_type_list")]
	public List<CellDataType> UpdatedTypeLists { get; } = new List<CellDataType>();


	[ProtoMember(8, Name = "height")]
	public uint Height
	{
		get
		{
			return __pbn__Height.GetValueOrDefault();
		}
		set
		{
			__pbn__Height = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCellId()
	{
		return __pbn__CellId.HasValue;
	}

	public void ResetCellId()
	{
		__pbn__CellId = null;
	}

	public bool ShouldSerializeBarrier()
	{
		return __pbn__Barrier.HasValue;
	}

	public void ResetBarrier()
	{
		__pbn__Barrier = null;
	}

	public bool ShouldSerializeFog()
	{
		return __pbn__Fog.HasValue;
	}

	public void ResetFog()
	{
		__pbn__Fog = null;
	}

	public bool ShouldSerializeTerrain()
	{
		return __pbn__Terrain.HasValue;
	}

	public void ResetTerrain()
	{
		__pbn__Terrain = null;
	}

	public bool ShouldSerializeHeight()
	{
		return __pbn__Height.HasValue;
	}

	public void ResetHeight()
	{
		__pbn__Height = null;
	}
}
