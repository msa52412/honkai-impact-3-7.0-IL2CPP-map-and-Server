using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DLCSupportType
{
	[ProtoEnum(Name = "DLC_SUPPORT_TYPE_COLLECTION")]
	DlcSupportTypeCollection = 1,
	[ProtoEnum(Name = "DLC_SUPPORT_TYPE_STAGE_BEGIN")]
	DlcSupportTypeStageBegin,
	[ProtoEnum(Name = "DLC_SUPPORT_TYPE_STORY")]
	DlcSupportTypeStory,
	[ProtoEnum(Name = "DLC_SUPPORT_TYPE_DIALOG")]
	DlcSupportTypeDialog,
	[ProtoEnum(Name = "DLC_SUPPORT_TYPE_QUEST")]
	DlcSupportTypeQuest
}
