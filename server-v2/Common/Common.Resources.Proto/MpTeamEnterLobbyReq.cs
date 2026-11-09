using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpTeamEnterLobbyReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 356
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private LobbyEnterType? __pbn__LobbyEnterType;

	private uint? __pbn__MinLevel;

	private string __pbn__TeamName;

	private bool? __pbn__IsTeamNameDefault;

	private uint? __pbn__MaxLevel;

	[ProtoMember(1, Name = "stage_id")]
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

	[ProtoMember(2, Name = "lobby_enter_type")]
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

	[ProtoMember(3, Name = "min_level")]
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

	[ProtoMember(4, Name = "team_name")]
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

	[ProtoMember(6, Name = "is_team_name_default")]
	public bool IsTeamNameDefault
	{
		get
		{
			return __pbn__IsTeamNameDefault.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTeamNameDefault = value;
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeLobbyEnterType()
	{
		return __pbn__LobbyEnterType.HasValue;
	}

	public void ResetLobbyEnterType()
	{
		__pbn__LobbyEnterType = null;
	}

	public bool ShouldSerializeMinLevel()
	{
		return __pbn__MinLevel.HasValue;
	}

	public void ResetMinLevel()
	{
		__pbn__MinLevel = null;
	}

	public bool ShouldSerializeTeamName()
	{
		return __pbn__TeamName != null;
	}

	public void ResetTeamName()
	{
		__pbn__TeamName = null;
	}

	public bool ShouldSerializeIsTeamNameDefault()
	{
		return __pbn__IsTeamNameDefault.HasValue;
	}

	public void ResetIsTeamNameDefault()
	{
		__pbn__IsTeamNameDefault = null;
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
