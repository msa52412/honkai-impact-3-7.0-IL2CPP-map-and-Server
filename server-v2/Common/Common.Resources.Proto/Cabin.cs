using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Cabin : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Type;

	private uint? __pbn__Level;

	private uint? __pbn__ExtendGrade;

	private uint? __pbn__LevelUpEndTime;

	[ProtoMember(1, Name = "type")]
	public uint Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
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

	[ProtoMember(3, Name = "extend_grade")]
	public uint ExtendGrade
	{
		get
		{
			return __pbn__ExtendGrade.GetValueOrDefault();
		}
		set
		{
			__pbn__ExtendGrade = value;
		}
	}

	[ProtoMember(4, Name = "level_up_end_time")]
	public uint LevelUpEndTime
	{
		get
		{
			return __pbn__LevelUpEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelUpEndTime = value;
		}
	}

	[ProtoMember(5, Name = "tech_list")]
	public List<CabinTech> TechLists { get; } = new List<CabinTech>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeExtendGrade()
	{
		return __pbn__ExtendGrade.HasValue;
	}

	public void ResetExtendGrade()
	{
		__pbn__ExtendGrade = null;
	}

	public bool ShouldSerializeLevelUpEndTime()
	{
		return __pbn__LevelUpEndTime.HasValue;
	}

	public void ResetLevelUpEndTime()
	{
		__pbn__LevelUpEndTime = null;
	}
}
