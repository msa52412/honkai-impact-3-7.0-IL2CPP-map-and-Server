using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldFunction
{
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_QUEST")]
	OpenworldFunctionQuest = 1,
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_COOK")]
	OpenworldFunctionCook,
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_MECHA")]
	OpenworldFunctionMecha,
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_MECHA_DEFENSE")]
	OpenworldFunctionMechaDefense,
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_SHOP")]
	OpenworldFunctionShop,
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_WEEK_SETTLE")]
	OpenworldFunctionWeekSettle,
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_TREASURE")]
	OpenworldFunctionTreasure,
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_TECH")]
	OpenworldFunctionTech,
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_WEATHER")]
	OpenworldFunctionWeather,
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_BOSS_HUNT")]
	OpenworldFunctionBossHunt,
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_STORY")]
	OpenworldFunctionStory,
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_KEY_QUEST")]
	OpenworldFunctionKeyQuest
}
