using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RestaurantWorkStatus
{
	[ProtoEnum(Name = "RESTAURANT_WORK_NOT_BEGIN")]
	RestaurantWorkNotBegin = 1,
	[ProtoEnum(Name = "RESTAURANT_WORK_DOING")]
	RestaurantWorkDoing,
	[ProtoEnum(Name = "RESTAURANT_WORK_PAUSE")]
	RestaurantWorkPause
}
