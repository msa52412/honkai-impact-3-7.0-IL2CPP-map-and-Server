using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UpdateCustomAvatarTeamRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 270
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "TEAM_ID_ERROR")]
		TeamIdError,
		[ProtoEnum(Name = "AVATAR_ID_ERROR")]
		AvatarIdError,
		[ProtoEnum(Name = "AVATAR_NUM_ERROR")]
		AvatarNumError,
		[ProtoEnum(Name = "SENSITIVE_WORDS")]
		SensitiveWords,
		[ProtoEnum(Name = "NAME_LEN_ERROR")]
		NameLenError
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
