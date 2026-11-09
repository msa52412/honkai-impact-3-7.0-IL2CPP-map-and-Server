using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpTeamEnterLobbyRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 357
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_TEAM")]
		NotInTeam,
		[ProtoEnum(Name = "MEMBER_EXCEED_MAX")]
		MemberExceedMax,
		[ProtoEnum(Name = "MEMBER_OFFLINE")]
		MemberOffline,
		[ProtoEnum(Name = "IN_ENTER_LOBBY")]
		InEnterLobby,
		[ProtoEnum(Name = "ENTER_TIME_OUT")]
		EnterTimeOut,
		[ProtoEnum(Name = "STAGE_ERROR")]
		StageError,
		[ProtoEnum(Name = "NOT_LEADER")]
		NotLeader,
		[ProtoEnum(Name = "MEMBER_NOT_ENTER_TEAM_UI")]
		MemberNotEnterTeamUi,
		[ProtoEnum(Name = "NO_AVAILABLE_AVATAR")]
		NoAvailableAvatar,
		[ProtoEnum(Name = "TEAM_NOT_SATIFY_RAID")]
		TeamNotSatifyRaid,
		[ProtoEnum(Name = "NOT_ALLOW_CROSS_REGION")]
		NotAllowCrossRegion,
		[ProtoEnum(Name = "TEAM_NAME_ERROR")]
		TeamNameError,
		[ProtoEnum(Name = "DEFAULT_TEAM_NAME_ERROR")]
		DefaultTeamNameError
	}

	private IExtension __pbn__extensionData;

	private int? __pbn__SubRetcode;

	private uint? __pbn__ErrorUid;

	private uint? __pbn__StageId;

	private uint? __pbn__TeamId;

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

	[ProtoMember(3, Name = "error_uid")]
	public uint ErrorUid
	{
		get
		{
			return __pbn__ErrorUid.GetValueOrDefault();
		}
		set
		{
			__pbn__ErrorUid = value;
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

	[ProtoMember(5, Name = "team_id")]
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

	[ProtoMember(6, Name = "team_name")]
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

	public bool ShouldSerializeErrorUid()
	{
		return __pbn__ErrorUid.HasValue;
	}

	public void ResetErrorUid()
	{
		__pbn__ErrorUid = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeTeamId()
	{
		return __pbn__TeamId.HasValue;
	}

	public void ResetTeamId()
	{
		__pbn__TeamId = null;
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
