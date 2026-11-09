using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldChapterFurnaceWorkStatus
{
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_FURNACE_WORK_INVALID")]
	OpenworldChapterFurnaceWorkInvalid,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_FURNACE_WORK_NOT_BEGIN")]
	OpenworldChapterFurnaceWorkNotBegin,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_FURNACE_WORK_DOING")]
	OpenworldChapterFurnaceWorkDoing,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_FURNACE_WORK_FINISHED")]
	OpenworldChapterFurnaceWorkFinished
}
