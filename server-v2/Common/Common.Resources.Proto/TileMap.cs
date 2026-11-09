using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TileMap : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__Width;

	private uint? __pbn__Height;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(2, Name = "width")]
	public uint Width
	{
		get
		{
			return __pbn__Width.GetValueOrDefault();
		}
		set
		{
			__pbn__Width = value;
		}
	}

	[ProtoMember(3, Name = "height")]
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

	[ProtoMember(5, Name = "cell_list")]
	public List<TileMapCell> CellLists { get; } = new List<TileMapCell>();


	[ProtoMember(6, Name = "trigger_list")]
	public List<TileTrigger> TriggerLists { get; } = new List<TileTrigger>();


	[ProtoMember(7, Name = "entity_list")]
	public List<TileEntity> EntityLists { get; } = new List<TileEntity>();


	[ProtoMember(8, Name = "mark_list")]
	public List<TileMark> MarkLists { get; } = new List<TileMark>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeWidth()
	{
		return __pbn__Width.HasValue;
	}

	public void ResetWidth()
	{
		__pbn__Width = null;
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
