using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetKingdomWarRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3051
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

	private uint? __pbn__DisplayBeginTime;

	private uint? __pbn__DisplayEndTime;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__KingdomId;

	private uint? __pbn__ActivityStamina;

	private uint? __pbn__LastRecoverStaminaTime;

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

	[ProtoMember(3, Name = "display_begin_time")]
	public uint DisplayBeginTime
	{
		get
		{
			return __pbn__DisplayBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DisplayBeginTime = value;
		}
	}

	[ProtoMember(4, Name = "display_end_time")]
	public uint DisplayEndTime
	{
		get
		{
			return __pbn__DisplayEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DisplayEndTime = value;
		}
	}

	[ProtoMember(5, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(6, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(7, Name = "kingdom_war")]
	public KingdomWarData KingdomWar { get; set; }

	[ProtoMember(8, Name = "kingdom_id")]
	public uint KingdomId
	{
		get
		{
			return __pbn__KingdomId.GetValueOrDefault();
		}
		set
		{
			__pbn__KingdomId = value;
		}
	}

	[ProtoMember(9, Name = "activity_stamina")]
	public uint ActivityStamina
	{
		get
		{
			return __pbn__ActivityStamina.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityStamina = value;
		}
	}

	[ProtoMember(10, Name = "last_recover_stamina_time")]
	public uint LastRecoverStaminaTime
	{
		get
		{
			return __pbn__LastRecoverStaminaTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastRecoverStaminaTime = value;
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

	public bool ShouldSerializeDisplayBeginTime()
	{
		return __pbn__DisplayBeginTime.HasValue;
	}

	public void ResetDisplayBeginTime()
	{
		__pbn__DisplayBeginTime = null;
	}

	public bool ShouldSerializeDisplayEndTime()
	{
		return __pbn__DisplayEndTime.HasValue;
	}

	public void ResetDisplayEndTime()
	{
		__pbn__DisplayEndTime = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeKingdomId()
	{
		return __pbn__KingdomId.HasValue;
	}

	public void ResetKingdomId()
	{
		__pbn__KingdomId = null;
	}

	public bool ShouldSerializeActivityStamina()
	{
		return __pbn__ActivityStamina.HasValue;
	}

	public void ResetActivityStamina()
	{
		__pbn__ActivityStamina = null;
	}

	public bool ShouldSerializeLastRecoverStaminaTime()
	{
		return __pbn__LastRecoverStaminaTime.HasValue;
	}

	public void ResetLastRecoverStaminaTime()
	{
		__pbn__LastRecoverStaminaTime = null;
	}
}
