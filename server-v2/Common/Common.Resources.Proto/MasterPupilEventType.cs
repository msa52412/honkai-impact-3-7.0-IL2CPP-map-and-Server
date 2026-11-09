using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MasterPupilEventType
{
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_GET_PUPIL")]
	MasterPupilEventGetPupil = 1,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_GET_MASTER")]
	MasterPupilEventGetMaster,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_PUPIL_ONLINE")]
	MasterPupilEventPupilOnline,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_MASTER_ONLINE")]
	MasterPupilEventMasterOnline,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_PUPIL_OFFLINE")]
	MasterPupilEventPupilOffline,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_MASTER_OFFLINE")]
	MasterPupilEventMasterOffline,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_FINISH_MISSION")]
	MasterPupilEventFinishMission,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_STEP_EVAL")]
	MasterPupilEventStepEval,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_PUPIL_GRADUATED")]
	MasterPupilEventPupilGraduated,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_SELF_MISSION")]
	MasterPupilEventSelfMission,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_APPLY_PUPIL")]
	MasterPupilEventApplyPupil,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_APPLY_MASTER")]
	MasterPupilEventApplyMaster,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_REJECT_MASTER")]
	MasterPupilEventRejectMaster,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_REJECT_PUPIL")]
	MasterPupilEventRejectPupil,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_AGREE_MASTER")]
	MasterPupilEventAgreeMaster,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_AGREE_PUPIL")]
	MasterPupilEventAgreePupil,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_DISMISS_MASTER")]
	MasterPupilEventDismissMaster,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_DISMISS_PUPIL")]
	MasterPupilEventDismissPupil,
	[ProtoEnum(Name = "MASTER_PUPIL_EVENT_COMPANY_MISSION")]
	MasterPupilEventCompanyMission,
	[ProtoEnum(Name = "MASTER_PUPIL_EVNET_RECALL_APPLICATION")]
	MasterPupilEvnetRecallApplication
}
