using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RoomKickMemberRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3176
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ROOM")]
		NotInRoom,
		[ProtoEnum(Name = "NOT_LEADER")]
		NotLeader,
		[ProtoEnum(Name = "NO_TARGET")]
		NoTarget,
		[ProtoEnum(Name = "SELF")]
		Self
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
