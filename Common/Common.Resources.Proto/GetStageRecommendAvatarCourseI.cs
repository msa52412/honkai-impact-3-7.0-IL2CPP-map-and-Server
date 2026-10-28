using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetStageRecommendAvatarCourseInfoReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1630
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Id;

	private AvatarRecommendType? __pbn__Type;

	private uint? __pbn__RecommendStyle;

	private uint? __pbn__Rank;

	[ProtoMember(1, Name = "id")]
	[DefaultValue("")]
	public string Id
	{
		get
		{
			return __pbn__Id ?? "";
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "type")]
	[DefaultValue(AvatarRecommendType.AvatarRecommendNone)]
	public AvatarRecommendType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(3, Name = "recommend_style")]
	public uint RecommendStyle
	{
		get
		{
			return __pbn__RecommendStyle.GetValueOrDefault();
		}
		set
		{
			__pbn__RecommendStyle = value;
		}
	}

	[ProtoMember(4, Name = "rank")]
	public uint Rank
	{
		get
		{
			return __pbn__Rank.GetValueOrDefault();
		}
		set
		{
			__pbn__Rank = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id != null;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeRecommendStyle()
	{
		return __pbn__RecommendStyle.HasValue;
	}

	public void ResetRecommendStyle()
	{
		__pbn__RecommendStyle = null;
	}

	public bool ShouldSerializeRank()
	{
		return __pbn__Rank.HasValue;
	}

	public void ResetRank()
	{
		__pbn__Rank = null;
	}
}
[ProtoContract]
public class GetStageRecommendAvatarCourseInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1631
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private string __pbn__CourseInfo;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "course_info")]
	[DefaultValue("")]
	public string CourseInfo
	{
		get
		{
			return __pbn__CourseInfo ?? "";
		}
		set
		{
			__pbn__CourseInfo = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCourseInfo()
	{
		return __pbn__CourseInfo != null;
	}

	public void ResetCourseInfo()
	{
		__pbn__CourseInfo = null;
	}
}
