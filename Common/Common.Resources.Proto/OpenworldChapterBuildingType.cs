using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldChapterBuildingType
{
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_BUILDING_FURNACE")]
	OpenworldChapterBuildingFurnace = 1,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_BUILDING_QUEST")]
	OpenworldChapterBuildingQuest,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_BUILDING_WORKSHOP")]
	OpenworldChapterBuildingWorkshop,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_BUILDING_BAG")]
	OpenworldChapterBuildingBag,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_BUILDING_DIGSITE")]
	OpenworldChapterBuildingDigsite,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_BUILDING_TRANSPORT")]
	OpenworldChapterBuildingTransport
}
