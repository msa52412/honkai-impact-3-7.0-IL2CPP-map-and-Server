using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpEnterTeamRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 351
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "TEAM_NOT_EXIST")]
		TeamNotExist,
		[ProtoEnum(Name = "TEAM_FULL")]
		TeamFull,
		[ProtoEnum(Name = "TEAM_ENTER_LOBBY")]
		TeamEnterLobby,
		[ProtoEnum(Name = "TEAM_IN_LOBBY")]
		TeamInLobby,
		[ProtoEnum(Name = "TEAM_IN_FIGHT")]
		TeamInFight,
		[ProtoEnum(Name = "ALREADY_IN_TEAM")]
		AlreadyInTeam,
		[ProtoEnum(Name = "CLIENT_VERSION_ERROR")]
		ClientVersionError,
		[ProtoEnum(Name = "SERVER_BUSY")]
		ServerBusy,
		[ProtoEnum(Name = "IN_SQUAD")]
		InSquad,
		[ProtoEnum(Name = "IN_SQUAD_MATCH")]
		InSquadMatch,
		[ProtoEnum(Name = "IN_ROOM")]
		InRoom,
		[ProtoEnum(Name = "CONDITION_NOT_MEET")]
		ConditionNotMeet,
		[ProtoEnum(Name = "FEATURE_LIMIT")]
		FeatureLimit,
		[ProtoEnum(Name = "DATA_VERSION_CONFLICT")]
		DataVersionConflict
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TeamId;

	private uint? __pbn__StageId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "team_id")]
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

	[ProtoMember(3, Name = "stage_id")]
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

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}
}
