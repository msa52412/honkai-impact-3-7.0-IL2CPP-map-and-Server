using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetRaidActiveInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1623
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private RaidActiveType? __pbn__ActiveType;

	private bool? __pbn__HasDailyRaidActivity;

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

	[ProtoMember(3, Name = "active_type")]
	[DefaultValue(RaidActiveType.RaidActiveNone)]
	public RaidActiveType ActiveType
	{
		get
		{
			return __pbn__ActiveType.GetValueOrDefault();
		}
		set
		{
			__pbn__ActiveType = value;
		}
	}

	[ProtoMember(4, Name = "has_daily_raid_activity")]
	public bool HasDailyRaidActivity
	{
		get
		{
			return __pbn__HasDailyRaidActivity.GetValueOrDefault();
		}
		set
		{
			__pbn__HasDailyRaidActivity = value;
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

	public bool ShouldSerializeActiveType()
	{
		return __pbn__ActiveType.HasValue;
	}

	public void ResetActiveType()
	{
		__pbn__ActiveType = null;
	}

	public bool ShouldSerializeHasDailyRaidActivity()
	{
		return __pbn__HasDailyRaidActivity.HasValue;
	}

	public void ResetHasDailyRaidActivity()
	{
		__pbn__HasDailyRaidActivity = null;
	}
}
