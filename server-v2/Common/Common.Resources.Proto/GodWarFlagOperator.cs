using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarFlagOperator
{
	[ProtoEnum(Name = "GOD_WAR_FLAG_OPERATOR_INVALID")]
	GodWarFlagOperatorInvalid,
	[ProtoEnum(Name = "GOD_WAR_FLAG_OPERATOR_AND")]
	GodWarFlagOperatorAnd,
	[ProtoEnum(Name = "GOD_WAR_FLAG_OPERATOR_OR")]
	GodWarFlagOperatorOr,
	[ProtoEnum(Name = "GOD_WAR_FLAG_OPERATOR_NOT")]
	GodWarFlagOperatorNot,
	[ProtoEnum(Name = "GOD_WAR_FLAG_OPERATOR_EQ")]
	GodWarFlagOperatorEq,
	[ProtoEnum(Name = "GOD_WAR_FLAG_OPERATOR_GT")]
	GodWarFlagOperatorGt,
	[ProtoEnum(Name = "GOD_WAR_FLAG_OPERATOR_LT")]
	GodWarFlagOperatorLt,
	[ProtoEnum(Name = "GOD_WAR_FLAG_OPERATOR_COUNT_GE")]
	GodWarFlagOperatorCountGe,
	[ProtoEnum(Name = "GOD_WAR_FLAG_OPERATOR_SUM_GE")]
	GodWarFlagOperatorSumGe,
	[ProtoEnum(Name = "GOD_WAR_FLAG_OPERATOR_COUNT_LT")]
	GodWarFlagOperatorCountLt,
	[ProtoEnum(Name = "GOD_WAR_FLAG_OPERATOR_SUM_LT")]
	GodWarFlagOperatorSumLt,
	[ProtoEnum(Name = "GOD_WAR_FLAG_OPERATOR_COUNT_EQ")]
	GodWarFlagOperatorCountEq,
	[ProtoEnum(Name = "GOD_WAR_FLAG_OPERATOR_SUM_EQ")]
	GodWarFlagOperatorSumEq
}
