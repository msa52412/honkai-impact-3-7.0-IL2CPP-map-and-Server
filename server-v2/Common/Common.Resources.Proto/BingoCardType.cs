using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum BingoCardType
{
	[ProtoEnum(Name = "BINGO_CARD_WRONG")]
	BingoCardWrong,
	[ProtoEnum(Name = "BINGO_CARD_CORRECT")]
	BingoCardCorrect
}
