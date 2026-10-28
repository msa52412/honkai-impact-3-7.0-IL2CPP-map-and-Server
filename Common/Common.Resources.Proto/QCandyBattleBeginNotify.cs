using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleBeginNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6603
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LobbyId;

	private uint? __pbn__BattleId;

	private uint? __pbn__MapId;

	private uint? __pbn__FightserverIp;

	private uint? __pbn__FightserverPort;

	private uint? __pbn__ActivityId;

	[ProtoMember(1, Name = "lobby_id")]
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

	[ProtoMember(2, Name = "battle_id")]
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

	[ProtoMember(3, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(4, Name = "fightserver_ip")]
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

	[ProtoMember(5, Name = "fightserver_port")]
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

	[ProtoMember(6, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(7, Name = "best_record")]
	public QCandyBattleSettleInfo BestRecord { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLobbyId()
	{
		return __pbn__LobbyId.HasValue;
	}

	public void ResetLobbyId()
	{
		__pbn__LobbyId = null;
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
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

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}
}
