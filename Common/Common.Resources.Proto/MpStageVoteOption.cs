using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MpStageVoteOption
{
	[ProtoEnum(Name = "MP_STAGE_VOTE_OPTION_STAY_IN_TEAM")]
	MpStageVoteOptionStayInTeam,
	[ProtoEnum(Name = "MP_STAGE_VOTE_OPTION_STAGE_AGAIN")]
	MpStageVoteOptionStageAgain,
	[ProtoEnum(Name = "MP_STAGE_VOTE_OPTION_NEXT_STAGE")]
	MpStageVoteOptionNextStage
}
