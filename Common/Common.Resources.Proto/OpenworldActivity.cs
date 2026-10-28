using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__PhaseId;

	private uint? __pbn__ActivityLevel;

	private uint? __pbn__ActivityExp;

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

	[ProtoMember(2, Name = "phase_id")]
	public uint PhaseId
	{
		get
		{
			return __pbn__PhaseId.GetValueOrDefault();
		}
		set
		{
			__pbn__PhaseId = value;
		}
	}

	[ProtoMember(3, Name = "activity_level")]
	public uint ActivityLevel
	{
		get
		{
			return __pbn__ActivityLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityLevel = value;
		}
	}

	[ProtoMember(4, Name = "activity_exp")]
	public uint ActivityExp
	{
		get
		{
			return __pbn__ActivityExp.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityExp = value;
		}
	}

	[ProtoMember(5, Name = "activity_reward_taken_level_list")]
	public uint[] ActivityRewardTakenLevelLists { get; set; }

	[ProtoMember(6, Name = "activity_treasure_taken_list")]
	public uint[] ActivityTreasureTakenLists { get; set; }

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

	public bool ShouldSerializePhaseId()
	{
		return __pbn__PhaseId.HasValue;
	}

	public void ResetPhaseId()
	{
		__pbn__PhaseId = null;
	}

	public bool ShouldSerializeActivityLevel()
	{
		return __pbn__ActivityLevel.HasValue;
	}

	public void ResetActivityLevel()
	{
		__pbn__ActivityLevel = null;
	}

	public bool ShouldSerializeActivityExp()
	{
		return __pbn__ActivityExp.HasValue;
	}

	public void ResetActivityExp()
	{
		__pbn__ActivityExp = null;
	}
}
