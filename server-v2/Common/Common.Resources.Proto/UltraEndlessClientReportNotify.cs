using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UltraEndlessClientReportNotify : IExtensible
{
	[ProtoContract]
	public enum ClientReportType
	{
		[ProtoEnum(Name = "CLIENT_REPORT_TYPE_NONE")]
		ClientReportTypeNone,
		[ProtoEnum(Name = "CLIENT_REPORT_TYPE_STAGE_BEGIN")]
		ClientReportTypeStageBegin
	}

	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5219
	}

	private IExtension __pbn__extensionData;

	private ClientReportType? __pbn__client_report_type;

	private string __pbn__ClientTransactionStr;

	private string __pbn__ReportMsg;

	[ProtoMember(1)]
	[DefaultValue(ClientReportType.ClientReportTypeNone)]
	public ClientReportType client_report_type
	{
		get
		{
			return __pbn__client_report_type.GetValueOrDefault();
		}
		set
		{
			__pbn__client_report_type = value;
		}
	}

	[ProtoMember(2, Name = "client_transaction_str")]
	[DefaultValue("")]
	public string ClientTransactionStr
	{
		get
		{
			return __pbn__ClientTransactionStr ?? "";
		}
		set
		{
			__pbn__ClientTransactionStr = value;
		}
	}

	[ProtoMember(3, Name = "report_msg")]
	[DefaultValue("")]
	public string ReportMsg
	{
		get
		{
			return __pbn__ReportMsg ?? "";
		}
		set
		{
			__pbn__ReportMsg = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeclient_report_type()
	{
		return __pbn__client_report_type.HasValue;
	}

	public void Resetclient_report_type()
	{
		__pbn__client_report_type = null;
	}

	public bool ShouldSerializeClientTransactionStr()
	{
		return __pbn__ClientTransactionStr != null;
	}

	public void ResetClientTransactionStr()
	{
		__pbn__ClientTransactionStr = null;
	}

	public bool ShouldSerializeReportMsg()
	{
		return __pbn__ReportMsg != null;
	}

	public void ResetReportMsg()
	{
		__pbn__ReportMsg = null;
	}
}
