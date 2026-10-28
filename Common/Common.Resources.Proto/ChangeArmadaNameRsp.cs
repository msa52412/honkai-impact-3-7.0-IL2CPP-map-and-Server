using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChangeArmadaNameRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2692
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SCOIN_LACK")]
		ScoinLack,
		[ProtoEnum(Name = "HCOIN_LACK")]
		HcoinLack,
		[ProtoEnum(Name = "NAME_REPEAT")]
		NameRepeat,
		[ProtoEnum(Name = "NAME_FORMAT_ERROR")]
		NameFormatError,
		[ProtoEnum(Name = "NAME_SENSITIVE_WORDS")]
		NameSensitiveWords,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada,
		[ProtoEnum(Name = "POSITION_LIMIT")]
		PositionLimit,
		[ProtoEnum(Name = "IN_CD")]
		InCd,
		[ProtoEnum(Name = "IN_AUDIT")]
		InAudit
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Name;

	private string __pbn__OldName;

	private string __pbn__AuditName;

	private bool? __pbn__IsChangeNameFree;

	private uint? __pbn__CdEndTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "name")]
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

	[ProtoMember(3, Name = "old_name")]
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

	[ProtoMember(4, Name = "audit_name")]
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

	[ProtoMember(5, Name = "is_change_name_free")]
	public bool IsChangeNameFree
	{
		get
		{
			return __pbn__IsChangeNameFree.GetValueOrDefault();
		}
		set
		{
			__pbn__IsChangeNameFree = value;
		}
	}

	[ProtoMember(6, Name = "cd_end_time")]
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

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}

	public bool ShouldSerializeOldName()
	{
		return __pbn__OldName != null;
	}

	public void ResetOldName()
	{
		__pbn__OldName = null;
	}

	public bool ShouldSerializeAuditName()
	{
		return __pbn__AuditName != null;
	}

	public void ResetAuditName()
	{
		__pbn__AuditName = null;
	}

	public bool ShouldSerializeIsChangeNameFree()
	{
		return __pbn__IsChangeNameFree.HasValue;
	}

	public void ResetIsChangeNameFree()
	{
		__pbn__IsChangeNameFree = null;
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
