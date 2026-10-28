using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleSkillComponent : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "skill_info_list")]
	public List<QAvatarBattleSkillInfo> SkillInfoLists { get; } = new List<QAvatarBattleSkillInfo>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
