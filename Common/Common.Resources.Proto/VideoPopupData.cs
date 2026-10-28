using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VideoPopupData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private string __pbn__MiddleImagePath;

	private string __pbn__VideoIcon;

	private string __pbn__VideoLink;

	private uint? __pbn__VideoLinkOpenType;

	private uint? __pbn__MaxTimes;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(3, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(4, Name = "middle_image_path")]
	[DefaultValue("")]
	public string MiddleImagePath
	{
		get
		{
			return __pbn__MiddleImagePath ?? "";
		}
		set
		{
			__pbn__MiddleImagePath = value;
		}
	}

	[ProtoMember(5, Name = "video_icon")]
	[DefaultValue("")]
	public string VideoIcon
	{
		get
		{
			return __pbn__VideoIcon ?? "";
		}
		set
		{
			__pbn__VideoIcon = value;
		}
	}

	[ProtoMember(6, Name = "video_link")]
	[DefaultValue("")]
	public string VideoLink
	{
		get
		{
			return __pbn__VideoLink ?? "";
		}
		set
		{
			__pbn__VideoLink = value;
		}
	}

	[ProtoMember(7, Name = "video_link_open_type")]
	public uint VideoLinkOpenType
	{
		get
		{
			return __pbn__VideoLinkOpenType.GetValueOrDefault();
		}
		set
		{
			__pbn__VideoLinkOpenType = value;
		}
	}

	[ProtoMember(8, Name = "max_times")]
	public uint MaxTimes
	{
		get
		{
			return __pbn__MaxTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeMiddleImagePath()
	{
		return __pbn__MiddleImagePath != null;
	}

	public void ResetMiddleImagePath()
	{
		__pbn__MiddleImagePath = null;
	}

	public bool ShouldSerializeVideoIcon()
	{
		return __pbn__VideoIcon != null;
	}

	public void ResetVideoIcon()
	{
		__pbn__VideoIcon = null;
	}

	public bool ShouldSerializeVideoLink()
	{
		return __pbn__VideoLink != null;
	}

	public void ResetVideoLink()
	{
		__pbn__VideoLink = null;
	}

	public bool ShouldSerializeVideoLinkOpenType()
	{
		return __pbn__VideoLinkOpenType.HasValue;
	}

	public void ResetVideoLinkOpenType()
	{
		__pbn__VideoLinkOpenType = null;
	}

	public bool ShouldSerializeMaxTimes()
	{
		return __pbn__MaxTimes.HasValue;
	}

	public void ResetMaxTimes()
	{
		__pbn__MaxTimes = null;
	}
}
