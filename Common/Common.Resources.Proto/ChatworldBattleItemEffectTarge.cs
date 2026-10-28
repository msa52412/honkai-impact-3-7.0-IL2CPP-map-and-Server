using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldBattleItemEffectTarget
{
	[ProtoEnum(Name = "CHATWORLD_BATTLE_ITEM_EFFECT_ALL")]
	ChatworldBattleItemEffectAll,
	[ProtoEnum(Name = "CHATWORLD_BATTLE_ITEM_EFFECT_ENEMY_CAMP")]
	ChatworldBattleItemEffectEnemyCamp,
	[ProtoEnum(Name = "CHATWORLD_BATTLE_ITEM_EFFECT_SELF_CAMP")]
	ChatworldBattleItemEffectSelfCamp
}
