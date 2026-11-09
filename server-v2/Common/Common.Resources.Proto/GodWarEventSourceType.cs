using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarEventSourceType
{
	[ProtoEnum(Name = "GOD_WAR_EVENT_SOURCE_NORMAL")]
	GodWarEventSourceNormal = 1,
	[ProtoEnum(Name = "GOD_WAR_EVENT_SOURCE_SITE_ENTER")]
	GodWarEventSourceSiteEnter,
	[ProtoEnum(Name = "GOD_WAR_EVENT_SOURCE_SITE_BUFF")]
	GodWarEventSourceSiteBuff,
	[ProtoEnum(Name = "GOD_WAR_EVENT_SOURCE_SHOP_RECOVER")]
	GodWarEventSourceShopRecover,
	[ProtoEnum(Name = "GOD_WAR_EVENT_SOURCE_SHOP_CHANGE_STATE")]
	GodWarEventSourceShopChangeState
}
