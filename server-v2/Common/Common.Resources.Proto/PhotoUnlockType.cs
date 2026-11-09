using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PhotoUnlockType
{
	[ProtoEnum(Name = "PHOTO_UNLOCK_FINISH_OW_EVENT")]
	PhotoUnlockFinishOwEvent = 1,
	[ProtoEnum(Name = "PHOTO_UNLOCK_KILL_MONSTER")]
	PhotoUnlockKillMonster,
	[ProtoEnum(Name = "PHOTO_UNLOCK_FINISH_OW_STORY")]
	PhotoUnlockFinishOwStory,
	[ProtoEnum(Name = "PHOTO_UNLOCK_FINISH_DLC_DIALOG")]
	PhotoUnlockFinishDlcDialog,
	[ProtoEnum(Name = "PHOTO_UNLOCK_GOD_WAR")]
	PhotoUnlockGodWar,
	[ProtoEnum(Name = "PHOTO_UNLOCK_VISUAL_NOVEL")]
	PhotoUnlockVisualNovel,
	[ProtoEnum(Name = "PHOTO_UNLOCK_MATERIAL")]
	PhotoUnlockMaterial
}
