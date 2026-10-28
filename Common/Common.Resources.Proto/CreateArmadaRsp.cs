using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CreateArmadaRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2604
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "SCOIN_LACK")]
		ScoinLack,
		[ProtoEnum(Name = "HCOIN_LACK")]
		HcoinLack,
		[ProtoEnum(Name = "IN_ARMADA")]
		InArmada,
		[ProtoEnum(Name = "IN_APPLYING")]
		InApplying,
		[ProtoEnum(Name = "NAME_REPEAT")]
		NameRepeat,
		[ProtoEnum(Name = "NAME_FORMAT_ERROR")]
		NameFormatError,
		[ProtoEnum(Name = "NAME_SENSITIVE_WORDS")]
		NameSensitiveWords,
		[ProtoEnum(Name = "IN_CD_TIME")]
		InCdTime,
		[ProtoEnum(Name = "ACCOUNT_NONE")]
		AccountNone,
		[ProtoEnum(Name = "IN_LOW_POPL_CD_TIME")]
		InLowPoplCdTime,
		[ProtoEnum(Name = "LABEL_ERROR")]
		LabelError,
		[ProtoEnum(Name = "IN_AUDIT")]
		InAudit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ArmadaId;

	private string __pbn__ArmadaName;

	private string __pbn__AuditArmadaName;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "armada_id")]
	public uint ArmadaId
	{
		get
		{
			return __pbn__ArmadaId.GetValueOrDefault();
		}
		set
		{
			__pbn__ArmadaId = value;
		}
	}

	[ProtoMember(3, Name = "armada_name")]
	[DefaultValue("")]
	public string ArmadaName
	{
		get
		{
			return __pbn__ArmadaName ?? "";
		}
		set
		{
			__pbn__ArmadaName = value;
		}
	}

	[ProtoMember(4, Name = "audit_armada_name")]
	[DefaultValue("")]
	public string AuditArmadaName
	{
		get
		{
			return __pbn__AuditArmadaName ?? "";
		}
		set
		{
			__pbn__AuditArmadaName = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeArmadaId()
	{
		return __pbn__ArmadaId.HasValue;
	}

	public void ResetArmadaId()
	{
		__pbn__ArmadaId = null;
	}

	public bool ShouldSerializeArmadaName()
	{
		return __pbn__ArmadaName != null;
	}

	public void ResetArmadaName()
	{
		__pbn__ArmadaName = null;
	}

	public bool ShouldSerializeAuditArmadaName()
	{
		return __pbn__AuditArmadaName != null;
	}

	public void ResetAuditArmadaName()
	{
		__pbn__AuditArmadaName = null;
	}
}
