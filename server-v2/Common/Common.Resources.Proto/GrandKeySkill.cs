using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GrandKeySkill : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__KeyId;

	private uint? __pbn__SkillId;

	private uint? __pbn__LastTime;

	[ProtoMember(1, Name = "key_id")]
	public uint KeyId
	{
		get
		{
			return __pbn__KeyId.GetValueOrDefault();
		}
		set
		{
			__pbn__KeyId = value;
		}
	}

	[ProtoMember(2, Name = "skill_id")]
	public uint SkillId
	{
		get
		{
			return __pbn__SkillId.GetValueOrDefault();
		}
		set
		{
			__pbn__SkillId = value;
		}
	}

	[ProtoMember(3, Name = "last_time")]
	public uint LastTime
	{
		get
		{
			return __pbn__LastTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeKeyId()
	{
		return __pbn__KeyId.HasValue;
	}

	public void ResetKeyId()
	{
		__pbn__KeyId = null;
	}

	public bool ShouldSerializeSkillId()
	{
		return __pbn__SkillId.HasValue;
	}

	public void ResetSkillId()
	{
		__pbn__SkillId = null;
	}

	public bool ShouldSerializeLastTime()
	{
		return __pbn__LastTime.HasValue;
	}

	public void ResetLastTime()
	{
		__pbn__LastTime = null;
	}
}
