using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QTEMapEndStatus
{
	[ProtoEnum(Name = "QTE_MAP_END_STATUS_SUCC")]
	QteMapEndStatusSucc,
	[ProtoEnum(Name = "QTE_MAP_END_STATUS_EXIT")]
	QteMapEndStatusExit,
	[ProtoEnum(Name = "QTE_MAP_END_STATUS_EXHAUSTED")]
	QteMapEndStatusExhausted
}
