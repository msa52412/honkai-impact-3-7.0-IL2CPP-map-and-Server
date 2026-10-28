using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OptionalBuffType
{
	[ProtoEnum(Name = "OPTIONAL_BUFF_NONE")]
	OptionalBuffNone,
	[ProtoEnum(Name = "OPTIONAL_BUFF_DEBUFF")]
	OptionalBuffDebuff,
	[ProtoEnum(Name = "OPTIONAL_BUFF_RESTRICT")]
	OptionalBuffRestrict
}
