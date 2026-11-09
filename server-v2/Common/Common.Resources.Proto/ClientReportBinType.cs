using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ClientReportBinType
{
	[ProtoEnum(Name = "CLIENT_REPORT_BIN_INVALID")]
	ClientReportBinInvalid,
	[ProtoEnum(Name = "CLIENT_REPORT_BIN_AVATAR_DETAIL")]
	ClientReportBinAvatarDetail,
	[ProtoEnum(Name = "CLIENT_REPORT_BIN_ATTACK_SNAPSHOT")]
	ClientReportBinAttackSnapshot
}
