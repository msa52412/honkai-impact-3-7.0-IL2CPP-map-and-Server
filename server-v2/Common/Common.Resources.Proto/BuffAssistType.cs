using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum BuffAssistType
{
	[ProtoEnum(Name = "BUFF_ASSIST_PRODUCER")]
	BuffAssistProducer = 1,
	[ProtoEnum(Name = "BUFF_ASSIST_CONSUMER")]
	BuffAssistConsumer
}
