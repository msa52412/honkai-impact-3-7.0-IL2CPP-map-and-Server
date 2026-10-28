using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RaffleTicket : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TicketNum;

	private uint? __pbn__DrawIdx;

	private RaffleRewardGrade? __pbn__RewardGrade;

	private bool? __pbn__IsRewardTaken;

	[ProtoMember(1, Name = "ticket_num")]
	public uint TicketNum
	{
		get
		{
			return __pbn__TicketNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TicketNum = value;
		}
	}

	[ProtoMember(2, Name = "draw_idx")]
	public uint DrawIdx
	{
		get
		{
			return __pbn__DrawIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__DrawIdx = value;
		}
	}

	[ProtoMember(3, Name = "reward_grade")]
	[DefaultValue(RaffleRewardGrade.RaffleRewardNone)]
	public RaffleRewardGrade RewardGrade
	{
		get
		{
			return __pbn__RewardGrade.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardGrade = value;
		}
	}

	[ProtoMember(4, Name = "is_reward_taken")]
	public bool IsRewardTaken
	{
		get
		{
			return __pbn__IsRewardTaken.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRewardTaken = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTicketNum()
	{
		return __pbn__TicketNum.HasValue;
	}

	public void ResetTicketNum()
	{
		__pbn__TicketNum = null;
	}

	public bool ShouldSerializeDrawIdx()
	{
		return __pbn__DrawIdx.HasValue;
	}

	public void ResetDrawIdx()
	{
		__pbn__DrawIdx = null;
	}

	public bool ShouldSerializeRewardGrade()
	{
		return __pbn__RewardGrade.HasValue;
	}

	public void ResetRewardGrade()
	{
		__pbn__RewardGrade = null;
	}

	public bool ShouldSerializeIsRewardTaken()
	{
		return __pbn__IsRewardTaken.HasValue;
	}

	public void ResetIsRewardTaken()
	{
		__pbn__IsRewardTaken = null;
	}
}
