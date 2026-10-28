using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MedalOpType
{
	[ProtoEnum(Name = "MEDAL_WEAR")]
	MedalWear = 1,
	[ProtoEnum(Name = "MEDAL_OFF")]
	MedalOff
}
