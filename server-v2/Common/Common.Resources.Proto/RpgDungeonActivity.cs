using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgDungeonActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "quest_list")]
	public List<RpgDungeonQuest> QuestLists { get; } = new List<RpgDungeonQuest>();


	[ProtoMember(3, Name = "candidate_buff_id_list")]
	public uint[] CandidateBuffIdLists { get; set; }

	[ProtoMember(4, Name = "buff_list")]
	public List<RpgDungeonBuff> BuffLists { get; } = new List<RpgDungeonBuff>();


	[ProtoMember(5, Name = "pool_list")]
	public List<RpgDungeonBuffPool> PoolLists { get; } = new List<RpgDungeonBuffPool>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}
}
