using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldActivityType
{
	[ProtoEnum(Name = "CHATWORLD_ACTIVITY_TYPE_MAIN_PAGE_NOTICE")]
	ChatworldActivityTypeMainPageNotice = 1,
	[ProtoEnum(Name = "CHATWORLD_ACTIVITY_TYPE_ACTIVITY_NOTICE")]
	ChatworldActivityTypeActivityNotice,
	[ProtoEnum(Name = "CHATWORLD_ACTIVITY_TYPE_FIRE_WORKS")]
	ChatworldActivityTypeFireWorks,
	[ProtoEnum(Name = "CHATWORLD_ACTIVITY_TYPE_FISH")]
	ChatworldActivityTypeFish,
	[ProtoEnum(Name = "CHATWORLD_ACTIVITY_TYPE_PRAY")]
	ChatworldActivityTypePray,
	[ProtoEnum(Name = "CHATWORLD_ACTIVITY_TYPE_QUESTION")]
	ChatworldActivityTypeQuestion,
	[ProtoEnum(Name = "CHATWORLD_ACTIVITY_TYPE_PREPARE")]
	ChatworldActivityTypePrepare,
	[ProtoEnum(Name = "CHATWORLD_ACTIVITY_TYPE_ARMADA_REUNION")]
	ChatworldActivityTypeArmadaReunion,
	[ProtoEnum(Name = "CHATWORLD_ACTIVITY_TYPE_DISH")]
	ChatworldActivityTypeDish,
	[ProtoEnum(Name = "CHATWORLD_ACTIVITY_TYPE_BEAST")]
	ChatworldActivityTypeBeast,
	[ProtoEnum(Name = "CHATWORLD_ACTIVITY_TYPE_BOX")]
	ChatworldActivityTypeBox,
	[ProtoEnum(Name = "CHATWORLD_ACTIVITY_TYPE_PRAY_WITH_ORDER")]
	ChatworldActivityTypePrayWithOrder,
	[ProtoEnum(Name = "CHATWORLD_ACTIVITY_TYPE_CINEMA")]
	ChatworldActivityTypeCinema,
	[ProtoEnum(Name = "CHATWORLD_ACTIVITY_TYPE_WOODEN_BOY")]
	ChatworldActivityTypeWoodenBoy
}
