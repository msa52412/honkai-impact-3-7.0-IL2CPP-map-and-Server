using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlotGroupIDToTriggerTime : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PlotGroupId;

	private int? __pbn__TriggerTime;

	[ProtoMember(1, Name = "plot_group_id")]
	public uint PlotGroupId
	{
		get
		{
			return __pbn__PlotGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__PlotGroupId = value;
		}
	}

	[ProtoMember(2, Name = "trigger_time")]
	public int TriggerTime
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePlotGroupId()
	{
		return __pbn__PlotGroupId.HasValue;
	}

	public void ResetPlotGroupId()
	{
		__pbn__PlotGroupId = null;
	}

	public bool ShouldSerializeTriggerTime()
	{
		return __pbn__TriggerTime.HasValue;
	}

	public void ResetTriggerTime()
	{
		__pbn__TriggerTime = null;
	}
}
