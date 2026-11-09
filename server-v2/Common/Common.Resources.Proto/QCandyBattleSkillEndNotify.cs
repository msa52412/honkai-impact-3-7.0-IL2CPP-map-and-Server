using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleSkillEndNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6590
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "player_skill_end")]
	public PlayerSkill PlayerSkillEnd { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
