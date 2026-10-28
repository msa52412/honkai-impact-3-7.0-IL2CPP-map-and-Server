using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum FeatureSwitchOpType
{
	[ProtoEnum(Name = "FEATURE_SWITCH_OP_NONE")]
	FeatureSwitchOpNone,
	[ProtoEnum(Name = "FEATURE_SWITCH_OP_ADD")]
	FeatureSwitchOpAdd,
	[ProtoEnum(Name = "FEATURE_SWITCH_OP_DEL")]
	FeatureSwitchOpDel
}
