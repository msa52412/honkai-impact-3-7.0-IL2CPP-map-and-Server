using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InLevelActorSnapshot : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "avatar_snapshot")]
	public InLevelAvatarActorSnapshot AvatarSnapshot { get; set; }

	[ProtoMember(2, Name = "monster_snapshot")]
	public InLevelMonsterActorSnapshot MonsterSnapshot { get; set; }

	[ProtoMember(3, Name = "base_snapshot")]
	public InLevelBaseActorSnapshot BaseSnapshot { get; set; }

	[ProtoMember(4, Name = "property_list")]
	public List<InLevelEntityProperty> PropertyLists { get; } = new List<InLevelEntityProperty>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
