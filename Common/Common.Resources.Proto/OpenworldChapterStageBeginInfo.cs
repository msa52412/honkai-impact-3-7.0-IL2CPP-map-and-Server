using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterStageBeginInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__LocationId;

	private uint? __pbn__StoryMode;

	private uint? __pbn__StorySerial;

	private bool? __pbn__IsQuickStart;

	[ProtoMember(1, Name = "location_id")]
	public uint LocationId
	{
		get
		{
			return __pbn__LocationId.GetValueOrDefault();
		}
		set
		{
			__pbn__LocationId = value;
		}
	}

	[ProtoMember(2, Name = "story_mode")]
	public uint StoryMode
	{
		get
		{
			return __pbn__StoryMode.GetValueOrDefault();
		}
		set
		{
			__pbn__StoryMode = value;
		}
	}

	[ProtoMember(3, Name = "story_serial")]
	public uint StorySerial
	{
		get
		{
			return __pbn__StorySerial.GetValueOrDefault();
		}
		set
		{
			__pbn__StorySerial = value;
		}
	}

	[ProtoMember(4, Name = "is_quick_start")]
	public bool IsQuickStart
	{
		get
		{
			return __pbn__IsQuickStart.GetValueOrDefault();
		}
		set
		{
			__pbn__IsQuickStart = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLocationId()
	{
		return __pbn__LocationId.HasValue;
	}

	public void ResetLocationId()
	{
		__pbn__LocationId = null;
	}

	public bool ShouldSerializeStoryMode()
	{
		return __pbn__StoryMode.HasValue;
	}

	public void ResetStoryMode()
	{
		__pbn__StoryMode = null;
	}

	public bool ShouldSerializeStorySerial()
	{
		return __pbn__StorySerial.HasValue;
	}

	public void ResetStorySerial()
	{
		__pbn__StorySerial = null;
	}

	public bool ShouldSerializeIsQuickStart()
	{
		return __pbn__IsQuickStart.HasValue;
	}

	public void ResetIsQuickStart()
	{
		__pbn__IsQuickStart = null;
	}
}
