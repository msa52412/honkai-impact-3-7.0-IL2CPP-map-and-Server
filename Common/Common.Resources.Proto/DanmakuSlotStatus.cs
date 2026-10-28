using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DanmakuSlotStatus
{
	[ProtoEnum(Name = "DANMAKU_SLOT_STATUS_NONE")]
	DanmakuSlotStatusNone,
	[ProtoEnum(Name = "DANMAKU_SLOT_STATUS_CLOSE")]
	DanmakuSlotStatusClose,
	[ProtoEnum(Name = "DANMAKU_SLOT_STATUS_FAKE")]
	DanmakuSlotStatusFake
}
