using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RecommendAvatar : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleTimes;

	private float? __pbn__BattleRate;

	private AvatarRecommendSource? __pbn__Source;

	private string __pbn__ExtInfo;

	private uint? __pbn__RecommendStyle;

	private uint? __pbn__Rank;

	private bool? __pbn__IsHasCourseInfo;

	[ProtoMember(1, Name = "lineup_info")]
	public LineupBriefInfo LineupInfo { get; set; }

	[ProtoMember(2, Name = "battle_times")]
	public uint BattleTimes
	{
		get
		{
			return __pbn__BattleTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleTimes = value;
		}
	}

	[ProtoMember(3, Name = "battle_rate")]
	public float BattleRate
	{
		get
		{
			return __pbn__BattleRate.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleRate = value;
		}
	}

	[ProtoMember(5, Name = "source")]
	[DefaultValue(AvatarRecommendSource.AvatarRecommendSourceNone)]
	public AvatarRecommendSource Source
	{
		get
		{
			return __pbn__Source.GetValueOrDefault();
		}
		set
		{
			__pbn__Source = value;
		}
	}

	[ProtoMember(8, Name = "ext_info")]
	[DefaultValue("")]
	public string ExtInfo
	{
		get
		{
			return __pbn__ExtInfo ?? "";
		}
		set
		{
			__pbn__ExtInfo = value;
		}
	}

	[ProtoMember(9, Name = "recommend_style")]
	public uint RecommendStyle
	{
		get
		{
			return __pbn__RecommendStyle.GetValueOrDefault();
		}
		set
		{
			__pbn__RecommendStyle = value;
		}
	}

	[ProtoMember(10, Name = "rank")]
	public uint Rank
	{
		get
		{
			return __pbn__Rank.GetValueOrDefault();
		}
		set
		{
			__pbn__Rank = value;
		}
	}

	[ProtoMember(11, Name = "is_has_course_info")]
	public bool IsHasCourseInfo
	{
		get
		{
			return __pbn__IsHasCourseInfo.GetValueOrDefault();
		}
		set
		{
			__pbn__IsHasCourseInfo = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleTimes()
	{
		return __pbn__BattleTimes.HasValue;
	}

	public void ResetBattleTimes()
	{
		__pbn__BattleTimes = null;
	}

	public bool ShouldSerializeBattleRate()
	{
		return __pbn__BattleRate.HasValue;
	}

	public void ResetBattleRate()
	{
		__pbn__BattleRate = null;
	}

	public bool ShouldSerializeSource()
	{
		return __pbn__Source.HasValue;
	}

	public void ResetSource()
	{
		__pbn__Source = null;
	}

	public bool ShouldSerializeExtInfo()
	{
		return __pbn__ExtInfo != null;
	}

	public void ResetExtInfo()
	{
		__pbn__ExtInfo = null;
	}

	public bool ShouldSerializeRecommendStyle()
	{
		return __pbn__RecommendStyle.HasValue;
	}

	public void ResetRecommendStyle()
	{
		__pbn__RecommendStyle = null;
	}

	public bool ShouldSerializeRank()
	{
		return __pbn__Rank.HasValue;
	}

	public void ResetRank()
	{
		__pbn__Rank = null;
	}

	public bool ShouldSerializeIsHasCourseInfo()
	{
		return __pbn__IsHasCourseInfo.HasValue;
	}

	public void ResetIsHasCourseInfo()
	{
		__pbn__IsHasCourseInfo = null;
	}
}
