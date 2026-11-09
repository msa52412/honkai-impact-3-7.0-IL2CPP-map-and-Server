using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ShigureKiraProgressSlotType
{
	[ProtoEnum(Name = "SHIGURE_KIRA_PROGRESS_SLOT_NONE")]
	ShigureKiraProgressSlotNone,
	[ProtoEnum(Name = "SHIGURE_KIRA_PROGRESS_SLOT_FANCHANT")]
	ShigureKiraProgressSlotFanchant,
	[ProtoEnum(Name = "SHIGURE_KIRA_PROGRESS_SLOT_CONCERT")]
	ShigureKiraProgressSlotConcert
}
