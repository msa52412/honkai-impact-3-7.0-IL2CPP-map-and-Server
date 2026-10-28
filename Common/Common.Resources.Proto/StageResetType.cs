using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum StageResetType
{
	[ProtoEnum(Name = "STAGE_RESET_TYPE_RESET")]
	StageResetTypeReset,
	[ProtoEnum(Name = "STAGE_RESET_TYPE_ADD")]
	StageResetTypeAdd
}
