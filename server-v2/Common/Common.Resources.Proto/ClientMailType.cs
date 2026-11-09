using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ClientMailType
{
	[ProtoEnum(Name = "CLIENT_MAIL_NONE")]
	ClientMailNone,
	[ProtoEnum(Name = "CLIENT_MAIL_MAIL")]
	ClientMailMail,
	[ProtoEnum(Name = "CLIENT_MAIL_NOTICE")]
	ClientMailNotice
}
