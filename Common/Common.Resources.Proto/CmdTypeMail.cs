using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeMail
{
	[ProtoEnum(Name = "CMD_GET_CLIENT_MAIL_DATA_REQ")]
	CmdGetClientMailDataReq = 3800,
	[ProtoEnum(Name = "CMD_GET_CLIENT_MAIL_DATA_RSP")]
	CmdGetClientMailDataRsp,
	[ProtoEnum(Name = "CMD_TAKE_CLIENT_MAIL_ATTACHMENT_REQ")]
	CmdTakeClientMailAttachmentReq,
	[ProtoEnum(Name = "CMD_TAKE_CLIENT_MAIL_ATTACHMENT_RSP")]
	CmdTakeClientMailAttachmentRsp,
	[ProtoEnum(Name = "CMD_MARK_CLIENT_MAIL_READ_REQ")]
	CmdMarkClientMailReadReq,
	[ProtoEnum(Name = "CMD_MARK_CLIENT_MAIL_READ_RSP")]
	CmdMarkClientMailReadRsp,
	[ProtoEnum(Name = "CMD_DEL_CLIENT_MAIL_REQ")]
	CmdDelClientMailReq,
	[ProtoEnum(Name = "CMD_DEL_CLIENT_MAIL_RSP")]
	CmdDelClientMailRsp,
	[ProtoEnum(Name = "CMD_NEW_CLIENT_MAIL_NOTIFY")]
	CmdNewClientMailNotify,
	[ProtoEnum(Name = "CMD_SET_CLIENT_MAIL_FAVORITE_REQ")]
	CmdSetClientMailFavoriteReq,
	[ProtoEnum(Name = "CMD_SET_CLIENT_MAIL_FAVORITE_RSP")]
	CmdSetClientMailFavoriteRsp
}
