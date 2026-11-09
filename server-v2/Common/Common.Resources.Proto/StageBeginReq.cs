using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageBeginReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 43
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private AvatarTeamType? __pbn__AvatarTeamType;

	private uint? __pbn__AssistantUid;

	private bool? __pbn__IsSpeedUpStage;

	private uint? __pbn__TargetWave;

	private uint? __pbn__GratuityStageUniqueId;

	private bool? __pbn__IsGratuityBonus;

	private string __pbn__MtpReportType;

	private string __pbn__MtpReportValue;

	private bool? __pbn__IsReBegin;

	private bool? __pbn__IsThemeWantedMpAssistMode;

	private uint? __pbn__ScDlcSupportAvatarId;

	private uint? __pbn__StageBeginFloor;

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

	[ProtoMember(2, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	[ProtoMember(3, Name = "avatar_team_type")]
	[DefaultValue(AvatarTeamType.AvatarTeamNormal)]
	public AvatarTeamType AvatarTeamType
	{
		get
		{
			return __pbn__AvatarTeamType ?? AvatarTeamType.AvatarTeamNormal;
		}
		set
		{
			__pbn__AvatarTeamType = value;
		}
	}

	[ProtoMember(4, Name = "assistant_uid")]
	public uint AssistantUid
	{
		get
		{
			return __pbn__AssistantUid.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistantUid = value;
		}
	}

	[ProtoMember(5, Name = "is_speed_up_stage")]
	public bool IsSpeedUpStage
	{
		get
		{
			return __pbn__IsSpeedUpStage.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSpeedUpStage = value;
		}
	}

	[ProtoMember(6, Name = "avatar_trial_id_list")]
	public uint[] AvatarTrialIdLists { get; set; }

	[ProtoMember(7, Name = "elf_id_list")]
	public uint[] ElfIdLists { get; set; }

	[ProtoMember(8, Name = "target_wave")]
	public uint TargetWave
	{
		get
		{
			return __pbn__TargetWave.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetWave = value;
		}
	}

	[ProtoMember(9, Name = "gratuity_stage_unique_id")]
	public uint GratuityStageUniqueId
	{
		get
		{
			return __pbn__GratuityStageUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__GratuityStageUniqueId = value;
		}
	}

	[ProtoMember(10, Name = "is_gratuity_bonus")]
	public bool IsGratuityBonus
	{
		get
		{
			return __pbn__IsGratuityBonus.GetValueOrDefault();
		}
		set
		{
			__pbn__IsGratuityBonus = value;
		}
	}

	[ProtoMember(11, Name = "mtp_report_type")]
	[DefaultValue("")]
	public string MtpReportType
	{
		get
		{
			return __pbn__MtpReportType ?? "";
		}
		set
		{
			__pbn__MtpReportType = value;
		}
	}

	[ProtoMember(12, Name = "mtp_report_value")]
	[DefaultValue("")]
	public string MtpReportValue
	{
		get
		{
			return __pbn__MtpReportValue ?? "";
		}
		set
		{
			__pbn__MtpReportValue = value;
		}
	}

	[ProtoMember(13, Name = "is_re_begin")]
	public bool IsReBegin
	{
		get
		{
			return __pbn__IsReBegin.GetValueOrDefault();
		}
		set
		{
			__pbn__IsReBegin = value;
		}
	}

	[ProtoMember(14, Name = "avatar_restrict_list")]
	public uint[] AvatarRestrictLists { get; set; }

	[ProtoMember(15, Name = "is_theme_wanted_mp_assist_mode")]
	public bool IsThemeWantedMpAssistMode
	{
		get
		{
			return __pbn__IsThemeWantedMpAssistMode.GetValueOrDefault();
		}
		set
		{
			__pbn__IsThemeWantedMpAssistMode = value;
		}
	}

	[ProtoMember(16, Name = "sc_dlc_support_avatar_id")]
	public uint ScDlcSupportAvatarId
	{
		get
		{
			return __pbn__ScDlcSupportAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScDlcSupportAvatarId = value;
		}
	}

	[ProtoMember(17, Name = "stage_begin_floor")]
	public uint StageBeginFloor
	{
		get
		{
			return __pbn__StageBeginFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__StageBeginFloor = value;
		}
	}

	[ProtoMember(18, Name = "monster_card_unique_id_list")]
	public uint[] MonsterCardUniqueIdLists { get; set; }

	[ProtoMember(19, Name = "fake_avatar_id_list")]
	public uint[] FakeAvatarIdLists { get; set; }

	[ProtoMember(20, Name = "extra_item_id_list")]
	public uint[] ExtraItemIdLists { get; set; }

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

	public bool ShouldSerializeAvatarTeamType()
	{
		return __pbn__AvatarTeamType.HasValue;
	}

	public void ResetAvatarTeamType()
	{
		__pbn__AvatarTeamType = null;
	}

	public bool ShouldSerializeAssistantUid()
	{
		return __pbn__AssistantUid.HasValue;
	}

	public void ResetAssistantUid()
	{
		__pbn__AssistantUid = null;
	}

	public bool ShouldSerializeIsSpeedUpStage()
	{
		return __pbn__IsSpeedUpStage.HasValue;
	}

	public void ResetIsSpeedUpStage()
	{
		__pbn__IsSpeedUpStage = null;
	}

	public bool ShouldSerializeTargetWave()
	{
		return __pbn__TargetWave.HasValue;
	}

	public void ResetTargetWave()
	{
		__pbn__TargetWave = null;
	}

	public bool ShouldSerializeGratuityStageUniqueId()
	{
		return __pbn__GratuityStageUniqueId.HasValue;
	}

	public void ResetGratuityStageUniqueId()
	{
		__pbn__GratuityStageUniqueId = null;
	}

	public bool ShouldSerializeIsGratuityBonus()
	{
		return __pbn__IsGratuityBonus.HasValue;
	}

	public void ResetIsGratuityBonus()
	{
		__pbn__IsGratuityBonus = null;
	}

	public bool ShouldSerializeMtpReportType()
	{
		return __pbn__MtpReportType != null;
	}

	public void ResetMtpReportType()
	{
		__pbn__MtpReportType = null;
	}

	public bool ShouldSerializeMtpReportValue()
	{
		return __pbn__MtpReportValue != null;
	}

	public void ResetMtpReportValue()
	{
		__pbn__MtpReportValue = null;
	}

	public bool ShouldSerializeIsReBegin()
	{
		return __pbn__IsReBegin.HasValue;
	}

	public void ResetIsReBegin()
	{
		__pbn__IsReBegin = null;
	}

	public bool ShouldSerializeIsThemeWantedMpAssistMode()
	{
		return __pbn__IsThemeWantedMpAssistMode.HasValue;
	}

	public void ResetIsThemeWantedMpAssistMode()
	{
		__pbn__IsThemeWantedMpAssistMode = null;
	}

	public bool ShouldSerializeScDlcSupportAvatarId()
	{
		return __pbn__ScDlcSupportAvatarId.HasValue;
	}

	public void ResetScDlcSupportAvatarId()
	{
		__pbn__ScDlcSupportAvatarId = null;
	}

	public bool ShouldSerializeStageBeginFloor()
	{
		return __pbn__StageBeginFloor.HasValue;
	}

	public void ResetStageBeginFloor()
	{
		__pbn__StageBeginFloor = null;
	}
}
