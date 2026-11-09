using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DLCAvatarReviveType
{
	[ProtoEnum(Name = "DLC_AVATAR_REVIVE_TYPE_NONE")]
	DlcAvatarReviveTypeNone,
	[ProtoEnum(Name = "DLC_AVATAR_REVIVE_TYPE_DLC_TOWER")]
	DlcAvatarReviveTypeDlcTower,
	[ProtoEnum(Name = "DLC_AVATAR_REVIVE_TYPE_DLC_STAGE")]
	DlcAvatarReviveTypeDlcStage,
	[ProtoEnum(Name = "DLC_AVATAR_REVIVE_TYPE_OUTER_STAGE")]
	DlcAvatarReviveTypeOuterStage,
	[ProtoEnum(Name = "DLC_AVATAR_REVIVE_TYPE_DLC_OW")]
	DlcAvatarReviveTypeDlcOw
}
