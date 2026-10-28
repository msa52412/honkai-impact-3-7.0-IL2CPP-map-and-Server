using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MasterPupilMsgNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 589
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private MasterPupilEventType? __pbn__Event;

	[ProtoMember(1, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	[ProtoMember(2, Name = "event")]
	[DefaultValue(MasterPupilEventType.MasterPupilEventGetPupil)]
	public MasterPupilEventType Event
	{
		get
		{
			return __pbn__Event ?? MasterPupilEventType.MasterPupilEventGetPupil;
		}
		set
		{
			__pbn__Event = value;
		}
	}

	[ProtoMember(3, Name = "data")]
	public MasterPupilEventData Data { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializeEvent()
	{
		return __pbn__Event.HasValue;
	}

	public void ResetEvent()
	{
		__pbn__Event = null;
	}
}
