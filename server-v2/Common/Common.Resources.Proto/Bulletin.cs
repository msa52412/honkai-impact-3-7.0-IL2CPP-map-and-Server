using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Bulletin : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__Type;

	private uint? __pbn__Weight;

	private string __pbn__TitleButton;

	private string __pbn__Title;

	private string __pbn__BannerPath;

	private string __pbn__EventDateStr;

	private string __pbn__Content;

	private uint? __pbn__Mark;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__UpdateTime;

	private uint? __pbn__ShowConfigId;

	private uint? __pbn__ClientReqType;

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

	[ProtoMember(2, Name = "type")]
	public uint Type
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

	[ProtoMember(3, Name = "weight")]
	public uint Weight
	{
		get
		{
			return __pbn__Weight.GetValueOrDefault();
		}
		set
		{
			__pbn__Weight = value;
		}
	}

	[ProtoMember(4, Name = "title_button")]
	[DefaultValue("")]
	public string TitleButton
	{
		get
		{
			return __pbn__TitleButton ?? "";
		}
		set
		{
			__pbn__TitleButton = value;
		}
	}

	[ProtoMember(5, Name = "title")]
	[DefaultValue("")]
	public string Title
	{
		get
		{
			return __pbn__Title ?? "";
		}
		set
		{
			__pbn__Title = value;
		}
	}

	[ProtoMember(6, Name = "banner_path")]
	[DefaultValue("")]
	public string BannerPath
	{
		get
		{
			return __pbn__BannerPath ?? "";
		}
		set
		{
			__pbn__BannerPath = value;
		}
	}

	[ProtoMember(7, Name = "event_date_str")]
	[DefaultValue("")]
	public string EventDateStr
	{
		get
		{
			return __pbn__EventDateStr ?? "";
		}
		set
		{
			__pbn__EventDateStr = value;
		}
	}

	[ProtoMember(8, Name = "content")]
	[DefaultValue("")]
	public string Content
	{
		get
		{
			return __pbn__Content ?? "";
		}
		set
		{
			__pbn__Content = value;
		}
	}

	[ProtoMember(9, Name = "mark")]
	public uint Mark
	{
		get
		{
			return __pbn__Mark.GetValueOrDefault();
		}
		set
		{
			__pbn__Mark = value;
		}
	}

	[ProtoMember(10, Name = "begin_time")]
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

	[ProtoMember(11, Name = "end_time")]
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

	[ProtoMember(12, Name = "update_time")]
	public uint UpdateTime
	{
		get
		{
			return __pbn__UpdateTime.GetValueOrDefault();
		}
		set
		{
			__pbn__UpdateTime = value;
		}
	}

	[ProtoMember(13, Name = "show_config_id")]
	public uint ShowConfigId
	{
		get
		{
			return __pbn__ShowConfigId.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowConfigId = value;
		}
	}

	[ProtoMember(14, Name = "client_req_type")]
	public uint ClientReqType
	{
		get
		{
			return __pbn__ClientReqType.GetValueOrDefault();
		}
		set
		{
			__pbn__ClientReqType = value;
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

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeWeight()
	{
		return __pbn__Weight.HasValue;
	}

	public void ResetWeight()
	{
		__pbn__Weight = null;
	}

	public bool ShouldSerializeTitleButton()
	{
		return __pbn__TitleButton != null;
	}

	public void ResetTitleButton()
	{
		__pbn__TitleButton = null;
	}

	public bool ShouldSerializeTitle()
	{
		return __pbn__Title != null;
	}

	public void ResetTitle()
	{
		__pbn__Title = null;
	}

	public bool ShouldSerializeBannerPath()
	{
		return __pbn__BannerPath != null;
	}

	public void ResetBannerPath()
	{
		__pbn__BannerPath = null;
	}

	public bool ShouldSerializeEventDateStr()
	{
		return __pbn__EventDateStr != null;
	}

	public void ResetEventDateStr()
	{
		__pbn__EventDateStr = null;
	}

	public bool ShouldSerializeContent()
	{
		return __pbn__Content != null;
	}

	public void ResetContent()
	{
		__pbn__Content = null;
	}

	public bool ShouldSerializeMark()
	{
		return __pbn__Mark.HasValue;
	}

	public void ResetMark()
	{
		__pbn__Mark = null;
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

	public bool ShouldSerializeUpdateTime()
	{
		return __pbn__UpdateTime.HasValue;
	}

	public void ResetUpdateTime()
	{
		__pbn__UpdateTime = null;
	}

	public bool ShouldSerializeShowConfigId()
	{
		return __pbn__ShowConfigId.HasValue;
	}

	public void ResetShowConfigId()
	{
		__pbn__ShowConfigId = null;
	}

	public bool ShouldSerializeClientReqType()
	{
		return __pbn__ClientReqType.HasValue;
	}

	public void ResetClientReqType()
	{
		__pbn__ClientReqType = null;
	}
}
