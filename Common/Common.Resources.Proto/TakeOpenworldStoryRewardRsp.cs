using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeOpenworldStoryRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4457
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "STORY_NOT_FOUND")]
		StoryNotFound
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StoryId;

	private bool? __pbn__IsAcceptFollowStory;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(3, Name = "story_id")]
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

	[ProtoMember(4, Name = "accept_story_id_list")]
	public uint[] AcceptStoryIdLists { get; set; }

	[ProtoMember(5, Name = "is_accept_follow_story")]
	public bool IsAcceptFollowStory
	{
		get
		{
			return __pbn__IsAcceptFollowStory.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAcceptFollowStory = value;
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

	public bool ShouldSerializeIsAcceptFollowStory()
	{
		return __pbn__IsAcceptFollowStory.HasValue;
	}

	public void ResetIsAcceptFollowStory()
	{
		__pbn__IsAcceptFollowStory = null;
	}
}
