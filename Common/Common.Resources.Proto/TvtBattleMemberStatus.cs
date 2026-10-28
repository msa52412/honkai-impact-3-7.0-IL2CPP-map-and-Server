using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TvtBattleMemberStatus
{
	[ProtoEnum(Name = "TVT_BATTLE_MEMBER_STATUS_NONE")]
	TvtBattleMemberStatusNone,
	[ProtoEnum(Name = "TVT_BATTLE_MEMBER_STATUS_ONLINE")]
	TvtBattleMemberStatusOnline,
	[ProtoEnum(Name = "TVT_BATTLE_MEMBER_STATUS_OFFLINE")]
	TvtBattleMemberStatusOffline
}
