using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldFishRewardNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2441
	}

	private IExtension __pbn__extensionData;

	private FishRewardSrc? __pbn__RewardSrc;

	private uint? __pbn__FishId;

	private uint? __pbn__AddCurrencyNum;

	private uint? __pbn__CurDailyGetCurrencyNum;

	[ProtoMember(1, Name = "reward_src")]
	[DefaultValue(FishRewardSrc.FishRewardSrcNone)]
	public FishRewardSrc RewardSrc
	{
		get
		{
			return __pbn__RewardSrc.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardSrc = value;
		}
	}

	[ProtoMember(2, Name = "fish_id")]
	public uint FishId
	{
		get
		{
			return __pbn__FishId.GetValueOrDefault();
		}
		set
		{
			__pbn__FishId = value;
		}
	}

	[ProtoMember(3, Name = "add_currency_num")]
	public uint AddCurrencyNum
	{
		get
		{
			return __pbn__AddCurrencyNum.GetValueOrDefault();
		}
		set
		{
			__pbn__AddCurrencyNum = value;
		}
	}

	[ProtoMember(4, Name = "cur_daily_get_currency_num")]
	public uint CurDailyGetCurrencyNum
	{
		get
		{
			return __pbn__CurDailyGetCurrencyNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CurDailyGetCurrencyNum = value;
		}
	}

	[ProtoMember(5, Name = "extra_reward_data")]
	public RewardData ExtraRewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRewardSrc()
	{
		return __pbn__RewardSrc.HasValue;
	}

	public void ResetRewardSrc()
	{
		__pbn__RewardSrc = null;
	}

	public bool ShouldSerializeFishId()
	{
		return __pbn__FishId.HasValue;
	}

	public void ResetFishId()
	{
		__pbn__FishId = null;
	}

	public bool ShouldSerializeAddCurrencyNum()
	{
		return __pbn__AddCurrencyNum.HasValue;
	}

	public void ResetAddCurrencyNum()
	{
		__pbn__AddCurrencyNum = null;
	}

	public bool ShouldSerializeCurDailyGetCurrencyNum()
	{
		return __pbn__CurDailyGetCurrencyNum.HasValue;
	}

	public void ResetCurDailyGetCurrencyNum()
	{
		__pbn__CurDailyGetCurrencyNum = null;
	}
}
