using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetNewOpenworldRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4497
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "CYCLE_NOT_OPEN")]
		CycleNotOpen
	}

	private IExtension __pbn__extensionData;

	private OpenworldDataType? __pbn__DataType;

	private uint? __pbn__NextRefreshTime;

	private uint? __pbn__GlobalRandomSeed;

	private uint? __pbn__CloseTime;

	private uint? __pbn__QuestThemeIndex;

	private uint? __pbn__QuestLevel;

	private uint? __pbn__QuestStar;

	private uint? __pbn__MaxQuestLevel;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "data_type")]
	[DefaultValue(OpenworldDataType.OpenworldDataAll)]
	public OpenworldDataType DataType
	{
		get
		{
			return __pbn__DataType.GetValueOrDefault();
		}
		set
		{
			__pbn__DataType = value;
		}
	}

	[ProtoMember(3, Name = "map_list")]
	public List<OpenworldMapBriefData> MapLists { get; } = new List<OpenworldMapBriefData>();


	[ProtoMember(4, Name = "next_refresh_time")]
	public uint NextRefreshTime
	{
		get
		{
			return __pbn__NextRefreshTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextRefreshTime = value;
		}
	}

	[ProtoMember(6, Name = "global_random_seed")]
	public uint GlobalRandomSeed
	{
		get
		{
			return __pbn__GlobalRandomSeed.GetValueOrDefault();
		}
		set
		{
			__pbn__GlobalRandomSeed = value;
		}
	}

	[ProtoMember(7, Name = "close_time")]
	public uint CloseTime
	{
		get
		{
			return __pbn__CloseTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CloseTime = value;
		}
	}

	[ProtoMember(8, Name = "cook_list")]
	public List<OpenworldCook> CookLists { get; } = new List<OpenworldCook>();


	[ProtoMember(9, Name = "quest_theme_index")]
	public uint QuestThemeIndex
	{
		get
		{
			return __pbn__QuestThemeIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__QuestThemeIndex = value;
		}
	}

	[ProtoMember(10, Name = "tech")]
	public List<OpenworldTechData> Techs { get; } = new List<OpenworldTechData>();


	[ProtoMember(11, Name = "mecha_port_list")]
	public List<OpenworldMechaPort> MechaPortLists { get; } = new List<OpenworldMechaPort>();


	[ProtoMember(12, Name = "quest_level")]
	public uint QuestLevel
	{
		get
		{
			return __pbn__QuestLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__QuestLevel = value;
		}
	}

	[ProtoMember(13, Name = "quest_star")]
	public uint QuestStar
	{
		get
		{
			return __pbn__QuestStar.GetValueOrDefault();
		}
		set
		{
			__pbn__QuestStar = value;
		}
	}

	[ProtoMember(14, Name = "max_quest_level")]
	public uint MaxQuestLevel
	{
		get
		{
			return __pbn__MaxQuestLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxQuestLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDataType()
	{
		return __pbn__DataType.HasValue;
	}

	public void ResetDataType()
	{
		__pbn__DataType = null;
	}

	public bool ShouldSerializeNextRefreshTime()
	{
		return __pbn__NextRefreshTime.HasValue;
	}

	public void ResetNextRefreshTime()
	{
		__pbn__NextRefreshTime = null;
	}

	public bool ShouldSerializeGlobalRandomSeed()
	{
		return __pbn__GlobalRandomSeed.HasValue;
	}

	public void ResetGlobalRandomSeed()
	{
		__pbn__GlobalRandomSeed = null;
	}

	public bool ShouldSerializeCloseTime()
	{
		return __pbn__CloseTime.HasValue;
	}

	public void ResetCloseTime()
	{
		__pbn__CloseTime = null;
	}

	public bool ShouldSerializeQuestThemeIndex()
	{
		return __pbn__QuestThemeIndex.HasValue;
	}

	public void ResetQuestThemeIndex()
	{
		__pbn__QuestThemeIndex = null;
	}

	public bool ShouldSerializeQuestLevel()
	{
		return __pbn__QuestLevel.HasValue;
	}

	public void ResetQuestLevel()
	{
		__pbn__QuestLevel = null;
	}

	public bool ShouldSerializeQuestStar()
	{
		return __pbn__QuestStar.HasValue;
	}

	public void ResetQuestStar()
	{
		__pbn__QuestStar = null;
	}

	public bool ShouldSerializeMaxQuestLevel()
	{
		return __pbn__MaxQuestLevel.HasValue;
	}

	public void ResetMaxQuestLevel()
	{
		__pbn__MaxQuestLevel = null;
	}
}
