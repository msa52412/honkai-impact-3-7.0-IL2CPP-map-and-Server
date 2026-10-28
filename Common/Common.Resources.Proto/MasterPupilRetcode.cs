using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MasterPupilRetcode : IExtensible
{
	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "PUPIL_LEVEL_LIMIT")]
		PupilLevelLimit = 2,
		[ProtoEnum(Name = "MASTER_LEVEL_LIMIT")]
		MasterLevelLimit = 3,
		[ProtoEnum(Name = "APPLY_NUM_LIMIT")]
		ApplyNumLimit = 6,
		[ProtoEnum(Name = "TARGET_APPLY_NUM_LIMIT")]
		TargetApplyNumLimit = 7,
		[ProtoEnum(Name = "INVALID_UID")]
		InvalidUid = 8,
		[ProtoEnum(Name = "IS_EVALED")]
		IsEvaled = 9,
		[ProtoEnum(Name = "EXAM_MISSION_UNFINISH")]
		ExamMissionUnfinish = 10,
		[ProtoEnum(Name = "FAME_LEVEL_LIMIT")]
		FameLevelLimit = 12,
		[ProtoEnum(Name = "FAME_LEVEL_REWARDED")]
		FameLevelRewarded = 13,
		[ProtoEnum(Name = "CONTENT_STR_INVALID")]
		ContentStrInvalid = 14,
		[ProtoEnum(Name = "MASTER_PUPIL_TYPE_ERROR")]
		MasterPupilTypeError = 16,
		[ProtoEnum(Name = "APPLY_REPEATE")]
		ApplyRepeate = 17,
		[ProtoEnum(Name = "HAS_RELATION")]
		HasRelation = 18,
		[ProtoEnum(Name = "NO_RELATION")]
		NoRelation = 19,
		[ProtoEnum(Name = "RELATION_CD_LIMIT")]
		RelationCdLimit = 21,
		[ProtoEnum(Name = "HAS_STEP_UNFINISH")]
		HasStepUnfinish = 22,
		[ProtoEnum(Name = "NO_STEP_DOING")]
		NoStepDoing = 23,
		[ProtoEnum(Name = "DAILY_REFRESH_TIMES_LIMIT")]
		DailyRefreshTimesLimit = 24,
		[ProtoEnum(Name = "SAME_DAY_TIME")]
		SameDayTime = 25,
		[ProtoEnum(Name = "RECOMMEND_CD")]
		RecommendCd = 26,
		[ProtoEnum(Name = "INVALID_STAR")]
		InvalidStar = 27,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen = 28,
		[ProtoEnum(Name = "NOT_ALLOW_CROSS_REGION_MASTER_PUPIL")]
		NotAllowCrossRegionMasterPupil = 29,
		[ProtoEnum(Name = "FEATURE_LIMIT")]
		FeatureLimit = 30,
		[ProtoEnum(Name = "SELF_PUPIL_NUM_LIMIT")]
		SelfPupilNumLimit = 101,
		[ProtoEnum(Name = "SELF_HAS_MASTER")]
		SelfHasMaster = 102,
		[ProtoEnum(Name = "SELF_STEP_ERROR")]
		SelfStepError = 103,
		[ProtoEnum(Name = "SELF_DISMISS_TIME_LIMIT")]
		SelfDismissTimeLimit = 104,
		[ProtoEnum(Name = "SELF_IS_GRADUATED")]
		SelfIsGraduated = 105,
		[ProtoEnum(Name = "SELF_MONTH_PUPIL_NUM_LIMIT")]
		SelfMonthPupilNumLimit = 106,
		[ProtoEnum(Name = "SELF_IS_MASTER")]
		SelfIsMaster = 107,
		[ProtoEnum(Name = "TARGET_PUPIL_NUM_LIMIT")]
		TargetPupilNumLimit = 201,
		[ProtoEnum(Name = "TARGET_HAS_MASTER")]
		TargetHasMaster = 202,
		[ProtoEnum(Name = "TARGET_STEP_ERROR")]
		TargetStepError = 203,
		[ProtoEnum(Name = "TARGET_DISMISS_TIME_LIMIT")]
		TargetDismissTimeLimit = 204,
		[ProtoEnum(Name = "TARGET_IS_GRADUATED")]
		TargetIsGraduated = 205,
		[ProtoEnum(Name = "TARGET_MONTH_PUPIL_NUM_LIMIT")]
		TargetMonthPupilNumLimit = 206,
		[ProtoEnum(Name = "TARGET_IS_MASTER")]
		TargetIsMaster = 207,
		[ProtoEnum(Name = "DECLARATION_IN_CD")]
		DeclarationInCd = 208,
		[ProtoEnum(Name = "DECLARATION_IN_AUDIT")]
		DeclarationInAudit = 209,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed = 210,
		[ProtoEnum(Name = "TARGET_RECALLED")]
		TargetRecalled = 211
	}

	private IExtension __pbn__extensionData;

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
