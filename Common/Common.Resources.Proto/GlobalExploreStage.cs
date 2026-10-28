using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalExploreStage : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__EntityId;

	private uint? __pbn__GridId;

	private bool? __pbn__IsNeedBlock;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "entity_id")]
	public uint EntityId
	{
		get
		{
			return __pbn__EntityId.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityId = value;
		}
	}

	[ProtoMember(3, Name = "grid_id")]
	public uint GridId
	{
		get
		{
			return __pbn__GridId.GetValueOrDefault();
		}
		set
		{
			__pbn__GridId = value;
		}
	}

	[ProtoMember(4, Name = "is_need_block")]
	public bool IsNeedBlock
	{
		get
		{
			return __pbn__IsNeedBlock.GetValueOrDefault();
		}
		set
		{
			__pbn__IsNeedBlock = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeEntityId()
	{
		return __pbn__EntityId.HasValue;
	}

	public void ResetEntityId()
	{
		__pbn__EntityId = null;
	}

	public bool ShouldSerializeGridId()
	{
		return __pbn__GridId.HasValue;
	}

	public void ResetGridId()
	{
		__pbn__GridId = null;
	}

	public bool ShouldSerializeIsNeedBlock()
	{
		return __pbn__IsNeedBlock.HasValue;
	}

	public void ResetIsNeedBlock()
	{
		__pbn__IsNeedBlock = null;
	}
}
