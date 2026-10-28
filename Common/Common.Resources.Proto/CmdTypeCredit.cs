using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeCredit
{
	[ProtoEnum(Name = "CMD_CREDIT_REPORT_DATA_REQ")]
	CmdCreditReportDataReq = 6000,
	[ProtoEnum(Name = "CMD_CREDIT_REPORT_DATA_RSP")]
	CmdCreditReportDataRsp,
	[ProtoEnum(Name = "CMD_CREDIT_ACCOUNT_GET_REQ")]
	CmdCreditAccountGetReq,
	[ProtoEnum(Name = "CMD_CREDIT_ACCOUNT_GET_RSP")]
	CmdCreditAccountGetRsp
}
