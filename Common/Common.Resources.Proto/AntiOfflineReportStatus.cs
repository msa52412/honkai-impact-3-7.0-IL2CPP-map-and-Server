using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AntiOfflineReportStatus
{
	[ProtoEnum(Name = "ANTI_OFFLINE_REPORT_CORRECT")]
	AntiOfflineReportCorrect,
	[ProtoEnum(Name = "ANTI_OFFLINE_REPORT_ERROR")]
	AntiOfflineReportError,
	[ProtoEnum(Name = "ANTI_OFFLINE_REPORT_TIMEOUT")]
	AntiOfflineReportTimeout,
	[ProtoEnum(Name = "ANTI_OFFLINE_REPORT_SOON_LOGOUT")]
	AntiOfflineReportSoonLogout,
	[ProtoEnum(Name = "ANTI_OFFLINE_REPORT_UNEXPECTED")]
	AntiOfflineReportUnexpected
}
