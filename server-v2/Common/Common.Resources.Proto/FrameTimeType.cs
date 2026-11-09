using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum FrameTimeType
{
	[ProtoEnum(Name = "FRAME_TIME_FOREVER")]
	FrameTimeForever,
	[ProtoEnum(Name = "FRAME_TIME_LIMIT")]
	FrameTimeLimit
}
