using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MonopolyAreaStatus
{
	[ProtoEnum(Name = "MONOPOLY_AREA_STATUS_NONE")]
	MonopolyAreaStatusNone,
	[ProtoEnum(Name = "MONOPOLY_AREA_STATUS_LOCKED")]
	MonopolyAreaStatusLocked,
	[ProtoEnum(Name = "MONOPOLY_AREA_STATUS_UNLOCKED")]
	MonopolyAreaStatusUnlocked
}
