using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MonopolyAddMonsterReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3867
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AreaId;

	private uint? __pbn__FloorId;

	private uint? __pbn__MonsterId;

	private uint? __pbn__Position;

	private uint? __pbn__Direction;

	[ProtoMember(1, Name = "area_id")]
	public uint AreaId
	{
		get
		{
			return __pbn__AreaId.GetValueOrDefault();
		}
		set
		{
			__pbn__AreaId = value;
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

	[ProtoMember(3, Name = "monster_id")]
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

	[ProtoMember(4, Name = "position")]
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

	[ProtoMember(5, Name = "direction")]
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

	public bool ShouldSerializeAreaId()
	{
		return __pbn__AreaId.HasValue;
	}

	public void ResetAreaId()
	{
		__pbn__AreaId = null;
	}

	public bool ShouldSerializeFloorId()
	{
		return __pbn__FloorId.HasValue;
	}

	public void ResetFloorId()
	{
		__pbn__FloorId = null;
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
}
