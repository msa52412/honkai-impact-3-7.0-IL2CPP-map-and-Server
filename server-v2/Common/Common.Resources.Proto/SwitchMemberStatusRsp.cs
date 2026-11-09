using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SwitchMemberStatusRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 310
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_LOBBY")]
		NotInLobby,
		[ProtoEnum(Name = "STATUS_ERROR")]
		StatusError,
		[ProtoEnum(Name = "IS_LEADER")]
		IsLeader
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
