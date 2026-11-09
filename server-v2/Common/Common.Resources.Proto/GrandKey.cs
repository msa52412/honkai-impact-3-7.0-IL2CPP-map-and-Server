using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GrandKey : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__EndTime;

	private uint? __pbn__UnlockLevel;

	private uint? __pbn__ScoinNum;

	private uint? __pbn__Level;

	private uint? __pbn__BreachLevel;

	private uint? __pbn__ActivateLevel;

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

	[ProtoMember(2, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(3, Name = "unlock_level")]
	public uint UnlockLevel
	{
		get
		{
			return __pbn__UnlockLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__UnlockLevel = value;
		}
	}

	[ProtoMember(4, Name = "scoin_num")]
	public uint ScoinNum
	{
		get
		{
			return __pbn__ScoinNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoinNum = value;
		}
	}

	[ProtoMember(5, Name = "level")]
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

	[ProtoMember(6, Name = "breach_level")]
	public uint BreachLevel
	{
		get
		{
			return __pbn__BreachLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__BreachLevel = value;
		}
	}

	[ProtoMember(7, Name = "activate_level")]
	public uint ActivateLevel
	{
		get
		{
			return __pbn__ActivateLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivateLevel = value;
		}
	}

	[ProtoMember(8, Name = "skill")]
	public GrandKeySkill Skill { get; set; }

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

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeUnlockLevel()
	{
		return __pbn__UnlockLevel.HasValue;
	}

	public void ResetUnlockLevel()
	{
		__pbn__UnlockLevel = null;
	}

	public bool ShouldSerializeScoinNum()
	{
		return __pbn__ScoinNum.HasValue;
	}

	public void ResetScoinNum()
	{
		__pbn__ScoinNum = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeBreachLevel()
	{
		return __pbn__BreachLevel.HasValue;
	}

	public void ResetBreachLevel()
	{
		__pbn__BreachLevel = null;
	}

	public bool ShouldSerializeActivateLevel()
	{
		return __pbn__ActivateLevel.HasValue;
	}

	public void ResetActivateLevel()
	{
		__pbn__ActivateLevel = null;
	}
}
