using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarNpcType
{
	[ProtoEnum(Name = "GOD_WAR_NPC_NONE")]
	GodWarNpcNone,
	[ProtoEnum(Name = "GOD_WAR_NPC_MAIN_LINE")]
	GodWarNpcMainLine,
	[ProtoEnum(Name = "GOD_WAR_NPC_BRANCH_LINE")]
	GodWarNpcBranchLine
}
