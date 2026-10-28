using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum BuffAcquireType
{
	[ProtoEnum(Name = "BUFF_ACQUIRE_GOBACK")]
	BuffAcquireGoback = 1,
	[ProtoEnum(Name = "BUFF_ACQUIRE_ITEMUSE")]
	BuffAcquireItemuse = 3
}
