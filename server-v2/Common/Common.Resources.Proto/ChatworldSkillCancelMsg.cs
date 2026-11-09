using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldSkillCancelMsg : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SkillUniqueId;

	[ProtoMember(1, Name = "skill_unique_id")]
	public uint SkillUniqueId
	{
		get
		{
			return __pbn__SkillUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__SkillUniqueId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSkillUniqueId()
	{
		return __pbn__SkillUniqueId.HasValue;
	}

	public void ResetSkillUniqueId()
	{
		__pbn__SkillUniqueId = null;
	}
}
