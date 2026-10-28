using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TileMapUpdateNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4923
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


	[ProtoMember(4, Name = "entity_list")]
	public List<TileEntity> EntityLists { get; } = new List<TileEntity>();


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
