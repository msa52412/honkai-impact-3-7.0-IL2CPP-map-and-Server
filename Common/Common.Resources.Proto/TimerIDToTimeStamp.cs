using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TimerIDToTimeStamp : IExtensible
{
	private IExtension __pbn__extensionData;

	private int? __pbn__TimerId;

	private uint? __pbn__TimeStamp;

	[ProtoMember(1, Name = "timer_id")]
	public int TimerId
	{
		get
		{
			return __pbn__TimerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TimerId = value;
		}
	}

	[ProtoMember(2, Name = "time_stamp")]
	public uint TimeStamp
	{
		get
		{
			return __pbn__TimeStamp.GetValueOrDefault();
		}
		set
		{
			__pbn__TimeStamp = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTimerId()
	{
		return __pbn__TimerId.HasValue;
	}

	public void ResetTimerId()
	{
		__pbn__TimerId = null;
	}

	public bool ShouldSerializeTimeStamp()
	{
		return __pbn__TimeStamp.HasValue;
	}

	public void ResetTimeStamp()
	{
		__pbn__TimeStamp = null;
	}
}
