using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AreaType
{
	[ProtoEnum(Name = "AREA_CN")]
	AreaCn = 1,
	[ProtoEnum(Name = "AREA_JP")]
	AreaJp,
	[ProtoEnum(Name = "AREA_ASIA")]
	AreaAsia,
	[ProtoEnum(Name = "AREA_WEST")]
	AreaWest,
	[ProtoEnum(Name = "AREA_KR")]
	AreaKr,
	[ProtoEnum(Name = "AREA_OVERSEAS")]
	AreaOverseas
}
