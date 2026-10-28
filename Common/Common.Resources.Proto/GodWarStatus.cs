using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarStatus : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TeleportId;

	private bool? __pbn__IsAbandon;

	[ProtoMember(1, Name = "teleport_id")]
	public uint TeleportId
	{
		get
		{
			return __pbn__TeleportId.GetValueOrDefault();
		}
		set
		{
			__pbn__TeleportId = value;
		}
	}

	[ProtoMember(3, Name = "is_abandon")]
	public bool IsAbandon
	{
		get
		{
			return __pbn__IsAbandon.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAbandon = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTeleportId()
	{
		return __pbn__TeleportId.HasValue;
	}

	public void ResetTeleportId()
	{
		__pbn__TeleportId = null;
	}

	public bool ShouldSerializeIsAbandon()
	{
		return __pbn__IsAbandon.HasValue;
	}

	public void ResetIsAbandon()
	{
		__pbn__IsAbandon = null;
	}
}
