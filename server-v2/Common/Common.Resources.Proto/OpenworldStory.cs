using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldStory : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StoryId;

	private uint? __pbn__StoryProgress;

	private uint? __pbn__AcceptTime;

	[ProtoMember(1, Name = "story_id")]
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

	[ProtoMember(2, Name = "story_progress")]
	public uint StoryProgress
	{
		get
		{
			return __pbn__StoryProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__StoryProgress = value;
		}
	}

	[ProtoMember(3, Name = "accept_time")]
	public uint AcceptTime
	{
		get
		{
			return __pbn__AcceptTime.GetValueOrDefault();
		}
		set
		{
			__pbn__AcceptTime = value;
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

	public bool ShouldSerializeStoryProgress()
	{
		return __pbn__StoryProgress.HasValue;
	}

	public void ResetStoryProgress()
	{
		__pbn__StoryProgress = null;
	}

	public bool ShouldSerializeAcceptTime()
	{
		return __pbn__AcceptTime.HasValue;
	}

	public void ResetAcceptTime()
	{
		__pbn__AcceptTime = null;
	}
}
