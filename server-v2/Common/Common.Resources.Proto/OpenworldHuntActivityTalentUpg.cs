using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldHuntActivityTalentUpgradeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4606
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__TalentId;

	private string __pbn__ReportExt;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "talent_id")]
	public uint TalentId
	{
		get
		{
			return __pbn__TalentId.GetValueOrDefault();
		}
		set
		{
			__pbn__TalentId = value;
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

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeTalentId()
	{
		return __pbn__TalentId.HasValue;
	}

	public void ResetTalentId()
	{
		__pbn__TalentId = null;
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
[ProtoContract]
public class OpenworldHuntActivityTalentUpgradeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4607
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen,
		[ProtoEnum(Name = "TALENT_LEVEL_ERROR")]
		TalentLevelError,
		[ProtoEnum(Name = "PRE_TALENT_LACK")]
		PreTalentLack,
		[ProtoEnum(Name = "MAP_NOT_OPEN")]
		MapNotOpen,
		[ProtoEnum(Name = "MAP_PROGRESS_LACK")]
		MapProgressLack,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
