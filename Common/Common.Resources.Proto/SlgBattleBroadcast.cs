using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgBattleBroadcast : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BroadcastId;

	private uint? __pbn__TriggerTime;

	private uint? __pbn__PointId;

	private uint? __pbn__Para;

	[ProtoMember(1, Name = "broadcast_id")]
	public uint BroadcastId
	{
		get
		{
			return __pbn__BroadcastId.GetValueOrDefault();
		}
		set
		{
			__pbn__BroadcastId = value;
		}
	}

	[ProtoMember(2, Name = "trigger_time")]
	public uint TriggerTime
	{
		get
		{
			return __pbn__TriggerTime.GetValueOrDefault();
		}
		set
		{
			__pbn__TriggerTime = value;
		}
	}

	[ProtoMember(3, Name = "point_id")]
	public uint PointId
	{
		get
		{
			return __pbn__PointId.GetValueOrDefault();
		}
		set
		{
			__pbn__PointId = value;
		}
	}

	[ProtoMember(4, Name = "para")]
	public uint Para
	{
		get
		{
			return __pbn__Para.GetValueOrDefault();
		}
		set
		{
			__pbn__Para = value;
		}
	}

	[ProtoMember(5, Name = "trigger_player")]
	public SlgBroadcastPlayer TriggerPlayer { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBroadcastId()
	{
		return __pbn__BroadcastId.HasValue;
	}

	public void ResetBroadcastId()
	{
		__pbn__BroadcastId = null;
	}

	public bool ShouldSerializeTriggerTime()
	{
		return __pbn__TriggerTime.HasValue;
	}

	public void ResetTriggerTime()
	{
		__pbn__TriggerTime = null;
	}

	public bool ShouldSerializePointId()
	{
		return __pbn__PointId.HasValue;
	}

	public void ResetPointId()
	{
		__pbn__PointId = null;
	}

	public bool ShouldSerializePara()
	{
		return __pbn__Para.HasValue;
	}

	public void ResetPara()
	{
		__pbn__Para = null;
	}
}
