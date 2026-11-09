using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GalInteractBubbleType
{
	[ProtoEnum(Name = "GALINTERACT_BUBBLE_NO")]
	GalinteractBubbleNo,
	[ProtoEnum(Name = "GALINTERACT_BUBBLE_YES")]
	GalinteractBubbleYes,
	[ProtoEnum(Name = "GALINTERACT_BUBBLE_PLACEHOLD")]
	GalinteractBubblePlacehold,
	[ProtoEnum(Name = "GALINTERACT_BUBBLE_WITH_CONDITION")]
	GalinteractBubbleWithCondition
}
