using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CityActivityDataType
{
	[ProtoEnum(Name = "CITY_ACTIVITY_DATA_ALL")]
	CityActivityDataAll,
	[ProtoEnum(Name = "CITY_ACTIVITY_DATA_EVENT")]
	CityActivityDataEvent,
	[ProtoEnum(Name = "CITY_ACTIVITY_DATA_PHONE_EVENT")]
	CityActivityDataPhoneEvent,
	[ProtoEnum(Name = "CITY_ACTIVITY_DATA_PHOTO")]
	CityActivityDataPhoto
}
