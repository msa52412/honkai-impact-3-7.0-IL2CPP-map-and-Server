using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterArk : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "chapter_id")]
	public uint ChapterId { get; set; }

	[ProtoMember(2, Name = "role_list")]
	public List<ChapterArkRoleInfo> RoleLists { get; } = new List<ChapterArkRoleInfo>();


	[ProtoMember(3, Name = "avatar_list", IsPacked = true)]
	public uint[] AvatarLists { get; set; }

	[ProtoMember(4, Name = "skill_list")]
	public List<ChapterArkSkillInfo> SkillLists { get; } = new List<ChapterArkSkillInfo>();


	[ProtoMember(5, Name = "daily_stage_list", IsPacked = true)]
	public uint[] DailyStageLists { get; set; }

	[ProtoMember(6, Name = "reward_line_taken_progress")]
	public uint RewardLineTakenProgress { get; set; }

	[ProtoMember(7, Name = "is_finish_opening")]
	public bool IsFinishOpening { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
