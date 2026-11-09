using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetNewConsignedOrderDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 518
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
}
