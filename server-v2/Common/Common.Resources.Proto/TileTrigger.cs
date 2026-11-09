using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TileTrigger : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private bool? __pbn__IsActive;

	private uint? __pbn__Position;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "is_active")]
	public bool IsActive
	{
		get
		{
			return __pbn__IsActive.GetValueOrDefault();
		}
		set
		{
			__pbn__IsActive = value;
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeIsActive()
	{
		return __pbn__IsActive.HasValue;
	}

	public void ResetIsActive()
	{
		__pbn__IsActive = null;
	}

	public bool ShouldSerializePosition()
	{
		return __pbn__Position.HasValue;
	}

	public void ResetPosition()
	{
		__pbn__Position = null;
	}
}
