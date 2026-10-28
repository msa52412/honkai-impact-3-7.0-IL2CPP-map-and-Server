using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldSkillDamageMsg : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SkillUniqueId;

	private uint? __pbn__SkillId;

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

	[ProtoMember(3, Name = "damage_pos")]
	public ChatworldPosition DamagePos { get; set; }

	[ProtoMember(4, Name = "entity_hp_list")]
	public List<ChatworldEntityHp> EntityHpLists { get; } = new List<ChatworldEntityHp>();


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

	public bool ShouldSerializeSkillId()
	{
		return __pbn__SkillId.HasValue;
	}

	public void ResetSkillId()
	{
		__pbn__SkillId = null;
	}
}
