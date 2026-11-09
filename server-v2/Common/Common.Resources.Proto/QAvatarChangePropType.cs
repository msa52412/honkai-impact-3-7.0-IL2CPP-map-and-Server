using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarChangePropType
{
	[ProtoEnum(Name = "QAVATAR_CHANGE_PROP_NONE")]
	QavatarChangePropNone,
	[ProtoEnum(Name = "QAVATAR_CHANGE_PROP_HP")]
	QavatarChangePropHp,
	[ProtoEnum(Name = "QAVATAR_CHANGE_PROP_SP")]
	QavatarChangePropSp
}
