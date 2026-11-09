using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlayerAdventureData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AdventureGroupLevel;

	[ProtoMember(1, Name = "adventure_group_level")]
	public uint AdventureGroupLevel
	{
		get
		{
			return __pbn__AdventureGroupLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__AdventureGroupLevel = value;
		}
	}

	[ProtoMember(2, Name = "adventure_avatar_list")]
	public List<AdventureAvatar> AdventureAvatarLists { get; } = new List<AdventureAvatar>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAdventureGroupLevel()
	{
		return __pbn__AdventureGroupLevel.HasValue;
	}

	public void ResetAdventureGroupLevel()
	{
		__pbn__AdventureGroupLevel = null;
	}
}
