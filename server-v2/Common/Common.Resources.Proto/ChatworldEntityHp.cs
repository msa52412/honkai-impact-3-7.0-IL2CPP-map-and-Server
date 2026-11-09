using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldEntityHp : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__HitUniqueId;

	private uint? __pbn__OldHp;

	private uint? __pbn__LeftHp;

	[ProtoMember(1, Name = "hit_unique_id")]
	public uint HitUniqueId
	{
		get
		{
			return __pbn__HitUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__HitUniqueId = value;
		}
	}

	[ProtoMember(2, Name = "old_hp")]
	public uint OldHp
	{
		get
		{
			return __pbn__OldHp.GetValueOrDefault();
		}
		set
		{
			__pbn__OldHp = value;
		}
	}

	[ProtoMember(3, Name = "left_hp")]
	public uint LeftHp
	{
		get
		{
			return __pbn__LeftHp.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftHp = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHitUniqueId()
	{
		return __pbn__HitUniqueId.HasValue;
	}

	public void ResetHitUniqueId()
	{
		__pbn__HitUniqueId = null;
	}

	public bool ShouldSerializeOldHp()
	{
		return __pbn__OldHp.HasValue;
	}

	public void ResetOldHp()
	{
		__pbn__OldHp = null;
	}

	public bool ShouldSerializeLeftHp()
	{
		return __pbn__LeftHp.HasValue;
	}

	public void ResetLeftHp()
	{
		__pbn__LeftHp = null;
	}
}
