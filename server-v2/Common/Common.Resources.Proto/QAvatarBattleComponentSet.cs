using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleComponentSet : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "position_component")]
	public QAvatarBattlePositionComponent PositionComponent { get; set; }

	[ProtoMember(2, Name = "speed_component")]
	public QAvatarBattleSpeedComponent SpeedComponent { get; set; }

	[ProtoMember(3, Name = "skill_component")]
	public QAvatarBattleSkillComponent SkillComponent { get; set; }

	[ProtoMember(4, Name = "evade_component")]
	public QAvatarBattleEvadeComponent EvadeComponent { get; set; }

	[ProtoMember(5, Name = "buff_component")]
	public QAvatarBattleBuffComponent BuffComponent { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
