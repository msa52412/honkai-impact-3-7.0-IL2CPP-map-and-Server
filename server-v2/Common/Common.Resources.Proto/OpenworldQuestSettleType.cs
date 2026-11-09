using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldQuestSettleType
{
	[ProtoEnum(Name = "OPENWORLD_QUEST_SETTLE_DEFAULT")]
	OpenworldQuestSettleDefault,
	[ProtoEnum(Name = "OPENWORLD_QUEST_SETTLE_CHALLENGE")]
	OpenworldQuestSettleChallenge
}
