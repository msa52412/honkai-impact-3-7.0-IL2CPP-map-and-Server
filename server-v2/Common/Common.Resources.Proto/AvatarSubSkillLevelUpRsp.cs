using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarSubSkillLevelUpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 51
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "AVATAR_NOT_EXIST")]
		AvatarNotExist,
		[ProtoEnum(Name = "SKILL_NOT_EXIST")]
		SkillNotExist,
		[ProtoEnum(Name = "SUB_SKILL_NOT_EXIST")]
		SubSkillNotExist,
		[ProtoEnum(Name = "AVATAR_LEVEL_LACK")]
		AvatarLevelLack,
		[ProtoEnum(Name = "AVATAR_STAR_LACK")]
		AvatarStarLack,
		[ProtoEnum(Name = "SCOIN_LACK")]
		ScoinLack,
		[ProtoEnum(Name = "SKILL_POINT_LACK")]
		SkillPointLack,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "LEVEL_FULL")]
		LevelFull
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
