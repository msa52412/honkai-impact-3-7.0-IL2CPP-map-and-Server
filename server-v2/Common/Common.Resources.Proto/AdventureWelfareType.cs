using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AdventureWelfareType
{
	[ProtoEnum(Name = "ADVENTURE_WELFARE_NONE")]
	AdventureWelfareNone,
	[ProtoEnum(Name = "ADVENTURE_WELFARE_VENTURE_STAMINA")]
	AdventureWelfareVentureStamina,
	[ProtoEnum(Name = "ADVENTURE_WELFARE_SHOP")]
	AdventureWelfareShop,
	[ProtoEnum(Name = "ADVENTURE_WELFARE_QUEST_LEVEL")]
	AdventureWelfareQuestLevel,
	[ProtoEnum(Name = "ADVENTURE_WELFARE_SCOIN")]
	AdventureWelfareScoin,
	[ProtoEnum(Name = "ADVENTURE_WELFARE_STAMINA")]
	AdventureWelfareStamina
}
