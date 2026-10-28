using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenWorldServerLocalMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "started_timer_id_to_time_stamp_list")]
	public List<TimerIDToTimeStamp> StartedTimerIdToTimeStampLists { get; } = new List<TimerIDToTimeStamp>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
