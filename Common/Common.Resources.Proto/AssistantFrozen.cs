using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AssistantFrozen : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__LeftFrozenTime;

	private uint? __pbn__LeftAssistantTimes;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "left_frozen_time")]
	public uint LeftFrozenTime
	{
		get
		{
			return __pbn__LeftFrozenTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftFrozenTime = value;
		}
	}

	[ProtoMember(3, Name = "left_assistant_times")]
	public uint LeftAssistantTimes
	{
		get
		{
			return __pbn__LeftAssistantTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftAssistantTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeLeftFrozenTime()
	{
		return __pbn__LeftFrozenTime.HasValue;
	}

	public void ResetLeftFrozenTime()
	{
		__pbn__LeftFrozenTime = null;
	}

	public bool ShouldSerializeLeftAssistantTimes()
	{
		return __pbn__LeftAssistantTimes.HasValue;
	}

	public void ResetLeftAssistantTimes()
	{
		__pbn__LeftAssistantTimes = null;
	}
}
