using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum BulletinNotShownFlagType
{
	[ProtoEnum(Name = "BULLETIN_NOT_SHOWN_FLAG_TYPE_FINISH_MISSION_AND")]
	BulletinNotShownFlagTypeFinishMissionAnd = 1,
	[ProtoEnum(Name = "BULLETIN_NOT_SHOWN_FLAG_TYPE_FINISH_MISSION_OR")]
	BulletinNotShownFlagTypeFinishMissionOr,
	[ProtoEnum(Name = "BULLETIN_NOT_SHOWN_FLAG_TYPE_HAS_GOT_ITEM_AND")]
	BulletinNotShownFlagTypeHasGotItemAnd,
	[ProtoEnum(Name = "BULLETIN_NOT_SHOWN_FLAG_TYPE_HAS_GOT_ITEM_OR")]
	BulletinNotShownFlagTypeHasGotItemOr
}
