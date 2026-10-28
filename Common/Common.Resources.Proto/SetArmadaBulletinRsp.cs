using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetArmadaBulletinRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2616
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_GRANTED")]
		NotGranted,
		[ProtoEnum(Name = "FORMAT_ERROR")]
		FormatError,
		[ProtoEnum(Name = "SENSITIVE_WORDS")]
		SensitiveWords,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada,
		[ProtoEnum(Name = "IN_CD")]
		InCd,
		[ProtoEnum(Name = "IN_AUDIT")]
		InAudit
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Bulletin;

	private string __pbn__OldBulletin;

	private string __pbn__AuditBulletin;

	private uint? __pbn__CdEndTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "bulletin")]
	[DefaultValue("")]
	public string Bulletin
	{
		get
		{
			return __pbn__Bulletin ?? "";
		}
		set
		{
			__pbn__Bulletin = value;
		}
	}

	[ProtoMember(3, Name = "old_bulletin")]
	[DefaultValue("")]
	public string OldBulletin
	{
		get
		{
			return __pbn__OldBulletin ?? "";
		}
		set
		{
			__pbn__OldBulletin = value;
		}
	}

	[ProtoMember(4, Name = "audit_bulletin")]
	[DefaultValue("")]
	public string AuditBulletin
	{
		get
		{
			return __pbn__AuditBulletin ?? "";
		}
		set
		{
			__pbn__AuditBulletin = value;
		}
	}

	[ProtoMember(5, Name = "cd_end_time")]
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

	public bool ShouldSerializeBulletin()
	{
		return __pbn__Bulletin != null;
	}

	public void ResetBulletin()
	{
		__pbn__Bulletin = null;
	}

	public bool ShouldSerializeOldBulletin()
	{
		return __pbn__OldBulletin != null;
	}

	public void ResetOldBulletin()
	{
		__pbn__OldBulletin = null;
	}

	public bool ShouldSerializeAuditBulletin()
	{
		return __pbn__AuditBulletin != null;
	}

	public void ResetAuditBulletin()
	{
		__pbn__AuditBulletin = null;
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
