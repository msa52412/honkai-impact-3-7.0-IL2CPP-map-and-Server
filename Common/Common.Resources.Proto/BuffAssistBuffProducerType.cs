using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum BuffAssistBuffProducerType
{
	[ProtoEnum(Name = "BUFF_ASSIST_BUFF_PRODUCER_PLAYER")]
	BuffAssistBuffProducerPlayer = 1,
	[ProtoEnum(Name = "BUFF_ASSIST_BUFF_PRODUCER_NPC")]
	BuffAssistBuffProducerNpc,
	[ProtoEnum(Name = "BUFF_ASSIST_BUFF_PRODUCER_ROBOT")]
	BuffAssistBuffProducerRobot
}
