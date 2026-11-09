using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SingleRaidActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CurStepStageId;

	private uint? __pbn__LastDoneChallengeStageId;

	private uint? __pbn__StepStageRemainEnterTimes;

	[ProtoMember(1, Name = "cur_step_stage_id")]
	public uint CurStepStageId
	{
		get
		{
			return __pbn__CurStepStageId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurStepStageId = value;
		}
	}

	[ProtoMember(2, Name = "last_done_challenge_stage_id")]
	public uint LastDoneChallengeStageId
	{
		get
		{
			return __pbn__LastDoneChallengeStageId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastDoneChallengeStageId = value;
		}
	}

	[ProtoMember(3, Name = "step_stage_remain_enter_times")]
	public uint StepStageRemainEnterTimes
	{
		get
		{
			return __pbn__StepStageRemainEnterTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__StepStageRemainEnterTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurStepStageId()
	{
		return __pbn__CurStepStageId.HasValue;
	}

	public void ResetCurStepStageId()
	{
		__pbn__CurStepStageId = null;
	}

	public bool ShouldSerializeLastDoneChallengeStageId()
	{
		return __pbn__LastDoneChallengeStageId.HasValue;
	}

	public void ResetLastDoneChallengeStageId()
	{
		__pbn__LastDoneChallengeStageId = null;
	}

	public bool ShouldSerializeStepStageRemainEnterTimes()
	{
		return __pbn__StepStageRemainEnterTimes.HasValue;
	}

	public void ResetStepStageRemainEnterTimes()
	{
		__pbn__StepStageRemainEnterTimes = null;
	}
}
