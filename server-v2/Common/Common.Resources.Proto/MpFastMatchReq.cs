using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpFastMatchReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1247
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__DeviceScore;

	private uint? __pbn__Level;

	private uint? __pbn__ClientRtt;

	private uint? __pbn__ReportNum;

	[ProtoMember(1, Name = "stage_id_list")]
	public uint[] StageIdLists { get; set; }

	[ProtoMember(2, Name = "device_score")]
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

	[ProtoMember(3, Name = "match_stage_list")]
	public List<MpMatchStage> MatchStageLists { get; } = new List<MpMatchStage>();


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

	[ProtoMember(5, Name = "client_rtt")]
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

	[ProtoMember(6, Name = "member")]
	public MpTeamMember Member { get; set; }

	[ProtoMember(7, Name = "report_num")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDeviceScore()
	{
		return __pbn__DeviceScore.HasValue;
	}

	public void ResetDeviceScore()
	{
		__pbn__DeviceScore = null;
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

	public bool ShouldSerializeReportNum()
	{
		return __pbn__ReportNum.HasValue;
	}

	public void ResetReportNum()
	{
		__pbn__ReportNum = null;
	}
}
