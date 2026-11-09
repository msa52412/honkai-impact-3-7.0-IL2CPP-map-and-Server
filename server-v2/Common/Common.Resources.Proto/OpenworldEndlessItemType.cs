using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldEndlessItemType
{
	[ProtoEnum(Name = "OPENWORLD_ENDLESS_ITEM_INVASION")]
	OpenworldEndlessItemInvasion = 1,
	[ProtoEnum(Name = "OPENWORLD_ENDLESS_ITEM_BUFF")]
	OpenworldEndlessItemBuff,
	[ProtoEnum(Name = "OPENWORLD_ENDLESS_ITEM_TRAP")]
	OpenworldEndlessItemTrap,
	[ProtoEnum(Name = "OPENWORLD_ENDLESS_ITEM_COUNTER")]
	OpenworldEndlessItemCounter,
	[ProtoEnum(Name = "OPENWORLD_ENDLESS_ITEM_CONSUME")]
	OpenworldEndlessItemConsume,
	[ProtoEnum(Name = "OPENWORLD_ENDLESS_ITEM_TRAP_BUFF")]
	OpenworldEndlessItemTrapBuff
}
