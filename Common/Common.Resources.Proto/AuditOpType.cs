using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AuditOpType
{
	[ProtoEnum(Name = "AUDIT_OP_NONE")]
	AuditOpNone,
	[ProtoEnum(Name = "AUDIT_OP_CHANGE_NICKNAME")]
	AuditOpChangeNickname,
	[ProtoEnum(Name = "AUDIT_OP_CHANGE_DESC")]
	AuditOpChangeDesc,
	[ProtoEnum(Name = "AUDIT_OP_CHANGE_ARMADA_NAME")]
	AuditOpChangeArmadaName,
	[ProtoEnum(Name = "AUDIT_OP_CHANGE_ARMADA_BULLETIN")]
	AuditOpChangeArmadaBulletin,
	[ProtoEnum(Name = "AUDIT_OP_CREATE_CHATGROUP")]
	AuditOpCreateChatgroup,
	[ProtoEnum(Name = "AUDIT_OP_CHANGE_CHATGROUP")]
	AuditOpChangeChatgroup,
	[ProtoEnum(Name = "AUDIT_OP_CHANGE_DORM")]
	AuditOpChangeDorm,
	[ProtoEnum(Name = "AUDIT_OP_CHANGE_MASTER_PUPIL_CARD")]
	AuditOpChangeMasterPupilCard
}
