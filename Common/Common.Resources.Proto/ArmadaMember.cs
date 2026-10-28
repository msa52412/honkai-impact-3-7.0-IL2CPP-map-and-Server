using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaMember : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private ArmadaPosition? __pbn__Position;

	private uint? __pbn__WeekPopl;

	private uint? __pbn__TotalPopl;

	private uint? __pbn__WeekFund;

	private uint? __pbn__TotalFund;

	private uint? __pbn__WeekContribution;

	private uint? __pbn__TotalContribution;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "position")]
	[DefaultValue(ArmadaPosition.ArmadaPositionNone)]
	public ArmadaPosition Position
	{
		get
		{
			return __pbn__Position.GetValueOrDefault();
		}
		set
		{
			__pbn__Position = value;
		}
	}

	[ProtoMember(3, Name = "brief_data")]
	public PlayerFriendBriefData BriefData { get; set; }

	[ProtoMember(4, Name = "week_popl")]
	public uint WeekPopl
	{
		get
		{
			return __pbn__WeekPopl.GetValueOrDefault();
		}
		set
		{
			__pbn__WeekPopl = value;
		}
	}

	[ProtoMember(5, Name = "total_popl")]
	public uint TotalPopl
	{
		get
		{
			return __pbn__TotalPopl.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalPopl = value;
		}
	}

	[ProtoMember(6, Name = "week_fund")]
	public uint WeekFund
	{
		get
		{
			return __pbn__WeekFund.GetValueOrDefault();
		}
		set
		{
			__pbn__WeekFund = value;
		}
	}

	[ProtoMember(7, Name = "total_fund")]
	public uint TotalFund
	{
		get
		{
			return __pbn__TotalFund.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalFund = value;
		}
	}

	[ProtoMember(8, Name = "week_contribution")]
	public uint WeekContribution
	{
		get
		{
			return __pbn__WeekContribution.GetValueOrDefault();
		}
		set
		{
			__pbn__WeekContribution = value;
		}
	}

	[ProtoMember(9, Name = "total_contribution")]
	public uint TotalContribution
	{
		get
		{
			return __pbn__TotalContribution.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalContribution = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializePosition()
	{
		return __pbn__Position.HasValue;
	}

	public void ResetPosition()
	{
		__pbn__Position = null;
	}

	public bool ShouldSerializeWeekPopl()
	{
		return __pbn__WeekPopl.HasValue;
	}

	public void ResetWeekPopl()
	{
		__pbn__WeekPopl = null;
	}

	public bool ShouldSerializeTotalPopl()
	{
		return __pbn__TotalPopl.HasValue;
	}

	public void ResetTotalPopl()
	{
		__pbn__TotalPopl = null;
	}

	public bool ShouldSerializeWeekFund()
	{
		return __pbn__WeekFund.HasValue;
	}

	public void ResetWeekFund()
	{
		__pbn__WeekFund = null;
	}

	public bool ShouldSerializeTotalFund()
	{
		return __pbn__TotalFund.HasValue;
	}

	public void ResetTotalFund()
	{
		__pbn__TotalFund = null;
	}

	public bool ShouldSerializeWeekContribution()
	{
		return __pbn__WeekContribution.HasValue;
	}

	public void ResetWeekContribution()
	{
		__pbn__WeekContribution = null;
	}

	public bool ShouldSerializeTotalContribution()
	{
		return __pbn__TotalContribution.HasValue;
	}

	public void ResetTotalContribution()
	{
		__pbn__TotalContribution = null;
	}
}
