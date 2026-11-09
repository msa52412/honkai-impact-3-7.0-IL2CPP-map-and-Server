using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GreedyEndlessReportFloorEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3455
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Floor;

	private uint? __pbn__Score;

	private uint? __pbn__ExtraScore;

	private uint? __pbn__StageCostTime;

	private StageEndStatus? __pbn__EndStatus;

	[ProtoMember(1, Name = "floor")]
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

	[ProtoMember(3, Name = "finish_challenge_id_list")]
	public uint[] FinishChallengeIdLists { get; set; }

	[ProtoMember(4, Name = "score")]
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

	[ProtoMember(5, Name = "extra_score")]
	public uint ExtraScore
	{
		get
		{
			return __pbn__ExtraScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ExtraScore = value;
		}
	}

	[ProtoMember(6, Name = "lineup_info")]
	public LineupBriefInfo LineupInfo { get; set; }

	[ProtoMember(7, Name = "stage_cost_time")]
	public uint StageCostTime
	{
		get
		{
			return __pbn__StageCostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__StageCostTime = value;
		}
	}

	[ProtoMember(8, Name = "end_status")]
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

	[ProtoMember(9, Name = "cheat_data_list")]
	public List<StageCheatData> CheatDataLists { get; } = new List<StageCheatData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
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

	public bool ShouldSerializeExtraScore()
	{
		return __pbn__ExtraScore.HasValue;
	}

	public void ResetExtraScore()
	{
		__pbn__ExtraScore = null;
	}

	public bool ShouldSerializeStageCostTime()
	{
		return __pbn__StageCostTime.HasValue;
	}

	public void ResetStageCostTime()
	{
		__pbn__StageCostTime = null;
	}

	public bool ShouldSerializeEndStatus()
	{
		return __pbn__EndStatus.HasValue;
	}

	public void ResetEndStatus()
	{
		__pbn__EndStatus = null;
	}
}
