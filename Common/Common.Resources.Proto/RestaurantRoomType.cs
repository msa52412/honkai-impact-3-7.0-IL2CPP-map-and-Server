using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RestaurantRoomType
{
	[ProtoEnum(Name = "RESTAURANT_ROOM_STORE")]
	RestaurantRoomStore = 1,
	[ProtoEnum(Name = "RESTAURANT_ROOM_KITCHEN")]
	RestaurantRoomKitchen,
	[ProtoEnum(Name = "RESTAURANT_ROOM_CHECKOUT")]
	RestaurantRoomCheckout
}
