using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DLCTalentType
{
	[ProtoEnum(Name = "DLC_TALENT_TYPE_NONE")]
	DlcTalentTypeNone,
	[ProtoEnum(Name = "DLC_TALENT_TYPE_AUTO")]
	DlcTalentTypeAuto,
	[ProtoEnum(Name = "DLC_TALENT_TYPE_ACTIVE")]
	DlcTalentTypeActive,
	[ProtoEnum(Name = "DLC_TALENT_TYPE_PASSIVE")]
	DlcTalentTypePassive,
	[ProtoEnum(Name = "DLC_TALENT_TYPE_REFRESHABLE")]
	DlcTalentTypeRefreshable
}
