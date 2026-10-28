using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpTeamData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TeamId;

	private uint? __pbn__LeaderUid;

	private MpTeamStatus? __pbn__Status;

	private LobbyStatus? __pbn__LobbyStatus;

	private uint? __pbn__MinLevel;

	private LobbyEnterType? __pbn__LobbyEnterType;

	private uint? __pbn__StageId;

	private string __pbn__TeamName;

	private uint? __pbn__MaxLevel;

	[ProtoMember(1, Name = "team_id")]
	public uint TeamId
	{
		get
		{
			return __pbn__TeamId.GetValueOrDefault();
		}
		set
		{
			__pbn__TeamId = value;
		}
	}

	[ProtoMember(2, Name = "leader_uid")]
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

	[ProtoMember(3, Name = "status")]
	[DefaultValue(MpTeamStatus.TeamStatusNone)]
	public MpTeamStatus Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(4, Name = "lobby_status")]
	[DefaultValue(LobbyStatus.LobbyNone)]
	public LobbyStatus LobbyStatus
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

	[ProtoMember(5, Name = "member_list")]
	public List<MpTeamMember> MemberLists { get; } = new List<MpTeamMember>();


	[ProtoMember(6, Name = "min_level")]
	public uint MinLevel
	{
		get
		{
			return __pbn__MinLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MinLevel = value;
		}
	}

	[ProtoMember(7, Name = "lobby_enter_type")]
	[DefaultValue(LobbyEnterType.LobbyEnterTypeNone)]
	public LobbyEnterType LobbyEnterType
	{
		get
		{
			return __pbn__LobbyEnterType.GetValueOrDefault();
		}
		set
		{
			__pbn__LobbyEnterType = value;
		}
	}

	[ProtoMember(8, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(9, Name = "team_name")]
	[DefaultValue("")]
	public string TeamName
	{
		get
		{
			return __pbn__TeamName ?? "";
		}
		set
		{
			__pbn__TeamName = value;
		}
	}

	[ProtoMember(11, Name = "max_level")]
	public uint MaxLevel
	{
		get
		{
			return __pbn__MaxLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTeamId()
	{
		return __pbn__TeamId.HasValue;
	}

	public void ResetTeamId()
	{
		__pbn__TeamId = null;
	}

	public bool ShouldSerializeLeaderUid()
	{
		return __pbn__LeaderUid.HasValue;
	}

	public void ResetLeaderUid()
	{
		__pbn__LeaderUid = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeLobbyStatus()
	{
		return __pbn__LobbyStatus.HasValue;
	}

	public void ResetLobbyStatus()
	{
		__pbn__LobbyStatus = null;
	}

	public bool ShouldSerializeMinLevel()
	{
		return __pbn__MinLevel.HasValue;
	}

	public void ResetMinLevel()
	{
		__pbn__MinLevel = null;
	}

	public bool ShouldSerializeLobbyEnterType()
	{
		return __pbn__LobbyEnterType.HasValue;
	}

	public void ResetLobbyEnterType()
	{
		__pbn__LobbyEnterType = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeTeamName()
	{
		return __pbn__TeamName != null;
	}

	public void ResetTeamName()
	{
		__pbn__TeamName = null;
	}

	public bool ShouldSerializeMaxLevel()
	{
		return __pbn__MaxLevel.HasValue;
	}

	public void ResetMaxLevel()
	{
		__pbn__MaxLevel = null;
	}
}
