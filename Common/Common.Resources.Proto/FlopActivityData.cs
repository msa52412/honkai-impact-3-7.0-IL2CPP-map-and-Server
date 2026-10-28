using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FlopActivityData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__FlopTimes;

	private uint? __pbn__CurFrontCard;

	private uint? __pbn__ShowId;

	private uint? __pbn__TotalFlopTimes;

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

	[ProtoMember(2, Name = "card_list")]
	public List<FlopActivityCardData> CardLists { get; } = new List<FlopActivityCardData>();


	[ProtoMember(3, Name = "has_take_reward_list")]
	public uint[] HasTakeRewardLists { get; set; }

	[ProtoMember(4, Name = "flop_times")]
	public uint FlopTimes
	{
		get
		{
			return __pbn__FlopTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__FlopTimes = value;
		}
	}

	[ProtoMember(5, Name = "cur_front_card")]
	public uint CurFrontCard
	{
		get
		{
			return __pbn__CurFrontCard.GetValueOrDefault();
		}
		set
		{
			__pbn__CurFrontCard = value;
		}
	}

	[ProtoMember(6, Name = "show_id")]
	public uint ShowId
	{
		get
		{
			return __pbn__ShowId.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowId = value;
		}
	}

	[ProtoMember(7, Name = "total_flop_times")]
	public uint TotalFlopTimes
	{
		get
		{
			return __pbn__TotalFlopTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalFlopTimes = value;
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

	public bool ShouldSerializeFlopTimes()
	{
		return __pbn__FlopTimes.HasValue;
	}

	public void ResetFlopTimes()
	{
		__pbn__FlopTimes = null;
	}

	public bool ShouldSerializeCurFrontCard()
	{
		return __pbn__CurFrontCard.HasValue;
	}

	public void ResetCurFrontCard()
	{
		__pbn__CurFrontCard = null;
	}

	public bool ShouldSerializeShowId()
	{
		return __pbn__ShowId.HasValue;
	}

	public void ResetShowId()
	{
		__pbn__ShowId = null;
	}

	public bool ShouldSerializeTotalFlopTimes()
	{
		return __pbn__TotalFlopTimes.HasValue;
	}

	public void ResetTotalFlopTimes()
	{
		__pbn__TotalFlopTimes = null;
	}
}
