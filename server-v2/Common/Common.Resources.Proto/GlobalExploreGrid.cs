using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalExploreGrid : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EntityId;

	private uint? __pbn__EntityExpireTime;

	private bool? __pbn__IsExplored;

	[ProtoMember(1, Name = "pos")]
	public GlobalExplorePos Pos { get; set; }

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

	[ProtoMember(3, Name = "entity_expire_time")]
	public uint EntityExpireTime
	{
		get
		{
			return __pbn__EntityExpireTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityExpireTime = value;
		}
	}

	[ProtoMember(4, Name = "is_explored")]
	public bool IsExplored
	{
		get
		{
			return __pbn__IsExplored.GetValueOrDefault();
		}
		set
		{
			__pbn__IsExplored = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEntityId()
	{
		return __pbn__EntityId.HasValue;
	}

	public void ResetEntityId()
	{
		__pbn__EntityId = null;
	}

	public bool ShouldSerializeEntityExpireTime()
	{
		return __pbn__EntityExpireTime.HasValue;
	}

	public void ResetEntityExpireTime()
	{
		__pbn__EntityExpireTime = null;
	}

	public bool ShouldSerializeIsExplored()
	{
		return __pbn__IsExplored.HasValue;
	}

	public void ResetIsExplored()
	{
		__pbn__IsExplored = null;
	}
}
