using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MpStageMemberLimitType
{
	[ProtoEnum(Name = "MP_MEMBER_LIMIT_NONE")]
	MpMemberLimitNone,
	[ProtoEnum(Name = "MP_MEMBER_LIMIT_MASTER_ONLY")]
	MpMemberLimitMasterOnly,
	[ProtoEnum(Name = "MP_MEMBER_LIMIT_PUPIL_ONLY")]
	MpMemberLimitPupilOnly,
	[ProtoEnum(Name = "MP_MEMBER_LIMIT_ARMADA_ONLY")]
	MpMemberLimitArmadaOnly,
	[ProtoEnum(Name = "MP_MEMBER_LIMIT_NEWBIE_ONLY")]
	MpMemberLimitNewbieOnly
}
