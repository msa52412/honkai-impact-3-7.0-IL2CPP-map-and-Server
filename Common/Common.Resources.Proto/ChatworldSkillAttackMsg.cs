using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldSkillAttackMsg : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SkillId;

	[ProtoMember(1, Name = "skill_unique_id_list")]
	public uint[] SkillUniqueIdLists { get; set; }

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

	[ProtoMember(3, Name = "attack_pos")]
	public ChatworldPosition AttackPos { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSkillId()
	{
		return __pbn__SkillId.HasValue;
	}

	public void ResetSkillId()
	{
		__pbn__SkillId = null;
	}
}
