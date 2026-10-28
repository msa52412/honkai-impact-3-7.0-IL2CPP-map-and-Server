using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleGetLobbyInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5553
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_EXIST")]
		NotExist
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TaleId;

	private uint? __pbn__LobbyId;

	private uint? __pbn__LobbyStatus;

	private uint? __pbn__FightserverIp;

	private uint? __pbn__FightserverPort;

	private uint? __pbn__BattleId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

	[ProtoMember(3, Name = "lobby_id")]
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

	[ProtoMember(4, Name = "lobby_status")]
	public uint LobbyStatus
	{
		get
		{
			return __pbn__LobbyStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__LobbyStatus = value;
		}
	}

	[ProtoMember(5, Name = "fightserver_ip")]
	public uint FightserverIp
	{
		get
		{
			return __pbn__FightserverIp.GetValueOrDefault();
		}
		set
		{
			__pbn__FightserverIp = value;
		}
	}

	[ProtoMember(6, Name = "fightserver_port")]
	public uint FightserverPort
	{
		get
		{
			return __pbn__FightserverPort.GetValueOrDefault();
		}
		set
		{
			__pbn__FightserverPort = value;
		}
	}

	[ProtoMember(7, Name = "battle_id")]
	public uint BattleId
	{
		get
		{
			return __pbn__BattleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleId = value;
		}
	}

	[ProtoMember(8, Name = "member_list")]
	public List<QAvatarBattleMember> MemberLists { get; } = new List<QAvatarBattleMember>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeLobbyId()
	{
		return __pbn__LobbyId.HasValue;
	}

	public void ResetLobbyId()
	{
		__pbn__LobbyId = null;
	}

	public bool ShouldSerializeLobbyStatus()
	{
		return __pbn__LobbyStatus.HasValue;
	}

	public void ResetLobbyStatus()
	{
		__pbn__LobbyStatus = null;
	}

	public bool ShouldSerializeFightserverIp()
	{
		return __pbn__FightserverIp.HasValue;
	}

	public void ResetFightserverIp()
	{
		__pbn__FightserverIp = null;
	}

	public bool ShouldSerializeFightserverPort()
	{
		return __pbn__FightserverPort.HasValue;
	}

	public void ResetFightserverPort()
	{
		__pbn__FightserverPort = null;
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}
}
