using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetReclaimAvatarTeamRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1172
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
		[ProtoEnum(Name = "AVATAR_REPEATED")]
		AvatarRepeated,
		[ProtoEnum(Name = "AVATAR_EMPTY")]
		AvatarEmpty,
		[ProtoEnum(Name = "TEAM_SIZE_LIMIT")]
		TeamSizeLimit
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
