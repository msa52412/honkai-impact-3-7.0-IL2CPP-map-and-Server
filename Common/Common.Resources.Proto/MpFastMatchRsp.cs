using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpFastMatchRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1248
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "STAGE_ERROR")]
		StageError,
		[ProtoEnum(Name = "CLIENT_VERSION_ERROR")]
		ClientVersionError,
		[ProtoEnum(Name = "BAD_NETWORK")]
		BadNetwork,
		[ProtoEnum(Name = "SERVER_BUSY")]
		ServerBusy,
		[ProtoEnum(Name = "ALREADY_IN_TEAM")]
		AlreadyInTeam,
		[ProtoEnum(Name = "MATCH_STAGE_EXCEED_MAX")]
		MatchStageExceedMax,
		[ProtoEnum(Name = "IN_SQUAD")]
		InSquad,
		[ProtoEnum(Name = "IN_SQUAD_MATCH")]
		InSquadMatch,
		[ProtoEnum(Name = "IN_ROOM")]
		InRoom
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LobbyId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "stage_id_list")]
	public uint[] StageIdLists { get; set; }

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
}
