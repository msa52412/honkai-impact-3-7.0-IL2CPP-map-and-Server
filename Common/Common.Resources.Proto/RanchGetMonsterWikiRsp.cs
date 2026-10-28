using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RanchGetMonsterWikiRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5846
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__CollectionPoint;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "monster_wiki_list")]
	public List<RanchMonsterWiki> MonsterWikiLists { get; } = new List<RanchMonsterWiki>();


	[ProtoMember(4, Name = "skill_wiki_list")]
	public List<RanchSkillWiki> SkillWikiLists { get; } = new List<RanchSkillWiki>();


	[ProtoMember(5, Name = "collection_point")]
	public uint CollectionPoint
	{
		get
		{
			return __pbn__CollectionPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__CollectionPoint = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeCollectionPoint()
	{
		return __pbn__CollectionPoint.HasValue;
	}

	public void ResetCollectionPoint()
	{
		__pbn__CollectionPoint = null;
	}
}
