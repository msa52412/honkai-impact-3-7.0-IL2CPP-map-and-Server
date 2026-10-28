using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CreateChatgroupRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2201
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "CREATE_MAX")]
		CreateMax,
		[ProtoEnum(Name = "JOIN_MAX")]
		JoinMax,
		[ProtoEnum(Name = "SENSITIVE_WORD")]
		SensitiveWord,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "LABEL_ERROR")]
		LabelError,
		[ProtoEnum(Name = "NAME_FORMAT_ERROR")]
		NameFormatError,
		[ProtoEnum(Name = "NOTICE_FORMAT_ERROR")]
		NoticeFormatError,
		[ProtoEnum(Name = "FEATURE_LIMIT")]
		FeatureLimit,
		[ProtoEnum(Name = "IN_AUDIT")]
		InAudit
	}

	private IExtension __pbn__extensionData;

	private string __pbn__AuditName;

	private string __pbn__AuditNotice;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chatgroup")]
	public Chatgroup Chatgroup { get; set; }

	[ProtoMember(3, Name = "audit_name")]
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

	[ProtoMember(4, Name = "audit_notice")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
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
}
