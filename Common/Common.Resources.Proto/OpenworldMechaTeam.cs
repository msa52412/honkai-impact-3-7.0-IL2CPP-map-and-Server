using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldMechaTeam : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "mecha_list")]
	public List<OpenworldMechaTeamMember> MechaLists { get; } = new List<OpenworldMechaTeamMember>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
