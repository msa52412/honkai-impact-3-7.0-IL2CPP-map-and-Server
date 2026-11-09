using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MonsterCardActivityRankInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CollectionLevel;

	private uint? __pbn__Floor;

	[ProtoMember(1, Name = "team_brief_data_list")]
	public List<MonsterCardCardBriefData> TeamBriefDataLists { get; } = new List<MonsterCardCardBriefData>();


	[ProtoMember(2, Name = "collection_level")]
	public uint CollectionLevel
	{
		get
		{
			return __pbn__CollectionLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__CollectionLevel = value;
		}
	}

	[ProtoMember(3, Name = "floor")]
	public uint Floor
	{
		get
		{
			return __pbn__Floor.GetValueOrDefault();
		}
		set
		{
			__pbn__Floor = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCollectionLevel()
	{
		return __pbn__CollectionLevel.HasValue;
	}

	public void ResetCollectionLevel()
	{
		__pbn__CollectionLevel = null;
	}

	public bool ShouldSerializeFloor()
	{
		return __pbn__Floor.HasValue;
	}

	public void ResetFloor()
	{
		__pbn__Floor = null;
	}
}
