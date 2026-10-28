using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SwitchAvatarSubSkillRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 254
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
		[ProtoEnum(Name = "SUB_SKILL_CAN_NOT_SWITCH")]
		SubSkillCanNotSwitch
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
