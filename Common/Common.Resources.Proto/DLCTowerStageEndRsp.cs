using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DLCTowerStageEndRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3566
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "NOT_BEGIN")]
		NotBegin,
		[ProtoEnum(Name = "PROGRESS_ERROR")]
		ProgressError,
		[ProtoEnum(Name = "PRODUCE_NUM_ERROR")]
		ProduceNumError,
		[ProtoEnum(Name = "AVATAR_ERROR")]
		AvatarError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Progress;

	private uint? __pbn__EndProgress;

	private uint? __pbn__Score;

	private bool? __pbn__IsNewRecord;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "progress")]
	public uint Progress
	{
		get
		{
			return __pbn__Progress.GetValueOrDefault();
		}
		set
		{
			__pbn__Progress = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(4, Name = "end_progress")]
	public uint EndProgress
	{
		get
		{
			return __pbn__EndProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__EndProgress = value;
		}
	}

	[ProtoMember(5, Name = "score")]
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

	[ProtoMember(6, Name = "is_new_record")]
	public bool IsNewRecord
	{
		get
		{
			return __pbn__IsNewRecord.GetValueOrDefault();
		}
		set
		{
			__pbn__IsNewRecord = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeEndProgress()
	{
		return __pbn__EndProgress.HasValue;
	}

	public void ResetEndProgress()
	{
		__pbn__EndProgress = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeIsNewRecord()
	{
		return __pbn__IsNewRecord.HasValue;
	}

	public void ResetIsNewRecord()
	{
		__pbn__IsNewRecord = null;
	}
}
