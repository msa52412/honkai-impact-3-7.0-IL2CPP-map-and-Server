using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MpAction
{
	[ProtoEnum(Name = "MP_ACTION_ERROR")]
	MpActionError,
	[ProtoEnum(Name = "MP_ACTION_CLOSE_MVP_PAGE")]
	MpActionCloseMvpPage,
	[ProtoEnum(Name = "MP_ACTION_CLOSE_LIKE_PAGE")]
	MpActionCloseLikePage
}
