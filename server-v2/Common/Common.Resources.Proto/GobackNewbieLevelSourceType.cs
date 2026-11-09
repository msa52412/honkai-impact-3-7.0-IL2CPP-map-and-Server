using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GobackNewbieLevelSourceType
{
	[ProtoEnum(Name = "GOBACK_NEWBIE_LEVEL_SOURCE_GOBACK_NEWBIE_LEVEL")]
	GobackNewbieLevelSourceGobackNewbieLevel,
	[ProtoEnum(Name = "GOBACK_NEWBIE_LEVEL_SOURCE_GOBACK_LEVEL")]
	GobackNewbieLevelSourceGobackLevel
}
