using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MonopolyBuildingFuncType
{
	[ProtoEnum(Name = "MONOPOLY_BUILDING_FUNC_PRODUCT")]
	MonopolyBuildingFuncProduct = 1,
	[ProtoEnum(Name = "MONOPOLY_BUILDING_FUNC_BUFF")]
	MonopolyBuildingFuncBuff
}
