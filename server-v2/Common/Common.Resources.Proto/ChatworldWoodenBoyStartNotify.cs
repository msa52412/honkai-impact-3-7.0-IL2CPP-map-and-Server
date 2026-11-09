using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldWoodenBoyStartNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2481
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StartTime;

	private uint? __pbn__TimelineId;

	[ProtoMember(1, Name = "start_time")]
	public uint StartTime
	{
		get
		{
			return __pbn__StartTime.GetValueOrDefault();
		}
		set
		{
			__pbn__StartTime = value;
		}
	}

	[ProtoMember(2, Name = "npc_switch_time_list")]
	public uint[] NpcSwitchTimeLists { get; set; }

	[ProtoMember(3, Name = "timeline_id")]
	public uint TimelineId
	{
		get
		{
			return __pbn__TimelineId.GetValueOrDefault();
		}
		set
		{
			__pbn__TimelineId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStartTime()
	{
		return __pbn__StartTime.HasValue;
	}

	public void ResetStartTime()
	{
		__pbn__StartTime = null;
	}

	public bool ShouldSerializeTimelineId()
	{
		return __pbn__TimelineId.HasValue;
	}

	public void ResetTimelineId()
	{
		__pbn__TimelineId = null;
	}
}
