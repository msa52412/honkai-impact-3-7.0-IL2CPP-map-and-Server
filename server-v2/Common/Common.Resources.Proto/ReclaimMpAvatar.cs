using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReclaimMpAvatar : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ReclaimLevel;

	private uint? __pbn__AvatarId;

	[ProtoMember(1, Name = "reclaim_avatar")]
	public ReclaimAvatar ReclaimAvatar { get; set; }

	[ProtoMember(2, Name = "reclaim_level")]
	public uint ReclaimLevel
	{
		get
		{
			return __pbn__ReclaimLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ReclaimLevel = value;
		}
	}

	[ProtoMember(3, Name = "reclaim_avatar_id_list")]
	public uint[] ReclaimAvatarIdLists { get; set; }

	[ProtoMember(4, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReclaimLevel()
	{
		return __pbn__ReclaimLevel.HasValue;
	}

	public void ResetReclaimLevel()
	{
		__pbn__ReclaimLevel = null;
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}
}
