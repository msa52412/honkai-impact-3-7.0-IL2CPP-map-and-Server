using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MosaicActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__Progress;

	private bool? __pbn__IsTakeReward;

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

	[ProtoMember(3, Name = "is_take_reward")]
	public bool IsTakeReward
	{
		get
		{
			return __pbn__IsTakeReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeReward = value;
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

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeIsTakeReward()
	{
		return __pbn__IsTakeReward.HasValue;
	}

	public void ResetIsTakeReward()
	{
		__pbn__IsTakeReward = null;
	}
}
