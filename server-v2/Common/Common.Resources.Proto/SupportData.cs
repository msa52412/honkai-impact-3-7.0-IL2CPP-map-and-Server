using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SupportData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SupportIndex;

	private bool? __pbn__IsTakeShareReward;

	private uint? __pbn__SupportEndTime;

	private uint? __pbn__SupportBeginTime;

	private uint? __pbn__CountDownTime;

	[ProtoMember(1, Name = "support_index")]
	public uint SupportIndex
	{
		get
		{
			return __pbn__SupportIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__SupportIndex = value;
		}
	}

	[ProtoMember(2, Name = "is_take_share_reward")]
	public bool IsTakeShareReward
	{
		get
		{
			return __pbn__IsTakeShareReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeShareReward = value;
		}
	}

	[ProtoMember(3, Name = "support_end_time")]
	public uint SupportEndTime
	{
		get
		{
			return __pbn__SupportEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__SupportEndTime = value;
		}
	}

	[ProtoMember(4, Name = "support_begin_time")]
	public uint SupportBeginTime
	{
		get
		{
			return __pbn__SupportBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__SupportBeginTime = value;
		}
	}

	[ProtoMember(5, Name = "count_down_time")]
	public uint CountDownTime
	{
		get
		{
			return __pbn__CountDownTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CountDownTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSupportIndex()
	{
		return __pbn__SupportIndex.HasValue;
	}

	public void ResetSupportIndex()
	{
		__pbn__SupportIndex = null;
	}

	public bool ShouldSerializeIsTakeShareReward()
	{
		return __pbn__IsTakeShareReward.HasValue;
	}

	public void ResetIsTakeShareReward()
	{
		__pbn__IsTakeShareReward = null;
	}

	public bool ShouldSerializeSupportEndTime()
	{
		return __pbn__SupportEndTime.HasValue;
	}

	public void ResetSupportEndTime()
	{
		__pbn__SupportEndTime = null;
	}

	public bool ShouldSerializeSupportBeginTime()
	{
		return __pbn__SupportBeginTime.HasValue;
	}

	public void ResetSupportBeginTime()
	{
		__pbn__SupportBeginTime = null;
	}

	public bool ShouldSerializeCountDownTime()
	{
		return __pbn__CountDownTime.HasValue;
	}

	public void ResetCountDownTime()
	{
		__pbn__CountDownTime = null;
	}
}
