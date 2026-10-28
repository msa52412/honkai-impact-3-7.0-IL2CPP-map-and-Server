using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LobbyStageBeginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 312
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
		[ProtoEnum(Name = "LOBBY_FIGHTING")]
		LobbyFighting,
		[ProtoEnum(Name = "NOT_LEADER")]
		NotLeader,
		[ProtoEnum(Name = "MEMBER_STATUS_ERROR")]
		MemberStatusError,
		[ProtoEnum(Name = "SYSTEM_ERROR")]
		SystemError,
		[ProtoEnum(Name = "MEMBER_NOT_ENTER_LOBBY")]
		MemberNotEnterLobby,
		[ProtoEnum(Name = "MEMBER_NOT_ONLINE")]
		MemberNotOnline,
		[ProtoEnum(Name = "MEMBER_DUPLICATE")]
		MemberDuplicate,
		[ProtoEnum(Name = "MEMBER_NOT_ENOUGH")]
		MemberNotEnough
	}

	private IExtension __pbn__extensionData;

	private string __pbn__FightServerIp;

	private uint? __pbn__FightServerPort;

	private uint? __pbn__LobbyId;

	private uint? __pbn__LeaderUid;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "fight_server_ip")]
	[DefaultValue("")]
	public string FightServerIp
	{
		get
		{
			return __pbn__FightServerIp ?? "";
		}
		set
		{
			__pbn__FightServerIp = value;
		}
	}

	[ProtoMember(3, Name = "fight_server_port")]
	public uint FightServerPort
	{
		get
		{
			return __pbn__FightServerPort.GetValueOrDefault();
		}
		set
		{
			__pbn__FightServerPort = value;
		}
	}

	[ProtoMember(4, Name = "lobby_id")]
	public uint LobbyId
	{
		get
		{
			return __pbn__LobbyId.GetValueOrDefault();
		}
		set
		{
			__pbn__LobbyId = value;
		}
	}

	[ProtoMember(5, Name = "uid_list")]
	public uint[] UidLists { get; set; }

	[ProtoMember(6, Name = "leader_uid")]
	public uint LeaderUid
	{
		get
		{
			return __pbn__LeaderUid.GetValueOrDefault();
		}
		set
		{
			__pbn__LeaderUid = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFightServerIp()
	{
		return __pbn__FightServerIp != null;
	}

	public void ResetFightServerIp()
	{
		__pbn__FightServerIp = null;
	}

	public bool ShouldSerializeFightServerPort()
	{
		return __pbn__FightServerPort.HasValue;
	}

	public void ResetFightServerPort()
	{
		__pbn__FightServerPort = null;
	}

	public bool ShouldSerializeLobbyId()
	{
		return __pbn__LobbyId.HasValue;
	}

	public void ResetLobbyId()
	{
		__pbn__LobbyId = null;
	}

	public bool ShouldSerializeLeaderUid()
	{
		return __pbn__LeaderUid.HasValue;
	}

	public void ResetLeaderUid()
	{
		__pbn__LeaderUid = null;
	}
}
