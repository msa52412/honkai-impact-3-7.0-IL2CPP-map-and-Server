using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleBeginNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5550
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TaleId;

	private uint? __pbn__SiteId;

	private uint? __pbn__FightserverIp;

	private uint? __pbn__FightserverPort;

	private uint? __pbn__ChatworldId;

	private uint? __pbn__SceneId;

	private uint? __pbn__LobbyId;

	private uint? __pbn__BattleId;

	private uint? __pbn__BattleStartTime;

	[ProtoMember(1, Name = "tale_id")]
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

	[ProtoMember(2, Name = "site_id")]
	public uint SiteId
	{
		get
		{
			return __pbn__SiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__SiteId = value;
		}
	}

	[ProtoMember(3, Name = "fightserver_ip")]
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

	[ProtoMember(4, Name = "fightserver_port")]
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

	[ProtoMember(5, Name = "chatworld_id")]
	public uint ChatworldId
	{
		get
		{
			return __pbn__ChatworldId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatworldId = value;
		}
	}

	[ProtoMember(6, Name = "scene_id")]
	public uint SceneId
	{
		get
		{
			return __pbn__SceneId.GetValueOrDefault();
		}
		set
		{
			__pbn__SceneId = value;
		}
	}

	[ProtoMember(10, Name = "lobby_id")]
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

	[ProtoMember(11, Name = "battle_id")]
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

	[ProtoMember(12, Name = "battle_start_time")]
	public uint BattleStartTime
	{
		get
		{
			return __pbn__BattleStartTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleStartTime = value;
		}
	}

	[ProtoMember(13, Name = "member_list")]
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

	public bool ShouldSerializeSiteId()
	{
		return __pbn__SiteId.HasValue;
	}

	public void ResetSiteId()
	{
		__pbn__SiteId = null;
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

	public bool ShouldSerializeChatworldId()
	{
		return __pbn__ChatworldId.HasValue;
	}

	public void ResetChatworldId()
	{
		__pbn__ChatworldId = null;
	}

	public bool ShouldSerializeSceneId()
	{
		return __pbn__SceneId.HasValue;
	}

	public void ResetSceneId()
	{
		__pbn__SceneId = null;
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

	public bool ShouldSerializeBattleStartTime()
	{
		return __pbn__BattleStartTime.HasValue;
	}

	public void ResetBattleStartTime()
	{
		__pbn__BattleStartTime = null;
	}
}
