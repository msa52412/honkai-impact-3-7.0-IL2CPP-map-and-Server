using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtBattleClientReportNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3358
	}

	private IExtension __pbn__extensionData;

	private string __pbn__ReportType;

	private string __pbn__ReportValue;

	[ProtoMember(1, Name = "report_type")]
	[DefaultValue("")]
	public string ReportType
	{
		get
		{
			return __pbn__ReportType ?? "";
		}
		set
		{
			__pbn__ReportType = value;
		}
	}

	[ProtoMember(2, Name = "report_value")]
	[DefaultValue("")]
	public string ReportValue
	{
		get
		{
			return __pbn__ReportValue ?? "";
		}
		set
		{
			__pbn__ReportValue = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReportType()
	{
		return __pbn__ReportType != null;
	}

	public void ResetReportType()
	{
		__pbn__ReportType = null;
	}

	public bool ShouldSerializeReportValue()
	{
		return __pbn__ReportValue != null;
	}

	public void ResetReportValue()
	{
		__pbn__ReportValue = null;
	}
}
