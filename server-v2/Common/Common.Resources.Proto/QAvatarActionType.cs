using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarActionType
{
	[ProtoEnum(Name = "QAVATAR_ACTION_NONE")]
	QavatarActionNone = 0,
	[ProtoEnum(Name = "QAVATAR_ACTION_ADD_BUFF")]
	QavatarActionAddBuff = 1,
	[ProtoEnum(Name = "QAVATAR_ACTION_REMOVE_BUFF")]
	QavatarActionRemoveBuff = 2,
	[ProtoEnum(Name = "QAVATAR_ACTION_EVADE")]
	QavatarActionEvade = 3,
	[ProtoEnum(Name = "QAVATAR_ACTION_SHOOT_BULLET")]
	QavatarActionShootBullet = 4,
	[ProtoEnum(Name = "QAVATAR_ACTION_MELEE_ATK")]
	QavatarActionMeleeAtk = 5,
	[ProtoEnum(Name = "QAVATAR_ACTION_COMBO_ATK")]
	QavatarActionComboAtk = 6,
	[ProtoEnum(Name = "QAVATAR_ACTION_CREATE_GADGET")]
	QavatarActionCreateGadget = 7,
	[ProtoEnum(Name = "QAVATAR_ACTION_MUTE_SKILL")]
	QavatarActionMuteSkill = 8,
	[ProtoEnum(Name = "QAVATAR_ACTION_BREAK_SKILL")]
	QavatarActionBreakSkill = 9,
	[ProtoEnum(Name = "QAVATAR_ACTION_CHANGE_PROP")]
	QavatarActionChangeProp = 11,
	[ProtoEnum(Name = "QAVATAR_ACTION_HOLDUP_ACTION")]
	QavatarActionHoldupAction = 13,
	[ProtoEnum(Name = "QAVATAR_ACTION_SET_ANIM_PARA")]
	QavatarActionSetAnimPara = 21,
	[ProtoEnum(Name = "QAVATAR_ACTION_APPLY_EFFECT")]
	QavatarActionApplyEffect = 22,
	[ProtoEnum(Name = "QAVATAR_ACTION_SET_CAMERA_PARA")]
	QavatarActionSetCameraPara = 23,
	[ProtoEnum(Name = "QAVATAR_ACTION_LOCK_AIM_ROTATION")]
	QavatarActionLockAimRotation = 24,
	[ProtoEnum(Name = "QAVATAR_ACTION_SIM_SKILL_BUTTON_OP")]
	QavatarActionSimSkillButtonOp = 25
}
