using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetRedPointStatusNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1707
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ContentId;

	private uint? __pbn__RedPointType;

	private bool? __pbn__IsShow;

	[ProtoMember(1, Name = "content_id")]
	public uint ContentId
	{
		get
		{
			return __pbn__ContentId.GetValueOrDefault();
		}
		set
		{
			__pbn__ContentId = value;
		}
	}

	[ProtoMember(2, Name = "red_point_type")]
	public uint RedPointType
	{
		get
		{
			return __pbn__RedPointType.GetValueOrDefault();
		}
		set
		{
			__pbn__RedPointType = value;
		}
	}

	[ProtoMember(3, Name = "is_show")]
	public bool IsShow
	{
		get
		{
			return __pbn__IsShow.GetValueOrDefault();
		}
		set
		{
			__pbn__IsShow = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeContentId()
	{
		return __pbn__ContentId.HasValue;
	}

	public void ResetContentId()
	{
		__pbn__ContentId = null;
	}

	public bool ShouldSerializeRedPointType()
	{
		return __pbn__RedPointType.HasValue;
	}

	public void ResetRedPointType()
	{
		__pbn__RedPointType = null;
	}

	public bool ShouldSerializeIsShow()
	{
		return __pbn__IsShow.HasValue;
	}

	public void ResetIsShow()
	{
		__pbn__IsShow = null;
	}
}
