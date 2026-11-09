using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DLCAvatarReviveCostType
{
	[ProtoEnum(Name = "DLC_AVATAR_REVIVE_COST_TYPE_NONE")]
	DlcAvatarReviveCostTypeNone,
	[ProtoEnum(Name = "DLC_AVATAR_REVIVE_COST_TYPE_REVIVE_COIN")]
	DlcAvatarReviveCostTypeReviveCoin,
	[ProtoEnum(Name = "DLC_AVATAR_REVIVE_COST_TYPE_COIN")]
	DlcAvatarReviveCostTypeCoin
}
