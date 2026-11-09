using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OfflineResourceType
{
	[ProtoEnum(Name = "OFFLINE_RESOURCE_TYPE_NONE")]
	OfflineResourceTypeNone,
	[ProtoEnum(Name = "OFFLINE_RESOURCE_TYPE_OW_QUEST")]
	OfflineResourceTypeOwQuest,
	[ProtoEnum(Name = "OFFLINE_RESOURCE_TYPE_OW_MECHA")]
	OfflineResourceTypeOwMecha,
	[ProtoEnum(Name = "OFFLINE_RESOURCE_TYPE_WEEK_DAY")]
	OfflineResourceTypeWeekDay,
	[ProtoEnum(Name = "OFFLINE_RESOURCE_TYPE_ARMADA")]
	OfflineResourceTypeArmada,
	[ProtoEnum(Name = "OFFLINE_RESOURCE_TYPE_ENDLESS")]
	OfflineResourceTypeEndless,
	[ProtoEnum(Name = "OFFLINE_RESOURCE_TYPE_EX_BOSS")]
	OfflineResourceTypeExBoss
}
