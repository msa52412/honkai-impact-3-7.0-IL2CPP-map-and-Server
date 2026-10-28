using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldDataType
{
	[ProtoEnum(Name = "OPENWORLD_DATA_ALL")]
	OpenworldDataAll,
	[ProtoEnum(Name = "OPENWORLD_DATA_COOK")]
	OpenworldDataCook,
	[ProtoEnum(Name = "OPENWORLD_DATA_TECH")]
	OpenworldDataTech,
	[ProtoEnum(Name = "OPENWORLD_DATA_MECHA")]
	OpenworldDataMecha
}
