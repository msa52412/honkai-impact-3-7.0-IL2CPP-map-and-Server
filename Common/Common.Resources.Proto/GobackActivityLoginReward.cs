using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GobackActivityLoginReward : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TakeDays;

	[ProtoMember(1, Name = "take_days")]
	public uint TakeDays
	{
		get
		{
			return __pbn__TakeDays.GetValueOrDefault();
		}
		set
		{
			__pbn__TakeDays = value;
		}
	}

	[ProtoMember(2, Name = "take_reward_list")]
	public uint[] TakeRewardLists { get; set; }

	[ProtoMember(3, Name = "take_bonus_reward_list")]
	public uint[] TakeBonusRewardLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTakeDays()
	{
		return __pbn__TakeDays.HasValue;
	}

	public void ResetTakeDays()
	{
		__pbn__TakeDays = null;
	}
}
