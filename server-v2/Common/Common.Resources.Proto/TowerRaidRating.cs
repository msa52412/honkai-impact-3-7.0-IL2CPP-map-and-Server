using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TowerRaidRating
{
	[ProtoEnum(Name = "TOWER_RAID_RATING_NONE")]
	TowerRaidRatingNone,
	[ProtoEnum(Name = "TOWER_RAID_RATING_DEGRADE")]
	TowerRaidRatingDegrade,
	[ProtoEnum(Name = "TOWER_RAID_RATING_KEEP")]
	TowerRaidRatingKeep,
	[ProtoEnum(Name = "TOWER_RAID_RATING_UPGRADE")]
	TowerRaidRatingUpgrade
}
