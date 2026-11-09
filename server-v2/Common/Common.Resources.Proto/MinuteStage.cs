using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MinuteStage : IExtensible
{
	[ProtoContract]
	public enum StageState
	{
		[ProtoEnum(Name = "LOCKED")]
		Locked,
		[ProtoEnum(Name = "UNLOCK")]
		Unlock,
		[ProtoEnum(Name = "FINISH")]
		Finish
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MinuteId;

	private StageState? __pbn__State;

	[ProtoMember(1, Name = "minute_id")]
	public uint MinuteId
	{
		get
		{
			return __pbn__MinuteId.GetValueOrDefault();
		}
		set
		{
			__pbn__MinuteId = value;
		}
	}

	[ProtoMember(2, Name = "state")]
	[DefaultValue(StageState.Locked)]
	public StageState State
	{
		get
		{
			return __pbn__State.GetValueOrDefault();
		}
		set
		{
			__pbn__State = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMinuteId()
	{
		return __pbn__MinuteId.HasValue;
	}

	public void ResetMinuteId()
	{
		__pbn__MinuteId = null;
	}

	public bool ShouldSerializeState()
	{
		return __pbn__State.HasValue;
	}

	public void ResetState()
	{
		__pbn__State = null;
	}
}
