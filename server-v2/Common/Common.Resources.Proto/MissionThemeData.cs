using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MissionThemeData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ThemeId;

	private bool? __pbn__IsThemeUpgrade;

	private bool? __pbn__IsHasTakeAllExtraReward;

	[ProtoMember(1, Name = "theme_id")]
	public uint ThemeId
	{
		get
		{
			return __pbn__ThemeId.GetValueOrDefault();
		}
		set
		{
			__pbn__ThemeId = value;
		}
	}

	[ProtoMember(2, Name = "is_theme_upgrade")]
	public bool IsThemeUpgrade
	{
		get
		{
			return __pbn__IsThemeUpgrade.GetValueOrDefault();
		}
		set
		{
			__pbn__IsThemeUpgrade = value;
		}
	}

	[ProtoMember(3, Name = "has_take_extra_reward_mission_list")]
	public uint[] HasTakeExtraRewardMissionLists { get; set; }

	[ProtoMember(4, Name = "is_has_take_all_extra_reward")]
	public bool IsHasTakeAllExtraReward
	{
		get
		{
			return __pbn__IsHasTakeAllExtraReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsHasTakeAllExtraReward = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeThemeId()
	{
		return __pbn__ThemeId.HasValue;
	}

	public void ResetThemeId()
	{
		__pbn__ThemeId = null;
	}

	public bool ShouldSerializeIsThemeUpgrade()
	{
		return __pbn__IsThemeUpgrade.HasValue;
	}

	public void ResetIsThemeUpgrade()
	{
		__pbn__IsThemeUpgrade = null;
	}

	public bool ShouldSerializeIsHasTakeAllExtraReward()
	{
		return __pbn__IsHasTakeAllExtraReward.HasValue;
	}

	public void ResetIsHasTakeAllExtraReward()
	{
		__pbn__IsHasTakeAllExtraReward = null;
	}
}
