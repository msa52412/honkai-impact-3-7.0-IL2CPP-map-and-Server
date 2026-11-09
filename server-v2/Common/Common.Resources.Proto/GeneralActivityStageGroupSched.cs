using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GeneralActivityStageGroupScheduleInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageGroupId;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	[ProtoMember(1, Name = "stage_group_id")]
	public uint StageGroupId
	{
		get
		{
			return __pbn__StageGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageGroupId = value;
		}
	}

	[ProtoMember(2, Name = "begin_time")]
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

	[ProtoMember(3, Name = "end_time")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageGroupId()
	{
		return __pbn__StageGroupId.HasValue;
	}

	public void ResetStageGroupId()
	{
		__pbn__StageGroupId = null;
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
}
