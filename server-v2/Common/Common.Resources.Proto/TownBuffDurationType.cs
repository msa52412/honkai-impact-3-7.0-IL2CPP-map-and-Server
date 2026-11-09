using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TownBuffDurationType
{
	[ProtoEnum(Name = "TOWN_BUFF_DURATION_PERMANENT")]
	TownBuffDurationPermanent,
	[ProtoEnum(Name = "TOWN_BUFF_DURATION_BATTLE")]
	TownBuffDurationBattle,
	[ProtoEnum(Name = "TOWN_BUFF_DURATION_STEP")]
	TownBuffDurationStep,
	[ProtoEnum(Name = "TOWN_BUFF_DURATION_BOSS")]
	TownBuffDurationBoss
}
