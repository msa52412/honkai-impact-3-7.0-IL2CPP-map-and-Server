using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LoginActivityData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__LoginDays;

	private uint? __pbn__AcceptTime;

	private uint? __pbn__DurationEndTime;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "login_days")]
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

	[ProtoMember(3, Name = "has_take_days_reward_list")]
	public uint[] HasTakeDaysRewardLists { get; set; }

	[ProtoMember(4, Name = "has_take_days_bonus_reward_list")]
	public uint[] HasTakeDaysBonusRewardLists { get; set; }

	[ProtoMember(5, Name = "accept_time")]
	public uint AcceptTime
	{
		get
		{
			return __pbn__AcceptTime.GetValueOrDefault();
		}
		set
		{
			__pbn__AcceptTime = value;
		}
	}

	[ProtoMember(6, Name = "duration_end_time")]
	public uint DurationEndTime
	{
		get
		{
			return __pbn__DurationEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DurationEndTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeLoginDays()
	{
		return __pbn__LoginDays.HasValue;
	}

	public void ResetLoginDays()
	{
		__pbn__LoginDays = null;
	}

	public bool ShouldSerializeAcceptTime()
	{
		return __pbn__AcceptTime.HasValue;
	}

	public void ResetAcceptTime()
	{
		__pbn__AcceptTime = null;
	}

	public bool ShouldSerializeDurationEndTime()
	{
		return __pbn__DurationEndTime.HasValue;
	}

	public void ResetDurationEndTime()
	{
		__pbn__DurationEndTime = null;
	}
}
