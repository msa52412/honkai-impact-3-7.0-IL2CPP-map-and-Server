using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ConstantEnum
{
	[ProtoEnum(Name = "UN_LIMIT_TIMES")]
	UnLimitTimes = 65535,
	[ProtoEnum(Name = "FRAME_DEFAULT_ID")]
	FrameDefaultId = 200001
}
