using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetWareHouseDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2648
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada,
		[ProtoEnum(Name = "STATUS_ERROR")]
		StatusError
	}

	private IExtension __pbn__extensionData;

	private WareHouseStatus? __pbn__Status;

	private uint? __pbn__DonateUid;

	private uint? __pbn__CdFinishTime;

	private uint? __pbn__DayDonateTimes;

	private uint? __pbn__MaxDonateTimes;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "demand_list")]
	public List<WareHouseDemand> DemandLists { get; } = new List<WareHouseDemand>();


	[ProtoMember(3, Name = "status")]
	[DefaultValue(WareHouseStatus.WarehouseStatusNone)]
	public WareHouseStatus Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(4, Name = "my_demand")]
	public WareHouseDemand MyDemand { get; set; }

	[ProtoMember(5, Name = "donate_uid")]
	public uint DonateUid
	{
		get
		{
			return __pbn__DonateUid.GetValueOrDefault();
		}
		set
		{
			__pbn__DonateUid = value;
		}
	}

	[ProtoMember(6, Name = "cd_finish_time")]
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

	[ProtoMember(7, Name = "day_donate_times")]
	public uint DayDonateTimes
	{
		get
		{
			return __pbn__DayDonateTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DayDonateTimes = value;
		}
	}

	[ProtoMember(8, Name = "max_donate_times")]
	public uint MaxDonateTimes
	{
		get
		{
			return __pbn__MaxDonateTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxDonateTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeDonateUid()
	{
		return __pbn__DonateUid.HasValue;
	}

	public void ResetDonateUid()
	{
		__pbn__DonateUid = null;
	}

	public bool ShouldSerializeCdFinishTime()
	{
		return __pbn__CdFinishTime.HasValue;
	}

	public void ResetCdFinishTime()
	{
		__pbn__CdFinishTime = null;
	}

	public bool ShouldSerializeDayDonateTimes()
	{
		return __pbn__DayDonateTimes.HasValue;
	}

	public void ResetDayDonateTimes()
	{
		__pbn__DayDonateTimes = null;
	}

	public bool ShouldSerializeMaxDonateTimes()
	{
		return __pbn__MaxDonateTimes.HasValue;
	}

	public void ResetMaxDonateTimes()
	{
		__pbn__MaxDonateTimes = null;
	}
}
