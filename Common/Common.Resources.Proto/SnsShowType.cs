using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SnsShowType
{
	[ProtoEnum(Name = "SNS_NONE")]
	SnsNone,
	[ProtoEnum(Name = "SNS_MAIN")]
	SnsMain,
	[ProtoEnum(Name = "SNS_WAR_HISTORY")]
	SnsWarHistory,
	[ProtoEnum(Name = "SNS_WIKI")]
	SnsWiki,
	[ProtoEnum(Name = "SNS_MSG_CARD")]
	SnsMsgCard
}
