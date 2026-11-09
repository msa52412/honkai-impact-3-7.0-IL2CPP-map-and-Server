using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DreamUnlockType
{
	[ProtoEnum(Name = "DREAM_UNLOCK_TYPE_NO_NEWBIE_SCHEDULE")]
	DreamUnlockTypeNoNewbieSchedule = 1,
	[ProtoEnum(Name = "DREAM_UNLOCK_TYPE_HAS_NOT_FINISHED_DREAM")]
	DreamUnlockTypeHasNotFinishedDream,
	[ProtoEnum(Name = "DREAM_UNLOCK_TYPE_GOBACK_LEVEL")]
	DreamUnlockTypeGobackLevel,
	[ProtoEnum(Name = "DREAM_UNLOCK_TYPE_AVATAR_LOCKED")]
	DreamUnlockTypeAvatarLocked,
	[ProtoEnum(Name = "DREAM_UNLOCK_TYPE_HAS_NOT_GOT_ITEM")]
	DreamUnlockTypeHasNotGotItem
}
