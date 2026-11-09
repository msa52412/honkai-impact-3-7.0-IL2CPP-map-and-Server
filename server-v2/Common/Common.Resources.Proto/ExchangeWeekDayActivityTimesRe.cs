using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExchangeWeekDayActivityTimesReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 259
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__ActivityTimes;

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

	[ProtoMember(2, Name = "activity_times")]
	public uint ActivityTimes
	{
		get
		{
			return __pbn__ActivityTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityTimes = value;
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

	public bool ShouldSerializeActivityTimes()
	{
		return __pbn__ActivityTimes.HasValue;
	}

	public void ResetActivityTimes()
	{
		__pbn__ActivityTimes = null;
	}
}
