using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatgroupSystemNotifyType
{
	[ProtoEnum(Name = "CHATGROUP_NOTIFY_NONE")]
	ChatgroupNotifyNone,
	[ProtoEnum(Name = "CHATGROUP_NOTIFY_NEW")]
	ChatgroupNotifyNew,
	[ProtoEnum(Name = "CHATGROUP_NOTIFY_JOIN")]
	ChatgroupNotifyJoin,
	[ProtoEnum(Name = "CHATGROUP_NOTIFY_LEAVE")]
	ChatgroupNotifyLeave,
	[ProtoEnum(Name = "CHATGROUP_NOTIFY_DISSOLVE")]
	ChatgroupNotifyDissolve,
	[ProtoEnum(Name = "CHATGROUP_NOTIFY_SETTING")]
	ChatgroupNotifySetting,
	[ProtoEnum(Name = "CHATGROUP_NOTIFY_INVITE")]
	ChatgroupNotifyInvite
}
