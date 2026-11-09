using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScDLCTowerStageEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6336
	}

	private IExtension __pbn__extensionData;

	private StageEndStatus? __pbn__EndStatus;

	private uint? __pbn__Floor;

	private uint? __pbn__Score;

	private uint? __pbn__ScDlcFeverScore;

	private uint? __pbn__ScDlcClimaxScore;

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

	[ProtoMember(2, Name = "floor")]
	public uint Floor
	{
		get
		{
			return __pbn__Floor.GetValueOrDefault();
		}
		set
		{
			__pbn__Floor = value;
		}
	}

	[ProtoMember(3, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(4, Name = "avatar_info_list")]
	public List<ScDLCTowerAvatarInfo> AvatarInfoLists { get; } = new List<ScDLCTowerAvatarInfo>();


	[ProtoMember(5, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(6, Name = "sc_dlc_fever_score")]
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

	[ProtoMember(7, Name = "sc_dlc_climax_score")]
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

	[ProtoMember(8, Name = "report_ext")]
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

	public bool ShouldSerializeFloor()
	{
		return __pbn__Floor.HasValue;
	}

	public void ResetFloor()
	{
		__pbn__Floor = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
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

	public bool ShouldSerializeReportExt()
	{
		return __pbn__ReportExt != null;
	}

	public void ResetReportExt()
	{
		__pbn__ReportExt = null;
	}
}
