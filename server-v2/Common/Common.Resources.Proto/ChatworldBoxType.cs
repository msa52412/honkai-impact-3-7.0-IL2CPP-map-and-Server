using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldBoxType
{
	[ProtoEnum(Name = "CHATWORLD_BOX_TYPE_NONE")]
	ChatworldBoxTypeNone,
	[ProtoEnum(Name = "CHATWORLD_BOX_TYPE_REWARD")]
	ChatworldBoxTypeReward,
	[ProtoEnum(Name = "CHATWORLD_BOX_TYPE_BATTLE")]
	ChatworldBoxTypeBattle
}
