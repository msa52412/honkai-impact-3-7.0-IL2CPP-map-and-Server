using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum StagePassType
{
	[ProtoEnum(Name = "STAGE_PASS_TYPE_NORMAL")]
	StagePassTypeNormal,
	[ProtoEnum(Name = "STAGE_PASS_TYPE_BURDEN_ALLEVIATION")]
	StagePassTypeBurdenAlleviation
}
