using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldStageCondition
{
	[ProtoEnum(Name = "OPENWORLD_STAGE_COND_LD_SP")]
	OpenworldStageCondLdSp = 1
}
