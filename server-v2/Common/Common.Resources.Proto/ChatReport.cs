using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatReport : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private uint? __pbn__ReportType;

	private string __pbn__ReportDetail;

	private uint? __pbn__ReporterUid;

	[ProtoMember(1, Name = "target_uid")]
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

	[ProtoMember(2, Name = "report_type")]
	public uint ReportType
	{
		get
		{
			return __pbn__ReportType.GetValueOrDefault();
		}
		set
		{
			__pbn__ReportType = value;
		}
	}

	[ProtoMember(3, Name = "report_detail")]
	[DefaultValue("")]
	public string ReportDetail
	{
		get
		{
			return __pbn__ReportDetail ?? "";
		}
		set
		{
			__pbn__ReportDetail = value;
		}
	}

	[ProtoMember(4, Name = "reporter_uid")]
	public uint ReporterUid
	{
		get
		{
			return __pbn__ReporterUid.GetValueOrDefault();
		}
		set
		{
			__pbn__ReporterUid = value;
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

	public bool ShouldSerializeReportType()
	{
		return __pbn__ReportType.HasValue;
	}

	public void ResetReportType()
	{
		__pbn__ReportType = null;
	}

	public bool ShouldSerializeReportDetail()
	{
		return __pbn__ReportDetail != null;
	}

	public void ResetReportDetail()
	{
		__pbn__ReportDetail = null;
	}

	public bool ShouldSerializeReporterUid()
	{
		return __pbn__ReporterUid.HasValue;
	}

	public void ResetReporterUid()
	{
		__pbn__ReporterUid = null;
	}
}
