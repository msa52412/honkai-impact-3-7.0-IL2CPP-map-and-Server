using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum StigmataType
{
	[ProtoEnum(Name = "STIGMATA_SLOT_1")]
	StigmataSlot1 = 1,
	[ProtoEnum(Name = "STIGMATA_SLOT_2")]
	StigmataSlot2,
	[ProtoEnum(Name = "STIGMATA_SLOT_3")]
	StigmataSlot3
}
