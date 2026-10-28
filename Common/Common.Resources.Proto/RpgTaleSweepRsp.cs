using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleSweepRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2377
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LACK_MATERIAL")]
		LackMaterial,
		[ProtoEnum(Name = "STAGE_ERROR")]
		StageError,
		[ProtoEnum(Name = "NO_REWARD")]
		NoReward,
		[ProtoEnum(Name = "ENTER_TIMES_LACK")]
		EnterTimesLack,
		[ProtoEnum(Name = "CHALLENGE_NUM_LACK")]
		ChallengeNumLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__Times;

	private uint? __pbn__TaleId;

	private uint? __pbn__MaxFloor;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "stage_id")]
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

	[ProtoMember(3, Name = "times")]
	public uint Times
	{
		get
		{
			return __pbn__Times.GetValueOrDefault();
		}
		set
		{
			__pbn__Times = value;
		}
	}

	[ProtoMember(4, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(5, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

	[ProtoMember(6, Name = "max_floor")]
	public uint MaxFloor
	{
		get
		{
			return __pbn__MaxFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxFloor = value;
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

	public bool ShouldSerializeTimes()
	{
		return __pbn__Times.HasValue;
	}

	public void ResetTimes()
	{
		__pbn__Times = null;
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeMaxFloor()
	{
		return __pbn__MaxFloor.HasValue;
	}

	public void ResetMaxFloor()
	{
		__pbn__MaxFloor = null;
	}
}
