using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LotteryData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__LotteryId;

	private uint? __pbn__DrawNumberBeginTime;

	private uint? __pbn__DrawNumberEndTime;

	private uint? __pbn__TakeRewardBeginTime;

	private uint? __pbn__FirstRewardId;

	private string __pbn__FirstRewardNumberStr;

	private uint? __pbn__SecondRewardId;

	private string __pbn__SecondRewardNumberStr;

	private uint? __pbn__ThirdRewardId;

	private uint? __pbn__MaxDrawTimes;

	private uint? __pbn__SpecialNumber;

	[ProtoMember(1, Name = "lottery_id")]
	public uint LotteryId
	{
		get
		{
			return __pbn__LotteryId.GetValueOrDefault();
		}
		set
		{
			__pbn__LotteryId = value;
		}
	}

	[ProtoMember(2, Name = "lottery_number")]
	public uint[] LotteryNumbers { get; set; }

	[ProtoMember(3, Name = "reward_taken_lottery_number")]
	public uint[] RewardTakenLotteryNumbers { get; set; }

	[ProtoMember(4, Name = "draw_number_begin_time")]
	public uint DrawNumberBeginTime
	{
		get
		{
			return __pbn__DrawNumberBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DrawNumberBeginTime = value;
		}
	}

	[ProtoMember(5, Name = "draw_number_end_time")]
	public uint DrawNumberEndTime
	{
		get
		{
			return __pbn__DrawNumberEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DrawNumberEndTime = value;
		}
	}

	[ProtoMember(6, Name = "take_reward_begin_time")]
	public uint TakeRewardBeginTime
	{
		get
		{
			return __pbn__TakeRewardBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__TakeRewardBeginTime = value;
		}
	}

	[ProtoMember(7, Name = "first_reward_id")]
	public uint FirstRewardId
	{
		get
		{
			return __pbn__FirstRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__FirstRewardId = value;
		}
	}

	[ProtoMember(8, Name = "first_reward_number_str")]
	[DefaultValue("")]
	public string FirstRewardNumberStr
	{
		get
		{
			return __pbn__FirstRewardNumberStr ?? "";
		}
		set
		{
			__pbn__FirstRewardNumberStr = value;
		}
	}

	[ProtoMember(9, Name = "second_reward_id")]
	public uint SecondRewardId
	{
		get
		{
			return __pbn__SecondRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__SecondRewardId = value;
		}
	}

	[ProtoMember(10, Name = "second_reward_number_str")]
	[DefaultValue("")]
	public string SecondRewardNumberStr
	{
		get
		{
			return __pbn__SecondRewardNumberStr ?? "";
		}
		set
		{
			__pbn__SecondRewardNumberStr = value;
		}
	}

	[ProtoMember(11, Name = "third_reward_id")]
	public uint ThirdRewardId
	{
		get
		{
			return __pbn__ThirdRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__ThirdRewardId = value;
		}
	}

	[ProtoMember(12, Name = "max_draw_times")]
	public uint MaxDrawTimes
	{
		get
		{
			return __pbn__MaxDrawTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxDrawTimes = value;
		}
	}

	[ProtoMember(13, Name = "special_number")]
	public uint SpecialNumber
	{
		get
		{
			return __pbn__SpecialNumber.GetValueOrDefault();
		}
		set
		{
			__pbn__SpecialNumber = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLotteryId()
	{
		return __pbn__LotteryId.HasValue;
	}

	public void ResetLotteryId()
	{
		__pbn__LotteryId = null;
	}

	public bool ShouldSerializeDrawNumberBeginTime()
	{
		return __pbn__DrawNumberBeginTime.HasValue;
	}

	public void ResetDrawNumberBeginTime()
	{
		__pbn__DrawNumberBeginTime = null;
	}

	public bool ShouldSerializeDrawNumberEndTime()
	{
		return __pbn__DrawNumberEndTime.HasValue;
	}

	public void ResetDrawNumberEndTime()
	{
		__pbn__DrawNumberEndTime = null;
	}

	public bool ShouldSerializeTakeRewardBeginTime()
	{
		return __pbn__TakeRewardBeginTime.HasValue;
	}

	public void ResetTakeRewardBeginTime()
	{
		__pbn__TakeRewardBeginTime = null;
	}

	public bool ShouldSerializeFirstRewardId()
	{
		return __pbn__FirstRewardId.HasValue;
	}

	public void ResetFirstRewardId()
	{
		__pbn__FirstRewardId = null;
	}

	public bool ShouldSerializeFirstRewardNumberStr()
	{
		return __pbn__FirstRewardNumberStr != null;
	}

	public void ResetFirstRewardNumberStr()
	{
		__pbn__FirstRewardNumberStr = null;
	}

	public bool ShouldSerializeSecondRewardId()
	{
		return __pbn__SecondRewardId.HasValue;
	}

	public void ResetSecondRewardId()
	{
		__pbn__SecondRewardId = null;
	}

	public bool ShouldSerializeSecondRewardNumberStr()
	{
		return __pbn__SecondRewardNumberStr != null;
	}

	public void ResetSecondRewardNumberStr()
	{
		__pbn__SecondRewardNumberStr = null;
	}

	public bool ShouldSerializeThirdRewardId()
	{
		return __pbn__ThirdRewardId.HasValue;
	}

	public void ResetThirdRewardId()
	{
		__pbn__ThirdRewardId = null;
	}

	public bool ShouldSerializeMaxDrawTimes()
	{
		return __pbn__MaxDrawTimes.HasValue;
	}

	public void ResetMaxDrawTimes()
	{
		__pbn__MaxDrawTimes = null;
	}

	public bool ShouldSerializeSpecialNumber()
	{
		return __pbn__SpecialNumber.HasValue;
	}

	public void ResetSpecialNumber()
	{
		__pbn__SpecialNumber = null;
	}
}
