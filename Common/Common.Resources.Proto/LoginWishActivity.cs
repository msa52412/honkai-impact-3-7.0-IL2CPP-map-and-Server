using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LoginWishActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__ShowBeginTime;

	private uint? __pbn__ShowEndTime;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__LoginDays;

	[ProtoMember(1, Name = "activity_id")]
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

	[ProtoMember(2, Name = "show_begin_time")]
	public uint ShowBeginTime
	{
		get
		{
			return __pbn__ShowBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowBeginTime = value;
		}
	}

	[ProtoMember(3, Name = "show_end_time")]
	public uint ShowEndTime
	{
		get
		{
			return __pbn__ShowEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowEndTime = value;
		}
	}

	[ProtoMember(4, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(5, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(6, Name = "login_days")]
	public uint LoginDays
	{
		get
		{
			return __pbn__LoginDays.GetValueOrDefault();
		}
		set
		{
			__pbn__LoginDays = value;
		}
	}

	[ProtoMember(7, Name = "has_take_reward_login_day_list")]
	public uint[] HasTakeRewardLoginDayLists { get; set; }

	[ProtoMember(8, Name = "has_take_reward_special_day_list")]
	public uint[] HasTakeRewardSpecialDayLists { get; set; }

	[ProtoMember(9, Name = "has_made_wish_list")]
	public List<LoginWish> HasMadeWishLists { get; } = new List<LoginWish>();


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

	public bool ShouldSerializeShowBeginTime()
	{
		return __pbn__ShowBeginTime.HasValue;
	}

	public void ResetShowBeginTime()
	{
		__pbn__ShowBeginTime = null;
	}

	public bool ShouldSerializeShowEndTime()
	{
		return __pbn__ShowEndTime.HasValue;
	}

	public void ResetShowEndTime()
	{
		__pbn__ShowEndTime = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeLoginDays()
	{
		return __pbn__LoginDays.HasValue;
	}

	public void ResetLoginDays()
	{
		__pbn__LoginDays = null;
	}
}
