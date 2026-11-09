using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetConsignedOrderDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2640
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Progress;

	private uint? __pbn__CdFinishTime;

	private uint? __pbn__FinishOrderTimes;

	private uint? __pbn__TotalOrderTimes;

	private uint? __pbn__ArmadaFinishTimes;

	private uint? __pbn__ApplyOrderTimes;

	private uint? __pbn__TotalApplyTimes;

	private uint? __pbn__MyCredit;

	private bool? __pbn__IsRewardTaken;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "progress")]
	public uint Progress
	{
		get
		{
			return __pbn__Progress.GetValueOrDefault();
		}
		set
		{
			__pbn__Progress = value;
		}
	}

	[ProtoMember(3, Name = "order_list")]
	public List<ConsignedOrder> OrderLists { get; } = new List<ConsignedOrder>();


	[ProtoMember(4, Name = "cd_finish_time")]
	public uint CdFinishTime
	{
		get
		{
			return __pbn__CdFinishTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CdFinishTime = value;
		}
	}

	[ProtoMember(5, Name = "finish_order_times")]
	public uint FinishOrderTimes
	{
		get
		{
			return __pbn__FinishOrderTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishOrderTimes = value;
		}
	}

	[ProtoMember(6, Name = "total_order_times")]
	public uint TotalOrderTimes
	{
		get
		{
			return __pbn__TotalOrderTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalOrderTimes = value;
		}
	}

	[ProtoMember(7, Name = "armada_finish_times")]
	public uint ArmadaFinishTimes
	{
		get
		{
			return __pbn__ArmadaFinishTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ArmadaFinishTimes = value;
		}
	}

	[ProtoMember(8, Name = "apply_order_times")]
	public uint ApplyOrderTimes
	{
		get
		{
			return __pbn__ApplyOrderTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ApplyOrderTimes = value;
		}
	}

	[ProtoMember(9, Name = "total_apply_times")]
	public uint TotalApplyTimes
	{
		get
		{
			return __pbn__TotalApplyTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalApplyTimes = value;
		}
	}

	[ProtoMember(10, Name = "my_credit")]
	public uint MyCredit
	{
		get
		{
			return __pbn__MyCredit.GetValueOrDefault();
		}
		set
		{
			__pbn__MyCredit = value;
		}
	}

	[ProtoMember(11, Name = "is_reward_taken")]
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

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeCdFinishTime()
	{
		return __pbn__CdFinishTime.HasValue;
	}

	public void ResetCdFinishTime()
	{
		__pbn__CdFinishTime = null;
	}

	public bool ShouldSerializeFinishOrderTimes()
	{
		return __pbn__FinishOrderTimes.HasValue;
	}

	public void ResetFinishOrderTimes()
	{
		__pbn__FinishOrderTimes = null;
	}

	public bool ShouldSerializeTotalOrderTimes()
	{
		return __pbn__TotalOrderTimes.HasValue;
	}

	public void ResetTotalOrderTimes()
	{
		__pbn__TotalOrderTimes = null;
	}

	public bool ShouldSerializeArmadaFinishTimes()
	{
		return __pbn__ArmadaFinishTimes.HasValue;
	}

	public void ResetArmadaFinishTimes()
	{
		__pbn__ArmadaFinishTimes = null;
	}

	public bool ShouldSerializeApplyOrderTimes()
	{
		return __pbn__ApplyOrderTimes.HasValue;
	}

	public void ResetApplyOrderTimes()
	{
		__pbn__ApplyOrderTimes = null;
	}

	public bool ShouldSerializeTotalApplyTimes()
	{
		return __pbn__TotalApplyTimes.HasValue;
	}

	public void ResetTotalApplyTimes()
	{
		__pbn__TotalApplyTimes = null;
	}

	public bool ShouldSerializeMyCredit()
	{
		return __pbn__MyCredit.HasValue;
	}

	public void ResetMyCredit()
	{
		__pbn__MyCredit = null;
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
