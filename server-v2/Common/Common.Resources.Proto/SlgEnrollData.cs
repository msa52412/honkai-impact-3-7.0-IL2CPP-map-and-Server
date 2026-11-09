using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgEnrollData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EnrollBattleScheduleId;

	private uint? __pbn__EnrollBeginTime;

	private uint? __pbn__EnrollEndTime;

	private bool? __pbn__IsEnrolled;

	[ProtoMember(1, Name = "enroll_battle_schedule_id")]
	public uint EnrollBattleScheduleId
	{
		get
		{
			return __pbn__EnrollBattleScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__EnrollBattleScheduleId = value;
		}
	}

	[ProtoMember(2, Name = "enroll_begin_time")]
	public uint EnrollBeginTime
	{
		get
		{
			return __pbn__EnrollBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EnrollBeginTime = value;
		}
	}

	[ProtoMember(3, Name = "enroll_end_time")]
	public uint EnrollEndTime
	{
		get
		{
			return __pbn__EnrollEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EnrollEndTime = value;
		}
	}

	[ProtoMember(4, Name = "is_enrolled")]
	public bool IsEnrolled
	{
		get
		{
			return __pbn__IsEnrolled.GetValueOrDefault();
		}
		set
		{
			__pbn__IsEnrolled = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEnrollBattleScheduleId()
	{
		return __pbn__EnrollBattleScheduleId.HasValue;
	}

	public void ResetEnrollBattleScheduleId()
	{
		__pbn__EnrollBattleScheduleId = null;
	}

	public bool ShouldSerializeEnrollBeginTime()
	{
		return __pbn__EnrollBeginTime.HasValue;
	}

	public void ResetEnrollBeginTime()
	{
		__pbn__EnrollBeginTime = null;
	}

	public bool ShouldSerializeEnrollEndTime()
	{
		return __pbn__EnrollEndTime.HasValue;
	}

	public void ResetEnrollEndTime()
	{
		__pbn__EnrollEndTime = null;
	}

	public bool ShouldSerializeIsEnrolled()
	{
		return __pbn__IsEnrolled.HasValue;
	}

	public void ResetIsEnrolled()
	{
		__pbn__IsEnrolled = null;
	}
}
