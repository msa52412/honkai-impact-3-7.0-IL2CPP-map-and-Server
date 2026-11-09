using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarRoll : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__Progress;

	private bool? __pbn__IsUnlock;

	[ProtoMember(1, Name = "avatar_id")]
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

	[ProtoMember(2, Name = "progress")]
	public uint Progress
	{
		get
		{
			return __pbn__Progress.GetValueOrDefault();
		}
		set
		{
			__pbn__Progress = value;
		}
	}

	[ProtoMember(3, Name = "has_take_group_list")]
	public uint[] HasTakeGroupLists { get; set; }

	[ProtoMember(4, Name = "is_unlock")]
	public bool IsUnlock
	{
		get
		{
			return __pbn__IsUnlock.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUnlock = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeIsUnlock()
	{
		return __pbn__IsUnlock.HasValue;
	}

	public void ResetIsUnlock()
	{
		__pbn__IsUnlock = null;
	}
}
