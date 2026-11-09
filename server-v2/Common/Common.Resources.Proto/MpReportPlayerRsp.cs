using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpReportPlayerRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 382
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_TEAM")]
		NotInTeam,
		[ProtoEnum(Name = "ALREADY_REPORT")]
		AlreadyReport
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private bool? __pbn__ReportSuccess;

	private uint? __pbn__JudgeTerm;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	[ProtoMember(3, Name = "report_success")]
	public bool ReportSuccess
	{
		get
		{
			return __pbn__ReportSuccess.GetValueOrDefault();
		}
		set
		{
			__pbn__ReportSuccess = value;
		}
	}

	[ProtoMember(4, Name = "judge_term")]
	public uint JudgeTerm
	{
		get
		{
			return __pbn__JudgeTerm.GetValueOrDefault();
		}
		set
		{
			__pbn__JudgeTerm = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializeReportSuccess()
	{
		return __pbn__ReportSuccess.HasValue;
	}

	public void ResetReportSuccess()
	{
		__pbn__ReportSuccess = null;
	}

	public bool ShouldSerializeJudgeTerm()
	{
		return __pbn__JudgeTerm.HasValue;
	}

	public void ResetJudgeTerm()
	{
		__pbn__JudgeTerm = null;
	}
}
