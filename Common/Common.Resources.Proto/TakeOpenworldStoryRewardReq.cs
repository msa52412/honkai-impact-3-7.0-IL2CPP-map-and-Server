using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeOpenworldStoryRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4456
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StoryId;

	private bool? __pbn__IsAcceptFollowStory;

	private string __pbn__ReportExt;

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

	[ProtoMember(2, Name = "is_accept_follow_story")]
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

	[ProtoMember(3, Name = "report_ext")]
	[DefaultValue("")]
	public string ReportExt
	{
		get
		{
			return __pbn__ReportExt ?? "";
		}
		set
		{
			__pbn__ReportExt = value;
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

	public bool ShouldSerializeReportExt()
	{
		return __pbn__ReportExt != null;
	}

	public void ResetReportExt()
	{
		__pbn__ReportExt = null;
	}
}
