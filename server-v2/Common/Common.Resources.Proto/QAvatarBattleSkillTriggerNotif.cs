using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleSkillTriggerNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5610
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TriggerEntityId;

	[ProtoMember(1, Name = "trigger_entity_id")]
	public uint TriggerEntityId
	{
		get
		{
			return __pbn__TriggerEntityId.GetValueOrDefault();
		}
		set
		{
			__pbn__TriggerEntityId = value;
		}
	}

	[ProtoMember(2, Name = "trigger_entity")]
	public QAvatarBattleEntity TriggerEntity { get; set; }

	[ProtoMember(3, Name = "skill_info")]
	public QAvatarBattleSkillInfo SkillInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTriggerEntityId()
	{
		return __pbn__TriggerEntityId.HasValue;
	}

	public void ResetTriggerEntityId()
	{
		__pbn__TriggerEntityId = null;
	}
}
