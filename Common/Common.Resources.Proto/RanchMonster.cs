using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RanchMonster : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	private uint? __pbn__MonsterId;

	private uint? __pbn__RanchAreaId;

	private uint? __pbn__RanchLastSettleTime;

	private uint? __pbn__MonsterLevel;

	private uint? __pbn__MonsterExp;

	[ProtoMember(1, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(2, Name = "monster_id")]
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

	[ProtoMember(3, Name = "skill_list")]
	public List<RanchMonsterSkill> SkillLists { get; } = new List<RanchMonsterSkill>();


	[ProtoMember(4, Name = "ranch_area_id")]
	public uint RanchAreaId
	{
		get
		{
			return __pbn__RanchAreaId.GetValueOrDefault();
		}
		set
		{
			__pbn__RanchAreaId = value;
		}
	}

	[ProtoMember(5, Name = "ranch_last_settle_time")]
	public uint RanchLastSettleTime
	{
		get
		{
			return __pbn__RanchLastSettleTime.GetValueOrDefault();
		}
		set
		{
			__pbn__RanchLastSettleTime = value;
		}
	}

	[ProtoMember(6, Name = "monster_level")]
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

	[ProtoMember(7, Name = "monster_exp")]
	public uint MonsterExp
	{
		get
		{
			return __pbn__MonsterExp.GetValueOrDefault();
		}
		set
		{
			__pbn__MonsterExp = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeMonsterId()
	{
		return __pbn__MonsterId.HasValue;
	}

	public void ResetMonsterId()
	{
		__pbn__MonsterId = null;
	}

	public bool ShouldSerializeRanchAreaId()
	{
		return __pbn__RanchAreaId.HasValue;
	}

	public void ResetRanchAreaId()
	{
		__pbn__RanchAreaId = null;
	}

	public bool ShouldSerializeRanchLastSettleTime()
	{
		return __pbn__RanchLastSettleTime.HasValue;
	}

	public void ResetRanchLastSettleTime()
	{
		__pbn__RanchLastSettleTime = null;
	}

	public bool ShouldSerializeMonsterLevel()
	{
		return __pbn__MonsterLevel.HasValue;
	}

	public void ResetMonsterLevel()
	{
		__pbn__MonsterLevel = null;
	}

	public bool ShouldSerializeMonsterExp()
	{
		return __pbn__MonsterExp.HasValue;
	}

	public void ResetMonsterExp()
	{
		__pbn__MonsterExp = null;
	}
}
