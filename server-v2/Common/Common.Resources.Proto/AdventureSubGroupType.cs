using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AdventureSubGroupType
{
	[ProtoEnum(Name = "ADVENTURE_SUB_GROUP_NORMAL")]
	AdventureSubGroupNormal,
	[ProtoEnum(Name = "ADVENTURE_SUB_GROUP_S_AVATAR")]
	AdventureSubGroupSAvatar,
	[ProtoEnum(Name = "ADVENTURE_SUB_GROUP_A_AVATAR")]
	AdventureSubGroupAAvatar
}
