using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldTechData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__Level;

	private uint? __pbn__Exp;

	private uint? __pbn__NextCollectTime;

	private uint? __pbn__SkillPoint;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(2, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(3, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(4, Name = "next_collect_time")]
	public uint NextCollectTime
	{
		get
		{
			return __pbn__NextCollectTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextCollectTime = value;
		}
	}

	[ProtoMember(5, Name = "skill_point")]
	public uint SkillPoint
	{
		get
		{
			return __pbn__SkillPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__SkillPoint = value;
		}
	}

	[ProtoMember(6, Name = "unlock_skill_list")]
	public uint[] UnlockSkillLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeNextCollectTime()
	{
		return __pbn__NextCollectTime.HasValue;
	}

	public void ResetNextCollectTime()
	{
		__pbn__NextCollectTime = null;
	}

	public bool ShouldSerializeSkillPoint()
	{
		return __pbn__SkillPoint.HasValue;
	}

	public void ResetSkillPoint()
	{
		__pbn__SkillPoint = null;
	}
}
