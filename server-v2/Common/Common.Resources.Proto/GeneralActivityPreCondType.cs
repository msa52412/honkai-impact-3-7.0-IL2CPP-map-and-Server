using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GeneralActivityPreCondType
{
	[ProtoEnum(Name = "GENERAL_ACTIVITY_PRE_COND_TYPE_NONE")]
	GeneralActivityPreCondTypeNone,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_PRE_COND_TYPE_SCORE")]
	GeneralActivityPreCondTypeScore
}
