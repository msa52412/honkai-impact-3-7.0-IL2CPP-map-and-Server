using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarSubSkill : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SubSkillId;

	private uint? __pbn__Level;

	private bool? __pbn__IsMask;

	[ProtoMember(1, Name = "sub_skill_id")]
	public uint SubSkillId
	{
		get
		{
			return __pbn__SubSkillId.GetValueOrDefault();
		}
		set
		{
			__pbn__SubSkillId = value;
		}
	}

	[ProtoMember(2, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(3, Name = "is_mask")]
	public bool IsMask
	{
		get
		{
			return __pbn__IsMask.GetValueOrDefault();
		}
		set
		{
			__pbn__IsMask = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSubSkillId()
	{
		return __pbn__SubSkillId.HasValue;
	}

	public void ResetSubSkillId()
	{
		__pbn__SubSkillId = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeIsMask()
	{
		return __pbn__IsMask.HasValue;
	}

	public void ResetIsMask()
	{
		__pbn__IsMask = null;
	}
}
