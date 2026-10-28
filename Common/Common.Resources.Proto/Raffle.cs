using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Raffle : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__DrawBeginTime;

	private uint? __pbn__DrawEndTime;

	private uint? __pbn__RewardBeginTime;

	private uint? __pbn__MissionId;

	private uint? __pbn__TotalTicketNum;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(3, Name = "draw_begin_time")]
	public uint DrawBeginTime
	{
		get
		{
			return __pbn__DrawBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DrawBeginTime = value;
		}
	}

	[ProtoMember(4, Name = "draw_end_time")]
	public uint DrawEndTime
	{
		get
		{
			return __pbn__DrawEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DrawEndTime = value;
		}
	}

	[ProtoMember(5, Name = "reward_begin_time")]
	public uint RewardBeginTime
	{
		get
		{
			return __pbn__RewardBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardBeginTime = value;
		}
	}

	[ProtoMember(6, Name = "mission_id")]
	public uint MissionId
	{
		get
		{
			return __pbn__MissionId.GetValueOrDefault();
		}
		set
		{
			__pbn__MissionId = value;
		}
	}

	[ProtoMember(7, Name = "total_ticket_num")]
	public uint TotalTicketNum
	{
		get
		{
			return __pbn__TotalTicketNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalTicketNum = value;
		}
	}

	[ProtoMember(8, Name = "ticket_list")]
	public List<RaffleTicket> TicketLists { get; } = new List<RaffleTicket>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeDrawBeginTime()
	{
		return __pbn__DrawBeginTime.HasValue;
	}

	public void ResetDrawBeginTime()
	{
		__pbn__DrawBeginTime = null;
	}

	public bool ShouldSerializeDrawEndTime()
	{
		return __pbn__DrawEndTime.HasValue;
	}

	public void ResetDrawEndTime()
	{
		__pbn__DrawEndTime = null;
	}

	public bool ShouldSerializeRewardBeginTime()
	{
		return __pbn__RewardBeginTime.HasValue;
	}

	public void ResetRewardBeginTime()
	{
		__pbn__RewardBeginTime = null;
	}

	public bool ShouldSerializeMissionId()
	{
		return __pbn__MissionId.HasValue;
	}

	public void ResetMissionId()
	{
		__pbn__MissionId = null;
	}

	public bool ShouldSerializeTotalTicketNum()
	{
		return __pbn__TotalTicketNum.HasValue;
	}

	public void ResetTotalTicketNum()
	{
		__pbn__TotalTicketNum = null;
	}
}
