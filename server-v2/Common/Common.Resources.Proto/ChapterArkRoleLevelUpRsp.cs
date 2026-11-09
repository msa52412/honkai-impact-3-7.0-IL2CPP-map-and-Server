using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterArkRoleLevelUpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7953
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ROLE_ERROR")]
		RoleError,
		[ProtoEnum(Name = "NO_UNLOCK_AVATAR")]
		NoUnlockAvatar,
		[ProtoEnum(Name = "TARGET_LEVEL_ERROR")]
		TargetLevelError,
		[ProtoEnum(Name = "LEVEL_LIMIT")]
		LevelLimit,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chapter_id")]
	public uint ChapterId { get; set; }

	[ProtoMember(3, Name = "role_id")]
	public uint RoleId { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
