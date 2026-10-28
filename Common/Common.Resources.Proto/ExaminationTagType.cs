using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ExaminationTagType
{
	[ProtoEnum(Name = "EXAMINATION_TYPE_NONE")]
	ExaminationTypeNone,
	[ProtoEnum(Name = "EXAMINATION_TYPE_AVATAR")]
	ExaminationTypeAvatar,
	[ProtoEnum(Name = "EXAMINATION_TYPE_ELF")]
	ExaminationTypeElf,
	[ProtoEnum(Name = "EXAMINATION_TYPE_MONSTER")]
	ExaminationTypeMonster,
	[ProtoEnum(Name = "EXAMINATION_TYPE_COMMON")]
	ExaminationTypeCommon
}
