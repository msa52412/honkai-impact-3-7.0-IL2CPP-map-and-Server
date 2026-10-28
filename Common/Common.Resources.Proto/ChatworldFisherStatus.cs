using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldFisherStatus
{
	[ProtoEnum(Name = "CHATWORLD_FISHER_STATUS_NONE")]
	ChatworldFisherStatusNone,
	[ProtoEnum(Name = "CHATWORLD_FISHER_STATUS_PREPARE")]
	ChatworldFisherStatusPrepare,
	[ProtoEnum(Name = "CHATWORLD_FISHER_STATUS_START")]
	ChatworldFisherStatusStart,
	[ProtoEnum(Name = "CHATWORLD_FISHER_STATUS_END")]
	ChatworldFisherStatusEnd,
	[ProtoEnum(Name = "CHATWORLD_FISHER_STATUS_WAIT_ASSIST")]
	ChatworldFisherStatusWaitAssist
}
