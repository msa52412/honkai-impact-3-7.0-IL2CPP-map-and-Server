using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MissionType
{
	[ProtoEnum(Name = "MISSION_NONE")]
	MissionNone,
	[ProtoEnum(Name = "MISSION_LINEAR")]
	MissionLinear,
	[ProtoEnum(Name = "MISSION_BOUNTY")]
	MissionBounty,
	[ProtoEnum(Name = "MISSION_TIMED")]
	MissionTimed,
	[ProtoEnum(Name = "MISSION_SIDE")]
	MissionSide,
	[ProtoEnum(Name = "MISSION_CYCLE")]
	MissionCycle,
	[ProtoEnum(Name = "MISSION_DAILY")]
	MissionDaily,
	[ProtoEnum(Name = "MISSION_PROGRESS")]
	MissionProgress,
	[ProtoEnum(Name = "MISSION_CHALLENGE")]
	MissionChallenge,
	[ProtoEnum(Name = "MISSION_MINUTE")]
	MissionMinute,
	[ProtoEnum(Name = "MISSION_MASTER_PUPIL_EXAM")]
	MissionMasterPupilExam,
	[ProtoEnum(Name = "MISSION_MASTER_PUPIL_DAILY")]
	MissionMasterPupilDaily,
	[ProtoEnum(Name = "MISSION_AVATAR_TRAINING")]
	MissionAvatarTraining,
	[ProtoEnum(Name = "MISSION_RPG_TALE")]
	MissionRpgTale,
	[ProtoEnum(Name = "MISSION_MAINLINE_STEP")]
	MissionMainlineStep,
	[ProtoEnum(Name = "MISSION_GOBACK")]
	MissionGoback,
	[ProtoEnum(Name = "MISSION_WEEKLY_ROUTINE")]
	MissionWeeklyRoutine,
	[ProtoEnum(Name = "MISSION_BATTLE_PASS")]
	MissionBattlePass,
	[ProtoEnum(Name = "MISSION_ADVENTURE_GROUP")]
	MissionAdventureGroup,
	[ProtoEnum(Name = "MISSION_MASTER_PUPIL_COMPANY")]
	MissionMasterPupilCompany,
	[ProtoEnum(Name = "MISSION_ACTIVITY")]
	MissionActivity,
	[ProtoEnum(Name = "MISSION_COMPETITION_SEASON")]
	MissionCompetitionSeason
}
