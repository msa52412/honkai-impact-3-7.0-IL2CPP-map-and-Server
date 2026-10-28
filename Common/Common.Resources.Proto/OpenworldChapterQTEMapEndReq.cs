using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterQTEMapEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4683
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__QteMapId;

	private uint? __pbn__NormalDropNum;

	private bool? __pbn__IsGotLastReward;

	private QTEMapEndStatus? __pbn__EndStatus;

	private string __pbn__ReportExt;

	[ProtoMember(1, Name = "map_id")]
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

	[ProtoMember(2, Name = "qte_map_id")]
	public uint QteMapId
	{
		get
		{
			return __pbn__QteMapId.GetValueOrDefault();
		}
		set
		{
			__pbn__QteMapId = value;
		}
	}

	[ProtoMember(3, Name = "normal_drop_num")]
	public uint NormalDropNum
	{
		get
		{
			return __pbn__NormalDropNum.GetValueOrDefault();
		}
		set
		{
			__pbn__NormalDropNum = value;
		}
	}

	[ProtoMember(4, Name = "is_got_last_reward")]
	public bool IsGotLastReward
	{
		get
		{
			return __pbn__IsGotLastReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsGotLastReward = value;
		}
	}

	[ProtoMember(5, Name = "end_status")]
	[DefaultValue(QTEMapEndStatus.QteMapEndStatusSucc)]
	public QTEMapEndStatus EndStatus
	{
		get
		{
			return __pbn__EndStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__EndStatus = value;
		}
	}

	[ProtoMember(6, Name = "report_ext")]
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

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeQteMapId()
	{
		return __pbn__QteMapId.HasValue;
	}

	public void ResetQteMapId()
	{
		__pbn__QteMapId = null;
	}

	public bool ShouldSerializeNormalDropNum()
	{
		return __pbn__NormalDropNum.HasValue;
	}

	public void ResetNormalDropNum()
	{
		__pbn__NormalDropNum = null;
	}

	public bool ShouldSerializeIsGotLastReward()
	{
		return __pbn__IsGotLastReward.HasValue;
	}

	public void ResetIsGotLastReward()
	{
		__pbn__IsGotLastReward = null;
	}

	public bool ShouldSerializeEndStatus()
	{
		return __pbn__EndStatus.HasValue;
	}

	public void ResetEndStatus()
	{
		__pbn__EndStatus = null;
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
