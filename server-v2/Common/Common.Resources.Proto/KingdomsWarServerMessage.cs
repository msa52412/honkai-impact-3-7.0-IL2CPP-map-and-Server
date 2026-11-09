using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomsWarServerMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	private bool? __pbn__PlayedStartStory;

	private long? __pbn__StoryPlayRecord;

	[ProtoMember(1, Name = "played_start_story")]
	public bool PlayedStartStory
	{
		get
		{
			return __pbn__PlayedStartStory.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayedStartStory = value;
		}
	}

	[ProtoMember(2, Name = "story_play_record")]
	public long StoryPlayRecord
	{
		get
		{
			return __pbn__StoryPlayRecord.GetValueOrDefault();
		}
		set
		{
			__pbn__StoryPlayRecord = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePlayedStartStory()
	{
		return __pbn__PlayedStartStory.HasValue;
	}

	public void ResetPlayedStartStory()
	{
		__pbn__PlayedStartStory = null;
	}

	public bool ShouldSerializeStoryPlayRecord()
	{
		return __pbn__StoryPlayRecord.HasValue;
	}

	public void ResetStoryPlayRecord()
	{
		__pbn__StoryPlayRecord = null;
	}
}
