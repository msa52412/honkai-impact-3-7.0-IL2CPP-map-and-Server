using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldSkillDamageJudgeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2420
	}

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

	[ProtoMember(2, Name = "hit_unique_id_list")]
	public uint[] HitUniqueIdLists { get; set; }

	[ProtoMember(3, Name = "damage_pos")]
	public ChatworldPosition DamagePos { get; set; }

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
