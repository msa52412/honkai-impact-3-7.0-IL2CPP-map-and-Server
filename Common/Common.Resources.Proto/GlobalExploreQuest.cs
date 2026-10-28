using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalExploreQuest : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__QuestId;

	private uint? __pbn__Progress;

	private GlobalExploreQuestStatus? __pbn__Status;

	private uint? __pbn__FinishTimes;

	[ProtoMember(1, Name = "quest_id")]
	public uint QuestId
	{
		get
		{
			return __pbn__QuestId.GetValueOrDefault();
		}
		set
		{
			__pbn__QuestId = value;
		}
	}

	[ProtoMember(2, Name = "progress")]
	public uint Progress
	{
		get
		{
			return __pbn__Progress.GetValueOrDefault();
		}
		set
		{
			__pbn__Progress = value;
		}
	}

	[ProtoMember(3, Name = "status")]
	[DefaultValue(GlobalExploreQuestStatus.GlobalExploreQuestDoing)]
	public GlobalExploreQuestStatus Status
	{
		get
		{
			return __pbn__Status ?? GlobalExploreQuestStatus.GlobalExploreQuestDoing;
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(4, Name = "finish_times")]
	public uint FinishTimes
	{
		get
		{
			return __pbn__FinishTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeQuestId()
	{
		return __pbn__QuestId.HasValue;
	}

	public void ResetQuestId()
	{
		__pbn__QuestId = null;
	}

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeFinishTimes()
	{
		return __pbn__FinishTimes.HasValue;
	}

	public void ResetFinishTimes()
	{
		__pbn__FinishTimes = null;
	}
}
