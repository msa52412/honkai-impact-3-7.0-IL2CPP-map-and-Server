using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldWoodenBoyStatus
{
	[ProtoEnum(Name = "CHATWORLD_WOODEN_BOY_NONE")]
	ChatworldWoodenBoyNone,
	[ProtoEnum(Name = "CHATWORLD_WOODEN_BOY_PREPARE")]
	ChatworldWoodenBoyPrepare,
	[ProtoEnum(Name = "CHATWORLD_WOODEN_BOY_PLAY")]
	ChatworldWoodenBoyPlay,
	[ProtoEnum(Name = "CHATWORLD_WOODEN_BOY_RESULT")]
	ChatworldWoodenBoyResult,
	[ProtoEnum(Name = "CHATWORLD_WOODEN_BOY_CD")]
	ChatworldWoodenBoyCd
}
