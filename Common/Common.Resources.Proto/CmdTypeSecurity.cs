using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeSecurity
{
	[ProtoEnum(Name = "CMD_UPDATE_CLIENT_SETTING_NOTIFY")]
	CmdUpdateClientSettingNotify = 5002,
	[ProtoEnum(Name = "CMD_COMMON_CD_CHECK_NOTIFY")]
	CmdCommonCdCheckNotify = 5003,
	[ProtoEnum(Name = "CMD_ANTI_ADDICT_NOTIFY")]
	CmdAntiAddictNotify = 5004,
	[ProtoEnum(Name = "CMD_SENSITIVE_WORD_CHECK_REQ")]
	CmdSensitiveWordCheckReq = 5005,
	[ProtoEnum(Name = "CMD_SENSITIVE_WORD_CHECK_RSP")]
	CmdSensitiveWordCheckRsp = 5006,
	[ProtoEnum(Name = "CMD_KICK_OUT_PLAYER_NOTIFY")]
	CmdKickOutPlayerNotify = 5007,
	[ProtoEnum(Name = "CMD_CLIENT_REPORT_REQ")]
	CmdClientReportReq = 5008,
	[ProtoEnum(Name = "CMD_CLIENT_REPORT_RSP")]
	CmdClientReportRsp = 5009,
	[ProtoEnum(Name = "CMD_GET_AUTHKEY_REQ")]
	CmdGetAuthkeyReq = 5010,
	[ProtoEnum(Name = "CMD_GET_AUTHKEY_RSP")]
	CmdGetAuthkeyRsp = 5011,
	[ProtoEnum(Name = "CMD_CLIENT_REPORT_BIN_REQ")]
	CmdClientReportBinReq = 5012,
	[ProtoEnum(Name = "CMD_CLIENT_REPORT_BIN_RSP")]
	CmdClientReportBinRsp = 5013,
	[ProtoEnum(Name = "CMD_GET_SECURITY_PASSWORD_REQ")]
	CmdGetSecurityPasswordReq = 5021,
	[ProtoEnum(Name = "CMD_GET_SECURITY_PASSWORD_RSP")]
	CmdGetSecurityPasswordRsp = 5022,
	[ProtoEnum(Name = "CMD_SET_SECURITY_PASSWORD_REQ")]
	CmdSetSecurityPasswordReq = 5023,
	[ProtoEnum(Name = "CMD_SET_SECURITY_PASSWORD_RSP")]
	CmdSetSecurityPasswordRsp = 5024,
	[ProtoEnum(Name = "CMD_CHANGE_SECURITY_PASSWORD_REQ")]
	CmdChangeSecurityPasswordReq = 5025,
	[ProtoEnum(Name = "CMD_CHANGE_SECURITY_PASSWORD_RSP")]
	CmdChangeSecurityPasswordRsp = 5026,
	[ProtoEnum(Name = "CMD_UNLOCK_DEVICE_SECURITY_PASSWORD_REQ")]
	CmdUnlockDeviceSecurityPasswordReq = 5027,
	[ProtoEnum(Name = "CMD_UNLOCK_DEVICE_SECURITY_PASSWORD_RSP")]
	CmdUnlockDeviceSecurityPasswordRsp = 5028,
	[ProtoEnum(Name = "CMD_SECURITY_PASSWORD_OP_REQ")]
	CmdSecurityPasswordOpReq = 5029,
	[ProtoEnum(Name = "CMD_SECURITY_PASSWORD_OP_RSP")]
	CmdSecurityPasswordOpRsp = 5030,
	[ProtoEnum(Name = "CMD_RESET_SECURITY_PASSWORD_REQ")]
	CmdResetSecurityPasswordReq = 5031,
	[ProtoEnum(Name = "CMD_RESET_SECURITY_PASSWORD_RSP")]
	CmdResetSecurityPasswordRsp = 5032,
	[ProtoEnum(Name = "CMD_SECURITY_PASSWORD_REJECTION_NOTIFY")]
	CmdSecurityPasswordRejectionNotify = 5033
}
