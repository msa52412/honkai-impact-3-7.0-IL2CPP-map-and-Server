using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldEventSubType
{
	[ProtoEnum(Name = "OPENWORLD_EVENT_SUB_TREASURE")]
	OpenworldEventSubTreasure = 50
}
