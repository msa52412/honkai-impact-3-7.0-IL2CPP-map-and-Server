using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum StageTagRestrictType
{
	[ProtoEnum(Name = "STAGE_TAG_RESTRICT_NONE")]
	StageTagRestrictNone,
	[ProtoEnum(Name = "STAGE_TAG_RESTRICT_TAG")]
	StageTagRestrictTag,
	[ProtoEnum(Name = "STAGE_TAG_RESTRICT_NO_TAG")]
	StageTagRestrictNoTag,
	[ProtoEnum(Name = "STAGE_TAG_RESTRICT_ATTRIBUTE")]
	StageTagRestrictAttribute,
	[ProtoEnum(Name = "STAGE_TAG_RESTRICT_NO_ATTRIBUTE")]
	StageTagRestrictNoAttribute,
	[ProtoEnum(Name = "STAGE_TAG_RESTRICT_ROLE")]
	StageTagRestrictRole,
	[ProtoEnum(Name = "STAGE_TAG_RESTRICT_NO_ROLE")]
	StageTagRestrictNoRole
}
