using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LotteryPlayerInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__Level;

	private uint? __pbn__VipPoint;

	private uint? __pbn__VipLevel;

	private uint? __pbn__AccountType;

	private uint? __pbn__Tag;

	private uint? __pbn__FirstRewardNum;

	private bool? __pbn__IsLastScheduleGetRareReward;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "level")]
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

	[ProtoMember(3, Name = "vip_point")]
	public uint VipPoint
	{
		get
		{
			return __pbn__VipPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__VipPoint = value;
		}
	}

	[ProtoMember(4, Name = "vip_level")]
	public uint VipLevel
	{
		get
		{
			return __pbn__VipLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__VipLevel = value;
		}
	}

	[ProtoMember(5, Name = "account_type")]
	public uint AccountType
	{
		get
		{
			return __pbn__AccountType.GetValueOrDefault();
		}
		set
		{
			__pbn__AccountType = value;
		}
	}

	[ProtoMember(6, Name = "tag")]
	public uint Tag
	{
		get
		{
			return __pbn__Tag.GetValueOrDefault();
		}
		set
		{
			__pbn__Tag = value;
		}
	}

	[ProtoMember(7, Name = "first_reward_num")]
	public uint FirstRewardNum
	{
		get
		{
			return __pbn__FirstRewardNum.GetValueOrDefault();
		}
		set
		{
			__pbn__FirstRewardNum = value;
		}
	}

	[ProtoMember(8, Name = "is_last_schedule_get_rare_reward")]
	public bool IsLastScheduleGetRareReward
	{
		get
		{
			return __pbn__IsLastScheduleGetRareReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLastScheduleGetRareReward = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeVipPoint()
	{
		return __pbn__VipPoint.HasValue;
	}

	public void ResetVipPoint()
	{
		__pbn__VipPoint = null;
	}

	public bool ShouldSerializeVipLevel()
	{
		return __pbn__VipLevel.HasValue;
	}

	public void ResetVipLevel()
	{
		__pbn__VipLevel = null;
	}

	public bool ShouldSerializeAccountType()
	{
		return __pbn__AccountType.HasValue;
	}

	public void ResetAccountType()
	{
		__pbn__AccountType = null;
	}

	public bool ShouldSerializeTag()
	{
		return __pbn__Tag.HasValue;
	}

	public void ResetTag()
	{
		__pbn__Tag = null;
	}

	public bool ShouldSerializeFirstRewardNum()
	{
		return __pbn__FirstRewardNum.HasValue;
	}

	public void ResetFirstRewardNum()
	{
		__pbn__FirstRewardNum = null;
	}

	public bool ShouldSerializeIsLastScheduleGetRareReward()
	{
		return __pbn__IsLastScheduleGetRareReward.HasValue;
	}

	public void ResetIsLastScheduleGetRareReward()
	{
		__pbn__IsLastScheduleGetRareReward = null;
	}
}
