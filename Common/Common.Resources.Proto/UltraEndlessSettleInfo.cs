using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UltraEndlessSettleInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__GroupLevel;

	private uint? __pbn__MaxStageScore;

	private uint? __pbn__GroupMemberNum;

	private uint? __pbn__Rank;

	private uint? __pbn__CupNumBefore;

	private uint? __pbn__CupNumAfterScheduleSettle;

	private uint? __pbn__CupNumAfterSeasonSettle;

	private uint? __pbn__CupNumBeforeSeasonSettle;

	private uint? __pbn__CupNum;

	private uint? __pbn__MmrScore;

	private uint? __pbn__BufferCupBeforeScheduleSettle;

	private uint? __pbn__BufferCup;

	[ProtoMember(1, Name = "schedule_id")]
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

	[ProtoMember(2, Name = "group_level")]
	public uint GroupLevel
	{
		get
		{
			return __pbn__GroupLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupLevel = value;
		}
	}

	[ProtoMember(3, Name = "max_stage_score")]
	public uint MaxStageScore
	{
		get
		{
			return __pbn__MaxStageScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxStageScore = value;
		}
	}

	[ProtoMember(4, Name = "group_member_num")]
	public uint GroupMemberNum
	{
		get
		{
			return __pbn__GroupMemberNum.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupMemberNum = value;
		}
	}

	[ProtoMember(5, Name = "rank")]
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

	[ProtoMember(6, Name = "cup_num_before")]
	public uint CupNumBefore
	{
		get
		{
			return __pbn__CupNumBefore.GetValueOrDefault();
		}
		set
		{
			__pbn__CupNumBefore = value;
		}
	}

	[ProtoMember(7, Name = "cup_num_after_schedule_settle")]
	public uint CupNumAfterScheduleSettle
	{
		get
		{
			return __pbn__CupNumAfterScheduleSettle.GetValueOrDefault();
		}
		set
		{
			__pbn__CupNumAfterScheduleSettle = value;
		}
	}

	[ProtoMember(8, Name = "cup_num_after_season_settle")]
	public uint CupNumAfterSeasonSettle
	{
		get
		{
			return __pbn__CupNumAfterSeasonSettle.GetValueOrDefault();
		}
		set
		{
			__pbn__CupNumAfterSeasonSettle = value;
		}
	}

	[ProtoMember(9, Name = "cup_num_before_season_settle")]
	public uint CupNumBeforeSeasonSettle
	{
		get
		{
			return __pbn__CupNumBeforeSeasonSettle.GetValueOrDefault();
		}
		set
		{
			__pbn__CupNumBeforeSeasonSettle = value;
		}
	}

	[ProtoMember(10, Name = "cup_num")]
	public uint CupNum
	{
		get
		{
			return __pbn__CupNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CupNum = value;
		}
	}

	[ProtoMember(11, Name = "old_endless_settle_list")]
	public List<OldEndlessPlayerSettle> OldEndlessSettleLists { get; } = new List<OldEndlessPlayerSettle>();


	[ProtoMember(12, Name = "mmr_score")]
	public uint MmrScore
	{
		get
		{
			return __pbn__MmrScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MmrScore = value;
		}
	}

	[ProtoMember(13, Name = "buffer_cup_before_schedule_settle")]
	public uint BufferCupBeforeScheduleSettle
	{
		get
		{
			return __pbn__BufferCupBeforeScheduleSettle.GetValueOrDefault();
		}
		set
		{
			__pbn__BufferCupBeforeScheduleSettle = value;
		}
	}

	[ProtoMember(15, Name = "buffer_cup")]
	public uint BufferCup
	{
		get
		{
			return __pbn__BufferCup.GetValueOrDefault();
		}
		set
		{
			__pbn__BufferCup = value;
		}
	}

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

	public bool ShouldSerializeGroupLevel()
	{
		return __pbn__GroupLevel.HasValue;
	}

	public void ResetGroupLevel()
	{
		__pbn__GroupLevel = null;
	}

	public bool ShouldSerializeMaxStageScore()
	{
		return __pbn__MaxStageScore.HasValue;
	}

	public void ResetMaxStageScore()
	{
		__pbn__MaxStageScore = null;
	}

	public bool ShouldSerializeGroupMemberNum()
	{
		return __pbn__GroupMemberNum.HasValue;
	}

	public void ResetGroupMemberNum()
	{
		__pbn__GroupMemberNum = null;
	}

	public bool ShouldSerializeRank()
	{
		return __pbn__Rank.HasValue;
	}

	public void ResetRank()
	{
		__pbn__Rank = null;
	}

	public bool ShouldSerializeCupNumBefore()
	{
		return __pbn__CupNumBefore.HasValue;
	}

	public void ResetCupNumBefore()
	{
		__pbn__CupNumBefore = null;
	}

	public bool ShouldSerializeCupNumAfterScheduleSettle()
	{
		return __pbn__CupNumAfterScheduleSettle.HasValue;
	}

	public void ResetCupNumAfterScheduleSettle()
	{
		__pbn__CupNumAfterScheduleSettle = null;
	}

	public bool ShouldSerializeCupNumAfterSeasonSettle()
	{
		return __pbn__CupNumAfterSeasonSettle.HasValue;
	}

	public void ResetCupNumAfterSeasonSettle()
	{
		__pbn__CupNumAfterSeasonSettle = null;
	}

	public bool ShouldSerializeCupNumBeforeSeasonSettle()
	{
		return __pbn__CupNumBeforeSeasonSettle.HasValue;
	}

	public void ResetCupNumBeforeSeasonSettle()
	{
		__pbn__CupNumBeforeSeasonSettle = null;
	}

	public bool ShouldSerializeCupNum()
	{
		return __pbn__CupNum.HasValue;
	}

	public void ResetCupNum()
	{
		__pbn__CupNum = null;
	}

	public bool ShouldSerializeMmrScore()
	{
		return __pbn__MmrScore.HasValue;
	}

	public void ResetMmrScore()
	{
		__pbn__MmrScore = null;
	}

	public bool ShouldSerializeBufferCupBeforeScheduleSettle()
	{
		return __pbn__BufferCupBeforeScheduleSettle.HasValue;
	}

	public void ResetBufferCupBeforeScheduleSettle()
	{
		__pbn__BufferCupBeforeScheduleSettle = null;
	}

	public bool ShouldSerializeBufferCup()
	{
		return __pbn__BufferCup.HasValue;
	}

	public void ResetBufferCup()
	{
		__pbn__BufferCup = null;
	}
}
