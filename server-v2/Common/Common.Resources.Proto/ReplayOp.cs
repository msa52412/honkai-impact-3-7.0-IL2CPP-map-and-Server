using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ReplayOp
{
	[ProtoEnum(Name = "REPLAY_UPLOAD")]
	ReplayUpload = 1,
	[ProtoEnum(Name = "REPLAY_DELETE")]
	ReplayDelete
}
