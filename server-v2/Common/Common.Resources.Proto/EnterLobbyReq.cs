using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EnterLobbyReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 303
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__LobbyId;

	private bool? __pbn__IsFriendInvitation;

	private uint? __pbn__Level;

	private uint? __pbn__ClientRtt;

	private bool? __pbn__HasLeaderStamina;

	private bool? __pbn__HasLeaderEnterTimes;

	private uint? __pbn__DeviceScore;

	private uint? __pbn__ReportNum;

	private bool? __pbn__HasLeaderThemeWantedTicket;

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

	[ProtoMember(2, Name = "lobby_id")]
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

	[ProtoMember(3, Name = "is_friend_invitation")]
	public bool IsFriendInvitation
	{
		get
		{
			return __pbn__IsFriendInvitation.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFriendInvitation = value;
		}
	}

	[ProtoMember(4, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(6, Name = "client_rtt")]
	public uint ClientRtt
	{
		get
		{
			return __pbn__ClientRtt.GetValueOrDefault();
		}
		set
		{
			__pbn__ClientRtt = value;
		}
	}

	[ProtoMember(7, Name = "has_leader_stamina")]
	public bool HasLeaderStamina
	{
		get
		{
			return __pbn__HasLeaderStamina.GetValueOrDefault();
		}
		set
		{
			__pbn__HasLeaderStamina = value;
		}
	}

	[ProtoMember(8, Name = "member")]
	public MpTeamMember Member { get; set; }

	[ProtoMember(9, Name = "has_leader_enter_times")]
	public bool HasLeaderEnterTimes
	{
		get
		{
			return __pbn__HasLeaderEnterTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__HasLeaderEnterTimes = value;
		}
	}

	[ProtoMember(10, Name = "device_score")]
	public uint DeviceScore
	{
		get
		{
			return __pbn__DeviceScore.GetValueOrDefault();
		}
		set
		{
			__pbn__DeviceScore = value;
		}
	}

	[ProtoMember(11, Name = "report_num")]
	public uint ReportNum
	{
		get
		{
			return __pbn__ReportNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ReportNum = value;
		}
	}

	[ProtoMember(12, Name = "has_leader_theme_wanted_ticket")]
	public bool HasLeaderThemeWantedTicket
	{
		get
		{
			return __pbn__HasLeaderThemeWantedTicket.GetValueOrDefault();
		}
		set
		{
			__pbn__HasLeaderThemeWantedTicket = value;
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

	public bool ShouldSerializeLobbyId()
	{
		return __pbn__LobbyId.HasValue;
	}

	public void ResetLobbyId()
	{
		__pbn__LobbyId = null;
	}

	public bool ShouldSerializeIsFriendInvitation()
	{
		return __pbn__IsFriendInvitation.HasValue;
	}

	public void ResetIsFriendInvitation()
	{
		__pbn__IsFriendInvitation = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeClientRtt()
	{
		return __pbn__ClientRtt.HasValue;
	}

	public void ResetClientRtt()
	{
		__pbn__ClientRtt = null;
	}

	public bool ShouldSerializeHasLeaderStamina()
	{
		return __pbn__HasLeaderStamina.HasValue;
	}

	public void ResetHasLeaderStamina()
	{
		__pbn__HasLeaderStamina = null;
	}

	public bool ShouldSerializeHasLeaderEnterTimes()
	{
		return __pbn__HasLeaderEnterTimes.HasValue;
	}

	public void ResetHasLeaderEnterTimes()
	{
		__pbn__HasLeaderEnterTimes = null;
	}

	public bool ShouldSerializeDeviceScore()
	{
		return __pbn__DeviceScore.HasValue;
	}

	public void ResetDeviceScore()
	{
		__pbn__DeviceScore = null;
	}

	public bool ShouldSerializeReportNum()
	{
		return __pbn__ReportNum.HasValue;
	}

	public void ResetReportNum()
	{
		__pbn__ReportNum = null;
	}

	public bool ShouldSerializeHasLeaderThemeWantedTicket()
	{
		return __pbn__HasLeaderThemeWantedTicket.HasValue;
	}

	public void ResetHasLeaderThemeWantedTicket()
	{
		__pbn__HasLeaderThemeWantedTicket = null;
	}
}
