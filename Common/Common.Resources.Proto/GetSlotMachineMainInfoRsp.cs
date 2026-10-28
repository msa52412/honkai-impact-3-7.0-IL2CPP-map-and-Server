using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetSlotMachineMainInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4316
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__CurGachaId;

	private uint? __pbn__GachaTimes;

	private uint? __pbn__HasTakeRewardProgress;

	private bool? __pbn__IsNextSuperGacha;

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

	[ProtoMember(3, Name = "cur_gacha_id")]
	public uint CurGachaId
	{
		get
		{
			return __pbn__CurGachaId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurGachaId = value;
		}
	}

	[ProtoMember(4, Name = "gacha_times")]
	public uint GachaTimes
	{
		get
		{
			return __pbn__GachaTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaTimes = value;
		}
	}

	[ProtoMember(5, Name = "has_take_reward_progress")]
	public uint HasTakeRewardProgress
	{
		get
		{
			return __pbn__HasTakeRewardProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeRewardProgress = value;
		}
	}

	[ProtoMember(6, Name = "is_next_super_gacha")]
	public bool IsNextSuperGacha
	{
		get
		{
			return __pbn__IsNextSuperGacha.GetValueOrDefault();
		}
		set
		{
			__pbn__IsNextSuperGacha = value;
		}
	}

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

	public bool ShouldSerializeCurGachaId()
	{
		return __pbn__CurGachaId.HasValue;
	}

	public void ResetCurGachaId()
	{
		__pbn__CurGachaId = null;
	}

	public bool ShouldSerializeGachaTimes()
	{
		return __pbn__GachaTimes.HasValue;
	}

	public void ResetGachaTimes()
	{
		__pbn__GachaTimes = null;
	}

	public bool ShouldSerializeHasTakeRewardProgress()
	{
		return __pbn__HasTakeRewardProgress.HasValue;
	}

	public void ResetHasTakeRewardProgress()
	{
		__pbn__HasTakeRewardProgress = null;
	}

	public bool ShouldSerializeIsNextSuperGacha()
	{
		return __pbn__IsNextSuperGacha.HasValue;
	}

	public void ResetIsNextSuperGacha()
	{
		__pbn__IsNextSuperGacha = null;
	}
}
