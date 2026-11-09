using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MonopolyAddMonsterRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3868
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ALREADY_ADD")]
		AlreadyAdd
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MonsterId;

	private uint? __pbn__Position;

	private uint? __pbn__Direction;

	private uint? __pbn__UniqueId;

	private uint? __pbn__EntityType;

	private uint? __pbn__TileEntityId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "monster_id")]
	public uint MonsterId
	{
		get
		{
			return __pbn__MonsterId.GetValueOrDefault();
		}
		set
		{
			__pbn__MonsterId = value;
		}
	}

	[ProtoMember(3, Name = "position")]
	public uint Position
	{
		get
		{
			return __pbn__Position.GetValueOrDefault();
		}
		set
		{
			__pbn__Position = value;
		}
	}

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

	[ProtoMember(5, Name = "unique_id")]
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

	[ProtoMember(6, Name = "entity_type")]
	public uint EntityType
	{
		get
		{
			return __pbn__EntityType.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityType = value;
		}
	}

	[ProtoMember(7, Name = "tile_entity_id")]
	public uint TileEntityId
	{
		get
		{
			return __pbn__TileEntityId.GetValueOrDefault();
		}
		set
		{
			__pbn__TileEntityId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMonsterId()
	{
		return __pbn__MonsterId.HasValue;
	}

	public void ResetMonsterId()
	{
		__pbn__MonsterId = null;
	}

	public bool ShouldSerializePosition()
	{
		return __pbn__Position.HasValue;
	}

	public void ResetPosition()
	{
		__pbn__Position = null;
	}

	public bool ShouldSerializeDirection()
	{
		return __pbn__Direction.HasValue;
	}

	public void ResetDirection()
	{
		__pbn__Direction = null;
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeEntityType()
	{
		return __pbn__EntityType.HasValue;
	}

	public void ResetEntityType()
	{
		__pbn__EntityType = null;
	}

	public bool ShouldSerializeTileEntityId()
	{
		return __pbn__TileEntityId.HasValue;
	}

	public void ResetTileEntityId()
	{
		__pbn__TileEntityId = null;
	}
}
