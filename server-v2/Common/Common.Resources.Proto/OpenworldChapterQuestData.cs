using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterQuestData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__QuestAcceptTimes;

	private uint? __pbn__QuestRefreshTimes;

	private uint? __pbn__SpecialQuestFinishedNum;

	[ProtoMember(1, Name = "quest_list")]
	public List<OpenworldQuest> QuestLists { get; } = new List<OpenworldQuest>();


	[ProtoMember(2, Name = "quest_accept_times")]
	public uint QuestAcceptTimes
	{
		get
		{
			return __pbn__QuestAcceptTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__QuestAcceptTimes = value;
		}
	}

	[ProtoMember(3, Name = "quest_refresh_times")]
	public uint QuestRefreshTimes
	{
		get
		{
			return __pbn__QuestRefreshTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__QuestRefreshTimes = value;
		}
	}

	[ProtoMember(4, Name = "special_quest_finished_num")]
	public uint SpecialQuestFinishedNum
	{
		get
		{
			return __pbn__SpecialQuestFinishedNum.GetValueOrDefault();
		}
		set
		{
			__pbn__SpecialQuestFinishedNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeQuestAcceptTimes()
	{
		return __pbn__QuestAcceptTimes.HasValue;
	}

	public void ResetQuestAcceptTimes()
	{
		__pbn__QuestAcceptTimes = null;
	}

	public bool ShouldSerializeQuestRefreshTimes()
	{
		return __pbn__QuestRefreshTimes.HasValue;
	}

	public void ResetQuestRefreshTimes()
	{
		__pbn__QuestRefreshTimes = null;
	}

	public bool ShouldSerializeSpecialQuestFinishedNum()
	{
		return __pbn__SpecialQuestFinishedNum.HasValue;
	}

	public void ResetSpecialQuestFinishedNum()
	{
		__pbn__SpecialQuestFinishedNum = null;
	}
}
