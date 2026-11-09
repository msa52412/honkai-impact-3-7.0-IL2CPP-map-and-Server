using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TileEntity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	private uint? __pbn__EntityType;

	private uint? __pbn__Position;

	private uint? __pbn__Direction;

	private uint? __pbn__ResetTag;

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

	[ProtoMember(2, Name = "entity_type")]
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

	[ProtoMember(5, Name = "reset_tag")]
	public uint ResetTag
	{
		get
		{
			return __pbn__ResetTag.GetValueOrDefault();
		}
		set
		{
			__pbn__ResetTag = value;
		}
	}

	[ProtoMember(11, Name = "player_info")]
	public TilePlayerInfo PlayerInfo { get; set; }

	[ProtoMember(12, Name = "npc_info")]
	public TileNpcInfo NpcInfo { get; set; }

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

	public bool ShouldSerializeEntityType()
	{
		return __pbn__EntityType.HasValue;
	}

	public void ResetEntityType()
	{
		__pbn__EntityType = null;
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

	public bool ShouldSerializeResetTag()
	{
		return __pbn__ResetTag.HasValue;
	}

	public void ResetResetTag()
	{
		__pbn__ResetTag = null;
	}
}
