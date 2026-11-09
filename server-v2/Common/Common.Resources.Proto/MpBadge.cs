using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpBadge : IExtensible
{
	private IExtension __pbn__extensionData;

	private ulong? __pbn__UniqueIdU64;

	private uint? __pbn__BadgeId;

	[ProtoMember(1, Name = "unique_id_u64")]
	public ulong UniqueIdU64
	{
		get
		{
			return __pbn__UniqueIdU64.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueIdU64 = value;
		}
	}

	[ProtoMember(2, Name = "badge_id")]
	public uint BadgeId
	{
		get
		{
			return __pbn__BadgeId.GetValueOrDefault();
		}
		set
		{
			__pbn__BadgeId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueIdU64()
	{
		return __pbn__UniqueIdU64.HasValue;
	}

	public void ResetUniqueIdU64()
	{
		__pbn__UniqueIdU64 = null;
	}

	public bool ShouldSerializeBadgeId()
	{
		return __pbn__BadgeId.HasValue;
	}

	public void ResetBadgeId()
	{
		__pbn__BadgeId = null;
	}
}
