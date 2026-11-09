using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetOpenworldStoryProgressReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4454
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StoryId;

	private uint? __pbn__StoryProgress;

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
}
