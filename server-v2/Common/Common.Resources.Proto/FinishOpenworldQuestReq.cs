using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishOpenworldQuestReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4488
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__QuestId;

	private uint? __pbn__CostTime;

	private uint? __pbn__MechaId;

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

	[ProtoMember(2, Name = "quest_id")]
	public uint QuestId
	{
		get
		{
			return __pbn__QuestId.GetValueOrDefault();
		}
		set
		{
			__pbn__QuestId = value;
		}
	}

	[ProtoMember(3, Name = "cost_time")]
	public uint CostTime
	{
		get
		{
			return __pbn__CostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CostTime = value;
		}
	}

	[ProtoMember(4, Name = "finish_challenge_list")]
	public uint[] FinishChallengeLists { get; set; }

	[ProtoMember(5, Name = "mecha_id")]
	public uint MechaId
	{
		get
		{
			return __pbn__MechaId.GetValueOrDefault();
		}
		set
		{
			__pbn__MechaId = value;
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

	public bool ShouldSerializeQuestId()
	{
		return __pbn__QuestId.HasValue;
	}

	public void ResetQuestId()
	{
		__pbn__QuestId = null;
	}

	public bool ShouldSerializeCostTime()
	{
		return __pbn__CostTime.HasValue;
	}

	public void ResetCostTime()
	{
		__pbn__CostTime = null;
	}

	public bool ShouldSerializeMechaId()
	{
		return __pbn__MechaId.HasValue;
	}

	public void ResetMechaId()
	{
		__pbn__MechaId = null;
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
