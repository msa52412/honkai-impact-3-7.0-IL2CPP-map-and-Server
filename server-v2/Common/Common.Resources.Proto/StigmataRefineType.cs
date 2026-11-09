using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum StigmataRefineType
{
	[ProtoEnum(Name = "STIGMATA_REFINE_NORMAL")]
	StigmataRefineNormal = 1,
	[ProtoEnum(Name = "STIGMATA_REFINE_ADD_SLOT")]
	StigmataRefineAddSlot,
	[ProtoEnum(Name = "STIGMATA_REFINE_SPECIAL")]
	StigmataRefineSpecial,
	[ProtoEnum(Name = "STIGMATA_REFINE_LOCK")]
	StigmataRefineLock
}
