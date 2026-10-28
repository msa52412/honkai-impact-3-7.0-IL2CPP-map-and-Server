using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleUseSkillNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6587
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "player_use_skill")]
	public PlayerSkill PlayerUseSkill { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
