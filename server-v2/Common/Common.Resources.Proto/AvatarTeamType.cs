using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AvatarTeamType
{
	[ProtoEnum(Name = "AVATAR_TEAM_NORMAL")]
	AvatarTeamNormal = 1,
	[ProtoEnum(Name = "AVATAR_TEAM_VIRTUAL")]
	AvatarTeamVirtual,
	[ProtoEnum(Name = "AVATAR_TEAM_DLC")]
	AvatarTeamDlc,
	[ProtoEnum(Name = "AVATAR_TEAM_SC_DLC")]
	AvatarTeamScDlc
}
