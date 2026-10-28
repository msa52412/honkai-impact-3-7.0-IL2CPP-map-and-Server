using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetSupportActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4333
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

	private bool? __pbn__IsTakeGlobalReward;

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

	[ProtoMember(3, Name = "support_data")]
	public SupportData SupportData { get; set; }

	[ProtoMember(4, Name = "is_take_global_reward")]
	public bool IsTakeGlobalReward
	{
		get
		{
			return __pbn__IsTakeGlobalReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeGlobalReward = value;
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

	public bool ShouldSerializeIsTakeGlobalReward()
	{
		return __pbn__IsTakeGlobalReward.HasValue;
	}

	public void ResetIsTakeGlobalReward()
	{
		__pbn__IsTakeGlobalReward = null;
	}
}
