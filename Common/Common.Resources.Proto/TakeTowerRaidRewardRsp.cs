using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeTowerRaidRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4188
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HAVE_TAKE")]
		HaveTake,
		[ProtoEnum(Name = "NOT_FINISH")]
		NotFinish
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__ActivityId;

	private uint? __pbn__HardLevel;

	private TowerRaidRating? __pbn__Rating;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "schedule_id")]
	public uint ScheduleId
	{
		get
		{
			return __pbn__ScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScheduleId = value;
		}
	}

	[ProtoMember(3, Name = "activity_id")]
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

	[ProtoMember(4, Name = "hard_level")]
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

	[ProtoMember(5, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(6, Name = "rating")]
	[DefaultValue(TowerRaidRating.TowerRaidRatingNone)]
	public TowerRaidRating Rating
	{
		get
		{
			return __pbn__Rating.GetValueOrDefault();
		}
		set
		{
			__pbn__Rating = value;
		}
	}

	[ProtoMember(7, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeHardLevel()
	{
		return __pbn__HardLevel.HasValue;
	}

	public void ResetHardLevel()
	{
		__pbn__HardLevel = null;
	}

	public bool ShouldSerializeRating()
	{
		return __pbn__Rating.HasValue;
	}

	public void ResetRating()
	{
		__pbn__Rating = null;
	}
}
