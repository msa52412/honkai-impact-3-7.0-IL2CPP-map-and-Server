using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DialogType
{
	[ProtoEnum(Name = "DIALOG_TYPE_NONE")]
	DialogTypeNone,
	[ProtoEnum(Name = "DIALOG_TYPE_CONTENT")]
	DialogTypeContent,
	[ProtoEnum(Name = "DIALOG_TYPE_SELECT")]
	DialogTypeSelect,
	[ProtoEnum(Name = "DIALOG_TYPE_HIDE")]
	DialogTypeHide
}
