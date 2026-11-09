using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetArmadaStageScoreActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2822
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__HasTakeSelfScore;

	private uint? __pbn__HasTakeArmadaScore;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "has_take_self_score")]
	public uint HasTakeSelfScore
	{
		get
		{
			return __pbn__HasTakeSelfScore.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeSelfScore = value;
		}
	}

	[ProtoMember(4, Name = "has_take_armada_score")]
	public uint HasTakeArmadaScore
	{
		get
		{
			return __pbn__HasTakeArmadaScore.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeArmadaScore = value;
		}
	}

	[ProtoMember(5, Name = "stage_score_list")]
	public List<ArmadaStageScore> StageScoreLists { get; } = new List<ArmadaStageScore>();


	[ProtoMember(6, Name = "armada_score_list")]
	public List<ArmadaStageScorePlayerScore> ArmadaScoreLists { get; } = new List<ArmadaStageScorePlayerScore>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeHasTakeSelfScore()
	{
		return __pbn__HasTakeSelfScore.HasValue;
	}

	public void ResetHasTakeSelfScore()
	{
		__pbn__HasTakeSelfScore = null;
	}

	public bool ShouldSerializeHasTakeArmadaScore()
	{
		return __pbn__HasTakeArmadaScore.HasValue;
	}

	public void ResetHasTakeArmadaScore()
	{
		__pbn__HasTakeArmadaScore = null;
	}
}
