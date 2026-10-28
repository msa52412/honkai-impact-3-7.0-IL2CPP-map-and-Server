using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SnsShowTowerRaidData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__HardLevel;

	private uint? __pbn__MaxHardLevel;

	private uint? __pbn__TotalEnterTimes;

	private uint? __pbn__TotalBattleDuringTime;

	[ProtoMember(1, Name = "hard_level")]
	public uint HardLevel
	{
		get
		{
			return __pbn__HardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HardLevel = value;
		}
	}

	[ProtoMember(2, Name = "max_hard_level")]
	public uint MaxHardLevel
	{
		get
		{
			return __pbn__MaxHardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxHardLevel = value;
		}
	}

	[ProtoMember(3, Name = "total_enter_times")]
	public uint TotalEnterTimes
	{
		get
		{
			return __pbn__TotalEnterTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalEnterTimes = value;
		}
	}

	[ProtoMember(4, Name = "total_battle_during_time")]
	public uint TotalBattleDuringTime
	{
		get
		{
			return __pbn__TotalBattleDuringTime.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalBattleDuringTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHardLevel()
	{
		return __pbn__HardLevel.HasValue;
	}

	public void ResetHardLevel()
	{
		__pbn__HardLevel = null;
	}

	public bool ShouldSerializeMaxHardLevel()
	{
		return __pbn__MaxHardLevel.HasValue;
	}

	public void ResetMaxHardLevel()
	{
		__pbn__MaxHardLevel = null;
	}

	public bool ShouldSerializeTotalEnterTimes()
	{
		return __pbn__TotalEnterTimes.HasValue;
	}

	public void ResetTotalEnterTimes()
	{
		__pbn__TotalEnterTimes = null;
	}

	public bool ShouldSerializeTotalBattleDuringTime()
	{
		return __pbn__TotalBattleDuringTime.HasValue;
	}

	public void ResetTotalBattleDuringTime()
	{
		__pbn__TotalBattleDuringTime = null;
	}
}
