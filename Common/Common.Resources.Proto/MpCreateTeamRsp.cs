using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpCreateTeamRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 349
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
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
		[ProtoEnum(Name = "FEATURE_LIMIT")]
		FeatureLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TeamId;

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
}
