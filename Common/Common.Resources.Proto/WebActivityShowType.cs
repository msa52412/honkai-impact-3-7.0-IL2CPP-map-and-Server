using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WebActivityShowType
{
	[ProtoEnum(Name = "WEB_ACTIVITY_SHOW_TYPE_NONE")]
	WebActivityShowTypeNone,
	[ProtoEnum(Name = "WEB_ACTIVITY_SHOW_TYPE_TRACK_POINT")]
	WebActivityShowTypeTrackPoint,
	[ProtoEnum(Name = "WEB_ACTIVITY_SHOW_TYPE_FINISH")]
	WebActivityShowTypeFinish,
	[ProtoEnum(Name = "WEB_ACTIVITY_SHOW_TYPE_HIDE")]
	WebActivityShowTypeHide
}
