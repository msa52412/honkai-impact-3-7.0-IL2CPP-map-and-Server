using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarBitmapType
{
	[ProtoEnum(Name = "GOD_WAR_BITMAP_EVENT")]
	GodWarBitmapEvent = 1,
	[ProtoEnum(Name = "GOD_WAR_BITMAP_NO_REPEAT_SETTLE_EVENT")]
	GodWarBitmapNoRepeatSettleEvent
}
