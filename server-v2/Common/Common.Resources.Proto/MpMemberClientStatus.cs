using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MpMemberClientStatus
{
	[ProtoEnum(Name = "MP_MEMBER_CLIENT_NONE")]
	MpMemberClientNone,
	[ProtoEnum(Name = "MP_MEMBER_CLIENT_IN_TEAM")]
	MpMemberClientInTeam,
	[ProtoEnum(Name = "MP_MEMBER_CLIENT_IN_LOBBY")]
	MpMemberClientInLobby
}
