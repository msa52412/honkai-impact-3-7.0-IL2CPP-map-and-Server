using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetChatgroupRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2207
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_CREATER")]
		NotCreater,
		[ProtoEnum(Name = "SENSITIVE_WORD")]
		SensitiveWord,
		[ProtoEnum(Name = "LABEL_ERROR")]
		LabelError,
		[ProtoEnum(Name = "NAME_FORMAT_ERROR")]
		NameFormatError,
		[ProtoEnum(Name = "NOTICE_FORMAT_ERROR")]
		NoticeFormatError,
		[ProtoEnum(Name = "FORBIDDEN")]
		Forbidden,
		[ProtoEnum(Name = "IN_CD")]
		InCd,
		[ProtoEnum(Name = "IN_AUDIT")]
		InAudit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatgroupId;

	private string __pbn__Name;

	private string __pbn__Notice;

	private uint? __pbn__IconId;

	private string __pbn__OldName;

	private string __pbn__OldNotice;

	private string __pbn__AuditName;

	private string __pbn__AuditNotice;

	private uint? __pbn__CdEndTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chatgroup_id")]
	public uint ChatgroupId
	{
		get
		{
			return __pbn__ChatgroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatgroupId = value;
		}
	}

	[ProtoMember(3, Name = "name")]
	[DefaultValue("")]
	public string Name
	{
		get
		{
			return __pbn__Name ?? "";
		}
		set
		{
			__pbn__Name = value;
		}
	}

	[ProtoMember(4, Name = "notice")]
	[DefaultValue("")]
	public string Notice
	{
		get
		{
			return __pbn__Notice ?? "";
		}
		set
		{
			__pbn__Notice = value;
		}
	}

	[ProtoMember(5, Name = "icon_id")]
	public uint IconId
	{
		get
		{
			return __pbn__IconId.GetValueOrDefault();
		}
		set
		{
			__pbn__IconId = value;
		}
	}

	[ProtoMember(6, Name = "label_id_list")]
	public uint[] LabelIdLists { get; set; }

	[ProtoMember(7, Name = "old_name")]
	[DefaultValue("")]
	public string OldName
	{
		get
		{
			return __pbn__OldName ?? "";
		}
		set
		{
			__pbn__OldName = value;
		}
	}

	[ProtoMember(8, Name = "old_notice")]
	[DefaultValue("")]
	public string OldNotice
	{
		get
		{
			return __pbn__OldNotice ?? "";
		}
		set
		{
			__pbn__OldNotice = value;
		}
	}

	[ProtoMember(9, Name = "audit_name")]
	[DefaultValue("")]
	public string AuditName
	{
		get
		{
			return __pbn__AuditName ?? "";
		}
		set
		{
			__pbn__AuditName = value;
		}
	}

	[ProtoMember(10, Name = "audit_notice")]
	[DefaultValue("")]
	public string AuditNotice
	{
		get
		{
			return __pbn__AuditNotice ?? "";
		}
		set
		{
			__pbn__AuditNotice = value;
		}
	}

	[ProtoMember(11, Name = "cd_end_time")]
	public uint CdEndTime
	{
		get
		{
			return __pbn__CdEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CdEndTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatgroupId()
	{
		return __pbn__ChatgroupId.HasValue;
	}

	public void ResetChatgroupId()
	{
		__pbn__ChatgroupId = null;
	}

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}

	public bool ShouldSerializeNotice()
	{
		return __pbn__Notice != null;
	}

	public void ResetNotice()
	{
		__pbn__Notice = null;
	}

	public bool ShouldSerializeIconId()
	{
		return __pbn__IconId.HasValue;
	}

	public void ResetIconId()
	{
		__pbn__IconId = null;
	}

	public bool ShouldSerializeOldName()
	{
		return __pbn__OldName != null;
	}

	public void ResetOldName()
	{
		__pbn__OldName = null;
	}

	public bool ShouldSerializeOldNotice()
	{
		return __pbn__OldNotice != null;
	}

	public void ResetOldNotice()
	{
		__pbn__OldNotice = null;
	}

	public bool ShouldSerializeAuditName()
	{
		return __pbn__AuditName != null;
	}

	public void ResetAuditName()
	{
		__pbn__AuditName = null;
	}

	public bool ShouldSerializeAuditNotice()
	{
		return __pbn__AuditNotice != null;
	}

	public void ResetAuditNotice()
	{
		__pbn__AuditNotice = null;
	}

	public bool ShouldSerializeCdEndTime()
	{
		return __pbn__CdEndTime.HasValue;
	}

	public void ResetCdEndTime()
	{
		__pbn__CdEndTime = null;
	}
}
