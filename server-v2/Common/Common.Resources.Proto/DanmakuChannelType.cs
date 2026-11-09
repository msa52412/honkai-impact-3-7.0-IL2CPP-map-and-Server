using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DanmakuChannelType
{
	[ProtoEnum(Name = "DANMAKU_CHANNEL_NONE")]
	DanmakuChannelNone,
	[ProtoEnum(Name = "DANMAKU_CHANNEL_NORMAL")]
	DanmakuChannelNormal,
	[ProtoEnum(Name = "DANMAKU_CHANNEL_KINGDOM")]
	DanmakuChannelKingdom
}
