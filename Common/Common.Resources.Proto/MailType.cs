using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MailType
{
	[ProtoEnum(Name = "MAIL_INNER")]
	MailInner = 1,
	[ProtoEnum(Name = "MAIL_OUTER")]
	MailOuter,
	[ProtoEnum(Name = "MAIL_NOTICE")]
	MailNotice
}
