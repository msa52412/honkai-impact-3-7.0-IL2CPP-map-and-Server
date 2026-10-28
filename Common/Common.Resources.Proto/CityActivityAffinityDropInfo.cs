using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CityActivityAffinityDropInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MaxDailyAffinityDropInStage;

	private uint? __pbn__TodayAffinityDropInStage;

	[ProtoMember(1, Name = "max_daily_affinity_drop_in_stage")]
	public uint MaxDailyAffinityDropInStage
	{
		get
		{
			return __pbn__MaxDailyAffinityDropInStage.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxDailyAffinityDropInStage = value;
		}
	}

	[ProtoMember(2, Name = "today_affinity_drop_in_stage")]
	public uint TodayAffinityDropInStage
	{
		get
		{
			return __pbn__TodayAffinityDropInStage.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayAffinityDropInStage = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMaxDailyAffinityDropInStage()
	{
		return __pbn__MaxDailyAffinityDropInStage.HasValue;
	}

	public void ResetMaxDailyAffinityDropInStage()
	{
		__pbn__MaxDailyAffinityDropInStage = null;
	}

	public bool ShouldSerializeTodayAffinityDropInStage()
	{
		return __pbn__TodayAffinityDropInStage.HasValue;
	}

	public void ResetTodayAffinityDropInStage()
	{
		__pbn__TodayAffinityDropInStage = null;
	}
}
