using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldMechaTeamPosType
{
	[ProtoEnum(Name = "MECHA_TEAM_POS_1")]
	MechaTeamPos1 = 1,
	[ProtoEnum(Name = "MECHA_TEAM_POS_2")]
	MechaTeamPos2,
	[ProtoEnum(Name = "MECHA_TEAM_POS_3")]
	MechaTeamPos3,
	[ProtoEnum(Name = "MECHA_TEAM_POS_4")]
	MechaTeamPos4,
	[ProtoEnum(Name = "MECHA_TEAM_POS_5")]
	MechaTeamPos5
}
