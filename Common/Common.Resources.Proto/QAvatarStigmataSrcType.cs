using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarStigmataSrcType
{
	[ProtoEnum(Name = "QAVATAR_STIGMATA_SRC_NONE")]
	QavatarStigmataSrcNone,
	[ProtoEnum(Name = "QAVATAR_STIGMATA_SRC_DEFAULT")]
	QavatarStigmataSrcDefault,
	[ProtoEnum(Name = "QAVATAR_STIGMATA_SRC_DROP")]
	QavatarStigmataSrcDrop
}
