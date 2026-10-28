using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalPollingGetDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5812
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen = 3,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed = 4
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__LastSupportTime;

	private uint? __pbn__SupportTargetId;

	private uint? __pbn__VoteBeginTime;

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

	[ProtoMember(3, Name = "last_support_time")]
	public uint LastSupportTime
	{
		get
		{
			return __pbn__LastSupportTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastSupportTime = value;
		}
	}

	[ProtoMember(4, Name = "support_target_id")]
	public uint SupportTargetId
	{
		get
		{
			return __pbn__SupportTargetId.GetValueOrDefault();
		}
		set
		{
			__pbn__SupportTargetId = value;
		}
	}

	[ProtoMember(5, Name = "vote_begin_time")]
	public uint VoteBeginTime
	{
		get
		{
			return __pbn__VoteBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__VoteBeginTime = value;
		}
	}

	[ProtoMember(6, Name = "target_data_list")]
	public List<GlobalPollingTargetData> TargetDataLists { get; } = new List<GlobalPollingTargetData>();


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

	public bool ShouldSerializeLastSupportTime()
	{
		return __pbn__LastSupportTime.HasValue;
	}

	public void ResetLastSupportTime()
	{
		__pbn__LastSupportTime = null;
	}

	public bool ShouldSerializeSupportTargetId()
	{
		return __pbn__SupportTargetId.HasValue;
	}

	public void ResetSupportTargetId()
	{
		__pbn__SupportTargetId = null;
	}

	public bool ShouldSerializeVoteBeginTime()
	{
		return __pbn__VoteBeginTime.HasValue;
	}

	public void ResetVoteBeginTime()
	{
		__pbn__VoteBeginTime = null;
	}
}
