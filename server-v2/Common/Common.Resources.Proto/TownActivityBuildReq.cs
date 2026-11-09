using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityBuildReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6454
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__UseBrickIndex;

	private uint? __pbn__UseBrickPosition;

	private uint? __pbn__TargetBuildingId;

	private uint? __pbn__TargetBuildingPosition;

	private uint? __pbn__Direction;

	[ProtoMember(1, Name = "head")]
	public TownCommonHead Head { get; set; }

	[ProtoMember(2, Name = "use_brick_index")]
	public uint UseBrickIndex
	{
		get
		{
			return __pbn__UseBrickIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__UseBrickIndex = value;
		}
	}

	[ProtoMember(3, Name = "use_brick_position")]
	public uint UseBrickPosition
	{
		get
		{
			return __pbn__UseBrickPosition.GetValueOrDefault();
		}
		set
		{
			__pbn__UseBrickPosition = value;
		}
	}

	[ProtoMember(4, Name = "target_building_id")]
	public uint TargetBuildingId
	{
		get
		{
			return __pbn__TargetBuildingId.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetBuildingId = value;
		}
	}

	[ProtoMember(5, Name = "target_building_position")]
	public uint TargetBuildingPosition
	{
		get
		{
			return __pbn__TargetBuildingPosition.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetBuildingPosition = value;
		}
	}

	[ProtoMember(6, Name = "direction")]
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

	[ProtoMember(7, Name = "consume_building_unique_id_list")]
	public uint[] ConsumeBuildingUniqueIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUseBrickIndex()
	{
		return __pbn__UseBrickIndex.HasValue;
	}

	public void ResetUseBrickIndex()
	{
		__pbn__UseBrickIndex = null;
	}

	public bool ShouldSerializeUseBrickPosition()
	{
		return __pbn__UseBrickPosition.HasValue;
	}

	public void ResetUseBrickPosition()
	{
		__pbn__UseBrickPosition = null;
	}

	public bool ShouldSerializeTargetBuildingId()
	{
		return __pbn__TargetBuildingId.HasValue;
	}

	public void ResetTargetBuildingId()
	{
		__pbn__TargetBuildingId = null;
	}

	public bool ShouldSerializeTargetBuildingPosition()
	{
		return __pbn__TargetBuildingPosition.HasValue;
	}

	public void ResetTargetBuildingPosition()
	{
		__pbn__TargetBuildingPosition = null;
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
