using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AcceptOpenworldStoryRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4453
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "CYCLE_NOT_MATCH")]
		CycleNotMatch,
		[ProtoEnum(Name = "PRE_STORY_NOT_FINISH")]
		PreStoryNotFinish,
		[ProtoEnum(Name = "STORY_NOT_BEGIN")]
		StoryNotBegin,
		[ProtoEnum(Name = "QUEST_LEVEL_LIMIT")]
		QuestLevelLimit,
		[ProtoEnum(Name = "STORY_DOING")]
		StoryDoing,
		[ProtoEnum(Name = "STORY_DONE")]
		StoryDone,
		[ProtoEnum(Name = "TECH_LEVEL_LIMIT")]
		TechLevelLimit,
		[ProtoEnum(Name = "CONDITION_NOT_MEET")]
		ConditionNotMeet
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StoryId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "story_id")]
	public uint StoryId
	{
		get
		{
			return __pbn__StoryId.GetValueOrDefault();
		}
		set
		{
			__pbn__StoryId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStoryId()
	{
		return __pbn__StoryId.HasValue;
	}

	public void ResetStoryId()
	{
		__pbn__StoryId = null;
	}
}
