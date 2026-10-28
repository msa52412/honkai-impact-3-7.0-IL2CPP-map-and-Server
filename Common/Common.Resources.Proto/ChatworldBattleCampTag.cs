using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldBattleCampTag
{
	[ProtoEnum(Name = "CHATWORLD_BATTLE_CAMP_TAG_NONE")]
	ChatworldBattleCampTagNone,
	[ProtoEnum(Name = "CHATWORLD_BATTLE_CAMP_TAG_1")]
	ChatworldBattleCampTag1,
	[ProtoEnum(Name = "CHATWORLD_BATTLE_CAMP_TAG_2")]
	ChatworldBattleCampTag2
}
