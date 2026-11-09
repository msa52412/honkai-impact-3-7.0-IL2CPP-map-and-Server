using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RanchMonsterBriefInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MonsterId;

	private uint? __pbn__MonsterLevel;

	[ProtoMember(1, Name = "monster_id")]
	public uint MonsterId
	{
		get
		{
			return __pbn__MonsterId.GetValueOrDefault();
		}
		set
		{
			__pbn__MonsterId = value;
		}
	}

	[ProtoMember(2, Name = "skill_id_list")]
	public uint[] SkillIdLists { get; set; }

	[ProtoMember(3, Name = "monster_level")]
	public uint MonsterLevel
	{
		get
		{
			return __pbn__MonsterLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MonsterLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMonsterId()
	{
		return __pbn__MonsterId.HasValue;
	}

	public void ResetMonsterId()
	{
		__pbn__MonsterId = null;
	}

	public bool ShouldSerializeMonsterLevel()
	{
		return __pbn__MonsterLevel.HasValue;
	}

	public void ResetMonsterLevel()
	{
		__pbn__MonsterLevel = null;
	}
}
