using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum StorySweepGroupType
{
	[ProtoEnum(Name = "STORY_SWEEP_GROUP_AVATAR_FRAGMENT")]
	StorySweepGroupAvatarFragment = 1,
	[ProtoEnum(Name = "STORY_SWEEP_GROUP_MATERIAL")]
	StorySweepGroupMaterial,
	[ProtoEnum(Name = "STORY_SWEEP_GROUP_ACTIVITY")]
	StorySweepGroupActivity,
	[ProtoEnum(Name = "STORY_SWEEP_GROUP_MATERIAL_NEW")]
	StorySweepGroupMaterialNew
}
