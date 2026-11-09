using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldStageEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4464
	}

	private IExtension __pbn__extensionData;

	private StageEndStatus? __pbn__EndStatus;

	private uint? __pbn__MapId;

	private uint? __pbn__MechaLostHpPercent;

	private uint? __pbn__MechaLostSpPercent;

	private uint? __pbn__MapEnergy;

	private uint? __pbn__ScDlcFeverScore;

	private uint? __pbn__ScDlcClimaxScore;

	private uint? __pbn__AreaId;

	private string __pbn__ReportExt;

	[ProtoMember(1, Name = "end_status")]
	[DefaultValue(StageEndStatus.StageWin)]
	public StageEndStatus EndStatus
	{
		get
		{
			return __pbn__EndStatus ?? StageEndStatus.StageWin;
		}
		set
		{
			__pbn__EndStatus = value;
		}
	}

	[ProtoMember(3, Name = "cheat_data_list")]
	public List<StageCheatData> CheatDataLists { get; } = new List<StageCheatData>();


	[ProtoMember(4, Name = "avatar_list")]
	public List<OpenworldStageAvatarInfo> AvatarLists { get; } = new List<OpenworldStageAvatarInfo>();


	[ProtoMember(5, Name = "sp_recover_avatar_list")]
	public uint[] SpRecoverAvatarLists { get; set; }

	[ProtoMember(6, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(7, Name = "mecha_lost_hp_percent")]
	public uint MechaLostHpPercent
	{
		get
		{
			return __pbn__MechaLostHpPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__MechaLostHpPercent = value;
		}
	}

	[ProtoMember(8, Name = "mecha_lost_sp_percent")]
	public uint MechaLostSpPercent
	{
		get
		{
			return __pbn__MechaLostSpPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__MechaLostSpPercent = value;
		}
	}

	[ProtoMember(10, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(11, Name = "map_energy")]
	public uint MapEnergy
	{
		get
		{
			return __pbn__MapEnergy.GetValueOrDefault();
		}
		set
		{
			__pbn__MapEnergy = value;
		}
	}

	[ProtoMember(12, Name = "sc_dlc_fever_score")]
	public uint ScDlcFeverScore
	{
		get
		{
			return __pbn__ScDlcFeverScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ScDlcFeverScore = value;
		}
	}

	[ProtoMember(13, Name = "sc_dlc_climax_score")]
	public uint ScDlcClimaxScore
	{
		get
		{
			return __pbn__ScDlcClimaxScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ScDlcClimaxScore = value;
		}
	}

	[ProtoMember(14, Name = "area_id")]
	public uint AreaId
	{
		get
		{
			return __pbn__AreaId.GetValueOrDefault();
		}
		set
		{
			__pbn__AreaId = value;
		}
	}

	[ProtoMember(15, Name = "report_ext")]
	[DefaultValue("")]
	public string ReportExt
	{
		get
		{
			return __pbn__ReportExt ?? "";
		}
		set
		{
			__pbn__ReportExt = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEndStatus()
	{
		return __pbn__EndStatus.HasValue;
	}

	public void ResetEndStatus()
	{
		__pbn__EndStatus = null;
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeMechaLostHpPercent()
	{
		return __pbn__MechaLostHpPercent.HasValue;
	}

	public void ResetMechaLostHpPercent()
	{
		__pbn__MechaLostHpPercent = null;
	}

	public bool ShouldSerializeMechaLostSpPercent()
	{
		return __pbn__MechaLostSpPercent.HasValue;
	}

	public void ResetMechaLostSpPercent()
	{
		__pbn__MechaLostSpPercent = null;
	}

	public bool ShouldSerializeMapEnergy()
	{
		return __pbn__MapEnergy.HasValue;
	}

	public void ResetMapEnergy()
	{
		__pbn__MapEnergy = null;
	}

	public bool ShouldSerializeScDlcFeverScore()
	{
		return __pbn__ScDlcFeverScore.HasValue;
	}

	public void ResetScDlcFeverScore()
	{
		__pbn__ScDlcFeverScore = null;
	}

	public bool ShouldSerializeScDlcClimaxScore()
	{
		return __pbn__ScDlcClimaxScore.HasValue;
	}

	public void ResetScDlcClimaxScore()
	{
		__pbn__ScDlcClimaxScore = null;
	}

	public bool ShouldSerializeAreaId()
	{
		return __pbn__AreaId.HasValue;
	}

	public void ResetAreaId()
	{
		__pbn__AreaId = null;
	}

	public bool ShouldSerializeReportExt()
	{
		return __pbn__ReportExt != null;
	}

	public void ResetReportExt()
	{
		__pbn__ReportExt = null;
	}
}
