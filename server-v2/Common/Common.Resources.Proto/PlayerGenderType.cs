using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PlayerGenderType
{
	[ProtoEnum(Name = "GENDER_NONE_TYPE")]
	GenderNoneType,
	[ProtoEnum(Name = "GENDER_MALE_TYPE")]
	GenderMaleType,
	[ProtoEnum(Name = "GENDER_FEMALE_TYPE")]
	GenderFemaleType
}
