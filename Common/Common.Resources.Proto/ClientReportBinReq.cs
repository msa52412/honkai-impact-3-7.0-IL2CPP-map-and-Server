using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ClientReportBinReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5012
	}

	private IExtension __pbn__extensionData;

	private ClientReportBinType? __pbn__ReportType;

	[ProtoMember(1, Name = "report_type")]
	[DefaultValue(ClientReportBinType.ClientReportBinInvalid)]
	public ClientReportBinType ReportType
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

	[ProtoMember(2, Name = "report_bin_list")]
	public List<byte[]> ReportBinLists { get; } = new List<byte[]>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReportType()
	{
		return __pbn__ReportType.HasValue;
	}

	public void ResetReportType()
	{
		__pbn__ReportType = null;
	}
}
