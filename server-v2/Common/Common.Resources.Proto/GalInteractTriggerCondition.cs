using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GalInteractTriggerCondition
{
	[ProtoEnum(Name = "GALINTERACT_CONDITION_DRESS")]
	GalinteractConditionDress = 1,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_SHAKE")]
	GalinteractConditionShake,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_MAIN_SCREEN")]
	GalinteractConditionMainScreen,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_INTERVAL")]
	GalinteractConditionInterval,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_DATETIME_YMD")]
	GalinteractConditionDatetimeYmd,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_DATETIME_MD")]
	GalinteractConditionDatetimeMd,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_WEEKDAY")]
	GalinteractConditionWeekday,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_DAILY_DUTY_POINT")]
	GalinteractConditionDailyDutyPoint,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_WEEKLY_DUTY_POINT")]
	GalinteractConditionWeeklyDutyPoint,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_WEATHER")]
	GalinteractConditionWeather,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_GACHA")]
	GalinteractConditionGacha,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_ONLINE_TIME")]
	GalinteractConditionOnlineTime,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_LOGIN_ITERVAL")]
	GalinteractConditionLoginIterval,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_MONTH_DAY_INTERVAL")]
	GalinteractConditionMonthDayInterval,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_USER_BIRTHDAY")]
	GalinteractConditionUserBirthday,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_GOODFEEL_LEVEL")]
	GalinteractConditionGoodfeelLevel,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_AFFINITY_GET_GIFT_LEVEL")]
	GalinteractConditionAffinityGetGiftLevel,
	[ProtoEnum(Name = "GALINTERACT_CONDITION_MISSION_FINISH")]
	GalinteractConditionMissionFinish
}
