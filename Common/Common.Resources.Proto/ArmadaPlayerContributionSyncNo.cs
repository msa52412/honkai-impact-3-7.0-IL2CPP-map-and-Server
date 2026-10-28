using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaPlayerContributionSyncNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2806
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ArmadaContribution;

	private uint? __pbn__Uid;

	private uint? __pbn__WeekContribution;

	private uint? __pbn__TotalContribution;

	[ProtoMember(1, Name = "armada_contribution")]
	public uint ArmadaContribution
	{
		get
		{
			return __pbn__ArmadaContribution.GetValueOrDefault();
		}
		set
		{
			__pbn__ArmadaContribution = value;
		}
	}

	[ProtoMember(2, Name = "uid")]
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

	[ProtoMember(3, Name = "week_contribution")]
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

	[ProtoMember(4, Name = "total_contribution")]
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

	public bool ShouldSerializeArmadaContribution()
	{
		return __pbn__ArmadaContribution.HasValue;
	}

	public void ResetArmadaContribution()
	{
		__pbn__ArmadaContribution = null;
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
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
