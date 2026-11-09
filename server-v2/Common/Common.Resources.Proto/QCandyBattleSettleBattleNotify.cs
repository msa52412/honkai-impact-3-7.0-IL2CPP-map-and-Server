using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleSettleBattleNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6608
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__LobbyId;

	private uint? __pbn__MapId;

	private uint? __pbn__BattleId;

	private uint? __pbn__FightserverIp;

	private uint? __pbn__SettleTime;

	[ProtoMember(1, Name = "activity_id")]
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

	[ProtoMember(2, Name = "lobby_id")]
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

	[ProtoMember(4, Name = "settle_info_list")]
	public List<QCandyBattleMemberSettleInfo> SettleInfoLists { get; } = new List<QCandyBattleMemberSettleInfo>();


	[ProtoMember(5, Name = "battle_id")]
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

	[ProtoMember(6, Name = "fightserver_ip")]
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

	[ProtoMember(7, Name = "settle_time")]
	public uint SettleTime
	{
		get
		{
			return __pbn__SettleTime.GetValueOrDefault();
		}
		set
		{
			__pbn__SettleTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeLobbyId()
	{
		return __pbn__LobbyId.HasValue;
	}

	public void ResetLobbyId()
	{
		__pbn__LobbyId = null;
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}

	public bool ShouldSerializeFightserverIp()
	{
		return __pbn__FightserverIp.HasValue;
	}

	public void ResetFightserverIp()
	{
		__pbn__FightserverIp = null;
	}

	public bool ShouldSerializeSettleTime()
	{
		return __pbn__SettleTime.HasValue;
	}

	public void ResetSettleTime()
	{
		__pbn__SettleTime = null;
	}
}
