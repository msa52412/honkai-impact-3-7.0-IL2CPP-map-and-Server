using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleStigmataComponent : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "stigmata_list")]
	public List<QAvatarBattleStigmata> StigmataLists { get; } = new List<QAvatarBattleStigmata>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
