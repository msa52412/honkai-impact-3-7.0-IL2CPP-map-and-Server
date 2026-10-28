using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldBeastAwardType
{
	[ProtoEnum(Name = "CHATWORLD_BEAST_AWARD_HP_RATIO")]
	ChatworldBeastAwardHpRatio = 1,
	[ProtoEnum(Name = "CHATWORLD_BEAST_AWARD_SCORE")]
	ChatworldBeastAwardScore
}
