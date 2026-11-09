using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CreateLobbyRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 302
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "STAGE_ERROR")]
		StageError = 2,
		[ProtoEnum(Name = "SERVER_BUSY")]
		ServerBusy = 3,
		[ProtoEnum(Name = "BAD_NETWORK")]
		BadNetwork = 4,
		[ProtoEnum(Name = "CLIENT_VERSION_ERROR")]
		ClientVersionError = 5,
		[ProtoEnum(Name = "NOT_ALLOWED")]
		NotAllowed = 6,
		[ProtoEnum(Name = "NO_AVAILABLE_AVATAR")]
		NoAvailableAvatar = 7,
		[ProtoEnum(Name = "TEAM_NAME_ERROR")]
		TeamNameError = 8,
		[ProtoEnum(Name = "IN_SQUAD")]
		InSquad = 9,
		[ProtoEnum(Name = "IN_SQUAD_MATCH")]
		InSquadMatch = 10,
		[ProtoEnum(Name = "IN_ROOM")]
		InRoom = 12,
		[ProtoEnum(Name = "FEATURE_LIMIT")]
		FeatureLimit = 13,
		[ProtoEnum(Name = "MEMBER_LIMIT")]
		MemberLimit = 14,
		[ProtoEnum(Name = "DEFAULT_TEAM_NAME_ERROR")]
		DefaultTeamNameError = 15
	}

	private IExtension __pbn__extensionData;

	private int? __pbn__SubRetcode;

	private uint? __pbn__LobbyId;

	private uint? __pbn__StageId;

	private uint? __pbn__MinLevel;

	private LobbyEnterType? __pbn__LobbyEnterType;

	private uint? __pbn__MaxLevel;

	private string __pbn__TeamName;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "sub_retcode")]
	public int SubRetcode
	{
		get
		{
			return __pbn__SubRetcode.GetValueOrDefault();
		}
		set
		{
			__pbn__SubRetcode = value;
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

	[ProtoMember(4, Name = "stage_id")]
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

	[ProtoMember(5, Name = "min_level")]
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

	[ProtoMember(6, Name = "lobby_enter_type")]
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

	[ProtoMember(7, Name = "max_level")]
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

	[ProtoMember(8, Name = "team_name")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSubRetcode()
	{
		return __pbn__SubRetcode.HasValue;
	}

	public void ResetSubRetcode()
	{
		__pbn__SubRetcode = null;
	}

	public bool ShouldSerializeLobbyId()
	{
		return __pbn__LobbyId.HasValue;
	}

	public void ResetLobbyId()
	{
		__pbn__LobbyId = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
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

	public bool ShouldSerializeMaxLevel()
	{
		return __pbn__MaxLevel.HasValue;
	}

	public void ResetMaxLevel()
	{
		__pbn__MaxLevel = null;
	}

	public bool ShouldSerializeTeamName()
	{
		return __pbn__TeamName != null;
	}

	public void ResetTeamName()
	{
		__pbn__TeamName = null;
	}
}
