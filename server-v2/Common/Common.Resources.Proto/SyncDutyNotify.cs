using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncDutyNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 969
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__DailyDutyPoint;

	private uint? __pbn__WeeklyDutyPoint;

	[ProtoMember(1, Name = "daily_duty_point")]
	public uint DailyDutyPoint
	{
		get
		{
			return __pbn__DailyDutyPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyDutyPoint = value;
		}
	}

	[ProtoMember(2, Name = "has_take_daily_duty_id_list")]
	public uint[] HasTakeDailyDutyIdLists { get; set; }

	[ProtoMember(3, Name = "weekly_duty_point")]
	public uint WeeklyDutyPoint
	{
		get
		{
			return __pbn__WeeklyDutyPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__WeeklyDutyPoint = value;
		}
	}

	[ProtoMember(4, Name = "has_take_weekly_duty_id_list")]
	public uint[] HasTakeWeeklyDutyIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDailyDutyPoint()
	{
		return __pbn__DailyDutyPoint.HasValue;
	}

	public void ResetDailyDutyPoint()
	{
		__pbn__DailyDutyPoint = null;
	}

	public bool ShouldSerializeWeeklyDutyPoint()
	{
		return __pbn__WeeklyDutyPoint.HasValue;
	}

	public void ResetWeeklyDutyPoint()
	{
		__pbn__WeeklyDutyPoint = null;
	}
}
