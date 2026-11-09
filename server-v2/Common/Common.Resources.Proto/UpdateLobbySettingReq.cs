using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UpdateLobbySettingReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1365
	}

	private IExtension __pbn__extensionData;

	private string __pbn__TeamName;

	private uint? __pbn__MinLevel;

	private LobbyEnterType? __pbn__LobbyEnterType;

	private uint? __pbn__MaxLevel;

	[ProtoMember(1, Name = "team_name")]
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

	[ProtoMember(4, Name = "lobby_enter_type")]
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

	[ProtoMember(5, Name = "max_level")]
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

	public bool ShouldSerializeTeamName()
	{
		return __pbn__TeamName != null;
	}

	public void ResetTeamName()
	{
		__pbn__TeamName = null;
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
}
