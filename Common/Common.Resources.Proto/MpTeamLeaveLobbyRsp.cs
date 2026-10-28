using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpTeamLeaveLobbyRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 374
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_LEADER")]
		NotLeader,
		[ProtoEnum(Name = "NOT_IN_TEAM")]
		NotInTeam,
		[ProtoEnum(Name = "NOT_IN_LOBBY")]
		NotInLobby,
		[ProtoEnum(Name = "LEADER_IS_WILD")]
		LeaderIsWild
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
