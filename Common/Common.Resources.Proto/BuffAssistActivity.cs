using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuffAssistActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__BuffScheduleId;

	private uint? __pbn__RefreshBuffPoolTimes;

	private uint? __pbn__CurStageGroupId;

	private uint? __pbn__LeftSelectBuffTimes;

	private uint? __pbn__AssistLevel;

	private uint? __pbn__AssistNum;

	private uint? __pbn__BeAssistedNum;

	private uint? __pbn__PublishBuffPoolTimes;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "buff_schedule_id")]
	public uint BuffScheduleId
	{
		get
		{
			return __pbn__BuffScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffScheduleId = value;
		}
	}

	[ProtoMember(3, Name = "wait_select_buff_pool")]
	public BuffAssistBuffPool WaitSelectBuffPool { get; set; }

	[ProtoMember(4, Name = "produce_buff_pool_list")]
	public List<BuffAssistBuffPool> ProduceBuffPoolLists { get; } = new List<BuffAssistBuffPool>();


	[ProtoMember(5, Name = "buff_slot_list")]
	public List<BuffAssistPlayerBuffSlot> BuffSlotLists { get; } = new List<BuffAssistPlayerBuffSlot>();


	[ProtoMember(6, Name = "refresh_buff_pool_times")]
	public uint RefreshBuffPoolTimes
	{
		get
		{
			return __pbn__RefreshBuffPoolTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshBuffPoolTimes = value;
		}
	}

	[ProtoMember(7, Name = "cur_stage_group_id")]
	public uint CurStageGroupId
	{
		get
		{
			return __pbn__CurStageGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurStageGroupId = value;
		}
	}

	[ProtoMember(8, Name = "left_select_buff_times")]
	public uint LeftSelectBuffTimes
	{
		get
		{
			return __pbn__LeftSelectBuffTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftSelectBuffTimes = value;
		}
	}

	[ProtoMember(9, Name = "history_list")]
	public List<BuffAssistHistory> HistoryLists { get; } = new List<BuffAssistHistory>();


	[ProtoMember(10, Name = "assist_level")]
	public uint AssistLevel
	{
		get
		{
			return __pbn__AssistLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistLevel = value;
		}
	}

	[ProtoMember(11, Name = "assist_num")]
	public uint AssistNum
	{
		get
		{
			return __pbn__AssistNum.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistNum = value;
		}
	}

	[ProtoMember(12, Name = "be_assisted_num")]
	public uint BeAssistedNum
	{
		get
		{
			return __pbn__BeAssistedNum.GetValueOrDefault();
		}
		set
		{
			__pbn__BeAssistedNum = value;
		}
	}

	[ProtoMember(13, Name = "publish_buff_pool_times")]
	public uint PublishBuffPoolTimes
	{
		get
		{
			return __pbn__PublishBuffPoolTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__PublishBuffPoolTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeBuffScheduleId()
	{
		return __pbn__BuffScheduleId.HasValue;
	}

	public void ResetBuffScheduleId()
	{
		__pbn__BuffScheduleId = null;
	}

	public bool ShouldSerializeRefreshBuffPoolTimes()
	{
		return __pbn__RefreshBuffPoolTimes.HasValue;
	}

	public void ResetRefreshBuffPoolTimes()
	{
		__pbn__RefreshBuffPoolTimes = null;
	}

	public bool ShouldSerializeCurStageGroupId()
	{
		return __pbn__CurStageGroupId.HasValue;
	}

	public void ResetCurStageGroupId()
	{
		__pbn__CurStageGroupId = null;
	}

	public bool ShouldSerializeLeftSelectBuffTimes()
	{
		return __pbn__LeftSelectBuffTimes.HasValue;
	}

	public void ResetLeftSelectBuffTimes()
	{
		__pbn__LeftSelectBuffTimes = null;
	}

	public bool ShouldSerializeAssistLevel()
	{
		return __pbn__AssistLevel.HasValue;
	}

	public void ResetAssistLevel()
	{
		__pbn__AssistLevel = null;
	}

	public bool ShouldSerializeAssistNum()
	{
		return __pbn__AssistNum.HasValue;
	}

	public void ResetAssistNum()
	{
		__pbn__AssistNum = null;
	}

	public bool ShouldSerializeBeAssistedNum()
	{
		return __pbn__BeAssistedNum.HasValue;
	}

	public void ResetBeAssistedNum()
	{
		__pbn__BeAssistedNum = null;
	}

	public bool ShouldSerializePublishBuffPoolTimes()
	{
		return __pbn__PublishBuffPoolTimes.HasValue;
	}

	public void ResetPublishBuffPoolTimes()
	{
		__pbn__PublishBuffPoolTimes = null;
	}
}
