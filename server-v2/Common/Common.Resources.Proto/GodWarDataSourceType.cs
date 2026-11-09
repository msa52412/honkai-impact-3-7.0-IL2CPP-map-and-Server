using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarDataSourceType
{
	[ProtoEnum(Name = "GOD_WAR_DATA_SOURCE_CUR_TALE")]
	GodWarDataSourceCurTale = 1,
	[ProtoEnum(Name = "GOD_WAR_DATA_SOURCE_SPECIFIC_TALE")]
	GodWarDataSourceSpecificTale
}
