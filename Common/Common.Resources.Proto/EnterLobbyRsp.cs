using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EnterLobbyRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 304
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LOBBY_FULL")]
		LobbyFull,
		[ProtoEnum(Name = "LEVEL_NOT_SATISFY")]
		LevelNotSatisfy,
		[ProtoEnum(Name = "ALREADY_IN_LOBBY")]
		AlreadyInLobby,
		[ProtoEnum(Name = "LOBBY_FIGHTING")]
		LobbyFighting,
		[ProtoEnum(Name = "LOBBY_NOT_EXIST")]
		LobbyNotExist,
		[ProtoEnum(Name = "NO_LOBBY")]
		NoLobby,
		[ProtoEnum(Name = "STAGE_ERROR")]
		StageError,
		[ProtoEnum(Name = "SERVER_BUSY")]
		ServerBusy,
		[ProtoEnum(Name = "BAD_NETWORK")]
		BadNetwork,
		[ProtoEnum(Name = "CLIENT_VERSION_ERROR")]
		ClientVersionError,
		[ProtoEnum(Name = "IN_MATCH_QUEUE")]
		InMatchQueue,
		[ProtoEnum(Name = "MATCH_STAGE_EXCEED_MAX")]
		MatchStageExceedMax,
		[ProtoEnum(Name = "ALREADY_IN_MATCH")]
		AlreadyInMatch,
		[ProtoEnum(Name = "NO_AVAILABLE_AVATAR")]
		NoAvailableAvatar,
		[ProtoEnum(Name = "NOT_ALLOW_CROSS_REGION")]
		NotAllowCrossRegion,
		[ProtoEnum(Name = "IN_SQUAD")]
		InSquad,
		[ProtoEnum(Name = "IN_SQUAD_MATCH")]
		InSquadMatch,
		[ProtoEnum(Name = "IN_ROOM")]
		InRoom,
		[ProtoEnum(Name = "FEATURE_LIMIT")]
		FeatureLimit,
		[ProtoEnum(Name = "NOT_IN_SAME_ARMADA")]
		NotInSameArmada,
		[ProtoEnum(Name = "DATA_VERSION_CONFLICT")]
		DataVersionConflict,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private int? __pbn__SubRetcode;

	private uint? __pbn__LobbyId;

	private uint? __pbn__StageId;

	private uint? __pbn__AvatarId;

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

	[ProtoMember(5, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
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

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}
}
