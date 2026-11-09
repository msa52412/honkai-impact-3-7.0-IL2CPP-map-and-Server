using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PushTowerActivityInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EnterTimes;

	private bool? __pbn__IsClearRewardTaken;

	private uint? __pbn__GrowBuffLevel;

	[ProtoMember(1, Name = "enter_times")]
	public uint EnterTimes
	{
		get
		{
			return __pbn__EnterTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__EnterTimes = value;
		}
	}

	[ProtoMember(2, Name = "stage_reward_taken_list")]
	public uint[] StageRewardTakenLists { get; set; }

	[ProtoMember(3, Name = "is_clear_reward_taken")]
	public bool IsClearRewardTaken
	{
		get
		{
			return __pbn__IsClearRewardTaken.GetValueOrDefault();
		}
		set
		{
			__pbn__IsClearRewardTaken = value;
		}
	}

	[ProtoMember(4, Name = "grow_buff_level")]
	public uint GrowBuffLevel
	{
		get
		{
			return __pbn__GrowBuffLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__GrowBuffLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEnterTimes()
	{
		return __pbn__EnterTimes.HasValue;
	}

	public void ResetEnterTimes()
	{
		__pbn__EnterTimes = null;
	}

	public bool ShouldSerializeIsClearRewardTaken()
	{
		return __pbn__IsClearRewardTaken.HasValue;
	}

	public void ResetIsClearRewardTaken()
	{
		__pbn__IsClearRewardTaken = null;
	}

	public bool ShouldSerializeGrowBuffLevel()
	{
		return __pbn__GrowBuffLevel.HasValue;
	}

	public void ResetGrowBuffLevel()
	{
		__pbn__GrowBuffLevel = null;
	}
}
