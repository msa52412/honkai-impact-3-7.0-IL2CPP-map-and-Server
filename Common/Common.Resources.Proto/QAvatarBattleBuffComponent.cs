using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleBuffComponent : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "buff_list")]
	public List<QAvatarBattleBuff> BuffLists { get; } = new List<QAvatarBattleBuff>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
